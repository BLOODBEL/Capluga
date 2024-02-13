using System.ComponentModel.DataAnnotations;

public class MedicalImplement
{
    [Key]
    public int MedicalImplementsID { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public bool State { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }
    public string Image { get; set; }
}
