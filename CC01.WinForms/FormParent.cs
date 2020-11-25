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
    public partial class FormParent : Form
    {
        public FormParent()
        {
            InitializeComponent();
        }

        private void FormParent_Click(object sender, EventArgs e)
        {

        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void schoolToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            Form f = new FormSchoolList();
            f.Show();
        }

        private void studentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form f = new FormStudentList();
            f.Show();
           // f.WindowState = FormWindowState.Maximized;
        }

        private void printToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormPreview form = new FormPreview();
            form.Show();
        }

        private void backToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form f = new FormStudentEdit();
            f.Show();
        }

        private void editToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Form f = new FormSchoolEdit();
            f.Show();
        }

        //private void PrintToolStripMenuItem_Click(object sender, EventArgs e)
        //{
        //    FrmPreview form = new FrmPreview();
        //    form.FormParent = this;
        //    form.Show();
        //}
    }
}
