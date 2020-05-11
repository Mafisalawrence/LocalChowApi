using LocalChow.Persistence.Models;
using LocalChow.Persistence.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LocalChow.Domain.Repository.MenuRepository
{
    public class MenuRepository : RepositoryBase<Menu>, IMenuRepository
    {
        public MenuRepository(LocalChowDbContext context) : base(context)
        {
        }

        public IEnumerable<Menu> GetAllMenu()
        {
            return FindAll().ToList();
        }
        public Menu GetMenuById(Guid id)
        {
            return FindByCondition(x => x.MenuID == id).FirstOrDefault();
        }
    }
}
