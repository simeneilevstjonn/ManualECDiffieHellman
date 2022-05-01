# ManualECDiffieHellman
Manually encrypt your chats over insecure channels using Elliptic Curve Diffie Hellman and AES.

This program generates an elliptic curve public key for you, and accepts you chat target's key as an input. These are combined to form a shared AES secret. You can then encrypt messages using this AES key.

## Installation
The crypto libraries on which this program depends, only work on Windows.

To build, run:
```
dotnet build
```

## Usage
The program will generate your public key immediately upon launch. Send this to your target, and paste their key in.
You will now have set up an AES secret.
You can encrypt messages by inputting text, prefixed by an 'e'. Whitespace on either end are ignored.
For instance:
```
e Hello world
```
You can then send the encrypted line over an insecure channel.


To decrypt, prefix the line by a 'd'. Then the two base64 strings, comma separated, should follow.
For instance:
```
d tpmwfefBsn5B/t5RS/rVsw==,1ICWhfYaaFmrC6ph09hrSA==
```

## Precautions
This program features no protection for man in the middle attacks, as there is no checking who you are communicating with. This must only be used over a channel in which you know who you are talking with.
Furthermore, to improve security in case your key is compromised, a new key should ideally be created for each message.
