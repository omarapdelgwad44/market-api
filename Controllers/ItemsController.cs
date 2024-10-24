using market_api.Dtos;
using market_api.models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SharpDX;

namespace market_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemsController : ControllerBase
    {
        private readonly DataBaseContext _db;
        private new List<string> _allawedEx = new List<string> { ".jpg", ".png" };
        private long _maxLengh=1048576;

        public ItemsController(DataBaseContext db)
        {
            _db = db;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var items = await _db.Items.Select(I=>I).Where(I=>!I.isDeleted).OrderBy(I => I.Name).ToListAsync();
            return Ok(items);
        }


        [HttpPost]
        public async Task<IActionResult> AddAsync(ItemDto dto)
        {  
            if (dto.InitialPrice==0|| dto.RetailPrice == 0||dto.WholesalePrice==0) {
                    return BadRequest("Prices Cant be 0");
                }
            Item item = new()
            {
                Name = dto.Name,
                InitialPrice = dto.InitialPrice,
                RetailPrice = dto.RetailPrice,
                WholesalePrice = dto.WholesalePrice,
                Quantity = dto.Quantity,
            };
            if (dto.image != null)
            {
                if (!_allawedEx.Contains(Path.GetExtension(dto.image.FileName).ToLower()))
                {
                    return BadRequest("Ex not allowed");
                }
                if(dto.image.Length>_maxLengh)
                {
                    return BadRequest("size not allowed");

                }
                using var DataStream = new MemoryStream();
                await dto.image.CopyToAsync(DataStream);
                item.image = DataStream.ToArray();
            }
            await _db.Items.AddAsync(item);
            _db.SaveChanges();
            return Ok(item);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(int id, [FromBody] ItemDto dto)
        {
            var item=await _db.Items.FindAsync(id);
            if (item == null)
            {
                return NotFound($"No item with ID {id}");
            }
            {
                item.Name = dto.Name;
                item.InitialPrice = dto.InitialPrice;
                item.RetailPrice = dto.RetailPrice;
                item.WholesalePrice = dto.WholesalePrice;
                item.Quantity = dto.Quantity;
            }
            _db.SaveChanges();
            return Ok(item);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var item = await _db.Items.FindAsync(id);
            if (item == null)
            {
                return NotFound($"No item with ID {id}");
            }
            item.isDeleted = true;
            _db.SaveChanges();
            return Ok(item);
        }

       
    }
}
