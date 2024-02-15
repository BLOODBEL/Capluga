using Capluga.Models;
using System.Collections.Generic;
using Capluga.Services.Interfaces;
using System.Data.Entity;
using System.Linq;

namespace Capluga.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly CaplugaDbContext _context;

        public UserService(CaplugaDbContext context)
        {
            _context = context;
        }

        public IEnumerable<User> GetAllUsers()
        {
            return _context.Users.ToList();
        }

        public User GetUserById(long userId)
        {
            return _context.Users.Find(userId);
        }

        public void AddUser(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
        }

        public void UpdateUser(User user)
        {
            _context.Entry(user).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void ToggleUserState(long userId)
        {
            var user = _context.Users.Find(userId);
            if (user != null)
            {
                user.State = !user.State;
                _context.SaveChanges();
            }
        }

        public void RecoverPassword(string email)
        {
            // Lógica para recuperar la contraseña
            // Por ejemplo, generar un token de restablecimiento y enviarlo por correo electrónico
        }

        public bool ValidateLogin(string email, string password)
        {
            // Aquí debes implementar la validación de las credenciales
            // Esto es solo un ejemplo y deberías usar hashing y salting para las contraseñas
            var user = _context.Users.FirstOrDefault(u => u.Email == email);
            return user != null && user.Password == password;
        }
        public bool UserExists(long userId)
        {  
            return _context.Users.Any(u => u.UserId == userId); 
        }
    }
}
