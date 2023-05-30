using IssueStock.Demo.API.Models;
using System.Collections.Generic;
using System.Linq;

namespace IssueStock.Demo.API.Repository
{
    public class StockDataRepository : IDataRepository<StockItem>
    {
        readonly StockContext _stockContext;

        public StockDataRepository(StockContext stockContext)
        {
            _stockContext = stockContext;
        }

        public void Add(StockItem entity)
        {
            _stockContext.StockItem.Add(entity);
            _stockContext.SaveChanges();
        }

        public void Delete(StockItem entity)
        {
            _stockContext.StockItem.Remove(entity);
            _stockContext.SaveChanges();
        }

        public StockItem Get(int id)
        {
            return _stockContext.StockItem
                 .FirstOrDefault(e => e.Id == id);
        }

        public IEnumerable<StockItem> GetAll()
        {
            return _stockContext.StockItem.ToList();
        }

        public void Update(StockItem dbEntity, StockItem entity)
        {
            dbEntity.Code = entity.Code;
            dbEntity.Description = entity.Description;
            dbEntity.Name = entity.Name;

            _stockContext.SaveChanges();
        }
    }
}
