using System;
using System.Collections.Generic;
using System.Text;

namespace Willowsoft.TamperProofData
{
    /// <summary>
    /// Construct a dictionary that can be used by LicenseWriter and a Signer
    /// subclass to create a license file that can be read by StandardLicenseBase.
    /// </summary>
    public static class StandardLicenseBuilder
    {
        public static Dictionary<string, string> Build(string licensedTo, DateTime? expirationDate,
            string emailAddress, string serialNumber, int licenseVersion)
        {
            Dictionary<string, string> values = new Dictionary<string, string>();
            values.Add(StandardLicenseBuilder.LicensedToKey, licensedTo);
            if (expirationDate.HasValue)
                values.Add(StandardLicenseBuilder.ExpirationDateKey, expirationDate.Value.ToShortDateString());
            values.Add(StandardLicenseBuilder.EmailAddressKey, emailAddress);
            values.Add(StandardLicenseBuilder.SerialNumberKey, serialNumber);
            values.Add(StandardLicenseBuilder.LicenseVersionKey, licenseVersion.ToString());
            return values;
        }
        
        public const string LicensedToKey = "LicensedTo";
        public const string ExpirationDateKey = "ExpirationDate";
        public const string EmailAddressKey = "EmailAddressKey";
        public const string SerialNumberKey = "SerialNumberKey";
        public const string LicenseVersionKey = "LicenseVersionKey";
    }
}
