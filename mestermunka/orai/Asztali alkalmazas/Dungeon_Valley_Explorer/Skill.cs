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
        public List<SpecialEffect> SpecialEffects { get; set; }
        public int CritChance { get; set; }
        public double CritDamage { get; set; }
        public string Range { get; set; }
        public int SPCost { get; set; }
        public int inCD { get; set; }
        public int CD {  get; set; }

        public Skill(string oneLine, List<SpecialEffect> specialEffects)
        {
            SpecialEffects = new List<SpecialEffect>();
            string[] linecutter = oneLine.Split('@');
            Id = Convert.ToInt32(linecutter[0]);
            SkillName = linecutter[1];
            Description = linecutter[2];
            DamageType = linecutter[3];
            CritChance = Convert.ToInt32(linecutter[4]);
            CritDamage = Convert.ToDouble(linecutter[5]);
            string[] specialEffectscutter = linecutter[6].Split(',');
            foreach (string specialEffect in specialEffectscutter)
            {
                for (int i = 0; i < specialEffects.Count(); i++)
                {
                    if (specialEffect == specialEffects[i].SpecialEffectName)
                    {
                        SpecialEffects.Add(specialEffects[i]);
                    }
                }
            }
            Range = linecutter[7];
            SPCost = Convert.ToInt32(linecutter[8]);
            CD = Convert.ToInt32(linecutter[9]);
        }

        public Skill()
        {
            SpecialEffects = new List<SpecialEffect>();
        }

        public static List<Hero> ExplorationSkills(Hero hero, Skill skill, List<Hero> party, Hero target, bool targeting)
        {
            if (targeting)
            {
                //party wide
                switch (skill.SkillName)
                {
                    default:
                        break;
                }
                party.Where(x => x.DisplayName == hero.DisplayName).Select(x => x).First().SP -= skill.SPCost;
            }
            else
            {
                //single
                switch (skill.SkillName)
                {
                    default:
                        break;
                }
                party.Where(x => x.DisplayName == hero.DisplayName).Select(x => x).First().SP -= skill.SPCost;
            }

            return party;
        }
    }
}
