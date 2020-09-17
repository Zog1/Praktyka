using System;

namespace CarRental.DAL.Entities
{
    public class BaseEntity
    {
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
        public string ModifiedBy { get; set; }
        
        public BaseEntity()
        {

        } 
    }
}
