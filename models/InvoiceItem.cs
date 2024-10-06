namespace market_api.models
{
    public class InvoiceItem
    {
        public int InvoiceId { get; set; }
        public Invoice Invoice { get; set; }

        public int ItemId { get; set; }
        public Item Item { get; set; }

        public int Quantity { get; set; }  // Quantity of this item in this invoice
        public double price { get; set; }
        public double TotalCost { get; set; } // This will be the computed column

        public byte PriceType { get; set; }

    }
}
