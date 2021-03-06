﻿using System;
using System.IO;
using System.Security.Cryptography;

namespace Willowsoft.TamperProofData
{
    /// <summary>
    /// Generate cryptographic RSA signature for arbitrary data.
    /// Must be subclassed to implement GetPrivateKey().
    /// A signature created by this class may be validated by using
    /// a subclass of TamperProofData.Validator which has been
    /// coded with the public key corresponding to the private
    /// key returned by GetPrivateKey().
    /// Source code for subclasses, including the private/public key
    /// pair, is intended to be generated by
    /// TamperProofCoder.CodeFactory.OutputClasses().
    /// </summary>
    public abstract class Signer
    {
        private RSAParameters PrivateKey_;

        public Signer()
        {
            PrivateKey_ = GetPrivateKey();
        }

        protected abstract RSAParameters GetPrivateKey();

        /// <summary>
        /// Generate an RSA cryptographic signature for the byte array passed in.
        /// Computes a SHA256 hash for the data passed, and a signature for that hash.
        /// </summary>
        /// <param name="data"></param>
        /// <returns>Returns the computed RSA signature.</returns>
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
