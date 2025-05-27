using System;
using static TripleDES.Core.Tables;
using static TripleDES.Core.Utils;

namespace TripleDES.Core
{
    public static class KeySchedule
    {
        public static readonly int[] LeftShifts = {
            1, 1, 2, 2, 2, 2, 2, 2,
            1, 2, 2, 2, 2, 2, 2, 1
        };

        public static bool[][] GenerateRoundKeys(byte[] key)
        {
            bool[] keyBits = Utils.BytesToBits(key);
            bool[] permutedKey = new bool[56];
            for (int i = 0; i < 56; i++)
                permutedKey[i] = keyBits[PC1[i] - 1];

            bool[] C = new bool[28];
            bool[] D = new bool[28];
            Array.Copy(permutedKey, 0, C, 0, 28);
            Array.Copy(permutedKey, 28, D, 0, 28);

            bool[][] roundKeys = new bool[16][];
            for (int round = 0; round < 16; round++)
            {
                C = LeftShift(C, LeftShifts[round]);
                D = LeftShift(D, LeftShifts[round]);

                bool[] CD = new bool[56];
                Array.Copy(C, 0, CD, 0, 28);
                Array.Copy(D, 0, CD, 28, 28);

                bool[] roundKey = new bool[48];
                for (int i = 0; i < 48; i++)
                    roundKey[i] = CD[PC2[i] - 1];

                roundKeys[round] = roundKey;
            }

            return roundKeys;
        }
    }
}