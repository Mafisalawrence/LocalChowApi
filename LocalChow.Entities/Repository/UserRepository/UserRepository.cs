using LocalChow.Persistence.Models;
using LocalChow.Persistence.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LocalChow.Domain.Repository.UserRepository
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public UserRepository(LocalChowDbContext context) : base(context)
        {
        }

        public User GetUserByID(int id)
        {
            return FindByCondition(x => x.UserID == id).FirstOrDefault();
        }
    }
}
