using System;
using System.Collections.Generic;
using System.Text;

namespace Willowsoft.TamperProofData
{
    /// <summary>
    /// An implementation of IStandardLicense based on TamperProofData.Validator
    /// and TamperProofData.LicenseReader.
    /// </summary>
    /// <typeparam name="TValidator"></typeparam>
    public abstract class StandardLicenseBase<TValidator> : IStandardLicense
        where TValidator : Validator, new()
    {
        private Dictionary<string, string> mValues = null;
        private LicenseStatus mStatus = LicenseStatus.NotLoaded;

        protected string GetValue(string key)
        {
            string value = null;
            if (mValues.TryGetValue(key, out value))
                return value;
            else
                return null;
        }

        public abstract string BaseFileName { get; }

        public LicenseStatus Status => mStatus;

        public abstract string LicenseTitle { get; }

        public string LicensedTo => GetValue(StandardLicenseBuilder.LicensedToKey);

        public DateTime? ExpirationDate
        {
            get
            {
                string value;
                if (mValues.TryGetValue(StandardLicenseBuilder.ExpirationDateKey, out value))
                    return DateTime.Parse(value);
                else
                    return null;
            }
        }

        public string EmailAddress => GetValue(StandardLicenseBuilder.EmailAddressKey);

        public string SerialNumber => GetValue(StandardLicenseBuilder.SerialNumberKey);

        public int LicenseVersion
        {
            get
            {
                string value;
                if (mValues.TryGetValue(StandardLicenseBuilder.LicenseVersionKey, out value))
                    return int.Parse(value);
                else
                    return 0;
            }
        }

        public abstract string AttributeSummary { get; }

        public Dictionary<string, string> Values => mValues;

        public void Load(string licenseFolder)
        {
            string strLicenseFile = System.IO.Path.Combine(licenseFolder, BaseFileName);
            mValues = null;
            mStatus = LicenseStatus.NotLoaded;
            if (System.IO.File.Exists(strLicenseFile))
            {
                using (System.IO.Stream licenseStream = new System.IO.FileStream(strLicenseFile, System.IO.FileMode.Open))
                {
                    try
                    {
                        mValues = Willowsoft.TamperProofData.LicenseReader.Read(licenseStream, new TValidator());
                        mStatus = LicenseStatus.Active;
                        DateTime? expDate = ExpirationDate;
                        if (expDate.HasValue)
                        {
                            if (expDate < DateTime.Today)
                            {
                                mStatus = LicenseStatus.Expired;
                                return;
                            }
                        }
                    }
                    catch (System.IO.InvalidDataException)
                    {
                        mStatus = LicenseStatus.Invalid;
                        return;
                    }
                }
            }
            else
            {
                mStatus = LicenseStatus.Missing;
                return;
            }
        }
    }
}
