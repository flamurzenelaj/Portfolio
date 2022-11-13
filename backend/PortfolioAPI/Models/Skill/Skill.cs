using System.ComponentModel.DataAnnotations;

namespace PortfolioAPI.Data
{
    public class Skill
    {
        public Guid ID { get; set; }
        public string SkillName { get; set; }
        public int SkillPrecent { get; set; }
    }
}
