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
        [HttpGet("GetInvoice{InvoiceId}")]
        public async Task<IActionResult> GetInvoice(int InvoiceId)
        {
            var invoice = await _context.Invoices
                .Include(i => i.InvoiceItems)
                .ThenInclude(ii => ii.Item) // Include the Item details
                .FirstOrDefaultAsync(i => i.Id == InvoiceId);

            if (invoice == null)
            {
                return NotFound();
            }

            // Mapping to DTO (you can create a more detailed DTO if needed)
            var invoiceDto = new InvoiceDto
            {
                Id = InvoiceId,
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


        [HttpGet("GetAllInvoices")]
        public async Task<IActionResult> GetAllInvoices()
        {
            
            // Fetch all invoices related to the ItemId
            var invoices = await _context.InvoiceItems
                .Select(x => x.Invoice)
                .ToListAsync();

            if (invoices.Count == 0)
            {
                return NotFound($"No invoices found.");
            }

            // Prepare a list to hold the invoice DTOs
            var invoiceDtos = new List<InvoiceDto>();

            // Loop through the invoices and call GetInvoice for each one
            foreach (var invoice in invoices)
            {
                var result = await GetInvoice(invoice.Id);

                if (result is OkObjectResult okResult && okResult.Value is InvoiceDto invoiceDto)
                {
                    invoiceDtos.Add(invoiceDto); // Add the DTO to the list
                }
            }

            // Return the list of invoice DTOs
            return Ok(invoiceDtos);
        }

        [HttpGet("ContainsItem{ItemId}")]

        public async Task<IActionResult> GetInvoicesWithItem(int ItemId)
        {
            var item = await _context.Items.FindAsync(ItemId);
            if (item == null)
            {
                return BadRequest($"Item with ID {ItemId} not found.");
            }

            // Fetch all invoices related to the ItemId
            var invoices = await _context.InvoiceItems
                .Where(x => x.ItemId == ItemId)
                .Select(x => x.Invoice)
                .ToListAsync();

            if (invoices.Count == 0)
            {
                return NotFound($"No invoices found for the item with ID {ItemId}.");
            }

            // Prepare a list to hold the invoice DTOs
            var invoiceDtos = new List<InvoiceDto>();

            // Loop through the invoices and call GetInvoice for each one
            foreach (var invoice in invoices)
            {
                var result = await GetInvoice(invoice.Id);

                if (result is OkObjectResult okResult && okResult.Value is InvoiceDto invoiceDto)
                {
                    invoiceDtos.Add(invoiceDto); // Add the DTO to the list
                }
            }

            // Return the list of invoice DTOs
            return Ok(invoiceDtos);
        }


        // [HttpPost]: Create a new invoice with one or more items
        [HttpPost("CreateInvoice")]
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

        [HttpDelete]
        public async Task<IActionResult> Delete(int InvoiceId)
        {
            var Invoice = await _context.Invoices.FindAsync(InvoiceId);
            if (Invoice == null)
            {
                return NotFound($"No item with ID {InvoiceId}");
            }
            _context.Invoices.Remove(Invoice);
            _context.SaveChanges();
            return Ok(Invoice);
        }
    }
}
