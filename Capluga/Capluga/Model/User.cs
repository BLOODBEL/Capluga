using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Capluga.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Surnames { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime RegistrationDate { get; set; }
        public bool State { get; set; }
        public double? Weight { get; set; }
        public DateTime? Age { get; set; }
        public string PhoneNumber { get; set; }

        [ForeignKey("RoleId")]
        public virtual Role Role { get; set; }
    }
}
