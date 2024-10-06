using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using market_api.Dtos;
using market_api.models;

namespace market_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InvoiceController : ControllerBase
    {
        private readonly DataBaseContext _context;

        public InvoiceController(DataBaseContext context)
        {
            _context = context;
        }

        // [HttpGet]: Get invoice details by id
        [HttpGet("{id}")]
        public async Task<IActionResult> GetInvoice(int id)
        {
            var invoice = await _context.Invoices
                .Include(i => i.InvoiceItems)
                .ThenInclude(ii => ii.Item) // Include the Item details
                .FirstOrDefaultAsync(i => i.Id == id);

            if (invoice == null)
            {
                return NotFound();
            }

            // Mapping to DTO (you can create a more detailed DTO if needed)
            var invoiceDto = new InvoiceDto
            {
                Description = invoice.Description,
                TotalPrice = (decimal)invoice.TotalPrice,
                Date = invoice.date,
                Time = invoice.Time,
                InvoiceItems = invoice.InvoiceItems.Select(ii => new InvoiceItemDto
                {
                    Name = ii.Item.Name,
                    Price = (decimal)ii.price,
                    Quantity = ii.Quantity,
                    PriceType = (byte)ii.PriceType
                }).ToList()
            };

            return Ok(invoiceDto);
        }

        // [HttpPost]: Create a new invoice with one or more items
        [HttpPost]
        public async Task<IActionResult> CreateInvoice([FromBody] CreateInvoiceDto createInvoiceDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Create a new invoice
            var invoice = new Invoice
            {
                Description = createInvoiceDto.Description,
                date = DateOnly.FromDateTime(DateTime.Now),
                Time = TimeOnly.FromDateTime(DateTime.Now),
            };

            // Calculate total price of the invoice
            decimal totalPrice = 0;

            foreach (var itemDto in createInvoiceDto.InvoiceItems)
            {
                var item = await _context.Items.FindAsync(itemDto.ItemId);
                if (item == null)
                {
                    return BadRequest($"Item with ID {itemDto.ItemId} not found.");
                }

                // Determine the price based on the price type (retail or wholesale)
                double v = itemDto.PriceType switch
                {
                    1 => item.RetailPrice,  // Retail
                    2 => item.WholesalePrice,  // Wholesale
                    _ => throw new ArgumentOutOfRangeException(nameof(itemDto.PriceType), "Invalid price type")
                };
                decimal price = (decimal)v;

                var invoiceItem = new InvoiceItem
                {
                    ItemId = item.Id,
                    Quantity = itemDto.Quantity,
                    price = (double)price,
                    TotalCost = (double)(price * itemDto.Quantity)
                    ,PriceType = itemDto.PriceType
                };

                item.Quantity-=itemDto.Quantity;
                totalPrice += (decimal)invoiceItem.TotalCost;

                invoice.InvoiceItems.Add(invoiceItem);
            }

            invoice.TotalPrice = (double)totalPrice;

            _context.Invoices.Add(invoice);
            await _context.SaveChangesAsync();

            return Ok(invoice.Id);
        }
    }
}
