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

        // Get all phones
        [HttpGet]
        public async Task<IActionResult> GetAllPhone()
        {
            var phones = await _fullStackDbContext.phones.ToListAsync();
            return Ok(phones);
        }

        // Get a single phone by id
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPhoneById(int id)
        {
            var phone = await _fullStackDbContext.phones.FindAsync(id);
            if (phone == null)
            {
                return NotFound();
            }
            return Ok(phone);
        }

        // Add a new phone
        [HttpPost]
        public async Task<IActionResult> AddPhones([FromBody] phone phonerequest)
        {
            await _fullStackDbContext.phones.AddAsync(phonerequest);
            await _fullStackDbContext.SaveChangesAsync();
            return Ok(phonerequest);
        }

        // Update an existing phone
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePhone(int id, [FromBody] phone phonerequest)
        {
            var existingPhone = await _fullStackDbContext.phones.FindAsync(id);
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

            _fullStackDbContext.phones.Update(existingPhone);
            await _fullStackDbContext.SaveChangesAsync();

            return Ok(existingPhone);
        }

        // Delete a phone by id
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePhone(int id)
        {
            var existingPhone = await _fullStackDbContext.phones.FindAsync(id);
            if (existingPhone == null)
            {
                return NotFound();
            }

            _fullStackDbContext.phones.Remove(existingPhone);
            await _fullStackDbContext.SaveChangesAsync();

            return Ok(existingPhone);
        }
    }
}
