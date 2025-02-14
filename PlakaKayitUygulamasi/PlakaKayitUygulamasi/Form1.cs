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


            // ToolbarControl nesnesini olu�tur ve formun �st�ne ekle
            var toolbar = new ToolbarControl();
            toolbar.SetParentForm(this); // Formu toolbar'a ver
            toolbar.NewClicked += Toolbar_NewClicked;
            toolbar.SaveClicked += Toolbar_SaveClicked;
            toolbar.EditClicked += Toolbar_EditClicked;
            toolbar.DeleteClicked += Toolbar_DeleteClicked;
            toolbar.SearchClicked += Toolbar_SearchClicked;
            this.Controls.Add(toolbar);
            toolbar.Dock = DockStyle.Top;

            // Toolbar y�ksekli�i kadar formdaki di�er kontrolleri a�a�� kayd�r
            int toolbarHeight = toolbar.Height;

            foreach (Control control in this.Controls)
            {
                if (control != toolbar)
                {
                    control.Top += toolbarHeight;
                }
            }

            // Formdaki t�m kontrolleri devre d��� b�rak
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
            MessageBox.Show("Veri g�ncellendi!");
        }

        private void Toolbar_DeleteClicked(object sender, EventArgs e)
        {
            MessageBox.Show("Veri silindi!");
        }

        private void Toolbar_SearchClicked(object sender, EventArgs e)
        {
            MessageBox.Show("Arama yap�ld�!");
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
                MessageBox.Show($"Veritaban� ba�latma hatas�: {ex.Message}");
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

                    // �nce plakay� kontrol et
                    string checkQuery = "SELECT COUNT(*) FROM PlakaKayitlari WHERE Plaka = @Plaka";
                    using (var checkCommand = new SQLiteCommand(checkQuery, connection))
                    { 
                        checkCommand.Parameters.AddWithValue("@Plaka", plaka);
                        int count = Convert.ToInt32(checkCommand.ExecuteScalar());

                        if (count > 0)
                        {
                            MessageBox.Show("Bu plaka zaten sistemde kay�tl�!", "Uyar�", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return; // ��lemi sonland�r
                        }
                    }
                    // E�er plaka yoksa ekleme i�lemini yap


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
                MessageBox.Show("Kay�t ba�ar�yla eklendi!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                TemizleForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Veritaban�na yazma hatas�: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private DataGridView dataGridViewResults;
        private DataTable messageTable;
        private int pLineNo = 0;
        public void LoadDataTable()
        {
            int pLineNo = 0;
        // DataGridView ayarlar�
            messageGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            messageGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // DataGridView'in formla birlikte b�y�y�p k���lmesini sa�lamak i�in Anchor kullan�yoruz
            messageGridView.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            //messageGridView.Location = new System.Drawing.Point(10, 100); // GridView pozisyonu
            messageGridView.Size = new System.Drawing.Size(this.ClientSize.Width - 20, this.ClientSize.Height - 150); // Dinamik boyutland�rma

            // DataTable olu�tur ve s�tunlar� ekle
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


            // DataGridView'e DataTable'� ba�la
            messageGridView.DataSource = messageTable;

            //// S�tun geni�li�ini ayarlamak i�in veri ba�lama i�lemini bekle
            //messageGridView.DataBindingComplete += (sender, e) =>
            //{
            //    // Veri ba�land�ktan sonra s�tun geni�liklerini ayarl�yoruz
            //    messageGridView.Columns["LineNo"].Width = 50;  // 100 piksel geni�lik
            //    messageGridView.Columns["Timestamp"].Width = 130;  // 100 piksel geni�lik
            //    messageGridView.Columns["FileName"].Width = 145;  // 100 piksel geni�lik
            //    messageGridView.Columns["Message"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill; // Mesaj kolonunu doldur
            //};
        }

        private void PlakaAratButonu_Click(object sender, EventArgs e)
        {
            string plakaArama = textBoxPlakaArama?.Text;
            if (string.IsNullOrEmpty(plakaArama))
            {
                MessageBox.Show("L�tfen bir plaka girin.");
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
                                richTextBoxAramaSonucu.AppendText("Ara� Geli� Tarihi: " + reader["AracGelisTarihi"] + Environment.NewLine);
                                richTextBoxAramaSonucu.AppendText("Ara� Geli� KM: " + reader["AracGelisKM"] + Environment.NewLine);
                                richTextBoxAramaSonucu.AppendText("Teslim Tarihi: " + reader["TeslimTarihi"] + Environment.NewLine);
                                richTextBoxAramaSonucu.AppendText("Yap�lan ��ler: " + reader["YapilanIsler"] + Environment.NewLine);
                                found = true;
                            }
                        }
                    }
                }

                if (!found)
                {
                    richTextBoxAramaSonucu.AppendText("Plaka bulunamad�.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Veritaban� okuma hatas�: {ex.Message}");
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

                    // Plaka i�inde arama yapmak i�in LIKE kullan�m�
                    string query = "SELECT * FROM PlakaKayitlari WHERE Plaka LIKE @Plaka";

                    using (var command = new SQLiteCommand(query, connection))
                    {
                        // Kullan�c�n�n girdi�i de�eri % i�aretleriyle �evrele
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
                    richTextBoxAramaSonucu.AppendText("Plaka bulunamad�.");
                    MessageBox.Show($"Plaka bulunamad�.{xplakaArama}");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Veritaban� okuma hatas�: {ex.Message}");
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
            if (messageTable == null) // Null kontrol�
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

            // Timestamp'e g�re ters s�rada s�ralama yap
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