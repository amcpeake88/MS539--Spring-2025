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
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace CertificationTracker
{
    public partial class MainForm : Form
    {
        private const string FilePath = "certifications.txt"; // File to save data
        private Dictionary<string, List<string>> certificationData = new Dictionary<string, List<string>>();

        public MainForm()
        {
            InitializeComponent();
            LoadCertifications();
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            string name = nameTextBox.Text;
            string certification = certificationComboBox.SelectedItem?.ToString();

            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(certification))
            {
                MessageBox.Show("Please enter a name and select a certification.", "Input Error");
                return;
            }

            // Add certification to the dictionary
            if (!certificationData.ContainsKey(name))
            {
                certificationData[name] = new List<string>();
                nameDropDown.Items.Add(name); // Add new name to dropdown
            }
            certificationData[name].Add(certification);

            // Update the ListBox and save the data
            UpdateCertificationList(name);
            SaveCertifications();
        }

        private void clearButton_Click(object sender, EventArgs e)
        {
            string name = nameDropDown.SelectedItem?.ToString();

            if (string.IsNullOrWhiteSpace(name) || !certificationData.ContainsKey(name))
            {
                MessageBox.Show("No certifications found for this user.", "Error");
                return;
            }

            // Clear certifications for the user
            certificationData[name].Clear();
            certificationListBox.Items.Clear();
            SaveCertifications();
            MessageBox.Show($"All certifications for {name} have been cleared.", "Success");
        }

        private void nameDropDown_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Update ListBox with selected user's certifications
            string selectedName = nameDropDown.SelectedItem?.ToString();
            if (!string.IsNullOrEmpty(selectedName))
            {
                UpdateCertificationList(selectedName);
            }
        }

        private void UpdateCertificationList(string name)
        {
            certificationListBox.Items.Clear();

            if (certificationData.ContainsKey(name))
            {
                foreach (var cert in certificationData[name])
                {
                    certificationListBox.Items.Add(cert);
                }
            }
        }

        private void SaveCertifications()
        {
            using (StreamWriter writer = new StreamWriter(FilePath))
            {
                foreach (var user in certificationData)
                {
                    string line = $"{user.Key}:{string.Join(",", user.Value)}";
                    writer.WriteLine(line);
                }
            }
        }

        private void LoadCertifications()
        {
            if (!File.Exists(FilePath)) return;

            using (StreamReader reader = new StreamReader(FilePath))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    var parts = line.Split(':');
                    if (parts.Length == 2)
                    {
                        string name = parts[0];
                        var certifications = parts[1].Split(',');
                        certificationData[name] = new List<string>(certifications);
                        nameDropDown.Items.Add(name); // Add names to dropdown on load
                    }
                }
            }
        }
    }
}

