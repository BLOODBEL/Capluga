using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Cart
{
    [Key]
    public int CartID { get; set; }
    public long MasterPurchaseID { get; set; }
    public int MedicalImplementsID { get; set; }
    public int Quantity { get; set; }

    [ForeignKey("MasterPurchaseID")]
    public virtual MasterPurchase MasterPurchase { get; set; }
    [ForeignKey("MedicalImplementsID")]
    public virtual MedicalImplement MedicalImplement { get; set; }
}
