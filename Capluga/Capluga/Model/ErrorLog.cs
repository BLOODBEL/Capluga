using Capluga.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class ErrorLog
{
    [Key]
    public int LogID { get; set; }
    public DateTime Timestamp { get; set; }
    public string ErrorMessage { get; set; }
    public string Source { get; set; }
    public string AdditionalInformation { get; set; }
    public long UserID { get; set; }

    [ForeignKey("UserID")]
    public virtual User User { get; set; }
}
