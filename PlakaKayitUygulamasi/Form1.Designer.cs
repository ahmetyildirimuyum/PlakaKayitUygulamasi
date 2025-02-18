namespace PlakaKayitUygulamasi
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        TextBox? textBoxPlaka, textBoxTelefon, textBoxAdSoyad, textBoxMarka, textBoxModel, textBoxRenk, textBoxGelisKM, textBoxYapilanIsler;
        TextBox? textBoxGelisTarihi, textBoxTeslimTarihi;
        Button? btnKaydet, btnPlakaArat;
        TextBox? textBoxPlakaArama;
        Label? labelPlakaArama;
        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            labelAdSoyad = new Label();
            textBoxAdSoyad = new TextBox();
            labelTelefon = new Label();
            textBoxTelefon = new TextBox();
            labelPlaka = new Label();
            textBoxPlaka = new TextBox();
            labelMarka = new Label();
            textBoxMarka = new TextBox();
            labelModel = new Label();
            textBoxModel = new TextBox();
            labelRenk = new Label();
            textBoxRenk = new TextBox();
            labelGelisTarihi = new Label();
            textBoxGelisTarihi = new TextBox();
            labelGelisKM = new Label();
            textBoxGelisKM = new TextBox();
            labelTeslimTarihi = new Label();
            textBoxTeslimTarihi = new TextBox();
            labelYapilanIsler = new Label();
            textBoxYapilanIsler = new TextBox();
            btnKaydet = new Button();
            btnPlakaArat = new Button();
            labelPlakaArama = new Label();
            textBoxPlakaArama = new TextBox();
            richTextBoxAramaSonucu = new RichTextBox();
            messageGridView = new DataGridView();
            ((System.ComponentModel.ISupportInitialize)messageGridView).BeginInit();
            SuspendLayout();
            // 
            // labelAdSoyad
            // 
            labelAdSoyad.Location = new Point(25, 0);
            labelAdSoyad.Margin = new Padding(4, 0, 4, 0);
            labelAdSoyad.Name = "labelAdSoyad";
            labelAdSoyad.Size = new Size(500, 25);
            labelAdSoyad.TabIndex = 0;
            labelAdSoyad.Text = "Ad Soyad:";
            // 
            // textBoxAdSoyad
            // 
            textBoxAdSoyad.Location = new Point(25, 25);
            textBoxAdSoyad.Margin = new Padding(4);
            textBoxAdSoyad.Name = "textBoxAdSoyad";
            textBoxAdSoyad.PlaceholderText = "Ad Soyad";
            textBoxAdSoyad.Size = new Size(499, 31);
            textBoxAdSoyad.TabIndex = 1;
            // 
            // labelTelefon
            // 
            labelTelefon.Location = new Point(25, 88);
            labelTelefon.Margin = new Padding(4, 0, 4, 0);
            labelTelefon.Name = "labelTelefon";
            labelTelefon.Size = new Size(500, 25);
            labelTelefon.TabIndex = 2;
            labelTelefon.Text = "Telefon No:";
            // 
            // textBoxTelefon
            // 
            textBoxTelefon.Location = new Point(25, 112);
            textBoxTelefon.Margin = new Padding(4);
            textBoxTelefon.Name = "textBoxTelefon";
            textBoxTelefon.PlaceholderText = "Telefon No";
            textBoxTelefon.Size = new Size(499, 31);
            textBoxTelefon.TabIndex = 3;
            // 
            // labelPlaka
            // 
            labelPlaka.Location = new Point(562, 0);
            labelPlaka.Margin = new Padding(4, 0, 4, 0);
            labelPlaka.Name = "labelPlaka";
            labelPlaka.Size = new Size(500, 25);
            labelPlaka.TabIndex = 4;
            labelPlaka.Text = "Plaka:";
            // 
            // textBoxPlaka
            // 
            textBoxPlaka.Location = new Point(562, 25);
            textBoxPlaka.Margin = new Padding(4);
            textBoxPlaka.Name = "textBoxPlaka";
            textBoxPlaka.PlaceholderText = "Plaka";
            textBoxPlaka.Size = new Size(499, 31);
            textBoxPlaka.TabIndex = 5;
            // 
            // labelMarka
            // 
            labelMarka.Location = new Point(562, 88);
            labelMarka.Margin = new Padding(4, 0, 4, 0);
            labelMarka.Name = "labelMarka";
            labelMarka.Size = new Size(500, 25);
            labelMarka.TabIndex = 6;
            labelMarka.Text = "Marka:";
            // 
            // textBoxMarka
            // 
            textBoxMarka.Location = new Point(562, 112);
            textBoxMarka.Margin = new Padding(4);
            textBoxMarka.Name = "textBoxMarka";
            textBoxMarka.PlaceholderText = "Marka";
            textBoxMarka.Size = new Size(499, 31);
            textBoxMarka.TabIndex = 7;
            // 
            // labelModel
            // 
            labelModel.Location = new Point(562, 175);
            labelModel.Margin = new Padding(4, 0, 4, 0);
            labelModel.Name = "labelModel";
            labelModel.Size = new Size(500, 25);
            labelModel.TabIndex = 8;
            labelModel.Text = "Model:";
            // 
            // textBoxModel
            // 
            textBoxModel.Location = new Point(562, 200);
            textBoxModel.Margin = new Padding(4);
            textBoxModel.Name = "textBoxModel";
            textBoxModel.PlaceholderText = "Model";
            textBoxModel.Size = new Size(499, 31);
            textBoxModel.TabIndex = 9;
            // 
            // labelRenk
            // 
            labelRenk.Location = new Point(562, 262);
            labelRenk.Margin = new Padding(4, 0, 4, 0);
            labelRenk.Name = "labelRenk";
            labelRenk.Size = new Size(500, 25);
            labelRenk.TabIndex = 10;
            labelRenk.Text = "Renk:";
            // 
            // textBoxRenk
            // 
            textBoxRenk.Location = new Point(562, 288);
            textBoxRenk.Margin = new Padding(4);
            textBoxRenk.Name = "textBoxRenk";
            textBoxRenk.PlaceholderText = "Renk";
            textBoxRenk.Size = new Size(499, 31);
            textBoxRenk.TabIndex = 11;
            // 
            // labelGelisTarihi
            // 
            labelGelisTarihi.Location = new Point(562, 350);
            labelGelisTarihi.Margin = new Padding(4, 0, 4, 0);
            labelGelisTarihi.Name = "labelGelisTarihi";
            labelGelisTarihi.Size = new Size(500, 25);
            labelGelisTarihi.TabIndex = 12;
            labelGelisTarihi.Text = "Araç Geliş Tarihi:";
            // 
            // textBoxGelisTarihi
            // 
            textBoxGelisTarihi.Location = new Point(562, 375);
            textBoxGelisTarihi.Margin = new Padding(4);
            textBoxGelisTarihi.Name = "textBoxGelisTarihi";
            textBoxGelisTarihi.PlaceholderText = "Araç Geliş Tarihi";
            textBoxGelisTarihi.Size = new Size(499, 31);
            textBoxGelisTarihi.TabIndex = 13;
            // 
            // labelGelisKM
            // 
            labelGelisKM.Location = new Point(562, 438);
            labelGelisKM.Margin = new Padding(4, 0, 4, 0);
            labelGelisKM.Name = "labelGelisKM";
            labelGelisKM.Size = new Size(500, 25);
            labelGelisKM.TabIndex = 14;
            labelGelisKM.Text = "Araç Geliş KM:";
            // 
            // textBoxGelisKM
            // 
            textBoxGelisKM.Location = new Point(562, 462);
            textBoxGelisKM.Margin = new Padding(4);
            textBoxGelisKM.Name = "textBoxGelisKM";
            textBoxGelisKM.PlaceholderText = "Araç Geliş KM";
            textBoxGelisKM.Size = new Size(499, 31);
            textBoxGelisKM.TabIndex = 15;
            // 
            // labelTeslimTarihi
            // 
            labelTeslimTarihi.Location = new Point(562, 525);
            labelTeslimTarihi.Margin = new Padding(4, 0, 4, 0);
            labelTeslimTarihi.Name = "labelTeslimTarihi";
            labelTeslimTarihi.Size = new Size(500, 25);
            labelTeslimTarihi.TabIndex = 16;
            labelTeslimTarihi.Text = "Teslim Tarihi:";
            // 
            // textBoxTeslimTarihi
            // 
            textBoxTeslimTarihi.Location = new Point(562, 550);
            textBoxTeslimTarihi.Margin = new Padding(4);
            textBoxTeslimTarihi.Name = "textBoxTeslimTarihi";
            textBoxTeslimTarihi.PlaceholderText = "Teslim Tarihi";
            textBoxTeslimTarihi.Size = new Size(499, 31);
            textBoxTeslimTarihi.TabIndex = 17;
            // 
            // labelYapilanIsler
            // 
            labelYapilanIsler.Location = new Point(25, 175);
            labelYapilanIsler.Margin = new Padding(4, 0, 4, 0);
            labelYapilanIsler.Name = "labelYapilanIsler";
            labelYapilanIsler.Size = new Size(500, 25);
            labelYapilanIsler.TabIndex = 18;
            labelYapilanIsler.Text = "Yapılan İşler:";
            // 
            // textBoxYapilanIsler
            // 
            textBoxYapilanIsler.Location = new Point(25, 204);
            textBoxYapilanIsler.Margin = new Padding(4);
            textBoxYapilanIsler.Multiline = true;
            textBoxYapilanIsler.Name = "textBoxYapilanIsler";
            textBoxYapilanIsler.PlaceholderText = "Yapılan İşler";
            textBoxYapilanIsler.Size = new Size(500, 377);
            textBoxYapilanIsler.TabIndex = 19;
            // 
            // btnKaydet
            // 
            btnKaydet.Location = new Point(25, 628);
            btnKaydet.Margin = new Padding(4);
            btnKaydet.Name = "btnKaydet";
            btnKaydet.Size = new Size(1100, 75);
            btnKaydet.TabIndex = 20;
            btnKaydet.Text = "Kaydet";
            btnKaydet.Click += KaydetButonu_Click;
            // 
            // btnPlakaArat
            // 
            btnPlakaArat.Location = new Point(1156, 25);
            btnPlakaArat.Margin = new Padding(4);
            btnPlakaArat.Name = "btnPlakaArat";
            btnPlakaArat.Size = new Size(250, 62);
            btnPlakaArat.TabIndex = 21;
            btnPlakaArat.Text = "Plaka Arat";
            btnPlakaArat.Click += PlakaAratButonu_Click;
            // 
            // labelPlakaArama
            // 
            labelPlakaArama.Location = new Point(1156, 100);
            labelPlakaArama.Margin = new Padding(4, 0, 4, 0);
            labelPlakaArama.Name = "labelPlakaArama";
            labelPlakaArama.Size = new Size(250, 25);
            labelPlakaArama.TabIndex = 22;
            labelPlakaArama.Text = "Plaka Arama:";
            // 
            // textBoxPlakaArama
            // 
            textBoxPlakaArama.Location = new Point(1156, 125);
            textBoxPlakaArama.Margin = new Padding(4);
            textBoxPlakaArama.Name = "textBoxPlakaArama";
            textBoxPlakaArama.Size = new Size(249, 31);
            textBoxPlakaArama.TabIndex = 23;
            // 
            // richTextBoxAramaSonucu
            // 
            richTextBoxAramaSonucu.BackColor = SystemColors.InfoText;
            richTextBoxAramaSonucu.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point, 162);
            richTextBoxAramaSonucu.ForeColor = Color.Lime;
            richTextBoxAramaSonucu.Location = new Point(1156, 200);
            richTextBoxAramaSonucu.Margin = new Padding(4);
            richTextBoxAramaSonucu.Name = "richTextBoxAramaSonucu";
            richTextBoxAramaSonucu.ReadOnly = true;
            richTextBoxAramaSonucu.Size = new Size(928, 503);
            richTextBoxAramaSonucu.TabIndex = 24;
            richTextBoxAramaSonucu.Text = "";
            // 
            // messageGridView
            // 
            messageGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            messageGridView.Location = new Point(25, 719);
            messageGridView.Name = "messageGridView";
            messageGridView.RowHeadersWidth = 62;
            messageGridView.Size = new Size(2093, 536);
            messageGridView.TabIndex = 25;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(2158, 1280);
            Controls.Add(messageGridView);
            Controls.Add(labelAdSoyad);
            Controls.Add(textBoxAdSoyad);
            Controls.Add(labelTelefon);
            Controls.Add(textBoxTelefon);
            Controls.Add(labelPlaka);
            Controls.Add(textBoxPlaka);
            Controls.Add(labelMarka);
            Controls.Add(textBoxMarka);
            Controls.Add(labelModel);
            Controls.Add(textBoxModel);
            Controls.Add(labelRenk);
            Controls.Add(textBoxRenk);
            Controls.Add(labelGelisTarihi);
            Controls.Add(textBoxGelisTarihi);
            Controls.Add(labelGelisKM);
            Controls.Add(textBoxGelisKM);
            Controls.Add(labelTeslimTarihi);
            Controls.Add(textBoxTeslimTarihi);
            Controls.Add(labelYapilanIsler);
            Controls.Add(textBoxYapilanIsler);
            Controls.Add(btnKaydet);
            Controls.Add(btnPlakaArat);
            Controls.Add(labelPlakaArama);
            Controls.Add(textBoxPlakaArama);
            Controls.Add(richTextBoxAramaSonucu);
            Margin = new Padding(4);
            Name = "Form1";
            Text = "REK";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)messageGridView).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label labelAdSoyad;
        private Label labelTelefon;
        private Label labelPlaka;
        private Label labelMarka;
        private Label labelModel;
        private Label labelRenk;
        private Label labelGelisTarihi;
        private Label labelGelisKM;
        private Label labelTeslimTarihi;
        private Label labelYapilanIsler;
        private RichTextBox richTextBoxAramaSonucu;
        private DataGridView messageGridView;
    }
}
