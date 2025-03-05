using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows;
using Org.BouncyCastle.Crypto;
using K4os.Compression.LZ4.Streams.Abstractions;

namespace Dungeon_Valley_Explorer
{
    static class WriteSave
    {
        public static void StartWriteSave(List<string> folders, List<string> files, List<Hero> heroes, List<Hero> party, Dictionary<string, bool> questsCompleted, Dictionary<string, int> consumables, int Gold, int Experience, Dictionary<string, bool> dungeonsCompleted, Dictionary<string, int> weaponsImproved, Dictionary<string, int> armorsImproved, Dictionary<string, bool> weaponsObtained, Dictionary<string, bool> armorsObtained, Dictionary<string, bool> consumablesUnlocked)
        {
            try
            {
                StreamWriter streamWriter = new StreamWriter($@"{folders[9]}\{folders.Last()}\{files.Last()}");
                int heroesCounter = 0;
                foreach (Hero hero in heroes)
                {
                    streamWriter.Write(WriteHero(hero, party));
                    if (heroesCounter < heroes.Count - 1)
                    {
                        streamWriter.Write("%");
                    }
                    heroesCounter++;
                }
                streamWriter.Write("$");
                int consumablesCounter = 0;
                foreach (var consumable in consumables)
                {
                    streamWriter.Write(consumable.Key);
                    streamWriter.Write("@");
                    streamWriter.Write(consumable.Value);

                    if (consumablesCounter < consumables.Count - 1)
                    {
                        streamWriter.Write("%");
                    }

                    consumablesCounter++;
                }
                streamWriter.Write("$");
                streamWriter.Write(Gold);
                streamWriter.Write("$");
                streamWriter.Write(Experience);
                streamWriter.Write("$");
                int questsCounter = 0;
                foreach (var quest in questsCompleted)
                {
                    streamWriter.Write(quest.Value);

                    if (questsCounter < questsCompleted.Count - 1)
                    {
                        streamWriter.Write("%");
                    }

                    questsCounter++;
                }
                streamWriter.Write("$");
                int dungeonsCounter = 0;
                foreach (var dungeon in dungeonsCompleted)
                {
                    streamWriter.Write(dungeon.Value);

                    if (dungeonsCounter < dungeonsCompleted.Count - 1)
                    {
                        streamWriter.Write("%");
                    }

                    dungeonsCounter++;
                }
                streamWriter.Write("$");
                int weaponsImprovementCounter = 0;
                foreach (var weaponImprovement in weaponsImproved)
                {
                    streamWriter.Write(weaponImprovement.Value);

                    if (weaponsImprovementCounter < weaponsImproved.Count - 1)
                    {
                        streamWriter.Write("%");
                    }

                    weaponsImprovementCounter++;
                }
                streamWriter.Write("$");
                int armorsImprovementCounter = 0;
                foreach (var armorImprovement in armorsImproved)
                {
                    streamWriter.Write(armorImprovement.Value);

                    if (armorsImprovementCounter < armorsImproved.Count - 1)
                    {
                        streamWriter.Write("%");
                    }

                    armorsImprovementCounter++;
                }
                streamWriter.Write("$");
                int weaponsObtainedCounter = 0;
                foreach (var weaponObtained in weaponsObtained)
                {
                    streamWriter.Write(weaponObtained.Value);

                    if (weaponsObtainedCounter < weaponsObtained.Count - 1)
                    {
                        streamWriter.Write("%");
                    }

                    weaponsObtainedCounter++;
                }
                streamWriter.Write("$");
                int armorsObtainedCounter = 0;
                foreach (var armorObtained in armorsObtained)
                {
                    streamWriter.Write(armorObtained.Value);

                    if (armorsObtainedCounter < armorsObtained.Count - 1)
                    {
                        streamWriter.Write("%");
                    }

                    armorsObtainedCounter++;
                }
                streamWriter.Write("$");
                int consumablesUnlockedCounter = 0;
                foreach (var consumableUnlocked in consumablesUnlocked)
                {
                    streamWriter.Write(consumableUnlocked.Value);

                    if (consumablesUnlockedCounter < consumablesUnlocked.Count - 1)
                    {
                        streamWriter.Write("%");
                    }

                    consumablesUnlockedCounter++;
                }
                streamWriter.Close();
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
            output += hero.Race.RaceName;
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

            return output;
        }

        public static string MakeString(List<string> folders, List<string> files, List<Hero> heroes, List<Hero> party, Dictionary<string, bool> questsCompleted, Dictionary<string, int> consumables, int Gold, int Experience, Dictionary<string, bool> dungeonsCompleted, Dictionary<string, int> weaponsImproved, Dictionary<string, int> armorsImproved, Dictionary<string, bool> weaponsObtained, Dictionary<string, bool> armorsObtained, Dictionary<string, bool> consumablesUnlocked)
        {
            string output = "";
            int heroesCounter = 0;
            foreach (Hero hero in heroes)
            {
                output += WriteHero(hero, party);
                if (heroesCounter < heroes.Count - 1)
                {
                    output += "%";
                }
                heroesCounter++;
            }
            output += "$";
            int consumablesCounter = 0;
            foreach (var consumable in consumables)
            {
                output += consumable.Key;
                output += "@";
                output += consumable.Value;

                if (consumablesCounter < consumables.Count - 1)
                {
                    output += "%";
                }

                consumablesCounter++;
            }
            output += "$";
            output += Gold;
            output += "$";
            output += Experience;
            output += "$";
            int questsCounter = 0;
            foreach (var quest in questsCompleted)
            {
                output += quest.Value;

                if (questsCounter < questsCompleted.Count - 1)
                {
                    output += "%";
                }

                questsCounter++;
            }
            output += "$";
            int dungeonsCounter = 0;
            foreach (var dungeon in dungeonsCompleted)
            {
                output += dungeon.Value;

                if (dungeonsCounter < dungeonsCompleted.Count - 1)
                {
                    output += "%";
                }

                dungeonsCounter++;
            }
            output += "$";
            int weaponsImprovementCounter = 0;
            foreach (var weaponImprovement in weaponsImproved)
            {
                output += weaponImprovement.Value;

                if (weaponsImprovementCounter < weaponsImproved.Count - 1)
                {
                    output += "%";
                }

                weaponsImprovementCounter++;
            }
            output += "$";
            int armorsImprovementCounter = 0;
            foreach (var armorImprovement in armorsImproved)
            {
                output += armorImprovement.Value;

                if (armorsImprovementCounter < armorsImproved.Count - 1)
                {
                    output += "%";
                }

                armorsImprovementCounter++;
            }
            output += "$";
            int weaponsObtainedCounter = 0;
            foreach (var weaponObtained in weaponsObtained)
            {
                output += weaponObtained.Value;

                if (weaponsObtainedCounter < weaponsObtained.Count - 1)
                {
                    output += "%";
                }

                weaponsObtainedCounter++;
            }
            output += "$";
            int armorsObtainedCounter = 0;
            foreach (var armorObtained in armorsObtained)
            {
                output += armorObtained.Value;

                if (armorsObtainedCounter < armorsObtained.Count - 1)
                {
                    output += "%";
                }

                armorsObtainedCounter++;
            }
            output += "$";
            int consumablesUnlockedCounter = 0;
            foreach (var consumableUnlocked in consumablesUnlocked)
            {
                output += consumableUnlocked.Value;

                if (consumablesUnlockedCounter < consumablesUnlocked.Count - 1)
                {
                    output += "%";
                }

                consumablesUnlockedCounter++;
            }
            return output;
        }
    }
}
