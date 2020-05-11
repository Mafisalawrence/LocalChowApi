using LocalChow.Persistence.Models;
using LocalChow.Persistence.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace LocalChow.Domain.Repository.MenuItemRepository
{
    public interface IMenuItemRepository:IRepositoryBase<MenuItem>
    {
        IEnumerable<MenuItem> GetAllMenuItems();
        MenuItem GetMenuItemById(Guid id);
    }
}
