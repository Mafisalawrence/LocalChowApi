using LocalChow.Persistence.Models;
using LocalChow.Persistence.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LocalChow.Domain.Repository.MenuItemRepository
{
    public class MenuItemRepository : RepositoryBase<MenuItem>, IMenuItemRepository
    {
        public MenuItemRepository(LocalChowDbContext context) : base(context)
        {
        }
        public IEnumerable<MenuItem> GetAllMenuItems()
        {
            return FindAll().ToList();
        }

        public MenuItem GetMenuItemById(int id)
        {
            return FindByCondition(x => x.MenuItemId == id).FirstOrDefault();
        }
    }
}
