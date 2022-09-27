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
    public class DealRepository
    {
        public async Task Add(Deal deal)
        {
            NotariusDbContext.db.Deals.Add(deal);
            await NotariusDbContext.db.SaveChangesAsync();
        }

        public async Task Update(Deal deal)
        {
            Deal oldDeal = await NotariusDbContext.db.Deals.FirstOrDefaultAsync(t => t.Id == deal.Id);
            if (oldDeal != null)
            {
                oldDeal.RealCommission = deal.RealCommission;
                oldDeal.Client = deal.Client;
                oldDeal.Date = deal.Date;
                oldDeal.Description = deal.Description;
                oldDeal.RealPrice = deal.RealPrice;
                oldDeal.Service = deal.Service;
                oldDeal.Status = deal.Status;
                oldDeal.TransactionAmount = deal.TransactionAmount;
            }
            await NotariusDbContext.db.SaveChangesAsync();
        }

        public async Task<Deal> Get(int id) => await NotariusDbContext.db.Deals.FirstOrDefaultAsync(t => t.Id == id);

        public async Task<List<Deal>> GetAll() => await NotariusDbContext.db.Deals.ToListAsync();
    }
}
