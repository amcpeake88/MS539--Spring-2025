using System;
using System.Collections.Generic;
using System.Linq;

namespace MultiComponentGUI
{
    public class CertificationManager
    {
        private readonly List<Certification> certifications;
        private readonly Stack<Action> undoStack;

        public CertificationManager()
        {
            certifications = new List<Certification>();
            undoStack = new Stack<Action>();
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
            }
        }

        public List<Certification> GetCertificationsForPerson(string personName)
        {
            return certifications
                .Where(c => c.PersonName.Equals(personName, StringComparison.OrdinalIgnoreCase))
                .ToList();
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
    }
}
