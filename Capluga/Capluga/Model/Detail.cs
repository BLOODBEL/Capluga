using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Detail
{
    [Key]
    public int DetailID { get; set; }
    public int MedicalImplementsID { get; set; }
    public decimal PaidPrice { get; set; }
    public int PaidQuantity { get; set; }
    public decimal Tax { get; set; }
    public long MasterPurchaseID { get; set; }

    [ForeignKey("MedicalImplementsID")]
    public virtual MedicalImplement MedicalImplement { get; set; }
    [ForeignKey("MasterPurchaseID")]
    public virtual MasterPurchase MasterPurchase { get; set; }
}
