using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Windows.Forms;

namespace MultiComponentGUI
{
    public class CertificationManager
    {
        private readonly List<Certification> certifications;
        private readonly Stack<Action> undoStack;
        private readonly string certFile = "certifications.txt";

        public CertificationManager()
        {
            certifications = new List<Certification>();
            undoStack = new Stack<Action>();
            LoadCertifications();
        }

        public void AddCertification(string personName, string certName, DateTime expirationDate)
        {
            var certification = new Certification
            {
                Name = certName,
                PersonName = personName,
                ExpirationDate = expirationDate
            };
            certifications.Add(certification);
            undoStack.Push(() => certifications.Remove(certification));
            try
            {
                string certInfo = $"{personName},{certName},{expirationDate}";
                File.AppendAllLines(certFile, new[] { certInfo });
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving certification: {ex.Message}");
            }
        }

        public void RemovePerson(string personName)
        {
            var personCerts = certifications
                .Where(c => c.PersonName.Equals(personName, StringComparison.OrdinalIgnoreCase))
                .ToList();
            foreach (var cert in personCerts)
            {
                certifications.Remove(cert);
            }
            undoStack.Push(() => certifications.AddRange(personCerts));

            // Remove person's certifications from file
            try
            {
                var lines = File.ReadAllLines(certFile)
                    .Where(line => !line.StartsWith(personName + ","))
                    .ToArray();
                File.WriteAllLines(certFile, lines);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error removing person's certifications: {ex.Message}");
            }
        }

        public void RemoveCertification(string personName, string certName)
        {
            var certToRemove = certifications.FirstOrDefault(c =>
                c.PersonName.Equals(personName, StringComparison.OrdinalIgnoreCase) &&
                c.Name == certName);
            if (certToRemove != null)
            {
                certifications.Remove(certToRemove);
                undoStack.Push(() => certifications.Add(certToRemove));

                // Remove certification from file
                try
                {
                    var lines = File.ReadAllLines(certFile)
                        .Where(line =>
                            !line.Equals($"{personName},{certName},{certToRemove.ExpirationDate}",
                                         StringComparison.OrdinalIgnoreCase))
                        .ToArray();
                    File.WriteAllLines(certFile, lines);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error removing certification: {ex.Message}");
                }
            }
        }

        public List<Certification> GetCertificationsForPerson(string personName)
        {
            return certifications
                .Where(c => c.PersonName.Equals(personName, StringComparison.OrdinalIgnoreCase))
                .ToList();
        }

        public List<string> SearchUsers(string searchTerm)
        {
            try
            {
                if (File.Exists(certFile))
                {
                    return File.ReadAllLines(certFile)
                        .Select(line => line.Split(',')[0]) // Get person names
                        .Distinct() // Remove duplicates
                        .Where(name => name.ToLower().Contains(searchTerm.ToLower()))
                        .ToList();
                }
                return new List<string>();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error searching users: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return new List<string>();
            }
        }

        public void Undo()
        {
            if (undoStack.Count > 0)
            {
                var action = undoStack.Pop();
                action();
            }
        }

        public bool CanUndo()
        {
            return undoStack.Count > 0;
        }

        private void LoadCertifications()
        {
            try
            {
                if (File.Exists(certFile))
                {
                    string[] lines = File.ReadAllLines(certFile);
                    foreach (string line in lines)
                    {
                        string[] parts = line.Split(',');
                        if (parts.Length == 3)
                        {
                            certifications.Add(new Certification
                            {
                                PersonName = parts[0],
                                Name = parts[1],
                                ExpirationDate = DateTime.Parse(parts[2])
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading certifications: {ex.Message}");
            }
        }
    }
}