using System;

namespace market_api.Dtos
{
    public class InvoiceDto
    {
        public int Id { get; set; }
        public string? Description { get; set; }
        public decimal TotalPrice { get; set; }
        public DateOnly Date { get; set; }
        public TimeOnly Time { get; set; }
        public List<InvoiceItemDto> InvoiceItems { get; set; } = new List<InvoiceItemDto>();
    }
}

