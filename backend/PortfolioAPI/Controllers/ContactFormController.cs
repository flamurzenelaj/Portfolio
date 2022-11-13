using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PortfolioAPI.Context;
using PortfolioAPI.Models.Contact;

namespace PortfolioAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContactFormController : Controller
    {
        private readonly PortfolioDbContext dbContext;

        public ContactFormController(PortfolioDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public async Task<ActionResult> GetContactForms()
        {
            return Ok(await dbContext.ContactForms.ToListAsync());
        }

        [HttpGet]
        [Route("{id:guid}")]
        public async Task<ActionResult> GetContactForm([FromRoute] Guid id)
        {
            var contact = await dbContext.ContactForms.FindAsync(id);

            if (contact == null)
            {
                return NotFound();
            }

            return Ok(contact);
        }

        [HttpPost]

        public async Task<IActionResult> AddContactForm(AddContactFormRequest addContactFormRequest)
        {
            var contact = new ContactForm()
            {
                ID = Guid.NewGuid(),
                Name = addContactFormRequest.Name,
                Email = addContactFormRequest.Email,
                Message = addContactFormRequest.Message,
            };

            await dbContext.ContactForms.AddAsync(contact);
            await dbContext.SaveChangesAsync();

            return Ok(contact);
        }

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateContactForm([FromRoute] Guid id, UpdateContactFormRequest updateContactFormRequest)
        {
            var contact = await dbContext.ContactForms.FindAsync(id);

            if (contact != null)
            {
               contact.Name = updateContactFormRequest.Name;
                contact.Email = updateContactFormRequest.Email;
                contact.Message = updateContactFormRequest.Message;

                await dbContext.SaveChangesAsync();

                return Ok(contact);
            }

            return NotFound();
        }


        [HttpDelete]
        [Route("{id:guid}")]

        public async Task<IActionResult> DeleteContactForm([FromRoute] Guid id)
        {
            var contact = await dbContext.ContactForms.FindAsync(id);

            if (contact != null)
            {
                dbContext.Remove(contact);
                dbContext.SaveChangesAsync();
                return Ok(contact);
            }
            return NotFound();

        }
    }
}
