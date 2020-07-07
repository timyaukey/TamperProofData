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
    }
}
