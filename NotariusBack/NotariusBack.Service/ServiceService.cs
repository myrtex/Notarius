using NotariusBack.Repository.Entity;
using NotariusBack.Repository;
using NotariusBack.Service.ModelDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotariusBack.Service
{
    public class ServiceService
    {
        ServiceRepository repository;

        public ServiceService()
        {
            repository = new ServiceRepository();
        }

        public async Task Add(ServiceDto serviceDto)
        {
            Repository.Entity.Service client = new Repository.Entity.Service() { Name = serviceDto.Name, Status = true, Description =  serviceDto.Description, Price = serviceDto.Price, Commission = serviceDto.Commission};
            await repository.Add(client);
        }

        public async Task Update(ServiceDto serviceDto, int id)
        {
            Repository.Entity.Service client = new Repository.Entity.Service() { Id = id, Status = true, Description = serviceDto.Description, Price = serviceDto.Price, Commission = serviceDto.Commission };
            await repository.Update(client);
        }

        public async Task UpdatePrice(ServiceDto serviceDto, int id)
        {
            Repository.Entity.Service client = new Repository.Entity.Service() { Id = id, Status = true, Description = null, Price = serviceDto.Price, Commission = serviceDto.Commission };
            await repository.Update(client);
        }

        public async Task Delete(int id)
        {
            Repository.Entity.Service client = new Repository.Entity.Service() { Id = id, Status = false, Description = null, Price = -1, Commission = -1 };
            await repository.Update(client);
        }

        public async Task<Repository.Entity.Service> Get(string name)
        {
            return await repository.Get(name);
        }

        public async Task<List<Repository.Entity.Service>> GetAll()
        {
            return await repository.GetAll();
        }
    }
}
