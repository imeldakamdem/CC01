using CC01.BLL;
using CC01.BO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CC01.WinForms
{
    public partial class FormSchoolList : Form
    {
        private SchoolBLO schoolBLO;
                public FormSchoolList()
        {
            InitializeComponent();
            dataGridView1.AutoGenerateColumns = false;
            schoolBLO = new SchoolBLO(ConfigurationManager.AppSettings["DbFolder"]);
        }
        
        private void loadData()
        {
            string value = txtSearch.Text.ToLower();
            var schools = schoolBLO.GetBy
            (
                x =>
                x.NameSchool.Contains(value) ||
                x.EmailSchool.ToLower().Contains(value)
            ).OrderBy(x => x.NameSchool).ToArray();
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = schools;
            dataGridView1.ClearSelection();
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            Form f = new FormSchoolEdit(loadData);
            f.Show();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                if (
                    MessageBox.Show
                    (
                        "Do you really want to delete this university(s)?",
                        "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Question
                    ) == DialogResult.Yes
                )
                {
                    for (int i = 0; i < dataGridView1.SelectedRows.Count; i++)
                    {
                        schoolBLO.DeleteSchool(dataGridView1.SelectedRows[i].DataBoundItem as School);
                    }
                    loadData();
                }
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtSearch.Text))
                loadData();
            else
                txtSearch.Clear();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                for (int i = 0; i < dataGridView1.SelectedRows.Count; i++)
                {
                    Form f = new FormSchoolEdit
                    (
                        dataGridView1.SelectedRows[i].DataBoundItem as School,
                        loadData
                    );
                    f.ShowDialog();
                }
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
