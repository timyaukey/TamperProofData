using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Willowsoft.TamperProofData
{
    /// <summary>
    /// Serialize a Dictionary<string, string>, and an RSA signature computed from it,
    /// to a Stream object. This Stream can be persisted in a disk file. This file can be
    /// read by TamperProofData.LicenseReader, which will also validate that the
    /// file has not been altered since being generated. As the names suggest, these
    /// classes can be used to generate files containing license keys (or any other string
    /// data) that cannot be changed without being detected.
    /// </summary>
    public static class LicenseWriter
    {
        public static void Write(Dictionary<string, string> licenseValues, Signer signer, Stream output)
        {
            using (MemoryStream valueStream = new MemoryStream())
            {
                using (BinaryWriter valueWriter = new BinaryWriter(valueStream, Encoding.Unicode))
                {
                    valueWriter.Write(licenseValues.Count);
                    foreach (KeyValuePair<string, string> entry in licenseValues)
                    {
                        valueWriter.Write(entry.Key);
                        valueWriter.Write(entry.Value);
                    }
                    valueWriter.Flush();
                    byte[] valueBytes = valueStream.ToArray();
                    byte[] signature = signer.MakeSignature(valueBytes);
                    BinaryWriter licenseWriter = new BinaryWriter(output);
                    licenseWriter.Write(LicenseHeader);
                    licenseWriter.Write(signature.Length);
                    licenseWriter.Write(signature);
                    licenseWriter.Write(valueBytes.Length);
                    licenseWriter.Write(valueBytes);
                    licenseWriter.Flush();
                }
            }
        }

        // Arbitrary byte sequence to use as prefix for license stream,
        // to allow validation when the stream is read. The last byte
        // is a format version number, and will increment if the license 
        // format changes.
        public static byte[] LicenseHeader = new byte[] { 89, 30, 94, 1 };
    }
}
