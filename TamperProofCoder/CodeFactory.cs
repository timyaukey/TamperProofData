﻿using System;
using System.IO;
using System.Security.Cryptography;

namespace Willowsoft.TamperProofCoder
{
    public static class CodeFactory
    {
        public static void OutputClasses(TextWriter output)
        {
            using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())
            {
                OutputSignerClass(output, rsa, "        ");
                output.WriteLine();
                OutputValidatorClass(output, rsa, "        ");
            }
        }

        private static void OutputSignerClass(TextWriter output, RSACryptoServiceProvider rsa, string indent)
        {
            output.WriteLine(indent + "// Code generated by " + nameof(TamperProofCoder) + "." + nameof(CodeFactory));
            output.WriteLine(indent + "// WARNING! This signer class contains your private key, and must NEVER be");
            output.WriteLine(indent + "// distributed to end users in source or compiled form! It generally belongs");
            output.WriteLine(indent + "// to a separate project/assembly used only for generating signatures.");
            output.WriteLine(indent + "public class YourOwnSignerClassName : Willowsoft.TamperProofData.Signer");
            output.WriteLine(indent + "{");
            output.WriteLine(indent + "  protected override RSAParameters GetPrivateKey()");
            output.WriteLine(indent + "  {");
            output.WriteLine(indent + "    RSAParameters rsap = new RSAParameters();");
            RSAParameters rsap = rsa.ExportParameters(true);
            OutputArray(output, indent, nameof(rsap.D), rsap.D);
            OutputArray(output, indent, nameof(rsap.DP), rsap.DP);
            OutputArray(output, indent, nameof(rsap.DQ), rsap.DQ);
            OutputArray(output, indent, nameof(rsap.Exponent), rsap.Exponent);
            OutputArray(output, indent, nameof(rsap.InverseQ), rsap.InverseQ);
            OutputArray(output, indent, nameof(rsap.Modulus), rsap.Modulus);
            OutputArray(output, indent, nameof(rsap.P), rsap.P);
            OutputArray(output, indent, nameof(rsap.Q), rsap.Q);
            output.WriteLine(indent + "    return rsap;");
            output.WriteLine(indent + "  }");
            output.WriteLine(indent + "}");
        }

        private static void OutputValidatorClass(TextWriter output, RSACryptoServiceProvider rsa, string indent)
        {
            output.WriteLine(indent + "// Code generated by " + nameof(TamperProofCoder) + "." + nameof(CodeFactory));
            output.WriteLine(indent + "public class YourOwnValidatorClassName : Willowsoft.TamperProofData.Validator");
            output.WriteLine(indent + "{");
            output.WriteLine(indent + "  protected override RSAParameters GetPublicKey()");
            output.WriteLine(indent + "  {");
            output.WriteLine(indent + "    RSAParameters rsap = new RSAParameters();");
            RSAParameters rsap = rsa.ExportParameters(false);
            OutputArray(output, indent, nameof(rsap.Exponent), rsap.Exponent);
            OutputArray(output, indent, nameof(rsap.Modulus), rsap.Modulus);
            output.WriteLine(indent + "    return rsap;");
            output.WriteLine(indent + "  }");
            output.WriteLine(indent + "}");
        }

        private static void OutputArray(TextWriter output, string indent, string fieldName, byte[] values)
        {
            string prefix = indent + "    rsap." + fieldName + " = new byte[] { ";
            int numbersOnCurrentLine = 0;
            foreach(byte v in values)
            {
                output.Write(prefix);
                if (numbersOnCurrentLine >= 16)
                {
                    output.WriteLine();
                    output.Write(indent + "      ");
                    numbersOnCurrentLine = 0;
                }
                output.Write(v.ToString());
                numbersOnCurrentLine++;
                prefix = ", ";
            }
            output.WriteLine(" };");
        }
    }
}
