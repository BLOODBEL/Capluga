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
    
    public partial class ScheduleAppointment
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ScheduleAppointment()
        {
            this.AppointmentScheduling = new HashSet<AppointmentScheduling>();
        }
    
        public long ScheduleID { get; set; }
        public string Dname { get; set; }
        public System.DateTime DateandTime { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AppointmentScheduling> AppointmentScheduling { get; set; }
    }
}