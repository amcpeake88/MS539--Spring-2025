using System;
using System.Collections.Generic;
using System.Linq;

namespace MultiComponentGUI
{
    public class CertificationManager
    {
        private readonly Dictionary<string, List<Certification>> personCertifications = new Dictionary<string, List<Certification>>();

        public void AddCertification(string personName, string certName, DateTime expirationDate)
        {
            if (!personCertifications.ContainsKey(personName))
            {
                personCertifications[personName] = new List<Certification>();
            }

            var cert = new Certification
            {
                PersonName = personName,
                CertificationName = certName,
                ExpirationDate = expirationDate
            };

            personCertifications[personName].Add(cert);
        }

        public void RemovePerson(string personName)
        {
            if (personCertifications.ContainsKey(personName))
            {
                personCertifications.Remove(personName);
            }
        }

        public void RemoveCertification(string personName, string certName)
        {
            if (personCertifications.ContainsKey(personName))
            {
                var certifications = personCertifications[personName];
                certifications.RemoveAll(cert => cert.CertificationName == certName);

                // If no certifications remain, remove the person entry
                if (!certifications.Any())
                {
                    personCertifications.Remove(personName);
                }
            }
        }

        public IEnumerable<Certification> GetAllCertifications()
        {
            return personCertifications.Values.SelectMany(certs => certs);
        }
    }
}
