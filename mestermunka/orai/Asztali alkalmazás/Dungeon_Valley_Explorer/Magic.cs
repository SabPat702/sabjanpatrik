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
        public List<SpecialEffect> SpecialEffect { get; set; }
        public int CritChance { get; set; }
        public double CritDamage { get; set; }
        public string Range { get; set; }

        public Magic(string oneLine)
        {
            MagicName = string.Empty;
            Description = string.Empty;
            DamageType = string.Empty;
            SpecialEffect = new List<SpecialEffect>();
            Range = string.Empty;
        }

        public Magic()
        {
            SpecialEffect = new List<SpecialEffect>();
        }
    }
}
