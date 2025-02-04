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
using System.Drawing;
using System.Windows.Forms;

namespace MultiComponentGUI
{
    public class MainForm : Form
    {
        private TextBox personNameTextBox;
        private TextBox searchTextBox;
        private ListBox certificationListBox;
        private Button addCertButton;
        private Button removePersonButton;
        private Button undoButton;
        private Button exitButton;
        private CertificationManager certManager;

        public MainForm()
        {
            certManager = new CertificationManager();
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            // Form properties
            this.ClientSize = new Size(500, 400);
            this.Text = "Certification Tracker";
            this.BackColor = Color.Red;
            this.StartPosition = FormStartPosition.CenterScreen;

            // Person Name Label and TextBox
            var personNameLabel = new Label
            {
                Text = "Person Name:",
                Location = new Point(20, 20),
                AutoSize = true
            };
            this.Controls.Add(personNameLabel);

            personNameTextBox = new TextBox
            {
                Location = new Point(120, 20),
                Width = 200
            };
            this.Controls.Add(personNameTextBox);

            // Search TextBox and Label
            var searchLabel = new Label
            {
                Text = "Search:",
                Location = new Point(20, 60),
                AutoSize = true
            };
            this.Controls.Add(searchLabel);

            searchTextBox = new TextBox
            {
                Location = new Point(120, 60),
                Width = 200
            };
            searchTextBox.TextChanged += SearchTextBox_TextChanged;
            this.Controls.Add(searchTextBox);

            // Certification ListBox and Label
            var certsLabel = new Label
            {
                Text = "Certifications:",
                Location = new Point(20, 100),
                AutoSize = true
            };
            this.Controls.Add(certsLabel);

            certificationListBox = new ListBox
            {
                Location = new Point(20, 120),
                Size = new Size(300, 120)
            };
            this.Controls.Add(certificationListBox);

            // Add Certification Button
            addCertButton = new Button
            {
                Text = "Add Certification",
                Location = new Point(350, 20),
                Width = 120,
                BackColor = Color.Black,
                ForeColor = Color.White
            };
            addCertButton.Click += AddCertButton_Click;
            this.Controls.Add(addCertButton);

            // Remove Person Button
            removePersonButton = new Button
            {
                Text = "Remove Person",
                Location = new Point(350, 60),
                Width = 120,
                BackColor = Color.Black,
                ForeColor = Color.White
            };
            removePersonButton.Click += RemovePersonButton_Click;
            this.Controls.Add(removePersonButton);

            // Undo Button
            undoButton = new Button
            {
                Text = "Undo",
                Location = new Point(350, 100),
                Width = 120,
                BackColor = Color.Black,
                ForeColor = Color.White,
                Enabled = false
            };
            undoButton.Click += UndoButton_Click;
            this.Controls.Add(undoButton);

            // Exit Button
            exitButton = new Button
            {
                Text = "Exit",
                Location = new Point(350, 140),
                Width = 120,
                BackColor = Color.Black,
                ForeColor = Color.White
            };
            exitButton.Click += (s, e) => this.Close();
            this.Controls.Add(exitButton);
        }

        private void AddCertButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(personNameTextBox.Text))
            {
                MessageBox.Show("Please enter a person name.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (var inputForm = new CertificationInputForm())
            {
                if (inputForm.ShowDialog() == DialogResult.OK)
                {
                    string personName = personNameTextBox.Text.Trim();
                    string certificationName = inputForm.CertificationName;
                    DateTime expirationDate = inputForm.ExpirationDate;

                    certManager.AddCertification(personName, certificationName, expirationDate);
                    MessageBox.Show("Certification added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    undoButton.Enabled = certManager.CanUndo();
                    SearchTextBox_TextChanged(null, EventArgs.Empty);
                }
            }
        }

        private void RemovePersonButton_Click(object sender, EventArgs e)
        {
            string personName = personNameTextBox.Text.Trim();
            if (string.IsNullOrWhiteSpace(personName))
            {
                MessageBox.Show("Please enter a person name.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            certManager.RemovePerson(personName);
            MessageBox.Show("Person removed successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            undoButton.Enabled = certManager.CanUndo();
            SearchTextBox_TextChanged(null, EventArgs.Empty);
        }

        private void UndoButton_Click(object sender, EventArgs e)
        {
            certManager.Undo();
            undoButton.Enabled = certManager.CanUndo();
            SearchTextBox_TextChanged(null, EventArgs.Empty);
        }

        private void SearchTextBox_TextChanged(object sender, EventArgs e)
        {
            string searchText = searchTextBox.Text.Trim();
            var certs = certManager.GetCertificationsForPerson(searchText);
            certificationListBox.Items.Clear();
            foreach (var cert in certs)
            {
                certificationListBox.Items.Add($"{cert.Name} (Expires: {cert.ExpirationDate.ToShortDateString()})");
            }
        }
    }
}
