using Capluga.Models;
using System.Web.Mvc; // Asegúrate de tener el namespace correcto para MVC
using System.Linq;
using System;

namespace Capluga.Controllers
{
    public class UsersController : Controller
    {
        // Contexto de base de datos (asumiendo que tienes uno configurado)
        private readonly CaplugaDbContext _context;

        public UsersController(CaplugaDbContext context)
        {
            _context = context;
        }

        // Agregar usuario (REQ4 y REQ5)
        [HttpPost]
        public ActionResult AddUser(User user)
        {
            if (ModelState.IsValid)
            {
                _context.Users.Add(user);
                _context.SaveChanges();
                return RedirectToAction("Index"); // Redirige a la vista principal de usuarios
            }
            return View(user); // Vuelve a mostrar el formulario si no es válido
        }

        // Editar usuario (REQ4 y REQ5)
        [HttpPost]
        public ActionResult EditUser(User user)
        {
            if (ModelState.IsValid)
            {
                _context.Entry(user).State = System.Data.Entity.EntityState.Modified;
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(user);
        }

        // Activar/Inactivar usuario (REQ5)
        [HttpPost]
        public ActionResult ToggleUserState(long userId)
        {
            var user = _context.Users.Find(userId);
            if (user != null)
            {
                user.State = !user.State;
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return HttpNotFound();
        }

        // Recuperar contraseña (REQ2)
        [HttpPost]
        public ActionResult RecoverPassword(string email)
        {
            // Implementar lógica de recuperación aquí
            return View();
        }

        // Inicio de sesión seguro (REQ3)
        [HttpPost]
        public ActionResult SecureLogin(string email, string password)
        {
            // Implementar lógica de inicio de sesión aquí
            return View();
        }

        // Listar usuarios (Para visualización, parte de REQ4 y REQ5)
        public ActionResult Index()
        {
            var users = _context.Users.ToList();
            return View(users);
        }
    }
}
