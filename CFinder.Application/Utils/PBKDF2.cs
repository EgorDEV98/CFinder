using System.Security.Cryptography;
using System.Text;

namespace CFinder.Application.Utils;

public static class PBKDF2
{
    public static byte[] DeriveKey(string? password, byte[] salt)
    {
        if (password == null)
        {
            return Array.Empty<byte>();
        }
        var passwordBytes = Encoding.UTF8.GetBytes(password);
        var prf = new HMACSHA256();

        prf.Key = passwordBytes;
        int num1 = prf.HashSize / 8;
        int num2 = 32;
        int length = (int) Math.Ceiling(num2 / (double) num1);
        int num3 = (length - 1) * num1;
        int num4 = num2 - num3;
        byte[][] numArray = new byte[length][];
    
        for (int index = 0; index < length; ++index)
            numArray[index] = PBKDF2.F(salt, 10000, index + 1, prf);
        numArray[length - 1] = Arrays.LeftmostBits(numArray[length - 1], num4 * 8);
        return Arrays.Concat(numArray);
    }

    private static byte[] F(byte[] salt, int iterationCount, int blockIndex, HMAC prf)
    {
        byte[] hash = prf.ComputeHash(Arrays.Concat(salt, Arrays.IntToBytes(blockIndex)));
        byte[] left = hash;
        for (int index = 2; index <= iterationCount; ++index)
        {
            hash = prf.ComputeHash(hash);
            left = Arrays.Xor(left, hash);
        }
        return left;
    }
    private static class Arrays
    {
        public static byte[] LeftmostBits(byte[] data, int lengthBits)
        {
            int count = lengthBits / 8;
            byte[] dst = new byte[count];
            Buffer.BlockCopy(data, 0, dst, 0, count);
            return dst;
        }
        public static byte[] Concat(params byte[][] arrays)
        {
            byte[] dst = new byte[arrays.Sum((Func<byte[], int>) (a => a.Length))];
            int dstOffset = 0;
            foreach (byte[] array in arrays)
            {
                Buffer.BlockCopy(array, 0, dst, dstOffset, array.Length);
                dstOffset += array.Length;
            }
            return dst;
        }
        public static byte[] Xor(byte[] left, byte[] right)
        {
            byte[] numArray = new byte[left.Length];
            for (int index = 0; index < left.Length; ++index)
                numArray[index] = (byte) (left[index] ^ (uint) right[index]);
            return numArray;
        }
    
        public static byte[] IntToBytes(int value)
        {
            uint num = (uint) value;
            return !BitConverter.IsLittleEndian ? new[]
            {
                (byte) (num & byte.MaxValue),
                (byte) (num >> 8 & byte.MaxValue),
                (byte) (num >> 16 & byte.MaxValue),
                (byte) (num >> 24 & byte.MaxValue)
            } : new[]
            {
                (byte) (num >> 24 & byte.MaxValue),
                (byte) (num >> 16 & byte.MaxValue),
                (byte) (num >> 8 & byte.MaxValue),
                (byte) (num & byte.MaxValue)
            };
        }
    }
}