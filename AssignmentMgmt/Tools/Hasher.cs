using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Cryptography;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// Reference: PeerVal Project 
/// </summary>

namespace AssignmentMgmt.Tools {
    /// <summary>
    /// Tool for keeping Hashing Consistent throughout the application.
    /// </summary>
    /// <remarks>
    /// Used: 
    /// https://tahirnaushad.com/2017/09/09/hashing-in-asp-net-core-2-0/
    /// </remarks>
    public static class Hasher {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="passToHash">password we need hashed</param>
        /// <param name="salt">salt to apply to hash</param>
        /// <param name="pepper">pepper to apply to hash</param>
        /// <param name="stretches">how many times we should iterate the hash. 12000 - 24000</param>
        /// <returns></returns>
        public static string Get(string passToHash, string salt, string pepper, int stretches, int numberOfChars = 64) {
            // https://lockmedown.com/hash-right-implementing-pbkdf2-net/
            // https://docs.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.cryptography.keyderivation.keyderivation.pbkdf2?view=aspnetcore-2.2

            // 64 characters every 3 bits makes a new 
            byte[] myBytes = KeyDerivation.Pbkdf2(passToHash,
                Encoding.UTF8.GetBytes(salt + pepper),
                KeyDerivationPrf.HMACSHA256,
                stretches,
                GetBase64NumberLength(numberOfChars)
                );

            return Convert.ToBase64String(myBytes).Substring(0, numberOfChars);

        }

        /// <summary>
        /// Gets better number for converting numbers from Base64 string conversion.
        /// </summary>
        /// <param name="numberOfChars"></param>
        /// <returns></returns>
        private static int GetBase64NumberLength(int numberOfChars) {
            int newNumb = numberOfChars / 4 * 3;
            newNumb += numberOfChars % 4 > 0 ? 1 : 0;
            return newNumb;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="passToHash">password we need hashed</param>
        /// <param name="salt">salt to apply to hash</param>
        /// <param name="pepper">pepper to apply to hash</param>
        /// <param name="stretches">how many times we should iterate the hash. 12000 - 24000</param>
        /// <param name="hashToCompare">hash to comare against</param>
        /// <returns></returns>
        public static bool IsValid(string passToCheck, string salt, string pepper, int stretches, string hashToCompare) {
            string hash = (Get(passToCheck, salt, pepper, stretches, hashToCompare.Length));

            
            
            if (hash == hashToCompare)
                return true;
            else
                return false;
            //return Get(passToCheck, salt, pepper, stretches, hashToCompare.Length) == hashToCompare;
        }
        /// <summary>
        /// Generate a new salt
        /// </summary>
        /// <returns></returns>
        public static string GenerateSalt(int numberOfChars = 50) {

            byte[] newSalter = new byte[numberOfChars]; //new byte[GetBase64NumberLength(numberOfChars)];
            RandomNumberGenerator rGen = RandomNumberGenerator.Create();
            rGen.GetBytes(newSalter);
            return Convert.ToBase64String(newSalter).Substring(0,numberOfChars);
            //return Convert.(newSalter);
        }
    }
}
