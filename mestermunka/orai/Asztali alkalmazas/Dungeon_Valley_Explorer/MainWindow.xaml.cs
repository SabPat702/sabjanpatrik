using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MySql.Data.MySqlClient;
using Org.BouncyCastle.Bcpg;

namespace Dungeon_Valley_Explorer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        static MySqlConnectionStringBuilder mySqlConnectionStringBuilder = new MySqlConnectionStringBuilder()
        {
            Server = "10.3.1.65",
            Port = 3306,
            UserID = "sabpat702",
            Password = "72587413702",
            Database = "sabpat702",
            SslMode = MySqlSslMode.Preferred
        };
        static string connectionString = mySqlConnectionStringBuilder.ConnectionString;
        MySqlConnection mySqlConnection = new MySqlConnection(connectionString);
        List<string> folders = new List<string> { "GameAssets","Enemies","Dungeons","Effects","Characters","Items","Abilities","EnvironmentHazard","Races","Profiles"};
        List<string> files = new List<string> { "Monsters.txt","Ais.txt","NPCs.txt","Dungeons.txt","EnvironmentHazards.txt","Passives.txt","BuffsDebuffs.txt","SpecialEffects.txt","Skills.txt","Magics.txt","Races.txt","Consumables.txt","Armors.txt","Weapons.txt"};
        List<Passive> passives = new List<Passive>();
        List<BuffDebuff> buffsDebuffs = new List<BuffDebuff>();
        List<SpecialEffect> specialEffects = new List<SpecialEffect>();
        List<Race> races = new List<Race>();
        List<Skill> skills = new List<Skill>();
        List<Magic> magics = new List<Magic>();
        List<Monster> monsters = new List<Monster>();
        List<Ai> ais = new List<Ai>();
        List<Armor> armors = new List<Armor>();
        List<Weapon> weapons = new List<Weapon>();
        List<Hero> npcs = new List<Hero>();
        List<Dungeon> dungeons = new List<Dungeon>();
        List<EnvironmentHazard> environmentHazards = new List<EnvironmentHazard>();
        List<Consumable> consumables = new List<Consumable>();




        /*public List<string> physicalDamageTypes = new List<string>();
        public List<string> magicalDamageTypes = new List<string>();
        public bool skipDamageCalculation = false;
        public Random random = new Random();
        public Race ExampleRace = new Race();
        public Hero ExampleHero = new Hero();
        public Weapon ExampleWeapon = new Weapon();
        public SpecialEffect ExampleSpecialEffect = new SpecialEffect();
        public BuffDebuff ExampleBuffDebuff = new BuffDebuff();
        public Passive ExamplePassive = new Passive();
        public Monster ExampleMonster = new Monster();
        public Skill ExampleSkill = new Skill();
        public Target targetPrep = new Target();
        public DamageSource damageSourcePrep = new DamageSource();*/

        public MainWindow()
        {
            InitializeComponent();

            if (!Directory.Exists($@"{folders[0]}") || !Directory.Exists($@"{folders[0]}\{folders[1]}") || !Directory.Exists($@"{folders[0]}\{folders[2]}") || !Directory.Exists($@"{folders[0]}\{folders[3]}") || !Directory.Exists($@"{folders[0]}\{folders[4]}") || !Directory.Exists($@"{folders[0]}\{folders[5]}") || !Directory.Exists($@"{folders[0]}\{folders[6]}") || !Directory.Exists($@"{folders[0]}\{folders[7]}") || !Directory.Exists($@"{folders[0]}\{folders[8]}") || !Directory.Exists($@"{folders[9]}") || !File.Exists($@"{folders[0]}\{folders[1]}\{files[0]}") || !File.Exists($@"{folders[0]}\{folders[1]}\{files[1]}") || !File.Exists($@"{folders[0]}\{folders[4]}\{files[2]}") || !File.Exists($@"{folders[0]}\{folders[2]}\{files[3]}") || !File.Exists($@"{folders[0]}\{folders[7]}\{files[4]}") || !File.Exists($@"{folders[0]}\{folders[3]}\{files[5]}") || !File.Exists($@"{folders[0]}\{folders[3]}\{files[6]}") || !File.Exists($@"{folders[0]}\{folders[3]}\{files[7]}") || !File.Exists($@"{folders[0]}\{folders[6]}\{files[8]}") || !File.Exists($@"{folders[0]}\{folders[6]}\{files[9]}") || !File.Exists($@"{folders[0]}\{folders[8]}\{files[10]}") || !File.Exists($@"{folders[0]}\{folders[5]}\{files[11]}") || !File.Exists($@"{folders[0]}\{folders[5]}\{files[12]}") || !File.Exists($@"{folders[0]}\{folders[5]}\{files[13]}"))
            {
                Downloader();
            }
            Initializer();

            /*ExampleRace.Id = 0;
            ExampleRace.RaceName = "Human";
            ExampleRace.Description = "";
            ExampleRace.Fatal.Add("");
            ExampleRace.Weak.Add("");
            ExampleRace.Resist.Add("");
            ExampleRace.Endure.Add("");
            ExampleRace.Nulls.Add("");

            races.Add(ExampleRace);

            ExampleRace.Id = 1;
            ExampleRace.RaceName = "Goblin";
            ExampleRace.Description = "";
            ExampleRace.Fatal.Add("");
            ExampleRace.Weak.Add("");
            ExampleRace.Resist.Add("");
            ExampleRace.Endure.Add("");
            ExampleRace.Nulls.Add("");

            races.Add(ExampleRace);

            ExamplePassive.Id = 0;
            ExamplePassive.PassiveName = "Sword Proficiency";
            ExamplePassive.Description = "Sword strikes are a little bit stronger.";
            ExamplePassive.Affect = "Damage Calculation";

            ExampleBuffDebuff.Id = 0;
            ExampleBuffDebuff.BuffDebuffName = "Damage up";
            ExampleBuffDebuff.Description = "A small increase in damage.";
            ExampleBuffDebuff.Affect = "Damage Calculation";
            ExampleBuffDebuff.Timer = 3;

            ExampleSpecialEffect.Id = 0;
            ExampleSpecialEffect.SpecialEffectName = "Piercing Blade";
            ExampleSpecialEffect.Description = "A blade that can even cut armor. (Ignores a set amount of defense(DEF))";
            ExampleSpecialEffect.Affect = "Damage Calculation";

            ExampleWeapon.Id = 0;
            ExampleWeapon.WeaponName = "TestWeapon";
            ExampleWeapon.ATK = 20;
            ExampleWeapon.Range = "Melee";
            ExampleWeapon.Description = "A weapon for testing.";
            ExampleWeapon.CritChance = 10;
            ExampleWeapon.CritDamage = 2;
            ExampleWeapon.DamageType = "Slashing";
            ExampleWeapon.SkillCompatibility = "Both";
            ExampleWeapon.SpecialEffect.Add(ExampleSpecialEffect);
            

            ExampleSkill.Id = 0;
            ExampleSkill.SkillName = "Basic Strike";
            ExampleSkill.Description = "A basic strike from the monster";
            ExampleSkill.Range = "Melee";
            ExampleSkill.CritChance = 10;
            ExampleSkill.CritDamage = 2;
            ExampleSkill.DamageType = "Blunt";

            ExampleMonster.Id = 0;
            ExampleMonster.MonsterName = "TestMonster";
            ExampleMonster.Race = races[1];
            ExampleMonster.DEF = 0;
            ExampleMonster.InDEF = 0;
            ExampleMonster.MDEF = 0;
            ExampleMonster.InMDEF = 0;
            ExampleMonster.InHP = 100;
            ExampleMonster.HP = 100;
            ExampleMonster.MaxHP = 100;
            ExampleMonster.InMP = 0;
            ExampleMonster.MP = 0;
            ExampleMonster.MaxMP = 0;
            ExampleMonster.ATK = 20;
            ExampleMonster.Guard = false;
            ExampleMonster.Skills.Add(ExampleSkill);

            ExampleHero.Id = 0;
            ExampleHero.HeroName = "TestHero";
            ExampleHero.Guard = false;
            ExampleHero.Lvl = 1;
            ExampleHero.Exp = 0;
            ExampleHero.RaceId = 0;
            ExampleHero.HP = 100;
            ExampleHero.MaxHP = 100;
            ExampleHero.InHP = 100;
            ExampleHero.MP = 5;
            ExampleHero.InMP = 5;
            ExampleHero.MaxMP = 5;
            ExampleHero.InDEF = 2;
            ExampleHero.DEF = 2;
            ExampleHero.MDEF = 2;
            ExampleHero.InMDEF = 2;
            ExampleHero.Weapons[0] = (ExampleWeapon);
            ExampleHero.Weapons[1] = (ExampleWeapon);
            ExampleHero.Weapons[2] = (ExampleWeapon);
            ExampleHero.BuffsDebuffs.Add(ExampleBuffDebuff);
            ExampleHero.Passives.Add(ExamplePassive);

            lbDisplay.Items.Add("This is the current extent of the project.");
            lbDisplay.Items.Add("The program will calculate damage after the press of the button in the bottom right.");
            lbDisplay.Items.Add("To choose the target write 1 or 2 int the textbox on the bottom of the screen.");
            lbOptions.Items.Add("1 (The target is the Monster)");
            lbOptions.Items.Add("2 (The target is the Hero)");*/
        }

        private void btInput_Click(object sender, RoutedEventArgs e)
        {
            /*if (tbInputArea.Text == "1")
            {
                damageSourcePrep = new DamageSource(ExampleHero, 0);
                targetPrep = new Target(ExampleHero);
                lbDisplay.Items.Add(DamageCalculation(targetPrep, damageSourcePrep));
            }
            else if (tbInputArea.Text == "2")
            {
                damageSourcePrep = new DamageSource(ExampleMonster, ExampleMonster.Skills[0]);
                targetPrep = new Target(ExampleMonster);
                lbDisplay.Items.Add(DamageCalculation(targetPrep, damageSourcePrep));
            }
            else
            {
                lbDisplay.Items.Add(" -- Please choose an option from the left. -- ");
            }*/
        }

        /*public int DamageCalculation(Target target, DamageSource damageSource)
        {
            skipDamageCalculation = false;
            int damage = random.Next(damageSource.ATK / 2, damageSource.ATK);
            if (random.Next(0, 100) < damageSource.CritChance)
            {
                damage = (int)(damage * damageSource.CritDamage);
            }
            damage = DMGCalcDamageTypeChecker(damage, target, damageSource);

            if (skipDamageCalculation == false)
            {
                if (physicalDamageTypes.Contains(damageSource.DamageType))
                {
                    damage = damage - target.DEF;
                }
                else if (magicalDamageTypes.Contains(damageSource.DamageType))
                {
                    damage = damage - target.MDEF;
                }
                if (target.Guard == true)
                {
                    damage = (int)Math.Round(damage * 0.5, 0);
                }
                damage = DMGCalcSpecialEffectChecker(damage, target, damageSource);
                damage = DMGCalcEffectChecker(damage, target, damageSource);
                damage = DMGCalcPassiveChecker(damage, target, damageSource);
            }
            return damage;
        }

        public int DMGCalcDamageTypeChecker(int damage, Target target, DamageSource damageSource)
        {
            if (races[target.RaceId].Fatal.Contains(damageSource.DamageType))
            {
                damage = damage * 2;
            }
            else if (races[target.RaceId].Weak.Contains(damageSource.DamageType))
            {
                damage = (int)Math.Round(damage * 1.5, 0);
            }
            else if (races[target.RaceId].Resist.Contains(damageSource.DamageType))
            {
                damage = (int)Math.Round(damage * 0.75, 0);
            }
            else if (races[target.RaceId].Endure.Contains(damageSource.DamageType))
            {
                damage = (int)Math.Round(damage * 0.25, 0);
            }
            else if (races[target.RaceId].Nulls.Contains(damageSource.DamageType))
            {
                damage = 0;
                skipDamageCalculation = true;
            }
            else
            {

            }
            return damage;
        }

        public int DMGCalcEffectChecker(int damage, Target target, DamageSource damageSource)
        {
            foreach (BuffDebuff buffdebuff in target.BuffsDebuffs)
            {
                if (buffdebuff.Affect == "Damage Calculation")
                {
                    switch (buffdebuff.BuffDebuffName)
                    {
                        case "W.I.P":
                            break;
                        default:

                            break;
                    }
                }
            }
            foreach (BuffDebuff buffdebuff in damageSource.BuffsDebuffs)
            {
                if (buffdebuff.Affect == "Damage Calculation")
                {
                    switch (buffdebuff.BuffDebuffName)
                    {
                        case "Damage up":
                            damage = BuffDebuff.DamageUp(damage);
                            break;
                        default:

                            break;
                    }
                }
            }
            return damage;
        }

        public int DMGCalcSpecialEffectChecker(int damage, Target target, DamageSource damageSource)
        {
            foreach (SpecialEffect specialEffect in damageSource.SpecialEffect)
            {
                if (specialEffect.Affect == "Damage Calculation")
                {
                    switch (specialEffect.SpecialEffectName)
                    {
                        case "Piercing Blade":
                            if (physicalDamageTypes.Contains(damageSource.DamageType))
                            {
                                damage = SpecialEffect.PiercingBlade(damage, target);
                            }
                            break;
                        default:

                            break;
                    }
                }
            }
            return damage;
        }

        public int DMGCalcPassiveChecker(int damage, Target target, DamageSource damageSource)
        {
            foreach (Passive passive in target.Passives)
            {
                if (passive.Affect == "Damage Calculation")
                {
                    switch (passive.PassiveName)
                    {
                        case "W.I.P":
                            break;
                        default:

                            break;
                    }
                }
            }
            foreach (Passive passive in damageSource.Passives)
            {
                if (passive.Affect == "Damage Calculation")
                {
                    switch (passive.PassiveName)
                    {
                        case "Sword Proficiency":
                            damage = Passive.SwordProficiency(damage);
                            break;
                        default:

                            break;
                    }
                }
            }
            return damage;
        }*/

        //Downloader below this line------------------------------------------------------------------------------------

        public void Downloader()
        {
            if (!Directory.Exists($@"{folders[0]}"))
            {
                Directory.CreateDirectory($@"{folders[0]}");
            }
            
            if (!Directory.Exists($@"{folders[0]}\{folders[1]}"))
            {
                Directory.CreateDirectory($@"{folders[0]}\{folders[1]}");
            }
            
            if (!Directory.Exists($@"{folders[0]}\{folders[2]}"))
            {
                Directory.CreateDirectory($@"{folders[0]}\{folders[2]}");
            }
            
            if (!Directory.Exists($@"{folders[0]}\{folders[3]}"))
            {
                Directory.CreateDirectory($@"{folders[0]}\{folders[3]}");
            }
            
            if (!Directory.Exists($@"{folders[0]}\{folders[4]}"))
            {
                Directory.CreateDirectory($@"{folders[0]}\{folders[4]}");
            }
            
            if (!Directory.Exists($@"{folders[0]}\{folders[5]}"))
            {
                Directory.CreateDirectory($@"{folders[0]}\{folders[5]}");
            }
            
            if (!Directory.Exists($@"{folders[0]}\{folders[6]}"))
            {
                Directory.CreateDirectory($@"{folders[0]}\{folders[6]}");
            }
            
            if (!Directory.Exists($@"{folders[0]}\{folders[7]}"))
            {
                Directory.CreateDirectory($@"{folders[0]}\{folders[7]}");
            }
            
            if (!Directory.Exists($@"{folders[0]}\{folders[8]}"))
            {
                Directory.CreateDirectory($@"{folders[0]}\{folders[8]}");
            }
            
            if (!Directory.Exists($@"{folders[0]}\{folders[9]}"))
            {
                Directory.CreateDirectory($@"{folders[0]}\{folders[9]}");
            }
            
            if (!Directory.Exists($@"{folders[9]}"))
            {
                Directory.CreateDirectory($@"{folders[9]}");
            }

            if (!File.Exists($@"{folders[0]}\{folders[1]}\{files[0]}"))
            {
                DownloadMonsters();
            }

            if (!File.Exists($@"{folders[0]}\{folders[1]}\{files[1]}"))
            {
                DownloadAis();
            }

            if (!File.Exists($@"{folders[0]}\{folders[4]}\{files[2]}"))
            {
                DownloadNPCs();
            }

            if (!File.Exists($@"{folders[0]}\{folders[2]}\{files[3]}"))
            {
                DownloadDungeons();
            }

            if (!File.Exists($@"{folders[0]}\{folders[7]}\{files[5]}"))
            {
                DownloadPassives();
            }

            if (!File.Exists($@"{folders[0]}\{folders[7]}\{files[6]}"))
            {
                DownloadBuffsDebuffs();
            }

            if (!File.Exists($@"{folders[0]}\{folders[7]}\{files[7]}"))
            {
                DownloadSpecialEffects();
            }

            if (!File.Exists($@"{folders[0]}\{folders[8]}\{files[10]}"))
            {
                DownloadRaces();
            }

            if (!File.Exists($@"{folders[0]}\{folders[7]}\{files[4]}"))
            {
                DownloadEnvironmentHazards();
            }

            if (!File.Exists($@"{folders[0]}\{folders[6]}\{files[8]}"))
            {
                DownloadSkills();
            }

            if (!File.Exists($@"{folders[0]}\{folders[6]}\{files[9]}"))
            {
                DownloadMagics();
            }

            if (!File.Exists($@"{folders[0]}\{folders[5]}\{files[11]}"))
            {
                DownloadConsumables();
            }

            if (!File.Exists($@"{folders[0]}\{folders[5]}\{files[13]}"))
            {
                DownloadWeapons();
            }

            if (!File.Exists($@"{folders[0]}\{folders[5]}\{files[12]}"))
            {
                DownloadArmors();
            }
        }

        public void DownloadMonsters()
        {
            List<string> monstersDownloader = new List<string>();
            string command = "SELECT * FROM monster";
            MySqlCommand mySqlCommand = new MySqlCommand(command, mySqlConnection);
            try
            {
                mySqlConnection.Open();
                MySqlDataReader mySqlDataReader = mySqlCommand.ExecuteReader();
                while (mySqlDataReader.Read())
                {
                    monstersDownloader.Add($"{mySqlDataReader.GetInt32(0)}@{mySqlDataReader.GetString(1)}@{mySqlDataReader.GetInt32(3)}@{mySqlDataReader.GetInt32(4)}@{mySqlDataReader.GetInt32(5)}@{mySqlDataReader.GetInt32(6)}@{mySqlDataReader.GetInt32(7)}@{mySqlDataReader.GetInt32(8)}@{mySqlDataReader.GetString(9)}@{mySqlDataReader.GetString(10)}@{mySqlDataReader.GetString(11)}@{mySqlDataReader.GetString(12)}@{mySqlDataReader.GetString(13)}@{mySqlDataReader.GetString(14)}");
                }
                mySqlConnection.Close();

                StreamWriter streamWriter = new StreamWriter($@"{folders[0]}\{folders[1]}\{files[0]}");
                foreach (string monster in monstersDownloader)
                { 
                    streamWriter.WriteLine(monster);
                }
                streamWriter.Close();
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message);
            }
        }


        //Maybe create a class for ais and have a list of easy access interface list
        public void DownloadAis()
        {
            List<string> aisDownloader = new List<string>();
            string command = "SELECT * FROM ai";
            MySqlCommand mySqlCommand = new MySqlCommand(command, mySqlConnection);
            try
            {
                mySqlConnection.Open();
                MySqlDataReader mySqlDataReader = mySqlCommand.ExecuteReader();
                while (mySqlDataReader.Read())
                {
                    aisDownloader.Add($"{mySqlDataReader.GetInt32(0)}@{mySqlDataReader.GetString(1)}");
                }
                mySqlConnection.Close();

                StreamWriter streamWriter = new StreamWriter($@"{folders[0]}\{folders[1]}\{files[1]}");
                foreach (string ai in aisDownloader)
                {
                    streamWriter.WriteLine(ai);
                }
                streamWriter.Close();
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message);
            }
        }

        public void DownloadDungeons()
        {
            List<string> dungeonsDownloader = new List<string>();
            string command = "SELECT * FROM dungeon";
            MySqlCommand mySqlCommand = new MySqlCommand(command, mySqlConnection);
            try
            {
                mySqlConnection.Open();
                MySqlDataReader mySqlDataReader = mySqlCommand.ExecuteReader();
                while (mySqlDataReader.Read())
                {
                    dungeonsDownloader.Add($"{mySqlDataReader.GetInt32(0)}@{mySqlDataReader.GetString(1)}@{mySqlDataReader.GetString(2)}@{mySqlDataReader.GetInt32(3)}@{mySqlDataReader.GetInt32(4)}@{mySqlDataReader.GetInt32(5)}");
                }
                mySqlConnection.Close();

                StreamWriter streamWriter = new StreamWriter($@"{folders[0]}\{folders[2]}\{files[3]}");
                foreach (string dungeon in dungeonsDownloader)
                {
                    streamWriter.WriteLine(dungeon);
                }
                streamWriter.Close();
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message);
            }
        }

        public void DownloadEnvironmentHazards()
        {
            List<string> environmentHazardsDownloader = new List<string>();
            string command = "SELECT * FROM environment_hazard";
            MySqlCommand mySqlCommand = new MySqlCommand(command, mySqlConnection);
            try
            {
                mySqlConnection.Open();
                MySqlDataReader mySqlDataReader = mySqlCommand.ExecuteReader();
                while (mySqlDataReader.Read())
                {
                    environmentHazardsDownloader.Add($"{mySqlDataReader.GetInt32(0)}@{mySqlDataReader.GetString(1)}@{mySqlDataReader.GetInt32(3)}@{mySqlDataReader.GetString(4)}@{mySqlDataReader.GetInt32(5)}@{mySqlDataReader.GetDouble(6)}@{mySqlDataReader.GetString(7)}@{mySqlDataReader.GetString(8)}");
                }
                mySqlConnection.Close();

                StreamWriter streamWriter = new StreamWriter($@"{folders[0]}\{folders[7]}\{files[4]}");
                foreach (string environmentHazard in environmentHazardsDownloader)
                {
                    streamWriter.WriteLine(environmentHazard);
                }
                streamWriter.Close();
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message);
            }
        }

        public void DownloadBuffsDebuffs()
        {
            List<string> buffsDebuffsDownloader = new List<string>();
            string command = "SELECT * FROM buff_and_debuff";
            MySqlCommand mySqlCommand = new MySqlCommand(command, mySqlConnection);
            try
            {
                mySqlConnection.Open();
                MySqlDataReader mySqlDataReader = mySqlCommand.ExecuteReader();
                while (mySqlDataReader.Read())
                {
                    buffsDebuffsDownloader.Add($"{mySqlDataReader.GetInt32(0)}@{mySqlDataReader.GetString(1)}@{mySqlDataReader.GetString(2)}@{mySqlDataReader.GetString(3)}");
                }
                mySqlConnection.Close();

                StreamWriter streamWriter = new StreamWriter($@"{folders[0]}\{folders[3]}\{files[6]}");
                foreach (string buffDebuff in buffsDebuffsDownloader)
                {
                    streamWriter.WriteLine(buffDebuff);
                }
                streamWriter.Close();
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message);
            }
        }

        public void DownloadPassives()
        {
            List<string> passivesDownloader = new List<string>();
            string command = "SELECT * FROM passive";
            MySqlCommand mySqlCommand = new MySqlCommand(command, mySqlConnection);
            try
            {
                mySqlConnection.Open();
                MySqlDataReader mySqlDataReader = mySqlCommand.ExecuteReader();
                while (mySqlDataReader.Read())
                {
                    passivesDownloader.Add($"{mySqlDataReader.GetInt32(0)}@{mySqlDataReader.GetString(1)}@{mySqlDataReader.GetString(2)}@{mySqlDataReader.GetString(3)}");
                }
                mySqlConnection.Close();

                StreamWriter streamWriter = new StreamWriter($@"{folders[0]}\{folders[3]}\{files[5]}");
                foreach (string passive in passivesDownloader)
                {
                    streamWriter.WriteLine(passive);
                }
                streamWriter.Close();
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message);
            }
        }

        public void DownloadSpecialEffects()
        {
            List<string> specialEffectsDownloader = new List<string>();
            string command = "SELECT * FROM special_effect";
            MySqlCommand mySqlCommand = new MySqlCommand(command, mySqlConnection);
            try
            {
                mySqlConnection.Open();
                MySqlDataReader mySqlDataReader = mySqlCommand.ExecuteReader();
                while (mySqlDataReader.Read())
                {
                    specialEffectsDownloader.Add($"{mySqlDataReader.GetInt32(0)}@{mySqlDataReader.GetString(1)}@{mySqlDataReader.GetString(2)}@{mySqlDataReader.GetString(3)}");
                }
                mySqlConnection.Close();

                StreamWriter streamWriter = new StreamWriter($@"{folders[0]}\{folders[3]}\{files[7]}");
                foreach (string specialEffect in specialEffectsDownloader)
                {
                    streamWriter.WriteLine(specialEffect);
                }
                streamWriter.Close();
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message);
            }
        }

        public void DownloadNPCs()
        {
            List<string> npcsDownloader = new List<string>();
            string command = "SELECT * FROM npc";
            MySqlCommand mySqlCommand = new MySqlCommand(command, mySqlConnection);
            try
            {
                mySqlConnection.Open();
                MySqlDataReader mySqlDataReader = mySqlCommand.ExecuteReader();
                while (mySqlDataReader.Read())
                {
                    npcsDownloader.Add($"{mySqlDataReader.GetInt32(0)}@{mySqlDataReader.GetString(1)}@{mySqlDataReader.GetInt32(3)}@{mySqlDataReader.GetInt32(4)}@{mySqlDataReader.GetInt32(5)}@{mySqlDataReader.GetInt32(6)}@{mySqlDataReader.GetInt32(7)}@{mySqlDataReader.GetInt32(8)}@{mySqlDataReader.GetInt32(9)}@{mySqlDataReader.GetString(10)}@{mySqlDataReader.GetString(11)}@{mySqlDataReader.GetString(12)}@{mySqlDataReader.GetString(13)}@{mySqlDataReader.GetString(14)}@{mySqlDataReader.GetString(15)}@{mySqlDataReader.GetString(16)}");
                }
                mySqlConnection.Close();

                StreamWriter streamWriter = new StreamWriter($@"{folders[0]}\{folders[4]}\{files[2]}");
                foreach (string npc in npcsDownloader)
                {
                    streamWriter.WriteLine(npc);
                }
                streamWriter.Close();
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message);
            }
        }

        public void DownloadConsumables()
        {
            List<string> consumablesDownloader = new List<string>();
            string command = "SELECT * FROM consumable";
            MySqlCommand mySqlCommand = new MySqlCommand(command, mySqlConnection);
            try
            {
                mySqlConnection.Open();
                MySqlDataReader mySqlDataReader = mySqlCommand.ExecuteReader();
                while (mySqlDataReader.Read())
                {
                    consumablesDownloader.Add($"{mySqlDataReader.GetInt32(0)}@{mySqlDataReader.GetString(1)}@{mySqlDataReader.GetString(2)}@{mySqlDataReader.GetString(3)}@{mySqlDataReader.GetInt32(4)}");
                }
                mySqlConnection.Close();

                StreamWriter streamWriter = new StreamWriter($@"{folders[0]}\{folders[5]}\{files[11]}");
                foreach (string consumable in consumablesDownloader)
                {
                    streamWriter.WriteLine(consumable);
                }
                streamWriter.Close();
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message);
            }
        }

        public void DownloadArmors()
        {
            List<string> armorsDownloader = new List<string>();
            string command = "SELECT * FROM armor";
            MySqlCommand mySqlCommand = new MySqlCommand(command, mySqlConnection);
            try
            {
                mySqlConnection.Open();
                MySqlDataReader mySqlDataReader = mySqlCommand.ExecuteReader();
                while (mySqlDataReader.Read())
                {
                    armorsDownloader.Add($"{mySqlDataReader.GetInt32(0)}@{mySqlDataReader.GetString(1)}@{mySqlDataReader.GetString(2)}@{mySqlDataReader.GetInt32(3)}@{mySqlDataReader.GetInt32(4)}@{mySqlDataReader.GetString(5)}@{mySqlDataReader.GetInt32(6)}@{mySqlDataReader.GetInt32(7)}");
                }
                mySqlConnection.Close();

                StreamWriter streamWriter = new StreamWriter($@"{folders[0]}\{folders[5]}\{files[12]}");
                foreach (string armor in armorsDownloader)
                {
                    streamWriter.WriteLine(armor);
                }
                streamWriter.Close();
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message);
            }
        }

        public void DownloadWeapons()
        {
            List<string> weaponsDownloader = new List<string>();
            string command = "SELECT * FROM weapon";
            MySqlCommand mySqlCommand = new MySqlCommand(command, mySqlConnection);
            try
            {
                mySqlConnection.Open();
                MySqlDataReader mySqlDataReader = mySqlCommand.ExecuteReader();
                while (mySqlDataReader.Read())
                {
                    weaponsDownloader.Add($"{mySqlDataReader.GetInt32(0)}@{mySqlDataReader.GetString(1)}@{mySqlDataReader.GetString(2)}@{mySqlDataReader.GetInt32(3)}@{mySqlDataReader.GetString(4)}@{mySqlDataReader.GetInt32(5)}@{mySqlDataReader.GetDouble(6)}@{mySqlDataReader.GetString(7)}@{mySqlDataReader.GetString(8)}@{mySqlDataReader.GetString(9)}@{mySqlDataReader.GetInt32(10)}");
                }
                mySqlConnection.Close();

                StreamWriter streamWriter = new StreamWriter($@"{folders[0]}\{folders[5]}\{files[13]}");
                foreach (string weapon in weaponsDownloader)
                {
                    streamWriter.WriteLine(weapon);
                }
                streamWriter.Close();
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message);
            }
        }

        public void DownloadSkills()
        {
            List<string> skillsDownloader = new List<string>();
            string command = "SELECT * FROM skill";
            MySqlCommand mySqlCommand = new MySqlCommand(command, mySqlConnection);
            try
            {
                mySqlConnection.Open();
                MySqlDataReader mySqlDataReader = mySqlCommand.ExecuteReader();
                while (mySqlDataReader.Read())
                {
                    skillsDownloader.Add($"{mySqlDataReader.GetInt32(0)}@{mySqlDataReader.GetString(1)}@{mySqlDataReader.GetString(2)}@{mySqlDataReader.GetString(3)}@{mySqlDataReader.GetInt32(4)}@{mySqlDataReader.GetDouble(5)}@{mySqlDataReader.GetString(6)}@{mySqlDataReader.GetString(7)}@{mySqlDataReader.GetInt32(8)}@{mySqlDataReader.GetInt32(9)}");
                }
                mySqlConnection.Close();

                StreamWriter streamWriter = new StreamWriter($@"{folders[0]}\{folders[6]}\{files[8]}");
                foreach (string skill in skillsDownloader)
                {
                    streamWriter.WriteLine(skill);
                }
                streamWriter.Close();
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message);
            }
        }

        public void DownloadMagics()
        {
            List<string> magicsDownloader = new List<string>();
            string command = "SELECT * FROM magic";
            MySqlCommand mySqlCommand = new MySqlCommand(command, mySqlConnection);
            try
            {
                mySqlConnection.Open();
                MySqlDataReader mySqlDataReader = mySqlCommand.ExecuteReader();
                while (mySqlDataReader.Read())
                {
                    magicsDownloader.Add($"{mySqlDataReader.GetInt32(0)}@{mySqlDataReader.GetString(1)}@{mySqlDataReader.GetString(2)}@{mySqlDataReader.GetInt32(3)}@{mySqlDataReader.GetString(4)}@{mySqlDataReader.GetInt32(5)}@{mySqlDataReader.GetDouble(6)}@{mySqlDataReader.GetString(7)}@{mySqlDataReader.GetString(8)}@{mySqlDataReader.GetInt32(9)}@{mySqlDataReader.GetInt32(10)}");
                }
                mySqlConnection.Close();

                StreamWriter streamWriter = new StreamWriter($@"{folders[0]}\{folders[6]}\{files[9]}");
                foreach (string magic in magicsDownloader)
                {
                    streamWriter.WriteLine(magic);
                }
                streamWriter.Close();
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message);
            }
        }

        public void DownloadRaces()
        {
            List<string> racesDownloader = new List<string>();
            string command = "SELECT * FROM race";
            MySqlCommand mySqlCommand = new MySqlCommand(command, mySqlConnection);
            try
            {
                mySqlConnection.Open();
                MySqlDataReader mySqlDataReader = mySqlCommand.ExecuteReader();
                while (mySqlDataReader.Read())
                {
                    racesDownloader.Add($"{mySqlDataReader.GetInt32(0)}@{mySqlDataReader.GetString(1)}@{mySqlDataReader.GetString(2)}@{mySqlDataReader.GetString(3)}@{mySqlDataReader.GetString(4)}@{mySqlDataReader.GetString(5)}@{mySqlDataReader.GetString(6)}@{mySqlDataReader.GetString(7)}");
                }
                mySqlConnection.Close();

                StreamWriter streamWriter = new StreamWriter($@"{folders[0]}\{folders[8]}\{files[10]}");
                foreach (string race in racesDownloader)
                {
                    streamWriter.WriteLine(race);
                }
                streamWriter.Close();
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message);
            }
        }
        
        //Downloader ends here------------------------------------------------------------------------------------------

        //Initializer starts here---------------------------------------------------------------------------------------

        public void Initializer()
        {
            LoadPassives();
            LoadBuffsDebuffs();
            LoadSpecialEffects();
            LoadRaces();
            LoadSkills();
            LoadMagics();
            LoadMonsters();
            LoadAis();
            LoadArmors();
            LoadWeapons();
            LoadNPCs();
            LoadDungeons();
            LoadEnvironmentHazards();
            LoadConsumables();
        }

        public void LoadMonsters()
        {
            monsters.Clear();
            try
            {
                StreamReader streamreader = new StreamReader($@"{folders[0]}\{folders[1]}\{files[0]}");
                while(!streamreader.EndOfStream)
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

        public void LoadAis()
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

        public void LoadNPCs()
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

        public void LoadDungeons()
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

        public void LoadEnvironmentHazards()
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

        public void LoadPassives()
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

        public void LoadBuffsDebuffs()
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

        public void LoadSpecialEffects()
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

        public void LoadSkills()
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

        public void LoadMagics()
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

        public void LoadRaces()
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

        public void LoadConsumables()
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

        public void LoadArmors()
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

        public void LoadWeapons()
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

        //Initializer ends here-----------------------------------------------------------------------------------------
    }
}

