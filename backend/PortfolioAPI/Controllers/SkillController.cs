using Microsoft.AspNetCore.Mvc;
using PortfolioAPI.Data;
using Microsoft.EntityFrameworkCore;
using PortfolioAPI.Context;
using PortfolioAPI.Models;

namespace PortfolioAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SkillController : Controller
    {
        private readonly PortfolioDbContext dbContext;

        public SkillController(PortfolioDbContext dbContext)
        {
            this.dbContext = dbContext; 
        }

        [HttpGet]
        public async Task<ActionResult> GetSkills()
        {
            return Ok(await dbContext.Skills.ToListAsync());
        }

        [HttpGet]
        [Route("{id:guid}")]
        public async Task<ActionResult> GetSkill([FromRoute] Guid id)
        {
            var skill = await dbContext.Skills.FindAsync(id);

            if (skill == null)
            {
                return NotFound();
            }

            return Ok(skill);
        }

        [HttpPost]

        public async Task<IActionResult> AddSkill(AddSkillRequest AddSkillRequest)
        {
            var skill = new Skill()
            {
                ID = Guid.NewGuid(),
                SkillName = AddSkillRequest.SkillName,
                SkillPrecent = AddSkillRequest.SkillPrecent,
            };

            await dbContext.Skills.AddAsync(skill);
            await dbContext.SaveChangesAsync();

            return Ok(skill);
        }

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateSkill([FromRoute] Guid id, UpdateSkillRequest updateSkillRequest)
        {
            var skill = await dbContext.Skills.FindAsync(id);

            if (skill != null)
            {
               skill.SkillName = updateSkillRequest.SkillName;
                skill.SkillPrecent = updateSkillRequest.SkillPrecent;  


                await dbContext.SaveChangesAsync();

                return Ok(skill);
            }

            return NotFound();
        }


        [HttpDelete]
        [Route("{id:guid}")]

        public async Task<IActionResult> DeleteSkill([FromRoute] Guid id)
        {
            var skill = await dbContext.Skills.FindAsync(id);

            if (skill != null)
            {
                dbContext.Remove(skill);
                dbContext.SaveChangesAsync();
                return Ok(skill);
            }
            return NotFound();

        }
    }
}
