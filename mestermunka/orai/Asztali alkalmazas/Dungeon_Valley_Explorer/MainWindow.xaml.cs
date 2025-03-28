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
using Mysqlx;
using Org.BouncyCastle.Bcpg;
using BCrypt.Net;
using System.Diagnostics;

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

        List<string> folders = new List<string> { "GameAssets","Enemies","Dungeons","Effects","Characters","Items","Abilities","EnvironmentHazard","Races","Profiles","Offline"};
        List<string> files = new List<string> { "Monsters.txt","Ais.txt","NPCs.txt","Dungeons.txt","EnvironmentHazards.txt","Passives.txt","BuffsDebuffs.txt","SpecialEffects.txt","Skills.txt","Magics.txt","Races.txt","Consumables.txt","Armors.txt","Weapons.txt"};
        List<string> tempProfiles = new List<string>();
        List<string> tempSaves = new List<string>();

        string addEmail = "";
        string addPassword = "";

        Hero newPlayerHero = new Hero();

        List<Hero> heroes = new List<Hero>();
        List<Hero> party = new List<Hero>();
        Dictionary<string, bool> questsCompleted = new Dictionary<string,bool>();
        Dictionary<string, int> consumables = new Dictionary<string, int>();
        bool newHero = false;
        int Gold = 0;
        int Experience = 0;
        Dictionary<string, bool> dungeonsCompleted = new Dictionary<string, bool>();
        Dictionary<string, int> weaponsImproved = new Dictionary<string, int>();
        Dictionary<string, int> armorsImproved = new Dictionary<string, int>();
        Dictionary<string, bool> weaponsObtained = new Dictionary<string, bool>();
        Dictionary<string, bool> armorsObtained = new Dictionary<string, bool>();
        Dictionary<string, bool> consumablesUnlocked = new Dictionary<string, bool>();

        List<string> classes = new List<string> { "Fighter","Hunter","Wizard","Paladin","Bounty Hunter","Warlock"};
        List<string> quests = new List<string> { "test"};
        List<string> physicalDamageTypes = new List<string> { "Blunt","Pierce","Slash"};
        List<string> magicalDamageTypes = new List<string> { "Fire"};
        bool skipDamageCalculation = false;
        Random random = new Random();
        Target targetPrep = new Target();
        DamageSource damageSourcePrep = new DamageSource();

        bool ShortDisplayNames = false;


        //Town Blacksmith variables ------------------------------------------------------------------------------------
        List<Weapon> purchasableWeapons = new List<Weapon>();
        Weapon buyWeaponSelectedWeapon = new Weapon();
        List<Armor> purchasableArmors = new List<Armor>();
        Armor buyArmorSelectedArmor = new Armor();
        List<Weapon> improvableWeapons = new List<Weapon>();
        Weapon improveWeaponSelectedWeapon = new Weapon();
        List<Armor> improvableArmors = new List<Armor>();
        Armor improveArmorSelectedArmor = new Armor();
        Hero selectedHero = new Hero();
        Weapon selectedWeapon = new Weapon();
        List<Weapon> selectableNewWeapons = new List<Weapon>();
        Weapon selectedNewWeapon = new Weapon();
        Armor selectedArmor = new Armor();
        List<Armor> selectableNewArmors = new List<Armor>();
        Armor selectedNewArmor = new Armor();
        //Town Blacksmith variables ------------------------------------------------------------------------------------
        public MainWindow()
        {
            InitializeComponent();

            if (!Directory.Exists($@"{folders[0]}") || !Directory.Exists($@"{folders[0]}\{folders[1]}") || !Directory.Exists($@"{folders[0]}\{folders[2]}") || !Directory.Exists($@"{folders[0]}\{folders[3]}") || !Directory.Exists($@"{folders[0]}\{folders[4]}") || !Directory.Exists($@"{folders[0]}\{folders[5]}") || !Directory.Exists($@"{folders[0]}\{folders[6]}") || !Directory.Exists($@"{folders[0]}\{folders[7]}") || !Directory.Exists($@"{folders[0]}\{folders[8]}") || !Directory.Exists($@"{folders[9]}") || !Directory.Exists($@"{folders[9]}\{folders[10]}") || !File.Exists($@"{folders[0]}\{folders[1]}\{files[0]}") || !File.Exists($@"{folders[0]}\{folders[1]}\{files[1]}") || !File.Exists($@"{folders[0]}\{folders[4]}\{files[2]}") || !File.Exists($@"{folders[0]}\{folders[2]}\{files[3]}") || !File.Exists($@"{folders[0]}\{folders[7]}\{files[4]}") || !File.Exists($@"{folders[0]}\{folders[3]}\{files[5]}") || !File.Exists($@"{folders[0]}\{folders[3]}\{files[6]}") || !File.Exists($@"{folders[0]}\{folders[3]}\{files[7]}") || !File.Exists($@"{folders[0]}\{folders[6]}\{files[8]}") || !File.Exists($@"{folders[0]}\{folders[6]}\{files[9]}") || !File.Exists($@"{folders[0]}\{folders[8]}\{files[10]}") || !File.Exists($@"{folders[0]}\{folders[5]}\{files[11]}") || !File.Exists($@"{folders[0]}\{folders[5]}\{files[12]}") || !File.Exists($@"{folders[0]}\{folders[5]}\{files[13]}"))
            {
                Downloader.Download(folders, files, mySqlConnection);
            }
            Initializer.Initialize(folders, files);
            Initializer.GetProfilesFromDevice(folders, lbOptions, tempProfiles);

            /*heroes.Add(new Hero("1@player@10@10@100@20@20@0@1@TestWeapon,Unarmed,Unarmed@Test Helmet,Test Chestplate,Test Leggings,Test Boots@Fighter@Power Slash@Firebolt,Self care@Sword Proficiency@Human", Initializer.passives, Initializer.skills, Initializer.magics, Initializer.races, Initializer.armors, Initializer.weapons));
            party.Add(new Hero("1@player@10@10@100@20@20@0@1@TestWeapon,Unarmed,Unarmed@Test Helmet,Test Chestplate,Test Leggings,Test Boots@Fighter@Power Slash@Firebolt,Self care@Sword Proficiency@Human", Initializer.passives, Initializer.skills, Initializer.magics, Initializer.races, Initializer.armors, Initializer.weapons));
            questsCompleted.Add("test", false);
            consumables.Add("Test Item", 1);
            Gold = 123;
            Experience = 100;
            dungeonsCompleted.Add("Test Place", false);
            weaponsImproved.Add("Unarmed", 0);
            weaponsImproved.Add("TestWeapon", 2);
            armorsImproved.Add("Test Helmet", 3);
            armorsImproved.Add("Test Chestplate", 0);
            armorsImproved.Add("Test Leggings", 0);
            armorsImproved.Add("Test Boots", 0);
            weaponsObtained.Add("TestWeapon", true);
            weaponsObtained.Add("Unarmed", true);
            armorsObtained.Add("Test Helmet", true);
            armorsObtained.Add("Test Chestplate", true);
            armorsObtained.Add("Test Leggings", true);
            armorsObtained.Add("Test Boots", true);
            consumablesUnlocked.Add("Test Item", true);

            files.Add("first_save.txt");
            folders.Add("Patrik05");
            EnterTown();*/

            lbDisplay.Items.Add("Welcome to Dungeon Valley Explorer!");
            lbDisplay.ScrollIntoView(lbDisplay.Items[lbDisplay.Items.Count - 1]);
            lbDisplay.Items.Add("Tip: To check if you have all the game assets downloaded just delete the GameAssets folder and download everything again.");
            lbDisplay.ScrollIntoView(lbDisplay.Items[lbDisplay.Items.Count - 1]);
            lbDisplay.Items.Add("Tip: To play with cloud saving you need to login to an account through the Select Profile option.");
            lbDisplay.ScrollIntoView(lbDisplay.Items[lbDisplay.Items.Count - 1]);
            lbDisplay.Items.Add("Tip: To progress write text based on the options on the far left into the area at the bottom of the window or select an option on the far left then press the input button. (This can be the number or the option as well example:'1'. 'Offline play')");
            lbDisplay.ScrollIntoView(lbDisplay.Items[lbDisplay.Items.Count - 1]);
            lbDisplay.Items.Add("Tip: To learn more about most options you can type '?' to get a short explanation.");
            lbDisplay.ScrollIntoView(lbDisplay.Items[lbDisplay.Items.Count - 1]);

            lbOptions.Items.Add("1. Offline play");
            lbOptions.Items.Add("2. Select Profile");
            lbOptions.Items.Add("3. Add Profile");
            lbOptions.Items.Add("4. Options");
            
            btInput.Click += new RoutedEventHandler(OfflineSelectProfileAddProfileOption);
        }

        //Main menu starts here ----------------------------------------------------------------------------------------

        public void OfflineSelectProfileAddProfileOption(object sender, RoutedEventArgs e)
        {
            btInput.Click -= new RoutedEventHandler(OfflineSelectProfileAddProfileOption);
            switch (tbInputArea.Text)
            {
                case "1":
                    OfflinePlay();
                    break;
                case "2":
                    SelectProfile();
                    break;
                case "3":
                    AddProfile();
                    break;
                case "4":
                    MainMenuOptions();
                    break;
                case "Offline play":
                    OfflinePlay();
                    break;
                case "Select Profile":
                    SelectProfile();
                    break;
                case "Add Profile":
                    AddProfile();
                    break;
                case "Options":
                    MainMenuOptions();
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

        public void ExplainOfflineLoginAddProfileOption()
        {
            lbDisplay.Items.Add("Offline play allows you to play without an account on this computer.");
            lbDisplay.ScrollIntoView(lbDisplay.Items[lbDisplay.Items.Count - 1]);
            lbDisplay.Items.Add("Select Profile allows you to select an already added profile to use the cloud save feature so that you can access your saves from different computers.");
            lbDisplay.ScrollIntoView(lbDisplay.Items[lbDisplay.Items.Count - 1]);
            lbDisplay.Items.Add("Add Profile allows you to add a existing account from our database so that you can create saves that have access to cloud saving and get access to existing cloud saves.");
            lbDisplay.ScrollIntoView(lbDisplay.Items[lbDisplay.Items.Count - 1]);
            lbDisplay.Items.Add("Options allows you to change some things about the game like for example change the color from black to white.");
            lbDisplay.ScrollIntoView(lbDisplay.Items[lbDisplay.Items.Count - 1]);
            btInput.Click += new RoutedEventHandler(OfflineSelectProfileAddProfileOption);
            tbInputArea.Text = "";
        }

        public void ReturnToOfflineSelectProfileAddProfileOption()
        {
            tbInputArea.Text = "";
            lbOptions.Items.Clear();
            lbOptions.Items.Add("1. Offline play");
            lbOptions.Items.Add("2. Select Profile");
            lbOptions.Items.Add("3. Add Profile");
            lbOptions.Items.Add("4. Options");
            btInput.Click += new RoutedEventHandler(OfflineSelectProfileAddProfileOption);
        }

        //Options starts here ------------------------------------------------------------------------------------------

        public void MainMenuOptions()
        {
            tbInputArea.Text = "";
            lbOptions.Items.Clear();
            lbOptions.Items.Add("1. Change Colors");
            lbOptions.Items.Add($"2. Shortened Names ({ShortDisplayNames})");
            lbOptions.Items.Add("3. Back");
            lbDisplay.Items.Add("Currently you can only change from dark mode to light mode or vice versa.");
            lbDisplay.ScrollIntoView(lbDisplay.Items[lbDisplay.Items.Count - 1]);
            btInput.Click += new RoutedEventHandler(MainMenuOptionsOptions);
        }

        public void MainMenuOptionsOptions(object sender, RoutedEventArgs e)
        {
            btInput.Click -= new RoutedEventHandler(MainMenuOptionsOptions);
            switch (tbInputArea.Text)
            {
                case "?":
                    ExplainMainMenuOptionsOptions();
                    break;
                case "1":
                    OptionsChangeColors();
                    break; 
                case "2":
                    OptionsShortenedNames();
                    break;
                case "3":
                    ReturnToOfflineSelectProfileAddProfileOption();
                    break;
                case "Change Colors":
                    OptionsChangeColors();
                    break;
                case "Shortened Names":
                    OptionsShortenedNames();
                    break;
                case "Back":
                    ReturnToOfflineSelectProfileAddProfileOption();
                    break;
                default:
                    MessageBox.Show("Please use the textbox at the bottom of the window to write a valid option from the left.");
                    btInput.Click += new RoutedEventHandler(MainMenuOptionsOptions);
                    break;
            } 
        }

        public void ExplainMainMenuOptionsOptions()
        {
            lbDisplay.Items.Add("Change colors will change everything from black to white and vice versa.");
            lbDisplay.ScrollIntoView(lbDisplay.Items[lbDisplay.Items.Count - 1]);
            lbDisplay.Items.Add("Shortened Names will make the friendly npcs and the hero use their first names when written onto a line. The true and false next to it shows the current state of the option. (Cuts off the name after the first 'Space' it finds)");
            lbDisplay.ScrollIntoView(lbDisplay.Items[lbDisplay.Items.Count - 1]);
            lbDisplay.Items.Add("Back will take you back to the first option.");
            lbDisplay.ScrollIntoView(lbDisplay.Items[lbDisplay.Items.Count - 1]);
            lbDisplay.Items.Add("There will also be more fun little things that can be change from here. (But these won't be priority)");
            lbDisplay.ScrollIntoView(lbDisplay.Items[lbDisplay.Items.Count - 1]);
            btInput.Click += new RoutedEventHandler(MainMenuOptionsOptions);
            tbInputArea.Text = "";
        }

        public void OptionsShortenedNames()
        {
            lbOptions.Items.Clear();
            if (ShortDisplayNames == false)
            {
                ShortDisplayNames = true;
            }
            else
            {
                ShortDisplayNames = false;
            }
            lbOptions.Items.Add("1. Change Colors");
            lbOptions.Items.Add($"2. Shortened Names ({ShortDisplayNames})");
            lbOptions.Items.Add("3. Back");
            btInput.Click += new RoutedEventHandler(MainMenuOptionsOptions);
            tbInputArea.Text = "";
        }

        public void OptionsChangeColors()
        {
            lbOptions.Items.Clear();
            lbOptions.Items.Add("1. Change Colors");
            lbOptions.Items.Add("2. Shortened Names");
            lbOptions.Items.Add("3. Back");
            if (lbDisplay.Background == Brushes.Black)
            {
                lbDisplay.Background = Brushes.White;
                lbDisplay.Foreground = Brushes.Black;
                lbOptions.Background = Brushes.White;
                lbOptions.Foreground = Brushes.Black;
                tbInputArea.Background = Brushes.White;
                tbInputArea.Foreground = Brushes.Black;
            }
            else
            {
                lbDisplay.Background = Brushes.Black;
                lbDisplay.Foreground = Brushes.White;
                lbOptions.Background = Brushes.Black;
                lbOptions.Foreground = Brushes.White;
                tbInputArea.Background = Brushes.Black;
                tbInputArea.Foreground = Brushes.White;
            }
            tbInputArea.Text = "";
            btInput.Click += new RoutedEventHandler(MainMenuOptionsOptions);
        }

        //Options ends here --------------------------------------------------------------------------------------------

        //Offline play starts here -------------------------------------------------------------------------------------

        public void OfflinePlay()
        {
            tbInputArea.Text = "";
            lbOptions.Items.Clear();
            lbDisplay.Items.Add("Please select a save from the left or start a new game.");
            lbDisplay.ScrollIntoView(lbDisplay.Items[lbDisplay.Items.Count - 1]);
            lbOptions.Items.Add("1. New Game");
            lbOptions.Items.Add("2. Back");
            GetProfileSaves();
            btInput.Click += new RoutedEventHandler(OfflinePlayChooseSave);
        }

        public void OfflinePlayChooseSave(object sender, RoutedEventArgs e)
        {
            btInput.Click -= new RoutedEventHandler(OfflinePlayChooseSave);
            if (tbInputArea.Text == "?")
            {
                ExplainOfflinePlayChooseSave();
                btInput.Click += new RoutedEventHandler(OfflinePlayChooseSave);
            }
            else if (tbInputArea.Text == "1" || tbInputArea.Text == "New Game")
            {
                CreateNewGame();
            }
            else if (tbInputArea.Text == "2" || tbInputArea.Text == "Back")
            {
                if (folders.Last() != folders[10])
                {
                    folders.Remove(folders.Last());
                }
                ReturnToOfflineSelectProfileAddProfileOption();
            }
            else
            {
                if (tempSaves.Contains(tbInputArea.Text) == true || (tbInputArea.Text.Contains("1") == true || tbInputArea.Text.Contains("2") == true || tbInputArea.Text.Contains("3") == true || tbInputArea.Text.Contains("4") == true || tbInputArea.Text.Contains("5") == true || tbInputArea.Text.Contains("6") == true || tbInputArea.Text.Contains("7") == true || tbInputArea.Text.Contains("8") == true || tbInputArea.Text.Contains("9") == true || tbInputArea.Text.Contains("0") == true))
                {
                    if (tempSaves.Contains(tbInputArea.Text) == true)
                    {
                        files.Add(tbInputArea.Text);
                        lbOptions.Items.Clear();
                        EnterTown();
                    }
                    else
                    {
                        try
                        {
                            int savesIndex = Convert.ToInt32(tbInputArea.Text) - 3;
                            files.Add(tempSaves[savesIndex]);
                            lbOptions.Items.Clear();
                            LoadExistingSave();
                            EnterTown();
                        }
                        catch
                        {
                            MessageBox.Show("Please use the textbox at the bottom of the window to write a valid option from the left.");
                            btInput.Click += new RoutedEventHandler(OfflinePlayChooseSave);
                            tbInputArea.Text = "";
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Please use the textbox at the bottom of the window to write a valid option from the left.");
                    btInput.Click += new RoutedEventHandler(OfflinePlayChooseSave);
                    tbInputArea.Text = "";
                }
            }
        }

        public void ExplainOfflinePlayChooseSave()
        {
            lbDisplay.Items.Add("New game will start a new game that you can use through Offline play later and load from your local saves");
            lbDisplay.ScrollIntoView(lbDisplay.Items[lbDisplay.Items.Count - 1]);
            lbDisplay.Items.Add("Back will take you back to the first option.");
            lbDisplay.ScrollIntoView(lbDisplay.Items[lbDisplay.Items.Count - 1]);
            lbDisplay.Items.Add("All other options are saves from your local Offline folder.");
            lbDisplay.ScrollIntoView(lbDisplay.Items[lbDisplay.Items.Count - 1]);
            tbInputArea.Text = "";
        }

        //Offline play ends here ---------------------------------------------------------------------------------------

        //Profile selection starts here --------------------------------------------------------------------------------

        public void SelectProfile()
        {
            tbInputArea.Text = "";
            lbOptions.Items.Clear();
            for (int i = 0; i < tempProfiles.Count(); i++)
            {
                lbOptions.Items.Add($"{i + 1}. " + tempProfiles[i]);
            }
            if (lbOptions.Items.IsEmpty == true)
            {
                lbDisplay.Items.Add("There are no profiles added to the game yet, please add a profile through the Add Profile option to add a profile or choose Offline play and play using the local saves.");
                lbDisplay.ScrollIntoView(lbDisplay.Items[lbDisplay.Items.Count - 1]);
                ReturnToOfflineSelectProfileAddProfileOption();
            }
            else
            {
                lbDisplay.Items.Add("Please select a profile from the left.");
                lbDisplay.ScrollIntoView(lbDisplay.Items[lbDisplay.Items.Count - 1]);
                btInput.Click += new RoutedEventHandler(SelectProfileChooseProfile);
            }
        }

        public void SelectProfileChooseProfile(object sender, RoutedEventArgs e)
        {
            btInput.Click -= new RoutedEventHandler(SelectProfileChooseProfile);
            if (tbInputArea.Text == "?")
            {
                ExplainSelectProfileChooseProfile();
                btInput.Click += new RoutedEventHandler(SelectProfileChooseProfile);
            }
            else
            {
                if (tempProfiles.Contains(tbInputArea.Text) == true || (tbInputArea.Text.Contains("1") == true || tbInputArea.Text.Contains("2") == true || tbInputArea.Text.Contains("3") == true || tbInputArea.Text.Contains("4") == true || tbInputArea.Text.Contains("5") == true || tbInputArea.Text.Contains("6") == true || tbInputArea.Text.Contains("7") == true || tbInputArea.Text.Contains("8") == true || tbInputArea.Text.Contains("9") == true || tbInputArea.Text.Contains("0") == true))
                {
                    if (tempProfiles.Contains(tbInputArea.Text) == true)
                    {
                        
                        folders.Add(tbInputArea.Text);
                        lbDisplay.Items.Add("Will you login with this profile? You can also write 'Back' to cancel.");
                        lbDisplay.ScrollIntoView(lbDisplay.Items[lbDisplay.Items.Count - 1]);
                        lbOptions.Items.Clear();
                        lbOptions.Items.Add("1. Login");
                        lbOptions.Items.Add("2. Back");
                        btInput.Click += new RoutedEventHandler(SelectProfileLoginOrBack);
                        tbInputArea.Text = "";
                    }
                    else
                    {
                        try
                        {
                            int profileIndex = Convert.ToInt32(tbInputArea.Text)-1;
                            folders.Add(tempProfiles[profileIndex]);
                            lbDisplay.Items.Add("Will you login with this profile? You can also write 'Back' to cancel.");
                            lbDisplay.ScrollIntoView(lbDisplay.Items[lbDisplay.Items.Count - 1]);
                            lbOptions.Items.Clear();
                            lbOptions.Items.Add("1. Login");
                            lbOptions.Items.Add("2. Back");
                            btInput.Click += new RoutedEventHandler(SelectProfileLoginOrBack);
                            tbInputArea.Text = "";
                        }
                        catch
                        {
                            MessageBox.Show("Please use the textbox at the bottom of the window to write a valid option from the left.");
                            btInput.Click += new RoutedEventHandler(SelectProfileChooseProfile);
                            tbInputArea.Text = "";
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Please use the textbox at the bottom of the window to write a valid option from the left.");
                    btInput.Click += new RoutedEventHandler(SelectProfileChooseProfile);
                    tbInputArea.Text = "";
                }
            }
        }

        public void ExplainSelectProfileChooseProfile()
        {
            lbDisplay.Items.Add("Each option is a profile added to the game, and by choosing one of them you can login to them and use cloud saving to access saves from our database from anywhere.");
            lbDisplay.ScrollIntoView(lbDisplay.Items[lbDisplay.Items.Count - 1]);
            tbInputArea.Text = "";
        }

        public void SelectProfileLoginOrBack(object sender, RoutedEventArgs e)
        {
            btInput.Click -= new RoutedEventHandler(SelectProfileLoginOrBack);
            switch (tbInputArea.Text)
            {
                case "?":
                    ExplainSelectProfileLoginOrBack();
                    break;
                case "1":
                    tbInputArea.Text = "";
                    lbOptions.Items.Clear();
                    lbDisplay.Items.Add("To login to a profile you will have to first write down your email then write down your password into the textbox and click the input button after both the email and the password. (You can also write 'Back' to go back.).");
                    lbDisplay.ScrollIntoView(lbDisplay.Items[lbDisplay.Items.Count - 1]);
                    btInput.Click += new RoutedEventHandler(SelectProfileEmail);
                    break;
                case "2":
                    if (folders.Last() != folders[10])
                    {
                        folders.Remove(folders.Last());
                    }
                    ReturnToOfflineSelectProfileAddProfileOption();
                    break;
                case "Login":
                    tbInputArea.Text = "";
                    lbOptions.Items.Clear();
                    lbDisplay.Items.Add("To login to a profile you will have to first write down your email then write down your password into the textbox and click the input button after both the email and the password. (You can also write 'Back' to go back.).");
                    lbDisplay.ScrollIntoView(lbDisplay.Items[lbDisplay.Items.Count - 1]);
                    btInput.Click += new RoutedEventHandler(SelectProfileEmail);
                    break;
                case "Back":
                    if (folders.Last() != folders[10])
                    {
                        folders.Remove(folders.Last());
                    }
                    ReturnToOfflineSelectProfileAddProfileOption();
                    break;
                default:
                    MessageBox.Show("Please use the textbox at the bottom of the window to write a valid option from the left.");
                    btInput.Click += new RoutedEventHandler(SelectProfileLoginOrBack);
                    break;
            }
        }

        public void ExplainSelectProfileLoginOrBack()
        {
            lbDisplay.Items.Add("Login will allow you to try and access saves that have cloud save compatibility.");
            lbDisplay.ScrollIntoView(lbDisplay.Items[lbDisplay.Items.Count - 1]);
            lbDisplay.Items.Add("Back will take you back to the first option.");
            lbDisplay.ScrollIntoView(lbDisplay.Items[lbDisplay.Items.Count - 1]);
            tbInputArea.Text = "";
            btInput.Click += new RoutedEventHandler(SelectProfileLoginOrBack);
        }

        public void SelectProfileEmail(object sender, RoutedEventArgs e)
        {
            btInput.Click -= new RoutedEventHandler(SelectProfileEmail);
            if (!tbInputArea.Text.Contains('@') || !tbInputArea.Text.Contains('.'))
            {
                switch (tbInputArea.Text)
                {
                    case "Back":
                        ReturnToOfflineSelectProfileAddProfileOption();
                        break;
                    default:
                        MessageBox.Show("Please write down the email for the profile or write 'Back' to cancel.");
                        btInput.Click += new RoutedEventHandler(SelectProfileEmail);
                        break;
                }
            }
            else if (tbInputArea.Text.Contains('@') && tbInputArea.Text.Contains('.'))
            {
                addEmail = tbInputArea.Text;
                lbDisplay.Items.Add("Now please write down the password.");
                lbDisplay.ScrollIntoView(lbDisplay.Items[lbDisplay.Items.Count - 1]);
                btInput.Click += new RoutedEventHandler(SelectProfilePassword);
                tbInputArea.Text = "";
            }
        }

        public void SelectProfilePassword(object sender, RoutedEventArgs e)
        {
            btInput.Click -= new RoutedEventHandler(SelectProfilePassword);
            switch (tbInputArea.Text)
            {
                case "Back":
                    ReturnToOfflineSelectProfileAddProfileOption();
                    break;
                default:
                    addPassword = tbInputArea.Text;
                    tbInputArea.Text = "";
                    SelectProfileLogin();
                    break;
            }
        }

        public void SelectProfileLogin()
        {
            string command = $"Select UserName, Password from user where Email = '{addEmail}' limit 1";
            MySqlCommand mySqlCommand = new MySqlCommand(command, mySqlConnection);
            try
            {
                mySqlConnection.Open();
                MySqlDataReader mySqlDataReader = mySqlCommand.ExecuteReader();
                bool foundProfile = false;
                while (mySqlDataReader.Read())
                {
                    string hashedPassword = mySqlDataReader.GetString(1);
                    if (mySqlDataReader.GetString(0) == folders.Last() && CheckHashedPassword(addPassword, hashedPassword) == true)
                    {
                        GetProfileSaves();
                        foundProfile = true;
                    }
                }

                if (lbOptions.Items.IsEmpty == true && foundProfile == true)
                {
                    lbDisplay.Items.Add("No saves were detected for this profile please start a new game and get to the town to create a cloud save for later use.");
                    lbDisplay.ScrollIntoView(lbDisplay.Items[lbDisplay.Items.Count - 1]);
                    lbOptions.Items.Add("1. New Game");
                    lbOptions.Items.Add("2. Log out");
                    btInput.Click += new RoutedEventHandler(SelectProfileCreateSave);
                }
                else if (foundProfile == true)
                {
                    lbDisplay.Items.Add("Please select a save from the left or start a new game.");
                    lbDisplay.ScrollIntoView(lbDisplay.Items[lbDisplay.Items.Count - 1]);
                    lbOptions.Items.Clear();
                    tempSaves.Clear();
                    lbOptions.Items.Add("1. New Game");
                    lbOptions.Items.Add("2. Log out");
                    GetProfileSaves();
                    btInput.Click += new RoutedEventHandler(SelectProfileChooseSave);
                }
                else
                {
                    MessageBox.Show("The email or password was incorrect for the selected profile.");
                    lbOptions.Items.Add("1. Login");
                    lbOptions.Items.Add("2. Back");
                    btInput.Click += new RoutedEventHandler(SelectProfileLoginOrBack);
                }

                mySqlConnection.Close();
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message);
                lbOptions.Items.Add("1. Login");
                lbOptions.Items.Add("2. Back");
                btInput.Click += new RoutedEventHandler(SelectProfileLoginOrBack);
            }
        }

        public void GetProfileSaves()
        {
            string[] seged = Directory.GetFiles($@"{folders[9]}\{folders.Last()}");
            for (int i = 0; i < seged.Count(); i++)
            {
                string[] linecutter = seged[i].Split('\\');
                lbOptions.Items.Add($"{i + 3}. " + linecutter[2].Substring(0,linecutter[2].Length-4));
                tempSaves.Add(linecutter[2]);
            }
        }

        public void SelectProfileChooseSave(object sender, RoutedEventArgs e)
        {
            btInput.Click -= new RoutedEventHandler(SelectProfileChooseSave);
            if (tbInputArea.Text == "?")
            {
                ExplainSelectProfileChooseSave();
                btInput.Click += new RoutedEventHandler(SelectProfileChooseSave);
            }
            else if (tbInputArea.Text == "1" || tbInputArea.Text == "New Game")
            {
                CreateNewGame();
            }
            else if (tbInputArea.Text == "2" || tbInputArea.Text == "Log out")
            {
                if (folders.Last() != folders[10])
                {
                    folders.Remove(folders.Last());
                }
                tempSaves.Clear();
                ReturnToOfflineSelectProfileAddProfileOption();
            }
            else
            {
                if (tempSaves.Contains(tbInputArea.Text) == true || (tbInputArea.Text.Contains("1") == true || tbInputArea.Text.Contains("2") == true || tbInputArea.Text.Contains("3") == true || tbInputArea.Text.Contains("4") == true || tbInputArea.Text.Contains("5") == true || tbInputArea.Text.Contains("6") == true || tbInputArea.Text.Contains("7") == true || tbInputArea.Text.Contains("8") == true || tbInputArea.Text.Contains("9") == true || tbInputArea.Text.Contains("0") == true))
                {
                    if (tempSaves.Contains(tbInputArea.Text) == true)
                    {
                        files.Add(tbInputArea.Text);
                        lbOptions.Items.Clear();
                        LoadExistingSave();
                        EnterTown();
                    }
                    else
                    {
                        try
                        {
                            int savesIndex = Convert.ToInt32(tbInputArea.Text) - 3;
                            files.Add(tempSaves[savesIndex]);
                            lbOptions.Items.Clear();
                            LoadExistingSave();
                            EnterTown();
                        }
                        catch
                        {
                            MessageBox.Show("Please use the textbox at the bottom of the window to write a valid option from the left.");
                            btInput.Click += new RoutedEventHandler(SelectProfileChooseSave);
                            tbInputArea.Text = "";
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Please use the textbox at the bottom of the window to write a valid option from the left.");
                    btInput.Click += new RoutedEventHandler(SelectProfileChooseSave);
                    tbInputArea.Text = "";
                }
            }
        }

        public void ExplainSelectProfileChooseSave()
        {
            lbDisplay.Items.Add("New game will start a new game that you can use for cloud saves later and load from here as well.");
            lbDisplay.ScrollIntoView(lbDisplay.Items[lbDisplay.Items.Count - 1]);
            lbDisplay.Items.Add("Log out will take you back to the first option and remove the selected profile selection meaning you will have to login to it again.");
            lbDisplay.ScrollIntoView(lbDisplay.Items[lbDisplay.Items.Count - 1]);
            lbDisplay.Items.Add("All other options are saves from your selected profile.");
            lbDisplay.ScrollIntoView(lbDisplay.Items[lbDisplay.Items.Count - 1]);
            tbInputArea.Text = "";
        }

        public void SelectProfileCreateSave(object sender, RoutedEventArgs e)
        {
            btInput.Click -= new RoutedEventHandler(SelectProfileCreateSave);
            switch (tbInputArea.Text)
            {
                case "?":
                    ExplainSelectProfileCreateSave();
                    btInput.Click += new RoutedEventHandler(SelectProfileCreateSave);
                    break;
                case "1":
                    CreateNewGame();
                    break;
                case "2":
                    if (folders.Last() != folders[10])
                    {
                        folders.Remove(folders.Last());
                    }
                    ReturnToOfflineSelectProfileAddProfileOption();
                    break;
                case "New game":
                    CreateNewGame();
                    break;
                case "Log out":
                    if (folders.Last() != folders[10])
                    {
                        folders.Remove(folders.Last());
                    }
                    ReturnToOfflineSelectProfileAddProfileOption();
                    break;
                default:
                    MessageBox.Show("Please use the textbox at the bottom of the window to write a valid option from the left.");
                    btInput.Click += new RoutedEventHandler(SelectProfileCreateSave);
                    break;
            }
        }

        public void ExplainSelectProfileCreateSave()
        {
            lbDisplay.Items.Add("New game will start a new game that you can use for cloud saves later and load from here as well.");
            lbDisplay.ScrollIntoView(lbDisplay.Items[lbDisplay.Items.Count - 1]);
            lbDisplay.Items.Add("Log out will take you back to the first option and remove the selected profile selection meaning you will have to login to it again.");
            lbDisplay.ScrollIntoView(lbDisplay.Items[lbDisplay.Items.Count - 1]);
            tbInputArea.Text = "";
        }

        //Profile selection ends here ----------------------------------------------------------------------------------

        //Profile adding starts here -----------------------------------------------------------------------------------

        public void AddProfile()
        {
            tbInputArea.Text = "";
            lbDisplay.Items.Add("To add a profile you will have to first write down your email then write down your password into the textbox and click the input button after both the email and the password. (You can also write 'Back' to go back.).");
            lbDisplay.ScrollIntoView(lbDisplay.Items[lbDisplay.Items.Count - 1]);
            btInput.Click += new RoutedEventHandler(AddProfileEmail);
            lbOptions.Items.Clear();
        }

        public void AddProfileEmail(object sender, RoutedEventArgs e)
        {
            btInput.Click -= new RoutedEventHandler(AddProfileEmail);
            if (!tbInputArea.Text.Contains('@') || !tbInputArea.Text.Contains('.'))
            {
                switch (tbInputArea.Text)
                {
                    case "Back":
                        ReturnToOfflineSelectProfileAddProfileOption();
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
                lbDisplay.ScrollIntoView(lbDisplay.Items[lbDisplay.Items.Count - 1]);
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
                    ReturnToOfflineSelectProfileAddProfileOption();
                    break;
                default:
                    addPassword = tbInputArea.Text;
                    lbDisplay.Items.Add($"Are you sure you want to add this profile? (Email: {addEmail},Password: {addPassword})");
                    lbDisplay.ScrollIntoView(lbDisplay.Items[lbDisplay.Items.Count - 1]);
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
            string command = $"Select UserName, Password from user where Email = '{addEmail}' limit 1";
            MySqlCommand mySqlCommand = new MySqlCommand(command, mySqlConnection);
            try
            {
                mySqlConnection.Open();
                MySqlDataReader mySqlDataReader = mySqlCommand.ExecuteReader();
                while (mySqlDataReader.Read())
                {
                    string hashedPassword = mySqlDataReader.GetString(1);
                    if (CheckHashedPassword(addPassword, hashedPassword) == true)
                    {
                        folders.Add(mySqlDataReader.GetString(0));
                    }
                }
                mySqlConnection.Close();

                if (!Directory.Exists($@"{folders[9]}\{folders.Last()}") && folders.Last() != folders[10])
                {
                    Directory.CreateDirectory($@"{folders[9]}\{folders.Last()}");
                    AddProfileGetProfileSaves();
                    folders.Remove(folders[folders.Count() - 1]);
                    MessageBox.Show("The profile has been added successfully.");

                }
                else if (Directory.Exists($@"{folders[9]}\{folders.Last()}") && folders.Last() != folders[10])
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
            Initializer.GetProfilesFromDevice(folders, lbOptions, tempProfiles);
            ReturnToOfflineSelectProfileAddProfileOption();
        }

        public void AddProfileGetProfileSaves()
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

                if (files.Last() != files[13])
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
                mySqlConnection.Close();
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message);
            }
        }

        public void ExitAddProfile()
        {
            ReturnToOfflineSelectProfileAddProfileOption();
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
            lbDisplay.ScrollIntoView(lbDisplay.Items[lbDisplay.Items.Count - 1]);
            lbDisplay.Items.Add("No will cancel the Add Profile process and give you the previous 3 options.");
            lbDisplay.ScrollIntoView(lbDisplay.Items[lbDisplay.Items.Count - 1]);
            lbDisplay.Items.Add("Change informatoin will send you back to the email adding step.");
            lbDisplay.ScrollIntoView(lbDisplay.Items[lbDisplay.Items.Count - 1]);
            btInput.Click += new RoutedEventHandler(AddProfileConfirm);
            tbInputArea.Text = "";
        }

        //Profile adding ends here -------------------------------------------------------------------------------------

        public bool CheckHashedPassword(string password, string hashedPassword)
        {
            bool verifyPassword = BCrypt.Net.BCrypt.Verify(password, hashedPassword);
            return verifyPassword;
        }

        //Main menu ends here ------------------------------------------------------------------------------------------

        //Game starting starts here ------------------------------------------------------------------------------------

        //New Game starts here -----------------------------------------------------------------------------------------

        public void CreateNewGame()
        {
            lbOptions.Items.Clear();
            tbInputArea.Text = "";
            lbDisplay.Items.Add("'Back' and other options of the same nature will no longer exist for a lot of options so keep in mind that you will not be able to cancel and back out of options if you don't see that option on the left.");
            lbDisplay.ScrollIntoView(lbDisplay.Items[lbDisplay.Items.Count - 1]);
            lbDisplay.Items.Add("Before starting the game you need to make a character of your own.");
            lbDisplay.ScrollIntoView(lbDisplay.Items[lbDisplay.Items.Count - 1]);
            lbDisplay.Items.Add("The first step is to choose a class for yourself and keep in mind that you can't change this choice for a long time.");
            lbDisplay.ScrollIntoView(lbDisplay.Items[lbDisplay.Items.Count - 1]);
            lbOptions.Items.Add("1. Fighter");
            lbOptions.Items.Add("2. Hunter");
            lbOptions.Items.Add("3. Wizard");
            lbOptions.Items.Add("4. Paladin");
            lbOptions.Items.Add("5. Bounty Hunter");
            lbOptions.Items.Add("6. Warlock");
            btInput.Click += new RoutedEventHandler(NewGameCharacterClassSelection);
        }

        public void NewGameCharacterClassSelection(object sender, RoutedEventArgs e)
        {
            btInput.Click -= new RoutedEventHandler(NewGameCharacterClassSelection);
            switch (tbInputArea.Text)
            {
                case "?":
                    ExplainNewGameCharacterClassSelection();
                    break;
                case "1":
                    newPlayerHero.heroClass = "Fighter";
                    NewGameSetCharacterClass();
                    NewGameCharacterBackground();
                    break;
                case "Fighter":
                    newPlayerHero.heroClass = "Fighter";
                    NewGameSetCharacterClass();
                    NewGameCharacterBackground();
                    break;
                case "2":
                    newPlayerHero.heroClass = "Hunter";
                    NewGameSetCharacterClass();
                    NewGameCharacterBackground();
                    break;
                case "Hunter":
                    newPlayerHero.heroClass = "Hunter";
                    NewGameSetCharacterClass();
                    NewGameCharacterBackground();
                    break;
                case "3":
                    newPlayerHero.heroClass = "Wizard";
                    NewGameSetCharacterClass();
                    NewGameCharacterBackground();
                    break;
                case "Wizard":
                    newPlayerHero.heroClass = "Wizard";
                    NewGameSetCharacterClass();
                    NewGameCharacterBackground();
                    break;
                case "4":
                    newPlayerHero.heroClass = "Paladin";
                    NewGameSetCharacterClass();
                    NewGameCharacterBackground();
                    break;
                case "Paladin":
                    newPlayerHero.heroClass = "Paladin";
                    NewGameSetCharacterClass();
                    NewGameCharacterBackground();
                    break;
                case "5":
                    newPlayerHero.heroClass = "Bounty Hunter";
                    NewGameSetCharacterClass();
                    NewGameCharacterBackground();
                    break;
                case "Bounty Hunter":
                    newPlayerHero.heroClass = "Bounty Hunter";
                    NewGameSetCharacterClass();
                    NewGameCharacterBackground();
                    break;
                case "6":
                    newPlayerHero.heroClass = "Warlock";
                    NewGameSetCharacterClass();
                    NewGameCharacterBackground();
                    break;
                case "Warlock":
                    newPlayerHero.heroClass = "Warlock";
                    NewGameSetCharacterClass();
                    NewGameCharacterBackground();
                    break;
                default:
                    MessageBox.Show("Please use the textbox at the bottom of the window to write a valid option from the left.");
                    btInput.Click += new RoutedEventHandler(NewGameCharacterClassSelection);
                    break;
            }
        }

        public void NewGameSetCharacterClass()
        {
            switch (newPlayerHero.heroClass)
            {
                case "Fighter":
                    Hero.SetClassFighter(newPlayerHero);
                    break;
                case "Hunter":
                    Hero.SetClassHunter(newPlayerHero);
                    break;
                case "Wizard":
                    Hero.SetClassWizard(newPlayerHero);
                    break;
                case "Paladin":
                    Hero.SetClassPaladin(newPlayerHero);
                    break;
                case "Bounty Hunter":
                    Hero.SetClassBountyHunter(newPlayerHero);
                    break;
                case "Warlock":
                    Hero.SetClassWarlock(newPlayerHero);
                    break;
            }
        }

        public void ExplainNewGameCharacterClassSelection()
        {
            tbInputArea.Text = "";
            lbDisplay.Items.Add("Each class option will determine your characters basic role in the game as well as your starting stats.");
            lbDisplay.ScrollIntoView(lbDisplay.Items[lbDisplay.Items.Count - 1]);
            lbDisplay.Items.Add("Fighter is a basic melee class that focuses on a balanced build with a focus on physical damage output.");
            lbDisplay.ScrollIntoView(lbDisplay.Items[lbDisplay.Items.Count - 1]);
            lbDisplay.Items.Add("Hunter is a basic ranged class that focuses on a more frail build with a focus on physical damage output with devastating crits.");
            lbDisplay.ScrollIntoView(lbDisplay.Items[lbDisplay.Items.Count - 1]);
            lbDisplay.Items.Add("Wizard is a basic magic class that focuses on magic versatility with a focus on versatile use of magic for both offense and support.");
            lbDisplay.ScrollIntoView(lbDisplay.Items[lbDisplay.Items.Count - 1]);
            lbDisplay.Items.Add("Paladin is a slightly more advanced class that focuses on tanking and bursts of damage they rely on heavy defense and mostly holy magic.");
            lbDisplay.ScrollIntoView(lbDisplay.Items[lbDisplay.Items.Count - 1]);
            lbDisplay.Items.Add("Bounty Hunter is a more advanced class that focuses on marking and targeting enemies they work alone but are stronger with a team.");
            lbDisplay.ScrollIntoView(lbDisplay.Items[lbDisplay.Items.Count - 1]);
            lbDisplay.Items.Add("Warlock is a slightly more advanced class that focuses on destructive magic they use powerful magic at the cost of survivability.");
            lbDisplay.ScrollIntoView(lbDisplay.Items[lbDisplay.Items.Count - 1]);
            btInput.Click += new RoutedEventHandler(NewGameCharacterClassSelection);
        }

        public void NewGameCharacterBackground()
        {
            tbInputArea.Text = "";
            lbOptions.Items.Clear();
            lbDisplay.Items.Add("Next you will choose a background for your character. This is a passive that your character will recieve.");
            lbDisplay.ScrollIntoView(lbDisplay.Items[lbDisplay.Items.Count - 1]);
            lbOptions.Items.Add("1. Adventurer");
            lbOptions.Items.Add("2. Noble");
            lbOptions.Items.Add("3. Merchant");
            lbOptions.Items.Add("4. Blacksmith");
            btInput.Click += new RoutedEventHandler(NewGameCharacterBackgroundSelection);
        }

        public void NewGameCharacterBackgroundSelection(object sender, RoutedEventArgs e)
        {
            btInput.Click -= new RoutedEventHandler(NewGameCharacterBackgroundSelection);
            switch (tbInputArea.Text)
            {
                case "?":
                    ExplainNewGameCharacterBackgroundSelection();
                    break;
                case "1":
                    NewGameSetCharacterBackground("Adventurer");
                    NewGameCharacterRace();
                    break;
                case "Adventurer":
                    NewGameSetCharacterBackground("Adventurer");
                    NewGameCharacterRace();
                    break;
                case "2":
                    NewGameSetCharacterBackground("Noble");
                    NewGameCharacterRace();
                    break;
                case "Noble":
                    NewGameSetCharacterBackground("Noble");
                    NewGameCharacterRace();
                    break;
                case "3":
                    NewGameSetCharacterBackground("Merchant");
                    NewGameCharacterRace();
                    break;
                case "Merchant":
                    NewGameSetCharacterBackground("Merchant");
                    NewGameCharacterRace();
                    break;
                case "4":
                    NewGameSetCharacterBackground("Blacksmith");
                    NewGameCharacterRace();
                    break;
                case "Blacksmith":
                    NewGameSetCharacterBackground("Blacksmith");
                    NewGameCharacterRace();
                    break;
                default:
                    MessageBox.Show("Please use the textbox at the bottom of the window to write a valid option from the left.");
                    btInput.Click += new RoutedEventHandler(NewGameCharacterBackgroundSelection);
                    break;
            }
        }

        public void ExplainNewGameCharacterBackgroundSelection()
        {
            tbInputArea.Text = "";
            lbDisplay.Items.Add("Each background will act as a passive bonus for the character.");
            lbDisplay.ScrollIntoView(lbDisplay.Items[lbDisplay.Items.Count - 1]);
            lbDisplay.Items.Add("Adventurer will give you a small stat buff, rest bonus and a experience gain bonus.");
            lbDisplay.ScrollIntoView(lbDisplay.Items[lbDisplay.Items.Count - 1]);
            lbDisplay.Items.Add("Noble will give you starting gold, higher starting MP and discounts in shops.");
            lbDisplay.ScrollIntoView(lbDisplay.Items[lbDisplay.Items.Count - 1]);
            lbDisplay.Items.Add("Merchant will give you a passive gold gain based on your level and merchants will offer more items for sale.");
            lbDisplay.ScrollIntoView(lbDisplay.Items[lbDisplay.Items.Count - 1]);
            lbDisplay.Items.Add("Blacksmith gives you a blunt damage bonus and gives you the ability to upgrade armors and weapons at the blacksmith with a heavy discount but a with a small chance to fail.");
            lbDisplay.ScrollIntoView(lbDisplay.Items[lbDisplay.Items.Count - 1]);
            btInput.Click += new RoutedEventHandler(NewGameCharacterBackgroundSelection);
        }

        public void NewGameSetCharacterBackground(string background)
        {
            switch (background)
            {
                case "Adventurer":
                    Hero.SetBackgroundAdventurer(newPlayerHero);
                    break;
                case "Noble":
                    Hero.SetBackgroundNoble(newPlayerHero);
                    Gold += 300;
                    break;
                case "Merchant":
                    Hero.SetBackgroundMerchant(newPlayerHero);
                    Gold += 10;
                    break;
                case "Blacksmith":
                    Hero.SetBackgroundBlacksmith(newPlayerHero);
                    break;
            }
        }

        public void NewGameCharacterRace()
        {
            tbInputArea.Text = "";
            lbOptions.Items.Clear();
            lbDisplay.Items.Add("Now you will choose a race for your character. This affects mostly your damage resistances.");
            lbDisplay.ScrollIntoView(lbDisplay.Items[lbDisplay.Items.Count - 1]);
            lbOptions.Items.Add("1. Human");
            lbOptions.Items.Add("2. Elf");
            lbOptions.Items.Add("3. Dwarf");
            lbOptions.Items.Add("4. Halfling");
            btInput.Click += new RoutedEventHandler(NewGameCharacterRaceSelection);
        }

        public void NewGameCharacterRaceSelection(object sender, RoutedEventArgs e)
        {
            btInput.Click -= new RoutedEventHandler(NewGameCharacterRaceSelection);
            switch (tbInputArea.Text)
            {
                case "?":
                    ExplainNewGameCharacterRaceSelection();
                    break;
                case "1":
                    NewGameSetCharacterRace("Human");
                    NewGameCharacterName();
                    break;
                case "Human":
                    NewGameSetCharacterRace("Human");
                    NewGameCharacterName();
                    break;
                case "2":
                    NewGameSetCharacterRace("Elf");
                    NewGameCharacterName();
                    break;
                case "Elf":
                    NewGameSetCharacterRace("Elf");
                    NewGameCharacterName();
                    break;
                case "3":
                    NewGameSetCharacterRace("Dwarf");
                    NewGameCharacterName();
                    break;
                case "Dwarf":
                    NewGameSetCharacterRace("Dwarf");
                    NewGameCharacterName();
                    break;
                case "4":
                    NewGameSetCharacterRace("Halfling");
                    NewGameCharacterName();
                    break;
                case "Halfling":
                    NewGameSetCharacterRace("Halfling");
                    NewGameCharacterName();
                    break;
                default:
                    MessageBox.Show("Please use the textbox at the bottom of the window to write a valid option from the left.");
                    btInput.Click += new RoutedEventHandler(NewGameCharacterRaceSelection);
                    break;
            }
        }

        public void ExplainNewGameCharacterRaceSelection()
        {
            tbInputArea.Text = "";
            foreach (Race race in Initializer.races)
            {
                if (race.RaceName == "Human" || race.RaceName == "Elf" || race.RaceName == "Dwarf" || race.RaceName == "Halfling")
                {
                    if (race.Fatal.Contains("None"))
                    {
                        race.Fatal.Clear();
                    }
                    if (race.Weak.Contains("None"))
                    {
                        race.Weak.Clear();
                    }
                    if (race.Resist.Contains("None"))
                    {
                        race.Resist.Clear();
                    }
                    if (race.Endure.Contains("None"))
                    {
                        race.Endure.Clear();
                    }
                    if (race.Nulls.Contains("None"))
                    {
                        race.Nulls.Clear();
                    }
                    string output = $"The {race.RaceName} has ";
                    if (race.Fatal.Count() > 0)
                    {
                        output += $"{race.Fatal.Count()} Fatal damage resistances (";
                        int resistanceCounter = 0;
                        foreach (string resistance in  race.Fatal)
                        {
                            if (resistanceCounter < race.Fatal.Count() - 1)
                            {
                                output += $"{resistance},";
                            }
                            else
                            {
                                output += $"{resistance}), ";
                            }
                            resistanceCounter++;
                        }
                    }
                    else
                    {
                        output += $"0 Fatal damage resistances, ";
                    }
                    if (race.Weak.Count() > 0)
                    {
                        output += $"{race.Weak.Count()} Weak damage resistances (";
                        int resistanceCounter = 0;
                        foreach (string resistance in race.Weak)
                        {
                            if (resistanceCounter < race.Weak.Count() - 1)
                            {
                                output += $"{resistance},";
                            }
                            else
                            {
                                output += $"{resistance}), ";
                            }
                            resistanceCounter++;
                        }
                    }
                    else
                    {
                        output += $"0 Weak damage resistances, ";
                    }
                    if (race.Resist.Count() > 0)
                    {
                        output += $"{race.Resist.Count()} Resisted damage resistances (";
                        int resistanceCounter = 0;
                        foreach (string resistance in race.Resist)
                        {
                            if (resistanceCounter < race.Resist.Count() - 1)
                            {
                                output += $"{resistance},";
                            }
                            else
                            {
                                output += $"{resistance}), ";
                            }
                            resistanceCounter++;
                        }
                    }
                    else
                    {
                        output += $"0 Resisted damage resistances, ";
                    }
                    if (race.Endure.Count() > 0)
                    {
                        output += $"{race.Endure.Count()} Endured damage resistances (";
                        int resistanceCounter = 0;
                        foreach (string resistance in race.Endure)
                        {
                            if (resistanceCounter < race.Endure.Count() - 1)
                            {
                                output += $"{resistance},";
                            }
                            else
                            {
                                output += $"{resistance}), ";
                            }
                            resistanceCounter++;
                        }
                    }
                    else
                    {
                        output += $"0 Endured damage resistances, ";
                    }
                    if (race.Nulls.Count() > 0)
                    {
                        output += $"{race.Nulls.Count()} Null damage resistances (";
                        int resistanceCounter = 0;
                        foreach (string resistance in race.Nulls)
                        {
                            if (resistanceCounter < race.Nulls.Count() - 1)
                            {
                                output += $"{resistance},";
                            }
                            else
                            {
                                output += $"{resistance}) ";
                            }
                            resistanceCounter++;
                        }
                    }
                    else
                    {
                        output += $"0 Null damage resistances ";
                    }
                    lbDisplay.Items.Add(output);
                    lbDisplay.ScrollIntoView(lbDisplay.Items[lbDisplay.Items.Count - 1]);
                }
            }
            lbDisplay.Items.Add("Humans receive increased exp gain, Elves receive extra mana, mana regen and darksight that makes them better for traps.");
            lbDisplay.ScrollIntoView(lbDisplay.Items[lbDisplay.Items.Count - 1]);
            lbDisplay.Items.Add("Dwarves receive extra health and defense, Halflinges receive less health but are immune to some debuffs and benefit more from crits.");
            lbDisplay.ScrollIntoView(lbDisplay.Items[lbDisplay.Items.Count - 1]);
            btInput.Click += new RoutedEventHandler(NewGameCharacterRaceSelection);
        }

        public void NewGameSetCharacterRace(string race)
        {
            switch (race)
            {
                case "Human":
                    Hero.SetRaceHuman(newPlayerHero);
                    break;
                case "Elf":
                    Hero.SetRaceElf(newPlayerHero);
                    break;
                case "Dwarf":
                    Hero.SetRaceDwarf(newPlayerHero);
                    break;
                case "Halfling":
                    Hero.SetRaceHalfling(newPlayerHero);
                    break;
            }
        }

        public void NewGameCharacterName()
        {
            tbInputArea.Text = "";
            lbOptions.Items.Clear();
            lbDisplay.Items.Add("The final touch is to name your character. With the shortened names option after the first space input the name will be cut off while it is used in text.");
            lbDisplay.ScrollIntoView(lbDisplay.Items[lbDisplay.Items.Count - 1]);
            lbDisplay.Items.Add("The name can only be 100 characters long at most and you can't have repeat names in our cloud saves because it is going to overwrite the old one when saving, but this is not a problem with offline play.");
            lbDisplay.ScrollIntoView(lbDisplay.Items[lbDisplay.Items.Count - 1]);
            lbDisplay.Items.Add("In a future update we will make a workaround for this.");
            lbDisplay.ScrollIntoView(lbDisplay.Items[lbDisplay.Items.Count - 1]);
            try
            {
                if (folders.Last() != folders[10])
                {
                    string names = "";
                    int namesCount = 0;
                    int namesCounter = 0;
                    string command = $"Select Count(sabpat702.hero.Name) from sabpat702.hero inner join sabpat702.user on sabpat702.user.Id = sabpat702.hero.UserId Where sabpat702.user.UserName = '{folders.Last()}'";
                    MySqlCommand mySqlCommand = new MySqlCommand(command, mySqlConnection);
                    mySqlConnection.Open();
                    MySqlDataReader mySqlDataReader = mySqlCommand.ExecuteReader();
                    while (mySqlDataReader.Read())
                    {
                        namesCount = mySqlDataReader.GetInt32(0);
                    }
                    mySqlConnection.Close();
                    command = $"Select sabpat702.hero.Name from sabpat702.hero inner join sabpat702.user on sabpat702.user.Id = sabpat702.hero.UserId Where sabpat702.user.UserName = '{folders.Last()}'";
                    MySqlCommand mySqlCommand2 = new MySqlCommand(command, mySqlConnection);
                    mySqlConnection.Open();
                    MySqlDataReader mySqlDataReader2 = mySqlCommand2.ExecuteReader();
                    while (mySqlDataReader2.Read())
                    {
                        if (namesCounter < namesCount - 1)
                        {
                            names += $"{mySqlDataReader2.GetString(0)},";
                        }
                        else
                        {
                            names += $"{mySqlDataReader2.GetString(0)}";
                        }
                    }
                    mySqlConnection.Close();
                    lbDisplay.Items.Add($"The currently used names: {names}");
                    lbDisplay.ScrollIntoView(lbDisplay.Items[lbDisplay.Items.Count - 1]);
                }
            }
            catch
            {

            }
            btInput.Click += new RoutedEventHandler(NewGameCharacterNameNaming);
        }

        public void NewGameCharacterNameNaming(object sender, RoutedEventArgs e)
        {
            btInput.Click -= new RoutedEventHandler(NewGameCharacterNameNaming);
            if (tbInputArea.Text == "")
            {
                MessageBox.Show("Please give a name to the character using the textbox at the bottom of the screen.");
                btInput.Click += new RoutedEventHandler(NewGameCharacterNameNaming);
            }
            else if (tbInputArea.Text.Length > 100)
            {
                MessageBox.Show($"The name is too long to be used. (it is over 100 characters by {tbInputArea.Text.Length - 100} character(s))");
                btInput.Click += new RoutedEventHandler(NewGameCharacterNameNaming);
            }
            else
            {
                newPlayerHero.HeroName = tbInputArea.Text;
                if (ShortDisplayNames == false)
                {
                    newPlayerHero.DisplayName = tbInputArea.Text;
                }
                else
                {
                    newPlayerHero.DisplayName = tbInputArea.Text.Split(' ')[0];
                }
                NewGameCharacterConfirm();
            }
        }

        public void NewGameCharacterConfirm()
        {
            tbInputArea.Text = "";
            lbDisplay.Items.Add($"Are you sure you want this character? (name:{newPlayerHero.HeroName},display name:{newPlayerHero.DisplayName},class:{newPlayerHero.heroClass},race:{newPlayerHero.Race.RaceName},background:{newPlayerHero.Background})");
            lbDisplay.ScrollIntoView(lbDisplay.Items[lbDisplay.Items.Count - 1]);
            lbOptions.Items.Add("1. Yes");
            lbOptions.Items.Add("2. No");
            lbOptions.Items.Add("3. Change Name");
            btInput.Click += new RoutedEventHandler(NewGameCharacterConfirming);
        }

        public void NewGameCharacterConfirming(object sender, RoutedEventArgs e)
        {
            btInput.Click -= new RoutedEventHandler(NewGameCharacterConfirming);
            switch (tbInputArea.Text)
            {
                case "?":
                    tbInputArea.Text = "";
                    lbDisplay.Items.Add("There is nothing to explain here.");
                    lbDisplay.ScrollIntoView(lbDisplay.Items[lbDisplay.Items.Count - 1]);
                    btInput.Click += new RoutedEventHandler(NewGameCharacterConfirming);
                    break;
                case "1":
                    NewGameStart();
                    break;
                case "Yes":
                    NewGameStart();
                    break;
                case "2":
                    RestartNewGame();
                    break;
                case "No":
                    RestartNewGame();
                    break;
                case "3":
                    tbInputArea.Text = "";
                    lbOptions.Items.Clear();
                    btInput.Click += new RoutedEventHandler(NewGameCharacterNameNaming);
                    break;
                case "Change Name":
                    tbInputArea.Text = "";
                    lbOptions.Items.Clear();
                    btInput.Click += new RoutedEventHandler(NewGameCharacterNameNaming);
                    break;
                default:
                    MessageBox.Show("Please use the textbox at the bottom of the window to write a valid option from the left.");
                    btInput.Click += new RoutedEventHandler(NewGameCharacterConfirming);
                    break;
            }
        }

        public void RestartNewGame()
        {
            lbOptions.Items.Clear();
            newPlayerHero.Passives.Clear();
            newPlayerHero.BuffsDebuffs.Clear();
            newPlayerHero.Skills.Clear();
            newPlayerHero.Magics.Clear();
            lbOptions.Items.Add("1. Fighter");
            lbOptions.Items.Add("2. Hunter");
            lbOptions.Items.Add("3. Wizard");
            lbOptions.Items.Add("4. Paladin");
            lbOptions.Items.Add("5. Bounty Hunter");
            lbOptions.Items.Add("6. Warlock");
            btInput.Click += new RoutedEventHandler(NewGameCharacterClassSelection);
        }

        public void NewGameStart()
        {
            tbInputArea.Text = "";
            lbOptions.Items.Clear();
            heroes.Add(newPlayerHero);
            party.Add(newPlayerHero);
            foreach (Weapon weapon in Initializer.weapons)
            {
                weaponsImproved.Add(weapon.WeaponName, 0);
                weaponsObtained.Add(weapon.WeaponName, false);
            }
            weaponsObtained["Unarmed"] = true;
            weaponsObtained["Rusty Longsword"] = true;
            weaponsObtained["Broken Spear"] = true;
            weaponsObtained["Short Bow"] = true;
            weaponsObtained["Dagger"] = true;
            weaponsObtained["Travel Staff"] = true;
            weaponsObtained["Wooden Shield"] = true;
            foreach (Armor armor in Initializer.armors)
            {
                armorsImproved.Add(armor.ArmorName, 0);
                armorsObtained.Add(armor.ArmorName, false);
            }
            armorsObtained["None"] = true;
            armorsObtained["Worn Leather Chestpiece"] = true;
            armorsObtained["Old Travel Boots"] = true;
            armorsObtained["Dirty Cloak"] = true;
            armorsObtained["Mana Touched Rag"] = true;
            armorsObtained["Worn Leather Knee Pads"] = true;
            armorsObtained["Rusting Chainmail"] = true;
            foreach (Consumable consumable in Initializer.consumables)
            {
                consumables.Add(consumable.ConsumableName, 0);
                consumablesUnlocked.Add(consumable.ConsumableName, false);
            }
            foreach (Dungeon dungeon in Initializer.dungeons)
            {
                dungeonsCompleted.Add(dungeon.DungeonName, false);
            }
            foreach (string quest in quests)
            {
                questsCompleted.Add(quest, false);
            }
            lbDisplay.Items.Add("Do you want to do the tutorial?");
            lbDisplay.ScrollIntoView(lbDisplay.Items[lbDisplay.Items.Count - 1]);
            lbOptions.Items.Add("1. Yes");
            lbOptions.Items.Add("2. No");
            btInput.Click += new RoutedEventHandler(StartNewGameTutorialQuestion);
        }

        public void StartNewGameTutorialQuestion(object sender, RoutedEventArgs e)
        {
            btInput.Click -= new RoutedEventHandler(StartNewGameTutorialQuestion);
            switch (tbInputArea.Text)
            {
                case "?":
                    MessageBox.Show("There is nothing to explain here.");
                    btInput.Click += new RoutedEventHandler(StartNewGameTutorialQuestion);
                    break;
                case "1":
                    StartTutorial();
                    break;
                case "Yes":
                    StartTutorial();
                    break;
                case "2":
                    EnterTown();
                    break;
                case "No":
                    EnterTown();
                    break;
                default:
                    MessageBox.Show("Please use the textbox at the bottom of the window to write a valid option from the left.");
                    btInput.Click += new RoutedEventHandler(StartNewGameTutorialQuestion);
                    break;
            }
        }

        public void StartTutorial()
        {
            //Finish this later ----------------------------------------------------------------------------------------
            EnterTown();
        }

        //New Game ends here -------------------------------------------------------------------------------------------

        //Load Save starts here ----------------------------------------------------------------------------------------

        public void LoadExistingSave()
        {
            try
            {
                foreach(string oneline in File.ReadAllLines($@"{folders[9]}\{folders.Last()}\{files.Last()}"))
                {
                    string[] linecutter = oneline.Split('$');
                    
                    string[] consumablescutter = linecutter[2].Split('%');
                    foreach (string consumable in consumablescutter)
                    {
                        string[] itemcutter = consumable.Split('@');
                        consumables.Add(itemcutter[0], Convert.ToInt32(itemcutter[1]));
                    }
                    Gold = Convert.ToInt32(linecutter[3]);
                    Experience = Convert.ToInt32(linecutter[4]);
                    string[] questscutter = linecutter[5].Split('%');
                    for (int i = 0; i < quests.Count; i++)
                    {
                        questsCompleted.Add(quests[i], Convert.ToBoolean(questscutter[i]));
                    }
                    string[] dungeonscutter = linecutter[6].Split('%');
                    for (int i = 0; i < Initializer.dungeons.Count; i++)
                    {
                        dungeonsCompleted.Add(Initializer.dungeons[i].DungeonName, Convert.ToBoolean(dungeonscutter[i]));
                    }
                    string[] weaponsImprovementcutter = linecutter[7].Split('%');
                    for (int i = 0; i < Initializer.weapons.Count; i++)
                    {
                        weaponsImproved.Add(Initializer.weapons[i].WeaponName, Convert.ToInt32(weaponsImprovementcutter[i]));
                        Initializer.weapons[i].ATK += Convert.ToInt32(weaponsImprovementcutter[i]);
                    }
                    string[] armorsImprovementcutter = linecutter[8].Split('%');
                    for (int i = 0; i < Initializer.armors.Count; i++)
                    {
                        armorsImproved.Add(Initializer.armors[i].ArmorName, Convert.ToInt32(armorsImprovementcutter[i]));
                        Initializer.armors[i].DEF += Convert.ToInt32(armorsImprovementcutter[i]);
                        Initializer.armors[i].MDEF += Convert.ToInt32(armorsImprovementcutter[i]);
                    }
                    string[] weaponsObtainedcutter = linecutter[9].Split('%');
                    for (int i = 0; i < Initializer.weapons.Count; i++)
                    {
                        weaponsObtained.Add(Initializer.weapons[i].WeaponName, Convert.ToBoolean(weaponsObtainedcutter[i]));
                    }
                    string[] armorsObtainedcutter = linecutter[10].Split('%');
                    for (int i = 0; i < Initializer.armors.Count; i++)
                    {
                        armorsObtained.Add(Initializer.armors[i].ArmorName, Convert.ToBoolean(armorsObtainedcutter[i]));
                    }
                    string[] consumablesUnlockedcutter = linecutter[11].Split('%');
                    for (int i = 0; i < Initializer.consumables.Count; i++)
                    {
                        consumablesUnlocked.Add(Initializer.consumables[i].ConsumableName, Convert.ToBoolean(consumablesUnlockedcutter[i]));
                    }

                    string[] heroescutter = linecutter[1].Split('%');
                    foreach (string hero in heroescutter)
                    {
                        heroes.Add(new Hero(hero, Initializer.passives, Initializer.buffsDebuffs, Initializer.skills, Initializer.magics, Initializer.races, Initializer.armors, Initializer.weapons, ShortDisplayNames, weaponsImproved, armorsImproved));
                    }
                    for (int i = 0; i < heroescutter.Length; i++)
                    {
                        if (heroescutter[i].Split('@').Last() == "true")
                        {
                            party.Add(heroes[i]);
                        }
                    }
                }
            }
            catch (Exception error )
            {
                MessageBox.Show(error.Message);
            }
        }

        //Load Save ends here ------------------------------------------------------------------------------------------

        //Game starting ends here --------------------------------------------------------------------------------------

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
                damageSourcePrep = new DamageSource(Initializer.npcs[0], 0);
                targetPrep = new Target(Initializer.monsters[0]);
                lbDisplay.Items.Add(DamageCalculation(targetPrep, damageSourcePrep));
            }
            else if (tbInputArea.Text == "2")
            {
                damageSourcePrep = new DamageSource(Initializer.monsters[0], Initializer.monsters[0].Skills[0]);
                targetPrep = new Target(Initializer.npcs[0]);
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
            if (Initializer.races[target.Race.Id].Fatal.Contains(damageSource.DamageType))
            {
                damage = damage * 2;
            }
            else if (Initializer.races[target.Race.Id].Weak.Contains(damageSource.DamageType))
            {
                damage = (int)Math.Round(damage * 1.5, 0);
            }
            else if (Initializer.races[target.Race.Id].Resist.Contains(damageSource.DamageType))
            {
                damage = (int)Math.Round(damage * 0.75, 0);
            }
            else if (Initializer.races[target.Race.Id].Endure.Contains(damageSource.DamageType))
            {
                damage = (int)Math.Round(damage * 0.25, 0);
            }
            else if (Initializer.races[target.Race.Id].Nulls.Contains(damageSource.DamageType))
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

        //Town starts here ---------------------------------------------------------------------------------------------

        public void EnterTown()
        {
            heroes.Add(Initializer.npcs[0]);
            Gold += 1000;
            weaponsObtained["Family Lance"] = true;
            armorsObtained["Bejeweled Helmet"] = true;
            tbInputArea.Text = "";
            lbOptions.Items.Clear();
            lbDisplay.Items.Add("GAME: You have entered the town and now you can save and modify your party and your items.");
            lbDisplay.ScrollIntoView(lbDisplay.Items[lbDisplay.Items.Count-1]);
            MainTownOptions();
            btInput.Click += new RoutedEventHandler(MainTownOption);
        }

        public void MainTownOptions()
        {
            lbOptions.Items.Add("1. Quit Game");
            lbOptions.Items.Add("2. Save Game");
            lbOptions.Items.Add("3. Blacksmith");
            lbOptions.Items.Add("4. Tavern");
            lbOptions.Items.Add("5. Adventurers Guild");
            lbOptions.Items.Add("6. Enter Dungeon");
        }

        public void MainTownOption(object sender, RoutedEventArgs e)
        {
            btInput.Click -= new RoutedEventHandler(MainTownOption);
            switch (tbInputArea.Text)
            {
                case "?":
                    ExplainMainTownOption();
                    break;
                case "1":
                    QuitGame();
                    break;
                case "Quit Game":
                    QuitGame();
                    break;
                case "2":
                    SaveGame();
                    break;
                case "Save Game":
                    SaveGame();
                    break;
                case "3":
                    BlacksmithEnter();
                    break;
                case "Blacksmith":
                    BlacksmithEnter();
                    break;
                case "4":
                    break;
                case "Tavern":
                    break;
                case "5":
                    break;
                case "Adventurers Guild":
                    break;
                case "6":
                    break;
                case "Enter Dungeon":
                    break;
                default:
                    MessageBox.Show("Please use the textbox at the bottom of the window to write a valid option from the left.");
                    btInput.Click += new RoutedEventHandler(MainTownOption);
                    break;
            }
        }

        public void ExplainMainTownOption()
        {
            lbDisplay.Items.Add("EXPLANATION: The Guit Game option will close the game without saving if you are playing through the New Game option, but will save automatically if you have loaded an existing save to play.");
            lbDisplay.ScrollIntoView(lbDisplay.Items[lbDisplay.Items.Count - 1]);
            lbDisplay.Items.Add("EXPLANATION: The Save Game option will allow you to save the game without closing the game and it will allow you to create a new save as well so that you can avoid overwriting an existing save.");
            lbDisplay.ScrollIntoView(lbDisplay.Items[lbDisplay.Items.Count - 1]);
            lbDisplay.Items.Add("EXPLANATION: The Blacksmith can help you make new pieces of armors and weapons for your explorations and he can also upgrade any existing weapons and armors that you have so that they can become stronger.");
            lbDisplay.ScrollIntoView(lbDisplay.Items[lbDisplay.Items.Count - 1]);
            lbDisplay.Items.Add("EXPLANATION: The Tavern is where you can claim the obtained experience for your characters so that they can level up and become stronger. You may also buy meals that give a buff to your party.");
            lbDisplay.ScrollIntoView(lbDisplay.Items[lbDisplay.Items.Count - 1]);
            lbDisplay.Items.Add("EXPLANATION: The Adventurers Guild is where you can change your party composition (who is in the party and where) and start some of the quests.");
            lbDisplay.ScrollIntoView(lbDisplay.Items[lbDisplay.Items.Count - 1]);
            lbDisplay.Items.Add("EXPLANATION: The Enter Dungeon option is how you enter a dungeon to explore and fight in. Quests cand also be undertaken from here when found and started somewhere else in the game.");
            lbDisplay.ScrollIntoView(lbDisplay.Items[lbDisplay.Items.Count - 1]);
            tbInputArea.Text = "";
            btInput.Click += new RoutedEventHandler(MainTownOption);
        }

        //Saving starts here -------------------------------------------------------------------------------------------

        public void QuitGame()
        {
            if (files.Last() != files[13])
            {
                try
                {
                    StreamWriter streamWriter = new StreamWriter($@"{folders[9]}\{folders.Last()}\{files.Last()}");
                    streamWriter.Write(Saving.MakeSaveString(mySqlConnection, folders, files, heroes, party, Initializer.npcs, questsCompleted, consumables, Gold, Experience, dungeonsCompleted, weaponsImproved, armorsImproved, weaponsObtained, armorsObtained, consumablesUnlocked));
                    streamWriter.Close();

                    Saving.SavingStart(folders, files, heroes, party, questsCompleted, consumables, Gold, Experience, dungeonsCompleted, weaponsImproved, armorsImproved, weaponsObtained, armorsObtained, consumablesUnlocked, mySqlConnection, Initializer.npcs, newHero);
                }
                catch (Exception error)
                {
                    MessageBox.Show(error.Message);
                }
            }
            this.Close();
        }

        public void SaveGame()
        {
            tbInputArea.Text = "";
            lbOptions.Items.Clear();
            if (files.Last() != files[13])
            {
                lbDisplay.Items.Add($"GAME: Will you overwrite the current save ({files.Last()}) or create a new save?");
                lbDisplay.ScrollIntoView(lbDisplay.Items[lbDisplay.Items.Count - 1]);
                lbOptions.Items.Add("1. Overwrite Save");
                lbOptions.Items.Add("2. New Save");
                lbOptions.Items.Add("3. Cancel");
                btInput.Click += new RoutedEventHandler(SaveGameNewOrNot);
            }
            else
            {
                lbDisplay.Items.Add("GAME: To save the game you first need to give a name to the save.");
                lbDisplay.ScrollIntoView(lbDisplay.Items[lbDisplay.Items.Count - 1]);
                SaveGameNewSave();
            }
        }

        public void SaveGameNewOrNot(object sender, RoutedEventArgs e)
        {
            btInput.Click -= new RoutedEventHandler(SaveGameNewOrNot);
            switch (tbInputArea.Text)
            {
                case "?":
                    ExplainSaveGameNewOrNot();
                    break;
                case "1":
                    SaveGameSaving();
                    break;
                case "2":
                    SaveGameNewSave();
                    break;
                case "3":
                    SaveGameExit();
                    break;
                case "Overwrite Save":
                    SaveGameSaving();
                    break;
                case "New Save":
                    SaveGameNewSave();
                    break;
                case "Cancel":
                    SaveGameExit();
                    break;
                default:
                    MessageBox.Show("Please use the textbox at the bottom of the window to write a valid option from the left.");
                    btInput.Click += new RoutedEventHandler(SaveGameNewOrNot);
                    break;
            }
        }

        public void ExplainSaveGameNewOrNot()
        {
            tbInputArea.Text = "";
            lbDisplay.Items.Add("EXPLANATION: Overwrite Save will swap out the contents of the save that you opened in the beginning.");
            lbDisplay.ScrollIntoView(lbDisplay.Items[lbDisplay.Items.Count - 1]);
            lbDisplay.Items.Add("EXPLANATION: New Save will create a new text file in the current profile and create a save in that new file for later use.");
            lbDisplay.ScrollIntoView(lbDisplay.Items[lbDisplay.Items.Count - 1]);
            lbDisplay.Items.Add("EXPLANATION: Cancel will stop the Save Game process and take you back to the town.");
            lbDisplay.ScrollIntoView(lbDisplay.Items[lbDisplay.Items.Count - 1]);
            btInput.Click += new RoutedEventHandler(SaveGameNewOrNot);
        }

        public void SaveGameExit()
        {
            tbInputArea.Text = "";
            lbOptions.Items.Clear();
            MainTownOptions();
            btInput.Click += new RoutedEventHandler(MainTownOption);
        }

        public void SaveGameSaving()
        {
            try
            {
                StreamWriter streamWriter = new StreamWriter($@"{folders[9]}\{folders.Last()}\{files.Last()}");
                streamWriter.Write(Saving.MakeSaveString(mySqlConnection, folders, files, heroes, party, Initializer.npcs, questsCompleted, consumables, Gold, Experience, dungeonsCompleted, weaponsImproved, armorsImproved, weaponsObtained, armorsObtained, consumablesUnlocked));
                streamWriter.Close();
                MessageBox.Show("Game saved successfully");

                Saving.SavingStart(folders, files, heroes, party, questsCompleted, consumables, Gold, Experience, dungeonsCompleted, weaponsImproved, armorsImproved, weaponsObtained, armorsObtained, consumablesUnlocked, mySqlConnection, Initializer.npcs, newHero);
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message);
            }
            SaveGameExit();
        }

        public void SaveGameNewSave()
        {
            tbInputArea.Text = "";
            lbOptions.Items.Clear();
            lbDisplay.Items.Add("GAME: While writing a name for the save do keep in mind that the name will be given the '.txt' extension to make it a text file. (you can't name it '1', 'Cancel' or any file that is in the GameAssets folder)");
            lbDisplay.ScrollIntoView(lbDisplay.Items[lbDisplay.Items.Count - 1]);
            lbOptions.Items.Add("1. Cancel");
            btInput.Click += new RoutedEventHandler(NewSaveNaming);
        }

        public void NewSaveNaming(object sender, RoutedEventArgs e)
        {
            btInput.Click -= new RoutedEventHandler(NewSaveNaming);
            if (tbInputArea.Text == "1" || tbInputArea.Text == "Cancel")
            {
                SaveGameExit();
            }
            else if (tbInputArea.Text == "?")
            {
                ExplainNewSaveNaming();
            }
            else if (files.Contains(tbInputArea.Text + ".txt"))
            {
                MessageBox.Show("You can't name the save the same as the files in the GameAssets folder");
                tbInputArea.Text = "";
                btInput.Click += new RoutedEventHandler(NewSaveNaming);
            }
            else
            {
                NewSaveSaving();
            }
        }

        public void NewSaveSaving()
        {
            if (files.Last() != files[13])
            {
                files.Remove(files.Last());
            }
            files.Add(tbInputArea.Text + ".txt");
            try
            {
                if (Saving.SaveExist(mySqlConnection, folders, files) == false)
                {
                    StreamWriter streamWriter = new StreamWriter($@"{folders[9]}\{folders.Last()}\{files.Last()}");
                    streamWriter.Write(Saving.MakeSaveString(mySqlConnection, folders, files, heroes, party, Initializer.npcs, questsCompleted, consumables, Gold, Experience, dungeonsCompleted, weaponsImproved, armorsImproved, weaponsObtained, armorsObtained, consumablesUnlocked));
                    streamWriter.Close();
                    MessageBox.Show("Game saved successfully");

                    Saving.SavingStart(folders, files, heroes, party, questsCompleted, consumables, Gold, Experience, dungeonsCompleted, weaponsImproved, armorsImproved, weaponsObtained, armorsObtained, consumablesUnlocked, mySqlConnection, Initializer.npcs, newHero);
                    SaveGameExit();
                }
                else
                {
                    MessageBox.Show("You already have a save named like this in our database and maybe in your own profile folder and for our and your convenience we can't allow you to have a duplicate file name");
                    btInput.Click += new RoutedEventHandler(NewSaveNaming);
                    tbInputArea.Text = "";
                }
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message);
                SaveGameExit();
            }
        }

        public void ExplainNewSaveNaming()
        {
            tbInputArea.Text = "";
            lbDisplay.Items.Add("EXPLANATION: Cancel will stop the Save Game process and take you back to the town.");
            lbDisplay.ScrollIntoView(lbDisplay.Items[lbDisplay.Items.Count - 1]);
            btInput.Click += new RoutedEventHandler(NewSaveNaming);
        }

        //Saving ends here ---------------------------------------------------------------------------------------------

        //Blacksmith starts here ---------------------------------------------------------------------------------------

        public void BlacksmithEnter()
        {
            lbOptions.Items.Clear();
            tbInputArea.Text = "";
            lbDisplay.Items.Add("Blacksmith: Welcome customer and how can I help you today?");
            lbDisplay.ScrollIntoView(lbDisplay.Items[lbDisplay.Items.Count - 1]);
            lbOptions.Items.Add("1. Buy Weapon");
            lbOptions.Items.Add("2. Buy Armor");
            lbOptions.Items.Add("3. Improve Weapon");
            lbOptions.Items.Add("4. Improve Armor");
            lbOptions.Items.Add("5. Change Equipment");
            lbOptions.Items.Add("6. Leave");
            btInput.Click += new RoutedEventHandler(BlacksmithMainOption);
        }

        public void BlacksmithMainOption(object sender, RoutedEventArgs e)
        {
            btInput.Click -= new RoutedEventHandler(BlacksmithMainOption);
            switch (tbInputArea.Text)
            {
                case "?":
                    ExplainBlacksmithMainOption();
                    break;
                case "1":
                    BlacksmithBuyWeapon();
                    break;
                case "Buy Weapon":
                    BlacksmithBuyWeapon();
                    break;
                case "2":
                    BlacksmithBuyArmor();
                    break;
                case "Buy Armor":
                    BlacksmithBuyArmor();
                    break;
                case "3":
                    BlacksmithImproveWeapon();
                    break;
                case "Improve Weapon":
                    BlacksmithImproveWeapon();
                    break;
                case "4":
                    BlacksmithImproveArmor();
                    break;
                case "Improve Armor":
                    BlacksmithImproveArmor();
                    break;
                case "5":
                    BlacksmithChangeEquipmentFirstStep();
                    break;
                case "Change Equipment":
                    BlacksmithChangeEquipmentFirstStep();
                    break;
                case "6":
                    BlacksmithLeave();
                    break;
                case "Leave":
                    BlacksmithLeave();
                    break;
                default:
                    MessageBox.Show("Please use the textbox at the bottom of the window to write a valid option from the left.");
                    btInput.Click += new RoutedEventHandler(BlacksmithMainOption);
                    break;
            }
        }

        public void ExplainBlacksmithMainOption()
        {
            tbInputArea.Text = "";
            lbDisplay.Items.Add("EXPLANATION: Buy Weapon allows you to look at the unowned weapons that you can buy and then decide on buying or not buying from the weapons.");
            lbDisplay.ScrollIntoView(lbDisplay.Items[lbDisplay.Items.Count - 1]);
            lbDisplay.Items.Add("EXPLANATION: Buy Armor allows you to look at the unowned armors that you can buy and then decide on buying or not buying from the armors.");
            lbDisplay.ScrollIntoView(lbDisplay.Items[lbDisplay.Items.Count - 1]);
            lbDisplay.Items.Add("EXPLANATION: Improve Weapon allows you to look at the owned weapons and improve their ATK stat.");
            lbDisplay.ScrollIntoView(lbDisplay.Items[lbDisplay.Items.Count - 1]);
            lbDisplay.Items.Add("EXPLANATION: Improve Armor allows you to look at the owned armors and improve their DEF and MDEF stat.");
            lbDisplay.ScrollIntoView(lbDisplay.Items[lbDisplay.Items.Count - 1]);
            lbDisplay.Items.Add("EXPLANATION: Change Equipment allows you to change each piece of weapon or armor equipped on a character and change it to another one.");
            lbDisplay.ScrollIntoView(lbDisplay.Items[lbDisplay.Items.Count - 1]);
            lbDisplay.Items.Add("EXPLANATION: Leave allows you to leave the blacksmith alone to do his business.");
            lbDisplay.ScrollIntoView(lbDisplay.Items[lbDisplay.Items.Count - 1]);
            btInput.Click += new RoutedEventHandler(BlacksmithMainOption);
        }

        //Buy Weapon starts here ---------------------------------------------------------------------------------------

        public void BlacksmithBuyWeaponWeapons()
        {
            int counter = 2;
            purchasableWeapons.Clear();
            foreach (Weapon weapon in Initializer.weapons)
            {
                if (weaponsObtained[weapon.WeaponName] == false && weapon.Price > 0)
                {
                    lbOptions.Items.Add($"{counter}. {weapon.WeaponName}");
                    counter++;
                    purchasableWeapons.Add(weapon);
                }
            }
        }

        public void BlacksmithBuyWeapon()
        {
            tbInputArea.Text = "";
            lbOptions.Items.Clear();
            lbOptions.Items.Add("1. Cancel");
            lbDisplay.Items.Add("Blacksmith: Here...");
            lbDisplay.ScrollIntoView(lbDisplay.Items[lbDisplay.Items.Count - 1]);
            BlacksmithBuyWeaponWeapons();
            lbDisplay.Items.Add("Blacksmith: ... is the list. Anything that catches your eyes?");
            lbDisplay.ScrollIntoView(lbDisplay.Items[lbDisplay.Items.Count - 1]);
            btInput.Click += new RoutedEventHandler(BlacksmithBuyWeaponChooseWeapon);
        }
        
        public void BlacksmithBuyWeaponChooseWeapon(object sender, RoutedEventArgs e)
        {
            btInput.Click -= new RoutedEventHandler(BlacksmithBuyWeaponChooseWeapon);
            if (tbInputArea.Text == "?")
            {
                tbInputArea.Text = "";
                lbDisplay.Items.Add("EXPLANATION: You can cancel the process by using the Cancel option or first inspect a weapon then buy said weapon by selecting one of the available weapons from the left.");
                lbDisplay.ScrollIntoView(lbDisplay.Items[lbDisplay.Items.Count - 1]);
                btInput.Click += new RoutedEventHandler(BlacksmithBuyWeaponChooseWeapon);
            }
            else if (tbInputArea.Text == "1" ||  tbInputArea.Text == "Cancel")
            {
                purchasableWeapons.Clear();
                BlacksmithMainOptionReEntry();
            }
            else
            {
                List<string> purchasableWeaponsName = new List<string>(purchasableWeapons.Select(x => x.WeaponName));
                if (purchasableWeaponsName.Contains(tbInputArea.Text) == true || tbInputArea.Text.Contains("0") || tbInputArea.Text.Contains("1") || tbInputArea.Text.Contains("2") || tbInputArea.Text.Contains("3") || tbInputArea.Text.Contains("4") || tbInputArea.Text.Contains("5") || tbInputArea.Text.Contains("6") || tbInputArea.Text.Contains("7") || tbInputArea.Text.Contains("8") || tbInputArea.Text.Contains("9"))
                {
                    if (purchasableWeaponsName.Contains(tbInputArea.Text) == true)
                    {
                        foreach(Weapon weapon in purchasableWeapons)
                        {
                            if (weapon.WeaponName == tbInputArea.Text)
                            {
                                buyWeaponSelectedWeapon = weapon;
                            }
                        }
                        tbInputArea.Text = "";
                        BlacksmithBuyWeaponChosenWeapon();
                    }
                    else
                    {
                        try
                        {
                            int index = Convert.ToInt32(tbInputArea.Text) - 2;
                            buyWeaponSelectedWeapon = purchasableWeapons[index];
                            tbInputArea.Text = "";
                            BlacksmithBuyWeaponChosenWeapon();
                        }
                        catch
                        {
                            MessageBox.Show("Please use the textbox at the bottom of the window to write a valid option from the left.");
                            btInput.Click += new RoutedEventHandler(BlacksmithBuyWeaponChooseWeapon);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Please use the textbox at the bottom of the window to write a valid option from the left.");
                    btInput.Click += new RoutedEventHandler(BlacksmithBuyWeaponChooseWeapon);
                }
            }
        }

        public void BlacksmithBuyWeaponChosenWeapon()
        {
            string output = "";
            lbOptions.Items.Clear();
            lbOptions.Items.Add("1. Cancel");
            lbOptions.Items.Add("2. Buy");
            lbDisplay.Items.Add($"Weapon:{buyWeaponSelectedWeapon.WeaponName} ATK:{buyWeaponSelectedWeapon.ATK} Crit:{buyWeaponSelectedWeapon.CritChance}% {buyWeaponSelectedWeapon.CritDamage}x Damage type:{buyWeaponSelectedWeapon.DamageType} Range:{buyWeaponSelectedWeapon.Range} Skill compatibility:{buyWeaponSelectedWeapon.SkillCompatibility} Unique:{buyWeaponSelectedWeapon.Unique} Price:{buyWeaponSelectedWeapon.Price}");
            for (int i = 0; i < buyWeaponSelectedWeapon.SpecialEffects.Count; i++)
            {
                if (i < buyWeaponSelectedWeapon.SpecialEffects.Count - 1)
                {
                    output += buyWeaponSelectedWeapon.SpecialEffects[i].SpecialEffectName + ",";
                }
                else
                {
                    output += buyWeaponSelectedWeapon.SpecialEffects[i].SpecialEffectName;
                }
            }
            lbDisplay.Items.Add($"Special effects:{output}");
            lbDisplay.Items.Add($"Description:{buyWeaponSelectedWeapon.Description}");
            lbDisplay.Items.Add($"Current gold:{Gold}");
            lbDisplay.ScrollIntoView(lbDisplay.Items[lbDisplay.Items.Count - 1]);
            btInput.Click += new RoutedEventHandler(BlacksmithBuyWeaponChosenWeaponBuyOrCancel);
        }

        public void BlacksmithBuyWeaponChosenWeaponBuyOrCancel(object sender, RoutedEventArgs e)
        {
            btInput.Click -= new RoutedEventHandler(BlacksmithBuyWeaponChosenWeaponBuyOrCancel);
            switch (tbInputArea.Text)
            {
                case "?":
                    lbDisplay.Items.Add("Blacksmith: You buying or not?");
                    btInput.Click += new RoutedEventHandler(BlacksmithBuyWeaponChosenWeaponBuyOrCancel);
                    break;
                case "1":
                    lbDisplay.Items.Add("Blacksmith: Not what you're looking for? Then just look around some more.");
                    lbDisplay.ScrollIntoView(lbDisplay.Items[lbDisplay.Items.Count - 1]);
                    BlacksmithBuyWeaponChooseWeaponReEntry();
                    break;
                case "Cancel":
                    lbDisplay.Items.Add("Blacksmith: Not what you're looking for? Then just look around some more.");
                    lbDisplay.ScrollIntoView(lbDisplay.Items[lbDisplay.Items.Count - 1]);
                    BlacksmithBuyWeaponChooseWeaponReEntry();
                    break;
                case "2":
                    BlacksmithBuyWeaponChosenWeaponBought();
                    break;
                case "Buy":
                    BlacksmithBuyWeaponChosenWeaponBought();
                    break;
                default:
                    MessageBox.Show("Please use the textbox at the bottom of the window to write a valid option from the left.");
                    btInput.Click += new RoutedEventHandler(BlacksmithBuyWeaponChosenWeaponBuyOrCancel);
                    break;
            }
        }

        public void BlacksmithBuyWeaponChosenWeaponBought()
        {
            int cost = buyWeaponSelectedWeapon.Price;
            foreach (Passive passive in party[0].Passives)
            {
                if (passive.Affect.Contains("Shop Payment"))
                {
                    switch (passive.PassiveName)
                    {
                        case "Noble":
                            cost = Convert.ToInt32(cost * 0.9);
                            break;
                        case "Merchant":
                            cost = Convert.ToInt32(cost * 0.75);
                            break;
                        default:
                            break;
                    }
                }
            }
            if (Gold - cost >= 0)
            {
                Gold -= cost;
                weaponsObtained[buyWeaponSelectedWeapon.WeaponName] = true;
                lbDisplay.Items.Add("Blacksmith: A fine choice! I am sure it won't disappoint you later.");
                lbDisplay.ScrollIntoView(lbDisplay.Items[lbDisplay.Items.Count - 1]);
                purchasableWeapons.Remove(buyWeaponSelectedWeapon);
            }
            else
            {
                lbDisplay.Items.Add("Blacksmith: I'm afraid you can't afford this right now. Why not look at something else?");
                lbDisplay.ScrollIntoView(lbDisplay.Items[lbDisplay.Items.Count - 1]);
            }
            BlacksmithBuyWeaponChooseWeaponReEntry();
        }

        public void BlacksmithBuyWeaponChooseWeaponReEntry()
        {
            tbInputArea.Text = "";
            lbOptions.Items.Clear();
            lbOptions.Items.Add("1. Cancel");
            BlacksmithBuyWeaponWeapons();
            btInput.Click += new RoutedEventHandler(BlacksmithBuyWeaponChooseWeapon);
        }

        //Buy Weapon ends here -----------------------------------------------------------------------------------------

        //Buy Armor starts here ----------------------------------------------------------------------------------------

        public void BlacksmithBuyArmorArmors()
        {
            int counter = 2;
            purchasableArmors.Clear();
            foreach (Armor armor in Initializer.armors)
            {
                if (armorsObtained[armor.ArmorName] == false && armor.Price > 0)
                {
                    lbOptions.Items.Add($"{counter}. {armor.ArmorName}");
                    counter++;
                    purchasableArmors.Add(armor);
                }
            }
        }

        public void BlacksmithBuyArmor()
        {
            tbInputArea.Text = "";
            lbOptions.Items.Clear();
            lbOptions.Items.Add("1. Cancel");
            lbDisplay.Items.Add("Blacksmith: Here...");
            lbDisplay.ScrollIntoView(lbDisplay.Items[lbDisplay.Items.Count - 1]);
            BlacksmithBuyArmorArmors();
            lbDisplay.Items.Add("Blacksmith: ... is the list. Anything that catches your eyes?");
            lbDisplay.ScrollIntoView(lbDisplay.Items[lbDisplay.Items.Count - 1]);
            btInput.Click += new RoutedEventHandler(BlacksmithBuyArmorChooseArmor);
        }

        public void BlacksmithBuyArmorChooseArmor(object sender, RoutedEventArgs e)
        {
            btInput.Click -= new RoutedEventHandler(BlacksmithBuyArmorChooseArmor);
            if (tbInputArea.Text == "?")
            {
                tbInputArea.Text = "";
                lbDisplay.Items.Add("EXPLANATION: You can cancel the process by using the Cancel option or first inspect an armor then buy said armor by selecting one of the available armors from the left.");
                lbDisplay.ScrollIntoView(lbDisplay.Items[lbDisplay.Items.Count - 1]);
                btInput.Click += new RoutedEventHandler(BlacksmithBuyArmorChooseArmor);
            }
            else if (tbInputArea.Text == "1" || tbInputArea.Text == "Cancel")
            {
                purchasableArmors.Clear();
                BlacksmithMainOptionReEntry();
            }
            else
            {
                List<string> purchasableArmorsName = new List<string>(purchasableArmors.Select(x => x.ArmorName));
                if (purchasableArmorsName.Contains(tbInputArea.Text) == true || tbInputArea.Text.Contains("0") || tbInputArea.Text.Contains("1") || tbInputArea.Text.Contains("2") || tbInputArea.Text.Contains("3") || tbInputArea.Text.Contains("4") || tbInputArea.Text.Contains("5") || tbInputArea.Text.Contains("6") || tbInputArea.Text.Contains("7") || tbInputArea.Text.Contains("8") || tbInputArea.Text.Contains("9"))
                {
                    if (purchasableArmorsName.Contains(tbInputArea.Text) == true)
                    {
                        foreach (Armor armor in purchasableArmors)
                        {
                            if (armor.ArmorName == tbInputArea.Text)
                            {
                                buyArmorSelectedArmor = armor;
                            }
                        }
                        tbInputArea.Text = "";
                        BlacksmithBuyArmorChosenArmor();
                    }
                    else
                    {
                        try
                        {
                            int index = Convert.ToInt32(tbInputArea.Text) - 2;
                            buyArmorSelectedArmor = purchasableArmors[index];
                            tbInputArea.Text = "";
                            BlacksmithBuyArmorChosenArmor();
                        }
                        catch
                        {
                            MessageBox.Show("Please use the textbox at the bottom of the window to write a valid option from the left.");
                            btInput.Click += new RoutedEventHandler(BlacksmithBuyArmorChooseArmor);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Please use the textbox at the bottom of the window to write a valid option from the left.");
                    btInput.Click += new RoutedEventHandler(BlacksmithBuyArmorChooseArmor);
                }
            }
        }

        public void BlacksmithBuyArmorChosenArmor()
        {
            string output = "";
            lbOptions.Items.Clear();
            lbOptions.Items.Add("1. Cancel");
            lbOptions.Items.Add("2. Buy");
            lbDisplay.Items.Add($"Armor:{buyArmorSelectedArmor.ArmorName} DEF:{buyArmorSelectedArmor.DEF} MDEF:{buyArmorSelectedArmor.MDEF} Slot:{buyArmorSelectedArmor.Type} Unique:{buyArmorSelectedArmor.Unique} Price:{buyArmorSelectedArmor.Price}");
            for (int i = 0; i < buyArmorSelectedArmor.SpecialEffects.Count; i++)
            {
                if (i < buyArmorSelectedArmor.SpecialEffects.Count - 1)
                {
                    output += buyArmorSelectedArmor.SpecialEffects[i].SpecialEffectName + ",";
                }
                else
                {
                    output += buyArmorSelectedArmor.SpecialEffects[i].SpecialEffectName;
                }
            }
            lbDisplay.Items.Add($"Special effects:{output}");
            lbDisplay.Items.Add($"Description:{buyArmorSelectedArmor.Description}");
            lbDisplay.Items.Add($"Current gold:{Gold}");
            lbDisplay.ScrollIntoView(lbDisplay.Items[lbDisplay.Items.Count - 1]);
            btInput.Click += new RoutedEventHandler(BlacksmithBuyArmorChosenArmorBuyOrCancel);
        }

        public void BlacksmithBuyArmorChosenArmorBuyOrCancel(object sender, RoutedEventArgs e)
        {
            btInput.Click -= new RoutedEventHandler(BlacksmithBuyArmorChosenArmorBuyOrCancel);
            switch (tbInputArea.Text)
            {
                case "?":
                    lbDisplay.Items.Add("Blacksmith: You buying or not?");
                    btInput.Click += new RoutedEventHandler(BlacksmithBuyArmorChosenArmorBuyOrCancel);
                    break;
                case "1":
                    lbDisplay.Items.Add("Blacksmith: Not what you're looking for? Then just look around some more.");
                    lbDisplay.ScrollIntoView(lbDisplay.Items[lbDisplay.Items.Count - 1]);
                    BlacksmithBuyArmorChooseArmorReEntry();
                    break;
                case "Cancel":
                    lbDisplay.Items.Add("Blacksmith: Not what you're looking for? Then just look around some more.");
                    lbDisplay.ScrollIntoView(lbDisplay.Items[lbDisplay.Items.Count - 1]);
                    BlacksmithBuyArmorChooseArmorReEntry();
                    break;
                case "2":
                    BlacksmithBuyArmorChosenArmorBought();
                    break;
                case "Buy":
                    BlacksmithBuyArmorChosenArmorBought();
                    break;
                default:
                    MessageBox.Show("Please use the textbox at the bottom of the window to write a valid option from the left.");
                    btInput.Click += new RoutedEventHandler(BlacksmithBuyArmorChosenArmorBuyOrCancel);
                    break;
            }
        }

        public void BlacksmithBuyArmorChosenArmorBought()
        {
            int cost = buyArmorSelectedArmor.Price;
            foreach (Passive passive in party[0].Passives)
            {
                if (passive.Affect.Contains("Shop Payment"))
                {
                    switch (passive.PassiveName)
                    {
                        case "Noble":
                            cost = Convert.ToInt32(cost * 0.9);
                            break;
                        case "Merchant":
                            cost = Convert.ToInt32(cost * 0.75);
                            break;
                        default:
                            break;
                    }
                }
            }
            if (Gold - cost >= 0)
            {
                Gold -= cost;
                armorsObtained[buyArmorSelectedArmor.ArmorName] = true;
                lbDisplay.Items.Add("Blacksmith: A fine choice! I am sure it won't disappoint you later.");
                lbDisplay.ScrollIntoView(lbDisplay.Items[lbDisplay.Items.Count - 1]);
                purchasableArmors.Remove(buyArmorSelectedArmor);
            }
            else
            {
                lbDisplay.Items.Add("Blacksmith: I'm afraid you can't afford this right now. Why not look at something else?");
                lbDisplay.ScrollIntoView(lbDisplay.Items[lbDisplay.Items.Count - 1]);
            }
            BlacksmithBuyArmorChooseArmorReEntry();
        }

        public void BlacksmithBuyArmorChooseArmorReEntry()
        {
            tbInputArea.Text = "";
            lbOptions.Items.Clear();
            lbOptions.Items.Add("1. Cancel");
            BlacksmithBuyArmorArmors();
            btInput.Click += new RoutedEventHandler(BlacksmithBuyArmorChooseArmor);
        }

        //Buy Armor ends here ------------------------------------------------------------------------------------------

        //Improve Weapon starts here -----------------------------------------------------------------------------------

        public void BlacksmithImproveWeaponWeapons()
        {
            int counter = 2;
            improvableWeapons.Clear();
            foreach (Weapon weapon in Initializer.weapons)
            {
                if (weaponsObtained[weapon.WeaponName] == true && weapon.WeaponName != "Unarmed")
                {
                    lbOptions.Items.Add($"{counter}. {weapon.WeaponName}");
                    counter++;
                    improvableWeapons.Add(weapon);
                }
            }
        }

        public void BlacksmithImproveWeapon()
        {
            tbInputArea.Text = "";
            lbOptions.Items.Clear();
            lbOptions.Items.Add("1. Cancel");
            lbDisplay.Items.Add("Blacksmith: Here...");
            lbDisplay.ScrollIntoView(lbDisplay.Items[lbDisplay.Items.Count - 1]);
            BlacksmithImproveWeaponWeapons();
            lbDisplay.Items.Add("Blacksmith: ... is the list. What do you want me to improve?");
            lbDisplay.ScrollIntoView(lbDisplay.Items[lbDisplay.Items.Count - 1]);
            btInput.Click += new RoutedEventHandler(BlacksmithImproveWeaponChooseWeapon);
        }

        public void BlacksmithImproveWeaponChooseWeapon(object sender, RoutedEventArgs e)
        {
            btInput.Click -= new RoutedEventHandler(BlacksmithImproveWeaponChooseWeapon);
            if (tbInputArea.Text == "?")
            {
                tbInputArea.Text = "";
                lbDisplay.Items.Add("EXPLANATION: You can cancel the process by using the Cancel option or first inspect a weapon then improve said weapon by selecting one of the available weapons from the left.");
                lbDisplay.ScrollIntoView(lbDisplay.Items[lbDisplay.Items.Count - 1]);
                btInput.Click += new RoutedEventHandler(BlacksmithImproveWeaponChooseWeapon);
            }
            else if (tbInputArea.Text == "1" || tbInputArea.Text == "Cancel")
            {
                improvableWeapons.Clear();
                BlacksmithMainOptionReEntry();
            }
            else
            {
                List<string> improvableWeaponsName = new List<string>(improvableWeapons.Select(x => x.WeaponName));
                if (improvableWeaponsName.Contains(tbInputArea.Text) == true || tbInputArea.Text.Contains("0") || tbInputArea.Text.Contains("1") || tbInputArea.Text.Contains("2") || tbInputArea.Text.Contains("3") || tbInputArea.Text.Contains("4") || tbInputArea.Text.Contains("5") || tbInputArea.Text.Contains("6") || tbInputArea.Text.Contains("7") || tbInputArea.Text.Contains("8") || tbInputArea.Text.Contains("9"))
                {
                    if (improvableWeaponsName.Contains(tbInputArea.Text) == true)
                    {
                        foreach (Weapon weapon in improvableWeapons)
                        {
                            if (weapon.WeaponName == tbInputArea.Text)
                            {
                                improveWeaponSelectedWeapon = weapon;
                            }
                        }
                        tbInputArea.Text = "";
                        BlacksmithImproveWeaponChosenWeapon();
                    }
                    else
                    {
                        try
                        {
                            int index = Convert.ToInt32(tbInputArea.Text) - 2;
                            improveWeaponSelectedWeapon = improvableWeapons[index];
                            tbInputArea.Text = "";
                            BlacksmithImproveWeaponChosenWeapon();
                        }
                        catch
                        {
                            MessageBox.Show("Please use the textbox at the bottom of the window to write a valid option from the left.");
                            btInput.Click += new RoutedEventHandler(BlacksmithImproveWeaponChooseWeapon);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Please use the textbox at the bottom of the window to write a valid option from the left.");
                    btInput.Click += new RoutedEventHandler(BlacksmithImproveWeaponChooseWeapon);
                }
            }
        }

        public void BlacksmithImproveWeaponChosenWeapon()
        {
            lbOptions.Items.Clear();
            lbOptions.Items.Add("1. Cancel");
            lbOptions.Items.Add("2. Improve");
            lbDisplay.Items.Add($"Weapon:{improveWeaponSelectedWeapon.WeaponName} ATK:{improveWeaponSelectedWeapon.ATK} Improvements:{weaponsImproved[improveWeaponSelectedWeapon.WeaponName]} Price:{(weaponsImproved[improveWeaponSelectedWeapon.WeaponName] + 1) * improveWeaponSelectedWeapon.ATK}");
            lbDisplay.Items.Add($"Current gold:{Gold}");
            lbDisplay.ScrollIntoView(lbDisplay.Items[lbDisplay.Items.Count - 1]);
            btInput.Click += new RoutedEventHandler(BlacksmithImproveWeaponChosenWeaponImproveOrCancel);
        }

        public void BlacksmithImproveWeaponChosenWeaponImproveOrCancel(object sender, RoutedEventArgs e)
        {
            btInput.Click -= new RoutedEventHandler(BlacksmithImproveWeaponChosenWeaponImproveOrCancel);
            switch (tbInputArea.Text)
            {
                case "?":
                    lbDisplay.Items.Add("Blacksmith: You want it improved or not?");
                    btInput.Click += new RoutedEventHandler(BlacksmithImproveWeaponChosenWeaponImproveOrCancel);
                    break;
                case "1":
                    lbDisplay.Items.Add("Blacksmith: I can understand. It is a commitment after all sticking to one weapon.");
                    lbDisplay.ScrollIntoView(lbDisplay.Items[lbDisplay.Items.Count - 1]);
                    BlacksmithImproveWeaponChooseWeaponReEntry();
                    break;
                case "Cancel":
                    lbDisplay.Items.Add("Blacksmith: I can understand. It is a commitment after all sticking to one weapon.");
                    lbDisplay.ScrollIntoView(lbDisplay.Items[lbDisplay.Items.Count - 1]);
                    BlacksmithImproveWeaponChooseWeaponReEntry();
                    break;
                case "2":
                    BlacksmithImproveWeaponChosenWeaponImproved();
                    break;
                case "Improve":
                    BlacksmithImproveWeaponChosenWeaponImproved();
                    break;
                default:
                    MessageBox.Show("Please use the textbox at the bottom of the window to write a valid option from the left.");
                    btInput.Click += new RoutedEventHandler(BlacksmithImproveWeaponChosenWeaponImproveOrCancel);
                    break;
            }
        }

        public void BlacksmithImproveWeaponChosenWeaponImproved()
        {
            int successRate = 100;
            int cost = (weaponsImproved[improveWeaponSelectedWeapon.WeaponName] + 1) * improveWeaponSelectedWeapon.ATK;
            foreach (Passive passive in party[0].Passives)
            {
                if (passive.Affect.Contains("Shop Payment"))
                {
                    switch (passive.PassiveName)
                    {
                        case "Noble":
                            cost = Convert.ToInt32(cost * 0.9);
                            break;
                        case "Merchant":
                            cost = Convert.ToInt32(cost * 0.75);
                            break;
                        default:
                            break;
                    }
                }
                else if (passive.Affect.Contains("Upgrade Weapon"))
                {
                    switch (passive.PassiveName)
                    {
                        case "Blacksmith":
                            cost = Convert.ToInt32(cost * 0.2);
                            successRate = random.Next(0, 100);
                            break;
                        case "Dwarf":
                            successRate = 100;
                            break;
                        default:
                            break;
                    }
                }
            }

            if (Gold - cost >= 0 && successRate > 10)
            {
                Gold -= cost;
                weaponsImproved[improveWeaponSelectedWeapon.WeaponName]++;
                lbDisplay.Items.Add("Blacksmith: A fine choice! I am sure it won't disappoint you later.");
                lbDisplay.ScrollIntoView(lbDisplay.Items[lbDisplay.Items.Count - 1]);
                Initializer.weapons[improveWeaponSelectedWeapon.Id - 1].ATK += 1;
                foreach (Hero hero in heroes)
                {
                    foreach (Weapon weapon in hero.Weapons)
                    {
                        if (weapon.WeaponName == improveWeaponSelectedWeapon.WeaponName)
                        {
                            weapon.ATK += 1;
                        }
                    }
                }
            }
            else
            {
                lbDisplay.Items.Add("Blacksmith: I'm afraid you can't afford this right now. Why not look at something else?");
                lbDisplay.ScrollIntoView(lbDisplay.Items[lbDisplay.Items.Count - 1]);
            }
            BlacksmithImproveWeaponChooseWeaponReEntry();
        }

        public void BlacksmithImproveWeaponChooseWeaponReEntry()
        {
            tbInputArea.Text = "";
            lbOptions.Items.Clear();
            lbOptions.Items.Add("1. Cancel");
            BlacksmithImproveWeaponWeapons();
            btInput.Click += new RoutedEventHandler(BlacksmithImproveWeaponChooseWeapon);
        }

        //Improve Weapon ends here -------------------------------------------------------------------------------------

        //Improve Armor starts here ------------------------------------------------------------------------------------

        public void BlacksmithImproveArmorArmors()
        {
            int counter = 2;
            improvableArmors.Clear();
            foreach (Armor armor in Initializer.armors)
            {
                if (armorsObtained[armor.ArmorName] == true && armor.ArmorName != "None")
                {
                    lbOptions.Items.Add($"{counter}. {armor.ArmorName}");
                    counter++;
                    improvableArmors.Add(armor);
                }
            }
        }

        public void BlacksmithImproveArmor()
        {
            tbInputArea.Text = "";
            lbOptions.Items.Clear();
            lbOptions.Items.Add("1. Cancel");
            lbDisplay.Items.Add("Blacksmith: Here...");
            lbDisplay.ScrollIntoView(lbDisplay.Items[lbDisplay.Items.Count - 1]);
            BlacksmithImproveArmorArmors();
            lbDisplay.Items.Add("Blacksmith: ... is the list. What do you want me to improve?");
            lbDisplay.ScrollIntoView(lbDisplay.Items[lbDisplay.Items.Count - 1]);
            btInput.Click += new RoutedEventHandler(BlacksmithImproveArmorChooseArmor);
        }

        public void BlacksmithImproveArmorChooseArmor(object sender, RoutedEventArgs e)
        {
            btInput.Click -= new RoutedEventHandler(BlacksmithImproveArmorChooseArmor);
            if (tbInputArea.Text == "?")
            {
                tbInputArea.Text = "";
                lbDisplay.Items.Add("EXPLANATION: You can cancel the process by using the Cancel option or first inspect an armor then improve said armor by selecting one of the available armors from the left.");
                lbDisplay.ScrollIntoView(lbDisplay.Items[lbDisplay.Items.Count - 1]);
                btInput.Click += new RoutedEventHandler(BlacksmithImproveArmorChooseArmor);
            }
            else if (tbInputArea.Text == "1" || tbInputArea.Text == "Cancel")
            {
                improvableArmors.Clear();
                BlacksmithMainOptionReEntry();
            }
            else
            {
                List<string> improvableArmorsName = new List<string>(improvableArmors.Select(x => x.ArmorName));
                if (improvableArmorsName.Contains(tbInputArea.Text) == true || tbInputArea.Text.Contains("0") || tbInputArea.Text.Contains("1") || tbInputArea.Text.Contains("2") || tbInputArea.Text.Contains("3") || tbInputArea.Text.Contains("4") || tbInputArea.Text.Contains("5") || tbInputArea.Text.Contains("6") || tbInputArea.Text.Contains("7") || tbInputArea.Text.Contains("8") || tbInputArea.Text.Contains("9"))
                {
                    if (improvableArmorsName.Contains(tbInputArea.Text) == true)
                    {
                        foreach (Armor armor in improvableArmors)
                        {
                            if (armor.ArmorName == tbInputArea.Text)
                            {
                                improveArmorSelectedArmor = armor;
                            }
                        }
                        tbInputArea.Text = "";
                        BlacksmithImproveArmorChosenArmor();
                    }
                    else
                    {
                        try
                        {
                            int index = Convert.ToInt32(tbInputArea.Text) - 2;
                            improveArmorSelectedArmor = improvableArmors[index];
                            tbInputArea.Text = "";
                            BlacksmithImproveArmorChosenArmor();
                        }
                        catch
                        {
                            MessageBox.Show("Please use the textbox at the bottom of the window to write a valid option from the left.");
                            btInput.Click += new RoutedEventHandler(BlacksmithImproveArmorChooseArmor);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Please use the textbox at the bottom of the window to write a valid option from the left.");
                    btInput.Click += new RoutedEventHandler(BlacksmithImproveArmorChooseArmor);
                }
            }
        }

        public void BlacksmithImproveArmorChosenArmor()
        {
            lbOptions.Items.Clear();
            lbOptions.Items.Add("1. Cancel");
            lbOptions.Items.Add("2. Improve");
            lbDisplay.Items.Add($"Armor:{improveArmorSelectedArmor.ArmorName} DEF:{improveArmorSelectedArmor.DEF} MDEF:{improveArmorSelectedArmor.MDEF} Improvements:{armorsImproved[improveArmorSelectedArmor.ArmorName]} Price:{(armorsImproved[improveArmorSelectedArmor.ArmorName] + 1) * ((improveArmorSelectedArmor.DEF + improveArmorSelectedArmor.MDEF) / 2)}");
            lbDisplay.Items.Add($"Current gold:{Gold}");
            lbDisplay.ScrollIntoView(lbDisplay.Items[lbDisplay.Items.Count - 1]);
            btInput.Click += new RoutedEventHandler(BlacksmithImproveArmorChosenArmorImproveOrCancel);
        }

        public void BlacksmithImproveArmorChosenArmorImproveOrCancel(object sender, RoutedEventArgs e)
        {
            btInput.Click -= new RoutedEventHandler(BlacksmithImproveArmorChosenArmorImproveOrCancel);
            switch (tbInputArea.Text)
            {
                case "?":
                    lbDisplay.Items.Add("Blacksmith: You want it improved or not?");
                    btInput.Click += new RoutedEventHandler(BlacksmithImproveArmorChosenArmorImproveOrCancel);
                    break;
                case "1":
                    lbDisplay.Items.Add("Blacksmith: I can understand. It is a commitment after all sticking to one armor.");
                    lbDisplay.ScrollIntoView(lbDisplay.Items[lbDisplay.Items.Count - 1]);
                    BlacksmithImproveArmorChooseArmorReEntry();
                    break;
                case "Cancel":
                    lbDisplay.Items.Add("Blacksmith: I can understand. It is a commitment after all sticking to one armor.");
                    lbDisplay.ScrollIntoView(lbDisplay.Items[lbDisplay.Items.Count - 1]);
                    BlacksmithImproveArmorChooseArmorReEntry();
                    break;
                case "2":
                    BlacksmithImproveArmorChosenArmorImproved();
                    break;
                case "Improve":
                    BlacksmithImproveArmorChosenArmorImproved();
                    break;
                default:
                    MessageBox.Show("Please use the textbox at the bottom of the window to write a valid option from the left.");
                    btInput.Click += new RoutedEventHandler(BlacksmithImproveArmorChosenArmorImproveOrCancel);
                    break;
            }
        }

        public void BlacksmithImproveArmorChosenArmorImproved()
        {
            int successRate = 100;
            int cost = 0;
            if ((improveArmorSelectedArmor.DEF + improveArmorSelectedArmor.MDEF) / 2 < 0)
            {
                cost = ((improveArmorSelectedArmor.DEF + improveArmorSelectedArmor.MDEF) / 2) * -1;
            }
            else if ((improveArmorSelectedArmor.DEF + improveArmorSelectedArmor.MDEF) / 2 == 0)
            {
                cost = (armorsImproved[improveArmorSelectedArmor.ArmorName] + 1) * 12;
            }
            else
            {
                cost = (improveArmorSelectedArmor.DEF + improveArmorSelectedArmor.MDEF) / 2;
            }
            cost = cost * (armorsImproved[improveArmorSelectedArmor.ArmorName] + 1);

            foreach (Passive passive in party[0].Passives)
            {
                if (passive.Affect.Contains("Shop Payment"))
                {
                    switch (passive.PassiveName)
                    {
                        case "Noble":
                            cost = Convert.ToInt32(cost * 0.9);
                            break;
                        case "Merchant":
                            cost = Convert.ToInt32(cost * 0.75);
                            break;
                        default:
                            break;
                    }
                }
                else if (passive.Affect.Contains("Upgrade Armor"))
                {
                    switch (passive.PassiveName)
                    {
                        case "Blacksmith":
                            cost = Convert.ToInt32(cost * 0.2);
                            successRate = random.Next(0, 100);
                            break;
                        case "Dwarf":
                            successRate = 100;
                            break;
                        default:
                            break;
                    }
                }
            }

            if (Gold - cost >= 0 && successRate > 10)
            {
                Gold -= cost;
                armorsImproved[improveArmorSelectedArmor.ArmorName]++;
                lbDisplay.Items.Add("Blacksmith: A fine choice! I am sure it won't disappoint you later.");
                lbDisplay.ScrollIntoView(lbDisplay.Items[lbDisplay.Items.Count - 1]);
                Initializer.armors[improveArmorSelectedArmor.Id - 1].DEF += 1;
                Initializer.armors[improveArmorSelectedArmor.Id - 1].MDEF += 1;
                foreach (Hero hero in heroes)
                {
                    foreach (Armor armor in hero.Armors)
                    {
                        if (armor.ArmorName == improveArmorSelectedArmor.ArmorName)
                        {
                            armor.DEF += 1;
                            armor.MDEF += 1;
                        }
                    }
                }
            }
            else
            {
                lbDisplay.Items.Add("Blacksmith: I'm afraid you can't afford this right now. Why not look at something else?");
                lbDisplay.ScrollIntoView(lbDisplay.Items[lbDisplay.Items.Count - 1]);
            }
            BlacksmithImproveArmorChooseArmorReEntry();
        }

        public void BlacksmithImproveArmorChooseArmorReEntry()
        {
            tbInputArea.Text = "";
            lbOptions.Items.Clear();
            lbOptions.Items.Add("1. Cancel");
            BlacksmithImproveArmorArmors();
            btInput.Click += new RoutedEventHandler(BlacksmithImproveArmorChooseArmor);
        }

        //Improve Armor ends here --------------------------------------------------------------------------------------

        //Change Equipment starts here ---------------------------------------------------------------------------------

        public void BlacksmithChangeEquipmentPartyMembers()
        {
            for (int i = 0; i < party.Count; i++)
            {
                lbOptions.Items.Add($"{i+2}. {party[i].HeroName}");
            }
        }

        public void BlacksmithChangeEquipmentFirstStep()
        {
            tbInputArea.Text = "";
            lbOptions.Items.Clear();
            lbDisplay.Items.Add("GAME: First choose whose equipment you want to change.");
            lbDisplay.ScrollIntoView(lbDisplay.Items[lbDisplay.Items.Count - 1]);
            lbOptions.Items.Add("1. Cancel");
            BlacksmithChangeEquipmentPartyMembers();
            btInput.Click += new RoutedEventHandler(BlacksmithChangeEquipmentChoosePartyMember);
        }

        public void BlacksmithChangeEquipmentChoosePartyMember(object sender, RoutedEventArgs e)
        {
            btInput.Click -= new RoutedEventHandler(BlacksmithChangeEquipmentChoosePartyMember);
            List<string> partyMembers = new List<string>(party.Select(x => x.HeroName));
            if (tbInputArea.Text == "?")
            {
                tbInputArea.Text = "";
                lbDisplay.Items.Add("EXPLANATION: To change equipment first choose a party member or exit by using the cancel option");
                lbDisplay.ScrollIntoView(lbDisplay.Items[lbDisplay.Items.Count - 1]);
                btInput.Click += new RoutedEventHandler(BlacksmithChangeEquipmentChoosePartyMember);
            }
            else if (tbInputArea.Text == "1" ||  tbInputArea.Text == "Cancel")
            {
                BlacksmithMainOptionReEntry();
            }
            else if (partyMembers.Contains(tbInputArea.Text) == true || tbInputArea.Text.Contains("0") || tbInputArea.Text.Contains("1") || tbInputArea.Text.Contains("2") || tbInputArea.Text.Contains("3") || tbInputArea.Text.Contains("4") || tbInputArea.Text.Contains("5") || tbInputArea.Text.Contains("6") || tbInputArea.Text.Contains("7") || tbInputArea.Text.Contains("8") || tbInputArea.Text.Contains("9"))
            {
                if (partyMembers.Contains(tbInputArea.Text) == true)
                {
                    foreach (Hero hero in party)
                    {
                        if (hero.HeroName == tbInputArea.Text)
                        {
                            selectedHero = hero;
                        }
                    }
                    BlacksmithChangeEquipmentSecondStep();
                }
                else
                {
                    try
                    {
                        int index = Convert.ToInt32(tbInputArea.Text) - 2;
                        selectedHero = party[index];
                        BlacksmithChangeEquipmentSecondStep();
                    }
                    catch
                    {
                        MessageBox.Show("Please use the textbox at the bottom of the window to write a valid option from the left.");
                        btInput.Click += new RoutedEventHandler(BlacksmithChangeEquipmentChoosePartyMember);
                    }
                }
            }
            else
            {
                MessageBox.Show("Please use the textbox at the bottom of the window to write a valid option from the left.");
                btInput.Click += new RoutedEventHandler(BlacksmithChangeEquipmentChoosePartyMember);
            }
        }

        public void BlacksmithChangeEquipmentSecondStep()
        {
            tbInputArea.Text = "";
            lbOptions.Items.Clear();
            lbDisplay.Items.Add("GAME: Second choose what equipment you want to change.");
            lbDisplay.ScrollIntoView(lbDisplay.Items[lbDisplay.Items.Count - 1]);
            lbOptions.Items.Add("1. Weapons");
            lbOptions.Items.Add("2. Armors");
            lbOptions.Items.Add("3. Cancel");
            btInput.Click += new RoutedEventHandler(BlacksmithChangeEquipmentChooseEquipmentType);
        }

        public void BlacksmithChangeEquipmentChooseEquipmentType(object sender, RoutedEventArgs e)
        {
            btInput.Click -= new RoutedEventHandler(BlacksmithChangeEquipmentChooseEquipmentType);
            switch (tbInputArea.Text)
            {
                case "?":
                    tbInputArea.Text = "";
                    lbDisplay.Items.Add("EXPLANATION: By choosing 'Weapons' or 'Armors' you can choose what equipment you want to change afterwards. You can also change which party member you want to modify by using the cancel option.");
                    lbDisplay.ScrollIntoView(lbDisplay.Items[lbDisplay.Items.Count - 1]);
                    btInput.Click += new RoutedEventHandler(BlacksmithChangeEquipmentChooseEquipmentType);
                    break;
                case "1":
                    BlacksmithChangeWeapon();
                    break;
                case "Weapons":
                    BlacksmithChangeWeapon();
                    break;
                case "2":
                    BlacksmithChangeArmor();
                    break;
                case "Armors":
                    BlacksmithChangeArmor();
                    break;
                case "3":
                    BlacksmithChangeEquipmentChoosePartyMemberReEntry();
                    break;
                case "Cancel":
                    BlacksmithChangeEquipmentChoosePartyMemberReEntry();
                    break;
                default:
                    MessageBox.Show("Please use the textbox at the bottom of the window to write a valid option from the left.");
                    btInput.Click += new RoutedEventHandler(BlacksmithChangeEquipmentChooseEquipmentType);
                    break;
            }
        }

        public void BlacksmithChangeEquipmentChoosePartyMemberReEntry()
        {
            tbInputArea.Text = "";
            lbOptions.Items.Clear();
            lbOptions.Items.Add("1. Cancel");
            BlacksmithChangeEquipmentPartyMembers();
            btInput.Click += new RoutedEventHandler(BlacksmithChangeEquipmentChoosePartyMember);
        }

        //Change Equipment Weapon starts here --------------------------------------------------------------------------

        public void BlacksmithChangeWeaponDisplayWeaponStats()
        {
            string output = "";
            lbDisplay.Items.Add($"1. Weapon:{selectedHero.Weapons[0].WeaponName} ATK:{selectedHero.Weapons[0].ATK} Crit:{selectedHero.Weapons[0].CritChance}% {selectedHero.Weapons[0].CritDamage}x Damage type:{selectedHero.Weapons[0].DamageType} Range:{selectedHero.Weapons[0].Range} Skill compatibility:{selectedHero.Weapons[0].SkillCompatibility} Unique:{selectedHero.Weapons[0].Unique}");
            for (int i = 0; i < selectedHero.Weapons[0].SpecialEffects.Count; i++)
            {
                if (i < selectedHero.Weapons[0].SpecialEffects.Count - 1)
                {
                    output += selectedHero.Weapons[0].SpecialEffects[i].SpecialEffectName + ",";
                }
                else
                {
                    output += selectedHero.Weapons[0].SpecialEffects[i].SpecialEffectName;
                }
            }
            lbDisplay.Items.Add($"Special effects:{output}");
            lbDisplay.Items.Add($"Description:{selectedHero.Weapons[0].Description}");
            lbDisplay.Items.Add($"2. Weapon:{selectedHero.Weapons[1].WeaponName} ATK:{selectedHero.Weapons[1].ATK} Crit:{selectedHero.Weapons[1].CritChance}% {selectedHero.Weapons[1].CritDamage}x Damage type:{selectedHero.Weapons[1].DamageType} Range:{selectedHero.Weapons[1].Range} Skill compatibility:{selectedHero.Weapons[1].SkillCompatibility} Unique:{selectedHero.Weapons[1].Unique}");
            for (int i = 0; i < selectedHero.Weapons[1].SpecialEffects.Count; i++)
            {
                if (i < selectedHero.Weapons[1].SpecialEffects.Count - 1)
                {
                    output += selectedHero.Weapons[1].SpecialEffects[i].SpecialEffectName + ",";
                }
                else
                {
                    output += selectedHero.Weapons[1].SpecialEffects[i].SpecialEffectName;
                }
            }
            lbDisplay.Items.Add($"Special effects:{output}");
            lbDisplay.Items.Add($"Description:{selectedHero.Weapons[1].Description}");
            lbDisplay.Items.Add($"3. Weapon:{selectedHero.Weapons[2].WeaponName} ATK:{selectedHero.Weapons[2].ATK} Crit:{selectedHero.Weapons[2].CritChance}% {selectedHero.Weapons[2].CritDamage}x Damage type:{selectedHero.Weapons[2].DamageType} Range:{selectedHero.Weapons[2].Range} Skill compatibility:{selectedHero.Weapons[2].SkillCompatibility} Unique:{selectedHero.Weapons[2].Unique}");
            for (int i = 0; i < selectedHero.Weapons[2].SpecialEffects.Count; i++)
            {
                if (i < selectedHero.Weapons[2].SpecialEffects.Count - 1)
                {
                    output += selectedHero.Weapons[2].SpecialEffects[i].SpecialEffectName + ",";
                }
                else
                {
                    output += selectedHero.Weapons[2].SpecialEffects[i].SpecialEffectName;
                }
            }
            lbDisplay.Items.Add($"Special effects:{output}");
            lbDisplay.Items.Add($"Description:{selectedHero.Weapons[2].Description}");
            lbDisplay.ScrollIntoView(lbDisplay.Items[lbDisplay.Items.Count - 1]);
        }

        public void BlacksmithChangeWeapon()
        {
            tbInputArea.Text = "";
            lbOptions.Items.Clear();
            lbOptions.Items.Add($"1. {selectedHero.Weapons[0].WeaponName}");
            lbOptions.Items.Add($"2. {selectedHero.Weapons[1].WeaponName}");
            lbOptions.Items.Add($"3. {selectedHero.Weapons[2].WeaponName}");
            lbOptions.Items.Add("4. Cancel");
            lbDisplay.Items.Add("GAME: Choose which weapon you want to change.");
            lbDisplay.ScrollIntoView(lbDisplay.Items[lbDisplay.Items.Count - 1]);
            BlacksmithChangeWeaponDisplayWeaponStats();
            btInput.Click += new RoutedEventHandler(BlacksmithChangeWeaponChooseWeapon);
        }

        public void BlacksmithChangeWeaponChooseWeapon(object sender, RoutedEventArgs e)
        {
            btInput.Click -= new RoutedEventHandler(BlacksmithChangeWeaponChooseWeapon);
            if (tbInputArea.Text == "?")
            {
                lbDisplay.Items.Add("EXPLANATION: By choosing a weapon you can then choose a different weapon to replace it.");
                lbDisplay.ScrollIntoView(lbDisplay.Items[lbDisplay.Items.Count - 1]);
                btInput.Click += new RoutedEventHandler(BlacksmithChangeWeaponChooseWeapon);
            }
            else if (tbInputArea.Text == "4" || tbInputArea.Text == "Cancel")
            {
                BlacksmithChangeEquipmentChooseEquipmentTypeReEntry();
            }
            else if (tbInputArea.Text == selectedHero.Weapons[0].WeaponName || tbInputArea.Text == selectedHero.Weapons[1].WeaponName || tbInputArea.Text == selectedHero.Weapons[2].WeaponName)
            {
                foreach (Weapon weapon in selectedHero.Weapons)
                {
                    if (weapon.WeaponName == tbInputArea.Text)
                    {
                        selectedWeapon = weapon;
                    }
                }
                BlacksmithChangeWeaponNewWeapon();
            }
            else if (tbInputArea.Text.Contains("1") || tbInputArea.Text.Contains("2") || tbInputArea.Text.Contains("3"))
            {
                try
                {
                    int index = Convert.ToInt32(tbInputArea.Text) - 1;
                    selectedWeapon = selectedHero.Weapons[index];
                    BlacksmithChangeWeaponNewWeapon();
                }
                catch
                {
                    MessageBox.Show("Please use the textbox at the bottom of the window to write a valid option from the left.");
                    btInput.Click += new RoutedEventHandler(BlacksmithChangeWeaponChooseWeapon);
                }
            }
            else
            {
                MessageBox.Show("Please use the textbox at the bottom of the window to write a valid option from the left.");
                btInput.Click += new RoutedEventHandler(BlacksmithChangeWeaponChooseWeapon);
            }
        }

        public void BlacksmithChangerWeaponNewWeaponWeapons()
        {
            selectableNewWeapons.Clear();
            int counter = 0;
            foreach (Weapon weapon in Initializer.weapons)
            {
                if (weaponsObtained[weapon.WeaponName] == true)
                {
                    lbOptions.Items.Add($"{counter + 2}. {weapon.WeaponName}");
                    selectableNewWeapons.Add(weapon);
                    counter++;
                }
            }
        }

        public void BlacksmithChangeWeaponNewWeapon()
        {
            tbInputArea.Text = "";
            lbOptions.Items.Clear();
            lbOptions.Items.Add("1. Cancel");
            BlacksmithChangerWeaponNewWeaponWeapons();
            btInput.Click += new RoutedEventHandler(BlacksmithChangeWeaponChooseNewWeapon);
        }

        public void BlacksmithChangeWeaponChooseNewWeapon(object sender, RoutedEventArgs e)
        {
            btInput.Click -= new RoutedEventHandler(BlacksmithChangeWeaponChooseNewWeapon);
            List<string> selectableNewWeaponsName = new List<string>(selectableNewWeapons.Select(x => x.WeaponName));
            if (tbInputArea.Text == "?")
            {
                lbDisplay.Items.Add("EXPLANATION: From here you can choose a new weapon then you get to see it's stats and make the decision to change to this new weapon or cancel and go back to the previous weapon choosing option.");
                btInput.Click += new RoutedEventHandler(BlacksmithChangeWeaponChooseNewWeapon);
            }
            else if (tbInputArea.Text == "1" || tbInputArea.Text == "Cancel")
            {
                selectableNewWeapons.Clear();
                BlacksmithChangeWeaponChooseWeaponReEntry();
            }
            else if (selectableNewWeaponsName.Contains(tbInputArea.Text) == true || tbInputArea.Text.Contains("0") || tbInputArea.Text.Contains("1") || tbInputArea.Text.Contains("2") || tbInputArea.Text.Contains("3") || tbInputArea.Text.Contains("4") || tbInputArea.Text.Contains("5") || tbInputArea.Text.Contains("6") || tbInputArea.Text.Contains("7") || tbInputArea.Text.Contains("8") || tbInputArea.Text.Contains("9"))
            {
                if (selectableNewWeaponsName.Contains(tbInputArea.Text) == true)
                {
                    foreach (Weapon weapon in selectableNewWeapons)
                    {
                        if (weapon.WeaponName == tbInputArea.Text)
                        {
                            selectedNewWeapon = weapon;
                        }
                    }
                    BlacksmithChangeWeaponNewWeaponNewWeapon();
                }
                else
                {
                    try
                    {
                        int index = Convert.ToInt32(tbInputArea.Text) - 2;
                        selectedNewWeapon = selectableNewWeapons[index];
                        BlacksmithChangeWeaponNewWeaponNewWeapon();
                    }
                    catch
                    {
                        MessageBox.Show("Please use the textbox at the bottom of the window to write a valid option from the left.");
                        btInput.Click += new RoutedEventHandler(BlacksmithChangeWeaponChooseNewWeapon);
                    }
                }
            }
            else
            {
                MessageBox.Show("Please use the textbox at the bottom of the window to write a valid option from the left.");
                btInput.Click += new RoutedEventHandler(BlacksmithChangeWeaponChooseNewWeapon);
            }
        }

        public void BlacksmithChangeWeaponChooseWeaponReEntry()
        {
            tbInputArea.Text = "";
            lbOptions.Items.Clear();
            lbOptions.Items.Add($"1. {selectedHero.Weapons[0].WeaponName}");
            lbOptions.Items.Add($"2. {selectedHero.Weapons[1].WeaponName}");
            lbOptions.Items.Add($"3. {selectedHero.Weapons[2].WeaponName}");
            lbOptions.Items.Add("4. Cancel");
            btInput.Click += new RoutedEventHandler(BlacksmithChangeWeaponChooseWeapon);
        }

        public void BlacksmithChangeWeaponNewWeaponNewWeaponStats()
        {
            string output = "";
            lbDisplay.Items.Add($"New Weapon:{selectedNewWeapon.WeaponName} ATK:{selectedNewWeapon.ATK} Crit:{selectedNewWeapon.CritChance}% {selectedNewWeapon.CritDamage}x Damage type:{selectedNewWeapon.DamageType} Range:{selectedNewWeapon.Range} Skill compatibility:{selectedNewWeapon.SkillCompatibility} Unique:{selectedNewWeapon.Unique}");
            for (int i = 0; i < selectedNewWeapon.SpecialEffects.Count; i++)
            {
                if (i < selectedNewWeapon.SpecialEffects.Count - 1)
                {
                    output += selectedNewWeapon.SpecialEffects[i].SpecialEffectName + ",";
                }
                else
                {
                    output += selectedNewWeapon.SpecialEffects[i];
                }
            }
            lbDisplay.Items.Add($"Special effects:{output}");
            lbDisplay.Items.Add($"Description:{selectedNewWeapon.Description}");
            lbDisplay.ScrollIntoView(lbDisplay.Items[lbDisplay.Items.Count - 1]);
        }

        public void BlacksmithChangeWeaponNewWeaponNewWeapon()
        {
            tbInputArea.Text = "";
            lbOptions.Items.Clear();
            lbOptions.Items.Add("1. Confirm");
            lbOptions.Items.Add("2. Cancel");
            BlacksmithChangeWeaponNewWeaponNewWeaponStats();
            btInput.Click += new RoutedEventHandler(BlacksmithChangeWeaponNewWeaponChangeConfirmOrCancel);
        }

        public void BlacksmithChangeWeaponNewWeaponChangeConfirmOrCancel(object sender, RoutedEventArgs e)
        {
            btInput.Click -= new RoutedEventHandler(BlacksmithChangeWeaponNewWeaponChangeConfirmOrCancel);
            switch (tbInputArea.Text)
            {
                case "?":
                    tbInputArea.Text = "";
                    lbDisplay.Items.Add($"EXPLANATION: Confirm will replace the currently selected weapon ({selectedWeapon.WeaponName}) from the currently selected party member ({selectedHero.HeroName}). Or you can cancel with the cancel option.");
                    lbDisplay.ScrollIntoView(lbDisplay.Items[lbDisplay.Items.Count - 1]);
                    btInput.Click += new RoutedEventHandler(BlacksmithChangeWeaponNewWeaponChangeConfirmOrCancel);
                    break;
                case "1":
                    BlacksmithChangeWeaponNewWeaponChangeConfirmed();
                    break;
                case "Confirm":
                    BlacksmithChangeWeaponNewWeaponChangeConfirmed();
                    break;
                case "2":
                    BlacksmithChangeWeaponChooseNewWeaponReEntry();
                    break;
                case "Cancel":
                    BlacksmithChangeWeaponChooseNewWeaponReEntry();
                    break;
                default:
                    MessageBox.Show("Please use the textbox at the bottom of the window to write a valid option from the left.");
                    btInput.Click += new RoutedEventHandler(BlacksmithChangeWeaponNewWeaponChangeConfirmOrCancel);
                    break;
            }
        }

        public void BlacksmithChangeWeaponNewWeaponChangeConfirmed()
        {
            bool equipable = true;
            if (selectedNewWeapon.Unique == true)
            {
                foreach (Hero hero in heroes)
                {
                    foreach (Weapon weapon in hero.Weapons)
                    {
                        if (weapon.WeaponName == selectedNewWeapon.WeaponName)
                        {
                            MessageBox.Show($"This is a unique weapon and someone else already has it equipped ({hero.HeroName})");
                            BlacksmithChangeWeaponChooseNewWeaponReEntry();
                            equipable = false;
                        }
                    }
                }
                if (equipable == true)
                {
                    for (int i = 0; i < party.Count(); i++)
                    {
                        if (party[i] == selectedHero)
                        {
                            for (int j = 0; j < party[i].Weapons.Count(); j++)
                            {
                                if (party[i].Weapons[j] == selectedWeapon)
                                {
                                    party[i].Weapons[j] = Initializer.weapons[0];
                                    party[i] = HeroStatCalculation.HeroStatReCalculation(party[i]);
                                    party[i].Weapons[j] = selectedNewWeapon;
                                    party[i] = Weapon.EquipWeaponCheck(party[i], j);
                                }
                            }
                        }
                    }
                    tbInputArea.Text = "";
                    lbOptions.Items.Clear();
                    lbOptions.Items.Add("1. Weapons");
                    lbOptions.Items.Add("2. Armors");
                    lbOptions.Items.Add("3. Cancel");
                    selectableNewWeapons.Clear();
                    btInput.Click += new RoutedEventHandler(BlacksmithChangeEquipmentChooseEquipmentType);
                }
            }
            else
            {
                for (int i = 0; i < party.Count(); i++)
                {
                    if (party[i] == selectedHero)
                    {
                        if (party[i].Weapons.Contains(selectedNewWeapon))
                        {
                            equipable = false;
                        }
                        if (equipable == true)
                        {
                            for (int j = 0; j < party[i].Weapons.Count(); j++)
                            {
                                if (party[i].Weapons[j] == selectedWeapon)
                                {
                                    party[i].Weapons[j] = Initializer.weapons[0];
                                    party[i] = HeroStatCalculation.HeroStatReCalculation(party[i]);
                                    party[i].Weapons[j] = selectedNewWeapon;
                                    party[i] = Weapon.EquipWeaponCheck(party[i], j);
                                }
                            }
                            tbInputArea.Text = "";
                            lbOptions.Items.Clear();
                            lbOptions.Items.Add("1. Weapons");
                            lbOptions.Items.Add("2. Armors");
                            lbOptions.Items.Add("3. Cancel");
                            selectableNewWeapons.Clear();
                            btInput.Click += new RoutedEventHandler(BlacksmithChangeEquipmentChooseEquipmentType);
                        }
                        else
                        {
                            MessageBox.Show("You already have a copy of this weapon equipped and you can't equip another copy.");
                            BlacksmithChangeWeaponChooseNewWeaponReEntry();
                        }
                    }
                }
            }
        }

        public void BlacksmithChangeWeaponChooseNewWeaponReEntry()
        {
            tbInputArea.Text = "";
            lbOptions.Items.Clear();
            lbOptions.Items.Add("1. Cancel");
            BlacksmithChangerWeaponNewWeaponWeapons();
            btInput.Click += new RoutedEventHandler(BlacksmithChangeWeaponChooseNewWeapon);
        }

        //Change Equipment Weapon ends here ----------------------------------------------------------------------------

        //Change Equipment Armor starts here --------------------------------------------------------------------------

        public void BlacksmithChangeArmorDisplayArmorStats()
        {
            string output = "";
            lbDisplay.Items.Add($"1. Armor:{selectedHero.Armors[0].ArmorName} DEF:{selectedHero.Armors[0].DEF} MDEF:{selectedHero.Armors[0].MDEF} Slot:{selectedHero.Armors[0].Type} Unique:{selectedHero.Armors[0].Unique}");
            for (int i = 0; i < selectedHero.Armors[0].SpecialEffects.Count; i++)
            {
                if (i < selectedHero.Armors[0].SpecialEffects.Count - 1)
                {
                    output += selectedHero.Armors[0].SpecialEffects[i].SpecialEffectName + ",";
                }
                else
                {
                    output += selectedHero.Armors[0].SpecialEffects[i].SpecialEffectName;
                }
            }
            lbDisplay.Items.Add($"Special effects:{output}");
            lbDisplay.Items.Add($"Description:{selectedHero.Armors[0].Description}");
            lbDisplay.Items.Add($"2. Armor:{selectedHero.Armors[1].ArmorName} DEF:{selectedHero.Armors[1].DEF} MDEF:{selectedHero.Armors[1].MDEF} Slot:{selectedHero.Armors[1].Type} Unique:{selectedHero.Armors[1].Unique}");
            for (int i = 0; i < selectedHero.Armors[1].SpecialEffects.Count; i++)
            {
                if (i < selectedHero.Armors[1].SpecialEffects.Count - 1)
                {
                    output += selectedHero.Armors[1].SpecialEffects[i].SpecialEffectName + ",";
                }
                else
                {
                    output += selectedHero.Armors[1].SpecialEffects[i].SpecialEffectName;
                }
            }
            lbDisplay.Items.Add($"Special effects:{output}");
            lbDisplay.Items.Add($"Description:{selectedHero.Armors[1].Description}");
            lbDisplay.Items.Add($"3. Armor:{selectedHero.Armors[2].ArmorName} DEF:{selectedHero.Armors[2].DEF} MDEF:{selectedHero.Armors[2].MDEF} Slot:{selectedHero.Armors[2].Type} Unique:{selectedHero.Armors[2].Unique}");
            for (int i = 0; i < selectedHero.Armors[2].SpecialEffects.Count; i++)
            {
                if (i < selectedHero.Armors[2].SpecialEffects.Count - 1)
                {
                    output += selectedHero.Armors[2].SpecialEffects[i].SpecialEffectName + ",";
                }
                else
                {
                    output += selectedHero.Armors[2].SpecialEffects[i].SpecialEffectName;
                }
            }
            lbDisplay.Items.Add($"Special effects:{output}");
            lbDisplay.Items.Add($"Description:{selectedHero.Armors[2].Description}");
            lbDisplay.Items.Add($"4. Armor:{selectedHero.Armors[3].ArmorName} DEF:{selectedHero.Armors[3].DEF} MDEF:{selectedHero.Armors[3].MDEF} Slot:{selectedHero.Armors[3].Type} Unique:{selectedHero.Armors[3].Unique}");
            for (int i = 0; i < selectedHero.Armors[3].SpecialEffects.Count; i++)
            {
                if (i < selectedHero.Armors[3].SpecialEffects.Count - 1)
                {
                    output += selectedHero.Armors[3].SpecialEffects[i].SpecialEffectName + ",";
                }
                else
                {
                    output += selectedHero.Armors[3].SpecialEffects[i].SpecialEffectName;
                }
            }
            lbDisplay.Items.Add($"Special effects:{output}");
            lbDisplay.Items.Add($"Description:{selectedHero.Armors[3].Description}");
        }

        public void BlacksmithChangeArmor()
        {
            tbInputArea.Text = "";
            lbOptions.Items.Clear();
            lbOptions.Items.Add($"1. {selectedHero.Armors[0].ArmorName}");
            lbOptions.Items.Add($"2. {selectedHero.Armors[1].ArmorName}");
            lbOptions.Items.Add($"3. {selectedHero.Armors[2].ArmorName}");
            lbOptions.Items.Add($"4. {selectedHero.Armors[3].ArmorName}");
            lbOptions.Items.Add("5. Cancel");
            lbDisplay.Items.Add("GAME: Choose which armor you want to change. While choosing armor keep in mind you will only see the correct slot for the armor to replace it with.");
            lbDisplay.ScrollIntoView(lbDisplay.Items[lbDisplay.Items.Count - 1]);
            BlacksmithChangeArmorDisplayArmorStats();
            btInput.Click += new RoutedEventHandler(BlacksmithChangeArmorChooseArmor);
        }

        public void BlacksmithChangeArmorChooseArmor(object sender, RoutedEventArgs e)
        {
            btInput.Click -= new RoutedEventHandler(BlacksmithChangeArmorChooseArmor);
            if (tbInputArea.Text == "?")
            {
                lbDisplay.Items.Add("EXPLANATION: By choosing an armor you can then choose a different armor to replace it.");
                lbDisplay.ScrollIntoView(lbDisplay.Items[lbDisplay.Items.Count - 1]);
                btInput.Click += new RoutedEventHandler(BlacksmithChangeArmorChooseArmor);
            }
            else if (tbInputArea.Text == "5" || tbInputArea.Text == "Cancel")
            {
                BlacksmithChangeEquipmentChooseEquipmentTypeReEntry();
            }
            else if (tbInputArea.Text == selectedHero.Armors[0].ArmorName || tbInputArea.Text == selectedHero.Armors[1].ArmorName || tbInputArea.Text == selectedHero.Armors[2].ArmorName || tbInputArea.Text == selectedHero.Armors[3].ArmorName)
            {
                foreach (Armor armor in selectedHero.Armors)
                {
                    if (armor.ArmorName == tbInputArea.Text)
                    {
                        selectedArmor = armor;
                    }
                }
                BlacksmithChangeArmorNewArmor();
            }
            else if (tbInputArea.Text.Contains("1") || tbInputArea.Text.Contains("2") || tbInputArea.Text.Contains("3") || tbInputArea.Text.Contains("4"))
            {
                try
                {
                    int index = Convert.ToInt32(tbInputArea.Text) - 1;
                    selectedArmor = selectedHero.Armors[index];
                    BlacksmithChangeArmorNewArmor();
                }
                catch
                {
                    MessageBox.Show("Please use the textbox at the bottom of the window to write a valid option from the left.");
                    btInput.Click += new RoutedEventHandler(BlacksmithChangeArmorChooseArmor);
                }
            }
            else
            {
                MessageBox.Show("Please use the textbox at the bottom of the window to write a valid option from the left.");
                btInput.Click += new RoutedEventHandler(BlacksmithChangeArmorChooseArmor);
            }
        }

        public void BlacksmithChangerArmorNewArmorArmors()
        {
            selectableNewArmors.Clear();
            int counter = 0;
            foreach (Armor armor in Initializer.armors)
            {
                if (armorsObtained[armor.ArmorName] == true && armor.Type == selectedArmor.Type)
                {
                    lbOptions.Items.Add($"{counter + 2}. {armor.ArmorName}");
                    selectableNewArmors.Add(armor);
                    counter++;
                }
            }
        }

        public void BlacksmithChangeArmorNewArmor()
        {
            tbInputArea.Text = "";
            lbOptions.Items.Clear();
            lbOptions.Items.Add("1. Cancel");
            BlacksmithChangerArmorNewArmorArmors();
            btInput.Click += new RoutedEventHandler(BlacksmithChangeArmorChooseNewArmor);
        }

        public void BlacksmithChangeArmorChooseNewArmor(object sender, RoutedEventArgs e)
        {
            btInput.Click -= new RoutedEventHandler(BlacksmithChangeArmorChooseNewArmor);
            List<string> selectableNewArmorsName = new List<string>(selectableNewArmors.Select(x => x.ArmorName));
            if (tbInputArea.Text == "?")
            {
                lbDisplay.Items.Add("EXPLANATION: From here you can choose a new armor then you get to see it's stats and make the decision to change to this new armor or cancel and go back to the previous armor choosing option.");
                btInput.Click += new RoutedEventHandler(BlacksmithChangeArmorChooseNewArmor);
            }
            else if (tbInputArea.Text == "1" || tbInputArea.Text == "Cancel")
            {
                selectableNewArmors.Clear();
                BlacksmithChangeArmorChooseArmorReEntry();
            }
            else if (selectableNewArmorsName.Contains(tbInputArea.Text) == true || tbInputArea.Text.Contains("0") || tbInputArea.Text.Contains("1") || tbInputArea.Text.Contains("2") || tbInputArea.Text.Contains("3") || tbInputArea.Text.Contains("4") || tbInputArea.Text.Contains("5") || tbInputArea.Text.Contains("6") || tbInputArea.Text.Contains("7") || tbInputArea.Text.Contains("8") || tbInputArea.Text.Contains("9"))
            {
                if (selectableNewArmorsName.Contains(tbInputArea.Text) == true)
                {
                    foreach (Armor armor in selectableNewArmors)
                    {
                        if (armor.ArmorName == tbInputArea.Text)
                        {
                            selectedNewArmor = armor;
                        }
                    }
                    BlacksmithChangeArmorNewArmorNewArmor();
                }
                else
                {
                    try
                    {
                        int index = Convert.ToInt32(tbInputArea.Text) - 2;
                        selectedNewArmor = selectableNewArmors[index];
                        BlacksmithChangeArmorNewArmorNewArmor();
                    }
                    catch
                    {
                        MessageBox.Show("Please use the textbox at the bottom of the window to write a valid option from the left.");
                        btInput.Click += new RoutedEventHandler(BlacksmithChangeArmorChooseNewArmor);
                    }
                }
            }
            else
            {
                MessageBox.Show("Please use the textbox at the bottom of the window to write a valid option from the left.");
                btInput.Click += new RoutedEventHandler(BlacksmithChangeArmorChooseNewArmor);
            }
        }

        public void BlacksmithChangeArmorChooseArmorReEntry()
        {
            tbInputArea.Text = "";
            lbOptions.Items.Clear();
            lbOptions.Items.Add($"1. {selectedHero.Armors[0].ArmorName}");
            lbOptions.Items.Add($"2. {selectedHero.Armors[1].ArmorName}");
            lbOptions.Items.Add($"3. {selectedHero.Armors[2].ArmorName}");
            lbOptions.Items.Add($"4. {selectedHero.Armors[3].ArmorName}");
            lbOptions.Items.Add("5. Cancel");
            btInput.Click += new RoutedEventHandler(BlacksmithChangeArmorChooseArmor);
        }

        public void BlacksmithChangeArmorNewArmorNewArmorStats()
        {
            string output = "";
            lbDisplay.Items.Add($"New Armor:{selectedNewArmor.ArmorName} DEF:{selectedNewArmor.DEF} MDEF:{selectedNewArmor.MDEF} Slot:{selectedNewArmor.Type} Unique:{selectedNewArmor.Unique}");
            for (int i = 0; i < selectedNewArmor.SpecialEffects.Count; i++)
            {
                if (i < selectedNewArmor.SpecialEffects.Count - 1)
                {
                    output += selectedNewArmor.SpecialEffects[i].SpecialEffectName + ",";
                }
                else
                {
                    output += selectedNewArmor.SpecialEffects[i];
                }
            }
            lbDisplay.Items.Add($"Special effects:{output}");
            lbDisplay.Items.Add($"Description:{selectedNewArmor.Description}");
        }

        public void BlacksmithChangeArmorNewArmorNewArmor()
        {
            tbInputArea.Text = "";
            lbOptions.Items.Clear();
            lbOptions.Items.Add("1. Confirm");
            lbOptions.Items.Add("2. Cancel");
            BlacksmithChangeArmorNewArmorNewArmorStats();
            btInput.Click += new RoutedEventHandler(BlacksmithChangeArmorNewArmorChangeConfirmOrCancel);
        }

        public void BlacksmithChangeArmorNewArmorChangeConfirmOrCancel(object sender, RoutedEventArgs e)
        {
            btInput.Click -= new RoutedEventHandler(BlacksmithChangeArmorNewArmorChangeConfirmOrCancel);
            switch (tbInputArea.Text)
            {
                case "?":
                    tbInputArea.Text = "";
                    lbDisplay.Items.Add($"EXPLANATION: Confirm will replace the currently selected armor ({selectedArmor.ArmorName}) from the currently selected party member ({selectedHero.HeroName}). Or you can cancel with the cancel option.");
                    lbDisplay.ScrollIntoView(lbDisplay.Items[lbDisplay.Items.Count - 1]);
                    btInput.Click += new RoutedEventHandler(BlacksmithChangeArmorNewArmorChangeConfirmOrCancel);
                    break;
                case "1":
                    BlacksmithChangeArmorNewArmorChangeConfirmed();
                    break;
                case "Confirm":
                    BlacksmithChangeArmorNewArmorChangeConfirmed();
                    break;
                case "2":
                    BlacksmithChangeArmorChooseNewArmorReEntry();
                    break;
                case "Cancel":
                    BlacksmithChangeArmorChooseNewArmorReEntry();
                    break;
                default:
                    MessageBox.Show("Please use the textbox at the bottom of the window to write a valid option from the left.");
                    btInput.Click += new RoutedEventHandler(BlacksmithChangeArmorNewArmorChangeConfirmOrCancel);
                    break;
            }
        }

        public void BlacksmithChangeArmorNewArmorChangeConfirmed()
        {
            bool equipable = true;
            if (selectedNewArmor.Unique == true)
            {
                foreach (Hero hero in heroes)
                {
                    foreach (Armor armor in hero.Armors)
                    {
                        if (armor.ArmorName == selectedNewArmor.ArmorName)
                        {
                            MessageBox.Show($"This is a unique armor and someone else already has it equipped ({hero.HeroName})");
                            BlacksmithChangeArmorChooseNewArmorReEntry();
                            equipable = false;
                        }
                    }
                }
                if (equipable == true)
                {
                    for (int i = 0; i < party.Count(); i++)
                    {
                        if (party[i] == selectedHero)
                        {
                            for (int j = 0; j < party[i].Armors.Count(); j++)
                            {
                                if (party[i].Armors[j] == selectedArmor)
                                {
                                    party[i].Armors[j] = Initializer.armors[0];
                                    party[i] = HeroStatCalculation.HeroStatReCalculation(party[i]);
                                    party[i].Armors[j] = selectedNewArmor;
                                    party[i] = Armor.EquipArmorCheck(party[i], j);
                                }
                            }
                        }
                    }
                    tbInputArea.Text = "";
                    lbOptions.Items.Clear();
                    lbOptions.Items.Add("1. Weapons");
                    lbOptions.Items.Add("2. Armors");
                    lbOptions.Items.Add("3. Cancel");
                    selectableNewWeapons.Clear();
                    btInput.Click += new RoutedEventHandler(BlacksmithChangeEquipmentChooseEquipmentType);
                }
            }
            else
            {
                for (int i = 0; i < party.Count(); i++)
                {
                    if (party[i] == selectedHero)
                    {
                        for (int j = 0; j < party[i].Armors.Count(); j++)
                        {
                            if (party[i].Armors[j].Type == selectedArmor.Type)
                            {
                                party[i].Armors[j] = Initializer.armors[0];
                                party[i] = HeroStatCalculation.HeroStatReCalculation(party[i]);
                                party[i].Armors[j] = selectedNewArmor;
                                party[i] = Armor.EquipArmorCheck(party[i], j);
                            }
                        }
                    }
                }
                tbInputArea.Text = "";
                lbOptions.Items.Clear();
                lbOptions.Items.Add("1. Weapons");
                lbOptions.Items.Add("2. Armors");
                lbOptions.Items.Add("3. Cancel");
                selectableNewWeapons.Clear();
                btInput.Click += new RoutedEventHandler(BlacksmithChangeEquipmentChooseEquipmentType);
            }
        }

        public void BlacksmithChangeArmorChooseNewArmorReEntry()
        {
            tbInputArea.Text = "";
            lbOptions.Items.Clear();
            lbOptions.Items.Add("1. Cancel");
            BlacksmithChangerArmorNewArmorArmors();
            btInput.Click += new RoutedEventHandler(BlacksmithChangeArmorChooseNewArmor);
        }

        //Change Equipment Armor ends here ----------------------------------------------------------------------------

        //Change Equipment ends here -----------------------------------------------------------------------------------

        public void BlacksmithChangeEquipmentChooseEquipmentTypeReEntry()
        {
            tbInputArea.Text = "";
            lbOptions.Items.Clear();
            lbOptions.Items.Add("1. Weapons");
            lbOptions.Items.Add("2. Armors");
            lbOptions.Items.Add("3. Cancel");
            btInput.Click += new RoutedEventHandler(BlacksmithChangeEquipmentChooseEquipmentType);
        }

        public void BlacksmithMainOptionReEntry()
        {
            tbInputArea.Text = "";
            lbOptions.Items.Clear();
            lbOptions.Items.Add("1. Buy Weapon");
            lbOptions.Items.Add("2. Buy Armor");
            lbOptions.Items.Add("3. Improve Weapon");
            lbOptions.Items.Add("4. Improve Armor");
            lbOptions.Items.Add("5. Change Equipment");
            lbOptions.Items.Add("6. Leave");
            lbDisplay.Items.Add("Blacksmith: Nothing interesting i assume, but not to worry there is always more!");
            lbDisplay.ScrollIntoView(lbDisplay.Items[lbDisplay.Items.Count - 1]);
            btInput.Click += new RoutedEventHandler(BlacksmithMainOption);
        }

        public void BlacksmithLeave()
        {
            lbDisplay.Items.Add("Blacksmith: I hope we will see each other again and stay safe out there now!");
            lbDisplay.ScrollIntoView(lbDisplay.Items[lbDisplay.Items.Count - 1]);
            lbOptions.Items.Clear();
            tbInputArea.Text = "";
            MainTownOptions();
            btInput.Click += new RoutedEventHandler(MainTownOption);
        }

        //Blacksmith ends here -----------------------------------------------------------------------------------------

        //Town ends here -----------------------------------------------------------------------------------------------
    }
}

