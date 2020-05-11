using LocalChow.Persistence.Models;
using LocalChow.Persistence.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace LocalChow.Domain.Repository.StoreRepository
{
    public interface IStoreRepository: IRepositoryBase<Store>
    {
        IEnumerable<Store> GetAllStore();
        Store GetStoreById(Guid id);
    }
}
