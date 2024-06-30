using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WMSApp.Models;

public class DocumentItem
{
    [Key]
    public int DocumentItemId { get; set; }

    [Required]
    public int DocumentId { get; set; }

    [Required]
    public int ProductId { get; set; }

    [Required]
    public int Quantity { get; set; }

    [Display(Name = "Unit Price")]
    [DataType(DataType.Currency)]
    public decimal UnitPrice { get; set; }

    [Display(Name = "Total Amount")]
    [DataType(DataType.Currency)]
    public decimal TotalAmount { get; set; }

    [Required]
    [StringLength(10)]
    public string TransactionType { get; set; }

    // Navigation properties
    [ForeignKey("DocumentId")]
    public Document Document { get; set; }

    [ForeignKey("ProductId")]
    public Product Product { get; set; }
}