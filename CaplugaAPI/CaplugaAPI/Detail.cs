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
    
    public partial class Detail
    {
        public long DetailID { get; set; }
        public long MedicalImplementsID { get; set; }
        public decimal PaidPrice { get; set; }
        public int PaidQuantity { get; set; }
        public decimal Tax { get; set; }
        public long MasterPurchaseID { get; set; }
    
        public virtual MasterPurchase MasterPurchase { get; set; }
        public virtual MedicalImplements MedicalImplements { get; set; }
    }
}
