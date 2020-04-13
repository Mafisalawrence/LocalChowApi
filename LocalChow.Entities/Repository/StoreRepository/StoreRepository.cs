using LocalChow.Persistence.Models;
using LocalChow.Persistence.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LocalChow.Domain.Repository.StoreRepository
{
    public class StoreRepository : RepositoryBase<Store>, IStoreRepository
    {
        public StoreRepository(LocalChowDbContext context) : base(context)
        {
        }

        public IEnumerable<Store> GetAllStore()
        {
            return FindAll().ToList();
        }

        public Store GetStoreById(int id)
        {
            return FindByCondition(x => x.StoreID == id).FirstOrDefault();
        }
    }
}
