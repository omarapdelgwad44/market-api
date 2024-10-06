namespace market_api.models;

public class Invoice
{
    public int Id { get; set; }
    public string ?Description { get; set; }
    public double TotalPrice { get; set; }
    public DateOnly date { get; set; }
    public TimeOnly Time { get; set; }

    public ICollection<InvoiceItem> InvoiceItems { get; set; } = new List<InvoiceItem>();


    // Navigation property for the many-to-many relationship
}