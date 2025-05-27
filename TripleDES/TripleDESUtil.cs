using System;
using TripleDES.DES;

namespace TripleDES.TripleDES
{
    public static class TripleDESUtil
    {
        public static byte[] Encrypt(byte[] plaintext, byte[] key1, byte[] key2, byte[] key3 = null)
        {
            if (plaintext == null || key1 == null || key2 == null)
                throw new ArgumentNullException("Input parameters cannot be null.");
            if (key1.Length != 8 || key2.Length != 8 || (key3 != null && key3.Length != 8))
                throw new ArgumentException("Keys must be 8 bytes long.");

            key3 ??= key1;

            byte[] step1 = DESUtil.Encrypt(plaintext, key1);
            byte[] step2 = DESUtil.Decrypt(step1, key2);
            byte[] step3 = DESUtil.Encrypt(step2, key3);

            return step3;
        }

        public static byte[] Decrypt(byte[] ciphertext, byte[] key1, byte[] key2, byte[] key3 = null)
        {
            if (ciphertext == null || key1 == null || key2 == null)
                throw new ArgumentNullException("Input parameters cannot be null.");
            if (key1.Length != 8 || key2.Length != 8 || (key3 != null && key3.Length != 8))
                throw new ArgumentException("Keys must be 8 bytes long.");

            key3 ??= key1;

            byte[] step1 = DESUtil.Decrypt(ciphertext, key3);
            byte[] step2 = DESUtil.Encrypt(step1, key2);
            byte[] step3 = DESUtil.Decrypt(step2, key1);

            return step3;
        }
    }
}