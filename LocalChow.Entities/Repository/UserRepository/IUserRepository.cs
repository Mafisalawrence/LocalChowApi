using LocalChow.Persistence.Models;
using LocalChow.Persistence.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace LocalChow.Domain.Repository.UserRepository
{
    public interface IUserRepository: IRepositoryBase<User>
    {
        User GetUserByID(int id);
    }
}
