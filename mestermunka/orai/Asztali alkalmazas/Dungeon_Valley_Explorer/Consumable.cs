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

        public Consumable(string oneline)
        {
            SpecialEffects = new List<SpecialEffect>();
        }
    }
}
