using Repositories;
using Repositories.Entities;
using Repositories.Repositories;

namespace Repositories.UnitOfWorks
{
    public interface IUnitOfWork : IDisposable
    {
        UserRepository UserRepository { get; }
        CartRepository CartRepository { get; }
        ProductRepository ProductRepository { get; }
        ProductVariantRepository ProductVariantRepository { get; }
        DiscountCodeRepository DiscountCodeRepository { get; }
        OrderRepository OrderRepository { get; }
        UserDiscountCodeRepository UserDiscountCodeRepository { get; }
        ShippingRepository ShippingRepository { get; }
        PaymentRepository PaymentRepository { get; }
        CategoryRepository CategoryRepository { get; }
        ProductImageRepository ProductImageRepository { get; }
        int SaveChangesWithTransaction();
        Task<int> SaveChangesWithTransactionAsync();

    }
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DemoDbContext _context;

        public UserRepository UserRepository { get; private set; }
        public CartRepository CartRepository { get; private set; }
        public ProductRepository ProductRepository { get; private set; }
        public ProductImageRepository ProductImageRepository { get; private set; }
        public DiscountCodeRepository DiscountCodeRepository { get; private set; }
        public OrderRepository OrderRepository { get; private set; }
        public UserDiscountCodeRepository UserDiscountCodeRepository { get; private set; }
        public ShippingRepository ShippingRepository { get; private set; }
        public PaymentRepository PaymentRepository { get; private set; }
        public CategoryRepository CategoryRepository { get; private set; }

        public ProductVariantRepository ProductVariantRepository { get; private set; }

        public UnitOfWork(
            DemoDbContext context,
            UserRepository userRepository,
            CartRepository cartRepository,
            ProductRepository productRepository,
            DiscountCodeRepository discountCodeRepository,
            OrderRepository orderRepository,
            UserDiscountCodeRepository userDiscountCodeRepository,
            ShippingRepository shippingRepository,
            PaymentRepository paymentRepository,
            CategoryRepository categoryRepository,
            ProductVariantRepository productVariantRepository,
            ProductImageRepository productImageRepository
            )
        {
            _context = context;
            UserRepository = userRepository;
            CartRepository = cartRepository;
            ProductRepository = productRepository;
            DiscountCodeRepository = discountCodeRepository;
            OrderRepository = orderRepository;
            UserDiscountCodeRepository = userDiscountCodeRepository;
            ShippingRepository = shippingRepository;
            PaymentRepository = paymentRepository;
            CategoryRepository = categoryRepository;
            ProductVariantRepository = productVariantRepository;
            ProductImageRepository = productImageRepository;
        }

        public int SaveChangesWithTransaction()
        {
            int result = -1;

            //System.Data.IsolationLevel.Snapshot
            using (var dbContextTransaction = _context.Database.BeginTransaction())
            {
                try
                {
                    result = _context.SaveChanges();
                    dbContextTransaction.Commit();
                }
                catch (Exception)
                {
                    //Log Exception Handling message                      
                    result = -1;
                    dbContextTransaction.Rollback();
                }
            }

            return result;
        }

        public async Task<int> SaveChangesWithTransactionAsync()
        {
            int result = -1;

            //System.Data.IsolationLevel.Snapshot
            using (var dbContextTransaction = _context.Database.BeginTransaction())
            {
                try
                {
                    result = await _context.SaveChangesAsync();
                    dbContextTransaction.Commit();
                }
                catch (Exception)
                {
                    //Log Exception Handling message                      
                    result = -1;
                    dbContextTransaction.Rollback();
                }
            }

            return result;
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
