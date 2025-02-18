using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dungeon_Valley_Explorer
{
    public class Magic
    {
        public int Id { get; set; }
        public int ATK { get; set; }
        public string MagicName { get; set; }
        public string Description { get; set; }
        public string DamageType { get; set; }
        public List<SpecialEffect> SpecialEffects { get; set; }
        public int CritChance { get; set; }
        public double CritDamage { get; set; }
        public string Range { get; set; }
        public int MPCost { get; set; }
        public int inCD { get; set; }
        public int CD {  get; set; }

        public Magic(string oneLine, List<SpecialEffect> specialEffects)
        {
            SpecialEffects = new List<SpecialEffect>();
            string[] linecutter = oneLine.Split('@');
            Id = Convert.ToInt32(linecutter[0]);
            MagicName = linecutter[1];
            Description = linecutter[2];
            ATK = Convert.ToInt32(linecutter[3]);
            DamageType = linecutter[4];
            CritChance = Convert.ToInt32(linecutter[5]);
            CritDamage = Convert.ToDouble(linecutter[6]);
            string[] specialEffectscutter = linecutter[7].Split(',');
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
            Range = linecutter[8];
            MPCost = Convert.ToInt32(linecutter[9]);
            CD = Convert.ToInt32(linecutter[10]);
        }

        public Magic()
        {
            SpecialEffects = new List<SpecialEffect>();
        }
    }
}
