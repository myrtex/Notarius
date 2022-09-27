using Microsoft.EntityFrameworkCore;
using NotariusBack.Repository.Entity;
using NotariusBack.Repository.Entity.Enums;
using NotariusBack.Service;
using System.Security.Cryptography;
using System.Xml.Linq;

namespace NotariusBack.Repository
{
    public class UserRepository
    {
        public async Task Register(User model)
        {
            NotariusDbContext.db.Users.Add(model);
            await NotariusDbContext.db.SaveChangesAsync();
        }

        public async Task<int?> Login(string name)
        {
            User? user = await NotariusDbContext.db.Users.FirstOrDefaultAsync(u => u.Name == name);
            return user?.Id;
        }

        public async Task<User> Get(string name)
        {
            return await NotariusDbContext.db.Users.FirstOrDefaultAsync(u => u.Name == name);
        }

        public async Task<UserRoleEnum?> GetRole(int id) => (await NotariusDbContext.db.Users.FirstOrDefaultAsync(u => u.Id == id))?.Role;
    }
}