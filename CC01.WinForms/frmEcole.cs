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
    public partial class frmEcole : Form
    {
        private EcoleBLO ecoleBLO;

        public frmEcole()
        {
            InitializeComponent();
            dataGridView1.AutoGenerateColumns = false;
            ecoleBLO = new EcoleBLO(ConfigurationManager.AppSettings["DbFolder"]);
        }
        private void loadData()
        {
            string value = textBoxSearch.Text.ToLower();
            var etudiants = ecoleBLO.GetBy
            (x =>
             x.Intitule.ToLower().Contains(value)
            ).OrderBy(x => x.Intitule).ToArray();
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = etudiants;
            dataGridView1.ClearSelection();
        }

        private void textBoxSearch_TextChanged(object sender, EventArgs e)
        {
            loadData();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            frmCreerEcole f = new frmCreerEcole(loadData);
            //Close();
            f.Show();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                if (
                    MessageBox.Show
                    (
                        "Do you really want to delete this product(s)?",
                        "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Question
                    ) == DialogResult.Yes
                )
                {
                    for (int i = 0; i < dataGridView1.SelectedRows.Count; i++)
                    {
                        ecoleBLO.DeleteEtudiant(dataGridView1.SelectedRows[i].DataBoundItem as Ecole);
                    }
                    loadData();
                }
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                for (int i = 0; i < dataGridView1.SelectedRows.Count; i++)
                {
                    Form f = new frmCreerEcole
                    (
                        dataGridView1.SelectedRows[i].DataBoundItem as Ecole,
                        loadData
                    );
                    f.ShowDialog();
                }
            }
        }

        private void brnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxSearch.Text))
                loadData();
            else
                textBoxSearch.Clear();
        }

        private void frmEcole_Load(object sender, EventArgs e)
        {
            loadData();
        }
    }
}
