using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;

namespace MultiComponentGUI
{
    public static class UserSession
    {
        public static string LoggedInUser { get; private set; }

        public static void SetUser(string username)
        {
            LoggedInUser = username;
        }
    }

    public class LoginForm : Form
    {
        private TextBox usernameTextBox;
        private TextBox passwordTextBox;
        private Button loginButton, createAccountButton;
        private Label usernameLabel, passwordLabel;

        // Dictionary to store user credentials (For simplicity; should be replaced with database)
        private static Dictionary<string, string> userDatabase = new Dictionary<string, string>
        {
            { "admin", "password" } // Default Admin Account
        };

        public LoginForm()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.Text = "Login";
            this.ClientSize = new Size(350, 250);
            this.StartPosition = FormStartPosition.CenterScreen;

            usernameLabel = new Label { Text = "Username:", Location = new Point(20, 30), AutoSize = true };
            passwordLabel = new Label { Text = "Password:", Location = new Point(20, 80), AutoSize = true };

            usernameTextBox = new TextBox { Location = new Point(100, 30), Width = 200 };
            passwordTextBox = new TextBox { Location = new Point(100, 80), Width = 200, UseSystemPasswordChar = true };

            loginButton = new Button
            {
                Text = "Login",
                Location = new Point(100, 130),
                Width = 200,
                BackColor = Color.Black,
                ForeColor = Color.White
            };
            loginButton.Click += LoginButton_Click;

            createAccountButton = new Button
            {
                Text = "Create Account",
                Location = new Point(100, 170),
                Width = 200,
                BackColor = Color.DarkGray,
                ForeColor = Color.White
            };
            createAccountButton.Click += CreateAccountButton_Click;

            Controls.AddRange(new Control[]
            {
                usernameLabel, passwordLabel,
                usernameTextBox, passwordTextBox, loginButton, createAccountButton
            });

            this.AcceptButton = loginButton;
        }

        private void LoginButton_Click(object sender, EventArgs e)
        {
            string username = usernameTextBox.Text.Trim();
            string password = passwordTextBox.Text.Trim();

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Please enter both username and password.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (userDatabase.ContainsKey(username) && userDatabase[username] == password)
            {
                UserSession.SetUser(username); // Save the logged-in username
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show("Invalid username or password.", "Login Failed",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                passwordTextBox.Clear();
                passwordTextBox.Focus();
            }
        }

        private void CreateAccountButton_Click(object sender, EventArgs e)
        {
            string username = usernameTextBox.Text.Trim();
            string password = passwordTextBox.Text.Trim();

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Please enter both a username and password.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (userDatabase.ContainsKey(username))
            {
                MessageBox.Show("Username already exists. Please choose another.", "Account Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            userDatabase[username] = password; // Save new user
            MessageBox.Show("Account created successfully! You can now log in.", "Success",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
