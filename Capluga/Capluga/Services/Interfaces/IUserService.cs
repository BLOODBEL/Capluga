using Capluga.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Capluga.Services.Interfaces
{
    public interface IUserService
    {
        IEnumerable<User> GetAllUsers();
        User GetUserById(long userId);
        void AddUser(User user);
        void UpdateUser(User user);
        void ToggleUserState(long userId);
        void RecoverPassword(string email);
        bool ValidateLogin(string email, string password);
        bool UserExists(long userId);
    }
}
