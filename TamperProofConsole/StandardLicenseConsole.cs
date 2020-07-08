using System;
using System.Collections.Generic;

namespace Willowsoft.TamperProofConsole
{
    /// <summary>
    /// Implement a console application that prompts for license information and
    /// then uses Willowsoft.TamperProofData to write it to a signed license file.
    /// To use simply create an instance in main(), and call Run().
    /// </summary>
    /// <typeparam name="TSigner">The signer class, which must be a subclass
    /// of Willowsoft.TamperProofData.Signer, whose source code is typically
    /// created by Willowsoft.TamperProofCoder.</typeparam>
    public class StandardLicenseConsole<TSigner>
        where TSigner : Willowsoft.TamperProofData.Signer, new()
    {
        protected string mSoftwareTitle;
        protected string mLicenseFileName;
        protected int mLicenseVersion;

        public StandardLicenseConsole(string softwareTitle, string licenseFileName, int licenseVersion)
        {
            mSoftwareTitle = softwareTitle;
            mLicenseFileName = licenseFileName;
            mLicenseVersion = licenseVersion;
        }

        public void Run()
        {
            Writer.WriteLine("Create License File For " + mSoftwareTitle);
            Writer.Write("Entity receiving license (e.g. \"John Smith\" or \"Chess Club\"): ");
            string licensedTo = Reader.ReadLine();
            Writer.Write("License expiration date (\"mm/dd/yyyy\", blank if never expires): ");
            string expDateText = Reader.ReadLine();
            DateTime? expirationDate;
            if (string.IsNullOrEmpty(expDateText))
                expirationDate = null;
            else
            {
                DateTime expDateTemp;
                if (!DateTime.TryParseExact(expDateText, "MM/dd/yyyy", null, System.Globalization.DateTimeStyles.None, out expDateTemp))
                {
                    Writer.WriteLine("Invalid expiration date.");
                    return;
                }
                expirationDate = expDateTemp;
            }
            Writer.Write("Email address of licensed entity: ");
            string emailAddress = Reader.ReadLine();
            Writer.Write("License serial number (anything will work): ");
            string serialNumber = Reader.ReadLine();
            if (string.IsNullOrEmpty(licensedTo) || string.IsNullOrEmpty(emailAddress) || string.IsNullOrEmpty(serialNumber))
            {
                Writer.WriteLine("Missing data.");
                return;
            }
            Dictionary<string, string> values = Willowsoft.TamperProofData.StandardLicenseBuilder.Build(
                licensedTo, expirationDate, emailAddress, serialNumber, mLicenseVersion);
            if (!AddExtraKeywords(values))
                return;
            using (System.IO.Stream output = new System.IO.FileStream(mLicenseFileName, System.IO.FileMode.Create))
            {
                Willowsoft.TamperProofData.LicenseWriter.Write(values, new TSigner(), output);
                Writer.Write("User license written to " + mLicenseFileName);
            }
        }

        protected virtual System.IO.TextReader Reader
        {
            get
            {
                return System.Console.In;
            }
        }

        protected virtual System.IO.TextWriter Writer
        {
            get
            {
                return System.Console.Out;
            }
        }

        /// <summary>
        /// Override in a subclass to prompt for additional information and add it to "values".
        /// Must output a message and return "false" if the user cancels or enters invalid data.
        /// </summary>
        /// <param name="values"></param>
        /// <returns>Returns "true" if successful, or nothing to add.</returns>
        protected virtual bool AddExtraKeywords(Dictionary<string, string> values)
        {
            return true;
        }
    }
}
