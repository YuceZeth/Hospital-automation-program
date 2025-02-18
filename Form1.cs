using System.Data;

namespace hastane
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            hastagiris hastagiris = new hastagiris();
            hastagiris.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            doktor doktor = new doktor();
            doktor.Show();
            this.Hide();
        }
    }
}
