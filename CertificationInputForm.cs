using System;
using System.Drawing;
using System.Windows.Forms;

namespace MultiComponentGUI
{
    public class CertificationInputForm : Form
    {
        private TextBox certificationNameTextBox;
        private DateTimePicker expirationDatePicker;
        private Button okButton;
        private Button cancelButton;

        public string CertificationName { get; private set; }
        public DateTime ExpirationDate { get; private set; }

        public CertificationInputForm()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.Text = "Add Certification";
            this.ClientSize = new Size(400, 200);
            this.StartPosition = FormStartPosition.CenterParent;

            var certNameLabel = new Label
            {
                Text = "Certification Name:",
                Location = new Point(20, 20),
                AutoSize = true
            };
            this.Controls.Add(certNameLabel);

            certificationNameTextBox = new TextBox
            {
                Location = new Point(150, 20),
                Width = 200
            };
            this.Controls.Add(certificationNameTextBox);

            var expirationDateLabel = new Label
            {
                Text = "Expiration Date:",
                Location = new Point(20, 60),
                AutoSize = true
            };
            this.Controls.Add(expirationDateLabel);

            expirationDatePicker = new DateTimePicker
            {
                Location = new Point(150, 60),
                Width = 200
            };
            this.Controls.Add(expirationDatePicker);

            okButton = new Button
            {
                Text = "OK",
                Location = new Point(150, 120),
                Width = 80
            };
            okButton.Click += OkButton_Click;
            this.Controls.Add(okButton);

            cancelButton = new Button
            {
                Text = "Cancel",
                Location = new Point(240, 120),
                Width = 80
            };
            cancelButton.Click += (sender, e) => this.DialogResult = DialogResult.Cancel;
            this.Controls.Add(cancelButton);
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(certificationNameTextBox.Text))
            {
                MessageBox.Show("Please enter a certification name.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            CertificationName = certificationNameTextBox.Text.Trim();
            ExpirationDate = expirationDatePicker.Value;

            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
