﻿using System;
using System.Security.Cryptography;

namespace TamperProofUnitTests
{
    // Code generated by TamperProofCoder.CodeFactory
    // WARNING! This signer class contains your private key, and must NEVER be
    // distributed to end users in source or compiled form! It generally belongs
    // to a separate project/assembly used only for generating signatures.
    public class TestSigner : Willowsoft.TamperProofData.Signer
    {
        protected override RSAParameters GetPrivateKey()
        {
            RSAParameters rsap = new RSAParameters();
            rsap.D = new byte[] { 125, 123, 248, 84, 49, 7, 172, 171, 119, 106, 220, 46, 127, 192, 18, 116,
              33, 95, 216, 30, 137, 97, 45, 104, 0, 160, 121, 211, 108, 153, 107, 54,
              144, 217, 144, 103, 6, 195, 31, 235, 224, 179, 22, 122, 21, 113, 235, 153,
              158, 187, 137, 34, 168, 97, 28, 96, 118, 90, 221, 29, 46, 247, 166, 170,
              165, 158, 113, 247, 99, 127, 95, 8, 231, 12, 227, 118, 185, 63, 173, 245,
              118, 95, 112, 108, 36, 170, 230, 72, 116, 245, 127, 8, 79, 55, 250, 144,
              197, 109, 226, 40, 9, 193, 145, 149, 198, 220, 7, 38, 6, 113, 143, 126,
              100, 134, 25, 3, 237, 46, 48, 114, 171, 139, 230, 79, 221, 128, 153, 125 };
            rsap.DP = new byte[] { 117, 95, 0, 36, 203, 34, 197, 217, 248, 67, 215, 7, 230, 162, 215, 79,
              23, 193, 19, 73, 115, 176, 220, 120, 113, 79, 152, 172, 255, 91, 88, 132,
              138, 227, 3, 47, 160, 156, 6, 72, 205, 43, 63, 40, 21, 42, 149, 186,
              239, 116, 187, 43, 151, 58, 139, 106, 191, 253, 142, 28, 58, 15, 118, 35 };
            rsap.DQ = new byte[] { 220, 0, 194, 106, 228, 114, 232, 114, 151, 42, 81, 51, 115, 253, 208, 147,
              5, 13, 184, 45, 197, 205, 164, 11, 63, 175, 247, 165, 173, 14, 174, 244,
              244, 108, 139, 243, 54, 110, 217, 73, 149, 12, 216, 185, 176, 23, 156, 204,
              18, 67, 43, 222, 152, 233, 158, 239, 71, 106, 241, 156, 11, 86, 177, 163 };
            rsap.Exponent = new byte[] { 1, 0, 1 };
            rsap.InverseQ = new byte[] { 62, 176, 51, 115, 99, 107, 166, 134, 73, 71, 36, 2, 117, 208, 93, 10,
              196, 110, 169, 122, 68, 118, 204, 89, 178, 241, 80, 118, 204, 140, 237, 255,
              126, 63, 146, 98, 96, 158, 161, 84, 66, 210, 245, 16, 170, 228, 206, 228,
              35, 19, 17, 52, 41, 125, 86, 156, 95, 203, 77, 51, 245, 5, 188, 225 };
            rsap.Modulus = new byte[] { 175, 94, 24, 136, 157, 16, 212, 22, 170, 144, 26, 199, 67, 162, 125, 5,
              241, 164, 134, 178, 146, 124, 64, 80, 66, 42, 110, 10, 196, 121, 98, 169,
              22, 104, 240, 151, 171, 29, 1, 230, 85, 204, 137, 247, 98, 160, 186, 205,
              251, 32, 62, 224, 187, 207, 112, 218, 6, 148, 115, 2, 179, 224, 245, 71,
              250, 5, 24, 103, 91, 165, 33, 243, 249, 97, 3, 150, 89, 169, 184, 242,
              161, 39, 237, 84, 12, 45, 80, 169, 242, 253, 98, 167, 157, 176, 77, 209,
              180, 2, 125, 228, 222, 146, 189, 191, 5, 222, 3, 25, 147, 55, 233, 148,
              71, 248, 153, 156, 159, 173, 16, 166, 83, 44, 16, 210, 181, 15, 208, 9 };
            rsap.P = new byte[] { 200, 245, 200, 183, 2, 16, 79, 234, 12, 235, 140, 191, 197, 165, 10, 167,
              87, 213, 247, 51, 182, 80, 14, 184, 246, 222, 251, 22, 149, 253, 72, 166,
              231, 107, 21, 95, 106, 232, 34, 221, 37, 11, 175, 175, 88, 3, 86, 47,
              170, 209, 121, 163, 112, 131, 191, 13, 206, 147, 186, 41, 88, 207, 30, 59 };
            rsap.Q = new byte[] { 223, 101, 231, 126, 103, 40, 192, 72, 218, 199, 60, 190, 223, 14, 56, 215,
              196, 64, 131, 252, 124, 82, 158, 248, 196, 159, 111, 2, 25, 222, 233, 104,
              213, 216, 169, 122, 121, 103, 238, 173, 25, 31, 17, 135, 171, 31, 103, 244,
              187, 67, 19, 215, 228, 230, 229, 70, 5, 129, 159, 215, 62, 138, 210, 139 };
            return rsap;
        }
    }
}
