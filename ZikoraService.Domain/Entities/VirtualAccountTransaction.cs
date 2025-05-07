using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZikoraService.Domain.Entities
{
    public class VirtualAccountTransaction
    {
        public Guid Id { get; set; }
        public string AccountNumber { get; set; }
        public decimal Amount { get; set; }
        public string UniqueReference { get; set; }
        public DateTime InitiatedAt { get; set; } = DateTime.UtcNow;
        public bool IsVerified { get; set; } = false;
    }

}
