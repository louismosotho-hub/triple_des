using System;
using TripleDES.Core;
using static TripleDES.Core.Permutation;
using static TripleDES.Core.Feistel;
using static TripleDES.Core.KeySchedule;

namespace TripleDES.DES
{
    public static class DESUtil
    {
        public static byte[] Encrypt(byte[] plaintext, byte[] key)
        {
            bool[] bits = InitialPermutation(plaintext);

            bool[] left = bits[..32];
            bool[] right = bits[32..];

            var roundKeys = GenerateRoundKeys(key);
            for (int i = 0; i < 16; i++)
            {
                var fResult = FeistelFunction(right, roundKeys[i]);
                var newRight = new bool[32];
                for (int j = 0; j < 32; j++)
                    newRight[j] = left[j] ^ fResult[j];
                left = right;
                right = newRight;
            }

            bool[] combined = new bool[64];
            Array.Copy(right, 0, combined, 0, 32);
            Array.Copy(left, 0, combined, 32, 32);

            return FinalPermutation(combined);
        }

        public static byte[] Decrypt(byte[] ciphertext, byte[] key)
        {
            bool[] bits = InitialPermutation(ciphertext);

            bool[] left = bits[..32];
            bool[] right = bits[32..];

            var roundKeys = GenerateRoundKeys(key);
            for (int i = 15; i >= 0; i--)
            {
                var fResult = FeistelFunction(right, roundKeys[i]);
                var newRight = new bool[32];
                for (int j = 0; j < 32; j++)
                    newRight[j] = left[j] ^ fResult[j];
                left = right;
                right = newRight;
            }

            bool[] combined = new bool[64];
            Array.Copy(right, 0, combined, 0, 32);
            Array.Copy(left, 0, combined, 32, 32);

            return FinalPermutation(combined);
        }
    }
}