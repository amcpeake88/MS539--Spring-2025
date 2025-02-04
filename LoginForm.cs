using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace MultiComponentGUI
{
    public class LoginForm : Form
    {
        private Label usernameLabel;
        private Label passwordLabel;
        private TextBox usernameTextBox;
        private TextBox passwordTextBox;
        private Button loginButton;
        private Button createAccountButton;

        private static Dictionary<string, string> userDatabase = new Dictionary<string, string>();
        private const string userFilePath = "users.txt"; // File to store usernames and passwords
        private bool isLoginMode = true;

        public LoginForm()
        {
            LoadUsersFromFile();
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.usernameLabel = new Label();
            this.passwordLabel = new Label();
            this.usernameTextBox = new TextBox();
            this.passwordTextBox = new TextBox();
            this.loginButton = new Button();
            this.createAccountButton = new Button();

            // Username Label
            this.usernameLabel.Text = "Username:";
            this.usernameLabel.Location = new Point(50, 50);
            this.usernameLabel.AutoSize = true;

            // Password Label
            this.passwordLabel.Text = "Password:";
            this.passwordLabel.Location = new Point(50, 100);
            this.passwordLabel.AutoSize = true;

            // Username TextBox
            this.usernameTextBox.Location = new Point(150, 50);
            this.usernameTextBox.Width = 200;

            // Password TextBox
            this.passwordTextBox.Location = new Point(150, 100);
            this.passwordTextBox.Width = 200;
            this.passwordTextBox.UseSystemPasswordChar = true;

            // Login Button
            this.loginButton.Text = "Login";
            this.loginButton.Location = new Point(50, 150);
            this.loginButton.Click += LoginButton_Click;

            // Create Account Button
            this.createAccountButton.Text = "Create Account";
            this.createAccountButton.Location = new Point(200, 150);
            this.createAccountButton.Click += CreateAccountButton_Click;

            // Form Settings
            this.Controls.Add(this.usernameLabel);
            this.Controls.Add(this.passwordLabel);
            this.Controls.Add(this.usernameTextBox);
            this.Controls.Add(this.passwordTextBox);
            this.Controls.Add(this.loginButton);
            this.Controls.Add(this.createAccountButton);

            this.Text = "Login";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Size = new Size(400, 300);
        }

        private void LoadUsersFromFile()
        {
            if (File.Exists(userFilePath))
            {
                string[] lines = File.ReadAllLines(userFilePath);
                foreach (var line in lines)
                {
                    var parts = line.Split(':');
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

        private void CreateAccountButton_Click(object sender, EventArgs e)
        {
            isLoginMode = !isLoginMode;
            loginButton.Text = isLoginMode ? "Login" : "Create Account";
            createAccountButton.Text = isLoginMode ? "Create Account" : "Back to Login";
        }

        private void PerformLogin()
        {
            var username = usernameTextBox.Text.Trim();
            var password = passwordTextBox.Text.Trim();

            if (userDatabase.ContainsKey(username) && userDatabase[username] == password)
            {
                MessageBox.Show("Login Successful", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show("Invalid Credentials", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void PerformAccountCreation()
        {
            var username = usernameTextBox.Text.Trim();
            var password = passwordTextBox.Text.Trim();

            if (!userDatabase.ContainsKey(username))
            {
                userDatabase[username] = password;
                SaveUsersToFile();
                MessageBox.Show("Account Created", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                CreateAccountButton_Click(null, null); // Switch back to login mode
            }
            else
            {
                MessageBox.Show("Username already exists.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
