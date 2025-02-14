using System.Data;
using System.Data.SQLite;
using System.Text;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using Excel = Microsoft.Office.Interop.Excel;

namespace PlakaKayitUygulamasi
{
    public partial class Form1 : Form
    {

        private string connectionString = "Data Source=PlakaKayitlari.db;Version=3;";

        public Form1()
        {
            InitializeComponent();
            InitializeDatabaseSqlLite();
            this.Size = new System.Drawing.Size(1920, 1080); // Adjust the form size to 1920x1080
        }
       
        private void Form1_Load(object sender, EventArgs e)
        {
            this.Size = new System.Drawing.Size(1920, 1200);
            this.StartPosition = FormStartPosition.CenterScreen;


            // ToolbarControl nesnesini oluþtur ve formun üstüne ekle
            var toolbar = new ToolbarControl();
            toolbar.SetParentForm(this); // Formu toolbar'a ver
            toolbar.NewClicked += Toolbar_NewClicked;
            toolbar.SaveClicked += Toolbar_SaveClicked;
            toolbar.EditClicked += Toolbar_EditClicked;
            toolbar.DeleteClicked += Toolbar_DeleteClicked;
            toolbar.SearchClicked += Toolbar_SearchClicked;
            this.Controls.Add(toolbar);
            toolbar.Dock = DockStyle.Top;

            // Toolbar yüksekliði kadar formdaki diðer kontrolleri aþaðý kaydýr
            int toolbarHeight = toolbar.Height;

            foreach (Control control in this.Controls)
            {
                if (control != toolbar)
                {
                    control.Top += toolbarHeight;
                }
            }

            // Formdaki tüm kontrolleri devre dýþý býrak
            toolbar.DisableAllControlsExceptSearch(this,false);
        }
        private void Toolbar_NewClicked(object sender, EventArgs e)
        {
            var toolbar = sender as ToolbarControl;
            (this.Controls.OfType<ToolbarControl>().FirstOrDefault())?.DisableAllControlsExceptSearch(this, true);
        }

        private void Toolbar_SaveClicked(object sender, EventArgs e)
        {
            MessageBox.Show("Veri kaydedildi!");
        }

        private void Toolbar_EditClicked(object sender, EventArgs e)
        {
            MessageBox.Show("Veri güncellendi!");
        }

        private void Toolbar_DeleteClicked(object sender, EventArgs e)
        {
            MessageBox.Show("Veri silindi!");
        }

