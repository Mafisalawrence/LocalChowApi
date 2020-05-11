using LocalChow.Persistence.Models;
using LocalChow.Persistence.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace LocalChow.Domain.Repository.RoleRepository
{
    public interface IRoleRepository : IRepositoryBase<Role>
    {
        Role GetRoleByName(string name);
    }
}
