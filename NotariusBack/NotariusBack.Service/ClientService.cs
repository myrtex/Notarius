using NotariusBack.Repository;
using NotariusBack.Repository.Entity;
using NotariusBack.Repository.Entity.Enums;
using NotariusBack.Service.ModelDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotariusBack.Service
{
    public class ClientService
    {
        ClientRepository repository;

        public ClientService()
        {
            repository = new ClientRepository();
        }

        public async Task Add(ClientDto clientDto)
        {
            Client client = new Client() { Name = clientDto.Name, Adress = clientDto.Adress, Phone = clientDto.Phone, Type = clientDto.Type};
            await repository.Add(client);
        }

        public async Task Update(ClientDto clientDto)
        {
            Client client = new Client() { Adress = clientDto.Adress, Phone = clientDto.Phone};
            await repository.Update(client);
        }

        public async Task<Client> Get(string name)
        {
            return await repository.Get(name);
        }
    }
}
