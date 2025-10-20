using Repositories.Basic;
using Repositories.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Repositories
{
    public class PaymentRepository : GenericRepository<Payment>
    {
        public PaymentRepository(DemoDbContext context) : base(context)
        {
        }
    }
}
