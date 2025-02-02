using System;
using System.Windows.Forms;

namespace MultiComponentGUI
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            try
            {
                using (LoginForm loginForm = new LoginForm())
                {
                    // Display the login form first
                    if (loginForm.ShowDialog() == DialogResult.OK)
                    {
                        // If login is successful, open the main form
                        Application.Run(new MainBodyForm());
                    }
                    else
                    {
                        // If login is canceled, exit the application
                        Application.Exit();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"A critical error occurred: {ex.Message}\nThe application will now close.",
                    "Critical Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
