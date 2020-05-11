using LocalChow.Domain.Repository.MenuItemRepository;
using LocalChow.Domain.Repository.MenuRepository;
using LocalChow.Domain.Repository.OrderRepository;
using LocalChow.Domain.Repository.RoleRepository;
using LocalChow.Domain.Repository.StoreRepository;
using LocalChow.Domain.Repository.UserRepository;
using System;
using System.Collections.Generic;
using System.Text;

namespace LocalChow.Domain.Repository
{
    public interface IRepositoryWrapper
    {
        IMenuRepository Menu{ get; }
        IStoreRepository Store { get; }
        IOrderRepository Order { get;  }
        IMenuItemRepository MenuItem { get; }
        IUserRepository User { get; }
        IRoleRepository Role { get; }
        void Save();
    }
}
