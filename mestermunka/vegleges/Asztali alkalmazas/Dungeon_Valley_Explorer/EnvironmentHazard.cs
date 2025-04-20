using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dungeon_Valley_Explorer
{
    public class EnvironmentHazard
    {
        public int Id { get; set; }
        public string EnvironmentHazardName { get; set; }
        public int ATK { get; set; }
        public int CritChance { get; set; }
        public double CritDamage { get; set; }
        public string DamageType { get; set; }
        public List<SpecialEffect> SpecialEffects { get; set; }
        public string Dungeon {  get; set; }

        public EnvironmentHazard(string oneLine, List<SpecialEffect> specialEffects)
        {
            SpecialEffects = new List<SpecialEffect>();
            string[] linecutter = oneLine.Split('@');
            Id = Convert.ToInt32(linecutter[0]);
            EnvironmentHazardName = linecutter[1];
            ATK = Convert.ToInt32(linecutter[2]);
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
            Dungeon = linecutter[7];
        }

        public EnvironmentHazard()
        {
            SpecialEffects = new List<SpecialEffect>();
        }
    }
}
