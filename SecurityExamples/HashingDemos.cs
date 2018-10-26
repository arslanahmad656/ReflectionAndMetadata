using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Security;
using System.Security.Cryptography;
using static System.Console;

namespace SecurityExamples
{
    static class HashingDemos
    {
        public static void Run()
        {
            //ComputeTextHash();
            ComputeFileHash();
        }

        #region Demo

        static void ComputeTextHash()
        {
            string sample = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua";
            string sampleCorrupted = " Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua";   // has a whitespace at the start
            string sampleCopy = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua";

            WriteLine("Input texts:");
            WriteLine($"Original: {sample}");
            WriteLine($"Corrupted: {sampleCorrupted}");
            WriteLine($"Copy: {sampleCopy}");

            var sampleHash = ComputeHash(sample, HashingAlgos.MD5);
            var sampleCorruptedHash = ComputeHash(sampleCorrupted, HashingAlgos.MD5);
            var sampleCopyHash = ComputeHash(sampleCopy, HashingAlgos.MD5);
            WriteLine($"Hash of original: {BytesArrayToString(sampleHash)}");
            WriteLine($"Hash of corrupted: {BytesArrayToString(sampleCorruptedHash)}");
            WriteLine($"Hash of copied: {BytesArrayToString(sampleCopyHash)}");
            WriteLine($"Hash of original and corrupted are equal: {IsEqual(sampleHash, sampleCorruptedHash)}");
            WriteLine($"Hash of original and copy are equal: {IsEqual(sampleHash, sampleCopyHash)}");
        }

        static void ComputeFileHash()
        {
            Stream original = File.OpenRead(@"Sample Files\original.exe");
            Stream corrupted = File.OpenRead(@"Sample Files\corrupted.exe");
            Stream copied = File.OpenRead(@"Sample Files\copy.exe");

            var originalHash = ComputeHash(original, HashingAlgos.MD5);
            var corruptedHash = ComputeHash(corrupted, HashingAlgos.MD5);
            var copyHash = ComputeHash(copied, HashingAlgos.MD5);
            var alreadyComputedOriginalHash = ReadHashFromFile(@"Sample Files\original_md5");

            WriteLine($"Hash of original: {BytesArrayToString(originalHash)}");
            WriteLine($"Hash of corrupted: {BytesArrayToString(corruptedHash)}");
            WriteLine($"Hash of copied: {BytesArrayToString(copyHash)}");
            WriteLine($"Already computed hash: {BytesArrayToString(alreadyComputedOriginalHash)}");
            WriteLine($"Hash of original and corrupted are equal: {IsEqual(originalHash, corruptedHash)}");
            WriteLine($"Hash of original and copy are equal: {IsEqual(originalHash, copyHash)}");
            WriteLine($"Hash of original and already computed are equal: {IsEqual(originalHash, alreadyComputedOriginalHash)}");
        }

        static void WriteHashes()
        {
            var s = File.OpenRead(@"Sample Files\original.exe");
            var md5 = ComputeHash(s, HashingAlgos.MD5);
            var sha256 = ComputeHash(s, HashingAlgos.SHA256);
            var sha512 = ComputeHash(s, HashingAlgos.SHA512);
            WriteHashToFile(md5, "original_md5");
            WriteHashToFile(sha256, "original_sha256");
            WriteHashToFile(sha512, "original_sha512");
        }
        #endregion


        #region Utility

        /// <summary>
        /// Computes the hash from the given byte array
        /// </summary>
        /// <param name="bytes">Array whose hash is to be computed</param>
        /// <param name="algo">Hashing algo to use</param>
        /// <returns></returns>
        static byte[] ComputeHash(byte[] bytes, HashingAlgos algo = HashingAlgos.MD5)
        {
            switch (algo)
            {
                case HashingAlgos.MD5:
                    return MD5.Create().ComputeHash(bytes);
                case HashingAlgos.SHA256:
                    return SHA256.Create().ComputeHash(bytes);
                case HashingAlgos.SHA512:
                    return SHA512.Create().ComputeHash(bytes);
                default:
                    throw null;
            }
        }

