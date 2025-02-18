using System;
using System.Data;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace hastane
{
    public partial class doktorekle : Form
    {
        public doktorekle()
        {
            InitializeComponent();
        }

        private void LoadData()
        {
            string connStr = "Server=localhost;Database=hastane;Uid=root;Pwd='';";
            string query = "SELECT * FROM doktor";

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

        private void LoadBranslar()
        {
            string connStr = "Server=localhost;Database=hastane;Uid=root;Pwd='';";
            string query = "SELECT Brans_ID, Brans_AD FROM brans";

            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                try
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        MySqlDataReader reader = cmd.ExecuteReader();
                        comboBox2.Items.Clear();

                        while (reader.Read())
                        {
                            int bransID = reader.GetInt32("Brans_ID");
                            string bransAdi = reader.GetString("Brans_AD");
                            comboBox2.Items.Add(new KeyValuePair<int, string>(bransID, bransAdi));
                        }
                    }
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
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        MySqlDataReader reader = cmd.ExecuteReader();
                        comboBox1.Items.Clear();

                        while (reader.Read())
                        {
                            int poliklinikID = reader.GetInt32("Poliklinik_ID");
                            string poliklinikAdi = reader.GetString("Poliklinik_AD");
                            comboBox1.Items.Add(new KeyValuePair<int, string>(poliklinikID, poliklinikAdi));
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Hata: " + ex.Message);
                }
            }
        }

        private void AddDoktorToDatabase(string doktorAdi, string doktorSoyadi, int doktorno, int bransID, int poliklinikID)
        {
            string connStr = "Server=localhost;Database=hastane;Uid=root;Pwd='';";
            string query = "INSERT INTO doktor (Doktor_AD, Doktor_SOYAD, Doktor_NO, Doktor_BRANS, Doktor_POLIKLNIK) VALUES (@Doktor_AD, @Doktor_SOYAD, @Doktor_NO, @Doktor_BRANS, @Doktor_POLIKLNIK)";

            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                try
                {
                    conn.Open();

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Doktor_AD", doktorAdi);
                        cmd.Parameters.AddWithValue("@Doktor_SOYAD", doktorSoyadi);
                        cmd.Parameters.AddWithValue("@Doktor_NO", doktorno);
                        cmd.Parameters.AddWithValue("@Doktor_BRANS", bransID); // Doğru parametre ismi kullanılıyor
                        cmd.Parameters.AddWithValue("@Doktor_POLIKLNIK", poliklinikID); // Doğru parametre ismi kullanılıyor
                        cmd.ExecuteNonQuery();

                        MessageBox.Show("Doktor başarıyla eklendi!");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Hata: " + ex.Message);
                }
            }
        }

        private void DeleteSelectedRow()
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];
                int doktorID = Convert.ToInt32(selectedRow.Cells["Doktor_ID"].Value);

                string connStr = "Server=localhost;Database=hastane;Uid=root;Pwd='';";
                string query = "DELETE FROM doktor WHERE Doktor_ID = @Doktor_ID";

                using (MySqlConnection conn = new MySqlConnection(connStr))
                {
                    try
                    {
                        conn.Open();
                        using (MySqlCommand cmd = new MySqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@Doktor_ID", doktorID);
                            cmd.ExecuteNonQuery();

                            MessageBox.Show("Seçili doktor başarıyla silindi!");
                            LoadData();
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
                MessageBox.Show("Lütfen silmek istediğiniz satırı seçin.");
            }
        }

        private void UpdateSelectedRow()
        {
            if (dataGridView1.SelectedRows.Count > 0) // Seçili satır var mı kontrol et
            {
                DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];
                int doktorID = Convert.ToInt32(selectedRow.Cells["Doktor_ID"].Value); // Güncellenecek Doktor ID'sini al

                string doktorAdi = textBox1.Text; // Yeni Doktor Adı
                string doktorSoyadi = textBox2.Text; // Yeni Doktor Soyadı
                int doktorNo;
                int bransID = ((KeyValuePair<int, string>)comboBox1.SelectedItem).Key; // Yeni Branş ID
                int poliklinikID = ((KeyValuePair<int, string>)comboBox2.SelectedItem).Key; // Yeni Poliklinik ID

                if (int.TryParse(textBox3.Text, out doktorNo) &&
                    !string.IsNullOrEmpty(doktorAdi) &&
                    !string.IsNullOrEmpty(doktorSoyadi))
                {
                    string connStr = "Server=localhost;Database=hastane;Uid=root;Pwd='';";
                    string query = "UPDATE doktor SET Doktor_AD = @Doktor_AD, Doktor_SOYAD = @Doktor_SOYAD, Doktor_NO = @Doktor_NO, Doktor_BRANS = @Doktor_BRANS, Doktor_POLIKLNIK = @Doktor_POLIKLNIK WHERE Doktor_ID = @Doktor_ID";

                    using (MySqlConnection conn = new MySqlConnection(connStr))
                    {
                        try
                        {
                            conn.Open();
                            using (MySqlCommand cmd = new MySqlCommand(query, conn))
                            {
                                cmd.Parameters.AddWithValue("@Doktor_ID", doktorID);
                                cmd.Parameters.AddWithValue("@Doktor_AD", doktorAdi);
                                cmd.Parameters.AddWithValue("@Doktor_SOYAD", doktorSoyadi);
                                cmd.Parameters.AddWithValue("@Doktor_NO", doktorNo);
                                cmd.Parameters.AddWithValue("@Doktor_BRANS", bransID);
                                cmd.Parameters.AddWithValue("@Doktor_POLIKLNIK", poliklinikID);

                                cmd.ExecuteNonQuery();

                                MessageBox.Show("Seçili doktor başarıyla güncellendi!");
                                LoadData(); // Güncel tabloyu yükle
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
                    MessageBox.Show("Lütfen tüm alanları eksiksiz ve doğru şekilde doldurun.");
                }
            }
            else
            {
                MessageBox.Show("Lütfen güncellemek istediğiniz satırı seçin.");
            }
        }

        private void doktorekle_Load(object sender, EventArgs e)
        {
            LoadData();
            LoadBranslar();
            LoadPoliklinikler();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            var selectedPoliklinik = comboBox1.SelectedItem;
            var selectedBrans = comboBox2.SelectedItem;
            string doktorAdi = textBox1.Text;
            string doktorSoyadi = textBox2.Text;
            int doktorno;

            if (int.TryParse(textBox3.Text, out doktorno) &&
                selectedPoliklinik != null &&
                selectedBrans != null &&
                !string.IsNullOrEmpty(doktorAdi) &&
                !string.IsNullOrEmpty(doktorSoyadi))
            {
                int poliklinikID = ((KeyValuePair<int, string>)selectedPoliklinik).Key;
                int bransID = ((KeyValuePair<int, string>)selectedBrans).Key;

                try
                {
                    AddDoktorToDatabase(doktorAdi, doktorSoyadi, doktorno, bransID, poliklinikID);
                    LoadData();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Hata: " + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Lütfen tüm bilgileri eksiksiz doldurun.");
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Seçilen satırdaki verileri al
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

                textBox0.Text = row.Cells["Doktor_ID"].Value.ToString();
                textBox1.Text = row.Cells["Doktor_AD"].Value.ToString();
                textBox2.Text = row.Cells["Doktor_SOYAD"].Value.ToString();
                textBox3.Text = row.Cells["Doktor_NO"].Value.ToString();

                int bransID = Convert.ToInt32(row.Cells["Doktor_BRANS"].Value);
                int poliklinikID = Convert.ToInt32(row.Cells["Doktor_POLIKLNIK"].Value);
                foreach (KeyValuePair<int, string> item in comboBox2.Items)
                {
                    if (item.Key == bransID)
                    {
                        comboBox2.SelectedItem = item;
                        break;
                    }
                }
                foreach (KeyValuePair<int, string> item in comboBox1.Items)
                {
                    if (item.Key == poliklinikID)
                    {
                        comboBox1.SelectedItem = item;
                        break;
                    }
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DeleteSelectedRow();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            UpdateSelectedRow();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            doktor doktor = new doktor();
            doktor.Show();
            this.Close();
        }
    }
}
