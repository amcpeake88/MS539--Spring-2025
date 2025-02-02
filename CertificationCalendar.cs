using System;
using System.Drawing;
using System.Windows.Forms;

namespace MultiComponentGUI
{
    public class CertificationCalendar : Form
    {
        private System.ComponentModel.IContainer components = null;
        private MonthCalendar calendar;
        private TextBox certNameTextBox;
        private TextBox personNameTextBox;
        private Button addButton;
        private Label certNameLabel;
        private Label personNameLabel;
        private Label selectedDateLabel;
        private CertificationManager certManager;

        public CertificationCalendar(CertificationManager manager)
        {
            certManager = manager;
            InitializeComponent();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            // Form settings
            this.Size = new Size(500, 400);
            this.Text = "Add Certification with Expiration";
            this.StartPosition = FormStartPosition.CenterScreen;

            // Initialize components
            calendar = new MonthCalendar();
            certNameTextBox = new TextBox();
            personNameTextBox = new TextBox();
            addButton = new Button();
            certNameLabel = new Label();
            personNameLabel = new Label();
            selectedDateLabel = new Label();

            // Configure calendar
            calendar.Location = new Point(20, 130);
            calendar.MaxSelectionCount = 1;
            calendar.ShowToday = true;

            // Configure textboxes
            certNameTextBox.Location = new Point(150, 20);
            certNameTextBox.Size = new Size(200, 20);

            personNameTextBox.Location = new Point(150, 60);
            personNameTextBox.Size = new Size(200, 20);

            // Configure labels
            certNameLabel.AutoSize = true;
            certNameLabel.Location = new Point(20, 20);
            certNameLabel.Text = "Certification Name:";

            personNameLabel.AutoSize = true;
            personNameLabel.Location = new Point(20, 60);
            personNameLabel.Text = "Person Name:";

            selectedDateLabel.AutoSize = true;
            selectedDateLabel.Location = new Point(20, 100);
            selectedDateLabel.Text = "Select Expiration Date:";

            // Configure button
            addButton.Location = new Point(20, 300);
            addButton.Size = new Size(120, 30);
            addButton.Text = "Add Certification";
            addButton.BackColor = Color.Black;
            addButton.ForeColor = Color.White;
            addButton.Click += new EventHandler(AddButton_Click);

            // Add controls to form
            Controls.AddRange(new Control[] {
                certNameLabel,
                personNameLabel,
                selectedDateLabel,
                certNameTextBox,
                personNameTextBox,
                calendar,
                addButton
            });
        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(certNameTextBox.Text))
                    throw new ArgumentException("Certification name is required.");

                if (string.IsNullOrWhiteSpace(personNameTextBox.Text))
                    throw new ArgumentException("Person name is required.");

                certManager.AddCertification(
                    personNameTextBox.Text,
                    certNameTextBox.Text,
                    calendar.SelectionStart
                );

                MessageBox.Show("Certification added successfully!", "Success",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}