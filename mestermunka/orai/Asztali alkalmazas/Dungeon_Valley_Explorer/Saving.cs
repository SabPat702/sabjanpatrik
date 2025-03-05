using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows;
using Org.BouncyCastle.Crypto;
using K4os.Compression.LZ4.Streams.Abstractions;
using MySql.Data.MySqlClient;
using Org.BouncyCastle.Asn1.X509;
using System.Windows.Documents;

namespace Dungeon_Valley_Explorer
{
    static class Saving
    {
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

        public static void InsertSave(List<string> folders, List<string> files, List<Hero> heroes, List<Hero> party, Dictionary<string, bool> questsCompleted, Dictionary<string, int> consumables, int Gold, int Experience, Dictionary<string, bool> dungeonsCompleted, Dictionary<string, int> weaponsImproved, Dictionary<string, int> armorsImproved, Dictionary<string, bool> weaponsObtained, Dictionary<string, bool> armorsObtained, Dictionary<string, bool> consumablesUnlocked,MySqlConnection mySqlConnection, List<Hero> npcs)
        {
            if (NewHero(mySqlConnection, folders, heroes, npcs) == false)
            {

            }
            string command = $"Insert into sabpat702.save_game (HeroId, SaveData, SaveName) Values ('1','{MakeString(folders, files, heroes, party, questsCompleted, consumables, Gold, Experience, dungeonsCompleted, weaponsImproved, armorsImproved, weaponsObtained, armorsObtained, consumablesUnlocked)}','{files.Last()}')";
            MySqlCommand cmd = new MySqlCommand(command, mySqlConnection);
            mySqlConnection.Open();
            MySqlDataReader mySqlDataReader = cmd.ExecuteReader();
            mySqlConnection.Close();
        }

        public static bool NewHero(MySqlConnection mySqlConnection, List<string> folders, List<Hero> heroes, List<Hero> npcs)
        {
            Hero playerHero = new Hero();
            foreach (Hero hero in heroes)
            {
                if (npcs.Contains(hero))
                {

                }
            }
            try
            {
                string command = $"Select sabpat702.hero.Name from sabpat702.hero inner join sabpat702.user on sabpat702.user.Id = sabpat702.hero.UserId Where sabpat702.user.UserName = {folders.Last()}";
                MySqlCommand mySqlCommand = new MySqlCommand(command, mySqlConnection);
                MySqlDataReader mySqlDataReader = mySqlCommand.ExecuteReader();
                mySqlConnection.Open();
                while (mySqlDataReader.Read())
                {
                    string heroName = mySqlDataReader.GetString(0);
                }
                mySqlConnection.Close();
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message);
            }
            return false;
        }
    }
}
