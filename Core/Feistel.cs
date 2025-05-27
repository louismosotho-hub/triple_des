using System;
using TripleDES.Core;

namespace TripleDES.Core
{
    public static class Feistel
    {
        public static bool[] FeistelFunction(bool[] right, bool[] roundKey)
        {
            bool[] expanded = new bool[48];
            for (int i = 0; i < 48; i++)
                expanded[i] = right[Tables.E[i] - 1];

            bool[] xored = new bool[48];
            for (int i = 0; i < 48; i++)
                xored[i] = expanded[i] ^ roundKey[i];

            bool[] sboxOutput = new bool[32];
            for (int s = 0; s < 8; s++)
            {
                int row = (xored[s * 6] ? 2 : 0) | (xored[s * 6 + 5] ? 1 : 0);
                int col = (xored[s * 6 + 1] ? 8 : 0) | (xored[s * 6 + 2] ? 4 : 0) |
                          (xored[s * 6 + 3] ? 2 : 0) | (xored[s * 6 + 4] ? 1 : 0);
                int val = SBoxes.Boxes[s, row, col];
                for (int bit = 0; bit < 4; bit++)
                    sboxOutput[s * 4 + (3 - bit)] = ((val >> bit) & 1) == 1;
            }

            bool[] permuted = new bool[32];
            for (int i = 0; i < 32; i++)
                permuted[i] = sboxOutput[Tables.P[i] - 1];

            return permuted;
        }
    }
}