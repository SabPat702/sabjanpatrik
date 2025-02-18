using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dungeon_Valley_Explorer
{
    public class Hero
    {
        public int Id { get; set; }
        public string HeroName { get; set; }
        public int InDEF { get; set; }
        public int DEF { get; set; }
        public int InMDEF { get; set; }
        public int MDEF { get; set; }
        public int InHP { get; set; }
        public int HP { get; set; }
        public int MaxHP { get; set; }
        public Weapon[] Weapons { get; set; }
        public List<BuffDebuff> BuffsDebuffs { get; set; }
        public List<Passive> Passives { get; set; }
        public List<Skill> Skills { get; set; }
        public List<Magic> Magics { get; set; }
        public Armor[] Armors { get; set; }
        public string heroClass { get; set; }
        public Race Race { get; set; }
        public int InMP { get; set; }
        public int MaxMP { get; set; }
        public int MP { get; set; }
        public int InSP { get; set; }
        public int MaxSP { get; set; }
        public int SP { get; set; }
        public bool Guard { get; set; }
        public int Lvl { get; set; }
        public int Exp { get; set; }

        public Hero(string oneLine, List<Passive> passives, List<Skill> skills, List<Magic> magics, List<Race> races, List<Armor> armors, List<Weapon> weapons)
        {
            Weapons = new Weapon[3];
            Armors = new Armor[4];
            BuffsDebuffs = new List<BuffDebuff>();
            Passives = new List<Passive>();
            Skills = new List<Skill>();
            Magics = new List<Magic>();
            Race = new Race();

            string[] linecutter = oneLine.Split('@');
            Id = Convert.ToInt32(linecutter[0]);
            HeroName = linecutter[1];
            InDEF = Convert.ToInt32(linecutter[2]);
            DEF = InDEF;
            InMDEF = Convert.ToInt32(linecutter[3]);
            MDEF = InMDEF;
            InHP = Convert.ToInt32(linecutter[4]);
            MaxHP = InHP;
            HP = InHP;
            InSP = Convert.ToInt32(linecutter[5]);
            MaxSP = InSP;
            SP = InSP;
            InMP = Convert.ToInt32(linecutter[6]);
            MaxMP = InMP;
            MP = InMP;
            Exp = Convert.ToInt32(linecutter[7]);
            Lvl = Convert.ToInt32(linecutter[8]);
            string[] weaponscutter = linecutter[9].Split(',');
            int weaponcounter = 0;
            foreach (string weapon in weaponscutter)
            {
                for (int i = 0; i < weapons.Count(); i++)
                {
                    if (weapon == weapons[i].WeaponName)
                    {
                        Weapons[weaponcounter] = weapons[i];
                        weaponcounter++;
                    }
                }
            }
            string[] armorscutter = linecutter[10].Split(',');
            int armorcounter = 0;
            foreach (string armor in armorscutter)
            {
                for (int i = 0; i < armors.Count(); i++)
                {
                    if (armor == armors[i].ArmorName)
                    {
                        Armors[armorcounter] = armors[i];
                        armorcounter++;
                    }
                }
            }
            heroClass = linecutter[11];
            string[] skillscutter = linecutter[12].Split(',');
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
            string[] magicscutter = linecutter[13].Split(',');
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
            string[] passivescutter = linecutter[14].Split(',');
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
            for (int i = 0; i < races.Count(); i++)
            {
                if (linecutter[15] == races[i].RaceName)
                {
                    Race = races[i];
                }
            }
        }

        public Hero()
        {
            Weapons = new Weapon[3];
            BuffsDebuffs = new List<BuffDebuff>();
            Passives = new List<Passive>();
            Skills = new List<Skill>();
            Magics = new List<Magic>();
            Race = new Race();
            Armors = new Armor[4];
        }
    }
}
