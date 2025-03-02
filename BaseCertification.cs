using System;
using System.Collections.Generic;

namespace MultiComponentGUI
{
    // Base Certification Class
    public class BaseCertification
    {
        public string Name { get; set; }
        public string PersonName { get; set; }
        public DateTime ExpirationDate { get; set; }
        public bool IsExpired => DateTime.Now > ExpirationDate;
        public List<DateTime> RenewalHistory { get; private set; }

        // Constructor
        public BaseCertification()
        {
            RenewalHistory = new List<DateTime>();
            // Add current date as the initial certification date
            RenewalHistory.Add(DateTime.Now);
        }

        // Calculate the days remaining until expiration
        public int DaysRemaining => ExpirationDate > DateTime.Now
            ? (ExpirationDate - DateTime.Now).Days
            : 0;

        // Status of the certification
        public virtual string Status
        {
            get
            {
                if (IsExpired)
                    return "Expired";
                else if (DaysRemaining <= 30)
                    return "Expiring Soon";
                else
                    return "Valid";
            }
        }
        // Method to record renewals
        public void RenewCertification(DateTime renewalDate)
        {
            RenewalHistory.Add(renewalDate);
            // Update the expiration date based on renewal
            ExpirationDate = renewalDate.AddYears(1);
        }

        // Get history as a formatted string
        public string GetRenewalHistoryString()
        {
            if (RenewalHistory.Count == 0)
                return "No renewal history available.";

            string history = "Certification History:\n";
            for (int i = 0; i < RenewalHistory.Count; i++)
            {
                if (i == 0)
                    history += $"• Initial certification: {RenewalHistory[i]:MM/dd/yyyy}\n";
                else
                    history += $"• Renewal #{i}: {RenewalHistory[i]:MM/dd/yyyy}\n";
            }
            return history;
        }
        // Display info
        public override string ToString()
        {
            return $"{Name} (Expires: {ExpirationDate:d}, Status: {Status})";
        }
    }

    // Professional Certification Class - Inherits from BaseCertification
    public class ProfessionalCertification : BaseCertification
    {
        public string IssuingOrganization { get; set; }
        public string CertificationID { get; set; }
        public bool RequiresContinuingEducation { get; set; }
        public int ContinuingEducationHours { get; set; }

        // Override status to include continuing education requirements
        public override string Status
        {
            get
            {
                if (IsExpired)
                    return "Expired";
                else if (RequiresContinuingEducation && ContinuingEducationHours > 0)
                    return "CE Credits Required";
                else if (DaysRemaining <= 60)
                    return "Renewal Required Soon";
                else
                    return "Valid";
            }
        }

        // Enhanced display
        public new string ToString()
        {
            return $"{Name} - {IssuingOrganization} (ID: {CertificationID})" +
                   $"\nExpires: {ExpirationDate:d}, Status: {Status}" +
                   $"\nCE Hours Required: {(RequiresContinuingEducation ? ContinuingEducationHours.ToString() : "N/A")}";
        }
    }

    // Compliance Certification Class - Inherits from BaseCertification
    public class ComplianceCertification : BaseCertification
    {
        public string ComplianceStandard { get; set; }
        public string RegulatorAuthority { get; set; }
        public bool IsMandatory { get; set; }

        // Override status to include compliance-specific statuses
        public override string Status
        {
            get
            {
                if (IsExpired)
                    return IsMandatory ? "NON-COMPLIANT" : "Expired";
                else if (DaysRemaining <= 14)
                    return "URGENT RENEWAL NEEDED";
                else if (DaysRemaining <= 30)
                    return "Renewal Required";
                else
                    return "Compliant";
            }
        }

        // Enhanced display
        public new string ToString()
        {
            string mandatoryStatus = IsMandatory ? "MANDATORY" : "Optional";
            return $"{Name} - {ComplianceStandard} ({mandatoryStatus})" +
                   $"\nRegulator: {RegulatorAuthority}" +
                   $"\nExpires: {ExpirationDate:d}, Status: {Status}";
        }
    }
}