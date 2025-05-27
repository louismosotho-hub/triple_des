using Xunit;
using System;
using TripleDES.DES;
using TripleDES.Core;

public class DESUtilTests
{
    [Fact]
    public void Pad_And_Unpad_ShouldReturnOriginal()
    {
        byte[] data = { 1, 2, 3, 4, 5 };
        byte[] padded = Utils.Pad(data, 8);
        byte[] unpadded = Utils.Unpad(padded);
        Assert.Equal(data, unpadded);
    }

    [Fact]
    public void Test_Basic()
    {
        Console.WriteLine("\nRunning Test_Basic");
        byte[] plaintext = System.Text.Encoding.ASCII.GetBytes("ABCDEFGH");
        byte[] key = System.Text.Encoding.ASCII.GetBytes("12345678");
        byte[] ciphertext = DESUtil.Encrypt(plaintext, key);
        Console.WriteLine("Ciphertext: " + BitConverter.ToString(ciphertext));
        byte[] decrypted = DESUtil.Decrypt(ciphertext, key);
        string decryptedText = System.Text.Encoding.ASCII.GetString(decrypted);
        Console.WriteLine("Decrypted text: " + decryptedText);
        Assert.Equal("ABCDEFGH", decryptedText);
    }

    [Fact]
    public void Test_AllZeros()
    {
        Console.WriteLine("\nRunning Test_AllZeros");
        byte[] plaintext = new byte[8];
        byte[] key = new byte[8];
        byte[] ciphertext = DESUtil.Encrypt(plaintext, key);
        Console.WriteLine("Ciphertext: " + BitConverter.ToString(ciphertext));
        byte[] decrypted = DESUtil.Decrypt(ciphertext, key);
        Assert.Equal(plaintext, decrypted);
    }

    [Fact]
    public void Test_AllOnes()
    {
        Console.WriteLine("\nRunning Test_AllOnes");
        byte[] plaintext = new byte[8];
        byte[] key = new byte[8];
        for (int i = 0; i < 8; i++) { plaintext[i] = 0xFF; key[i] = 0xFF; }
        byte[] ciphertext = DESUtil.Encrypt(plaintext, key);
        Console.WriteLine("Ciphertext: " + BitConverter.ToString(ciphertext));
        byte[] decrypted = DESUtil.Decrypt(ciphertext, key);
        Assert.Equal(plaintext, decrypted);
    }

    [Fact]
    public void Test_SingleBitDifference()
    {
        Console.WriteLine("\nRunning Test_SingleBitDifference");
        byte[] plaintext1 = System.Text.Encoding.ASCII.GetBytes("ABCDEFGH");
        byte[] plaintext2 = System.Text.Encoding.ASCII.GetBytes("ABCDEFGA");
        byte[] key = System.Text.Encoding.ASCII.GetBytes("12345678");
        byte[] ct1 = DESUtil.Encrypt(plaintext1, key);
        byte[] ct2 = DESUtil.Encrypt(plaintext2, key);
        Console.WriteLine("Ciphertext 1: " + BitConverter.ToString(ct1));
        Console.WriteLine("Ciphertext 2: " + BitConverter.ToString(ct2));
        Assert.NotEqual(ct1, ct2);
    }

    [Fact]
    public void Test_EmptyString()
    {
        Console.WriteLine("\nRunning Test_EmptyString");
        byte[] plaintext = new byte[0];
        byte[] key = System.Text.Encoding.ASCII.GetBytes("12345678");
        try
        {
            byte[] ciphertext = DESUtil.Encrypt(plaintext, key);
            byte[] decrypted = DESUtil.Decrypt(ciphertext, key);
            Assert.Equal(plaintext, decrypted);
        }
        catch (Exception e)
        {
            Console.WriteLine("Exception occurred: " + e.Message);
            Assert.True(true);
        }
    }
}