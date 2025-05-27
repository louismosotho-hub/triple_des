using System;

namespace TripleDES.Core
{
    public static class Utils
    {
        public static bool[] BytesToBits(byte[] bytes)
        {
            bool[] bits = new bool[bytes.Length * 8];
            for (int i = 0; i < bytes.Length; i++)
                for (int bit = 0; bit < 8; bit++)
                    bits[i * 8 + bit] = (bytes[i] & (1 << (7 - bit))) != 0;
            return bits;
        }

        public static byte[] BitsToBytes(bool[] bits)
        {
            int byteCount = bits.Length / 8;
            byte[] bytes = new byte[byteCount];
            for (int i = 0; i < byteCount; i++)
            {
                byte b = 0;
                for (int bit = 0; bit < 8; bit++)
                    if (bits[i * 8 + bit])
                        b |= (byte)(1 << (7 - bit));
                bytes[i] = b;
            }
            return bytes;
        }

        public static bool[] LeftShift(bool[] bits, int count)
        {
            bool[] shifted = new bool[bits.Length];
            for (int i = 0; i < bits.Length; i++)
                shifted[i] = bits[(i + count) % bits.Length];
            return shifted;
        }

        public static byte[] Pad(byte[] data, int blockSize = 8)
        {
            int padLen = blockSize - (data.Length % blockSize);
            if (padLen == 0) padLen = blockSize;
            byte[] padded = new byte[data.Length + padLen];
            Array.Copy(data, padded, data.Length);
            for (int i = data.Length; i < padded.Length; i++)
                padded[i] = (byte)padLen;
            return padded;
        }

        public static byte[] Unpad(byte[] paddedData)
        {
            if (paddedData.Length == 0)
                throw new ArgumentException("Padded data cannot be empty.");
            byte padLen = paddedData[paddedData.Length - 1];
            if (padLen < 1 || padLen > 8)
                throw new ArgumentException("Invalid padding.");
            for (int i = paddedData.Length - padLen; i < paddedData.Length; i++)
                if (paddedData[i] != padLen)
                    throw new ArgumentException("Invalid padding.");
            byte[] data = new byte[paddedData.Length - padLen];
            Array.Copy(paddedData, data, data.Length);
            return data;
        }
    }
}