using System;
using System.Collections.Generic;
using System.Text;

using Willowsoft.TamperProofData;

namespace TamperProofUnitTests
{
    public class TestLicense : StandardLicenseBase<TestValidator>
    {
        public override string BaseFileName => "UTTest.lic";

        public override string LicenseTitle => "Test License";

        public override string AttributeSummary => "Attribs";

        public override string LicenseStatement => "License statement text";

        public override Uri LicenseUrl => new Uri("http://microsoft.com/license");

        public override Uri ProductUrl => new Uri("Http://google.com");
    }
}
