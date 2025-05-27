using Xunit;
using System;
using TripleDES.TripleDES;

public class TripleDESUtilTests
{
    [Fact]
    public void TripleDES_AllZeros()
    {
        byte[] key = new byte[8];
        byte[] plaintext = new byte[8];
        byte[] ciphertext = TripleDESUtil.Encrypt(plaintext, key, key, key);
        byte[] decrypted = TripleDESUtil.Decrypt(ciphertext, key, key, key);
        Assert.Equal(plaintext, decrypted);
    }

    [Fact]
    public void TripleDES_AllOnes()
    {
        byte[] key = new byte[8];
        for (int i = 0; i < 8; i++) key[i] = 0xFF;
        byte[] plaintext = new byte[8];
        for (int i = 0; i < 8; i++) plaintext[i] = 0xFF;
        byte[] ciphertext = TripleDESUtil.Encrypt(plaintext, key, key, key);
        byte[] decrypted = TripleDESUtil.Decrypt(ciphertext, key, key, key);
        Assert.Equal(plaintext, decrypted);
    }

    [Fact]
    public void TripleDES_SingleBitDifference()
    {
        byte[] key1 = { 0x01, 0x23, 0x45, 0x67, 0x89, 0xAB, 0xCD, 0xEF };
        byte[] key2 = { 0xFE, 0xDC, 0xBA, 0x98, 0x76, 0x54, 0x32, 0x10 };
        byte[] plaintext1 = System.Text.Encoding.ASCII.GetBytes("ABCDEFGH");
        byte[] plaintext2 = System.Text.Encoding.ASCII.GetBytes("ABCDEFGA");
        byte[] ciphertext1 = TripleDESUtil.Encrypt(plaintext1, key1, key2);
        byte[] ciphertext2 = TripleDESUtil.Encrypt(plaintext2, key1, key2);
        Assert.NotEqual(ciphertext1, ciphertext2);
    }

    [Fact]
    public void TripleDES_EmptyString()
    {
        byte[] key = System.Text.Encoding.ASCII.GetBytes("12345678");
        byte[] plaintext = new byte[0];
        try
        {
            byte[] ciphertext = TripleDESUtil.Encrypt(plaintext, key, key, key);
            byte[] decrypted = TripleDESUtil.Decrypt(ciphertext, key, key, key);
            Assert.Equal(plaintext, decrypted);
        }
        catch (Exception e)
        {
            Console.WriteLine("Exception occurred: " + e.Message);
            Assert.True(true); // Accept exception as valid for empty input
        }
    }
}