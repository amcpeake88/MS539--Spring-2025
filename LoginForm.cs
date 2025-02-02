using System;
using System.Collections.Generic;
using System.Drawing;
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

        // Simulated in-memory account storage (replace with database or file system in production)
        private readonly Dictionary<string, string> accounts = new Dictionary<string, string>
        {
            { "admin", "password" } // Example pre-existing account
        };

        public LoginForm()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.ClientSize = new Size(400, 300);
            this.Text = "Login";
            this.BackColor = Color.LightGray;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.StartPosition = FormStartPosition.CenterScreen;

            // Username Label
            usernameLabel = new Label
            {
                Text = "Username:",
                Location = new Point(50, 50),
                AutoSize = true
            };
            this.Controls.Add(usernameLabel);

            // Username TextBox
            usernameTextBox = new TextBox
            {
                Location = new Point(150, 50),
                Width = 200
            };
            this.Controls.Add(usernameTextBox);

            // Password Label
            passwordLabel = new Label
            {
                Text = "Password:",
                Location = new Point(50, 100),
                AutoSize = true
            };
            this.Controls.Add(passwordLabel);

            // Password TextBox
            passwordTextBox = new TextBox
            {
                Location = new Point(150, 100),
                Width = 200,
                UseSystemPasswordChar = true
            };
            this.Controls.Add(passwordTextBox);

            // Login Button
            loginButton = new Button
            {
                Text = "Login",
                Location = new Point(50, 150),
                Width = 100,
                BackColor = Color.Black,
                ForeColor = Color.White
            };
            loginButton.Click += LoginButton_Click;
            this.Controls.Add(loginButton);

            // Create Account Button
            createAccountButton = new Button
            {
                Text = "Create Account",
                Location = new Point(200, 150),
                Width = 120,
                BackColor = Color.Black,
                ForeColor = Color.White
            };
            createAccountButton.Click += CreateAccountButton_Click;
            this.Controls.Add(createAccountButton);

            this.AcceptButton = loginButton; // Pressing Enter triggers the login button
        }

        private void LoginButton_Click(object sender, EventArgs e)
        {
            try
            {
                string username = usernameTextBox.Text.Trim();
                string password = passwordTextBox.Text.Trim();

                if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                {
                    MessageBox.Show("Please enter both username and password.",
                        "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (accounts.ContainsKey(username) && accounts[username] == password)
                {
                    this.DialogResult = DialogResult.OK; // Allow access to the main application
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Invalid username or password.",
                        "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    passwordTextBox.Clear();
                    passwordTextBox.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CreateAccountButton_Click(object sender, EventArgs e)
        {
            try
            {
                string username = usernameTextBox.Text.Trim();
                string password = passwordTextBox.Text.Trim();

                if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                {
                    MessageBox.Show("Please enter both username and password.",
                        "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (accounts.ContainsKey(username))
                {
                    MessageBox.Show("An account with this username already exists.",
                        "Account Creation Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Add the new account to the in-memory storage
                accounts.Add(username, password);

                MessageBox.Show("Account created successfully!",
                    "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Optionally, clear the textboxes after successful account creation
                usernameTextBox.Clear();
                passwordTextBox.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}