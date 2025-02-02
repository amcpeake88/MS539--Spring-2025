using System;

namespace MultiComponentGUI
{
    public class Certification
    {
        public string PersonName { get; set; }
        public string CertificationName { get; set; }
        public DateTime ExpirationDate { get; set; }

        public override string ToString()
        {
            return $"{CertificationName} (Expires: {ExpirationDate:MM/dd/yyyy})";
        }

        // Placeholder method to replace RefreshCertificationList if needed
        public void DisplayCertificationDetails()
        {
            Console.WriteLine($"Person: {PersonName}, Certification: {CertificationName}, Expiration: {ExpirationDate:MM/dd/yyyy}");
        }
    }
}
