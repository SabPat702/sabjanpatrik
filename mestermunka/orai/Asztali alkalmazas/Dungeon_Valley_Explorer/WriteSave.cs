using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows;
using Org.BouncyCastle.Crypto;

namespace Dungeon_Valley_Explorer
{
    static class WriteSave
    {
        public static void StartWriteSave(List<string> folders, List<string> files, List<Hero> heroes, List<Hero> party, Dictionary<string, bool> questsCompleted, List<Consumable> consumables, int Gold = 0, int Experience = 0, Dictionary<Dungeon, bool> dungeonsCompleted, Dictionary<Weapon, int> weaponsImproved, Dictionary<Armor, int> armorsImproved, Dictionary<Weapon, bool> weaponsObtained, Dictionary<Armor, bool> armorsObtained, Dictionary<Consumable, bool> consumablesUnlocked)
        {
            try
            {
                StreamWriter streamWriter = new StreamWriter($@"{folders[9]}\{folders.Last()}\{files.Last()}");
                int heroesCounter = 0;
                foreach (Hero hero in heroes)
                {
                    streamWriter.Write(WriteHero(hero, party));
                    if (hero != null)
                    { 
                    
                    }
                }
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message);
            }
        }

        static string WriteHero (Hero hero, List<Hero> party)
        {
            string output = "";

            output += hero.Id;
            output += "@";
            output += hero.HeroName;
            output += "@";
            output += hero.InDEF;
            output += "@";
            output += hero.InMDEF;
            output += "@";
            output += hero.InHP;
            output += "@";
            output += hero.InSP;
            output += "@";
            output += hero.InMP;
            output += "@";
            output += hero.Exp;
            output += "@";
            output += hero.Lvl;
            output += "@";
            int weaponCounter = 0;
            foreach (Weapon weapon in hero.Weapons)
            {
                output += weapon.WeaponName;
                if ( weaponCounter < 2)
                {
                    output += ',';
                }
                weaponCounter++;
            }
            output += "@";
            int armorCounter = 0;
            foreach (Armor armor in hero.Armors)
            {
                output += armor.ArmorName;
                if (armorCounter < 3)
                {
                    output += ',';
                }
                armorCounter++;
            }
            output += "@";
            for (int i = 0; i < hero.Skills.Count; i++)
            {
                output += hero.Skills[i].SkillName;
                if (i < hero.Skills.Count-1)
                {
                    output += ',';
                }
            }
            output += "@";
            for (int i = 0; i < hero.Magics.Count; i++)
            {
                output += hero.Magics[i].MagicName;
                if (i < hero.Magics.Count - 1)
                {
                    output += ',';
                }
            }
            output += "@";
            for (int i = 0; i < hero.Passives.Count; i++)
            {
                output += hero.Passives[i].PassiveName;
                if (i < hero.Passives.Count - 1)
                {
                    output += ',';
                }
            }
            output += "@";
            for (int i = 0; i < hero.BuffsDebuffs.Count; i++)
            {
                output += hero.BuffsDebuffs[i].BuffDebuffName;
                if (i < hero.BuffsDebuffs.Count - 1)
                {
                    output += ',';
                }
            }
            output += "@";
            output += hero.heroClass;
            output += "@";
            output += hero.Race;
            output += "@";

            foreach (Hero partyMember in party)
            {
                if (partyMember.HeroName == hero.HeroName)
                {
                    output += true;
                }
                else
                {
                    output += false;
                }
            }
            
            output += "%";

            return output;
        }
    }
}
