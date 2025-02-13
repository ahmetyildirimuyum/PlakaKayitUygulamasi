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
                MessageBox.Show("Lütfen bir plaka girin.");
                return;
            }

            string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "PlakaKayitlari.csv");
            if (!File.Exists(filePath))
            {
                MessageBox.Show("CSV dosyasý bulunamadý.");
                return;
            }

            richTextBoxAramaSonucu.Clear();  // Önceki arama sonuçlarýný temizle
            bool found = false;

            try
            {
                Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
                // CSV dosyasýný okuyoruz, encoding olarak Windows-1254 kullanýyoruz (Türkçe karakterler için)
                using (var reader = new StreamReader(filePath, Encoding.GetEncoding(1254)))
                {
                    // Baþlýk satýrýný atlýyoruz
                    reader.ReadLine();

                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        // CSV satýrýný noktalý virgülle ayýrýyoruz
                        string[] values = line.Split(';');

                        // Plaka eþleþiyorsa bilgileri gösteriyoruz
                        if (values.Length > 0 && values[0] == plakaArama)
                        {
                            richTextBoxAramaSonucu.AppendText("Plaka: " + GetValueOrDefault(values[0]) + Environment.NewLine);
                            richTextBoxAramaSonucu.AppendText("Telefon No: " + GetValueOrDefault(values[1]) + Environment.NewLine);
                            richTextBoxAramaSonucu.AppendText("Ad Soyad: " + GetValueOrDefault(values[2]) + Environment.NewLine);
                            richTextBoxAramaSonucu.AppendText("Marka: " + GetValueOrDefault(values[3]) + Environment.NewLine);
                            richTextBoxAramaSonucu.AppendText("Model: " + GetValueOrDefault(values[4]) + Environment.NewLine);
                            richTextBoxAramaSonucu.AppendText("Renk: " + GetValueOrDefault(values[5]) + Environment.NewLine);
                            richTextBoxAramaSonucu.AppendText("Araç Geliþ Tarihi: " + GetValueOrDefault(values[6]) + Environment.NewLine);
                            richTextBoxAramaSonucu.AppendText("Araç Geliþ KM: " + GetValueOrDefault(values[7]) + Environment.NewLine);
                            richTextBoxAramaSonucu.AppendText("Teslim Tarihi: " + GetValueOrDefault(values[8]) + Environment.NewLine);
                            richTextBoxAramaSonucu.AppendText("Yapýlan Ýþler: " + GetValueOrDefault(values[9]) + Environment.NewLine);
                            found = true;
                            break;
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
                MessageBox.Show($"Dosya okuma hatasý: {ex.Message}");
            }
        }

        // Yardýmcý metod - null veya boþ deðer kontrolü
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
                // Dosyanýn var olup olmadýðýný kontrol et
                if (!File.Exists(csvFilePath))
                {
                    File.WriteAllText(csvFilePath, "Plaka;Telefon No;Ad Soyad;Marka;Model;Renk;Araç Geliþ Tarihi;Araç Geliþ KM;Teslim Tarihi;Yapýlan Ýþler;Tarih\n", Encoding.GetEncoding(1254));
                }

                // Yeni satýrý dosyaya ekle
                File.AppendAllText(csvFilePath, yeniKayit + Environment.NewLine);

                // Dosyanýn gerçekten oluþup oluþmadýðýný kontrol et
                if (File.Exists(csvFilePath))
                {
                    MessageBox.Show(string.Format("Dosya baþarýyla oluþturuldu ve veri eklendi!\n{0}", csvFilePath), "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show(string.Format("Dosya oluþturulamadý! \n{0}", csvFilePath), "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Dosyaya yazma sýrasýnda hata oluþtu: \n{0}\nHata : {1}", csvFilePath,ex.Message), "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
            //MessageBox.Show("Kayýt baþarýyla eklendi!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
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