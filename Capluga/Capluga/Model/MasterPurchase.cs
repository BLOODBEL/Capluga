using Capluga.Models;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using System.Collections.Generic;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class MasterPurchase
{
    [Key]
    public long MasterPurchaseID { get; set; }
    public long UserID { get; set; }
    public DateTime PurchaseDate { get; set; }
    public decimal TotalPurchase { get; set; }

    [ForeignKey("UserID")]
    public virtual User User { get; set; }
    // Relación con Cart
    public virtual ICollection<Cart> Carts { get; set; }
    // Relación con Detail
    public virtual ICollection<Detail> Details { get; set; }
}
