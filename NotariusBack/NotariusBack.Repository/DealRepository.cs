using Microsoft.EntityFrameworkCore;
using NotariusBack.Repository.Entity;
using NotariusBack.Repository.Entity.Enums;
using NotariusBack.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotariusBack.Repository
{
    public class DealRepository
    {
        public async Task Add(Deal deal)
        {
            NotariusDbContext.db.Deals.Add(deal);
            await NotariusDbContext.db.SaveChangesAsync();
        }

        public async Task UpdateTransaction(int id, int transactionAmount)
        {
            Deal deal = NotariusDbContext.db.Deals.FirstOrDefault(t => t.Id == id);
            if(deal != null)
            {
                deal.TransactionAmount = transactionAmount;
                await NotariusDbContext.db.SaveChangesAsync();
            }
            else
            {
                throw new ArgumentException("Такого id не существует");
            }
        }

        public async Task ChangeStatus(int id, DealStatusEnum dealStatus)
        {
            Deal deal = NotariusDbContext.db.Deals.FirstOrDefault(t => t.Id == id);
            if (deal != null)
            {
                deal.Status = dealStatus;
                await NotariusDbContext.db.SaveChangesAsync();
            }
            else
            {
                throw new ArgumentException("Такого id не существует");
            }
        }

        public async Task<Deal> Get(int id) => await NotariusDbContext.db.Deals.FirstOrDefaultAsync(t => t.Id == id);

        public async Task<List<Deal>> GetAll() => await NotariusDbContext.db.Deals.ToListAsync();
    }
}
