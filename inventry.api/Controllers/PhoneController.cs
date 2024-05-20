using inventry.api.data;
using inventry.api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace inventry.api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PhoneController : Controller
    {
        private readonly BackEndDbContext _BackEndDbContext;

        public PhoneController(BackEndDbContext BackEndDbContext)
        {
            _BackEndDbContext = BackEndDbContext;
        }

       
        [HttpGet]
        public async Task<IActionResult> GetAllPhone()
        {
            var Phones = await _BackEndDbContext.Phones.ToListAsync();
            return Ok(Phones);
        }

      
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPhoneById(int id)
        {
            var Phone = await _BackEndDbContext.Phones.FindAsync(id);
            if (Phone == null)
            {
                return NotFound();
            }
            return Ok(Phone);
        }

    
        [HttpPost]
        public async Task<IActionResult> AddPhones([FromBody] Phone phonerequest)
        {
            await _BackEndDbContext.Phones.AddAsync(phonerequest);
            await _BackEndDbContext.SaveChangesAsync();
            return Ok(phonerequest);
        }

       
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePhone(int id, [FromBody] Phone phonerequest)
        {
            var existingPhone = await _BackEndDbContext.Phones.FindAsync(id);
            if (existingPhone == null)
            {
                return NotFound();
            }

            existingPhone.ProductName = phonerequest.ProductName;
            existingPhone.BrandName = phonerequest.BrandName;
            existingPhone.Price = phonerequest.Price;
            existingPhone.Quantity = phonerequest.Quantity;
            existingPhone.Madein = phonerequest.Madein;
            existingPhone.Feature = phonerequest.Feature;
            existingPhone.Image = phonerequest.Image;

            _BackEndDbContext.Phones.Update(existingPhone);
            await _BackEndDbContext.SaveChangesAsync();

            return Ok(existingPhone);
        }

      
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePhone(int id)
        {
            var existingPhone = await _BackEndDbContext.Phones.FindAsync(id);
            if (existingPhone == null)
            {
                return NotFound();
            }

            _BackEndDbContext.Phones.Remove(existingPhone);
            await _BackEndDbContext.SaveChangesAsync();

            return Ok(existingPhone);
        }
    }
}
