using System.ComponentModel.DataAnnotations;

namespace WMSApp.Models;
public class Document
{
    [Key]
    public int DocumentId { get; set; }
    
    [Required]
    public DocumentStatus Status { get; set; }

    [Required]
    public DocumentType DocumentType { get; set; }
    public DateTime DocumentDate { get; set; }

    [Required]
    [StringLength(20)]
    public string DocumentNumber { get; set; }

    [DataType(DataType.Date)]
    public DateTime Date { get; set; }

    [Required]
    [StringLength(100)]
    public string CreatedBy { get; set; }

    [Display(Name = "Creation Date")]
    public DateTime CreatedDate { get; set; }

    // Navigation property
    public ICollection<DocumentItem> Items { get; set; }
}

public enum DocumentType
{
    Shipping,
    Receiving
}

public enum DocumentStatus
{
    Draft,
    WorkInProgress,
    Completed
}