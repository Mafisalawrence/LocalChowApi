using LocalChow.Domain.Repository;
using LocalChow.Domain.Repository.MenuItemRepository;
using LocalChow.Domain.Repository.MenuRepository;
using LocalChow.Domain.Repository.OrderRepository;
using LocalChow.Domain.Repository.RoleRepository;
using LocalChow.Domain.Repository.StoreRepository;
using LocalChow.Domain.Repository.UserRepository;
using LocalChow.Persistence.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace LocalChow.Domain
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private readonly LocalChowDbContext _context;
        private  IMenuItemRepository _menuItemRepository;
        private  IStoreRepository _storeRepository;
        private  IOrderRepository _orderRepository;
        private  IUserRepository _userRepository;
        private  IMenuRepository _menuRepository;
        private IRoleRepository _roleRepository;

        public RepositoryWrapper(LocalChowDbContext context) => _context = context;

        public IMenuRepository Menu
        {
            get
            {
                if (_menuRepository == null)
                {
                    _menuRepository = new MenuRepository(_context);
                }
                return _menuRepository;
            }
        }

        public IStoreRepository Store
        {
            get
            {
                if (_storeRepository == null)
                {
                    _storeRepository = new StoreRepository(_context);
                }
                return _storeRepository;
            }
        }

        public IOrderRepository Order
        {
            get
            {
                if (_orderRepository == null)
                {
                    _orderRepository = new OrderRepository(_context);
                }
                return _orderRepository;
            }
        }

        public IMenuItemRepository MenuItem
        {
            get
            {
                if (_menuItemRepository == null)
                {
                    _menuItemRepository = new MenuItemRepository(_context);
                }
                return _menuItemRepository;
            }
        }

        public IUserRepository User
        {
            get
            {
                if (_userRepository == null)
                {
                    _userRepository = new UserRepository(_context);
                }
                return _userRepository;
            }
        }
        public IRoleRepository Role
        {
            get
            {
                if (_roleRepository == null)
                {
                    _roleRepository = new RoleRepository(_context);
                }
                return _roleRepository;
            }
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
