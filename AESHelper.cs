using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ManualEllipticCurveDiffieHellman
{
    internal class AESHelper
    {
        byte[] Key { get; set; }

        /// <summary>
        /// Constructs a new instance of the AESHelper class.
        /// </summary>
        /// <param name="_Key">The shared AES key to use</param>
        internal AESHelper(byte[] _Key) 
        {
            Key = _Key;
        }

        /// <summary>
        /// Encrypts a message using the instanced encryption key.
        /// </summary>
        /// <param name="Input">An UTF-8 encoded input message to encrypt.</param>
        /// <param name="IV">The initialisation vector used by the encryption. This must be supplied to decrypt.</param>
        /// <returns>A byte array of the encrypted message.</returns>
        internal byte[] Encrypt(string Input, out byte[] IV) 
        {

            using Aes aes = Aes.Create();
            aes.Key = Key;
            IV = aes.IV;

            // Encrypt the message
            using MemoryStream CipherText = new();
            using CryptoStream cs = new(CipherText, aes.CreateEncryptor(), CryptoStreamMode.Write);

            byte[] Plaintext = Encoding.UTF8.GetBytes(Input);
            cs.Write(Plaintext, 0, Plaintext.Length);
            cs.Close();

            return CipherText.ToArray();
        }

        /// <summary>
        /// Decrypts a message using the instanced encryption key.
        /// </summary>
        /// <param name="Input">A byte array of the encrypted message.</param>
        /// <param name="IV">The initialisation vector used by the encryption.</param>
        /// <returns>An UTF-8 encoded string of the plaintext message.</returns>
        internal string Decrypt(byte[] Input, byte[] IV) 
        {
            using Aes aes = Aes.Create();
            aes.Key = Key;
            aes.IV = IV;

            // Decrypt the message
            using MemoryStream plaintext = new();
            using CryptoStream cs = new (plaintext, aes.CreateDecryptor(), CryptoStreamMode.Write);
            cs.Write(Input, 0, Input.Length);
            cs.Close();

            return Encoding.UTF8.GetString(plaintext.ToArray());
        }
    }
}
