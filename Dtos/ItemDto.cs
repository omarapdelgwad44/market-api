namespace market_api.Dtos
{
    public class ItemDto
    {
        public string Name { get; set; }
        public double InitialPrice { get; set; }
        public double RetailPrice { get; set; } // I assume you meant 'pricePrice' as retail price
        public double WholesalePrice { get; set; }
        public int Quantity { get; set; }
        public IFormFile ?image { get; set; }
    }
}
