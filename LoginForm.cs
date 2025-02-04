using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace MultiComponentGUI
{
    public class LoginForm : Form
    {
        private TextBox usernameTextBox;
        private TextBox passwordTextBox;
        private Button loginButton;
        private Button createAccountButton;
        private Label usernameLabel;
        private Label passwordLabel;

        private static Dictionary<string, string> userDatabase = new Dictionary<string, string>();
        private const string userFilePath = "users.txt"; // File to store usernames and passwords
        private bool isLoginMode = true; // Default mode is Login

        public LoginForm()
        {
            LoadUsersFromFile();
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            // Form properties
            this.ClientSize = new Size(400, 300);
            this.Text = "Login";
            this.BackColor = Color.LightGray;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.StartPosition = FormStartPosition.CenterScreen;

            // Username Label
            usernameLabel = new Label();
            usernameLabel.Text = "Username:";
            usernameLabel.Location = new Point(50, 50);
            usernameLabel.AutoSize = true;
            this.Controls.Add(usernameLabel);

            // Password Label
            passwordLabel = new Label();
            passwordLabel.Text = "Password:";
            passwordLabel.Location = new Point(50, 100);
            passwordLabel.AutoSize = true;
            this.Controls.Add(passwordLabel);

            // Username TextBox
            usernameTextBox = new TextBox();
            usernameTextBox.Location = new Point(150, 50);
            usernameTextBox.Size = new Size(200, 20);
            this.Controls.Add(usernameTextBox);

            // Password TextBox
            passwordTextBox = new TextBox();
            passwordTextBox.Location = new Point(150, 100);
            passwordTextBox.Size = new Size(200, 20);
            passwordTextBox.UseSystemPasswordChar = true;
            this.Controls.Add(passwordTextBox);

            // Login Button
            loginButton = new Button();
            loginButton.Text = "Login";
            loginButton.Location = new Point(50, 150);
            loginButton.Size = new Size(120, 30);
            loginButton.Click += LoginButton_Click;
            this.Controls.Add(loginButton);

            // Create Account Button
            createAccountButton = new Button();
            createAccountButton.Text = "Create Account";
            createAccountButton.Location = new Point(230, 150);
            createAccountButton.Size = new Size(120, 30);
            createAccountButton.Click += CreateAccountButton_Click;
            this.Controls.Add(createAccountButton);
        }

        private void LoadUsersFromFile()
        {
            if (File.Exists(userFilePath))
            {
                string[] lines = File.ReadAllLines(userFilePath);
                foreach (string line in lines)
                {
                    string[] parts = line.Split(':');
                    if (parts.Length == 2)
                    {
                        userDatabase[parts[0]] = parts[1];
                    }
                }
            }
        }

        private void SaveUsersToFile()
        {
            using (StreamWriter writer = new StreamWriter(userFilePath))
            {
                foreach (var user in userDatabase)
                {
                    writer.WriteLine($"{user.Key}:{user.Value}");
                }
            }
        }

        private void LoginButton_Click(object sender, EventArgs e)
        {
            if (isLoginMode)
            {
                PerformLogin();
            }
            else
            {
                PerformAccountCreation();
            }
        }

        private void PerformLogin()
        {
            string username = usernameTextBox.Text.Trim();
            string password = passwordTextBox.Text.Trim();

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Please enter both username and password.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (userDatabase.ContainsKey(username) && userDatabase[username] == password)
            {
                MessageBox.Show("Login successful!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show("Invalid username or password.", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void PerformAccountCreation()
        {
            string username = usernameTextBox.Text.Trim();
            string password = passwordTextBox.Text.Trim();

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Please enter both username and password.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (userDatabase.ContainsKey(username))
            {
                MessageBox.Show("Username already exists. Please choose a different one.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            userDatabase[username] = password;
            SaveUsersToFile();
            MessageBox.Show("Account created successfully! You can now log in.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            ToggleToLoginMode();
        }

        private void CreateAccountButton_Click(object sender, EventArgs e)
        {
            if (isLoginMode)
            {
                ToggleToCreateAccountMode();
            }
            else
            {
                PerformAccountCreation();
            }
        }

        private void ToggleToCreateAccountMode()
        {
            isLoginMode = false;
            loginButton.Text = "Create Account";
            createAccountButton.Text = "Back to Login";
            usernameTextBox.Clear();
            passwordTextBox.Clear();
        }

        private void ToggleToLoginMode()
        {
            isLoginMode = true;
            loginButton.Text = "Login";
            createAccountButton.Text = "Create Account";
            usernameTextBox.Clear();
            passwordTextBox.Clear();
        }
    }
}
