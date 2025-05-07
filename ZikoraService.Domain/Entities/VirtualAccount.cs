using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZikoraService.Domain.Entities
{
    public class VirtualAccount
    {
        public Guid Id { get; set; }
        public string AccountNumber { get; set; }
        public string ProductCode { get; set; }
        public string NumberOfAccounts { get; set; }
        public string TrackingReference { get; set; }
        public decimal Amount {  get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
