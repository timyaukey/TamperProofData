﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Willowsoft.TamperProofData
{
    /// <summary>
    /// Read (and validate) a file generated by TamperProofData.LicenseWriter,
    /// and return the Dictionary<string, string> it was generated from.
    /// Will throw an InvalidDataException if the contents of the file were
    /// modified, i.e. the RSA signature does not match the contents. 
    /// </summary>
    public static class LicenseReader
    {
        public static Dictionary<string, string> Read(Stream input, Validator validator)
        {
            try
            {
                BinaryReader licenseReader = new BinaryReader(input, Encoding.Unicode);
                byte[] headerBytes = licenseReader.ReadBytes(LicenseWriter.LicenseHeader.Length);
                for (int headerIdx = 0; headerIdx < LicenseWriter.LicenseHeader.Length; headerIdx++)
                {
                    if (headerBytes[headerIdx] != LicenseWriter.LicenseHeader[headerIdx])
                        throw new InvalidDataException("License has incorrect prefix bytes");
                }
                int signatureLength = licenseReader.ReadInt32();
                byte[] signature = licenseReader.ReadBytes(signatureLength);
                int valueLength = licenseReader.ReadInt32();
                byte[] valueBytes = licenseReader.ReadBytes(valueLength);
                if (!validator.IsValid(valueBytes, signature))
                    throw new InvalidDataException("License data does not match signature");
                Dictionary<string, string> licenseValues = new Dictionary<string, string>();
                BinaryReader valueReader = new BinaryReader(new MemoryStream(valueBytes), Encoding.Unicode);
                int valueCount = valueReader.ReadInt32();
                for (int valueIdx = 0; valueIdx < valueCount; valueIdx++)
                {
                    string key = valueReader.ReadString();
                    string value = valueReader.ReadString();
                    licenseValues.Add(key, value);
                }
                return licenseValues;
            }
            catch (Exception ex)
            {
                throw new InvalidDataException("Exception reading signed data", ex);
            }
        }
    }
}
