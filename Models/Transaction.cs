using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WMSApp.Models
{
    public class Transaction
    {
        [Key]
        public int TransactionId { get; set; }

        [Required]
        public TransactionType TransactionType { get; set; }

        public int ProductId { get; set; }
        public int? FromLocationId { get; set; }
        public int ToLocationId { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        public DateTime TransactionDate { get; set; }

        [ForeignKey("ProductId")]
        public Product Product { get; set; }

        [ForeignKey("LocationId")]
        public int LocationId { get; set; }
    }
    
    public enum TransactionType
    {
        Inbound,
        Outbound
    }
}