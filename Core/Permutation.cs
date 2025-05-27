using System;
using static TripleDES.Core.Utils;
using static TripleDES.Core.Tables;

namespace TripleDES.Core
{
    public static class Permutation
    {
        public static bool[] InitialPermutation(byte[] input)
        {
            bool[] bits = BytesToBits(input);
            bool[] permuted = new bool[64];
            for (int i = 0; i < 64; i++)
            {
                permuted[i] = bits[IP[i] - 1];
            }
            return permuted;
        }

        public static byte[] FinalPermutation(bool[] input)
        {
            bool[] permuted = new bool[64];
            for (int i = 0; i < 64; i++)
            {
                permuted[i] = input[FP[i] - 1];
            }
            return BitsToBytes(permuted);
        }
    }
}