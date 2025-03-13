using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Dungeon_Valley_Explorer
{
    static class Initializer
    {
        public static List<Passive> passives = new List<Passive>();
        public static List<BuffDebuff> buffsDebuffs = new List<BuffDebuff>();
        public static List<SpecialEffect> specialEffects = new List<SpecialEffect>();
        public static List<Race> races = new List<Race>();
        public static List<Skill> skills = new List<Skill>();
        public static List<Magic> magics = new List<Magic>();
        public static List<Monster> monsters = new List<Monster>();
        public static List<Ai> ais = new List<Ai>();
        public static List<Armor> armors = new List<Armor>();
        public static List<Weapon> weapons = new List<Weapon>();
        public static List<Hero> npcs = new List<Hero>();
        public static List<Dungeon> dungeons = new List<Dungeon>();
        public static List<EnvironmentHazard> environmentHazards = new List<EnvironmentHazard>();
        public static List<Consumable> consumables = new List<Consumable>();

        public static List<string> GetProfilesFromDevice(List<string> folders, ListBox lbOptions, List<string> tempProfiles)
        {
            string[] seged = Directory.GetDirectories($@"{folders[9]}");
            for (int i = 0; i < seged.Count(); i++)
            {
                string[] linecutter = seged[i].Split('\\');
                if (linecutter[1] != folders[10])
                {
                    tempProfiles.Add(linecutter[1]);
                }
            }
            return tempProfiles;
        }

        public static void Initialize(List<string> folders, List<string> files)
        {
            LoadPassives(folders, files);
            LoadBuffsDebuffs(folders, files);
            LoadSpecialEffects(folders, files);
            LoadRaces(folders, files);
            LoadSkills(folders, files);
            LoadMagics(folders, files);
            LoadMonsters(folders, files);
            LoadAis(folders, files);
            LoadArmors(folders, files);
            LoadWeapons(folders, files);
            LoadNPCs(folders, files);
            LoadDungeons(folders, files);
            LoadEnvironmentHazards(folders, files);
            LoadConsumables(folders, files);
        }

        public static void LoadMonsters(List<string> folders, List<string> files)
        {
            monsters.Clear();
            try
            {
                StreamReader streamreader = new StreamReader($@"{folders[0]}\{folders[1]}\{files[0]}");
                while (!streamreader.EndOfStream)
                {
                    Monster monster = new Monster(streamreader.ReadLine(), passives, skills, magics, races);
                    monsters.Add(monster);
                }
                streamreader.Close();
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message);
            }
        }

        public static void LoadAis(List<string> folders, List<string> files)
        {
            ais.Clear();
            try
            {
                StreamReader streamreader = new StreamReader($@"{folders[0]}\{folders[1]}\{files[1]}");
                while (!streamreader.EndOfStream)
                {
                    Ai ai = new Ai(streamreader.ReadLine());
                    ais.Add(ai);
                }
                streamreader.Close();
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message);
            }
        }

        public static void LoadNPCs(List<string> folders, List<string> files)
        {
            npcs.Clear();
            try
            {
                StreamReader streamreader = new StreamReader($@"{folders[0]}\{folders[4]}\{files[2]}");
                while (!streamreader.EndOfStream)
                {
                    Hero npc = new Hero(streamreader.ReadLine(), passives, skills, magics, races, armors, weapons);
                    npcs.Add(npc);
                }
                streamreader.Close();
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message);
            }
        }

        public static void LoadDungeons(List<string> folders, List<string> files)
        {
            dungeons.Clear();
            try
            {
                StreamReader streamreader = new StreamReader($@"{folders[0]}\{folders[2]}\{files[3]}");
                while (!streamreader.EndOfStream)
                {
                    Dungeon dungeon = new Dungeon(streamreader.ReadLine());
                    dungeons.Add(dungeon);
                }
                streamreader.Close();
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message);
            }
        }

        public static void LoadEnvironmentHazards(List<string> folders, List<string> files)
        {
            environmentHazards.Clear();
            try
            {
                StreamReader streamreader = new StreamReader($@"{folders[0]}\{folders[7]}\{files[4]}");
                while (!streamreader.EndOfStream)
                {
                    EnvironmentHazard environmentHazard = new EnvironmentHazard(streamreader.ReadLine(), specialEffects);
                    environmentHazards.Add(environmentHazard);
                }
                streamreader.Close();
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message);
            }
        }

        public static void LoadPassives(List<string> folders, List<string> files)
        {
            passives.Clear();
            try
            {
                StreamReader streamreader = new StreamReader($@"{folders[0]}\{folders[3]}\{files[5]}");
                while (!streamreader.EndOfStream)
                {
                    Passive passive = new Passive(streamreader.ReadLine());
                    passives.Add(passive);
                }
                streamreader.Close();
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message);
            }
        }

        public static void LoadBuffsDebuffs(List<string> folders, List<string> files)
        {
            buffsDebuffs.Clear();
            try
            {
                StreamReader streamreader = new StreamReader($@"{folders[0]}\{folders[3]}\{files[6]}");
                while (!streamreader.EndOfStream)
                {
                    BuffDebuff buffDebuff = new BuffDebuff(streamreader.ReadLine());
                    buffsDebuffs.Add(buffDebuff);
                }
                streamreader.Close();
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message);
            }
        }

        public static void LoadSpecialEffects(List<string> folders, List<string> files)
        {
            specialEffects.Clear();
            try
            {
                StreamReader streamreader = new StreamReader($@"{folders[0]}\{folders[3]}\{files[7]}");
                while (!streamreader.EndOfStream)
                {
                    SpecialEffect specialEffect = new SpecialEffect(streamreader.ReadLine());
                    specialEffects.Add(specialEffect);
                }
                streamreader.Close();
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message);
            }
        }

        public static void LoadSkills(List<string> folders, List<string> files)
        {
            skills.Clear();
            try
            {
                StreamReader streamreader = new StreamReader($@"{folders[0]}\{folders[6]}\{files[8]}");
                while (!streamreader.EndOfStream)
                {
                    Skill skill = new Skill(streamreader.ReadLine(), specialEffects);
                    skills.Add(skill);
                }
                streamreader.Close();
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message);
            }
        }

        public static void LoadMagics(List<string> folders, List<string> files)
        {
            magics.Clear();
            try
            {
                StreamReader streamreader = new StreamReader($@"{folders[0]}\{folders[6]}\{files[9]}");
                while (!streamreader.EndOfStream)
                {
                    Magic magic = new Magic(streamreader.ReadLine(), specialEffects);
                    magics.Add(magic);
                }
                streamreader.Close();
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message);
            }
        }

        public static void LoadRaces(List<string> folders, List<string> files)
        {
            races.Clear();
            try
            {
                StreamReader streamreader = new StreamReader($@"{folders[0]}\{folders[8]}\{files[10]}");
                while (!streamreader.EndOfStream)
                {
                    Race race = new Race(streamreader.ReadLine());
                    races.Add(race);
                }
                streamreader.Close();
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message);
            }
        }

        public static void LoadConsumables(List<string> folders, List<string> files)
        {
            consumables.Clear();
            try
            {
                StreamReader streamreader = new StreamReader($@"{folders[0]}\{folders[5]}\{files[11]}");
                while (!streamreader.EndOfStream)
                {
                    Consumable consumable = new Consumable(streamreader.ReadLine(), specialEffects);
                    consumables.Add(consumable);
                }
                streamreader.Close();
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message);
            }
        }

        public static void LoadArmors(List<string> folders, List<string> files)
        {
            armors.Clear();
            try
            {
                StreamReader streamreader = new StreamReader($@"{folders[0]}\{folders[5]}\{files[12]}");
                while (!streamreader.EndOfStream)
                {
                    Armor armor = new Armor(streamreader.ReadLine(), specialEffects);
                    armors.Add(armor);
                }
                streamreader.Close();
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message);
            }
        }

        public static void LoadWeapons(List<string> folders, List<string> files)
        {
            weapons.Clear();
            try
            {
                StreamReader streamreader = new StreamReader($@"{folders[0]}\{folders[5]}\{files[13]}");
                while (!streamreader.EndOfStream)
                {
                    Weapon weapon = new Weapon(streamreader.ReadLine(), specialEffects);
                    weapons.Add(weapon);
                }
                streamreader.Close();
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message);
            }
        }
    }
}
