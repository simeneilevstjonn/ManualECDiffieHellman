using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ManualEllipticCurveDiffieHellman
{
    internal class ECDHHElper
    {
        ECDiffieHellmanCng DH { get; set; }

        /// <summary>
        /// Gets the public key to send to the target.
        /// </summary>
        internal byte[] PublicKey { get => DH.PublicKey.ToByteArray(); }

        /// <summary>
        /// Constructs a new instance of the ECDHHElper class.
        /// </summary>
        internal ECDHHElper() 
        {
            // Create DH
            DH = new();

            // Set algorithms
            DH.KeyDerivationFunction = ECDiffieHellmanKeyDerivationFunction.Hash;
            DH.HashAlgorithm = CngAlgorithm.Sha256;
        }

        /// <summary>
        /// Is used to derive a shared AES key from another EC public key.
        /// </summary>
        /// <param name="TargetKey">The target's public EC key, in a byte array.</param>
        /// <returns>A shared AES key in a byte array.</returns>
        internal byte[] DeriveSharedKey(byte[] TargetKey) 
        {
            // Import key
            CngKey TargetCngKey = CngKey.Import(TargetKey, CngKeyBlobFormat.EccPublicBlob);

            return DH.DeriveKeyMaterial(TargetCngKey);
        }
    }
}
