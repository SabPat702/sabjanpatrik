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

        /*public List<string> physicalDamageTypes = new List<string>();
        public List<string> magicalDamageTypes = new List<string>();
        public bool skipDamageCalculator = false;
        public Random random = new Random();
        public List<Race> races = new List<Race>();
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

            if (!Directory.Exists(@"GameAssets") || !Directory.Exists(@"GameAssets\Enemies") || !Directory.Exists(@"GameAssets\Dungeons") || !Directory.Exists(@"GameAssets\Effects") || !Directory.Exists(@"GameAssets\Characters") || !Directory.Exists(@"GameAssets\Items") || !Directory.Exists(@"GameAssets\Abilities") || !Directory.Exists(@"GameAssets\Dungeons") || !Directory.Exists(@"GameAssets\EnvironmentHazards") || !Directory.Exists(@"GameAssets\Races") || !Directory.Exists(@"Profiles"))
            {
                DownloaderStepOne();
            }

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
            ExamplePassive.Affect = "Damage Calculator";

            ExampleBuffDebuff.Id = 0;
            ExampleBuffDebuff.BuffDebuffName = "Damage up";
            ExampleBuffDebuff.Description = "A small increase in damage.";
            ExampleBuffDebuff.Affect = "Damage Calculator";
            ExampleBuffDebuff.Timer = 3;

            ExampleSpecialEffect.Id = 0;
            ExampleSpecialEffect.SpecialEffectName = "Piercing Blade";
            ExampleSpecialEffect.Description = "A blade that can even cut armor. (Ignores a set amount of defense(DEF))";
            ExampleSpecialEffect.Affect = "Damage Calculator";

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
                lbDisplay.Items.Add(DamageCalculator(targetPrep, damageSourcePrep));
            }
            else if (tbInputArea.Text == "2")
            {
                damageSourcePrep = new DamageSource(ExampleMonster, ExampleMonster.Skills[0]);
                targetPrep = new Target(ExampleMonster);
                lbDisplay.Items.Add(DamageCalculator(targetPrep, damageSourcePrep));
            }
            else
            {
                lbDisplay.Items.Add(" -- Please choose an option from the left. -- ");
            }*/
        }

        /*public int DamageCalculator(Target target, DamageSource damageSource)
        {
            skipDamageCalculator = false;
            int damage = random.Next(damageSource.ATK / 2, damageSource.ATK);
            if (random.Next(0, 100) < damageSource.CritChance)
            {
                damage = (int)(damage * damageSource.CritDamage);
            }
            damage = DMGCalcDamageTypeChecker(damage, target, damageSource);

            if (skipDamageCalculator == false)
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
                skipDamageCalculator = true;
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
                if (buffdebuff.Affect == "Damage Calculator")
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
                if (buffdebuff.Affect == "Damage Calculator")
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
                if (specialEffect.Affect == "Damage Calculator")
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
                if (passive.Affect == "Damage Calculator")
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
                if (passive.Affect == "Damage Calculator")
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

        public void DownloaderStepOne()
        {
            if (!Directory.Exists(@"GameAssets"))
            {
                Directory.CreateDirectory(@"GameAssets");
            }
            
            if (!Directory.Exists(@"GameAssets\Enemies"))
            {
                Directory.CreateDirectory(@"GameAssets\Enemies");
                DownloadMonsters();
                DownloadAis();
            }
            
            if (!Directory.Exists(@"GameAssets\Dungeons"))
            {
                Directory.CreateDirectory(@"GameAssets\Dungeons");
                DownloadDungeons();
            }
            
            if (!Directory.Exists(@"GameAssets\Effects"))
            {
                Directory.CreateDirectory(@"GameAssets\Effects");
                DownloadBuffsDebuffs();
                DownloadPassives();
                DownloadSpecialEffects();
            }
            
            if (!Directory.Exists(@"GameAssets\Characters"))
            {
                Directory.CreateDirectory(@"GameAssets\Characters");
                DownloadNPCs();
            }
            
            if (!Directory.Exists(@"GameAssets\Items"))
            {
                Directory.CreateDirectory(@"GameAssets\Items");
                DownloadArmors();
                DownloadConsumables();
                DownloadWeapons();
            }
            
            if (!Directory.Exists(@"GameAssets\Abilities"))
            {
                Directory.CreateDirectory(@"GameAssets\Abilities");
                DownloadSkills();
                DownloadMagics();
            }
            
            if (!Directory.Exists(@"GameAssets\Dungeons"))
            {
                Directory.CreateDirectory(@"GameAssets\Dungeons");
                DownloadDungeons();
            }
            
            if (!Directory.Exists(@"GameAssets\EnvironmentHazards"))
            {
                Directory.CreateDirectory(@"GameAssets\EnvironmentHazards");
                DownloadEnvironmentHazards();
            }
            
            if (!Directory.Exists(@"GameAssets\Races"))
            {
                Directory.CreateDirectory(@"GameAssets\Races");
                DownloadRaces();
            }
            
            if (!Directory.Exists(@"Profiles"))
            {
                Directory.CreateDirectory(@"Profiles");
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

                StreamWriter streamWriter = new StreamWriter(@"GameAssets\Enemies\Monsters.txt");
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

                StreamWriter streamWriter = new StreamWriter(@"GameAssets\Enemies\Ais.txt");
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

                StreamWriter streamWriter = new StreamWriter(@"GameAssets\Dungeons\Dungeons.txt");
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

                StreamWriter streamWriter = new StreamWriter(@"GameAssets\EnvironmentHazards\EnvironmentHazards.txt");
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
            
            
        }

        public void DownloadPassives()
        {
            
            
        }

        public void DownloadSpecialEffects()
        {
            
            
        }

        public void DownloadNPCs()
        {
            
            
        }

        public void DownloadConsumables()
        {
            
            
        }

        public void DownloadArmors()
        {
            
            
        }

        public void DownloadWeapons()
        {
            
            
        }

        public void DownloadSkills()
        {
            
            
        }

        public void DownloadMagics()
        {
            
            
        }

        public void DownloadRaces()
        {
            
            
        }
    }
}

