using CaplugaAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CaplugaAPI.Controllers
{
    public class RegisteredcoursesController : ApiController
    {

        [HttpPost]
        [Route("RegistrarInscripcion")]
        public string RegistrarInscripcion(Registeredcourses registeredcourses)
        {
            
            using (var context = new CAPLUGAEntities())
            {
                var datos = (from x in context.Registeredcourses
                             where x.UserID == registeredcourses.UserID
                                && x.MedicalCourseID == registeredcourses.MedicalCourseID
                             select x).FirstOrDefault();

                if (datos == null)
                {
                    Registeredcourses registro = new Registeredcourses();
                    registro.UserID = registeredcourses.UserID;
                    registro.MedicalCourseID = registeredcourses.MedicalCourseID;
                    registro.Quantity = registeredcourses.Quantity;
                    registro.Registrationdate = registeredcourses.Registrationdate;
                    context.Registeredcourses.Add(registro);
                    context.SaveChanges();
                }
                else
                {
                    datos.Quantity = registeredcourses.Quantity;
                    context.SaveChanges();
                }
                return "OK";
            }
        }

        [HttpGet]
        [Route("ConsultarInscripcion")]
        public object ConsultarInscripcion(long q)
        {
            using (var context = new CAPLUGAEntities())
            {
                context.Configuration.LazyLoadingEnabled = false;
                return (from x in context.Registeredcourses
                        join y in context.MedicalCourses on x.MedicalCourseID equals y.MedicalCourseID
                        where x.UserID == q
                        select new
                        {
                            x.RegisteredcoursesID,
                            x.UserID,
                            x.MedicalCourseID,
                            x.Quantity,
                            x.Registrationdate,
                            y.Name,
                            y.Price,
                            SubTotal = (y.Price * x.Quantity),
                            Impuesto = (y.Price * x.Quantity) * 0.13M,
                            Total = (y.Price * x.Quantity) + (y.Price * x.Quantity) * 0.13M
                        }).ToList();
            }
        }

        //[HttpGet]
        //[Route("ConsultarInscripciones")]
        //public List<Registeredcourses> ConsultarInscripciones(long q)
        //{
        //    using (var context = new CAPLUGAEntities())
        //    {
        //        context.Configuration.LazyLoadingEnabled = false;
        //        return (from x in context.Registeredcourses
        //                where x.UserID == q
        //                select x).ToList();
        //    }
        //}

        [HttpDelete]
        [Route("EliminarRegistroCurso")]
        public void EliminarRegistroCurso(long q)
        {
            using (var context = new CAPLUGAEntities())
            {
                var datos = (from x in context.Registeredcourses
                             where x.RegisteredcoursesID == q
                             select x).FirstOrDefault();

                context.Registeredcourses.Remove(datos);
                context.SaveChanges();
            }
        }
    }
}
