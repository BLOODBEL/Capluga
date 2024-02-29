using Capluga.Entities;
using Capluga.Models;
using System.Linq;
using System.Web.Mvc;

namespace Capluga.Controllers
{
    public class LoginController : Controller
    {

        UsuarioModel usuarioModel = new UsuarioModel();
        RolesModel relesModel = new RolesModel();

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult CerrarSesion()
        {
            Session.Clear();
            return RedirectToAction("Index", "Login");
        }


        [HttpGet]
        public ActionResult IniciarSesion()
        {
            return View();
        }

        [HttpGet]
        public ActionResult RegistrarCuenta()
        {
            //ViewBag.ListaRoles = usuarioModel.ListaRoles();
            return View();
        }

        [HttpPost]
        public ActionResult RegistrarCuenta(UsuarioEnt entidad)
        {            
            string respuesta = usuarioModel.RegistrarCuenta(entidad);

            if (respuesta == "OK")
            {
                return RedirectToAction("IniciarSesion", "Login");
            }
            else
            {
                ViewBag.MensajeUsuario = "No se ha podido registrar su información";
                return View();
            }
        }

        [HttpPost]
        public ActionResult IniciarSesion(UsuarioEnt entidad)
        {
            var respuesta = usuarioModel.IniciarSesion(entidad);

            if (respuesta != null)
            {
                Session["Nombre"] = respuesta.Name;
                Session["Rol"] = respuesta.RoleID.RolName;

                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.MensajeUsuario = "No se ha podido validar su información";
                return View();
            }
        }

        [HttpGet]
        public ActionResult RecuperarContrasenna()
        {
            return View();
        }

        [HttpPost]
        public ActionResult RecuperarCuenta(UsuarioEnt entidad)
        {
            string respuesta = usuarioModel.RecuperarCuenta(entidad);

            if (respuesta == "OK")
            {
                return RedirectToAction("IniciarSesion", "Login");
            }
            else
            {
                ViewBag.MensajeUsuario = "No se ha podido recuperar su información";
                return View();
            }
        }

    }
}
