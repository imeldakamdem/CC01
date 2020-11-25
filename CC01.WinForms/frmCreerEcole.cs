using CC01.BLL;
using CC01.BO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CC01.WinForms
{
    public partial class frmCreerEcole : Form
    {
        private Action callBack;
        private Ecole oldEcole;

        public frmCreerEcole(Action callBack) : this()
        {
            this.callBack = callBack;
        }

        public frmCreerEcole(Ecole ecole, Action callBack) : this(callBack)
        {
            this.oldEcole = ecole;
            textBoxIntitule.Text = ecole.Intitule;
            textBoxTelephone.Text = ecole.Telephone.ToString();
            textBoxEcomail.Text = ecole.Ecomail;
            if (ecole.Logo != null)
                pictureBox2.Image = Image.FromStream(new MemoryStream(ecole.Logo));
        }
        public frmCreerEcole()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                checkForm();
                string filename = null;
                if (!string.IsNullOrEmpty(pictureBox2.ImageLocation))//hum
                {
                    string ext = Path.GetExtension(pictureBox2.ImageLocation);
                    filename = Guid.NewGuid().ToString() + ext;
                    FileInfo fileSource = new FileInfo(pictureBox2.ImageLocation);
                    string filePath = Path.Combine(ConfigurationManager.AppSettings["DbFolder"], "logoE", filename);
                    FileInfo fileDest = new FileInfo(filePath);
                    if (!fileDest.Directory.Exists)
                        fileDest.Directory.Create();
                    fileSource.CopyTo(fileDest.FullName);
                }
                Ecole newEcole = new Ecole(
                    textBoxIntitule.Text.ToUpper(),
                    long.Parse(textBoxTelephone.Text),
                    textBoxEcomail.Text,
                    !string.IsNullOrEmpty(pictureBox2.ImageLocation) ? File.ReadAllBytes(pictureBox2.ImageLocation) : this.oldEcole?.Logo
                    );
                EcoleBLO ecoblo = new EcoleBLO(ConfigurationManager.AppSettings["DbFolder"]);
                if (this.oldEcole == null)
                    ecoblo.CreateEtudiant(newEcole);
                else
                    ecoblo.EditEtudiant(oldEcole, newEcole);
                MessageBox.Show(
                    "Save done !",
                     "Confirm",
                     MessageBoxButtons.OK,
                     MessageBoxIcon.Information
                    );
                if (callBack != null)
                    callBack();
                if (oldEcole != null)
                    Close();
                textBoxIntitule.Clear();
                textBoxEcomail.Clear();
                textBoxTelephone.Clear();
                
            }
            catch (DuplicateNameException ex)
            {
                MessageBox.Show
              (
                  ex.Message,
                  "Duplicate error",
                  MessageBoxButtons.OK,
                  MessageBoxIcon.Warning
              );
            }
            catch (KeyNotFoundException ex)
            {
                MessageBox.Show
               (
                   ex.Message,
                   "Not found error",
                   MessageBoxButtons.OK,
                   MessageBoxIcon.Warning
               );
            }
            catch (Exception ex)
            {
                ex.WriteToFile();
                MessageBox.Show
               (
                   "An error occurred! Please try again later.",
                   "Erreur",
                   MessageBoxButtons.OK,
                   MessageBoxIcon.Error
               );
            }

        }
        private void checkForm()
        {
            string text = string.Empty;
            textBoxIntitule.BackColor = Color.White;
            if (string.IsNullOrWhiteSpace(textBoxIntitule.Text))
            {
                text += "- Please enter the reference ! \n";
                textBoxIntitule.BackColor = Color.Pink;
            }

            if (!string.IsNullOrEmpty(text))
                throw new TypingException(text);
        }

        private void brnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Choose a picture";
            ofd.Filter = "Image files|*.jpg;*.jpeg;*.png;*.gif";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                pictureBox2.ImageLocation = ofd.FileName;
            }
        }

        private void frmCreerEcole_Load(object sender, EventArgs e)
        {
            
        }
    }


}

