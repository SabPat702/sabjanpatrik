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
        public List<SpecialEffect> SpecialEffects { get; set; }
        public int CritChance { get; set; }
        public double CritDamage { get; set; }
        public string DamageType { get; set; }
        public string Range { get; set; }
        public string SkillCompatibility { get; set; }
        public int Price { get; set; }
        public bool Unique { get; set; }


        public Weapon(string oneLine, List<SpecialEffect> specialEffects)
        {
            SpecialEffects = new List<SpecialEffect>();
            string[] linecutter = oneLine.Split('@');
            Id = Convert.ToInt32(linecutter[0]);
            WeaponName = linecutter[1];
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
            SkillCompatibility = linecutter[9];
            Price = Convert.ToInt32(linecutter[10]);
            Unique = Convert.ToBoolean(linecutter[11]);
        }

        public Weapon()
        {
            SpecialEffects = new List<SpecialEffect>();
        }

        public static Hero EquipWeaponCheck(Hero selectedHero, int weaponIndex)
        {
            for (int i = 0; i < selectedHero.Weapons[weaponIndex].SpecialEffects.Count; i++)
            {
                if (selectedHero.Weapons[weaponIndex].SpecialEffects[i].Affect.Contains("Weapon Equip"))
                {
                    switch (selectedHero.Weapons[weaponIndex].SpecialEffects[i].SpecialEffectName)
                    {
                        case "Shield":
                            selectedHero.DEF += selectedHero.Weapons[weaponIndex].ATK;
                            break;
                        default:
                            break;
                    }
                }
            }
            return selectedHero;
        }
    }
}
