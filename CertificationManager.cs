using System;
using System.Collections.Generic;
using System.Linq;

namespace MultiComponentGUI
{
    public class CertificationManager
    {
        private List<Certification> certifications;
        private Stack<Action> undoStack;

        public CertificationManager()
        {
            certifications = new List<Certification>();
            undoStack = new Stack<Action>();
        }

        public void AddCertification(string personName, string certName, DateTime expirationDate)
        {
            var cert = new Certification
            {
                Name = certName,
                PersonName = personName,
                ExpirationDate = expirationDate
            };

            certifications.Add(cert);
            undoStack.Push(() => certifications.Remove(cert));
        }

        public List<Certification> GetCertificationsForPerson(string personName)
        {
            return certifications
                .Where(c => c.PersonName.Equals(personName, StringComparison.OrdinalIgnoreCase))
                .ToList();
        }

        public bool HasExpiredCertifications(string personName)
        {
            return certifications.Any(c =>
                c.PersonName.Equals(personName, StringComparison.OrdinalIgnoreCase) &&
                c.IsExpired);
        }

        public void RemovePerson(string personName)
        {
            var personCerts = GetCertificationsForPerson(personName).ToList();
            if (personCerts.Any())
            {
                foreach (var cert in personCerts)
                {
                    certifications.Remove(cert);
                }
                undoStack.Push(() => certifications.AddRange(personCerts));
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
    }
}