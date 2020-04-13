using LocalChow.Persistence.Models;
using LocalChow.Persistence.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace LocalChow.Domain.Repository.UserRepository
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public UserRepository(LocalChowDbContext context) : base(context)
        {
        }
    }
}
