using IssueStock.Demo.API.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IssueStock.Demo.API.Repository
{
    public class StockDataRepository : IDataRepository<StockItem>
    {
        readonly StockContext _stockContext;

        public StockDataRepository(StockContext stockContext)
        {
            _stockContext = stockContext;
        }

        public async Task AddAsync(StockItem entity)
        {
            await _stockContext.StockItem.AddAsync(entity);
            await _stockContext.SaveChangesAsync();
        }

        public async Task<StockItem> GetAsync(int id)
        {
            return await _stockContext.StockItem
                 .FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<IEnumerable<StockItem>> GetAllAsync()
        {
            return await _stockContext.StockItem.ToListAsync();
        }

        public async Task UpdateAsync(StockItem dbEntity, StockItem entity)
        {
            dbEntity.Code = entity.Code;
            dbEntity.Description = entity.Description;
            dbEntity.Name = entity.Name;

            await _stockContext.SaveChangesAsync();
        }
    }
}
