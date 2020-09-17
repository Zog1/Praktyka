using System;
using System.Collections.Generic;
using System.Security.Cryptography;

namespace CarRental.DAL.Entities
{
    public class User : BaseEntity
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string IdentificationNumber { get; set; }
        public string Email { get; set; }
        public string MobileNumber { get; set; }
        public string HashPassword { get; set; }
        public string Salt { get; set; }
        public string StatusOfVerification { get; set; }
        public RoleOfWorker RoleOfUser { get; set; }
        public string CodeOfVerification { get; set; }
        public bool IsDeleted { get; set; }
        public List<RefreshToken> RefreshTokens { get; set; }

            public User(string firstName, string lastName, string identificationNumber, string email,
                string mobileNumber)
        {
            FirstName = firstName;
            LastName = lastName;
            IdentificationNumber = identificationNumber;
            Email = email;
            MobileNumber = mobileNumber;
            DateCreated = DateTime.Now;
            RoleOfUser = RoleOfWorker.Worker;
            StatusOfVerification = "Processing...";
            CodeOfVerification = GetRandomString(32);
        }

        public User()
        {
        }
        public void Update(string firstName, string lastName, string identificationNumber, string email,
            string mobileNumber)
        {
            FirstName = firstName;
            LastName = lastName;
            IdentificationNumber = identificationNumber;
            Email = email;
            MobileNumber = mobileNumber;
            DateModified = DateTime.Now;
        }
        public void Delete(bool isdelete)
        {
            IsDeleted = isdelete;
        }
        public void SetPassword(string encodePassword, string salt)
        {
            Salt = salt;
            HashPassword = encodePassword;
            CodeOfVerification = null;
            StatusOfVerification = "Account has been registered.";
        }

        const string valid = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
        static string GetRandomString(int length)
        {
            string code = "";
            using (RNGCryptoServiceProvider provider = new RNGCryptoServiceProvider())
            {
                while (code.Length != length)
                {
                    byte[] oneByte = new byte[1];
                    provider.GetBytes(oneByte);
                    char character = (char)oneByte[0];
                    if (valid.Contains(character))
                    {
                        code += character;
                    }
                }
            }
            return code;
        }
    }
}
