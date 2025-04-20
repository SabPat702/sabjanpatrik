using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Dungeon_Valley_Explorer
{
    public class Target
    {
        public string TargetName { get; set; }
        public int DEF { get; set; }
        public int MDEF { get; set; }
        public int HP { get; set; }
        public int MaxHP { get; set; }
        public List<BuffDebuff> BuffsDebuffs { get; set; }
        public List<SpecialEffect> SpecialEffects { get; set; }
        public List<Passive> Passives { get; set; }
        public Race Race { get; set; }
        public int MaxMP { get; set; }
        public int MP { get; set; }
        public int MaxSP { get; set; }
        public int SP { get; set; }
        public bool Guard { get; set; }

        public Target()
        {
            BuffsDebuffs = new List<BuffDebuff>();
            SpecialEffects = new List<SpecialEffect>();
            Passives = new List<Passive>();
        }

        public Target(Hero hero)
        {
            BuffsDebuffs = new List<BuffDebuff>();
            SpecialEffects = new List<SpecialEffect>();
            Passives = new List<Passive>();
            TargetName = hero.DisplayName;
            DEF = hero.DEF;
            MDEF = hero.MDEF;
            HP = hero.HP;
            MaxHP = hero.MaxHP;
            BuffsDebuffs = hero.BuffsDebuffs;
            foreach (var armors in hero.Armors)
            {
                foreach (var specialEffect in armors.SpecialEffects)
                {
                    SpecialEffects.Add(specialEffect);
                }
            }
            Passives = hero.Passives;
            Race = hero.Race;
            MaxMP = hero.MaxMP;
            MP = hero.MaxMP;
            MaxSP = hero.MaxSP;
            SP = hero.SP;
            Guard = hero.Guard;
        }

        public Target(Monster monster)
        {
            BuffsDebuffs = new List<BuffDebuff>();
            SpecialEffects = new List<SpecialEffect>();
            Passives = new List<Passive>();
            TargetName = monster.MonsterName;
            DEF = monster.DEF;
            MDEF = monster.MDEF;
            HP = monster.HP;
            MaxHP = monster.MaxHP;
            BuffsDebuffs = monster.BuffsDebuffs;
            Passives = monster.Passives;
            Race = monster.Race;
            MaxMP = monster.MaxMP;
            MP = monster.MaxMP;
            MaxSP = monster.MaxSP;
            SP = monster.SP;
            Guard = monster.Guard;
        }
    }
}
