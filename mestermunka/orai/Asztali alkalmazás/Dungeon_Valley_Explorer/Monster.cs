using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dungeon_Valley_Explorer
{
    public class Monster
    {
        public int Id { get; set; }
        public string MonsterName { get; set; }
        public int InDEF { get; set; }
        public int DEF { get; set; }
        public int InMDEF { get; set; }
        public int MDEF { get; set; }
        public int InHP { get; set; }
        public int HP { get; set; }
        public int MaxHP { get; set; }
        public int ATK { get; set; }
        public List<BuffDebuff> BuffsDebuffs { get; set; }
        public List<Passive> Passives { get; set; }
        public List<Skill> Skills { get; set; }
        public List<Magic> Magics { get; set; }
        public int RaceId { get; set; }
        public int InMP { get; set; }
        public int MaxMP { get; set; }
        public int MP { get; set; }
        public bool Guard { get; set; }

        public Monster(string oneLine)
        {
            MonsterName = string.Empty;
            BuffsDebuffs = new List<BuffDebuff>();
            Passives = new List<Passive>();
            Skills = new List<Skill>();
            Magics = new List<Magic>();
        }

        public Monster()
        {
            BuffsDebuffs = new List<BuffDebuff>();
            Passives = new List<Passive>();
            Skills = new List<Skill>();
            Magics = new List<Magic>();
        }
    }
}
