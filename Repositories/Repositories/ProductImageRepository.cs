using Repositories.Basic;
using Repositories.Entities;

namespace Repositories.Repositories
{
    public class ProductImageRepository : GenericRepository<ProductImage>
    {
        public ProductImageRepository(DemoDbContext context) : base(context)
        {

        }
    }
}
