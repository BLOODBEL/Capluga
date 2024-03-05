using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CaplugaAPI.Entities
{
    public class CarritoEnt
    {

        public long CartID { get; set; }

        public long UserID { get; set; }

        public long MasterPurchaseID { get; set; }

        public long MedicalImplementsID { get; set; }

        public decimal Quantity { get; set; }



    }
}