using Repositories.Basic;
using Repositories.Entities;

namespace Repositories.Repositories
{
    public class ProductVariantRepository : GenericRepository<ProductVariant>
    {
        public ProductVariantRepository(DemoDbContext context) : base(context)
        {

        }
    }
}
