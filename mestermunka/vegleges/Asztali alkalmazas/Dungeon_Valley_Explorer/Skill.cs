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
            inCD = Convert.ToInt32(linecutter[9]);
            CD = 0;
        }

        public Skill()
        {
            SpecialEffects = new List<SpecialEffect>();
        }

        public static List<Hero> BasicStrike(List<Target> targets, DamageSource damageSource, List<Hero> party)
        {
            List<int> damages = DamageCalculator.PreDamageCalculation(targets, damageSource);

            for (int i = 0; i < targets.Count; i++)
            {
                party.Where(x => x.DisplayName == targets[i].TargetName).Select(x => x).First().HP -= damages[i];

                foreach (SpecialEffect specialEffect in targets[i].SpecialEffects)
                {
                    if (specialEffect.Affect.Contains("Recieve Damage"))
                    {
                        switch (specialEffect.SpecialEffectName)
                        {
                            default:
                                break;
                        }
                    }
                }

                foreach (BuffDebuff buffDebuff in targets[i].BuffsDebuffs)
                {
                    if (buffDebuff.Affect.Contains("Recieve Damage"))
                    {
                        switch (buffDebuff.BuffDebuffName)
                        {
                            default:
                                break;
                        }
                    }
                }

                foreach (Passive passive in targets[i].Passives)
                {
                    if (passive.Affect.Contains("Recieve Damage"))
                    {
                        switch (passive.PassiveName)
                        {
                            default:
                                break;
                        }
                    }
                }
            }

            return party;
        }
    }
}
