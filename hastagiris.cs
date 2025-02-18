using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using MySql.Data;
using MySql.Data.MySqlClient;
using static System.Net.Mime.MediaTypeNames;

namespace hastane
{
    public partial class hastagiris : Form
    {
        public hastagiris()
        {
            InitializeComponent();
        }

        private void LoadData()
        {
            string connStr = "Server=localhost;Database=hastane;Uid=root;Pwd='';";
            string query = "SELECT * FROM hasta";

            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                try
                {
                    conn.Open();
                    MySqlDataAdapter dataAdapter = new MySqlDataAdapter(query, conn);
                    DataTable dataTable = new DataTable();
                    dataAdapter.Fill(dataTable);
                    dataGridView1.DataSource = dataTable;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Hata: " + ex.Message);
                }
            }
        }

        private void AddData(int tc, string ad, string soyad)
        {
            string connStr = "Server=localhost;Database=hastane;Uid=root;Pwd='';";
            string query = "INSERT INTO hasta (Hasta_TC, Hasta_AD, Hasta_SOYAD) VALUES (@Hasta_TC, @Hasta_AD, @Hasta_SOYAD)";

            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                try
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Hasta_TC", tc);
                        cmd.Parameters.AddWithValue("@Hasta_AD", ad);
                        cmd.Parameters.AddWithValue("@Hasta_SOYAD", soyad);
                        cmd.ExecuteNonQuery();
                    }

