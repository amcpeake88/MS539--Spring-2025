/*
Name: Alexander McPeake
Date: 1/22
Program Description:
This program is a Certification Tracker GUI application. It allows users to:
1. Enter names of individuals.
2. Add certifications (e.g., CCNA, Net+, Sec+, A+).
3. View certifications for a selected name.
4. Clear certifications for an individual.
5. Save and load data persistently using a file.

Estimated Development Time:
I estimated that the development of this project would take approximately 3 hours:
1. Setting up the project and GUI components: 1 hour.
2. Adding functionality for data input and display: 1 hour.
3. Implementing save/load functionality: 1 hour.

Time Log:
1/22: Spent 2 hours creating the GUI layout and setting up components.
1/23: Spent 2 hours adding data input functionality.
1/24: Spent 2 hours implementing the dynamic dropdown and ListBox updates.
1/25: Spent 2 hours debugging file save/load features.
1/26: Spent 2 hours refining the UI and testing.
1/27: Spent 2 hours finalizing and reviewing the project.

Total Time Spent: 12 hours

Key Learnings:
Throughout this project, I learned the differences between C# and Python, particularly:
1. Syntax: C# is more structured and strongly typed, while Python is dynamically typed and concise.
2. Frameworks: C# leverages powerful frameworks like .NET for GUI applications, whereas Python often relies on libraries like Tkinter or PyQt.
3. File Handling: Implementing file saving/loading in C# involves classes like `StreamWriter` and `StreamReader`, which differ from Python’s file handling methods.

Analysis:
The project took significantly longer than estimated, with a total of 12 hours compared to the original estimate of 3 hours. The reasons for this were:
1. Debugging challenges with file saving and loading, ensuring data persistence across sessions.
2. Implementing dynamic updates for the dropdown and ListBox required more complex logic than anticipated.
3. Iterative refinements to the UI for improved usability.

For future projects, I can improve by:
- Allocating more time for debugging and refining features in my initial estimates.
- Researching unfamiliar features (e.g., file handling) before beginning implementation.
- Testing smaller parts of the program individually before integration to save time during debugging.
*/
using System;
using System.Linq;
using System.Windows.Forms;
using System.Drawing;

namespace MultiComponentGUI
{
    public class MainForm : Form
    {
        private Label personNameLabel;
        private Label searchLabel; // Added back
        private TextBox personNameTextBox;
        private TextBox searchTextBox;
        private Button searchButton;
        private ListBox certificationListBox;
        private Button addCertButton;
        private Button removePersonButton;
        private Button undoButton;

        private CertificationManager certificationManager;

