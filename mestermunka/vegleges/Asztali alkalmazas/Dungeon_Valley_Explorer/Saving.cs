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
        public static void SavingStart(List<string> folders, List<string> files, List<Hero> heroes, List<Hero> party, Dictionary<string, bool> questsCompleted, Dictionary<string, int> consumables, int Gold, int Experience, Dictionary<string, bool> dungeonsCompleted, Dictionary<string, int> weaponsImproved, Dictionary<string, int> armorsImproved, Dictionary<string, bool> weaponsObtained, Dictionary<string, bool> armorsObtained, Dictionary<string, bool> consumablesUnlocked, MySqlConnection mySqlConnection, List<Hero> npcs, bool newHero)
        {
            if (folders.Last() != folders[10])
            {
                for (int i = 0; i < heroes.Count; i++)
                {
                    if (party.Select(x => x.DisplayName).Contains(heroes[i].DisplayName))
                    {
                        heroes[i] = party.Where(x => x.DisplayName == heroes[i].DisplayName).Select(x => x).First();
                    }
                }

                if (SaveExist(mySqlConnection, folders, files) == true)
                {
                    UpdateSave(folders, files, heroes, party, questsCompleted, consumables, Gold, Experience, dungeonsCompleted, weaponsImproved, armorsImproved, weaponsObtained, armorsObtained, consumablesUnlocked, mySqlConnection, Initializer.npcs);
                }
                else
                {
                    InsertSave(folders, files, heroes, party, questsCompleted, consumables, Gold, Experience, dungeonsCompleted, weaponsImproved, armorsImproved, weaponsObtained, armorsObtained, consumablesUnlocked, mySqlConnection, Initializer.npcs, newHero);
                }
            }
        }

        public static void UpdateSave(List<string> folders, List<string> files, List<Hero> heroes, List<Hero> party, Dictionary<string, bool> questsCompleted, Dictionary<string, int> consumables, int Gold, int Experience, Dictionary<string, bool> dungeonsCompleted, Dictionary<string, int> weaponsImproved, Dictionary<string, int> armorsImproved, Dictionary<string, bool> weaponsObtained, Dictionary<string, bool> armorsObtained, Dictionary<string, bool> consumablesUnlocked, MySqlConnection mySqlConnection, List<Hero> npcs)
        {
            int HeroId = 0;
            Hero playerHero = FindHero(heroes, npcs);
            HeroId = GetHeroId(mySqlConnection, folders, heroes, npcs);
            int SaveId = GetSaveId(mySqlConnection, folders, files);
            try
            {
                string command = $"Update sabpat702.save_game set sabpat702.save_game.SaveData = '{MakeSaveString(mySqlConnection, folders, files, heroes, party, npcs, questsCompleted, consumables, Gold, Experience, dungeonsCompleted, weaponsImproved, armorsImproved, weaponsObtained, armorsObtained, consumablesUnlocked)}' Where (sabpat702.save_game.Id = '{SaveId}')";
                MySqlCommand mySqlCommand = new MySqlCommand(command, mySqlConnection);
                mySqlConnection.Open();
                mySqlCommand.ExecuteNonQuery();
                mySqlConnection.Close();
                string command2 = $"UPDATE sabpat702.hero SET sabpat702.hero.DEF = '{playerHero.DEF}', sabpat702.hero.MDEF = '{playerHero.MDEF}', sabpat702.hero.HP = '{playerHero.HP}', sabpat702.hero.SP = '{playerHero.SP}', sabpat702.hero.MP = '{playerHero.MP}', sabpat702.hero.EXP = '{playerHero.Exp}', sabpat702.hero.LVL = '{playerHero.Lvl}', sabpat702.hero.Weapon = '{playerHero.Weapons[0].WeaponName},{playerHero.Weapons[1].WeaponName},{playerHero.Weapons[2].WeaponName}', sabpat702.hero.Armor = '{playerHero.Armors[0].ArmorName},{playerHero.Armors[1].ArmorName},{playerHero.Armors[2].ArmorName},{playerHero.Armors[3].ArmorName}', sabpat702.hero.Class = '{playerHero.heroClass}', Skill = '";
                foreach (Skill skill in playerHero.Skills)
                {
                    if (playerHero.Skills.Last() != skill)
                    {
                        command2 += $"{skill.SkillName},";
                    }
                    else
                    {
                        command2 += $"{skill.SkillName}', Magic = '";
                    }
                }
                foreach (Magic magic in playerHero.Magics)
                {
                    if (playerHero.Magics.Last() != magic)
                    {
                        command2 += $"{magic.MagicName},";
                    }
                    else
                    {
                        command2 += $"{magic.MagicName}', Passive = '";
                    }
                }
                foreach (Passive passive in playerHero.Passives)
                {
                    if (playerHero.Passives.Last() != passive)
                    {
                        command2 += $"{passive.PassiveName},";
                    }
                    else
                    {
                        command2 += $"{passive.PassiveName}' WHERE (sabpat702.hero.Id = '{HeroId}')";
                    }
                }
                MySqlCommand mySqlCommand2 = new MySqlCommand (command2, mySqlConnection);
                mySqlConnection.Open();
                mySqlCommand2.ExecuteNonQuery();
                mySqlConnection.Close();
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message);
            }
        }

        public static int GetSaveId(MySqlConnection mySqlConnection, List<string> folders, List<string> files)
        {
            int SaveId = 0;
            try
            {
                string command = $"Select sabpat702.save_game.Id from sabpat702.save_game inner join sabpat702.hero on sabpat702.hero.Id = sabpat702.save_game.HeroId inner join sabpat702.user on sabpat702.user.Id = sabpat702.hero.UserId Where sabpat702.save_game.SaveName = '{files.Last().Substring(0, files.Last().Length-4)}' And sabpat702.user.UserName = '{folders.Last()}' ";
                MySqlCommand mySqlCommand = new MySqlCommand(command, mySqlConnection);
                mySqlConnection.Open();
                MySqlDataReader mySqlDataReader = mySqlCommand.ExecuteReader();
                while (mySqlDataReader.Read())
                {
                    SaveId = mySqlDataReader.GetInt32(0);
                }
                mySqlConnection.Close();
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message);
            }
           
            return SaveId;
        }

        public static bool SaveExist(MySqlConnection mySqlConnection, List<string> folders, List<string> files)
        {
            bool saveExist = false;
            try
            {
                string command = $"Select sabpat702.save_game.SaveName from sabpat702.save_game inner join sabpat702.hero on sabpat702.hero.Id = sabpat702.save_game.HeroId inner join sabpat702.user on sabpat702.user.Id = sabpat702.hero.UserId Where sabpat702.user.UserName = '{folders.Last()}' ";
                MySqlCommand mySqlCommand = new MySqlCommand(command, mySqlConnection);
                mySqlConnection.Open();
                MySqlDataReader mySqlDataReader = mySqlCommand.ExecuteReader();
                while (mySqlDataReader.Read())
                {
                    if (mySqlDataReader.GetString(0) == files.Last().Substring(0, files.Last().Length-4))
                    {
                        saveExist = true;
                    }
                }
                mySqlConnection.Close();
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message);
            }
            return saveExist;
        }

        static string WriteHero(Hero hero, List<Hero> party)
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
                if (weaponCounter < 2)
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
                if (i < hero.Skills.Count - 1)
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
            output += hero.Background;
            output += "@";

            foreach (Hero partyMember in party)
            {
                if (partyMember == hero)
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

        public static string MakeSaveString(MySqlConnection mySqlConnection, List<string> folders, List<string> files, List<Hero> heroes, List<Hero> party, List<Hero> npcs, Dictionary<string, bool> questsCompleted, Dictionary<string, int> consumables, int Gold, int Experience, Dictionary<string, bool> dungeonsCompleted, Dictionary<string, int> weaponsImproved, Dictionary<string, int> armorsImproved, Dictionary<string, bool> weaponsObtained, Dictionary<string, bool> armorsObtained, Dictionary<string, bool> consumablesUnlocked)
        {
            int HeroId = GetHeroId(mySqlConnection, folders, heroes, npcs);
            string output = $"{HeroId}$";
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

        public static void InsertSave(List<string> folders, List<string> files, List<Hero> heroes, List<Hero> party, Dictionary<string, bool> questsCompleted, Dictionary<string, int> consumables, int Gold, int Experience, Dictionary<string, bool> dungeonsCompleted, Dictionary<string, int> weaponsImproved, Dictionary<string, int> armorsImproved, Dictionary<string, bool> weaponsObtained, Dictionary<string, bool> armorsObtained, Dictionary<string, bool> consumablesUnlocked, MySqlConnection mySqlConnection, List<Hero> npcs, bool newHero)
        {
            int HeroId = 0;
            if (HeroExists(mySqlConnection, folders, heroes, npcs, newHero) == false)
            {
                NewHero(mySqlConnection, folders, heroes, npcs);
            }
            HeroId = GetHeroId(mySqlConnection, folders, heroes, npcs);
            try
            {
                string command = $"Insert into sabpat702.save_game (HeroId, SaveData, SaveName) Values ('{HeroId}','{MakeSaveString(mySqlConnection, folders, files, heroes, party, npcs, questsCompleted, consumables, Gold, Experience, dungeonsCompleted, weaponsImproved, armorsImproved, weaponsObtained, armorsObtained, consumablesUnlocked)}','{files.Last().Substring(0, files.Last().Length - 4)}')";
                MySqlCommand cmd = new MySqlCommand(command, mySqlConnection);
                mySqlConnection.Open();
                cmd.ExecuteNonQuery();
                mySqlConnection.Close();
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message);
            }
        }

        public static int GetHeroId(MySqlConnection mySqlConnection, List<string> folders, List<Hero> heroes, List<Hero> npcs)
        {
            int HeroId = 0;
            Hero playerHero = FindHero(heroes, npcs);
            try
            {
                string command = $"Select sabpat702.hero.Id from sabpat702.hero inner join sabpat702.user on sabpat702.user.Id = sabpat702.hero.UserId Where sabpat702.user.UserName = '{folders.Last()}' And sabpat702.hero.Name = '{playerHero.HeroName}'";
                MySqlCommand mySqlCommand = new MySqlCommand(command, mySqlConnection);
                mySqlConnection.Open();
                MySqlDataReader mySqlDataReader = mySqlCommand.ExecuteReader();
                while (mySqlDataReader.Read())
                {
                    HeroId = mySqlDataReader.GetInt32(0);
                }
                mySqlConnection.Close();
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message);
            }
            return HeroId;
        }

        public static void NewHero(MySqlConnection mySqlConnection, List<string> folders, List<Hero> heroes, List<Hero> npcs)
        {
            Hero playerHero = FindHero(heroes, npcs);
            try
            {
                int userId = GetUserId(folders, mySqlConnection);
                string command = $"Insert into sabpat702.hero (Name, DEF, MDEF, HP, SP, MP, EXP, LVL, Weapon, Armor, Class, Skill, Magic, Passive, UserId, Race, Background) Values ('{playerHero.HeroName}','{playerHero.DEF}','{playerHero.MDEF}','{playerHero.HP}','{playerHero.SP}','{playerHero.MP}','{playerHero.Exp}','{playerHero.Lvl}','{playerHero.Weapons[0].WeaponName},{playerHero.Weapons[1].WeaponName},{playerHero.Weapons[2].WeaponName}','{playerHero.Armors[0].ArmorName},{playerHero.Armors[1].ArmorName},{playerHero.Armors[2].ArmorName},{playerHero.Armors[3].ArmorName}','{playerHero.heroClass}','";
                foreach (Skill skill in playerHero.Skills)
                {
                    if (skill != playerHero.Skills.Last())
                    {
                        command += $"{skill.SkillName},";
                    }
                    else
                    {
                        command += $"{skill.SkillName}','";
                    }
                }
                foreach (Magic magic in playerHero.Magics)
                {
                    if (magic != playerHero.Magics.Last())
                    {
                        command += $"{magic.MagicName},";
                    }
                    else
                    {
                        command += $"{magic.MagicName}','";
                    }
                }
                foreach (Passive passive in playerHero.Passives)
                {
                    if (passive != playerHero.Passives.Last())
                    {
                        command += $"{passive.PassiveName},";
                    }
                    else
                    {
                        command += $"{passive.PassiveName}','";
                    }
                }
                command += $"{userId}','{playerHero.Race.RaceName}','{playerHero.Background}')";
                MySqlCommand mySqlCommand = new MySqlCommand(command, mySqlConnection);
                mySqlConnection.Open();
                mySqlCommand.ExecuteNonQuery();
                mySqlConnection.Close();
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message);
            }
        }

        public static int GetUserId(List<string> folders, MySqlConnection mySqlConnection)
        {
            int userId = new int();
            try
            {
                string command = $"Select sabpat702.user.ID from sabpat702.user Where sabpat702.user.UserName = '{folders.Last()}'";
                MySqlCommand mySqlCommand = new MySqlCommand(command, mySqlConnection);
                mySqlConnection.Open();
                MySqlDataReader mySqlDataReader = mySqlCommand.ExecuteReader();
                while (mySqlDataReader.Read())
                {
                    userId = mySqlDataReader.GetInt32(0);
                }
                mySqlConnection.Close();
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message);
            }
            return userId;
        }

        public static Hero FindHero(List<Hero> heroes, List<Hero> npcs)
        {
            Hero playerHero = new Hero();
            foreach (Hero hero in heroes)
            {
                if (!npcs.Contains(hero))
                {
                    playerHero = hero;
                }
            }
            return playerHero;
        }

        public static bool HeroExists(MySqlConnection mySqlConnection, List<string> folders, List<Hero> heroes, List<Hero> npcs, bool NewHero)
        {
            bool result = false;
            Hero playerHero = FindHero(heroes, npcs);
            try
            {
                List<string> playerHeroes = new List<string>();
                string command = $"Select sabpat702.hero.Name from sabpat702.hero inner join sabpat702.user on sabpat702.user.Id = sabpat702.hero.UserId Where sabpat702.user.UserName = '{folders.Last()}'";
                MySqlCommand mySqlCommand = new MySqlCommand(command, mySqlConnection);
                mySqlConnection.Open();
                MySqlDataReader mySqlDataReader = mySqlCommand.ExecuteReader();
                while (mySqlDataReader.Read())
                {
                    playerHeroes.Add(mySqlDataReader.GetString(0));
                }
                foreach (string heroName in playerHeroes)
                {
                    if (heroName == playerHero.HeroName && NewHero == false)
                    {
                        result = true;
                    }
                }
                mySqlConnection.Close();
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message);
            }
            return result;
        }
    }
}