        /// <summary>
        /// Computes the hash from the given stream
        /// </summary>
        /// <param name="stream">Stream from which the hash is to be computed</param>
        /// <param name="algo">Hashing algo to use</param>
        /// <returns>The computed hash</returns>
        static byte[] ComputeHash(Stream stream, HashingAlgos algo = HashingAlgos.MD5)
        {
            switch (algo)
            {
                case HashingAlgos.MD5:
                    return MD5.Create().ComputeHash(stream);
                case HashingAlgos.SHA256:
                    return SHA256.Create().ComputeHash(stream);
                case HashingAlgos.SHA512:
                    return SHA512.Create().ComputeHash(stream);
                default:
                    throw null;
            }
        }

        /// <summary>
        /// Computes the hash of the given text
        /// </summary>
        /// <param name="text">Text whose hash is to be computed</param>
        /// <param name="algo">Hashing algo to use</param>
        /// <returns>The computed hash</returns>
        static byte[] ComputeHash(string text, HashingAlgos algo = HashingAlgos.MD5)
        {
            var bytes = Encoding.UTF8.GetBytes(text);
            return ComputeHash(bytes, algo);
        }

        /// <summary>
        /// Writes the bytes in the hash to the file created by the path specified
        /// </summary>
        /// <param name="hash">Bytes to write</param>
        /// <param name="file">Path of the file to create</param>
        static void WriteHashToFile(byte[] hash, string file)
        {
            var text = Encoding.UTF8.GetString(hash);
            using(var s = File.Create(file))
            {
                s.Write(hash, 0, hash.Length);
            }
        }

        /// <summary>
        /// Reads the hash from the file given
        /// </summary>
        /// <param name="file">File from where the bytes are to be read</param>
        /// <returns>Bytes from the file</returns>
        static byte[] ReadHashFromFile(string file)
        {
            using(var s = File.OpenRead(file))
            {
                var bytes = new byte[s.Length];
                s.Read(bytes, 0, bytes.Length);
                return bytes;
            }
        }

        /// <summary>
        /// Compares the hashes given as byte arrays using the specified hashing algo
        /// </summary>
        /// <param name="hash1">Hash value (one of the two to compare)</param>
        /// <param name="hash2">Hash value (one of the two to compare)</param>
        /// <returns>True if the hashes are equal</returns>
        static bool CompareHash(byte[] hash1, byte[] hash2)
        {
            return IsEqual(hash1, hash2);
        }

        /// <summary>
        /// Compares the given hash and the hash computed from the given stream using the specified hashing algo
        /// </summary>
        /// <param name="hash">Hash against which the stream's computed hash is to be compared</param>
        /// <param name="stream">Stream whose computed hash is to be compared</param>
        /// <param name="algo">Hashing algo to use</param>
        /// <returns>True if the hashes are equal</returns>
        static bool CompareHash(byte[] hash, Stream stream, HashingAlgos algo = HashingAlgos.MD5)
        {
            var computedHash = ComputeHash(stream, algo);
            return IsEqual(hash, computedHash);
        }

        /// <summary>
        /// Compares the hash of the given streams using the specified hashing algo
        /// </summary>
        /// <param name="stream1">First stream</param>
        /// <param name="stream2">Second stream</param>
        /// <param name="algo">Hashing algo to use</param>
        /// <returns>True if the hashes are equal</returns>
        static bool CompareHash(Stream stream1, Stream stream2, HashingAlgos algo = HashingAlgos.MD5)
        {
            var hash1 = ComputeHash(stream1, algo);
            var hash2 = ComputeHash(stream2, algo);
            return IsEqual(hash1, hash2);
        }

        static bool IsEqual(byte[] arr1, byte[] arr2)
        {
            if(arr1 == null || arr2 == null || arr1.Length != arr2.Length)
            {
                return false;
            }

            for (int i = 0; i < arr1.Length; i++)
            {
                if(arr1[i] != arr2[i])
                {
                    return false;
                }
            }

            return true;
        }

        static string BytesArrayToString(byte[] arr)
        {
            StringBuilder sb = new StringBuilder();
            arr.All(b =>
            {
                sb.Append($"{(int)b} ");
                return true;
            });
            return sb.ToString();
        }

        enum HashingAlgos
        {
            MD5, SHA256, SHA512
        }

        #endregion
    }
}
