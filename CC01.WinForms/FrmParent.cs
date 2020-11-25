using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CC01.WinForms
{
    public partial class FrmParent : Form
    {
        public FrmParent()
        {
            InitializeComponent();
        }

        private void ecoleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmEcole f = new frmEcole();
            f.Show();
        }

        private void etudiantToolStripMenuItem_Click(object sender, EventArgs e)
        {
           Form f = new frmEtudiant();
            f.Show();
        }

        private void quitterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void creerToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}
