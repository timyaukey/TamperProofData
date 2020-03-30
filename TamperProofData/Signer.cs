using System;
using System.IO;
using System.Security.Cryptography;

namespace Willowsoft.TamperProofData
{
    public abstract class Signer
    {
        private RSAParameters PrivateKey_;

        public Signer()
        {
            PrivateKey_ = GetPrivateKey();
        }

        protected abstract RSAParameters GetPrivateKey();

        public byte[] MakeSignature(byte[] data)
        {
            using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())
            {
                rsa.ImportParameters(PrivateKey_);
                //The hash to sign.
                byte[] hash;
                using (SHA256 sha256 = SHA256.Create())
                {
                    hash = sha256.ComputeHash(data);
                }

                //Create an RSASignatureFormatter object and pass it the 
                //RSACryptoServiceProvider to transfer the key information.
                RSAPKCS1SignatureFormatter formatter = new RSAPKCS1SignatureFormatter(rsa);

                //Set the hash algorithm to SHA256.
                formatter.SetHashAlgorithm("SHA256");

                //Create a signature for HashValue and return it.
                byte[] signedHash = formatter.CreateSignature(hash);
                return signedHash;
            }
        }
    }
}
