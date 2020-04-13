using LocalChow.Persistence.Models;
using LocalChow.Persistence.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace LocalChow.Domain.Repository.MenuRepository
{
    public interface IMenuRepository: IRepositoryBase<Menu>
    {
        IEnumerable<Menu> GetAllMenu();
        Menu GetMenuById(int id);
    }
}
