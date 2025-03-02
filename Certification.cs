using System.Collections.Generic;
using System;

public class Certification
{
    public string Name { get; set; }
    public string PersonName { get; set; }
    public DateTime ExpirationDate { get; set; }
    public bool IsExpired => DateTime.Now > ExpirationDate;
    public List<DateTime> RenewalHistory { get; private set; }

    // Constructor to initialize the list
    public Certification()
    {
        RenewalHistory = new List<DateTime>();
        // Add current date as the initial certification date
        RenewalHistory.Add(DateTime.Now);
    }

    // Status property
    public virtual string Status
    {
        get
        {
            if (IsExpired)
                return "Expired";
            else if ((ExpirationDate - DateTime.Now).Days <= 30)
                return "Expiring Soon";
            else
                return "Valid";
        }
    }

    public override string ToString()
    {
        return $"{Name} (Expires: {ExpirationDate:d})";
    }
}