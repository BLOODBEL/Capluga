using Capluga.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class MedicalCourse
{
    [Key]
    public int MedicalCourseID { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Image { get; set; }
    public decimal Price { get; set; }
    public string State { get; set; }
    public long CreatedByUserID { get; set; }

    [ForeignKey("CreatedByUserID")]
    public virtual User CreatedByUser { get; set; }
}