        public MainForm()
        {
            certificationManager = new CertificationManager();
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.personNameLabel = new System.Windows.Forms.Label();
            this.searchLabel = new System.Windows.Forms.Label(); // Added back
            this.personNameTextBox = new System.Windows.Forms.TextBox();
            this.searchTextBox = new System.Windows.Forms.TextBox();
            this.certificationListBox = new System.Windows.Forms.ListBox();
            this.addCertButton = new System.Windows.Forms.Button();
            this.removePersonButton = new System.Windows.Forms.Button();
            this.undoButton = new System.Windows.Forms.Button();
            this.searchButton = new System.Windows.Forms.Button();

            this.SuspendLayout();

            // personNameLabel
            this.personNameLabel.AutoSize = true;
            this.personNameLabel.Location = new System.Drawing.Point(20, 20);
            this.personNameLabel.Name = "personNameLabel";
            this.personNameLabel.Size = new System.Drawing.Size(74, 13);
            this.personNameLabel.TabIndex = 0;
            this.personNameLabel.Text = "Person Name:";

            // searchLabel (Added back)
            this.searchLabel.AutoSize = true;
            this.searchLabel.Location = new System.Drawing.Point(20, 60);
            this.searchLabel.Name = "searchLabel";
            this.searchLabel.Size = new System.Drawing.Size(44, 13);
            this.searchLabel.TabIndex = 1;
            this.searchLabel.Text = "Search:";

            // personNameTextBox
            this.personNameTextBox.Location = new System.Drawing.Point(120, 20);
            this.personNameTextBox.Name = "personNameTextBox";
            this.personNameTextBox.Size = new System.Drawing.Size(200, 20);
            this.personNameTextBox.TabIndex = 2;

            // searchTextBox
            this.searchTextBox.Location = new System.Drawing.Point(120, 60);
            this.searchTextBox.Name = "searchTextBox";
            this.searchTextBox.Size = new System.Drawing.Size(200, 20);
            this.searchTextBox.TabIndex = 3;

             // searchButton
            this.searchButton.Location = new System.Drawing.Point(350, 60);
            this.searchButton.Name = "searchButton";
            this.searchButton.Size = new System.Drawing.Size(120, 30);
            this.searchButton.TabIndex = 8;
            this.searchButton.Text = "Search";
            this.searchButton.BackColor = Color.Black;
            this.searchButton.ForeColor = Color.White;
            this.searchButton.Click += new System.EventHandler(this.SearchButton_Click);

            // certificationListBox
            this.certificationListBox.Location = new System.Drawing.Point(20, 100);
            this.certificationListBox.Name = "certificationListBox";
            this.certificationListBox.Size = new System.Drawing.Size(300, 199);
            this.certificationListBox.TabIndex = 4;

            // addCertButton
            this.addCertButton.Location = new System.Drawing.Point(350, 20);
            this.addCertButton.Name = "addCertButton";
            this.addCertButton.Size = new System.Drawing.Size(120, 30);
            this.addCertButton.TabIndex = 5;
            this.addCertButton.Text = "Add Certification";
            this.addCertButton.BackColor = Color.Black;
            this.addCertButton.ForeColor = Color.White;
            this.addCertButton.Click += new System.EventHandler(this.AddCertButton_Click);

            // removePersonButton
            this.removePersonButton.Location = new System.Drawing.Point(350, 100);
            this.removePersonButton.Name = "removePersonButton";
            this.removePersonButton.Size = new System.Drawing.Size(120, 30);
            this.removePersonButton.TabIndex = 6;
            this.removePersonButton.Text = "Remove Person";
            this.removePersonButton.BackColor = Color.Black;
            this.removePersonButton.ForeColor = Color.White;
            this.removePersonButton.Click += new System.EventHandler(this.RemovePersonButton_Click);

            // undoButton
            this.undoButton.Location = new System.Drawing.Point(350, 140);
            this.undoButton.Name = "undoButton";
            this.undoButton.Size = new System.Drawing.Size(120, 30);
            this.undoButton.TabIndex = 7;
            this.undoButton.Text = "Undo";
            this.undoButton.BackColor = Color.Black;
            this.undoButton.ForeColor = Color.White;
            this.undoButton.Click += new System.EventHandler(this.UndoButton_Click);

            Button viewHistoryButton = new Button();
            viewHistoryButton.Text = "View History";
            viewHistoryButton.Location = new Point(350, 180);
            viewHistoryButton.Size = new Size(120, 30);
            viewHistoryButton.BackColor = Color.Black;
            viewHistoryButton.ForeColor = Color.White;
            viewHistoryButton.Click += new System.EventHandler(this.ViewHistoryButton_Click);
            this.Controls.Add(viewHistoryButton);

            // MainForm
            this.BackColor = System.Drawing.Color.Red;
            this.ClientSize = new System.Drawing.Size(500, 400);
            this.Controls.Add(this.personNameLabel);
            this.Controls.Add(this.searchLabel); // Added back
            this.Controls.Add(this.personNameTextBox);
            this.Controls.Add(this.searchTextBox);
            this.Controls.Add(this.searchButton);
            this.Controls.Add(this.certificationListBox);
            this.Controls.Add(this.addCertButton);
            this.Controls.Add(this.removePersonButton);
            this.Controls.Add(this.undoButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Certification Tracker";
            this.ResumeLayout(false);
            this.PerformLayout();
        }
        private void AddCertButton_Click(object sender, EventArgs e)
        {
            var personName = personNameTextBox.Text.Trim();

            // Use our new form that supports different certification types
            using (var certForm = new CertificationTypeForm(certificationManager))
            {
                if (certForm.ShowDialog() == DialogResult.OK)
                {
                    // The certification has already been added to the manager in the form
                    // Just update the display
                    UpdateCertificationList(personName);
                }
            }
        }
        private void ViewHistoryButton_Click(object sender, EventArgs e)
        {
            // Get the person name from the text box
            var personName = personNameTextBox.Text.Trim();

            if (string.IsNullOrWhiteSpace(personName))
            {
                MessageBox.Show("Please enter a person name first.",
                    "Person Name Required",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            // Get all certifications for this person
            var certifications = certificationManager.GetCertificationsForPerson(personName);

            if (certifications.Count == 0)
            {
                MessageBox.Show($"No certifications found for {personName}.",
                    "No Records",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                return;
            }

            // Build a comprehensive history string for all certifications
            System.Text.StringBuilder historyText = new System.Text.StringBuilder();
            historyText.AppendLine($"Certification History for {personName}:");
            historyText.AppendLine();

            foreach (var cert in certifications)
            {
                historyText.AppendLine($"Certification: {cert.Name}");
                historyText.AppendLine($"Status: {cert.Status}");
                historyText.AppendLine($"Current Expiration: {cert.ExpirationDate:MM/dd/yyyy}");
                historyText.AppendLine();

                // Add renewal history details
                if (cert.RenewalHistory.Count > 0)
                {
                    historyText.AppendLine("Renewal Dates:");
                    for (int i = 0; i < cert.RenewalHistory.Count; i++)
                    {
                        if (i == 0)
                            historyText.AppendLine($"• Initial certification: {cert.RenewalHistory[i]:MM/dd/yyyy}");
                        else
                            historyText.AppendLine($"• Renewal #{i}: {cert.RenewalHistory[i]:MM/dd/yyyy}");
                    }
                }
                else
                {
                    historyText.AppendLine("No renewal history available.");
                }

                historyText.AppendLine();
                historyText.AppendLine("----------------------------------------");
                historyText.AppendLine();
            }

            // Show history in a message box
            MessageBox.Show(historyText.ToString(),
                $"Certification History - {personName}",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        private void SearchButton_Click(object sender, EventArgs e)
        {
            string searchTerm = searchTextBox.Text.Trim();

            // Search for users
            var matchingUsers = certificationManager.SearchUsers(searchTerm);

            certificationListBox.Items.Clear();

            if (matchingUsers.Any())
            {
                foreach (var user in matchingUsers)
                {
                    // Display user
                    certificationListBox.Items.Add($"User: {user}");

                    // Show certifications for this user
                    var userCertifications = certificationManager.GetCertificationsForPerson(user);
                    if (userCertifications.Any())
                    {
                        foreach (var cert in userCertifications)
                        {
                            certificationListBox.Items.Add($"- {cert.Name} (Expires: {cert.ExpirationDate:d})");
                        }
                    }
                    else
                    {
                        certificationListBox.Items.Add("  No certifications");
                    }

                    certificationListBox.Items.Add(""); // Add empty line between users
                }
            }
            else
            {
                certificationListBox.Items.Add("No matching users found.");
            }
        }

        private void RemovePersonButton_Click(object sender, EventArgs e)
        {
            var personName = personNameTextBox.Text.Trim();
            if (string.IsNullOrWhiteSpace(personName))
            {
                MessageBox.Show("Enter a valid name to remove.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            certificationManager.RemovePerson(personName);
            MessageBox.Show($"All certifications for {personName} removed.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            certificationListBox.Items.Clear();
        }

        private void UndoButton_Click(object sender, EventArgs e)
        {
            if (certificationManager.CanUndo())
            {
                certificationManager.Undo();
                MessageBox.Show("Undo successful.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                UpdateCertificationList(personNameTextBox.Text.Trim());
            }
            else
            {
                MessageBox.Show("Nothing to undo.", "Undo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    
        private void UpdateCertificationList(string personName)
        {
            var certifications = certificationManager.GetCertificationsForPerson(personName);
            certificationListBox.Items.Clear();
            foreach (var cert in certifications)
            {
                certificationListBox.Items.Add(cert);
            }
        }
    }
}