using Capluga.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;

public class AppointmentScheduling
{
    [Key]
    public int AppointmentID { get; set; }
    public long UserID { get; set; }
    public int AddressID { get; set; }
    public DateTime AppointmentDate { get; set; }
    public string Description { get; set; }

    [ForeignKey("UserID")]
    public virtual User User { get; set; }

    [ForeignKey("AddressID")]
    public virtual Address Address { get; set; }
}