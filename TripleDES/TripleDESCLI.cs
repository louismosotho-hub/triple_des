using System;
using System.Text;
using TripleDES.TripleDES;

class TripleDESCLI
{
    static void Main()
    {
        Console.WriteLine("Triple DES Cipher CLI");
        Console.WriteLine("---------------------");
        Console.Write("Enter mode (encrypt/decrypt): ");
        string mode = Console.ReadLine()?.Trim().ToLower();

        if (mode == "encrypt")
        {
            Console.Write("Enter text (8 characters): ");
            string text = Console.ReadLine()?.Trim() ?? "";
            Console.Write("Enter key1 (8 characters): ");
            string key1 = Console.ReadLine()?.Trim() ?? "";
            Console.Write("Enter key2 (8 characters): ");
            string key2 = Console.ReadLine()?.Trim() ?? "";
            Console.Write("Enter key3 (8 characters, or leave blank to reuse key1): ");
            string key3 = Console.ReadLine()?.Trim();

            if (text.Length != 8)
            {
                Console.WriteLine("Text must be exactly 8 characters long for encryption.");
                return;
            }
            if (key1.Length != 8 || key2.Length != 8 || (!string.IsNullOrEmpty(key3) && key3.Length != 8))
            {
                Console.WriteLine("Each key must be exactly 8 characters long.");
                return;
            }

            byte[] plaintext = Encoding.ASCII.GetBytes(text);
            byte[] k1 = Encoding.ASCII.GetBytes(key1);
            byte[] k2 = Encoding.ASCII.GetBytes(key2);
            byte[] k3 = string.IsNullOrEmpty(key3) ? k1 : Encoding.ASCII.GetBytes(key3);

            byte[] ciphertext = TripleDESUtil.Encrypt(plaintext, k1, k2, k3);
            string ciphertextHex = BitConverter.ToString(ciphertext).Replace("-", "");
            Console.WriteLine("Ciphertext (hex): " + ciphertextHex);
        }
        else if (mode == "decrypt")
        {
            Console.Write("Enter ciphertext (8 characters or 16 hex digits): ");
            string text = Console.ReadLine()?.Trim() ?? "";
            Console.Write("Enter key1 (8 characters): ");
            string key1 = Console.ReadLine()?.Trim() ?? "";
            Console.Write("Enter key2 (8 characters): ");
            string key2 = Console.ReadLine()?.Trim() ?? "";
            Console.Write("Enter key3 (8 characters, or leave blank to reuse key1): ");
            string key3 = Console.ReadLine()?.Trim();

            if (key1.Length != 8 || key2.Length != 8 || (!string.IsNullOrEmpty(key3) && key3.Length != 8))
            {
                Console.WriteLine("Each key must be exactly 8 characters long.");
                return;
            }

            byte[] ciphertext;
            if (text.Length == 16)
            {
                try
                {
                    ciphertext = new byte[8];
                    for (int i = 0; i < 8; i++)
                        ciphertext[i] = Convert.ToByte(text.Substring(i * 2, 2), 16);
                }
                catch
                {
                    Console.WriteLine("Invalid hex input for ciphertext.");
                    return;
                }
            }
            else if (text.Length == 8)
            {
                ciphertext = Encoding.ASCII.GetBytes(text);
            }
            else
            {
                Console.WriteLine("Ciphertext must be exactly 8 characters or 16 hex digits long for decryption.");
                return;
            }

            byte[] k1 = Encoding.ASCII.GetBytes(key1);
            byte[] k2 = Encoding.ASCII.GetBytes(key2);
            byte[] k3 = string.IsNullOrEmpty(key3) ? k1 : Encoding.ASCII.GetBytes(key3);

            byte[] plaintext = TripleDESUtil.Decrypt(ciphertext, k1, k2, k3);
            string plaintextStr = Encoding.ASCII.GetString(plaintext);
            Console.WriteLine("Plaintext: " + plaintextStr);
        }
        else
        {
            Console.WriteLine("Unknown mode. Please use 'encrypt' or 'decrypt'.");
        }
    }
}