using System;
using System.Drawing;
using System.Windows.Forms;

namespace MultiComponentGUI
{
    public class CertificationTypeForm : Form
    {
        // Common fields
        private ComboBox certTypeComboBox;
        private TextBox certNameTextBox;
        private TextBox personNameTextBox;
        private DateTimePicker expirationDatePicker;
        private Button saveButton;
        private Button cancelButton;

        // Professional certification fields
        private Panel professionalPanel;
        private TextBox issuingOrgTextBox;
        private TextBox certIdTextBox;
        private CheckBox requiresCECheckBox;
        private TextBox ceHoursTextBox;

        // Compliance certification fields
        private Panel compliancePanel;
        private TextBox standardTextBox;
        private TextBox regulatorTextBox;
        private CheckBox mandatoryCheckBox;

        // Properties to access the created certification and person name
        public BaseCertification Certification { get; private set; }
        public string PersonName { get; private set; }

        // Reference to the certification manager
        private readonly CertificationManager certManager;

        public CertificationTypeForm(CertificationManager manager)
        {
            certManager = manager;
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            // Form settings
            this.Text = "Add Certification";
            this.Size = new Size(500, 500);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;

            // Labels
            Label certTypeLabel = new Label();
            certTypeLabel.Text = "Certification Type:";
            certTypeLabel.Location = new Point(20, 20);
            certTypeLabel.AutoSize = true;

            Label personNameLabel = new Label();
            personNameLabel.Text = "Person Name:";
            personNameLabel.Location = new Point(20, 60);
            personNameLabel.AutoSize = true;

            Label certNameLabel = new Label();
            certNameLabel.Text = "Certification Name:";
            certNameLabel.Location = new Point(20, 100);
            certNameLabel.AutoSize = true;

            Label expirationDateLabel = new Label();
            expirationDateLabel.Text = "Expiration Date:";
            expirationDateLabel.Location = new Point(20, 140);
            expirationDateLabel.AutoSize = true;

            // Create common controls
            certTypeComboBox = new ComboBox();
            certTypeComboBox.Location = new Point(150, 20);
            certTypeComboBox.Width = 300;
            certTypeComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            certTypeComboBox.Items.AddRange(new string[] {
                "Standard Certification",
                "Professional Certification",
                "Compliance Certification"
            });
            certTypeComboBox.SelectedIndex = 0;
            certTypeComboBox.SelectedIndexChanged += CertTypeComboBox_SelectedIndexChanged;

            personNameTextBox = new TextBox();
            personNameTextBox.Location = new Point(150, 60);
            personNameTextBox.Width = 300;

            certNameTextBox = new TextBox();
            certNameTextBox.Location = new Point(150, 100);
            certNameTextBox.Width = 300;

            expirationDatePicker = new DateTimePicker();
            expirationDatePicker.Location = new Point(150, 140);
            expirationDatePicker.Width = 300;
            expirationDatePicker.Format = DateTimePickerFormat.Short;
            expirationDatePicker.Value = DateTime.Now.AddYears(1);

            // Professional certification panel
            professionalPanel = new Panel();
            professionalPanel.Location = new Point(20, 180);
            professionalPanel.Size = new Size(450, 180);
            professionalPanel.BorderStyle = BorderStyle.FixedSingle;
            professionalPanel.Visible = false;

            Label issuingOrgLabel = new Label();
            issuingOrgLabel.Text = "Issuing Organization:";
            issuingOrgLabel.Location = new Point(10, 20);
            issuingOrgLabel.AutoSize = true;

            issuingOrgTextBox = new TextBox();
            issuingOrgTextBox.Location = new Point(150, 20);
            issuingOrgTextBox.Width = 280;

            Label certIdLabel = new Label();
            certIdLabel.Text = "Certification ID:";
            certIdLabel.Location = new Point(10, 60);
            certIdLabel.AutoSize = true;

            certIdTextBox = new TextBox();
            certIdTextBox.Location = new Point(150, 60);
            certIdTextBox.Width = 280;

            requiresCECheckBox = new CheckBox();
            requiresCECheckBox.Text = "Requires Continuing Education";
            requiresCECheckBox.Location = new Point(10, 100);
            requiresCECheckBox.AutoSize = true;
            requiresCECheckBox.CheckedChanged += RequiresCECheckBox_CheckedChanged;

            Label ceHoursLabel = new Label();
            ceHoursLabel.Text = "CE Hours Required:";
            ceHoursLabel.Location = new Point(10, 130);
            ceHoursLabel.AutoSize = true;
            ceHoursLabel.Enabled = false;

            ceHoursTextBox = new TextBox();
            ceHoursTextBox.Location = new Point(150, 130);
            ceHoursTextBox.Width = 100;
            ceHoursTextBox.Text = "0";
            ceHoursTextBox.Enabled = false;

            professionalPanel.Controls.AddRange(new Control[] {
                issuingOrgLabel, issuingOrgTextBox,
                certIdLabel, certIdTextBox,
                requiresCECheckBox,
                ceHoursLabel, ceHoursTextBox
            });

            // Compliance certification panel
            compliancePanel = new Panel();
            compliancePanel.Location = new Point(20, 180);
            compliancePanel.Size = new Size(450, 180);
            compliancePanel.BorderStyle = BorderStyle.FixedSingle;
            compliancePanel.Visible = false;

            Label standardLabel = new Label();
            standardLabel.Text = "Compliance Standard:";
            standardLabel.Location = new Point(10, 20);
            standardLabel.AutoSize = true;

            standardTextBox = new TextBox();
            standardTextBox.Location = new Point(150, 20);
            standardTextBox.Width = 280;

            Label regulatorLabel = new Label();
            regulatorLabel.Text = "Regulator Authority:";
            regulatorLabel.Location = new Point(10, 60);
            regulatorLabel.AutoSize = true;

            regulatorTextBox = new TextBox();
            regulatorTextBox.Location = new Point(150, 60);
            regulatorTextBox.Width = 280;

            mandatoryCheckBox = new CheckBox();
            mandatoryCheckBox.Text = "Mandatory Compliance";
            mandatoryCheckBox.Location = new Point(10, 100);
            mandatoryCheckBox.AutoSize = true;

            compliancePanel.Controls.AddRange(new Control[] {
                standardLabel, standardTextBox,
                regulatorLabel, regulatorTextBox,
                mandatoryCheckBox
            });

            // Buttons
            saveButton = new Button();
            saveButton.Text = "Add Certification";
            saveButton.Location = new Point(150, 400);
            saveButton.Size = new Size(150, 30);
            saveButton.BackColor = Color.Black;  
            saveButton.ForeColor = Color.White;  
            saveButton.Click += SaveButton_Click;

            cancelButton = new Button();
            cancelButton.Text = "Cancel";
            cancelButton.Location = new Point(320, 400);
            cancelButton.Size = new Size(130, 30);
            cancelButton.BackColor = Color.Black;  
            cancelButton.ForeColor = Color.White;  
            cancelButton.Click += (sender, e) => this.DialogResult = DialogResult.Cancel;
            
            // Add controls to form
            this.Controls.AddRange(new Control[] {
                certTypeLabel, certTypeComboBox,
                personNameLabel, personNameTextBox,
                certNameLabel, certNameTextBox,
                expirationDateLabel, expirationDatePicker,
                professionalPanel, compliancePanel,
                saveButton, cancelButton
            });
        }

