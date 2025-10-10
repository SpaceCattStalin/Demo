using Repositories;
using Repositories.Entities;
using Repositories.Repositories;

namespace Services
{
    public interface IUnitOfWork : IDisposable
    {
        UserRepository SystemUserAccountRepository { get; }
        //CartRepository CartRepository { get; }
        //CartItemRepository CartItemRepository { get; }
        //ProductRepository ProductRepository { get; }
    }
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        private readonly UserRepository _userRepository;
        private readonly CartRepository _cartRepository;
        private readonly ProductRepository _productRepository;
        private readonly CartItemRepository _cartItemRepository;
        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }
        public UserRepository SystemUserAccountRepository
        {
            get
            {
                if (_userRepository == null)
                {
                    return new UserRepository(_context);
                }
                return _userRepository;
            }
        }

        //public CartRepository CartRepository
        //{
        //    get
        //    {
        //        if (_cartItemRepository == null)
        //        {
        //            return new CartRepository();
        //        }
        //        return _cartRepository;
        //    }
        //}

        //public CartItemRepository CartItemRepository
        //{
        //    get
        //    {
        //        if (_cartItemRepository == null)
        //        {
        //            return new CartItemRepository();
        //        }
        //        return _cartItemRepository;
        //    }
        //}


        //public ProductRepository ProductRepository
        //{
        //    get
        //    {
        //        if (_productRepository == null)
        //        {
        //            return new ProductRepository();
        //        }
        //        return _productRepository;
        //    }
        //}

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
