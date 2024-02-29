using CaplugaAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Security.Cryptography;
using System.Web.Http;

namespace CaplugaAPI.Controllers
{
    public class UsuarioController : ApiController
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
                    user.RegistrationDate = entidad.RegistrationDate;
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
        public string IniciarSesion(UsuarioEnt entidad)
        {
            try
            {
                using (var context = new CAPLUGAEntities())
                {
                    var usuario = context.Users.FirstOrDefault(
                        u => u.Email == entidad.Email && u.Password == entidad.Password);
                    if (usuario != null)
                        return "OK";
                    else
                        return string.Empty;
                }
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }
        [HttpGet]
        [Route("RecuperarContrasenna")]

        public string RecuperarContrasenna(string email)
        {
            try
            {
                using (var context = new CAPLUGAEntities())
                {
                    var usuario = context.Users.FirstOrDefault(
                        u => u.Email == email);
                    if (usuario != null)
                    {
                        usuario.Password = GenerarContrasenna();

                        string rutaArchivo = AppDomain.CurrentDomain.BaseDirectory + "Templates\\Contrasenna.html";
                        string html = File.ReadAllText(rutaArchivo);

                        html = html.Replace("@@Nombre", usuario.Email);
                        html = html.Replace("@@Contrasenna", usuario.Password);

                        util.EnviarCorreo(email, "Contraseña de Acceso", html);

                        return "OK";
                    }
                    else { return string.Empty; }
                }
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }

        [HttpGet]
        [Route("ListaRoles")]
        public List<RoleEnt> ListaRoles()
        {

            using (var context = new CAPLUGAEntities())
            {
                var datos = (from x in context.Roles
                             select x).ToList();

                List<RoleEnt> listaEntidadResultado = new List<RoleEnt>();
                foreach (var item in datos)
                {
                    listaEntidadResultado.Add(new RoleEnt
                    {
                        RoleID = item.RolesID,
                        RolName = item.RoleName
                    });
                }

                return listaEntidadResultado;
            }
        }

        public static string GenerarContrasenna()
        {
            int tamanno = 8;
            const string letras = "ABCDEFGHJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!@#$%^&*";
            var randomBytes = new byte[tamanno];
            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(randomBytes);
            }

            var chars = new char[tamanno];
            int validarChar = letras.Length;
            for (int i = 0; i < tamanno; i++)
            {
                chars[i] = letras[randomBytes[i] % validarChar];
            }

            return new string(chars);
        }

    }
}
