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
        public List<SpecialEffect> SpecialEffects { get; set; }
        public List<BuffDebuff> BuffsDebuffs { get; set; }
        public List<Passive> Passives { get; set; }
        public int CritChance { get; set; }
        public double CritDamage { get; set; }
        public string DamageType { get; set; }
        public int MaxMP { get; set; }
        public int MP { get; set; }
        public int MaxSP { get; set; }
        public int SP { get; set; }
        public int DEF { get; set; }
        public int MDEF { get; set; }
        public int HP { get; set; }
        public int MaxHP { get; set; }

        public DamageSource()
        {
            SpecialEffects = new List<SpecialEffect>();
            BuffsDebuffs = new List<BuffDebuff>();
            Passives = new List<Passive>();
        }
        public DamageSource(EnvironmentHazard envHazard)
        {
            SpecialEffects = new List<SpecialEffect>();
            BuffsDebuffs = new List<BuffDebuff>();
            Passives = new List<Passive>();
            ATK = envHazard.ATK;
            SpecialEffects = envHazard.SpecialEffects;
            CritChance = envHazard.CritChance;
            CritDamage = envHazard.CritDamage;
            DamageType = envHazard.DamageType;
            MaxMP = 0;
            MP = 0;
            MaxSP = 0;
            SP = 0;
            DEF = 0;
            MDEF = 0;
            HP = 0;
            MaxHP = 0;
        }

        public DamageSource(Hero hero, int chosenWeapon)
        {
            SpecialEffects = new List<SpecialEffect>();
            BuffsDebuffs = new List<BuffDebuff>();
            Passives = new List<Passive>();
            ATK = hero.Weapons[chosenWeapon].ATK;
            SpecialEffects = hero.Weapons[chosenWeapon].SpecialEffects;
            foreach (var armors in hero.Armors)
            {
                foreach (var specialEffect in armors.SpecialEffects)
                {
                    SpecialEffects.Add(specialEffect);
                }
            }
            BuffsDebuffs = hero.BuffsDebuffs;
            Passives = hero.Passives;
            CritChance = hero.Weapons[chosenWeapon].CritChance;
            CritDamage = hero.Weapons[chosenWeapon].CritDamage;
            DamageType = hero.Weapons[chosenWeapon].DamageType;
            MaxMP = hero.MaxMP;
            MP = hero.MP;
            MaxSP = hero.MaxSP;
            SP = hero.SP;
            DEF = hero.DEF;
            MDEF = hero.MDEF;
            HP = hero.HP;
            MaxHP = hero.MaxHP;
        }

        public DamageSource(Hero hero, int chosenWeapon, Skill skill)
        {
            SpecialEffects = new List<SpecialEffect>();
            BuffsDebuffs = new List<BuffDebuff>();
            Passives = new List<Passive>();
            ATK = hero.Weapons[chosenWeapon].ATK;
            SpecialEffects = hero.Weapons[chosenWeapon].SpecialEffects;
            foreach (var specialEffect in skill.SpecialEffects)
            {
                SpecialEffects.Add(specialEffect);
            }
            foreach (var armors in hero.Armors)
            {
                foreach (var specialEffect in armors.SpecialEffects)
                {
                    SpecialEffects.Add(specialEffect);
                }
            }
            BuffsDebuffs = hero.BuffsDebuffs;
            Passives = hero.Passives;
            CritChance = skill.CritChance;
            CritDamage = skill.CritDamage;
            DamageType = hero.Weapons[chosenWeapon].DamageType;
            MaxMP = hero.MaxMP;
            MP = hero.MP;
            MaxSP = hero.MaxSP;
            SP = hero.SP;
            DEF = hero.DEF;
            MDEF = hero.MDEF;
            HP = hero.HP;
            MaxHP = hero.MaxHP;
        }

        public DamageSource(Hero hero, Magic magic)
        {
            SpecialEffects = new List<SpecialEffect>();
            BuffsDebuffs = new List<BuffDebuff>();
            Passives = new List<Passive>();
            ATK = magic.ATK;
            SpecialEffects = magic.SpecialEffects;
            foreach (var armors in hero.Armors)
            {
                foreach (var specialEffect in armors.SpecialEffects)
                {
                    SpecialEffects.Add(specialEffect);
                }
            }
            BuffsDebuffs = hero.BuffsDebuffs;
            Passives = hero.Passives;
            CritChance = magic.CritChance;
            CritDamage = magic.CritDamage;
            DamageType = magic.DamageType;
            MaxMP = hero.MaxMP;
            MP = hero.MP;
            MaxSP = hero.MaxSP;
            SP = hero.SP;
            DEF = hero.DEF;
            MDEF = hero.MDEF;
            HP = hero.HP;
            MaxHP = hero.MaxHP;
        }

        public DamageSource(Monster monster, Skill skill)
        {
            SpecialEffects = new List<SpecialEffect>();
            BuffsDebuffs = new List<BuffDebuff>();
            Passives = new List<Passive>();
            ATK = monster.ATK;
            SpecialEffects = skill.SpecialEffects;
            BuffsDebuffs = monster.BuffsDebuffs;
            Passives = monster.Passives;
            CritChance = skill.CritChance;
            CritDamage = skill.CritDamage;
            DamageType = skill.DamageType;
            MaxMP = monster.MaxMP;
            MP = monster.MP;
            MaxSP = monster.MaxSP;
            SP = monster.SP;
            DEF = monster.DEF;
            MDEF = monster.MDEF;
            HP = monster.HP;
            MaxHP = monster.MaxHP;
        }

        public DamageSource(Monster monster, Magic magic)
        {
            SpecialEffects = new List<SpecialEffect>();
            BuffsDebuffs = new List<BuffDebuff>();
            Passives = new List<Passive>();
            ATK = magic.ATK;
            SpecialEffects = magic.SpecialEffects;
            BuffsDebuffs = monster.BuffsDebuffs;
            Passives = monster.Passives;
            CritChance = magic.CritChance;
            CritDamage = magic.CritDamage;
            DamageType = magic.DamageType;
            MaxMP = monster.MaxMP;
            MP = monster.MP;
            MaxSP = monster.MaxSP;
            SP = monster.SP;
            DEF = monster.DEF;
            MDEF = monster.MDEF;
            HP = monster.HP;
            MaxHP = monster.MaxHP;
        }
    }
}
