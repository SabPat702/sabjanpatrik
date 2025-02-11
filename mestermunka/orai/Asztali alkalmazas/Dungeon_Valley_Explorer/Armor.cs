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

        public Armor()
        {
            SpecialEffects = new List<SpecialEffect>();
        }

        public Armor(string oneline)
        {
            SpecialEffects = new List<SpecialEffect>();
        }
    }
}
