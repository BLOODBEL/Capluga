using Capluga.Entities;
using Capluga.Models;
using DocumentFormat.OpenXml.Wordprocessing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Capluga.Controllers
{
    public class AgendaCitaController : Controller
    {

        AgendaCitaModel modelAgenda = new AgendaCitaModel();
        CitaHorarioModel claseHorario = new CitaHorarioModel();

        [HttpGet]
        public ActionResult RegistrarCita()
        {
            
            ViewBag.Horarios = claseHorario.verHorarios();
            return View(new AgendaEnt());
        }

        [HttpPost]
        public async Task<ActionResult> RegistrarCita(AgendaEnt entidad)
        {
           
            if (Session["UserID"] == null || Session["AddressID"] == null)
            {
                TempData["ErrorMessage"] = "No se ha iniciado sesión o la sesión ha caducado.";
                return RedirectToAction("RegistrarCita"); 
            }

            if (!long.TryParse(Session["UserID"].ToString(), out long userID) ||
                !long.TryParse(Session["AddressID"].ToString(), out long addressID))
            {
                TempData["ErrorMessage"] = "Error al obtener información de la sesión.";
                return RedirectToAction("RegistrarCita");
            }

            entidad.UserID = userID;
            entidad.AddressID = addressID;

            var respuesta = await modelAgenda.RegistrarCita(entidad);

            if (respuesta.Equals("OK", StringComparison.OrdinalIgnoreCase))
            {
                TempData["SuccessMessage"] = "La cita ha sido registrada con éxito.";
                return RedirectToAction("RegistrarCita");
            }
            else
            {
                TempData["ErrorMessage"] = "Error al registrar la cita: " + respuesta;
                return RedirectToAction("RegistrarCita"); 
            }
        }


        [HttpGet]
        public ActionResult ConsultaCitas()
        {
            var datos = modelAgenda.ConsultaCitas();
            return View(datos);
        }


        [HttpGet]
        public ActionResult Actualizarcita(long q)
        {
            var datos = modelAgenda.ConsultaCita(q);
            ViewBag.horarios = claseHorario.verHorarios();
            return View(datos);
        }



        [HttpPost]
        public ActionResult Actualizarcita(AgendaEnt entidad)
        {
            string respuesta = modelAgenda.Actualizarcita(entidad);

            if (respuesta == "OK")
            {
                return RedirectToAction("AgendaCita", "ConsultaCitas");
            }
            else
            {
                ViewBag.MensajeUsuario = "No se ha podido Actualizar la cita";
                ViewBag.horarios = claseHorario.verHorarios();
                return View();
            }
        }

    }
}