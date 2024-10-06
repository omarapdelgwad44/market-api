namespace market_api.Dtos
{

    public class CreateInvoiceItemDto
    {
        public int ItemId { get; set; } // Item to be added to the invoice
        public byte PriceType { get; set; } // 1 for retail, 2 for wholesale
        public int Quantity { get; set; }
    }
}
