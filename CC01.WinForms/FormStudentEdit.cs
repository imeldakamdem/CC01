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
    public partial class FormStudentEdit : Form
    {
           private Action callBack;
           private Student oldStudent;

        public FormStudentEdit()
              {
                   InitializeComponent();
              }
            public FormStudentEdit(Action callBack) : this()
            {
                this.callBack = callBack;
            }

            public FormStudentEdit(Student student, Action callBack) : this(callBack)
            {
                this.oldStudent = student;
            txtMatricule.Text = oldStudent.Matricule;
                txtNom.Text = oldStudent.Nom;
                txtPrenom.Text = oldStudent.Prenom;
                txtDate.Text = oldStudent.DateNais.ToString();
                txtLieu.Text = oldStudent.Lieu;
                txtContact.Text = (oldStudent.Contact).ToString();
                if (oldStudent.Photo != null)
                    pictureBox1.Image = Image.FromStream(new MemoryStream(student.Photo));

            }
            private void btnSave_Click(object sender, EventArgs e)
            {
                try
                {
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
                Student newStudent = new Student
                    (
                        txtMatricule.Text.ToUpper(),
                        txtNom.Text,
                        txtPrenom.Text,
                        txtDate.Text,
                        txtLieu.Text,
                        long.Parse(txtContact.Text),
                        !string.IsNullOrEmpty(pictureBox1.ImageLocation) ? File.ReadAllBytes(pictureBox1.ImageLocation) : this.oldStudent?.Photo
                    );

                    StudentBLO studentBLO = new StudentBLO(ConfigurationManager.AppSettings["DbFolder"]);

                    if (this.oldStudent == null)
                        studentBLO.CreateStudent(newStudent);
                    else
                        studentBLO.EditStudent(oldStudent, newStudent);

                    MessageBox.Show
                    (
                        "Save done !",
                        "Confirmation",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );

                    if (callBack != null)
                        callBack();

                    if (oldStudent != null)
                        Close();

                    txtNom.Clear();
                    txtPrenom.Clear();
                    txtDate.Clear();
                    txtContact.Clear();
                    txtLieu.Clear();

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
            txtMatricule.BackColor = Color.White;
            txtNom.BackColor = Color.White;
            if (string.IsNullOrWhiteSpace(txtMatricule.Text))
            {
                text += "- Please enter the reference ! \n";
                txtMatricule.BackColor = Color.Pink;
            }
            if (string.IsNullOrWhiteSpace(txtNom.Text))
            {
                text += "- Please enter the name ! \n";
                txtNom.BackColor = Color.Pink;
            }

            if (!string.IsNullOrEmpty(text))
                throw new TypingException(text);
        }
        private void btnCancel_Click(object sender, EventArgs e)
            {
                Close();
            }

            private void pictureBoxStudent_Click(object sender, EventArgs e)
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