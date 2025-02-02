namespace MultiComponentGUI
{
    partial class calendar
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

        private void InitializeComponent()
        {
            this.Size = new System.Drawing.Size(500, 400);
            this.Text = "Add Certification with Expiration";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;

            // Labels
            this.certNameLabel = new System.Windows.Forms.Label();
            this.certNameLabel.Text = "Certification Name:";
            this.certNameLabel.Location = new System.Drawing.Point(20, 20);
            this.certNameLabel.AutoSize = true;

            this.personNameLabel = new System.Windows.Forms.Label();
            this.personNameLabel.Text = "Person Name:";
            this.personNameLabel.Location = new System.Drawing.Point(20, 60);
            this.personNameLabel.AutoSize = true;

            this.selectedDateLabel = new System.Windows.Forms.Label();
            this.selectedDateLabel.Text = "Select Expiration Date:";
            this.selectedDateLabel.Location = new System.Drawing.Point(20, 100);
            this.selectedDateLabel.AutoSize = true;

            // TextBoxes
            this.certNameTextBox = new System.Windows.Forms.TextBox();
            this.certNameTextBox.Location = new System.Drawing.Point(150, 20);
            this.certNameTextBox.Width = 200;

            this.personNameTextBox = new System.Windows.Forms.TextBox();
            this.personNameTextBox.Location = new System.Drawing.Point(150, 60);
            this.personNameTextBox.Width = 200;

            // Calendar
            this.monthCalendar = new System.Windows.Forms.MonthCalendar();
            this.monthCalendar.Location = new System.Drawing.Point(20, 130);
            this.monthCalendar.MaxSelectionCount = 1;
            this.monthCalendar.ShowToday = true;

            // Add Button
            this.addButton = new System.Windows.Forms.Button();
            this.addButton.Text = "Add Certification";
            this.addButton.Location = new System.Drawing.Point(20, 300);
            this.addButton.Width = 120;
            this.addButton.BackColor = System.Drawing.Color.Black;
            this.addButton.ForeColor = System.Drawing.Color.White;
            this.addButton.Click += new System.EventHandler(this.AddButton_Click);

            // Add controls
            this.Controls.AddRange(new System.Windows.Forms.Control[] {
                this.certNameLabel, this.personNameLabel, this.selectedDateLabel,
                this.certNameTextBox, this.personNameTextBox,
                this.monthCalendar, this.addButton
            });

            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}