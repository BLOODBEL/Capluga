using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CaplugaAPI.Controllers
{
    public class ProductoController : ApiController
    {
        [HttpGet]
        [Route("Productos")]
        public List<MedicalImplements> Productos()
        {
            using (var context = new CAPLUGAEntities())
            {
                context.Configuration.LazyLoadingEnabled = false;
                return context.MedicalImplements.ToList();
            }
        }

        [HttpGet]
        [Route("Producto")]
        public MedicalImplements Producto(long q)
        {
            using (var context = new CAPLUGAEntities())
            {
                context.Configuration.LazyLoadingEnabled = false;
                return (from x in context.MedicalImplements
                        where x.MedicalImplementsID == q
                        select x).FirstOrDefault();
            }
        }

        [HttpPost]
        [Route("RegistrarProducto")]
        public long RegistrarProducto(MedicalImplements MedicalImplements)
        {
            using (var context = new CAPLUGAEntities())
            {
                context.MedicalImplements.Add(MedicalImplements);
                context.SaveChanges();
                return MedicalImplements.MedicalImplementsID;
            }
        }

        [HttpPut]
        [Route("ActualizarRutaProducto")]
        public string ActualizarRutaProducto(MedicalImplements MedicalImplements)
        {
            using (var context = new CAPLUGAEntities())
            {
                var datos = context.MedicalImplements.Where(x => x.MedicalImplementsID == MedicalImplements.MedicalImplementsID).FirstOrDefault();
                datos.Image = MedicalImplements.Image;
                context.SaveChanges();
                return "OK";
            }
        }





        [HttpPut]
        [Route("ActualizarProducto")]
        public string ActualizarProducto(MedicalImplements MedicalImplements)
        {
            using (var context = new CAPLUGAEntities())
            {
                var datos = context.MedicalImplements.Where(x => x.MedicalImplementsID == MedicalImplements.MedicalImplementsID).FirstOrDefault();
                datos.Name = MedicalImplements.Name;
                datos.Description = MedicalImplements.Description;
                datos.Quantity = MedicalImplements.Quantity;
                datos.Price = MedicalImplements.Price;
                context.SaveChanges();
                return "OK";
            }
        }



        [HttpPut]
        [Route("ActualizarEstadoProducto")]
        public string ActualizarEstadoUsuario(MedicalImplements MedicalImplements)
        {
            using (var context = new CAPLUGAEntities())
            {
                var datos = context.MedicalImplements.Where(x => x.MedicalImplementsID == MedicalImplements.MedicalImplementsID).FirstOrDefault();
                datos.State = (datos.State ? false : true);
                context.SaveChanges();
                return "OK";
            }
        }
    }
}
