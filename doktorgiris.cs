using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace hastane
{
    public partial class doktorgiris : Form
    {
        public doktorgiris()
        {
            InitializeComponent();
        }

        private void DoctorLogin()
        {
            string connStr = "Server=localhost;Database=hastane;Uid=root;Pwd='';";
            int doktorNo;

            if (int.TryParse(textBox1.Text, out doktorNo))
            {
                string query = "SELECT COUNT(*) FROM doktor WHERE Doktor_NO = @Doktor_NO";

                using (MySqlConnection conn = new MySqlConnection(connStr))
                {
                    try
                    {
                        conn.Open();
                        using (MySqlCommand cmd = new MySqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@Doktor_NO", doktorNo);

                            int count = Convert.ToInt32(cmd.ExecuteScalar());

                            if (count > 0)
                            {
                                MessageBox.Show("Giriş yapıldı!");

                                // DoktorForm'a geçiş yap
                                doktorform doktorform = new doktorform();
                                doktorform.DoktorNo = doktorNo; // Doktor numarasını aktar
                                doktorform.Show();
                                this.Hide();
                            }
                            else
                            {
                                MessageBox.Show("Hatalı Doktor Numarası!");
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Hata: " + ex.Message);
                    }
                }
            }
            else
            {
                MessageBox.Show("Lütfen geçerli bir Doktor Numarası girin.");
            }
        }


        private void doktorgiris_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            DoctorLogin();
        }
    }
}
