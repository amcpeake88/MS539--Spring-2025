using System;

namespace MultiComponentGUI
{
    public class Certification
    {
        public string Name { get; set; }
        public string PersonName { get; set; }
        public DateTime ExpirationDate { get; set; }

        public bool IsExpired => DateTime.Now > ExpirationDate;

        public override string ToString()
        {
            return $"{Name} (Expires: {ExpirationDate:d})";
        }
    }
}
