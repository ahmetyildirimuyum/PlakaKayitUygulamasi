using System.Text;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;

namespace PlakaKayitUygulamasi
{
    public partial class Form1 : Form
    {

        private string csvFilePath ="";

        public Form1()
        {
            InitializeComponent();
            this.Size = new System.Drawing.Size(1920, 1080); // Adjust the form size to 1920x1080
        }


        private void PlakaAratButonu_Click(object sender, EventArgs e)
        {
            string plakaArama = textBoxPlakaArama?.Text;
            if (string.IsNullOrEmpty(plakaArama))
            {
                MessageBox.Show("L�tfen bir plaka girin.");
                return;
            }

            string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "PlakaKayitlari.csv");
            if (!File.Exists(filePath))
            {
                MessageBox.Show("CSV dosyas� bulunamad�.");
                return;
            }

            richTextBoxAramaSonucu.Clear();  // �nceki arama sonu�lar�n� temizle
            bool found = false;

            try
            {
                Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
                // CSV dosyas�n� okuyoruz, encoding olarak Windows-1254 kullan�yoruz (T�rk�e karakterler i�in)
                using (var reader = new StreamReader(filePath, Encoding.GetEncoding(1254)))
                {
                    // Ba�l�k sat�r�n� atl�yoruz
                    reader.ReadLine();

                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        // CSV sat�r�n� noktal� virg�lle ay�r�yoruz
                        string[] values = line.Split(';');

                        // Plaka e�le�iyorsa bilgileri g�steriyoruz
                        if (values.Length > 0 && values[0] == plakaArama)
                        {
                            richTextBoxAramaSonucu.AppendText("Plaka: " + GetValueOrDefault(values[0]) + Environment.NewLine);
                            richTextBoxAramaSonucu.AppendText("Telefon No: " + GetValueOrDefault(values[1]) + Environment.NewLine);
                            richTextBoxAramaSonucu.AppendText("Ad Soyad: " + GetValueOrDefault(values[2]) + Environment.NewLine);
                            richTextBoxAramaSonucu.AppendText("Marka: " + GetValueOrDefault(values[3]) + Environment.NewLine);
                            richTextBoxAramaSonucu.AppendText("Model: " + GetValueOrDefault(values[4]) + Environment.NewLine);
                            richTextBoxAramaSonucu.AppendText("Renk: " + GetValueOrDefault(values[5]) + Environment.NewLine);
                            richTextBoxAramaSonucu.AppendText("Ara� Geli� Tarihi: " + GetValueOrDefault(values[6]) + Environment.NewLine);
                            richTextBoxAramaSonucu.AppendText("Ara� Geli� KM: " + GetValueOrDefault(values[7]) + Environment.NewLine);
                            richTextBoxAramaSonucu.AppendText("Teslim Tarihi: " + GetValueOrDefault(values[8]) + Environment.NewLine);
                            richTextBoxAramaSonucu.AppendText("Yap�lan ��ler: " + GetValueOrDefault(values[9]) + Environment.NewLine);
                            found = true;
                            break;
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
                MessageBox.Show($"Dosya okuma hatas�: {ex.Message}");
            }
        }

        // Yard�mc� metod - null veya bo� de�er kontrol�
        private string GetValueOrDefault(string value)
        {
            return string.IsNullOrEmpty(value) ? "-" : value;
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            csvFilePath = Path.Combine(Application.StartupPath, "PlakaKayitlari.csv");
            this.Size = new System.Drawing.Size(1920, 1200);
            this.StartPosition = FormStartPosition.CenterScreen;

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
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            string yeniKayit = $"{plaka};{telefonNo};{adSoyad};{marka};{model};{renk};{gelisTarihi};{gelisKM};{teslimTarihi};{yapilanIsler};{tarih}";
            try
            {
                // Dosyan�n var olup olmad���n� kontrol et
                if (!File.Exists(csvFilePath))
                {
                    File.WriteAllText(csvFilePath, "Plaka;Telefon No;Ad Soyad;Marka;Model;Renk;Ara� Geli� Tarihi;Ara� Geli� KM;Teslim Tarihi;Yap�lan ��ler;Tarih\n", Encoding.GetEncoding(1254));
                }

                // Yeni sat�r� dosyaya ekle
                File.AppendAllText(csvFilePath, yeniKayit + Environment.NewLine);

                // Dosyan�n ger�ekten olu�up olu�mad���n� kontrol et
                if (File.Exists(csvFilePath))
                {
                    MessageBox.Show(string.Format("Dosya ba�ar�yla olu�turuldu ve veri eklendi!\n{0}", csvFilePath), "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show(string.Format("Dosya olu�turulamad�! \n{0}", csvFilePath), "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Dosyaya yazma s�ras�nda hata olu�tu: \n{0}\nHata : {1}", csvFilePath,ex.Message), "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
            //MessageBox.Show("Kay�t ba�ar�yla eklendi!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            TemizleForm();
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