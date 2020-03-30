using System;
using System.IO;
using System.Security.Cryptography;

namespace Willowsoft.TamperProofData
{
    public abstract class Validator
    {
        private RSAParameters PublicKey_;

        public Validator()
        {
            PublicKey_ = GetPublicKey();
        }

        protected abstract RSAParameters GetPublicKey();

        public bool IsValid(byte[] data, byte[] signature)
        {
            using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())
            {
                rsa.ImportParameters(PublicKey_);
                //The hash to validate.
                byte[] hash;
                using (SHA256 sha256 = SHA256.Create())
                {
                    hash = sha256.ComputeHash(data);
                }
                //Create an RSAPKCS1SignatureDeformatter object and pass it the  
                //RSACryptoServiceProvider to transfer the key information.
                RSAPKCS1SignatureDeformatter deformatter = new RSAPKCS1SignatureDeformatter(rsa);
                deformatter.SetHashAlgorithm("SHA256");
                //Verify the hash and return the result. 
                bool isValid = deformatter.VerifySignature(hash, signature);
                return isValid;
            }
        }
    }
}
