using CaplugaAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CaplugaAPI.Controllers
{
    public class AgendaCitaController : ApiController
    {
        [HttpPost]
        [Route("RegistrarCita")]
        public string RegistrarCita(AgendaEnt entidad)
        {
            try
            {
                using (var context = new CAPLUGAEntities())
                {
                  
                    context.InsertAppointment(entidad.UserID,entidad.AddressID, entidad.Name, entidad.Description, entidad.ScheduleID);
                    
                    return "Cita registrada con éxito.";
                }
            }
            catch (Exception)
            {

                return string.Empty;
            }
        }

        [HttpGet]
        [Route("ConsultaCitas")]
        public List<AppointmentScheduling> ConsultaCitas()
        {
            try
            {
                using (var context = new CAPLUGAEntities())
                {
                    context.Configuration.LazyLoadingEnabled = false;
                    return (from x in context.AppointmentScheduling
                            select x).ToList();
                }
            }
            catch (Exception)
            {
                return new List<AppointmentScheduling>();
            }
        }


        [HttpGet]
        [Route("ConsultaCita")]
        public AppointmentScheduling ConsultaCita(long q)
        {
            try
            {
                using (var context = new CAPLUGAEntities())
                {
                    context.Configuration.LazyLoadingEnabled = false;
                    return (from x in context.AppointmentScheduling
                            where x.AppointmentID == q
                            select x).FirstOrDefault();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        [HttpPut]
        [Route("Actualizarcita")]
        public string Actualizarcita(AgendaEnt entidad)
        {
            try
            {
                using (var context = new CAPLUGAEntities())
                {
                    context.UpdateAppointment(entidad.AppointmentID, entidad.UserID, entidad.AddressID, entidad.Name, entidad.Description, entidad.ScheduleID);
                    return "OK";
                }
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }
    }
}
