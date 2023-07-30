using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SCCFantasy.Common.Security
{
    public static class HashDataHelper
    {
        public static string HashData(string inputText)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                // Convert the input string to a byte array
                byte[] inputBytes = Encoding.UTF8.GetBytes(inputText);

                // Compute the hash value of the input data
                byte[] hashBytes = sha256.ComputeHash(inputBytes);

                // Convert the hashed byte array to a hexadecimal string
                string hashedData = Convert.ToHexString(hashBytes);

                return hashedData;
            }
        }
    }
}
