using System;
using System.Drawing;
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
                Enabled = false
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
                Enabled = false
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
            btnSave.Click += (sender, e) => SaveClicked?.Invoke(sender, e);
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

        // Vazgeç butonuna basıldığında çalışacak metod
        public void ResetToolbarState()
        {
            btnNew.Enabled = true;
            btnSearch.Enabled = true;
            btnEdit.Enabled = true;
            btnDelete.Enabled = true;
            btnSave.Enabled = false;
            btnCancel.Visible = false;
            ClearFormControls();
        }

        // Formdaki tüm kontrolleri temizle
        private void ClearFormControls()
        {
            if (_parentForm != null)
            {
                foreach (Control control in _parentForm.Controls)
                {
                    if (control is Button nutton)
                        continue;
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
                    else
                    { 
                    
                    }
                    control.Enabled = false;
                }
            }
        }

        // Formdaki tüm kontrolleri devre dışı bırak
        public void DisableAllControlsExceptSearch(Form targetForm,bool xEnabled)
        {
            // LINQ ile Toolbar hariç tüm kontrolleri devre dışı bırak
            targetForm.Controls.Cast<Control>()
                .Where(control => control is not ToolbarControl)
                .ToList()
                .ForEach(control => control.Enabled = xEnabled);

        }

    }

}
