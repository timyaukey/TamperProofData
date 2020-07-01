using System;
using System.Collections.Generic;
using System.IO;
using NUnit.Framework;

using Willowsoft.TamperProofCoder;
using Willowsoft.TamperProofData;

namespace TamperProofUnitTests
{
    public class TamperProofFixture
    {
        [Test]
        public void ValidationPasses()
        {
            TestSigner signer = new TestSigner();
            byte[] data = new byte[] { 10, 22, 5, 0, 200};
            byte[] signature = signer.MakeSignature(data);
            TestValidator validator = new TestValidator();
            Assert.IsTrue(validator.IsValid(data, signature), "Did not validate");
        }

        [Test]
        public void DataChangeBreaks()
        {
            TestSigner signer = new TestSigner();
            byte[] data = new byte[] { 10, 22, 5, 0, 200 };
            byte[] signature = signer.MakeSignature(data);
            TestValidator validator = new TestValidator();
            data[1] = 23;
            Assert.IsFalse(validator.IsValid(data, signature), "Should not validate");
        }

        [Test]
        public void SignatureChangeBreaks()
        {
            TestSigner signer = new TestSigner();
            byte[] data = new byte[] { 10, 22, 5, 0, 200 };
            byte[] signature = signer.MakeSignature(data);
            TestValidator validator = new TestValidator();
            signature[1] = (byte)(255 - signature[1]);
            Assert.IsFalse(validator.IsValid(data, signature), "Should not validate");
        }

        [Test]
        public void FactoryRunsAreDifferent()
        {
            StringWriter output1 = new StringWriter();
            StringWriter output2 = new StringWriter();
            CodeFactory.OutputClasses(output1);
            CodeFactory.OutputClasses(output2);
            Assert.That(output1.ToString().Substring(0, 20), Is.EqualTo(output2.ToString().Substring(0, 20)), 
                "Beginning of output should be the same");
            string modulus1 = output1.ToString().Substring(output1.ToString().IndexOf("rsap.Modulus"), 50);
            string modulus2 = output2.ToString().Substring(output2.ToString().IndexOf("rsap.Modulus"), 50);
            Assert.That(modulus1, Is.Not.EqualTo(modulus2), "Exponents should be different");
        }

        [Test]
        public void LicenseValid()
        {
            Dictionary<string, string> values = new Dictionary<string, string>();
            values.Add("Animal1", "Cat");
            values.Add("Animal2", "Winged Horse");
            MemoryStream licenseStream = new MemoryStream();
            LicenseWriter.Write(values, new TestSigner(), licenseStream);
            licenseStream.Position = 0;
            Dictionary<string, string> decodedValues = LicenseReader.Read(licenseStream, new TestValidator());
            Assert.That(decodedValues.Count, Is.EqualTo(2), "Wrong number of license values");
            Assert.That(decodedValues["Animal1"], Is.EqualTo("Cat"), "Animal1 should have been Cat");
            Assert.That(decodedValues["Animal2"], Is.EqualTo("Winged Horse"), "Animal2 should have been Winged Horse");
        }

        [Test]
        public void LicenseSignatureInvalid1()
        {
            Dictionary<string, string> values = new Dictionary<string, string>();
            values.Add("Animal1", "Cat");
            values.Add("Animal2", "Winged Horse");
            MemoryStream licenseStream = new MemoryStream();
            LicenseWriter.Write(values, new TestSigner(), licenseStream);
            licenseStream.Position = 150;
            licenseStream.WriteByte(1);
            licenseStream.Position = 0;
            try
            {
                Dictionary<string, string> decodedValues = LicenseReader.Read(licenseStream, new TestValidator());
                Assert.Fail("Should have thrown exception");
            }
            catch (InvalidDataException ex)
            {

            }
        }

        [Test]
        public void LicenseSignatureInvalid2()
        {
            Dictionary<string, string> values = new Dictionary<string, string>();
            values.Add("Animal1", "Cat");
            values.Add("Animal2", "Winged Horse");
            MemoryStream licenseStream = new MemoryStream();
            LicenseWriter.Write(values, new TestSigner(), licenseStream);
            licenseStream.Position = 30;
            licenseStream.WriteByte(1);
            licenseStream.Position = 0;
            try
            {
                Dictionary<string, string> decodedValues = LicenseReader.Read(licenseStream, new TestValidator());
                Assert.Fail("Should have thrown exception");
            }
            catch (InvalidDataException ex)
            {

            }
        }

        [Test]
        public void LicenseSignatureInvalid3()
        {
            Dictionary<string, string> values = new Dictionary<string, string>();
            values.Add("Animal1", "Cat");
            values.Add("Animal2", "Winged Horse");
            MemoryStream licenseStream = new MemoryStream();
            LicenseWriter.Write(values, new TestSigner(), licenseStream);
            licenseStream.Position = 1;
            licenseStream.WriteByte(1);
            licenseStream.Position = 0;
            try
            {
                Dictionary<string, string> decodedValues = LicenseReader.Read(licenseStream, new TestValidator());
                Assert.Fail("Should have thrown exception");
            }
            catch (InvalidDataException ex)
            {

            }
        }
    }
}