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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace hastane
{
    public partial class doktorform : Form
    {
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)] // Tasarımcı serileştirmesini engelle
        [Browsable(false)] // Özelliğin tasarımcıda görünmesini engelle
        public int DoktorNo { get; set; }
        public doktorform()
        {
            InitializeComponent();
        }

        private void ShowTeshisByDoktorNo(int doktorNo)
        {
            string connStr = "Server=localhost;Database=hastane;Uid=root;Pwd='';";
            string doktorQuery = "SELECT Doktor_ID FROM doktor WHERE Doktor_NO = " + doktorNo + "";
            string teshisQuery = "SELECT * FROM teshis WHERE Doktor_ID = @Doktor_ID";

            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                try
                {
                    conn.Open();

                    // Doktor ID'sini bul
                    int doktorID = -1;
                    using (MySqlCommand cmd = new MySqlCommand(doktorQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@Doktor_NO", doktorNo);
                        object result = cmd.ExecuteScalar();

                        if (result != null)
                        {
                            doktorID = Convert.ToInt32(result);
                        }
                        else
                        {
                            MessageBox.Show("Doktor bulunamadı!");
                            return;
                        }
                    }

                    // Teşhisleri getir
                    using (MySqlCommand cmd = new MySqlCommand(teshisQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@Doktor_ID", doktorID);
                        MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);

                        dataGridView1.DataSource = dataTable; // Teşhisleri DataGridView'e yükle
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Hata: " + ex.Message);
                }
            }
        }

        private void AddTedavi(int poli, int doktorid, int hastatc, string doktorteshis, string ilac, string tedavisekli)
        {
            string connStr = "Server=localhost;Database=hastane;Uid=root;Pwd='';";
            string query = "INSERT INTO tedavi (Poliklinik_IDd, Doktor_IDd, Hasta_TCc, Doktor_Teshis, Ilac, Tedavi_Sekli) VALUES" +
                " (@Poliklinik_IDd, @Doktor_IDd, @Hasta_TCc, @Doktor_Teshis, @Ilac, @Tedavi_Sekli)";

            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                try
                {
                    conn.Open();

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Poliklinik_IDd", poli);
                        cmd.Parameters.AddWithValue("@Doktor_IDd", doktorid);
                        cmd.Parameters.AddWithValue("@Hasta_TCc", hastatc);
                        cmd.Parameters.AddWithValue("@Doktor_Teshis", doktorteshis);
                        cmd.Parameters.AddWithValue("@Ilac", ilac);
                        cmd.Parameters.AddWithValue("@Tedavi_Sekli", tedavisekli);
                        cmd.ExecuteNonQuery();

                        MessageBox.Show("Tedavi başarıyla eklendi!");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Hata: " + ex.Message);
                }
            }
        }

        private void doktorform_Load(object sender, EventArgs e)
        {
            if (DoktorNo > 0)
            {
                ShowTeshisByDoktorNo(DoktorNo);
            }
            else
            {
                MessageBox.Show("Doktor numarası geçersiz!");
            }
            ShowTedaviByDoktorID(DoktorNo);

        }

        private void LoadData()
        {
            string connStr = "Server=localhost;Database=hastane;Uid=root;Pwd='';";
            string query = "SELECT * FROM tedavi";

            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                try
                {
                    conn.Open();
                    MySqlDataAdapter dataAdapter = new MySqlDataAdapter(query, conn);
                    DataTable dataTable = new DataTable();
                    dataAdapter.Fill(dataTable);
                    dataGridView2.DataSource = dataTable;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Hata: " + ex.Message);
                }
            }
        }

        private void ShowTedaviByDoktorID(int doktorNo)
        {
            string connStr = "Server=localhost;Database=hastane;Uid=root;Pwd='';";
            string doktorQuery = "SELECT Doktor_ID FROM doktor WHERE Doktor_NO = " + doktorNo + "";
            string teshisQuery = "SELECT * FROM tedavi WHERE Doktor_IDd = @Doktor_ID";

            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                try
                {
                    conn.Open();

                    // Doktor ID'sini bul
                    int doktorID = -1;
                    using (MySqlCommand cmd = new MySqlCommand(doktorQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@Doktor_NO", doktorNo);
                        object result = cmd.ExecuteScalar();

                        if (result != null)
                        {
                            doktorID = Convert.ToInt32(result);
                        }
                        else
                        {
                            MessageBox.Show("Doktor bulunamadı!");
                            return;
                        }
                    }

                    // Teşhisleri getir
                    using (MySqlCommand cmd = new MySqlCommand(teshisQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@Doktor_ID", doktorID);
                        MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);

                        dataGridView2.DataSource = dataTable; // Teşhisleri DataGridView'e yükle
                    }
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
            string query = "DELETE FROM tedavi WHERE Tedavi_ID = @Tedavi_ID";

            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                try
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Tedavi_ID", id);
                        cmd.ExecuteNonQuery();
                    }
                    MessageBox.Show("Veri başarıyla silindi!");
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

        private void UpdateData(int id, string doktor_teshis, string ilac, string tedavi_sekli)
        {
            string connStr = "Server=localhost;Database=hastane;Uid=root;Pwd='';";
            string query = "UPDATE tedavi SET Doktor_Teshis = @Doktor_Teshis, Ilac = @Ilac, Tedavi_Sekli = @Tedavi_Sekli WHERE Tedavi_ID = " + id + "";
            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                try
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Tedavi_ID", id);
                        cmd.Parameters.AddWithValue("@Doktor_Teshis", doktor_teshis);
                        cmd.Parameters.AddWithValue("@Ilac", ilac);
                        cmd.Parameters.AddWithValue("@Tedavi_Sekli", tedavi_sekli);
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

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                textBox0.Text = row.Cells["Hasta_TC"].Value.ToString();
                textBox4.Text = row.Cells["Poliklinik_ID"].Value.ToString();
                textBox5.Text = row.Cells["Doktor_ID"].Value.ToString();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int poli = Convert.ToInt32(textBox4.Text);
            int doktorid = Convert.ToInt32(textBox5.Text);
            int hastatc = Convert.ToInt32(textBox0.Text);
            string doktorteshis = textBox1.Text;
            string ilac = textBox2.Text;
            string tedavisekli = textBox3.Text;

            AddTedavi(poli, doktorid, hastatc, doktorteshis, ilac, tedavisekli);
            ShowTedaviByDoktorID(DoktorNo);
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView2.Rows[e.RowIndex];
                textBox1.Text = row.Cells["Doktor_Teshis"].Value.ToString();
                textBox2.Text = row.Cells["Ilac"].Value.ToString();
                textBox3.Text = row.Cells["Tedavi_Sekli"].Value.ToString();
                textBox6.Text = row.Cells["Tedavi_ID"].Value.ToString();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            doktorgiris doktorgiris = new doktorgiris();
            doktorgiris.Show();
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBox6.Text))
            {
                int id = Convert.ToInt32(textBox6.Text);
                DeleteData(id);
                ShowTedaviByDoktorID(DoktorNo);
            }
            else
            {
                MessageBox.Show("Lütfen silinecek bir satır seçin.");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(textBox6.Text);
            string doktor_teshis = textBox1.Text;
            string ilac = textBox2.Text;
            string tedavi_sekli = textBox3.Text;

            if (string.IsNullOrEmpty(textBox6.Text) || string.IsNullOrEmpty(textBox1.Text) || string.IsNullOrWhiteSpace(textBox2.Text) || string.IsNullOrWhiteSpace(textBox3.Text))
            {
                MessageBox.Show("Lütfen tüm alanları doldurun.");
                return;
            }
            UpdateData(id, doktor_teshis, ilac, tedavi_sekli);
            ShowTedaviByDoktorID(DoktorNo);
        }
    }
}
