using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.DTOs
{
    namespace Services.DTOs
    {
        public class AdminPaymentStatisticDto
        {
            public int TotalPayments { get; set; }
            public decimal TotalAmountPaid { get; set; }

            public int PendingCount { get; set; }
            public int PaidCount { get; set; }
            public int FailedCount { get; set; }

            public List<PaymentMethodStatistic> ByMethod { get; set; } = new();
        }

        public class PaymentMethodStatistic
        {
            public string MethodName { get; set; }
            public string MethodCode { get; set; }
            public int Count { get; set; }
            public decimal TotalAmount { get; set; }
        }
    }

}