        private void CertTypeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Hide all panels
            professionalPanel.Visible = false;
            compliancePanel.Visible = false;

            // Show the appropriate panel based on selection
            switch (certTypeComboBox.SelectedIndex)
            {
                case 1: // Professional
                    professionalPanel.Visible = true;
                    break;

                case 2: // Compliance
                    compliancePanel.Visible = true;
                    break;
            }
        }

        private void RequiresCECheckBox_CheckedChanged(object sender, EventArgs e)
        {
            // Enable/disable CE Hours controls based on checkbox state
            foreach (Control control in professionalPanel.Controls)
            {
                if (control is Label label && label.Text == "CE Hours Required:")
                {
                    label.Enabled = requiresCECheckBox.Checked;
                }
            }

            ceHoursTextBox.Enabled = requiresCECheckBox.Checked;
            if (!requiresCECheckBox.Checked)
            {
                ceHoursTextBox.Text = "0";
            }
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            try
            {
                // Validate common fields
                if (string.IsNullOrWhiteSpace(personNameTextBox.Text))
                {
                    MessageBox.Show("Please enter a person name.", "Validation Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (string.IsNullOrWhiteSpace(certNameTextBox.Text))
                {
                    MessageBox.Show("Please enter a certification name.", "Validation Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Get person name
                PersonName = personNameTextBox.Text.Trim();

                // Create the appropriate certification type
                switch (certTypeComboBox.SelectedIndex)
                {
                    case 0: // Standard
                        Certification = new BaseCertification
                        {
                            Name = certNameTextBox.Text.Trim(),
                            PersonName = PersonName,
                            ExpirationDate = expirationDatePicker.Value
                        };
                        break;

                    case 1: // Professional
                        // Additional validation
                        if (string.IsNullOrWhiteSpace(issuingOrgTextBox.Text))
                        {
                            MessageBox.Show("Please enter an issuing organization.", "Validation Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }

                        int ceHours = 0;
                        if (requiresCECheckBox.Checked &&
                            (!int.TryParse(ceHoursTextBox.Text, out ceHours) || ceHours < 0))
                        {
                            MessageBox.Show("Please enter a valid number for CE hours.", "Validation Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }

                        Certification = new ProfessionalCertification
                        {
                            Name = certNameTextBox.Text.Trim(),
                            PersonName = PersonName,
                            ExpirationDate = expirationDatePicker.Value,
                            IssuingOrganization = issuingOrgTextBox.Text.Trim(),
                            CertificationID = certIdTextBox.Text.Trim(),
                            RequiresContinuingEducation = requiresCECheckBox.Checked,
                            ContinuingEducationHours = ceHours
                        };
                        break;

                    case 2: // Compliance
                        // Additional validation
                        if (string.IsNullOrWhiteSpace(standardTextBox.Text))
                        {
                            MessageBox.Show("Please enter a compliance standard.", "Validation Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }

                        if (string.IsNullOrWhiteSpace(regulatorTextBox.Text))
                        {
                            MessageBox.Show("Please enter a regulator authority.", "Validation Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }

                        Certification = new ComplianceCertification
                        {
                            Name = certNameTextBox.Text.Trim(),
                            PersonName = PersonName,
                            ExpirationDate = expirationDatePicker.Value,
                            ComplianceStandard = standardTextBox.Text.Trim(),
                            RegulatorAuthority = regulatorTextBox.Text.Trim(),
                            IsMandatory = mandatoryCheckBox.Checked
                        };
                        break;
                }

                // Add the certification to the manager
                certManager.AddCertification(
                    Certification.PersonName,
                    Certification.Name,
                    Certification.ExpirationDate
                );

                MessageBox.Show("Certification added successfully!", "Success",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding certification: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
    }
