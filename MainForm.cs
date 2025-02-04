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
        private Label personNameLabel;
        private Label searchLabel;

        public MainForm()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            // Form properties
            this.ClientSize = new Size(500, 400);
            this.Text = "Certification Tracker";
            this.BackColor = Color.Red;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.StartPosition = FormStartPosition.CenterScreen;

            // Person Name Label
            personNameLabel = new Label();
            personNameLabel.Text = "Person Name:";
            personNameLabel.Location = new Point(20, 20);
            personNameLabel.AutoSize = true;
            this.Controls.Add(personNameLabel);

            // Search Label
            searchLabel = new Label();
            searchLabel.Text = "Search:";
            searchLabel.Location = new Point(20, 60);
            searchLabel.AutoSize = true;
            this.Controls.Add(searchLabel);

            // Person Name TextBox
            personNameTextBox = new TextBox();
            personNameTextBox.Location = new Point(120, 20);
            personNameTextBox.Width = 200;
            this.Controls.Add(personNameTextBox);

            // Search TextBox
            searchTextBox = new TextBox();
            searchTextBox.Location = new Point(120, 60);
            searchTextBox.Width = 200;
            this.Controls.Add(searchTextBox);

            // Certification ListBox
            certificationListBox = new ListBox();
            certificationListBox.Location = new Point(20, 100);
            certificationListBox.Size = new Size(300, 200);
            this.Controls.Add(certificationListBox);

            // Add Certification Button
            addCertButton = new Button();
            addCertButton.Text = "Add Certification";
            addCertButton.Location = new Point(350, 20);
            addCertButton.Size = new Size(120, 30);
            addCertButton.Click += AddCertButton_Click;
            this.Controls.Add(addCertButton);

            // Remove Person Button
            removePersonButton = new Button();
            removePersonButton.Text = "Remove Person";
            removePersonButton.Location = new Point(350, 60);
            removePersonButton.Size = new Size(120, 30);
            removePersonButton.Click += RemovePersonButton_Click;
            this.Controls.Add(removePersonButton);

            // Undo Button
            undoButton = new Button();
            undoButton.Text = "Undo";
            undoButton.Location = new Point(350, 100);
            undoButton.Size = new Size(120, 30);
            undoButton.Click += UndoButton_Click;
            this.Controls.Add(undoButton);
        }

        private void AddCertButton_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Add Certification button clicked.");
        }

        private void RemovePersonButton_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Remove Person button clicked.");
        }

        private void UndoButton_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Undo button clicked.");
        }
    }
}

