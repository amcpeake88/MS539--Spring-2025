using System;
using System.Drawing;
using System.Windows.Forms;
using System.Linq;
using System.IO;

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
        private readonly string accountsFile = "accounts.txt";

        public LoginForm()
        {
            InitializeComponent();
        }

        private bool TryParseUserInput(string input, out string validatedInput)
        {
            validatedInput = string.Empty;

            try
            {
                // Check if input is null or empty
                if (string.IsNullOrWhiteSpace(input))
                    return false;

                // Validate input length
                if (input.Length < 4 || input.Length > 20)
                    return false;

                // Check for valid characters (letters, numbers, and basic punctuation only)
                if (!input.All(c => char.IsLetterOrDigit(c) || char.IsPunctuation(c)))
                    return false;

                validatedInput = input.Trim();
                return true;
            }
            catch
            {
                return false;
            }
        }

        private void SaveAccount(string username, string password)
        {
            try
            {
                // Save in format: username,password
                string accountInfo = $"{username},{password}";
                File.AppendAllLines(accountsFile, new[] { accountInfo });
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving account: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool CheckAccount(string username, string password)
        {
            try
            {
                if (!File.Exists(accountsFile))
                    return false;

                string[] accounts = File.ReadAllLines(accountsFile);
                foreach (string account in accounts)
                {
                    string[] credentials = account.Split(',');
                    if (credentials.Length == 2)
                    {
                        if (credentials[0] == username && credentials[1] == password)
                            return true;
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error checking account: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        private bool ValidateLoginCredentials(out string errorMessage)
        {
            errorMessage = string.Empty;
            string validatedUsername, validatedPassword;

            if (!TryParseUserInput(usernameTextBox.Text, out validatedUsername))
            {
                errorMessage = "Invalid username format. Must be 4-20 characters long.";
                return false;
            }

            if (!TryParseUserInput(passwordTextBox.Text, out validatedPassword))
            {
                errorMessage = "Invalid password format. Must be 4-20 characters long.";
                return false;
            }

            return true;
        }

        private void InitializeComponent()
        {
            try
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
                usernameLabel.Location = new Point(100, 30);
                usernameLabel.AutoSize = true;
                this.Controls.Add(usernameLabel);

                // Password Label
                passwordLabel = new Label();
                passwordLabel.Text = "Password:";
                passwordLabel.Location = new Point(100, 80);
                passwordLabel.AutoSize = true;
                this.Controls.Add(passwordLabel);

                // Username TextBox
                usernameTextBox = new TextBox();
                usernameTextBox.Location = new Point(100, 50);
                usernameTextBox.Size = new Size(200, 20);
                this.Controls.Add(usernameTextBox);

                // Password TextBox
                passwordTextBox = new TextBox();
                passwordTextBox.Location = new Point(100, 100);
                passwordTextBox.Size = new Size(200, 20);
                passwordTextBox.UseSystemPasswordChar = true;
                this.Controls.Add(passwordTextBox);

                // Login Button
                loginButton = new Button();
                loginButton.Text = "Login";
                loginButton.Location = new Point(100, 150);
                loginButton.Size = new Size(90, 30);
                loginButton.BackColor = Color.Black;
                loginButton.ForeColor = Color.White;
                loginButton.Click += LoginButton_Click;
                this.Controls.Add(loginButton);

                // Create Account Button
                createAccountButton = new Button();
                createAccountButton.Text = "Create Account";
                createAccountButton.Location = new Point(210, 150);
                createAccountButton.Size = new Size(90, 30);
                createAccountButton.BackColor = Color.Black;
                createAccountButton.ForeColor = Color.White;
                createAccountButton.Click += CreateAccountButton_Click;
                this.Controls.Add(createAccountButton);

                this.AcceptButton = loginButton;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error initializing form: {ex.Message}",
                    "Initialization Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoginButton_Click(object sender, EventArgs e)
        {
            try
            {
                string errorMessage;
                if (!ValidateLoginCredentials(out errorMessage))
                {
                    MessageBox.Show(errorMessage, "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                try
                {
                    VerifyUserCredentials();
                }
                catch (UnauthorizedAccessException ex)
                {
                    MessageBox.Show(ex.Message, "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    passwordTextBox.Clear();
                    passwordTextBox.Focus();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Authentication error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Critical error during login: " + ex.Message, "Critical Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void VerifyUserCredentials()
        {
            string username = usernameTextBox.Text.Trim();
            string password = passwordTextBox.Text.Trim();

            if (CheckAccount(username, password))
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                throw new UnauthorizedAccessException("Invalid username or password");
            }
        }

        private void CreateAccountButton_Click(object sender, EventArgs e)
        {
            try
            {
                loginButton.Hide();
                createAccountButton.Hide();

                // Create new controls for account creation
                TextBox confirmPasswordTextBox = new TextBox();
                Label confirmPasswordLabel = new Label();
                Button confirmCreateButton = new Button();
                Button backButton = new Button();

                // Set up confirm password controls
                confirmPasswordLabel.Text = "Confirm:";
                confirmPasswordLabel.Location = new Point(100, 130);
                confirmPasswordLabel.AutoSize = true;

                confirmPasswordTextBox.Location = new Point(100, 150);
                confirmPasswordTextBox.Size = new Size(200, 20);
                confirmPasswordTextBox.UseSystemPasswordChar = true;

                // Set up buttons
                confirmCreateButton.Text = "Create";
                confirmCreateButton.Location = new Point(100, 190);
                confirmCreateButton.Size = new Size(90, 30);
                confirmCreateButton.BackColor = Color.Black;
                confirmCreateButton.ForeColor = Color.White;

                backButton.Text = "Back";
                backButton.Location = new Point(210, 190);
                backButton.Size = new Size(90, 30);
                backButton.BackColor = Color.Black;
                backButton.ForeColor = Color.White;

                // Add new controls
                this.Controls.AddRange(new Control[] {
                    confirmPasswordLabel,
                    confirmPasswordTextBox,
                    confirmCreateButton,
                    backButton
                });

                // Change form title
                this.Text = "Create Account";

                void CleanupCreateAccount()
                {
                    this.Controls.Remove(confirmPasswordLabel);
                    this.Controls.Remove(confirmPasswordTextBox);
                    this.Controls.Remove(confirmCreateButton);
                    this.Controls.Remove(backButton);
                    loginButton.Show();
                    createAccountButton.Show();
                    this.Text = "Login";
                    usernameTextBox.Clear();
                    passwordTextBox.Clear();
                }

                // Add confirmation button event handler
                confirmCreateButton.Click += (s, args) =>
                {
                    try
                    {
                        string validatedUsername, validatedPassword, validatedConfirmPassword;

                        if (!TryParseUserInput(usernameTextBox.Text, out validatedUsername))
                        {
                            MessageBox.Show("Invalid username format.", "Validation Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }

                        if (!TryParseUserInput(passwordTextBox.Text, out validatedPassword))
                        {
                            MessageBox.Show("Invalid password format.", "Validation Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }

                        if (!TryParseUserInput(confirmPasswordTextBox.Text, out validatedConfirmPassword))
                        {
                            MessageBox.Show("Invalid confirm password format.", "Validation Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }

                        if (validatedPassword != validatedConfirmPassword)
                        {
                            MessageBox.Show("Passwords do not match.", "Validation Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }

                        // Save the account
                        SaveAccount(validatedUsername, validatedPassword);

                        MessageBox.Show("Account created successfully!", "Success",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        CleanupCreateAccount();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error creating account: {ex.Message}",
                            "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                };

                // Add back button event handler
                backButton.Click += (s, args) => CleanupCreateAccount();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error in account creation: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            try
            {
                base.OnFormClosing(e);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error closing form: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}