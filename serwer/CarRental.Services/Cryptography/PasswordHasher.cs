using CarRental.DAL.Entities;
using System;
using System.Security.Cryptography;

namespace CarRental.Services.Cryptography
{
    public class PasswordHasher
    {
        public HashSalt GenerateSaltedHash(int size, string password)
        {
            var saltBytes = new byte[size];
            var provider = new RNGCryptoServiceProvider();
            provider.GetNonZeroBytes(saltBytes);
            var salt = Convert.ToBase64String(saltBytes);

            var rfc2898DeriveBytes = new Rfc2898DeriveBytes(password, saltBytes, 10000);
            var hashPassword = Convert.ToBase64String(rfc2898DeriveBytes.GetBytes(256));

            return new HashSalt { Hash = hashPassword, Salt = salt };
        }
    }
}
