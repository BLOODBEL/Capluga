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
    
    public partial class AppointmentScheduling
    {
        public long AppointmentID { get; set; }
        public long UserID { get; set; }
        public long AddressID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public long ScheduleID { get; set; }
    
        public virtual Addresses Addresses { get; set; }
        public virtual Users Users { get; set; }
        public virtual ScheduleAppointment ScheduleAppointment { get; set; }
    }
}
