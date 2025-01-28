namespace CertificationTracker
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.nameTextBox = new System.Windows.Forms.TextBox();
            this.nameDropDown = new System.Windows.Forms.ComboBox();
            this.certificationComboBox = new System.Windows.Forms.ComboBox();
            this.certificationListBox = new System.Windows.Forms.ListBox();
            this.nameLabel = new System.Windows.Forms.Label();
            this.certificationLabel = new System.Windows.Forms.Label();
            this.addButton = new System.Windows.Forms.Button();
            this.clearButton = new System.Windows.Forms.Button();

            // 
            // nameTextBox
            // 
            this.nameTextBox.Location = new System.Drawing.Point(20, 40);
            this.nameTextBox.Name = "nameTextBox";
            this.nameTextBox.Size = new System.Drawing.Size(200, 22);
            this.nameTextBox.TabIndex = 0;

            // 
            // nameDropDown
            // 
            this.nameDropDown.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.nameDropDown.FormattingEnabled = true;
            this.nameDropDown.Location = new System.Drawing.Point(20, 80);
            this.nameDropDown.Name = "nameDropDown";
            this.nameDropDown.Size = new System.Drawing.Size(200, 24);
            this.nameDropDown.TabIndex = 1;
            this.nameDropDown.SelectedIndexChanged += new System.EventHandler(this.nameDropDown_SelectedIndexChanged);

            // 
            // certificationComboBox
            // 
            this.certificationComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.certificationComboBox.FormattingEnabled = true;
            this.certificationComboBox.Items.AddRange(new object[] {
            "CCNA",
            "Net+",
            "Sec+",
            "A+"});
            this.certificationComboBox.Location = new System.Drawing.Point(20, 130);
            this.certificationComboBox.Name = "certificationComboBox";
            this.certificationComboBox.Size = new System.Drawing.Size(200, 24);
            this.certificationComboBox.TabIndex = 2;

            // 
            // certificationListBox
            // 
            this.certificationListBox.FormattingEnabled = true;
            this.certificationListBox.ItemHeight = 16;
            this.certificationListBox.Location = new System.Drawing.Point(20, 200);
            this.certificationListBox.Name = "certificationListBox";
            this.certificationListBox.Size = new System.Drawing.Size(200, 132);
            this.certificationListBox.TabIndex = 6;

            // 
            // nameLabel
            // 
            this.nameLabel.AutoSize = true;
            this.nameLabel.Location = new System.Drawing.Point(20, 20);
            this.nameLabel.Name = "nameLabel";
            this.nameLabel.Size = new System.Drawing.Size(45, 17);
            this.nameLabel.TabIndex = 1;
            this.nameLabel.Text = "Name";

            // 
            // certificationLabel
            // 
            this.certificationLabel.AutoSize = true;
            this.certificationLabel.Location = new System.Drawing.Point(20, 110);
            this.certificationLabel.Name = "certificationLabel";
            this.certificationLabel.Size = new System.Drawing.Size(85, 17);
            this.certificationLabel.TabIndex = 3;
            this.certificationLabel.Text = "Certification";

            // 
            // addButton
            // 
            this.addButton.Location = new System.Drawing.Point(20, 350);
            this.addButton.Name = "addButton";
            this.addButton.Size = new System.Drawing.Size(95, 30);
            this.addButton.TabIndex = 4;
            this.addButton.Text = "Add";
            this.addButton.UseVisualStyleBackColor = true;
            this.addButton.Click += new System.EventHandler(this.addButton_Click);

            // 
            // clearButton
            // 
            this.clearButton.Location = new System.Drawing.Point(125, 350);
            this.clearButton.Name = "clearButton";
            this.clearButton.Size = new System.Drawing.Size(95, 30);
            this.clearButton.TabIndex = 5;
            this.clearButton.Text = "Clear All";
            this.clearButton.UseVisualStyleBackColor = true;
            this.clearButton.Click += new System.EventHandler(this.clearButton_Click);

            // 
            // MainForm
            // 
            this.ClientSize = new System.Drawing.Size(250, 450);
            this.Controls.Add(this.certificationListBox);
            this.Controls.Add(this.clearButton);
            this.Controls.Add(this.addButton);
            this.Controls.Add(this.certificationLabel);
            this.Controls.Add(this.certificationComboBox);
            this.Controls.Add(this.nameDropDown);
            this.Controls.Add(this.nameLabel);
            this.Controls.Add(this.nameTextBox);
            this.Name = "MainForm";
            this.Text = "Certification Tracker";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.TextBox nameTextBox;
        private System.Windows.Forms.ComboBox nameDropDown;
        private System.Windows.Forms.ComboBox certificationComboBox;
        private System.Windows.Forms.ListBox certificationListBox;
        private System.Windows.Forms.Label nameLabel;
        private System.Windows.Forms.Label certificationLabel;
        private System.Windows.Forms.Button addButton;
        private System.Windows.Forms.Button clearButton;
    }
}