using System;

namespace CarRental.DAL.Entities
{
    public class RefreshToken
    {
        public int Id { get; set; }
        public string Refresh { get; set; }
        public int UserId { get; set; }
        public DateTime DateOfStart { get; set; }
        public DateTime DateOfEnd { get; set; }
        public User User { get; set; }
        public bool IsValid { get; set; }

        public RefreshToken() { }

         public RefreshToken( string refresh,int userId,bool isvalid)
          {
            Refresh = refresh;
            UserId = userId;
            DateOfStart = DateTime.Now;
            DateOfEnd = DateTime.Now.AddDays(100);
            IsValid = isvalid;
          }
        public void Delete(bool isvalid)
        {
            IsValid = isvalid;
        }
    }
}
