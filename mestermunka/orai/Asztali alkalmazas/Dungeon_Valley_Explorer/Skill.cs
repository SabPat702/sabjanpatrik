using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dungeon_Valley_Explorer
{
    public class Skill
    {
        public int Id { get; set; }
        public string SkillName { get; set; }
        public string Description { get; set; }
        public string DamageType { get; set; }
        public List<SpecialEffect> SpecialEffect { get; set; }
        public int CritChance { get; set; }
        public double CritDamage { get; set; }
        public string Range { get; set; }

        public Skill(string oneLine)
        {
            SkillName = string.Empty;
            Description = string.Empty;
            DamageType = string.Empty;
            SpecialEffect = new List<SpecialEffect>();
            Range = string.Empty;
        }

        public Skill()
        {
            SpecialEffect = new List<SpecialEffect>();
        }
    }
}
