using System.Data.Entity;
using Capluga.Models;

namespace Capluga.Models
{
    public class CaplugaDbContext : DbContext
    {
        public CaplugaDbContext() : base("name=DB_CAPLUGAEntities")
        {
        }

        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<MedicalCourse> MedicalCourses { get; set; }
        public DbSet<MedicalImplement> MedicalImplements { get; set; }
        public DbSet<MasterPurchase> MasterPurchases { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<Detail> Details { get; set; }
        public DbSet<ErrorLog> ErrorLogs { get; set; }
        public DbSet<AppointmentScheduling> AppointmentSchedulings { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
