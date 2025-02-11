using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dungeon_Valley_Explorer
{
    public class Weapon
    {
        public int Id { get; set; }
        public string WeaponName { get; set; }
        public int ATK { get; set; }
        public string Description { get; set; }
        public List<SpecialEffect> SpecialEffect { get; set; }
        public int CritChance { get; set; }
        public double CritDamage { get; set; }
        public string DamageType { get; set; }
        public string Range { get; set; }
        public string SkillCompatibility { get; set; }
        public int Price { get; set; }


        public Weapon(string oneLine)
        {
            Description = string.Empty;
            WeaponName = string.Empty;
            SpecialEffect = new List<SpecialEffect>();
            DamageType = string.Empty;
            Range = string.Empty;
            Price = new int();
        }

        public Weapon()
        {
            SpecialEffect = new List<SpecialEffect>();
        }
    }
}
