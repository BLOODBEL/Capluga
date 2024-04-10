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
        public string RegistrarCita(AppointmentScheduling appointmentScheduling)
        {
            using (var context = new CAPLUGAEntities())
            {
                var datos = (from x in context.AppointmentScheduling
                             where x.UserID == appointmentScheduling.UserID
                                && x.AddressID == appointmentScheduling.AddressID
                                && x.ScheduleID == appointmentScheduling.ScheduleID
                             select x).FirstOrDefault();
                if (datos == null)
                {
                    AppointmentScheduling appointment = new AppointmentScheduling();
                    appointment.UserID = appointmentScheduling.UserID;
                    appointment.AddressID = appointmentScheduling.AddressID;
                    appointment.ScheduleID = appointmentScheduling.ScheduleID;
                    appointment.Name = appointmentScheduling.Name;
                    appointment.Description = appointmentScheduling.Description;
                    context.AppointmentScheduling.Add(appointment);
                    context.SaveChanges();
                }
               
                return "OK";
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
