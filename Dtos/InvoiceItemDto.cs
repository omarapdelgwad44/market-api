using market_api.models;

namespace market_api.Dtos
{
    public class InvoiceItemDto
    {
        public string Name { get; set; } // Item name
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public byte PriceType { get; set; }
    }
}
