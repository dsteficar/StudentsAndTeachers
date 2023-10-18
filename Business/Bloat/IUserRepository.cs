using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Bloat
{
    public interface IUserRepository : IDisposable
    {
        Task<List<User>> GetUsers();
        Task<User> GetUserById(int id);
        Task InsertUser(User user);
        Task UpdateUser(User user);
        Task DeleteUser(User user);
        Task Save();
    }
}
