using Microsoft.EntityFrameworkCore;
using NotariusBack.Repository.Entity;
using NotariusBack.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotariusBack.Repository
{
    public class ClientRepository
    {
        public async Task Add(Client client)
        {
            NotariusDbContext.db.Clients.Add(client);
            await NotariusDbContext.db.SaveChangesAsync();
        }

        public async Task Update(Client client)
        {
            Client oldClient = await NotariusDbContext.db.Clients.FirstOrDefaultAsync(t => t.Id == client.Id);
            if(oldClient != null)
            {
                if(client.Phone != null) oldClient.Phone = client.Phone;
                if (client.Adress != null) oldClient.Adress = client.Adress;
            }
            await NotariusDbContext.db.SaveChangesAsync();
        }

        public async Task<Client> Get(string name) => await NotariusDbContext.db.Clients.FirstOrDefaultAsync(t => t.Name == name);
    }
}
