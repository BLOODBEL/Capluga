//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CaplugaAPI
{
    using System;
    using System.Collections.Generic;
    
    public partial class MedicalCourses
    {
        public long MedicalCourseID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public decimal Price { get; set; }
        public string State { get; set; }
        public long CreatedByUserID { get; set; }
    
        public virtual Users Users { get; set; }
    }
}