using LocalChow.Persistence.Models;
using LocalChow.Persistence.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LocalChow.Domain.Repository.RoleRepository
{
    public class RoleRepository : RepositoryBase<Role>, IRoleRepository
    {
        public RoleRepository(LocalChowDbContext context) : base(context)
        {
        }
        public Role GetRoleByName(string name)
        {
            return FindByCondition(x => x.NormalizedName == name.ToUpper()).FirstOrDefault();
        }
    }
}
