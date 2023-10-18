using Business.CustomExceptions;
using Business.Interfaces;
using Data;
using Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Bloat
{
    public class UserRepository : IUserRepository, IDisposable
    {
        private readonly ApplicationDbContext _context;
        private bool _disposed;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task DeleteUser(User user)
        {
            _context.UserList.Remove(user);
            await Save();
        }

        public async Task<User> GetUserById(int id)
        {
            var user = await _context.UserList.FindAsync(id);
            if (user == null)
            {
                throw new UserNotFoundException();
            }
            return user;
        }

        public async Task<List<User>> GetUsers()
        {
            var users = await _context.UserList.ToListAsync();
            return users;
        }

        public async Task InsertUser(User user)
        {
            await _context.UserList.AddAsync(user);
            await Save();
        }

        public async Task Save()
        {
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {

                throw new DbUpdateConcurrencyException();
            }
        }

        public async Task UpdateUser(User user)
        {
            _context.UserList.Update(user);
            await Save();
        }


        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
