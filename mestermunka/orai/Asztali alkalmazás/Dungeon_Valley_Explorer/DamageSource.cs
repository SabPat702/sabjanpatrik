using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dungeon_Valley_Explorer
{
    public class DamageSource
    {
        public int ATK { get; set; }
        public List<SpecialEffect> SpecialEffect { get; set; }
        public List<BuffDebuff> BuffsDebuffs { get; set; }
        public List<Passive> Passives { get; set; }
        public int CritChance { get; set; }
        public double CritDamage { get; set; }
        public string DamageType { get; set; }
        public int MaxMP { get; set; }
        public int MP { get; set; }
        public int DEF { get; set; }
        public int MDEF { get; set; }
        public int HP { get; set; }
        public int MaxHP { get; set; }

        public DamageSource()
        {

        }
        public DamageSource(EnvironmentHazard envHazard)
        {
            ATK = envHazard.ATK;
            SpecialEffect = envHazard.SpecialEffect;
            BuffsDebuffs = new List<BuffDebuff>();
            Passives = new List<Passive>();
            CritChance = envHazard.CritChance;
            CritDamage = envHazard.CritDamage;
            DamageType = envHazard.DamageType;
            MaxMP = 0;
            MP = 0;
            DEF = 0;
            MDEF = 0;
            HP = 0;
            MaxHP = 0;
        }

        public DamageSource(Hero hero, int chosenWeapon)
        {
            ATK = hero.Weapons[chosenWeapon].ATK;
            SpecialEffect = hero.Weapons[chosenWeapon].SpecialEffect;
            BuffsDebuffs = hero.BuffsDebuffs;
            Passives = hero.Passives;
            CritChance = hero.Weapons[chosenWeapon].CritChance;
            CritDamage = hero.Weapons[chosenWeapon].CritDamage;
            DamageType = hero.Weapons[chosenWeapon].DamageType;
            MaxMP = hero.MaxMP;
            MP = hero.MP;
            DEF = hero.DEF;
            MDEF = hero.MDEF;
            HP = hero.HP;
            MaxHP = hero.MaxHP;
        }

        public DamageSource(Hero hero, int chosenWeapon, Skill skill)
        {
            ATK = hero.Weapons[chosenWeapon].ATK;
            SpecialEffect = hero.Weapons[chosenWeapon].SpecialEffect;
            foreach (var item in skill.SpecialEffect)
            {
                SpecialEffect.Add(item);
            }
            BuffsDebuffs = hero.BuffsDebuffs;
            Passives = hero.Passives;
            CritChance = skill.CritChance;
            CritDamage = skill.CritDamage;
            DamageType = hero.Weapons[chosenWeapon].DamageType;
            MaxMP = hero.MaxMP;
            MP = hero.MP;
            DEF = hero.DEF;
            MDEF = hero.MDEF;
            HP = hero.HP;
            MaxHP = hero.MaxHP;
        }

        public DamageSource(Hero hero, Magic magic)
        {
            ATK = magic.ATK;
            SpecialEffect = magic.SpecialEffect;
            BuffsDebuffs = hero.BuffsDebuffs;
            Passives = hero.Passives;
            CritChance = magic.CritChance;
            CritDamage = magic.CritDamage;
            DamageType = magic.DamageType;
            MaxMP = hero.MaxMP;
            MP = hero.MP;
            DEF = hero.DEF;
            MDEF = hero.MDEF;
            HP = hero.HP;
            MaxHP = hero.MaxHP;
        }

        public DamageSource(Monster monster, Skill skill)
        {
            ATK = monster.ATK;
            SpecialEffect = skill.SpecialEffect;
            BuffsDebuffs = monster.BuffsDebuffs;
            Passives = monster.Passives;
            CritChance = skill.CritChance;
            CritDamage = skill.CritDamage;
            DamageType = skill.DamageType;
            MaxMP = monster.MaxMP;
            MP = monster.MP;
            DEF = monster.DEF;
            MDEF = monster.MDEF;
            HP = monster.HP;
            MaxHP = monster.MaxHP;
        }

        public DamageSource(Monster monster, Magic magic)
        {
            ATK = magic.ATK;
            SpecialEffect = magic.SpecialEffect;
            BuffsDebuffs = monster.BuffsDebuffs;
            Passives = monster.Passives;
            CritChance = magic.CritChance;
            CritDamage = magic.CritDamage;
            DamageType = magic.DamageType;
            MaxMP = monster.MaxMP;
            MP = monster.MP;
            DEF = monster.DEF;
            MDEF = monster.MDEF;
            HP = monster.HP;
            MaxHP = monster.MaxHP;
        }
    }
}
