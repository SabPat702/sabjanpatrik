using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dungeon_Valley_Explorer
{
    public class Consumable
    {
        public int Id { get; set; }
        public string ConsumableName { get; set; }
        public string Description { get; set; }
        public List<SpecialEffect> SpecialEffects { get; set; }
        public int Price { get; set; }

        public Consumable()
        { 
            SpecialEffects = new List<SpecialEffect>();
        }

        public Consumable(string oneline, List<SpecialEffect> specialEffects)
        {
            string[] linecutter = oneline.Split('@');
            Id = Convert.ToInt32(linecutter[0]);
            ConsumableName = linecutter[1];
            Description = linecutter[2];
            string[] specialEffectscutter = linecutter[3].Split(',');
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
            Price = Convert.ToInt32(linecutter[4]);
        }
    }
}
