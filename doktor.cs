using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace hastane
{
    public partial class doktor : Form
    {
        public doktor()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            doktorekle doktorekle = new doktorekle();
            doktorekle.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            form1.Show();
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            doktorgiris doktorgiris = new doktorgiris();
            doktorgiris.Show();
            this.Hide();
        }
    }
}
