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
        public Race Race { get; set; }
        public string Ai { get; set; }
        public string Dungeon { get; set; }
        public int InMP { get; set; }
        public int MaxMP { get; set; }
        public int MP { get; set; }
        public int InSP { get; set; }
        public int MaxSP { get; set; }
        public int SP { get; set; }
        public bool Guard { get; set; }

        public Monster(string oneLine, List<Passive> passives, List<Skill> skills, List<Magic> magics, List<Race> races)
        {
            string[] linecutter = oneLine.Split('@');
            Id = Convert.ToInt32(linecutter[0]);
            MonsterName = linecutter[1];
            InHP = Convert.ToInt32(linecutter[2]);
            MaxHP = InHP;
            HP = InHP;
            InDEF = Convert.ToInt32(linecutter[3]);
            DEF = InDEF;
            InMDEF = Convert.ToInt32(linecutter[4]);
            MDEF = InMDEF;
            ATK = Convert.ToInt32(linecutter[5]);
            InSP = Convert.ToInt32(linecutter[6]);
            MaxSP = InSP;
            SP = InSP;
            InMP = Convert.ToInt32(linecutter[7]);
            MaxMP = InMP;
            MP = InMP;
            string[] passivescutter = linecutter[8].Split(',');
            foreach (string passive in passivescutter)
            {
                for (int i = 0; i < passives.Count(); i++)
                {
                    if (passive == passives[i].PassiveName)
                    {
                        Passives.Add(passives[i]);
                    }
                }
            }
            string[] skillscutter = linecutter[9].Split(',');
            foreach (string skill in skillscutter)
            {
                for (int i = 0; i < skillscutter.Count(); i++)
                {
                    if (skill == skills[i].SkillName)
                    {
                        Skills.Add(skills[i]);
                    }
                }
            }
            string[] magicscutter = linecutter[10].Split(',');
            foreach (string magic in magicscutter)
            {
                for (int i = 0; i < magicscutter.Count(); i++)
                {
                    if (magic == magics[i].MagicName)
                    {
                        Magics.Add(magics[i]);
                    }
                }
            }
            for (int i = 0; i < races.Count(); i++)
            {
                if (linecutter[11] == races[i].RaceName)
                {
                    Race = races[i];
                }
            }
            Ai = linecutter[12];
            Dungeon = linecutter[13];

            BuffsDebuffs = new List<BuffDebuff>();
        }

        public Monster()
        {
            BuffsDebuffs = new List<BuffDebuff>();
            Passives = new List<Passive>();
            Skills = new List<Skill>();
            Magics = new List<Magic>();
            Race = new Race();
        }
    }
}
