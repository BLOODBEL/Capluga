using Capluga.Models;
using Capluga.Services.Interfaces;
using System.Web.Mvc;

namespace Capluga.Controllers
{
    public class UsersController : Controller
    {
        private readonly IUserService _userService;

        // El constructor ahora recibe la interfaz del servicio
        public UsersController(IUserService userService) => _userService = userService;

        // Listar usuarios (Para visualización, parte de REQ4 y REQ5)
        [HttpGet]
        public ActionResult Index()
        {
            var users = _userService.GetAllUsers();
            return View(users);
        }

        // Agregar usuario (REQ4 y REQ5)
        [HttpPost]
        public ActionResult AddUser(User user)
        {
            if (ModelState.IsValid)
            {
                _userService.AddUser(user);
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
                _userService.UpdateUser(user);
                return RedirectToAction("Index");
            }
            return View(user);
        }

        // Activar/Inactivar usuario (REQ5)
        [HttpPost]
        public ActionResult ToggleUserState(long userId)
        {
            if (_userService.UserExists(userId))
            {
                _userService.ToggleUserState(userId);
                return RedirectToAction("Index");
            }
            return HttpNotFound();
        }

        // Las acciones de recuperación de contraseña y de inicio de sesión seguro deben implementarse en el servicio
        // y luego invocarse desde aquí.

        // Recuperar contraseña (REQ2)
        [HttpPost]
        public ActionResult RecoverPassword(string email)
        {
            // Aquí invocarías un método del servicio, por ejemplo:
            // _userService.RecoverPassword(email);
            return View();
        }

        // Inicio de sesión seguro (REQ3)
        [HttpPost]
        public ActionResult SecureLogin(string email, string password)
        {
            // Aquí invocarías un método del servicio, por ejemplo:
            // var user = _userService.SecureLogin(email, password);
            return View();
        }

        // ... otras acciones
    }
}
