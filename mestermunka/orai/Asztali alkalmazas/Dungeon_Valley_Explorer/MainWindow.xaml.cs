﻿using System;
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

        string addEmail = "";
        string addPassword = "";



        public List<string> physicalDamageTypes = new List<string> { "Blunt","Pierce","Slash"};
        public List<string> magicalDamageTypes = new List<string> { "Fire"};
        public bool skipDamageCalculation = false;
        public Random random = new Random();
        public Target targetPrep = new Target();
        public DamageSource damageSourcePrep = new DamageSource();

        public MainWindow()
        {
            InitializeComponent();

            if (!Directory.Exists($@"{folders[0]}") || !Directory.Exists($@"{folders[0]}\{folders[1]}") || !Directory.Exists($@"{folders[0]}\{folders[2]}") || !Directory.Exists($@"{folders[0]}\{folders[3]}") || !Directory.Exists($@"{folders[0]}\{folders[4]}") || !Directory.Exists($@"{folders[0]}\{folders[5]}") || !Directory.Exists($@"{folders[0]}\{folders[6]}") || !Directory.Exists($@"{folders[0]}\{folders[7]}") || !Directory.Exists($@"{folders[0]}\{folders[8]}") || !Directory.Exists($@"{folders[9]}") || !File.Exists($@"{folders[0]}\{folders[1]}\{files[0]}") || !File.Exists($@"{folders[0]}\{folders[1]}\{files[1]}") || !File.Exists($@"{folders[0]}\{folders[4]}\{files[2]}") || !File.Exists($@"{folders[0]}\{folders[2]}\{files[3]}") || !File.Exists($@"{folders[0]}\{folders[7]}\{files[4]}") || !File.Exists($@"{folders[0]}\{folders[3]}\{files[5]}") || !File.Exists($@"{folders[0]}\{folders[3]}\{files[6]}") || !File.Exists($@"{folders[0]}\{folders[3]}\{files[7]}") || !File.Exists($@"{folders[0]}\{folders[6]}\{files[8]}") || !File.Exists($@"{folders[0]}\{folders[6]}\{files[9]}") || !File.Exists($@"{folders[0]}\{folders[8]}\{files[10]}") || !File.Exists($@"{folders[0]}\{folders[5]}\{files[11]}") || !File.Exists($@"{folders[0]}\{folders[5]}\{files[12]}") || !File.Exists($@"{folders[0]}\{folders[5]}\{files[13]}"))
            {
                Downloader();
            }
            Initializer();

            lbDisplay.Items.Add("Welcome to Dungeon Valley Explorer!");
            lbDisplay.Items.Add("Tip: To check if you have all the game assets downloaded just delete the GameAssets folder and download everything again.");
            lbDisplay.Items.Add("Tip: To play with cloud saving you need to login to a account through the Login option.");
            lbDisplay.Items.Add("Tip: To progress write text based on the options on the far left into the area at the bottom of the window or select an option on the far left then press the input button. (This can be the number or the option as well example:'1'. 'Offline play')");
            lbDisplay.Items.Add("Tip: To learn more about most options you can type '?' to get a short explanation.");

            lbOptions.Items.Add("1. Offline play");
            lbOptions.Items.Add("2. Select Profile");
            lbOptions.Items.Add("3. Add Profile");

            btInput.Click += new RoutedEventHandler(OfflineSelectProfileAddProfileOption);
        }

        //Main menu starts here ----------------------------------------------------------------------------------------

        public void OfflineSelectProfileAddProfileOption(object sender, RoutedEventArgs e)
        {
            btInput.Click -= new RoutedEventHandler(OfflineSelectProfileAddProfileOption);
            switch (tbInputArea.Text)
            {
                case "1":

                    break;
                case "2":

                    break;
                case "3":
                    AddProfile();
                    break;
                case "Offline play":

                    break;
                case "Select Profile":

                    break;
                case "Add Profile":
                    AddProfile();
                    break;
                case "?":
                    ExplainOfflineLoginAddProfileOption();
                    break;
                default:
                    MessageBox.Show("Please use the textbox at the bottom of the window to write a valid option from the left.");
                    btInput.Click += new RoutedEventHandler(OfflineSelectProfileAddProfileOption);
                    break;
            }
        }

        public void OfflinePlay()
        {
            tbInputArea.Text = "";
        }

        public void SelectProfile()
        {
            tbInputArea.Text = "";
        }

        public void AddProfile()
        {
            tbInputArea.Text = "";
            lbDisplay.Items.Add("To add a profile you will have to first write in your email then write in your password into the textbox and click the input button after both the email and the password. (You can also write 'Back' to go back.)");
            btInput.Click += new RoutedEventHandler(AddProfileEmail);
            lbOptions.Items.Clear();
        }

        public void ExplainOfflineLoginAddProfileOption()
        {
            lbDisplay.Items.Add("Offline play allows you to play without an account on this computer.");
            lbDisplay.Items.Add("Select Profile allows you to select an already added profile to use the cloud save feature so that you can access your saves from different computers.");
            lbDisplay.Items.Add("Add Profile allows you to add a existing account from our database so that you can create saves that have access to cloud saving and get access to existing cloud saves.");
            btInput.Click += new RoutedEventHandler(OfflineSelectProfileAddProfileOption);
            tbInputArea.Text = "";
        }

        public void AddProfileEmail(object sender, RoutedEventArgs e)
        {
            btInput.Click -= new RoutedEventHandler(AddProfileEmail);
            if (!tbInputArea.Text.Contains('@') || !tbInputArea.Text.Contains('.'))
            {
                switch (tbInputArea.Text)
                {
                    case "Back":
                        tbInputArea.Text = "";
                        lbOptions.Items.Add("1. Offline play");
                        lbOptions.Items.Add("2. Select Profile");
                        lbOptions.Items.Add("3. Add Profile");
                        btInput.Click += new RoutedEventHandler(OfflineSelectProfileAddProfileOption);
                        break;
                    default:
                        MessageBox.Show("Please write down the email for the profile or write 'Back' to cancel.");
                        btInput.Click += new RoutedEventHandler(AddProfileEmail);
                        break;
                }
            }
            else if (tbInputArea.Text.Contains('@') && tbInputArea.Text.Contains('.'))
            {
                addEmail = tbInputArea.Text;
                lbDisplay.Items.Add("Now please write down the password.");
                btInput.Click += new RoutedEventHandler(AddProfilePassword);
                tbInputArea.Text = "";
            }
        }

        public void AddProfilePassword(object sender, RoutedEventArgs e)
        {
            btInput.Click -= new RoutedEventHandler(AddProfilePassword);
            switch (tbInputArea.Text)
            {
                case "Back":
                    tbInputArea.Text = "";
                    lbOptions.Items.Add("1. Offline play");
                    lbOptions.Items.Add("2. Select Profile");
                    lbOptions.Items.Add("3. Add Profile");
                    btInput.Click += new RoutedEventHandler(OfflineSelectProfileAddProfileOption);
                    break;
                default:
                    addPassword = tbInputArea.Text;
                    lbDisplay.Items.Add($"Are you sure you want to add this profile? (Email: {addEmail},Password: {addPassword})");
                    tbInputArea.Text = "";
                    lbOptions.Items.Add("1. Yes");
                    lbOptions.Items.Add("2. No");
                    lbOptions.Items.Add("3. Change information");
                    btInput.Click += new RoutedEventHandler(AddProfileConfirm);
                    break;
            }
        }

        public void AddProfileConfirm(object sender, RoutedEventArgs e)
        {
            btInput.Click -= new RoutedEventHandler(AddProfileConfirm);
            switch (tbInputArea.Text)
            {
                case "1":
                    AddProfileGetProfile();
                    break;
                case "2":
                    ExitAddProfile();
                    break;
                case "3":
                    ReAddProfileEmail();
                    break;
                case "Yes":
                    AddProfileGetProfile();
                    break;
                case "No":
                    ExitAddProfile();
                    break;
                case "Change information":
                    ReAddProfileEmail();
                    break;
                case "?":
                    ExplainAddProfileConfirm();
                    break;
                default:
                    MessageBox.Show("Please use the textbox at the bottom of the window to write a valid option from the left.");
                    btInput.Click += new RoutedEventHandler(AddProfileConfirm);
                    break;
            }
        }

        public void AddProfileGetProfile()
        {
            string command = $"Select UserName from user where Email = '{addEmail}' and Password = '{addPassword}' limit 1";
            MySqlCommand mySqlCommand = new MySqlCommand(command, mySqlConnection);
            try
            {
                mySqlConnection.Open();
                MySqlDataReader mySqlDataReader = mySqlCommand.ExecuteReader();
                while (mySqlDataReader.Read())
                {
                    folders.Add(mySqlDataReader.GetString(0));
                }
                mySqlConnection.Close();

                if (!Directory.Exists($@"{folders[9]}\{folders.Last()}") && folders.Last() != "Profiles")
                {
                    Directory.CreateDirectory($@"{folders[9]}\{folders.Last()}");
                    GetProfileSaves();
                    folders.Remove(folders[folders.Count() - 1]);
                    MessageBox.Show("The profile has been added successfully.");

                }
                else if (Directory.Exists($@"{folders[9]}\{folders.Last()}") && folders.Last() != "Profiles")
                {
                    MessageBox.Show("This profile has already been added to the game.");
                    folders.Remove(folders[folders.Count() - 1]);
                }
                else
                {
                    MessageBox.Show("We couldn't find a matching profile please try again.");
                }
            }
            catch (Exception error)
            { 
                MessageBox.Show(error.Message);
            }

            tbInputArea.Text = "";
            btInput.Click += new RoutedEventHandler(OfflineSelectProfileAddProfileOption);
            lbOptions.Items.Clear();
            lbOptions.Items.Add("1. Offline play");
            lbOptions.Items.Add("2. Select Profile");
            lbOptions.Items.Add("3. Add Profile");
        }

        public void GetProfileSaves()
        {
            string command = $"Select `save_game`.SaveName, `save_game`.SaveData from save_game inner join hero on hero.Id = save_game.HeroId inner join user on user.Id = hero.UserId where UserName = '{folders.Last()}'";
            MySqlCommand mySqlCommand = new MySqlCommand(command,mySqlConnection);
            int savesCount = 0;
            List<string> saveData = new List<string>();
            try
            {
                mySqlConnection.Open();
                MySqlDataReader mySqlDataReader = mySqlCommand.ExecuteReader();
                while (mySqlDataReader.Read())
                {
                    files.Add(mySqlDataReader.GetString(0));
                    saveData.Add(mySqlDataReader.GetString(1));
                    savesCount++;
                }

                if (files.Last() != "Weapons.txt")
                {
                    for (int i = 0; i < savesCount; i++)
                    {
                        StreamWriter streamWriter = new StreamWriter($@"{folders[9]}\{folders.Last()}\{files[files.Count()-(i+1)]}.txt");
                        streamWriter.WriteLine(saveData[i]);
                        streamWriter.Close();
                    }

                    for (int i = 0; i < savesCount; i++)
                    {
                        files.Remove(files[files.Count() - 1]);
                    }
                }
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message);
            }
        }

        public void ExitAddProfile()
        {
            tbInputArea.Text = "";
            btInput.Click += new RoutedEventHandler(OfflineSelectProfileAddProfileOption);
            lbOptions.Items.Clear();
            lbOptions.Items.Add("1. Offline play");
            lbOptions.Items.Add("2. Select Profile");
            lbOptions.Items.Add("3. Add Profile");
        }

        public void ReAddProfileEmail()
        {
            tbInputArea.Text = "";
            btInput.Click += new RoutedEventHandler(AddProfileEmail);
            lbOptions.Items.Clear();
        }

        public void ExplainAddProfileConfirm()
        {
            lbDisplay.Items.Add("Yes will try to get the profile from our and then download all saves that exist there.");
            lbDisplay.Items.Add("No will cancel the Add Profile process and give you the previous 3 options.");
            lbDisplay.Items.Add("Change informatoin will send you back to the email adding step.");
            btInput.Click += new RoutedEventHandler(AddProfileConfirm);
            tbInputArea.Text = "";
        }

        //Main menu ends here ------------------------------------------------------------------------------------------

        private void lbOptions_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListBox lboptions = sender as ListBox;
            if (lboptions.SelectedItem != null)
            {
                string selectedItem = lboptions.SelectedItem as string;
                int dotintext = selectedItem.IndexOf('.');
                tbInputArea.Text = selectedItem.Substring(0, dotintext);
            }
        }

        public void WIP()
        {
            lbDisplay.Items.Add("This is the current extent of the project.");
            lbDisplay.Items.Add("The program will calculate damage after the press of the button in the bottom right.");
            lbDisplay.Items.Add("To choose the target write 1 or 2 in the textbox on the bottom of the screen.");
            lbOptions.Items.Add("1. (The target is the Monster)");
            lbOptions.Items.Add("2. (The target is the Hero)");
            if (tbInputArea.Text == "1")
            {
                damageSourcePrep = new DamageSource(npcs[0], 0);
                targetPrep = new Target(monsters[0]);
                lbDisplay.Items.Add(DamageCalculation(targetPrep, damageSourcePrep));
            }
            else if (tbInputArea.Text == "2")
            {
                damageSourcePrep = new DamageSource(monsters[0], monsters[0].Skills[0]);
                targetPrep = new Target(npcs[0]);
                lbDisplay.Items.Add(DamageCalculation(targetPrep, damageSourcePrep));
            }
            else
            {
                lbDisplay.Items.Add(" -- Please choose an option from the left. -- ");
            }
        }

        public int DamageCalculation(Target target, DamageSource damageSource)
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
            if (races[target.Race.Id].Fatal.Contains(damageSource.DamageType))
            {
                damage = damage * 2;
            }
            else if (races[target.Race.Id].Weak.Contains(damageSource.DamageType))
            {
                damage = (int)Math.Round(damage * 1.5, 0);
            }
            else if (races[target.Race.Id].Resist.Contains(damageSource.DamageType))
            {
                damage = (int)Math.Round(damage * 0.75, 0);
            }
            else if (races[target.Race.Id].Endure.Contains(damageSource.DamageType))
            {
                damage = (int)Math.Round(damage * 0.25, 0);
            }
            else if (races[target.Race.Id].Nulls.Contains(damageSource.DamageType))
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
            foreach (SpecialEffect specialEffect in damageSource.SpecialEffects)
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
        }

        //Downloader starts here ---------------------------------------------------------------------------------------

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

