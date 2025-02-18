using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace PlakaKayitUygulamasi
{
    public partial class ToolbarControl : UserControl
    {
        public event EventHandler NewClicked;
        public event EventHandler EditClicked;
        public event EventHandler DeleteClicked;
        public event EventHandler SearchClicked;
        public event EventHandler SaveClicked;
        public event EventHandler CancelClicked;

        private Button btnNew;
        private Button btnEdit;
        private Button btnDelete;
        private Button btnSave;
        private Button btnSearch;
        private Button btnCancel;
        private TextBox txtSearch;
        private CheckBox chkSearchOption;

        public ToolbarControl()
        {
            InitializeComponent();
            InitializeToolbar();
        }
        private Form _parentForm;

        public void SetParentForm(Form form)
        {
            _parentForm = form;
        }
        private void InitializeToolbar()
        {
            var panel = new Panel
            {
                Dock = DockStyle.Top,
                Height = 60,
                BackColor = Color.FromArgb(45, 45, 48)
            };

            btnNew = new Button
            {
                Text = "➕ Yeni",
                Width = 100,
                Height = 45,
                Left = 10,
                Top = 7,
                BackColor = Color.LightGreen,
                ForeColor = Color.Black,
                FlatStyle = FlatStyle.Flat
            };
            btnNew.FlatAppearance.BorderSize = 0;
            btnNew.Click += (sender, e) => {
                NewClicked?.Invoke(sender, e);
                ActivateNewMode();
            };
            panel.Controls.Add(btnNew);

            btnEdit = new Button
            {
                Text = "✏️ Düzenle",
                Width = 100,
                Height = 45,
                Left = 120,
                Top = 7,
                BackColor = Color.LightGoldenrodYellow,
                ForeColor = Color.Black,
                FlatStyle = FlatStyle.Flat,
                Enabled = true
            };
            btnEdit.FlatAppearance.BorderSize = 0;
            btnEdit.Click += (sender, e) => EditClicked?.Invoke(sender, e);
            panel.Controls.Add(btnEdit);

            btnDelete = new Button
            {
                Text = "🗑️ Sil",
                Width = 100,
                Height = 45,
                Left = 230,
                Top = 7,
                BackColor = Color.LightCoral,
                ForeColor = Color.Black,
                FlatStyle = FlatStyle.Flat,
                Enabled = true
            };
            btnDelete.FlatAppearance.BorderSize = 0;
            btnDelete.Click += (sender, e) => DeleteClicked?.Invoke(sender, e);
            panel.Controls.Add(btnDelete);

            btnSave = new Button
            {
                Text = "💾 Kaydet",
                Width = 120,
                Height = 45,
                Left = 340,
                Top = 7,
                BackColor = Color.Gray,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Enabled = false
            };
            btnSave.FlatAppearance.BorderSize = 0;
            btnSave.Click += (sender, e) => {
                SaveClicked?.Invoke(sender, e);
                ResetToolbarState();
            };
            panel.Controls.Add(btnSave);

            btnCancel = new Button
            {
                Text = "❌ Vazgeç",
                Width = 120,
                Height = 45,
                Left = 470,
                Top = 7,
                BackColor = Color.OrangeRed,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Visible = false
            };
            btnCancel.FlatAppearance.BorderSize = 0;
            btnCancel.Click += (sender, e) => {
                CancelClicked?.Invoke(sender, e);
                ResetToolbarState();
            };
            panel.Controls.Add(btnCancel);

            btnSearch = new Button
            {
                Text = "🔍 Ara",
                Width = 100,
                Height = 45,
                Left = 600,
                Top = 7,
                BackColor = Color.LightSkyBlue,
                ForeColor = Color.Black,
                FlatStyle = FlatStyle.Flat
            };
            btnSearch.FlatAppearance.BorderSize = 0;
            btnSearch.Click += (sender, e) => SearchClicked?.Invoke(sender, e);
            panel.Controls.Add(btnSearch);

            // Yeni TextBox Ekleme (Ara için)
            txtSearch = new TextBox
            {
                Width = 350,
                Height = 30,
                Left = 710,
                Top = 15,
                PlaceholderText = "Plaka Girin"
            };
            panel.Controls.Add(txtSearch);

            // Yeni CheckBox Ekleme (Ara için)
            chkSearchOption = new CheckBox
            {
                Text = "Detaylı Arama",
                Left = 1150,
                Top = 18,
                AutoSize = true,
                ForeColor = Color.White
            };
            panel.Controls.Add(chkSearchOption);

            this.Controls.Add(panel);
            this.Dock = DockStyle.Top;
        }

        // Yeni butonuna basıldığında çalışacak metod
        private void ActivateNewMode()
        {
            btnNew.Enabled = false;
            btnSearch.Enabled = false;
            btnEdit.Enabled = false;
            btnDelete.Enabled = false;
            btnSave.Enabled = true;
            btnCancel.Visible = true;
        }

        // Vazgeç veya Kaydet butonlarına basıldığında çalışacak metod
        public void ResetToolbarState()
        {
            ClearFormControls();
            btnNew.Enabled = true;
            btnSearch.Enabled = true;
            btnEdit.Enabled = true;
            btnDelete.Enabled = true;
            btnSave.Enabled = false;
            btnCancel.Visible = false;
        }

        // Formdaki tüm kontrolleri temizle
        private void ClearFormControls()
        {
            if (_parentForm != null)
            {
                foreach (Control control in _parentForm.Controls)
                {
                    if (control is TextBox textBox)
                    {
                        textBox.Clear();
                    }
                    else if (control is RichTextBox richTextBox)
                    {
                        richTextBox.Clear();
                    }
                    else if (control is ComboBox comboBox)
                    {
                        comboBox.SelectedIndex = -1;
                    }
                    else if (control is CheckBox checkBox)
                    {
                        checkBox.Checked = false;
                    }
                    else if (control is RadioButton radioButton)
                    {
                        radioButton.Checked = false;
                    }
                    else if(control is DataGridView dataGridView)
                    {
                        continue;
                    }
                    control.Enabled = true;
                }
            }
        }

        // Formdaki tüm kontrolleri devre dışı bırak
        public void DisableAllControlsExceptSearch(Form targetForm, bool xEnabled)
        {
            targetForm.Controls.Cast<Control>()
                .Where(control => control is not ToolbarControl)
                .ToList()
                .ForEach(control => control.Enabled = xEnabled);
        }

        //// Gridde satıra tıklandığında Form1'e veri aktarma
        //public void GridRowClicked(DataGridView grid, Form1 form)
        //{
        //    if (grid.CurrentRow != null)
        //    {
        //        form.textBoxPlaka.Text = grid.CurrentRow.Cells["Plaka"].Value?.ToString();
        //        form.textBoxTelefon.Text = grid.CurrentRow.Cells["TelefonNo"].Value?.ToString();
        //        form.textBoxAdSoyad.Text = grid.CurrentRow.Cells["AdSoyad"].Value?.ToString();
        //        form.textBoxMarka.Text = grid.CurrentRow.Cells["Marka"].Value?.ToString();
        //        form.textBoxModel.Text = grid.CurrentRow.Cells["Model"].Value?.ToString();
        //        form.textBoxRenk.Text = grid.CurrentRow.Cells["Renk"].Value?.ToString();
        //        form.textBoxGelisTarihi.Text = grid.CurrentRow.Cells["AracGelisTarihi"].Value?.ToString();
        //        form.textBoxGelisKM.Text = grid.CurrentRow.Cells["AracGelisKM"].Value?.ToString();
        //        form.textBoxTeslimTarihi.Text = grid.CurrentRow.Cells["TeslimTarihi"].Value?.ToString();
        //        form.textBoxYapilanIsler.Text = grid.CurrentRow.Cells["YapilanIsler"].Value?.ToString();
        //    }
        //}

        // Arama için gerekli parametreleri almak
        public (string xSearchText, bool xChkSearchOption) GetSearchParameters()
        {
            return (txtSearch.Text, chkSearchOption.Checked);
        }
    }
}