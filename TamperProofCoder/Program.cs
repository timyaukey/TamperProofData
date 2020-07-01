using System;

namespace Willowsoft.TamperProofCoder
{
    /// <summary>
    /// Use this class to construct and output TamperProofData.Signer
    /// and TamperProofData.Validator subclasses for a related
    /// RSA private/public key pair.
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            CodeFactory.OutputClasses(Console.Out);
        }
    }
}
