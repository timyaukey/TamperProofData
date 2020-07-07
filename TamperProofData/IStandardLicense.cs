using System;
using System.Collections.Generic;

namespace Willowsoft.TamperProofData
{
    /// <summary>
    /// Interface defining a license suitable for most user stories.
    /// The license will be loaded from a file on disk.
    /// Implemented by StandardLicenseBase and StandardLicenseBuilder.
    /// </summary>
    public interface IStandardLicense
    {
        /// <summary>
        /// Load license information from file on disk, and set "Status" property.
        /// "Status" is always LicenseStatus.NotLoaded before Load() is called.
        /// </summary>
        /// <param name="licenseFolder"></param>
        void Load(string licenseFolder);
        
        /// <summary>
        /// The file name used by Load(), without path.
        /// </summary>
        string BaseFileName { get; }
        
        /// <summary>
        /// The result status set by Load().
        /// </summary>
        LicenseStatus Status { get; }

        /// <summary>
        /// A label describing the type of license, like "XYZZY Software License".
        /// </summary>
        string LicenseTitle { get; }

        /// <summary>
        /// The entity this license is granted to. May be the name of a person or organization.
        /// </summary>
        string LicensedTo { get; }

        /// <summary>
        /// If not null, the last date this license if valid for.
        /// </summary>
        DateTime? ExpirationDate { get; }

        /// <summary>
        /// The email address of the entity this license instance is granted to.
        /// May be blank.
        /// </summary>
        string EmailAddress { get; }

        /// <summary>
        /// The serial number of this license instance.
        /// May be blank.
        /// </summary>
        string SerialNumber { get; }

        /// <summary>
        /// The version of this license instance. May be checked by the licensed
        /// software to decide if the license is valid with the software version.
        /// </summary>
        int LicenseVersion { get; }

        /// <summary>
        /// A summary of capabilities granted by this license instance, like
        /// "Standard" or "Platinum". May be blank.
        /// </summary>
        string AttributeSummary { get; }

        /// <summary>
        /// A dictionary containing all license attributes, including the ones
        /// exposed by other IStandardLicense properties.
        /// </summary>
        Dictionary<string, string> Values { get; }
    }

    /// <summary>
    /// The result of calling IStandardLicense.Load().
    /// </summary>
    public enum LicenseStatus
    {
        NotLoaded = 0,
        Invalid = 1,
        Missing = 2,
        Expired = 3,
        Active = 4      // This is the only status that indicates a valid and usable license
    }

}
