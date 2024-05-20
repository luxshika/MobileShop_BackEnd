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
        private readonly FullStackDbContext _fullStackDbContext;

        public PhoneController(FullStackDbContext fullStackDbContext)
        {
            _fullStackDbContext = fullStackDbContext;
        }

       
        [HttpGet]
        public async Task<IActionResult> GetAllPhone()
        {
            var Phones = await _fullStackDbContext.Phones.ToListAsync();
            return Ok(Phones);
        }

      
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPhoneById(int id)
        {
            var Phone = await _fullStackDbContext.Phones.FindAsync(id);
            if (Phone == null)
            {
                return NotFound();
            }
            return Ok(Phone);
        }

    
        [HttpPost]
        public async Task<IActionResult> AddPhones([FromBody] Phone phonerequest)
        {
            await _fullStackDbContext.Phones.AddAsync(phonerequest);
            await _fullStackDbContext.SaveChangesAsync();
            return Ok(phonerequest);
        }

       
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePhone(int id, [FromBody] Phone phonerequest)
        {
            var existingPhone = await _fullStackDbContext.Phones.FindAsync(id);
            if (existingPhone == null)
            {
                return NotFound();
            }

            existingPhone.productName = phonerequest.productName;
            existingPhone.brandName = phonerequest.brandName;
            existingPhone.price = phonerequest.price;
            existingPhone.quantity = phonerequest.quantity;
            existingPhone.madein = phonerequest.madein;
            existingPhone.feature = phonerequest.feature;
            existingPhone.image = phonerequest.image;

            _fullStackDbContext.Phones.Update(existingPhone);
            await _fullStackDbContext.SaveChangesAsync();

            return Ok(existingPhone);
        }

      
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePhone(int id)
        {
            var existingPhone = await _fullStackDbContext.Phones.FindAsync(id);
            if (existingPhone == null)
            {
                return NotFound();
            }

            _fullStackDbContext.Phones.Remove(existingPhone);
            await _fullStackDbContext.SaveChangesAsync();

            return Ok(existingPhone);
        }
    }
}
