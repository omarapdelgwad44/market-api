namespace market_api.Dtos
{
    public class CreateInvoiceDto
    {
        public string? Description { get; set; }
        public List<CreateInvoiceItemDto> InvoiceItems { get; set; } = new List<CreateInvoiceItemDto>();
    }
}
