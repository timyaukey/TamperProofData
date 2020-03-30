using System;
using System.IO;
using NUnit.Framework;

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
            Willowsoft.TamperProofCoder.CodeFactory.OutputClasses(output1);
            Willowsoft.TamperProofCoder.CodeFactory.OutputClasses(output2);
            Assert.That(output1.ToString().Substring(0, 20), Is.EqualTo(output2.ToString().Substring(0, 20)), 
                "Beginning of output should be the same");
            string modulus1 = output1.ToString().Substring(output1.ToString().IndexOf("rsap.Modulus"), 50);
            string modulus2 = output2.ToString().Substring(output2.ToString().IndexOf("rsap.Modulus"), 50);
            Assert.That(modulus1, Is.Not.EqualTo(modulus2), "Exponents should be different");
        }
    }
}