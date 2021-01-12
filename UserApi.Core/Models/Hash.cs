using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace UserApi.Core.Models
{
   public class Hash
    {
        public String GenerateSha256Hash(string input)
        {
            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(input + "RpRyCqM=");
            SHA256Managed shaString = new SHA256Managed();
            byte[] hash = shaString.ComputeHash(bytes);

            return BitConverter.ToString(hash);
        }
      
    }
}
