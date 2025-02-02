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
    public class MainBodyForm : Form
    {
        private CertificationManager certManager;
        private TextBox personNameTextBox;
        private TextBox certNameTextBox;
        private TextBox expirationDateTextBox;
        private TextBox searchTextBox;
        private ListBox certificationListBox;
        private Button addButton, searchButton, deletePersonButton, removeCertButton, clearAllButton;

        public MainBodyForm()
        {
            certManager = new CertificationManager();
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            // Form Properties
            this.ClientSize = new Size(600, 500);
            this.Text = "Certification Tracker";
            this.BackColor = Color.Red; // Background color set to red
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.StartPosition = FormStartPosition.CenterScreen;

            // Labels
            Label personLabel = new Label { Text = "Person Name:", Location = new Point(20, 20), AutoSize = true };
            Label certLabel = new Label { Text = "Certification Name:", Location = new Point(20, 60), AutoSize = true };
            Label expirationLabel = new Label { Text = "Expiration Date (MM/DD/YYYY):", Location = new Point(20, 100), AutoSize = true };
            Label searchLabel = new Label { Text = "Search:", Location = new Point(20, 300), AutoSize = true };

            // Textboxes
            personNameTextBox = new TextBox { Location = new Point(200, 20), Width = 200 };
            certNameTextBox = new TextBox { Location = new Point(200, 60), Width = 200 };
            expirationDateTextBox = new TextBox { Location = new Point(200, 100), Width = 200 };
            searchTextBox = new TextBox { Location = new Point(80, 300), Width = 200 };

            // Listbox
            certificationListBox = new ListBox { Location = new Point(20, 140), Width = 550, Height = 150 };

            // Buttons
            addButton = new Button { Text = "Add", Location = new Point(420, 20), Width = 100 };
            searchButton = new Button { Text = "Search", Location = new Point(300, 300), Width = 100 };
            deletePersonButton = new Button { Text = "Delete Person", Location = new Point(420, 60), Width = 150 };
            removeCertButton = new Button { Text = "Remove Cert", Location = new Point(420, 100), Width = 150 };
            clearAllButton = new Button { Text = "Clear All", Location = new Point(420, 300), Width = 100 };

            // Event Handlers
            addButton.Click += AddButton_Click;
            searchButton.Click += SearchButton_Click;
            deletePersonButton.Click += DeletePersonButton_Click;
            removeCertButton.Click += RemoveCertButton_Click;
            clearAllButton.Click += ClearAllButton_Click;

            Controls.AddRange(new Control[]
            {
                personLabel, certLabel, expirationLabel, searchLabel,
                personNameTextBox, certNameTextBox, expirationDateTextBox, searchTextBox,
                certificationListBox, addButton, searchButton, deletePersonButton, removeCertButton, clearAllButton
            });
        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            string personName = personNameTextBox.Text.Trim();
            string certName = certNameTextBox.Text.Trim();

            if (string.IsNullOrEmpty(personName) || string.IsNullOrEmpty(certName))
            {
                MessageBox.Show("Please enter both person name and certification name.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!DateTime.TryParse(expirationDateTextBox.Text.Trim(), out DateTime expirationDate))
            {
                MessageBox.Show("Invalid expiration date. Please use MM/DD/YYYY format.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            certManager.AddCertification(personName, certName, expirationDate);
            RefreshCertificationList();
            ClearInputFields();
        }

        private void SearchButton_Click(object sender, EventArgs e)
        {
            string searchText = searchTextBox.Text.Trim();
            certificationListBox.Items.Clear();

            foreach (var cert in certManager.GetAllCertifications())
            {
                if (cert.PersonName.IndexOf(searchText, StringComparison.OrdinalIgnoreCase) >= 0 ||
                    cert.CertificationName.IndexOf(searchText, StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    certificationListBox.Items.Add(cert);
                }
            }
        }

        private void DeletePersonButton_Click(object sender, EventArgs e)
        {
            string personName = personNameTextBox.Text.Trim();
            if (string.IsNullOrEmpty(personName))
            {
                MessageBox.Show("Please enter a person name to delete.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            certManager.RemovePerson(personName);
            RefreshCertificationList();
        }

        private void RemoveCertButton_Click(object sender, EventArgs e)
        {
            string personName = personNameTextBox.Text.Trim();
            string certName = certNameTextBox.Text.Trim();

            if (string.IsNullOrEmpty(personName) || string.IsNullOrEmpty(certName))
            {
                MessageBox.Show("Please enter both person name and certification name to remove.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            certManager.RemoveCertification(personName, certName);
            RefreshCertificationList();
        }

        private void ClearAllButton_Click(object sender, EventArgs e)
        {
            certManager = new CertificationManager();
            RefreshCertificationList();
        }

        private void RefreshCertificationList()
        {
            certificationListBox.Items.Clear();
            foreach (var cert in certManager.GetAllCertifications())
            {
                certificationListBox.Items.Add(cert);
            }
        }

        private void ClearInputFields()
        {
            personNameTextBox.Clear();
            certNameTextBox.Clear();
            expirationDateTextBox.Clear();
        }
    }
}