                    MessageBox.Show("Veri başarıyla eklendi!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Hata: " + ex.Message);
                }
            }
        }

        private void DeleteData(int id)
        {
            string connStr = "Server=localhost;Database=hastane;Uid=root;Pwd='';";
            string query = "DELETE FROM hasta WHERE Hasta_TC = @Hasta_TC";

            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                try
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Hasta_TC", id);
                        cmd.ExecuteNonQuery();
                    }
                    MessageBox.Show("Veri başarıyla silindi!");
                    textBox1.Clear();
                    textBox2.Clear();
                    textBox3.Clear();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Hata: hastanın tedavisi devam ediyor");
                }
            }
        }

        private void UpdateData(int id, long tc, string ad, string soyad)
        {
            string connStr = "Server=localhost;Database=hastane;Uid=root;Pwd='';";
            string query = "UPDATE hasta SET Hasta_TC = @Hasta_TC, Hasta_AD = @Hasta_AD, Hasta_SOYAD = @Hasta_SOYAD WHERE Hasta_ID = @Hasta_ID";
            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                try
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Hasta_ID", id);
                        cmd.Parameters.AddWithValue("@Hasta_TC", tc);
                        cmd.Parameters.AddWithValue("@Hasta_AD", ad);
                        cmd.Parameters.AddWithValue("@Hasta_SOYAD", soyad);
                        cmd.ExecuteNonQuery();
                    }
                    MessageBox.Show("Veri başarıyla güncellendi!");
                    textBox1.Clear();
                    textBox2.Clear();
                    textBox3.Clear();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Hata: " + ex.Message);
                }
            }
        }

        private void LoadPoliklinikler()
        {
            string connStr = "Server=localhost;Database=hastane;Uid=root;Pwd='';";
            string query = "SELECT Poliklinik_ID, Poliklinik_AD FROM poliklinik";

            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                try
                {
                    conn.Open(); // Bağlantıyı açıyoruz

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        MySqlDataReader reader = cmd.ExecuteReader();
                        comboBox1.Items.Clear();

                        while (reader.Read())
                        {
                            int poliklinikID = reader.GetInt32("Poliklinik_ID");
                            string poliklinikAdi = reader.GetString("Poliklinik_AD");
                            comboBox1.Items.Add(new { ID = poliklinikID, Ad = poliklinikAdi });
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Hata: " + ex.Message);
                }
            }
        }

        private void LoadDoktorlar(int poliklinikID)
        {
            string connStr = "Server=localhost;Database=hastane;Uid=root;Pwd='';";
            string query = "SELECT Doktor_ID, Doktor_AD FROM doktor WHERE Doktor_POLIKLNIK = " + poliklinikID + "";

            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                try
                {
                    conn.Open(); // Bağlantıyı açıyoruz

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        // Parametreyi ekliyoruz
                        cmd.Parameters.AddWithValue("@Doktor_POLIKLINIK", poliklinikID);

                        MySqlDataReader reader = cmd.ExecuteReader();

                        comboBox2.Items.Clear();

                        while (reader.Read())
                        {
                            int doktorID = reader.GetInt32("Doktor_ID");
                            string doktorAdi = reader.GetString("Doktor_AD");
                            comboBox2.Items.Add(new { ID = doktorID, Ad = doktorAdi });
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Hata: " + ex.Message);
                }
            }
        }

        private void AddTeshisToDatabase(long tc, int poliklinikID, int doktorID, string teshis)
        {
            string connStr = "Server=localhost;Database=hastane;Uid=root;Pwd='';";
            string query = "INSERT INTO teshis (Teshis, Poliklinik_ID, Doktor_ID, Hasta_TC) VALUES (@Teshis, @Poliklinik_ID, @Doktor_ID, @Hasta_TC)";

            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                try
                {
                    conn.Open();

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Teshis", teshis);
                        cmd.Parameters.AddWithValue("@Poliklinik_ID", poliklinikID);
                        cmd.Parameters.AddWithValue("@Doktor_ID", doktorID);
                        cmd.Parameters.AddWithValue("@Hasta_TC", tc);

                        cmd.ExecuteNonQuery();

                        MessageBox.Show("Teşhis başarıyla kaydedildi!");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Hata: " + ex.ToString());
                }
            }
        }


        private void button1_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            form1.Show();
            this.Close();
        }


        private void hastagiris_Load(object sender, EventArgs e)
        {
            LoadData();
            LoadPoliklinikler();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            int tc = Convert.ToInt32(textBox1.Text);
            string ad = textBox2.Text;
            string soyad = textBox3.Text;
            AddData(tc, ad, soyad);
            LoadData();
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                textBox0.Text = row.Cells["Hasta_ID"].Value.ToString();
                textBox1.Text = row.Cells["Hasta_TC"].Value.ToString();
                textBox2.Text = row.Cells["Hasta_AD"].Value.ToString();
                textBox3.Text = row.Cells["Hasta_SOYAD"].Value.ToString();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBox1.Text))
            {
                int tc = Convert.ToInt32(textBox1.Text);
                DeleteData(tc);
                LoadData();
            }
            else
            {
                MessageBox.Show("Lütfen silinecek bir satır seçin.");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(textBox0.Text);
            long tc = Convert.ToInt64(textBox1.Text);
            string ad = textBox2.Text;
            string soyad = textBox3.Text;

            if (string.IsNullOrEmpty(textBox0.Text) || string.IsNullOrEmpty(textBox1.Text) || string.IsNullOrWhiteSpace(ad) || string.IsNullOrWhiteSpace(soyad))
            {
                MessageBox.Show("Lütfen tüm alanları doldurun.");
                return;
            }
            UpdateData(id, tc, ad, soyad);
            LoadData();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            var selectedPoliklinik = comboBox1.SelectedItem;
            if (selectedPoliklinik != null)
            {

                int poliklinikID = (selectedPoliklinik as dynamic).ID;
                LoadDoktorlar(poliklinikID);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            var selectedPoliklinik = comboBox1.SelectedItem;
            var selectedDoktor = comboBox2.SelectedItem;
            var teshis = textBox4.Text;
            var tcc = textBox1.Text;
            

            if (selectedPoliklinik != null && selectedDoktor != null && teshis != null && tcc != null)
            {
                int poliklinikID = (selectedPoliklinik as dynamic).ID; 
                int doktorID = (selectedDoktor as dynamic).ID;
                long tc = Convert.ToInt64(textBox1.Text);

                AddTeshisToDatabase(tc, poliklinikID, doktorID, teshis);
            }
            else
            {
                MessageBox.Show("Lütfen poliklinik, doktor, teshis ve tc seçin.");
            }
        }
    }
}