        private void Toolbar_SearchClicked(object sender, EventArgs e)
        {
            MessageBox.Show("Arama yapýldý!");
        }
        private void InitializeDatabaseSqlLite()
        {
            try
            {
                using (var connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();
                    string createTableQuery = @"
                CREATE TABLE IF NOT EXISTS PlakaKayitlari (
                    Plaka TEXT PRIMARY KEY,
                    TelefonNo TEXT,
                    AdSoyad TEXT,
                    Marka TEXT,
                    Model TEXT,
                    Renk TEXT,
                    AracGelisTarihi TEXT,
                    AracGelisKM TEXT,
                    TeslimTarihi TEXT,
                    YapilanIsler TEXT,
                    Tarih TEXT
                );";
                    using (var command = new SQLiteCommand(createTableQuery, connection))
                    {
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Veritabaný baþlatma hatasý: {ex.Message}");
            }
        }
        private void KaydetButonu_Click(object sender, EventArgs e)
        {
            string plaka = textBoxPlaka?.Text;
            string telefonNo = textBoxTelefon?.Text;
            string adSoyad = textBoxAdSoyad?.Text;
            string marka = textBoxMarka?.Text;
            string model = textBoxModel?.Text;
            string renk = textBoxRenk?.Text;
            string gelisTarihi = textBoxGelisTarihi?.Text;
            string gelisKM = textBoxGelisKM?.Text;
            string teslimTarihi = textBoxTeslimTarihi?.Text;
            string yapilanIsler = textBoxYapilanIsler?.Text;
            string tarih = DateTime.Now.ToString("dd/MM/yyyy HH:mm");

            try
            {
                using (var connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();

                    // Önce plakayý kontrol et
                    string checkQuery = "SELECT COUNT(*) FROM PlakaKayitlari WHERE Plaka = @Plaka";
                    using (var checkCommand = new SQLiteCommand(checkQuery, connection))
                    { 
                        checkCommand.Parameters.AddWithValue("@Plaka", plaka);
                        int count = Convert.ToInt32(checkCommand.ExecuteScalar());

                        if (count > 0)
                        {
                            MessageBox.Show("Bu plaka zaten sistemde kayýtlý!", "Uyarý", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return; // Ýþlemi sonlandýr
                        }
                    }
                    // Eðer plaka yoksa ekleme iþlemini yap


                    string insertQuery = @"INSERT INTO PlakaKayitlari (Plaka, TelefonNo, AdSoyad, Marka, Model, Renk, AracGelisTarihi, AracGelisKM, TeslimTarihi, YapilanIsler, Tarih)
                                                               VALUES (@Plaka, @TelefonNo, @AdSoyad, @Marka, @Model, @Renk, @AracGelisTarihi, @AracGelisKM, @TeslimTarihi, @YapilanIsler, @Tarih)";

                    using (var command = new SQLiteCommand(insertQuery, connection))
                    {
                        command.Parameters.AddWithValue("@Plaka", plaka);
                        command.Parameters.AddWithValue("@TelefonNo", telefonNo);
                        command.Parameters.AddWithValue("@AdSoyad", adSoyad);
                        command.Parameters.AddWithValue("@Marka", marka);
                        command.Parameters.AddWithValue("@Model", model);
                        command.Parameters.AddWithValue("@Renk", renk);
                        command.Parameters.AddWithValue("@AracGelisTarihi", gelisTarihi);
                        command.Parameters.AddWithValue("@AracGelisKM", gelisKM);
                        command.Parameters.AddWithValue("@TeslimTarihi", teslimTarihi);
                        command.Parameters.AddWithValue("@YapilanIsler", yapilanIsler);
                        command.Parameters.AddWithValue("@Tarih", tarih);
                        command.ExecuteNonQuery();
                    }
                }
                MessageBox.Show("Kayýt baþarýyla eklendi!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                TemizleForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Veritabanýna yazma hatasý: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private DataGridView dataGridViewResults;
        private DataTable messageTable;
        private int pLineNo = 0;
        public void LoadDataTable()
        {
            int pLineNo = 0;
        // DataGridView ayarlarý
            messageGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            messageGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // DataGridView'in formla birlikte büyüyüp küçülmesini saðlamak için Anchor kullanýyoruz
            messageGridView.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            //messageGridView.Location = new System.Drawing.Point(10, 100); // GridView pozisyonu
            messageGridView.Size = new System.Drawing.Size(this.ClientSize.Width - 20, this.ClientSize.Height - 150); // Dinamik boyutlandýrma

            // DataTable oluþtur ve sütunlarý ekle
            messageTable = new DataTable();
            messageTable.Columns.Add("SiraNo", typeof(int));
            messageTable.Columns.Add("Plaka", typeof(string));
            messageTable.Columns.Add("TelefonNo", typeof(string)); // DateTime yerine string olacak
            messageTable.Columns.Add("AdSoyad", typeof(string));
            messageTable.Columns.Add("Marka", typeof(string));
            messageTable.Columns.Add("Model", typeof(string));
            messageTable.Columns.Add("Renk", typeof(string));
            messageTable.Columns.Add("AracGelisTarihi", typeof(string));
            messageTable.Columns.Add("AracGelisKM", typeof(string));
            messageTable.Columns.Add("TeslimTarihi", typeof(string));
            messageTable.Columns.Add("YapilanIsler", typeof(string));
            messageTable.Columns.Add("Tarih", typeof(string));


            // DataGridView'e DataTable'ý baðla
            messageGridView.DataSource = messageTable;

            //// Sütun geniþliðini ayarlamak için veri baðlama iþlemini bekle
            //messageGridView.DataBindingComplete += (sender, e) =>
            //{
            //    // Veri baðlandýktan sonra sütun geniþliklerini ayarlýyoruz
            //    messageGridView.Columns["LineNo"].Width = 50;  // 100 piksel geniþlik
            //    messageGridView.Columns["Timestamp"].Width = 130;  // 100 piksel geniþlik
            //    messageGridView.Columns["FileName"].Width = 145;  // 100 piksel geniþlik
            //    messageGridView.Columns["Message"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill; // Mesaj kolonunu doldur
            //};
        }

        private void PlakaAratButonu_Click(object sender, EventArgs e)
        {
            string plakaArama = textBoxPlakaArama?.Text;
            if (string.IsNullOrEmpty(plakaArama))
            {
                MessageBox.Show("Lütfen bir plaka girin.");
                return;
            }

            richTextBoxAramaSonucu.Clear();
            bool found = false;

            try
            {
                using (var connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT * FROM PlakaKayitlari WHERE Plaka = @Plaka";
                    using (var command = new SQLiteCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Plaka", plakaArama);
                        using (var reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                richTextBoxAramaSonucu.AppendText("Plaka: " + reader["Plaka"] + Environment.NewLine);
                                richTextBoxAramaSonucu.AppendText("Telefon No: " + reader["TelefonNo"] + Environment.NewLine);
                                richTextBoxAramaSonucu.AppendText("Ad Soyad: " + reader["AdSoyad"] + Environment.NewLine);
                                richTextBoxAramaSonucu.AppendText("Marka: " + reader["Marka"] + Environment.NewLine);
                                richTextBoxAramaSonucu.AppendText("Model: " + reader["Model"] + Environment.NewLine);
                                richTextBoxAramaSonucu.AppendText("Renk: " + reader["Renk"] + Environment.NewLine);
                                richTextBoxAramaSonucu.AppendText("Araç Geliþ Tarihi: " + reader["AracGelisTarihi"] + Environment.NewLine);
                                richTextBoxAramaSonucu.AppendText("Araç Geliþ KM: " + reader["AracGelisKM"] + Environment.NewLine);
                                richTextBoxAramaSonucu.AppendText("Teslim Tarihi: " + reader["TeslimTarihi"] + Environment.NewLine);
                                richTextBoxAramaSonucu.AppendText("Yapýlan Ýþler: " + reader["YapilanIsler"] + Environment.NewLine);
                                found = true;
                            }
                        }
                    }
                }

                if (!found)
                {
                    richTextBoxAramaSonucu.AppendText("Plaka bulunamadý.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Veritabaný okuma hatasý: {ex.Message}");
            }

            AddRecordToList(plakaArama);



        }
        private void AddRecordToList(string xplakaArama)
        {
            LoadDataTable();
            bool found = false;
            try
            {
                using (var connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();

                    // Plaka içinde arama yapmak için LIKE kullanýmý
                    string query = "SELECT * FROM PlakaKayitlari WHERE Plaka LIKE @Plaka";

                    using (var command = new SQLiteCommand(query, connection))
                    {
                        // Kullanýcýnýn girdiði deðeri % iþaretleriyle çevrele
                        command.Parameters.AddWithValue("@Plaka", "%" + xplakaArama + "%");

                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                AddRecord(reader["Plaka"].ToString(),
                                         reader["TelefonNo"].ToString(),
                                         reader["AdSoyad"].ToString(),
                                         reader["Marka"].ToString(),
                                         reader["Model"].ToString(),
                                         reader["Renk"].ToString(),
                                         reader["AracGelisTarihi"].ToString(),
                                         reader["AracGelisKM"].ToString(),
                                         reader["TeslimTarihi"].ToString(),
                                         reader["YapilanIsler"].ToString(),
                                         reader["Tarih"].ToString());
                                found = true;
                            }
                        }
                    }
                }

                if (!found)
                {
                    richTextBoxAramaSonucu.AppendText("Plaka bulunamadý.");
                    MessageBox.Show($"Plaka bulunamadý.{xplakaArama}");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Veritabaný okuma hatasý: {ex.Message}");
            }
        }
        private void AddRecord(string xPlaka, 
                               string xTelefonNo = "",
                               string xAdSoyad = "",
                               string xMarka ="", 
                               string xModel = "",
                               string xRenk = "",
                               string xAracGelisTarihi = "",
                               string xAracGelisKM = "",
                               string xTeslimTarihi = "",
                               string xYapilanIsler = "",
                               string xTarih ="")
        {
            if (messageTable == null) // Null kontrolü
            {
                throw new NullReferenceException("messageTable nesnesi null.");
            }

            
            pLineNo++;
            DataRow newRow = messageTable.NewRow();
            newRow["SiraNo"] = pLineNo;
            newRow["Plaka"] = xPlaka;
            newRow["TelefonNo"] = xTelefonNo;
            newRow["AdSoyad"] = xAdSoyad;
            newRow["Marka"] = xMarka;
            newRow["Model"] = xModel;
            newRow["Renk"] = xRenk;
            newRow["AracGelisTarihi"] = xAracGelisTarihi;
            newRow["AracGelisKM"] = xAracGelisKM;
            newRow["TeslimTarihi"] = xTeslimTarihi;
            newRow["AracGelisKM"] = xAracGelisKM;
            newRow["Tarih"] = xTarih;

            messageTable.Rows.Add(newRow);

            // Timestamp'e göre ters sýrada sýralama yap
            messageTable.DefaultView.Sort = "Tarih DESC";
        }
        private void TemizleForm()
        {
            textBoxPlaka.Clear();
            textBoxTelefon.Clear();
            textBoxAdSoyad.Clear();
            textBoxMarka.Clear();
            textBoxModel.Clear();
            textBoxRenk.Clear();
            textBoxGelisKM.Clear();
            textBoxYapilanIsler.Clear();
            textBoxGelisTarihi.Clear();
            textBoxTeslimTarihi.Clear();
        }
    }
}