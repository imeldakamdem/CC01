using CC01.BLL;
using CC01.BO;
using CC01.DAL;
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
    public partial class FormSchoolEdit : Form
    {
        private Action callBack;
        private School oldSchool;
        private SchoolBLO schoolBLO;
        public FormSchoolEdit()
        {
            InitializeComponent();
            schoolBLO = new SchoolBLO(ConfigurationManager.AppSettings["DbFolder"]);
            oldSchool = schoolBLO.GetSchool();
            if (oldSchool != null)
            {
                txtEmail.Text = oldSchool.EmailSchool;
                txtName.Text = oldSchool.NameSchool;
                txtContact.Text = oldSchool.ContactSchool.ToString();
                pictureBox1.ImageLocation = oldSchool.Logo;
            }
        }

        public FormSchoolEdit(Action callBack) : this()
        {
            this.callBack = callBack;
        }

        public FormSchoolEdit(School school, Action callBack) : this(callBack)
        {
            this.oldSchool = school;
            txtName.Text = oldSchool.NameSchool;
            txtLocalisation.Text = oldSchool.Localisation;
            txtEmail.Text = oldSchool.EmailSchool;
            txtContact.Text = oldSchool.ContactSchool.ToString();
            //if (oldSchool.Logo != null)
            // pictureBox1.Image = Image.FromStream(new MemoryStream(oldSchool.Logo));
           
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

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void checkForm()
        {
            string text = string.Empty;
            txtName.BackColor = Color.White;
            txtEmail.BackColor = Color.White;
            if (!long.TryParse(txtContact.Text, out _))
            {
                text += "- Please enter a good phone number ! \n";
                txtName.BackColor = Color.Pink;
            }
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                text += "- Please enter the name ! \n";
                txtEmail.BackColor = Color.Pink;
            }

            if (!string.IsNullOrEmpty(text))
                throw new TypingException(text);
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                School newSchool = new School
                (
                    txtName.Text.ToUpper(),
                    txtLocalisation.Text,
                    long.Parse(txtContact.Text),
                    pictureBox1.ImageLocation
                    );

              SchoolBLO schoolBLO = new SchoolBLO(ConfigurationManager.AppSettings["DbFolder"]);
                            if (this.oldSchool == null)
              {
                    SchoolBLO.CreateSchool(oldSchool, newSchool);
                }
                else
                    SchoolDAO.EditSchool(oldSchool, newSchool);

                MessageBox.Show
                (
                    "Save done !",
                    "Confirmation",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );

                Close();


            }
            catch (TypingException ex)
            {
                MessageBox.Show
               (
                   ex.Message,
                   "Typing error",
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

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }
    }
}
