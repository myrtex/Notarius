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

        public async Task Update(ServiceDto serviceDto)
        {
            Repository.Entity.Service client = new Repository.Entity.Service() { Status = serviceDto.Status, Description = serviceDto.Description, Price = serviceDto.Price, Commission = serviceDto.Commission };
            await repository.Update(client);
        }

        public async Task UpdatePrice(ServiceDto serviceDto)
        {
            Repository.Entity.Service client = new Repository.Entity.Service() { Price = serviceDto.Price, Commission = serviceDto.Commission };
            await repository.Update(client);
        }

        public async Task<Repository.Entity.Service> Get(string name)
        {
            return await repository.Get(name);
        }
    }
}
