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
        bool tutorialCompleted = false;
        bool newHero = false;
        int HeroId = 1;
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
            lbDisplay.Items.Add("Tip: To check if you have all the game assets downloaded just delete the GameAssets folder and download everything again.");
            lbDisplay.Items.Add("Tip: To play with cloud saving you need to login to an account through the Select Profile option.");
            lbDisplay.Items.Add("Tip: To progress write text based on the options on the far left into the area at the bottom of the window or select an option on the far left then press the input button. (This can be the number or the option as well example:'1'. 'Offline play')");
            lbDisplay.Items.Add("Tip: To learn more about most options you can type '?' to get a short explanation.");

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
            lbDisplay.Items.Add("Select Profile allows you to select an already added profile to use the cloud save feature so that you can access your saves from different computers.");
            lbDisplay.Items.Add("Add Profile allows you to add a existing account from our database so that you can create saves that have access to cloud saving and get access to existing cloud saves.");
            lbDisplay.Items.Add("Options allows you to change some things about the game like for example change the color from black to white.");
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
            lbDisplay.Items.Add("Shortened Names will make the friendly npcs and the hero use their first names when written onto a line. The true and false next to it shows the current state of the option. (Cuts off the name after the first 'Space' it finds)");
            lbDisplay.Items.Add("Back will take you back to the first option.");
            lbDisplay.Items.Add("There will also be more fun little things that can be change from here. (But these won't be priority)");
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
                // This needs to be finished later ---------------------------------------------------------------------
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

                        // This needs to be finished later -------------------------------------------------------------
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

                            // This needs to be finished later ---------------------------------------------------------
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
            lbDisplay.Items.Add("Back will take you back to the first option.");
            lbDisplay.Items.Add("All other options are saves from your local Offline folder.");
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

                ReturnToOfflineSelectProfileAddProfileOption();
            }
            else
            {
                lbDisplay.Items.Add("Please select a profile from the left.");

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
            lbDisplay.Items.Add("Back will take you back to the first option.");
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
                    lbOptions.Items.Add("1. New Game");
                    lbOptions.Items.Add("2. Log out");
                    btInput.Click += new RoutedEventHandler(SelectProfileCreateSave);
                }
                else if (foundProfile == true)
                {
                    lbDisplay.Items.Add("Please select a save from the left or start a new game.");
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

                // This needs to be finished later ---------------------------------------------------------------------
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

                        // This needs to be finished later -------------------------------------------------------------
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

                            // This needs to be finished later ---------------------------------------------------------
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
            lbDisplay.Items.Add("Log out will take you back to the first option and remove the selected profile selection meaning you will have to login to it again.");
            lbDisplay.Items.Add("All other options are saves from your selected profile.");
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
                    // This needs to be finished later -----------------------------------------------------------------
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
                    // This needs to be finished later -----------------------------------------------------------------
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
            lbDisplay.Items.Add("Log out will take you back to the first option and remove the selected profile selection meaning you will have to login to it again.");
            tbInputArea.Text = "";
        }

        //Profile selection ends here ----------------------------------------------------------------------------------

        //Profile adding starts here -----------------------------------------------------------------------------------

        public void AddProfile()
        {
            tbInputArea.Text = "";
            lbDisplay.Items.Add("To add a profile you will have to first write down your email then write down your password into the textbox and click the input button after both the email and the password. (You can also write 'Back' to go back.).");
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
            lbDisplay.Items.Add("No will cancel the Add Profile process and give you the previous 3 options.");
            lbDisplay.Items.Add("Change informatoin will send you back to the email adding step.");
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
            lbDisplay.Items.Add("Before starting the game you need to make a character of your own.");
            lbDisplay.Items.Add("The first step is to choose a class for yourself and keep in mind that you can't change this choice for a long time.");
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
            lbDisplay.Items.Add("Fighter is a basic melee class that focuses on a balanced build with a focus on physical damage output.");
            lbDisplay.Items.Add("Hunter is a basic ranged class that focuses on a more frail build with a focus on physical damage output with devastating crits.");
            lbDisplay.Items.Add("Wizard is a basic magic class that focuses on magic versatility with a focus on versatile use of magic for both offense and support.");
            lbDisplay.Items.Add("Paladin is a slightly more advanced class that focuses on tanking and bursts of damage they rely on heavy defense and mostly holy magic.");
            lbDisplay.Items.Add("Bounty Hunter is a more advanced class that focuses on marking and targeting enemies they work alone but are stronger with a team.");
            lbDisplay.Items.Add("Warlock is a slightly more advanced class that focuses on destructive magic they use powerful magic at the cost of survivability.");
            btInput.Click += new RoutedEventHandler(NewGameCharacterClassSelection);
        }

        public void NewGameCharacterBackground()
        {
            tbInputArea.Text = "";
            lbOptions.Items.Clear();
            lbDisplay.Items.Add("Next you will choose a background for your character. This is a passive that your character will recieve.");
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
            lbDisplay.Items.Add("Adventurer will give you a small stat buff, rest bonus and a experience gain bonus.");
            lbDisplay.Items.Add("Noble will give you starting gold, higher starting MP and discounts in shops.");
            lbDisplay.Items.Add("Merchant will give you a passive gold gain based on your level and merchants will offer more items for sale.");
            lbDisplay.Items.Add("Blacksmith gives you a blunt damage bonus and gives you the ability to upgrade armors and weapons at the blacksmith with a heavy discount but a with a small chance to fail.");
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
            lbDisplay.Items.Add("Now you will choose a race for your character. This affects mostly your damage resistances.");
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
                }
            }
            lbDisplay.Items.Add("Humans receive increased exp gain, Elves receive extra mana, mana regen and darksight that makes them better for traps.");
            lbDisplay.Items.Add("Dwarves receive extra health and defense, Halflinges receive less health but are immune to some debuffs and benefit more from crits.");
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
            lbDisplay.Items.Add("The name can only be 100 characters long at most and you can't have repeat names in our cloud saves because it is going to overwrite the old one when saving, but this is not a problem with offline play.");
            lbDisplay.Items.Add("In a future update we will make a workaround for this.");
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
                    lbDisplay.Items.Add("There is nithing to explain here.");
                    btInput.Click += new RoutedEventHandler(NewGameCharacterConfirming);
                    break;
                case "1":
                    break;
                case "Yes":
                    break;
                case "2":
                    break;
                case "No":
                    break;
                case "3":
                    break;
                case "Change Name":
                    break;
                default:
                    MessageBox.Show("Please use the textbox at the bottom of the window to write a valid option from the left.");
                    btInput.Click += new RoutedEventHandler(NewGameCharacterConfirming);
                    break;
            }
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
                    //HeroId = Convert.ToInt32(linecutter[0]);
                    
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
                    }
                    string[] armorsImprovementcutter = linecutter[8].Split('%');
                    for (int i = 0; i < Initializer.armors.Count; i++)
                    {
                        armorsImproved.Add(Initializer.armors[i].ArmorName, Convert.ToInt32(armorsImprovementcutter[i]));
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
                    tutorialCompleted = Convert.ToBoolean(linecutter[12]);

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
            tbInputArea.Text = "";
            lbOptions.Items.Clear();
            lbDisplay.Items.Add("GAME: You have entered the town and now you can save and modify your party and your items.");
            MainTownLBOptions();
            btInput.Click += new RoutedEventHandler(MainTownOption);
        }

        public void MainTownLBOptions()
        {
            lbOptions.Items.Add("1. Quit Game");
            lbOptions.Items.Add("2. Save Game");
            lbOptions.Items.Add("3. ");
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
                case "2":
                    SaveGame();
                    break;
                case "Quit Game":
                    QuitGame();
                    break;
                case "Save Game":
                    SaveGame();
                    break;
                default:
                    MessageBox.Show("Please use the textbox at the bottom of the window to write a valid option from the left.");
                    btInput.Click += new RoutedEventHandler(MainTownOption);
                    break;
            }
        }

        public void ExplainMainTownOption()
        {
            lbDisplay.Items.Add("EXPLANATION: Guit Game will close the game without saving if you are playing through the New Game option, but will save automatically if you have loaded an existing save to play.");
            lbDisplay.Items.Add("EXPLANATION: Save Game will allow you to save the game without closing the game and it will allow you to create a new save as well so that you can avoid overwriting an existing save.");
            lbDisplay.Items.Add("EXPLANATION: ");
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
                    streamWriter.Write(Saving.MakeSaveString(mySqlConnection, folders, files, heroes, party, Initializer.npcs, questsCompleted, consumables, Gold, Experience, dungeonsCompleted, weaponsImproved, armorsImproved, weaponsObtained, armorsObtained, consumablesUnlocked, tutorialCompleted));
                    streamWriter.Close();

                    Saving.SavingStart(folders, files, heroes, party, questsCompleted, consumables, Gold, Experience, dungeonsCompleted, weaponsImproved, armorsImproved, weaponsObtained, armorsObtained, consumablesUnlocked, mySqlConnection, Initializer.npcs, tutorialCompleted, newHero);
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
                lbOptions.Items.Add("1. Overwrite Save");
                lbOptions.Items.Add("2. New Save");
                lbOptions.Items.Add("3. Cancel");
                btInput.Click += new RoutedEventHandler(SaveGameNewOrNot);
            }
            else
            {
                lbDisplay.Items.Add("GAME: To save the game you first need to give a name to the save.");
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
            lbDisplay.Items.Add("EXPLANATION: New Save will create a new text file in the current profile and create a save in that new file for later use.");
            lbDisplay.Items.Add("EXPLANATION: Cancel will stop the Save Game process and take you back to the town.");
            btInput.Click += new RoutedEventHandler(SaveGameNewOrNot);
        }

        public void SaveGameExit()
        {
            tbInputArea.Text = "";
            lbOptions.Items.Clear();
            MainTownLBOptions();
            btInput.Click += new RoutedEventHandler(MainTownOption);
        }

        public void SaveGameSaving()
        {
            try
            {
                StreamWriter streamWriter = new StreamWriter($@"{folders[9]}\{folders.Last()}\{files.Last()}");
                streamWriter.Write(Saving.MakeSaveString(mySqlConnection, folders, files, heroes, party, Initializer.npcs, questsCompleted, consumables, Gold, Experience, dungeonsCompleted, weaponsImproved, armorsImproved, weaponsObtained, armorsObtained, consumablesUnlocked, tutorialCompleted));
                streamWriter.Close();
                MessageBox.Show("Game saved successfully");

                Saving.SavingStart(folders, files, heroes, party, questsCompleted, consumables, Gold, Experience, dungeonsCompleted, weaponsImproved, armorsImproved, weaponsObtained, armorsObtained, consumablesUnlocked, mySqlConnection, Initializer.npcs, tutorialCompleted, newHero);
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
                    streamWriter.Write(Saving.MakeSaveString(mySqlConnection, folders, files, heroes, party, Initializer.npcs, questsCompleted, consumables, Gold, Experience, dungeonsCompleted, weaponsImproved, armorsImproved, weaponsObtained, armorsObtained, consumablesUnlocked, tutorialCompleted));
                    streamWriter.Close();
                    MessageBox.Show("Game saved successfully");

                    Saving.SavingStart(folders, files, heroes, party, questsCompleted, consumables, Gold, Experience, dungeonsCompleted, weaponsImproved, armorsImproved, weaponsObtained, armorsObtained, consumablesUnlocked, mySqlConnection, Initializer.npcs, tutorialCompleted, newHero);
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
            btInput.Click += new RoutedEventHandler(NewSaveNaming);
        }

        //Saving ends here ---------------------------------------------------------------------------------------------

        //Town ends here -----------------------------------------------------------------------------------------------
    }
}

