using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.DTOs
{
    public class AdminPaymentListDto
    {
        public int Id { get; set; }                 // Payment ID
        public decimal Amount { get; set; }         // Total paid amount

        public string Status { get; set; }          // e.g. "Success", "Pending"
        public string MethodName { get; set; }      // e.g. "VNPay", "Credit Card"
        public string MethodCode { get; set; }      // e.g. "VNPAY", "STRIPE"

        public int OrderId { get; set; }            // Related Order
        public string OrderStatus { get; set; }

        public int UserId { get; set; }             // Who paid
        public string UserName { get; set; }
        public string UserEmail { get; set; }

        public int CreatedDate { get; set; }        // Unix time for sorting/filtering
        public int? ProcessedDate { get; set; }
    }

    public class AdminPaymentDetailDto
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }

        public string Status { get; set; }
        public int? ProcessedDate { get; set; }
        public int CreatedDate { get; set; }
        public int? UpdatedDate { get; set; }

        // --- Payment Method Info ---
        public int MethodId { get; set; }
        public string MethodName { get; set; }
        public string MethodCode { get; set; }

        // --- Order Info ---
        public int OrderId { get; set; }
        public decimal OrderTotal { get; set; }
        public string OrderStatus { get; set; }

        // --- User Info ---
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string UserEmail { get; set; }
        public string? UserPhone { get; set; }
        public string? UserAddress { get; set; }
    }

}

