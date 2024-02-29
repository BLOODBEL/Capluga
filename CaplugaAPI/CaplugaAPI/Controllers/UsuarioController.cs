using CaplugaAPI.Entities;
using System;
using System.Linq;
using System.Web.Http;
using System.IO;
using System.Collections.Generic;

namespace CaplugaAPI.Controllers
{
    public class LoginController : ApiController
    {
        Utilitarios util = new Utilitarios();

        [HttpPost]
        [Route("RegistrarCuenta")]
        public string RegistrarCuenta(UsuarioEnt entidad)
        {
            try
            {
                using (var context = new CAPLUGAEntities())
                {
                    Users user = new Users();
                    user.Name = entidad.Name;
                    user.Surnames = entidad.Surnames;
                    user.Email = entidad.Email;
                    user.Password = entidad.Password;
                    user.RegistrationDate = DateTime.Now;
                    user.State = true;
                    user.Age = entidad.Age;
                    user.PhoneNumber = entidad.PhoneNumber;
                    user.RolesID = 2;

                    context.Users.Add(user);
                    context.SaveChanges();
                    return "OK";
                }
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }

        [HttpPost]
        [Route("IniciarSesion")]
        public bool IniciarSesion(UsuarioEnt entidad)
        {
            try
            {
                using (var context = new CAPLUGAEntities())
                {
                    var usuario = context.Users.FirstOrDefault(
                        u => u.Name == entidad.Name && u.Password == entidad.Password);
                    return usuario != null;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        //[HttpGet]
        //[Route("RecuperarCuenta")]
        //public string RecuperarCuenta(string Identificacion)
        //{
        //    try
        //    {
        //        using (var context = new CAPLUGAEntities())
        //        {
        //            var datos = context.RecuperarCuentaSP(Identificacion).FirstOrDefault();

        //            if (datos != null)
        //            {
        //                string rutaArchivo = AppDomain.CurrentDomain.BaseDirectory + "Templates\\Contrasenna.html";
        //                string html = File.ReadAllText(rutaArchivo);

        //                html = html.Replace("@@Nombre", datos.Nombre);
        //                html = html.Replace("@@Contrasenna", datos.Contrasenna);

        //                util.EnviarCorreo(datos.Correo, "Contraseña de Acceso", html);
        //                return "OK";
        //            }
        //            else
        //            {
        //                return string.Empty;
        //            }
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        return string.Empty;
        //    }
        //}

    }
}
