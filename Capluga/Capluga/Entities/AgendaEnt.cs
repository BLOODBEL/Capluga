using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Capluga.Entities
{
    public class AgendaEnt
    {

        public long AppointmentID { get; set; }

        public long UserID { get; set; }

        public long AddressID { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }
        public long ScheduleID { get; set; }
    }
}
