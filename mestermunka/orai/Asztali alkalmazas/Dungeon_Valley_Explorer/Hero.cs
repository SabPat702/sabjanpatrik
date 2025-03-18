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
        public string DisplayName { get; set; }

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
            InMDEF = Convert.ToInt32(linecutter[3]);
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
                for (int i = 0; i < skills.Count(); i++)
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
                for (int i = 0; i < magics.Count(); i++)
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


            foreach (Armor armor in Armors)
            {
                InDEF = InDEF + armor.DEF;
                InMDEF = InMDEF + armor.MDEF;
            }

            DEF = InDEF;
            MDEF = InMDEF;
        }

        public Hero(string oneLine, List<Passive> passives, List<BuffDebuff> buffDebuffs, List<Skill> skills, List<Magic> magics, List<Race> races, List<Armor> armors, List<Weapon> weapons, bool ShortDisplayNames)
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
            InMDEF = Convert.ToInt32(linecutter[3]);
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
            string[] skillscutter = linecutter[11].Split(',');
            foreach (string skill in skillscutter)
            {
                for (int i = 0; i < skills.Count(); i++)
                {
                    if (skill == skills[i].SkillName)
                    {
                        Skills.Add(skills[i]);
                    }
                }
            }
            string[] magicscutter = linecutter[12].Split(',');
            foreach (string magic in magicscutter)
            {
                for (int i = 0; i < magics.Count(); i++)
                {
                    if (magic == magics[i].MagicName)
                    {
                        Magics.Add(magics[i]);
                    }
                }
            }
            string[] passivescutter = linecutter[13].Split(',');
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
            string[] buffdebuffscutter = linecutter[14].Split(',');
            foreach (string buffdebuff in buffdebuffscutter)
            {
                for (int i = 0; i < buffDebuffs.Count(); i++)
                {
                    if (buffdebuff == buffDebuffs[i].BuffDebuffName)
                    {
                        BuffsDebuffs.Add(BuffsDebuffs[i]);
                    }
                }
            }
            heroClass = linecutter[15];
            for (int i = 0; i < races.Count(); i++)
            {
                if (linecutter[16] == races[i].RaceName)
                {
                    Race = races[i];
                }
            }

            foreach (Armor armor in Armors)
            {
                InDEF = InDEF + armor.DEF;
                InMDEF = InMDEF + armor.MDEF;
            }

            DEF = InDEF;
            MDEF = InMDEF;

            if (ShortDisplayNames == true)
            {
                DisplayName = HeroName.Split(' ')[0];
            }
            else
            {
                DisplayName = HeroName;
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

        public static Hero SetFighter(Hero newPlayerHero)
        {
            newPlayerHero.InHP = 20;
            newPlayerHero.HP = newPlayerHero.InHP;
            newPlayerHero.MaxHP = newPlayerHero.InHP;
            newPlayerHero.InDEF = 1;
            newPlayerHero.InMDEF = 0;
            newPlayerHero.InMP = 5;
            newPlayerHero.MaxMP = newPlayerHero.InMP;
            newPlayerHero.MP = newPlayerHero.InMP;
            newPlayerHero.InSP = 12;
            newPlayerHero.MaxSP = newPlayerHero.InSP;
            newPlayerHero.SP = newPlayerHero.InSP;
            newPlayerHero.Exp = 0;
            newPlayerHero.Lvl = 1;
            foreach (Armor armor in Initializer.armors)
            {
                if (armor.ArmorName == "Worn Leather Chestpiece")
                {
                    newPlayerHero.Armors[1] = armor;
                    break;
                }
            }
            foreach (Armor armor in Initializer.armors)
            {
                if (armor.ArmorName == "Old Travel Boots")
                {
                    newPlayerHero.Armors[3] = armor;
                    break;
                }
            }
            foreach (Armor armor in Initializer.armors)
            {
                if (armor.ArmorName == "None")
                {
                    newPlayerHero.Armors[0] = armor;
                    newPlayerHero.Armors[2] = armor;
                    break;
                }
            }
            foreach (Armor armor in newPlayerHero.Armors)
            {
                newPlayerHero.InDEF = newPlayerHero.InDEF + armor.DEF;
                newPlayerHero.InMDEF = newPlayerHero.InMDEF + armor.MDEF;
            }
            foreach (Weapon weapon in Initializer.weapons)
            {
                if (weapon.WeaponName == "Rusty Longsword")
                {
                    newPlayerHero.Weapons[0] = weapon;
                    break;
                }
            }
            foreach (Weapon weapon in Initializer.weapons)
            {
                if (weapon.WeaponName == "Broken Spear")
                {
                    newPlayerHero.Weapons[1] = weapon;
                    break;
                }
            }
            foreach (Weapon weapon in Initializer.weapons)
            {
                if (weapon.WeaponName == "Unarmed")
                {
                    newPlayerHero.Weapons[2] = weapon;
                    break;
                }
            }
            foreach (Skill skill in Initializer.skills)
            {
                if (skill.SkillName == "Powerful Cut")
                {
                    newPlayerHero.Skills.Add(skill);
                    break;
                }
            }
            foreach (Skill skill in Initializer.skills)
            {
                if (skill.SkillName == "Powerful Stab")
                {
                    newPlayerHero.Skills.Add(skill);
                    break;
                }
            }
            return newPlayerHero;
        }

        public static Hero SetHunter(Hero newPlayerHero)
        {
            newPlayerHero.InHP = 14;
            newPlayerHero.HP = newPlayerHero.InHP;
            newPlayerHero.MaxHP = newPlayerHero.InHP;
            newPlayerHero.InDEF = 0;
            newPlayerHero.InMDEF = 0;
            newPlayerHero.InMP = 6;
            newPlayerHero.MaxMP = newPlayerHero.InMP;
            newPlayerHero.MP = newPlayerHero.InMP;
            newPlayerHero.InSP = 14;
            newPlayerHero.MaxSP = newPlayerHero.InSP;
            newPlayerHero.SP = newPlayerHero.InSP;
            newPlayerHero.Exp = 0;
            newPlayerHero.Lvl = 1;
            foreach (Armor armor in Initializer.armors)
            {
                if (armor.ArmorName == "Dirty Cloak")
                {
                    newPlayerHero.Armors[1] = armor;
                    break;
                }
            }
            foreach (Armor armor in Initializer.armors)
            {
                if (armor.ArmorName == "Old Travel Boots")
                {
                    newPlayerHero.Armors[3] = armor;
                    break;
                }
            }
            foreach (Armor armor in Initializer.armors)
            {
                if (armor.ArmorName == "None")
                {
                    newPlayerHero.Armors[0] = armor;
                    newPlayerHero.Armors[2] = armor;
                    break;
                }
            }
            foreach (Armor armor in newPlayerHero.Armors)
            {
                newPlayerHero.InDEF = newPlayerHero.InDEF + armor.DEF;
                newPlayerHero.InMDEF = newPlayerHero.InMDEF + armor.MDEF;
            }
            foreach (Weapon weapon in Initializer.weapons)
            {
                if (weapon.WeaponName == "Short Bow")
                {
                    newPlayerHero.Weapons[0] = weapon;
                    break;
                }
            }
            foreach (Weapon weapon in Initializer.weapons)
            {
                if (weapon.WeaponName == "Dagger")
                {
                    newPlayerHero.Weapons[1] = weapon;
                    break;
                }
            }
            foreach (Weapon weapon in Initializer.weapons)
            {
                if (weapon.WeaponName == "Unarmed")
                {
                    newPlayerHero.Weapons[2] = weapon;
                    break;
                }
            }
            foreach (Skill skill in Initializer.skills)
            {
                if (skill.SkillName == "Powerful Shot")
                {
                    newPlayerHero.Skills.Add(skill);
                    break;
                }
            }
            foreach (Skill skill in Initializer.skills)
            {
                if (skill.SkillName == "Powerful Cut")
                {
                    newPlayerHero.Skills.Add(skill);
                    break;
                }
            }
            return newPlayerHero;
        }

        public static Hero SetWizard(Hero newPlayerHero)
        {
            newPlayerHero.InHP = 10;
            newPlayerHero.HP = newPlayerHero.InHP;
            newPlayerHero.MaxHP = newPlayerHero.InHP;
            newPlayerHero.InDEF = 0;
            newPlayerHero.InMDEF = 2;
            newPlayerHero.InMP = 12;
            newPlayerHero.MaxMP = newPlayerHero.InMP;
            newPlayerHero.MP = newPlayerHero.InMP;
            newPlayerHero.InSP = 6;
            newPlayerHero.MaxSP = newPlayerHero.InSP;
            newPlayerHero.SP = newPlayerHero.InSP;
            newPlayerHero.Exp = 0;
            newPlayerHero.Lvl = 1;
            foreach (Armor armor in Initializer.armors)
            {
                if (armor.ArmorName == "Mana Touched Rag")
                {
                    newPlayerHero.Armors[1] = armor;
                    break;
                }
            }
            foreach (Armor armor in Initializer.armors)
            {
                if (armor.ArmorName == "Old Travel Boots")
                {
                    newPlayerHero.Armors[3] = armor;
                    break;
                }
            }
            foreach (Armor armor in Initializer.armors)
            {
                if (armor.ArmorName == "None")
                {
                    newPlayerHero.Armors[0] = armor;
                    newPlayerHero.Armors[2] = armor;
                    break;
                }
            }
            foreach (Armor armor in newPlayerHero.Armors)
            {
                newPlayerHero.InDEF = newPlayerHero.InDEF + armor.DEF;
                newPlayerHero.InMDEF = newPlayerHero.InMDEF + armor.MDEF;
            }
            foreach (Weapon weapon in Initializer.weapons)
            {
                if (weapon.WeaponName == "Travel Staff")
                {
                    newPlayerHero.Weapons[0] = weapon;
                    break;
                }
            }
            foreach (Weapon weapon in Initializer.weapons)
            {
                if (weapon.WeaponName == "Dagger")
                {
                    newPlayerHero.Weapons[1] = weapon;
                    break;
                }
            }
            foreach (Weapon weapon in Initializer.weapons)
            {
                if (weapon.WeaponName == "Unarmed")
                {
                    newPlayerHero.Weapons[2] = weapon;
                    break;
                }
            }
            foreach (Skill skill in Initializer.skills)
            {
                if (skill.SkillName == "Knock Away")
                {
                    newPlayerHero.Skills.Add(skill);
                    break;
                }
            }
            return newPlayerHero;
        }

        public static Hero SetPaladin(Hero newPlayerHero)
        {
            newPlayerHero.InHP = 25;
            newPlayerHero.HP = newPlayerHero.InHP;
            newPlayerHero.MaxHP = newPlayerHero.InHP;
            newPlayerHero.InDEF = 2;
            newPlayerHero.InMDEF = 2;
            newPlayerHero.InMP = 8;
            newPlayerHero.MaxMP = newPlayerHero.InMP;
            newPlayerHero.MP = newPlayerHero.InMP;
            newPlayerHero.InSP = 10;
            newPlayerHero.MaxSP = newPlayerHero.InSP;
            newPlayerHero.SP = newPlayerHero.InSP;
            newPlayerHero.Exp = 0;
            newPlayerHero.Lvl = 1;
            foreach (Armor armor in Initializer.armors)
            {
                if (armor.ArmorName == "Rusting Chainmail")
                {
                    newPlayerHero.Armors[1] = armor;
                    break;
                }
            }
            foreach (Armor armor in Initializer.armors)
            {
                if (armor.ArmorName == "Worn Leather Knee Pads")
                {
                    newPlayerHero.Armors[2] = armor;
                    break;
                }
            }
            foreach (Armor armor in Initializer.armors)
            {
                if (armor.ArmorName == "Old Travel Boots")
                {
                    newPlayerHero.Armors[3] = armor;
                    break;
                }
            }
            foreach (Armor armor in Initializer.armors)
            {
                if (armor.ArmorName == "None")
                {
                    newPlayerHero.Armors[0] = armor;
                    break;
                }
            }
            foreach (Armor armor in newPlayerHero.Armors)
            {
                newPlayerHero.InDEF = newPlayerHero.InDEF + armor.DEF;
                newPlayerHero.InMDEF = newPlayerHero.InMDEF + armor.MDEF;
            }
            foreach (Weapon weapon in Initializer.weapons)
            {
                if (weapon.WeaponName == "Rusty Longsword")
                {
                    newPlayerHero.Weapons[0] = weapon;
                    break;
                }
            }
            foreach (Weapon weapon in Initializer.weapons)
            {
                if (weapon.WeaponName == "Wooden Shield")
                {
                    newPlayerHero.Weapons[1] = weapon;
                    break;
                }
            }
            foreach (SpecialEffect specialEffect in Initializer.specialEffects)
            {
                if (specialEffect.SpecialEffectName == "Shield")
                {
                    newPlayerHero.InDEF = newPlayerHero.InDEF + newPlayerHero.Weapons[1].ATK;
                    break;
                }
            }
            foreach (Weapon weapon in Initializer.weapons)
            {
                if (weapon.WeaponName == "Unarmed")
                {
                    newPlayerHero.Weapons[2] = weapon;
                    break;
                }
            }
            foreach (Skill skill in Initializer.skills)
            {
                if (skill.SkillName == "Powerful Cut")
                {
                    newPlayerHero.Skills.Add(skill);
                    break;
                }
            }
            foreach (Skill skill in Initializer.skills)
            {
                if (skill.SkillName == "Powerful Stab")
                {
                    newPlayerHero.Skills.Add(skill);
                    break;
                }
            }
            foreach (Skill skill in Initializer.skills)
            {
                if (skill.SkillName == "Taunt")
                {
                    newPlayerHero.Skills.Add(skill);
                    break;
                }
            }
            return newPlayerHero;
        }

        public static Hero SetBountyHunter(Hero newPlayerHero)
        {
            newPlayerHero.InHP = 16;
            newPlayerHero.HP = newPlayerHero.InHP;
            newPlayerHero.MaxHP = newPlayerHero.InHP;
            newPlayerHero.InDEF = 1;
            newPlayerHero.InMDEF = 0;
            newPlayerHero.InMP = 6;
            newPlayerHero.MaxMP = newPlayerHero.InMP;
            newPlayerHero.MP = newPlayerHero.InMP;
            newPlayerHero.InSP = 15;
            newPlayerHero.MaxSP = newPlayerHero.InSP;
            newPlayerHero.SP = newPlayerHero.InSP;
            newPlayerHero.Exp = 0;
            newPlayerHero.Lvl = 1;
            foreach (Armor armor in Initializer.armors)
            {
                if (armor.ArmorName == "Dirty Cloak")
                {
                    newPlayerHero.Armors[1] = armor;
                    break;
                }
            }
            foreach (Armor armor in Initializer.armors)
            {
                if (armor.ArmorName == "Old Travel Boots")
                {
                    newPlayerHero.Armors[3] = armor;
                    break;
                }
            }
            foreach (Armor armor in Initializer.armors)
            {
                if (armor.ArmorName == "None")
                {
                    newPlayerHero.Armors[0] = armor;
                    newPlayerHero.Armors[2] = armor;
                    break;
                }
            }
            foreach (Armor armor in newPlayerHero.Armors)
            {
                newPlayerHero.InDEF = newPlayerHero.InDEF + armor.DEF;
                newPlayerHero.InMDEF = newPlayerHero.InMDEF + armor.MDEF;
            }
            foreach (Weapon weapon in Initializer.weapons)
            {
                if (weapon.WeaponName == "Rusty Longsword")
                {
                    newPlayerHero.Weapons[0] = weapon;
                    break;
                }
            }
            foreach (Weapon weapon in Initializer.weapons)
            {
                if (weapon.WeaponName == "Short Bow")
                {
                    newPlayerHero.Weapons[1] = weapon;
                    break;
                }
            }
            foreach (Weapon weapon in Initializer.weapons)
            {
                if (weapon.WeaponName == "Unarmed")
                {
                    newPlayerHero.Weapons[2] = weapon;
                    break;
                }
            }
            foreach (Skill skill in Initializer.skills)
            {
                if (skill.SkillName == "Powerful Cut")
                {
                    newPlayerHero.Skills.Add(skill);
                    break;
                }
            }
            foreach (Skill skill in Initializer.skills)
            {
                if (skill.SkillName == "Powerful Stab")
                {
                    newPlayerHero.Skills.Add(skill);
                    break;
                }
            }
            foreach (Skill skill in Initializer.skills)
            {
                if (skill.SkillName == "Powerful Shot")
                {
                    newPlayerHero.Skills.Add(skill);
                    break;
                }
            }
            foreach (Skill skill in Initializer.skills)
            {
                if (skill.SkillName == "Mark Bounty")
                {
                    newPlayerHero.Skills.Add(skill);
                    break;
                }
            }
            return newPlayerHero;
        }

        public static Hero SetWarlock(Hero newPlayerHero)
        {
            newPlayerHero.InHP = 10;
            newPlayerHero.HP = newPlayerHero.InHP;
            newPlayerHero.MaxHP = newPlayerHero.InHP;
            newPlayerHero.InDEF = 0;
            newPlayerHero.InMDEF = 2;
            newPlayerHero.InMP = 12;
            newPlayerHero.MaxMP = newPlayerHero.InMP;
            newPlayerHero.MP = newPlayerHero.InMP;
            newPlayerHero.InSP = 6;
            newPlayerHero.MaxSP = newPlayerHero.InSP;
            newPlayerHero.SP = newPlayerHero.InSP;
            newPlayerHero.Exp = 0;
            newPlayerHero.Lvl = 1;
            foreach (Armor armor in Initializer.armors)
            {
                if (armor.ArmorName == "Mana Touched Rag")
                {
                    newPlayerHero.Armors[1] = armor;
                    break;
                }
            }
            foreach (Armor armor in Initializer.armors)
            {
                if (armor.ArmorName == "Old Travel Boots")
                {
                    newPlayerHero.Armors[3] = armor;
                    break;
                }
            }
            foreach (Armor armor in Initializer.armors)
            {
                if (armor.ArmorName == "None")
                {
                    newPlayerHero.Armors[0] = armor;
                    newPlayerHero.Armors[2] = armor;
                    break;
                }
            }
            foreach (Armor armor in newPlayerHero.Armors)
            {
                newPlayerHero.InDEF = newPlayerHero.InDEF + armor.DEF;
                newPlayerHero.InMDEF = newPlayerHero.InMDEF + armor.MDEF;
            }
            foreach (Weapon weapon in Initializer.weapons)
            {
                if (weapon.WeaponName == "Travel Staff")
                {
                    newPlayerHero.Weapons[0] = weapon;
                    break;
                }
            }
            foreach (Weapon weapon in Initializer.weapons)
            {
                if (weapon.WeaponName == "Dagger")
                {
                    newPlayerHero.Weapons[1] = weapon;
                    break;
                }
            }
            foreach (Weapon weapon in Initializer.weapons)
            {
                if (weapon.WeaponName == "Unarmed")
                {
                    newPlayerHero.Weapons[2] = weapon;
                    break;
                }
            }
            return newPlayerHero;
        }
    }
}
