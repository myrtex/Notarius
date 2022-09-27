using NotariusBack.Repository;
using NotariusBack.Repository.Entity;
using NotariusBack.Repository.Entity.Enums;
using NotariusBack.Service.ModelDto;
using System.Security.Cryptography;
using System.Text;

namespace NotariusBack.Service
{
    public class UserService
    {
        UserRepository repository;

        public UserService()
        {
            repository = new UserRepository();
        }

        public async Task Register(UserDto model)
        {
            if ((await repository.Get(model.Name)) == null)
            {
                if (model.Password.Length < 4)
                {
                    throw new ArgumentException();
                }
                SHA512 sha = SHA512.Create();
                string hash = Encoding.UTF8.GetString(sha.ComputeHash(Encoding.UTF8.GetBytes(model.Password)));
                User user = new User() { Name = model.Name, Password = hash, Role = model.Role };
                await repository.Register(user);
            }
            else
            {
                throw new ArgumentException();
            }
        }

        public async Task<int?> Login(LoginDto model)
        {
            User user = await repository.Get(model.Name);
            if (user != null)
            {
                SHA512 sha = SHA512.Create();
                string hash = Encoding.UTF8.GetString(sha.ComputeHash(Encoding.UTF8.GetBytes(model.Password)));
                if (user.Password == hash)
                {
                    return await repository.Login(model.Name);
                }
                else
                {
                    throw new ArgumentException();
                }
            }
            else
            {
                throw new ArgumentException();
            }
        }

        public async Task<UserRoleEnum?> GetRole(int id)
        {
            return await repository.GetRole(id);
        }
    }
}