using ManualEllipticCurveDiffieHellman;
using System;

// Create diffie hellman helper and print public key
ECDHHElper ECDH = new();

Console.WriteLine("Your base64 encoded Ellipic Curve Diffie Hellman public key follows on the next line. Send this to the encryption target.");
Console.WriteLine(Convert.ToBase64String(ECDH.PublicKey));

// Prompt for target key
Console.WriteLine("\nPlease paste your target's public key.");

// Derive key
byte[]? Key = null;

while(Key == null) 
{
    try
    {   // Prompt for target key
        Console.Write(">");
        string TargetKey = Console.ReadLine() ?? string.Empty; ;
        TargetKey = TargetKey.Trim();

        // Attempt to derive key
        Key = ECDH.DeriveSharedKey(Convert.FromBase64String(TargetKey));
    }
    catch
    {
        // Print error
        Console.WriteLine("Invalid input, please try again.\n");
    }
}


// Print note
Console.WriteLine("Successfully derived a shared AES key.");

// Create aes helper
AESHelper AES = new(Key);

// Print info about AES
Console.WriteLine("You can now encrypt and decrypt messages using your shared key. Prefix your lines with 'e' to encrypt, and 'd' to decrypt. Lines prefixed '>' signal that it was user input. The encrypted output consists of two base64 values, separated by a comma.");

// Do an eternal loop with AES functions
while (true) 
{
    // Print prefixed line
    Console.Write(">");

    // Get input
    string Input = Console.ReadLine() ?? string.Empty;
    Input = Input.Trim();

    // Check for prefix
    // Encryption
    if (Input.ToLower()[0] == 'e') 
    {
        // Encrypt
        byte[] IV;
        byte[] Cipher = AES.Encrypt(Input[1..].Trim(), out IV);

        // Merge IV and cipher into one string
        string Output = Convert.ToBase64String(IV) + "," + Convert.ToBase64String(Cipher);

        // Print encrypted string
        Console.WriteLine(Output);
    }
    // Decryption
    else if (Input.ToLower()[0] == 'd') 
    {
        // Split input
        string[] Inputs = Input[1..].Trim().Split(',');

        // Validate that there are two parts
        if (Inputs.Length != 2) 
        {
            // Print error
            Console.WriteLine("Error, decryption input must contain exactly two comma-separated base64 values.\n");
            continue;
        }

        try 
        {
            // Convert to byte array
            byte[] IV = Convert.FromBase64String(Inputs[0].Trim());
            byte[] Cipher = Convert.FromBase64String(Inputs[1].Trim());

            // Print decrypted string
            Console.WriteLine(AES.Decrypt(Cipher, IV));
        }
        catch 
        {
            Console.WriteLine("Invalid input");
        }
    }
    // Error
    else 
    {
        // Print error
        Console.WriteLine("Error, line prefix must be 'e' to encrypt or 'd' to decrypt.");
    }

    // Print newline
    Console.WriteLine();
}

