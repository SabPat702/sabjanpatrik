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
        public List<Passive> Passives { get; set; }
        public Race Race { get; set; }
        public int MaxMP { get; set; }
        public int MP { get; set; }
        public bool Guard { get; set; }

        public Target()
        {
            BuffsDebuffs = new List<BuffDebuff>();
            Passives = new List<Passive>();
        }

        public Target(Hero hero)
        {
            TargetName = hero.HeroName;
            DEF = hero.DEF;
            MDEF = hero.MDEF;
            HP = hero.HP;
            MaxHP = hero.MaxHP;
            BuffsDebuffs = hero.BuffsDebuffs;
            Passives = hero.Passives;
            Race = hero.Race;
            MaxMP = hero.MaxMP;
            MP = hero.MaxMP;
            Guard = hero.Guard;
        }

        public Target(Monster monster)
        {
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
            Guard = monster.Guard;
        }
    }
}
