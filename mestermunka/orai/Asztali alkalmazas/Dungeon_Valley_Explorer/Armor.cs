using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dungeon_Valley_Explorer
{
    public class Armor
    {
        public int Id { get; set; }
        public string ArmorName { get; set; }
        public string Description { get; set; }
        public int DEF {  get; set; }
        public int MDEF { get; set; }
        public List<SpecialEffect> SpecialEffects { get; set; }
        public int Type { get; set; }
        public int Price { get; set; }
        public bool Unique { get; set; }

        public Armor()
        {
            SpecialEffects = new List<SpecialEffect>();
        }

        public Armor(string oneline, List<SpecialEffect> specialEffects)
        {
            SpecialEffects = new List<SpecialEffect>();
            string[] linecutter = oneline.Split('@');
            Id = Convert.ToInt32(linecutter[0]);
            ArmorName = linecutter[1];
            Description = linecutter[2];
            DEF = Convert.ToInt32(linecutter[3]);
            MDEF = Convert.ToInt32(linecutter[4]);
            string[] specialEffectscutter = linecutter[5].Split(',');
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
            Type = Convert.ToInt32(linecutter[6]);
            Price = Convert.ToInt32(linecutter[7]);
            Unique = Convert.ToBoolean(linecutter[8]);
        }
    }
}
