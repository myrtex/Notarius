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
    public class ServiceRepository
    {
        public async Task Add(Entity.Service service)
        {
            NotariusDbContext.db.Services.Add(service);
            await NotariusDbContext.db.SaveChangesAsync();
        }

        public async Task Update(Entity.Service service)
        {
            Entity.Service oldservice = await NotariusDbContext.db.Services.FirstOrDefaultAsync(t => t.Id == service.Id);
            if (oldservice != null)
            {
                oldservice.Status = service.Status;
                oldservice.Description = service.Description;
                oldservice.Price = service.Price;
                oldservice.Commission = service.Commission;
            }
            await NotariusDbContext.db.SaveChangesAsync();
        }

        public async Task UpdatePrice(Entity.Service service)
        {
            Entity.Service oldservice = await NotariusDbContext.db.Services.FirstOrDefaultAsync(t => t.Id == service.Id);
            if (oldservice != null)
            {
                oldservice.Price = service.Price;
                oldservice.Commission = service.Commission;
            }
            await NotariusDbContext.db.SaveChangesAsync();
        }

        public async Task<Entity.Service> Get(int id) => await NotariusDbContext.db.Services.FirstOrDefaultAsync(t => t.Id == id);

        public async Task<Entity.Service> Get(string name) => await NotariusDbContext.db.Services.FirstOrDefaultAsync(t => t.Name == name);

        public async Task<List<Entity.Service>> GetAll() => await NotariusDbContext.db.Services.ToListAsync();
    }
}
