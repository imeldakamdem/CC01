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
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace CC01.WinForms
{
    public partial class frmEtudiant : Form
    {
        private EtudiantBLO etudiantBLO;
        
        public frmEtudiant()
        {
            InitializeComponent();
            dataGridView1.AutoGenerateColumns = false;
            etudiantBLO = new EtudiantBLO(ConfigurationManager.AppSettings["DbFolder"]);
        }

        private void loadData()
        {
            string value = textBoxSearch.Text.ToLower();
            var etudiants = etudiantBLO.GetBy
            (   x =>
                x.Matricule.ToLower().Contains(value)||
                x.Nom.ToLower().Contains(value)
            ).OrderBy(x => x.Matricule).ToArray();
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = etudiants;
            dataGridView1.ClearSelection();
        }
        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void frmEtudiant_Load(object sender, EventArgs e)
        {
            loadData();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            loadData();
        }


        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                for (int i = 0; i < dataGridView1.SelectedRows.Count; i++)
                {
                    Form f = new frmCreerEtudiant
                    (
                        dataGridView1.SelectedRows[i].DataBoundItem as Etudiant,
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

        private void btnClear_Click(object sender, EventArgs e)
        {
            
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            frmCreerEtudiant f = new frmCreerEtudiant(loadData);
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
                        etudiantBLO.DeleteEtudiant(dataGridView1.SelectedRows[i].DataBoundItem as Etudiant);
                    }
                    loadData();
                }
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxSearch.Text))
                loadData();
            else
                textBoxSearch.Clear();
        }
        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            btnEdit_Click(sender, e);
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        private void btnPrint_Click_1(object sender, EventArgs e)
        {
            List<EtudiantPrint> items = new List<EtudiantPrint>();
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                Etudiant s = dataGridView1.Rows[i].DataBoundItem as Etudiant;
                items.Add
                (
                new EtudiantPrint
                (
                    s.Matricule,
                       s.Nom,
                       s.Prenom,
                       DateTime.Parse(s.DateNaissance),
                       s.Photo
                    )
                );
            }
            FrmPreview form = new FrmPreview("EtudiantCart.rdlc", items);
            form.Show();
        }
    }
}
