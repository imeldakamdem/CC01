using System;
using CC01.BLL;
using CC01.BO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Configuration;

namespace CC01.WinForms
{
    public partial class FormStudentList : Form
    {
        private StudentBLO studentBLO;
        private SchoolBLO schoolBLO;
        public FormStudentList()
        {
            InitializeComponent();
            dataGridView1.AutoGenerateColumns = false;
            studentBLO = new StudentBLO(ConfigurationManager.AppSettings["DbFolder"]);
            schoolBLO = new SchoolBLO(ConfigurationManager.AppSettings["DbFolder"]);
        }
        private void loadData()
        {
            string value = txtSearch.Text.ToLower();
            var students = studentBLO.GetBy
            (
                x =>
                x.Matricule.ToLower().Contains(value) ||
                x.Nom.ToLower().Contains(value)
            ).OrderBy(x => x.Matricule).ToArray();
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = students;
            dataGridView1.ClearSelection();
        }
        private void FrmStudentList_Load(object sender, EventArgs e)
        {
            loadData();
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            Form f = new FormStudentEdit(loadData);
            f.Show();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                for (int i = 0; i < dataGridView1.SelectedRows.Count; i++)
                {
                    Form f = new FormStudentEdit
                    (
                        dataGridView1.SelectedRows[i].DataBoundItem as Student,
                        loadData
                    );
                    f.ShowDialog();
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                if (
                    MessageBox.Show
                    (
                        "Do you really want to delete this student(s)?",
                        "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Question
                    ) == DialogResult.Yes
                )
                {
                    for (int i = 0; i < dataGridView1.SelectedRows.Count; i++)
                    {
                        studentBLO.DeleteStudent(dataGridView1.SelectedRows[i].DataBoundItem as Student);
                    }
                    loadData();
                }
            }
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtSearch.Text))
                loadData();
            else
                txtSearch.Clear();
        }
        private void btnPrint_Click(object sender, EventArgs e)
        {
            List<StudentListPrint> items = new List<StudentListPrint>();
            Student studentl = studentBLO.Get();
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                Student s = dataGridView1.Rows[i].DataBoundItem as Student;
                items.Add
                (
                new StudentListPrint
                (
                    s.Matricule,
                    s.Nom,
                    s.Prenom,
                    s.Email,
                    s.Photo
                    )
                );
            }
            Form f = new FormPreview("StudentList.rdlc", items);
            f.Show();
        }
        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            loadData();
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            btnEdit_Click(sender, e);
        }

        private void FormStudentList_Load(object sender, EventArgs e)
        {
            loadData();
        }
    }

}
