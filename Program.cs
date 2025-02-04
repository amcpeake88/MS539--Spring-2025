using System;
using System.Windows.Forms;

namespace MultiComponentGUI
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            try
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);

                using (var loginForm = new LoginForm())  // Keep using LoginForm
                {
                    if (loginForm.ShowDialog() == DialogResult.OK)
                    {
                        // MainForm is launched after successful login
                        using (var mainForm = new MainForm())
                        {
                            Application.Run(mainForm);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"A critical error occurred: {ex.Message}\n\nThe application needs to close.",
                    "Critical Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
