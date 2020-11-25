using CC01.BO;
using CC01.BLL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;

namespace CC01.WinForms
{
    public partial class frmCreerEtudiant : Form
    {
        private Action callBack;
        private Etudiant oldEtudiant;

        public frmCreerEtudiant(Action callBack) : this()
        {
            this.callBack = callBack;
        }
        public frmCreerEtudiant(Etudiant etudiant, Action callBack) : this(callBack)
        {
            this.oldEtudiant = etudiant;
            textBoxNom.Text = etudiant.Nom;
            textBoxPrenom.Text = etudiant.Prenom;
            textBoxMatricule.Text = etudiant.Matricule;
            dateTimePicker1.Text = etudiant.DateNaissance.ToString();
            textBoxLieuNaiss.Text = etudiant.LieuNaissance;
            textBoxContact.Text = etudiant.Contact.ToString();
            textBoxEmail.Text = etudiant.Email;
            if (etudiant.Photo != null)
                pictureBox1.Image = Image.FromStream(new MemoryStream(etudiant.Photo));
        }
        public frmCreerEtudiant()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try {
                checkForm();
                string filename = null;
                if (!string.IsNullOrEmpty(pictureBox1.ImageLocation))//hum
                {
                    string ext = Path.GetExtension(pictureBox1.ImageLocation);
                    filename = Guid.NewGuid().ToString() + ext;
                    FileInfo fileSource = new FileInfo(pictureBox1.ImageLocation);
                    string filePath = Path.Combine(ConfigurationManager.AppSettings["DbFolder"], "logo", filename);
                    FileInfo fileDest = new FileInfo(filePath);
                    if (!fileDest.Directory.Exists)
                        fileDest.Directory.Create();
                    fileSource.CopyTo(fileDest.FullName);
                }
                Etudiant newEtudiant = new Etudiant(
                    textBoxNom.Text,
                    textBoxPrenom.Text,
                    dateTimePicker1.Text,
                    textBoxLieuNaiss.Text,
                    textBoxMatricule.Text.ToUpper(),
                    long.Parse(textBoxContact.Text),
                    textBoxEmail.Text,
                    !string.IsNullOrEmpty(pictureBox1.ImageLocation) ? File.ReadAllBytes(pictureBox1.ImageLocation) : this.oldEtudiant?.Photo
                    );
                EtudiantBLO eblo = new EtudiantBLO(ConfigurationManager.AppSettings["DbFolder"]);
                if (this.oldEtudiant == null)
                    eblo.CreateEtudiant(newEtudiant);
                else
                    eblo.EditEtudiant(oldEtudiant, newEtudiant);
                MessageBox.Show(
                    "Save done !",
                     "Confirm",
                     MessageBoxButtons.OK,
                     MessageBoxIcon.Information
                    );
                if (callBack != null)
                    callBack();
                if (oldEtudiant != null)
                    Close();
                textBoxMatricule.Clear();
                textBoxNom.Clear();
                textBoxContact.Clear();
                textBoxEmail.Clear();
                textBoxLieuNaiss.Clear();
                textBoxMatricule.Focus();
            }
            catch(DuplicateNameException ex)
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

        private void brnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void checkForm()
        {
            string text = string.Empty;
            textBoxMatricule.BackColor = Color.White;
            textBoxNom.BackColor = Color.White;
            if (string.IsNullOrWhiteSpace(textBoxMatricule.Text))
            {
                text += "- Please enter the reference ! \n";
                textBoxMatricule.BackColor = Color.Pink;
            }
            if (string.IsNullOrWhiteSpace(textBoxNom.Text))
            {
                text += "- Please enter the name ! \n";
                textBoxNom.BackColor = Color.Pink;
            }

            if (!string.IsNullOrEmpty(text))
                throw new TypingException(text);
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Choose a picture";
            ofd.Filter = "Image files|*.jpg;*.jpeg;*.png;*.gif";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.ImageLocation = ofd.FileName;
            }
        }
    }
}
