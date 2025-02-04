using System;
using System.Drawing;
using System.Windows.Forms;

namespace MultiComponentGUI
{
    public partial class calendar : Form
    {
        private MonthCalendar monthCalendar;
        private TextBox certNameTextBox;
        private TextBox personNameTextBox;
        private Button addButton;
        private Label certNameLabel;
        private Label personNameLabel;
        private Label selectedDateLabel;
        private readonly CertificationManager certManager;

        public calendar(CertificationManager manager)
        {
            certManager = manager;
            InitializeComponent();
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
                    monthCalendar.SelectionStart
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