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


        List<Hero> heroes = new List<Hero>();
        List<Hero> party = new List<Hero>();
        Dictionary<string, bool> questsCompleted = new Dictionary<string,bool>();
        Dictionary<string, int> consumables = new Dictionary<string, int>();
        int Gold = 0;
        int Experience = 0;
        Dictionary<string, bool> dungeonsCompleted = new Dictionary<string, bool>();
        Dictionary<string, int> weaponsImproved = new Dictionary<string, int>();
        Dictionary<string, int> armorsImproved = new Dictionary<string, int>();
        Dictionary<string, bool> weaponsObtained = new Dictionary<string, bool>();
        Dictionary<string, bool> armorsObtained = new Dictionary<string, bool>();
        Dictionary<string, bool> consumablesUnlocked = new Dictionary<string, bool>();


        List<string> physicalDamageTypes = new List<string> { "Blunt","Pierce","Slash"};
        List<string> magicalDamageTypes = new List<string> { "Fire"};
        bool skipDamageCalculation = false;
        Random random = new Random();
        Target targetPrep = new Target();
        DamageSource damageSourcePrep = new DamageSource();

        public MainWindow()
        {
            InitializeComponent();

            if (!Directory.Exists($@"{folders[0]}") || !Directory.Exists($@"{folders[0]}\{folders[1]}") || !Directory.Exists($@"{folders[0]}\{folders[2]}") || !Directory.Exists($@"{folders[0]}\{folders[3]}") || !Directory.Exists($@"{folders[0]}\{folders[4]}") || !Directory.Exists($@"{folders[0]}\{folders[5]}") || !Directory.Exists($@"{folders[0]}\{folders[6]}") || !Directory.Exists($@"{folders[0]}\{folders[7]}") || !Directory.Exists($@"{folders[0]}\{folders[8]}") || !Directory.Exists($@"{folders[9]}") || !Directory.Exists($@"{folders[9]}\{folders[10]}") || !File.Exists($@"{folders[0]}\{folders[1]}\{files[0]}") || !File.Exists($@"{folders[0]}\{folders[1]}\{files[1]}") || !File.Exists($@"{folders[0]}\{folders[4]}\{files[2]}") || !File.Exists($@"{folders[0]}\{folders[2]}\{files[3]}") || !File.Exists($@"{folders[0]}\{folders[7]}\{files[4]}") || !File.Exists($@"{folders[0]}\{folders[3]}\{files[5]}") || !File.Exists($@"{folders[0]}\{folders[3]}\{files[6]}") || !File.Exists($@"{folders[0]}\{folders[3]}\{files[7]}") || !File.Exists($@"{folders[0]}\{folders[6]}\{files[8]}") || !File.Exists($@"{folders[0]}\{folders[6]}\{files[9]}") || !File.Exists($@"{folders[0]}\{folders[8]}\{files[10]}") || !File.Exists($@"{folders[0]}\{folders[5]}\{files[11]}") || !File.Exists($@"{folders[0]}\{folders[5]}\{files[12]}") || !File.Exists($@"{folders[0]}\{folders[5]}\{files[13]}"))
            {
                Downloader.Download(folders, files, mySqlConnection);
            }
            Initializer.Initialize(folders, files);
            Initializer.GetProfilesFromDevice(folders, lbOptions, tempProfiles);

            heroes.Add(Initializer.npcs[0]);
            party.Add(Initializer.npcs[0]);
            questsCompleted.Add("test", false);
            consumables.Add("Test Item", 1);
            Gold = 123;
            Experience = 100;
            dungeonsCompleted.Add("Test Place", false);
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

            files.Add("Local Save.txt");
            EnterTown();

            /*lbDisplay.Items.Add("Welcome to Dungeon Valley Explorer!");
            lbDisplay.Items.Add("Tip: To check if you have all the game assets downloaded just delete the GameAssets folder and download everything again.");
            lbDisplay.Items.Add("Tip: To play with cloud saving you need to login to an account through the Select Profile option.");
            lbDisplay.Items.Add("Tip: To progress write text based on the options on the far left into the area at the bottom of the window or select an option on the far left then press the input button. (This can be the number or the option as well example:'1'. 'Offline play')");
            lbDisplay.Items.Add("Tip: To learn more about most options you can type '?' to get a short explanation.");

            lbOptions.Items.Add("1. Offline play");
            lbOptions.Items.Add("2. Select Profile");
            lbOptions.Items.Add("3. Add Profile");
            lbOptions.Items.Add("4. Options");
            
            btInput.Click += new RoutedEventHandler(OfflineSelectProfileAddProfileOption);*/
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
            lbOptions.Items.Add("2. Back");
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
                    ReturnToOfflineSelectProfileAddProfileOption();
                    break;
                case "Change Colors":
                    OptionsChangeColors();
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
            lbDisplay.Items.Add("Back will take you back to the first option.");
            lbDisplay.Items.Add("There will also be more fun little things that can be change from here. (But these won't be priority)");
            btInput.Click += new RoutedEventHandler(MainMenuOptionsOptions);
            tbInputArea.Text = "";
        }

        public void OptionsChangeColors()
        {
            lbOptions.Items.Clear();
            lbOptions.Items.Add("1. Change Colors");
            lbOptions.Items.Add("2. Back");
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
                lbOptions.Items.Clear();

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

                    }
                    else
                    {
                        try
                        {
                            int savesIndex = Convert.ToInt32(tbInputArea.Text) - 2;
                            files.Add(tempSaves[savesIndex]);
                            lbOptions.Items.Clear();

                            // This needs to be finished later ---------------------------------------------------------
                            LoadExistingSave();
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
            string command = $"Select UserName from user where Email = '{addEmail}' and Password = '{addPassword}' limit 1";
            MySqlCommand mySqlCommand = new MySqlCommand(command, mySqlConnection);
            try
            {
                mySqlConnection.Open();
                MySqlDataReader mySqlDataReader = mySqlCommand.ExecuteReader();
                bool foundProfile = false;
                while (mySqlDataReader.Read())
                {
                    if (mySqlDataReader.GetString(0) == folders.Last())
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
                lbOptions.Items.Clear();

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

                    }
                    else
                    {
                        try
                        {
                            int profileIndex = Convert.ToInt32(tbInputArea.Text) - 2;
                            folders.Add(tempProfiles[profileIndex]);
                            lbOptions.Items.Clear();

                            // This needs to be finished later ---------------------------------------------------------
                            LoadExistingSave();
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

        //Main menu ends here ------------------------------------------------------------------------------------------

        public void CreateNewGame()
        { 
        
        }

        public void LoadExistingSave ()
        {

        }

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
                    streamWriter.Write(Saving.MakeString(folders, files, heroes, party, questsCompleted, consumables, Gold, Experience, dungeonsCompleted, weaponsImproved, armorsImproved, weaponsObtained, armorsObtained, consumablesUnlocked));
                    streamWriter.Close();

                    if (folders.Last() != "Offline")
                    {
                        Saving.InsertSave(folders, files, heroes, party, questsCompleted, consumables, Gold, Experience, dungeonsCompleted, weaponsImproved, armorsImproved, weaponsObtained, armorsObtained, consumablesUnlocked, mySqlConnection, Initializer.npcs);
                    }
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
                streamWriter.Write(Saving.MakeString(folders, files, heroes, party, questsCompleted, consumables, Gold, Experience, dungeonsCompleted, weaponsImproved, armorsImproved, weaponsObtained, armorsObtained, consumablesUnlocked));
                streamWriter.Close();
                MessageBox.Show("Game saved successfully");

                if (folders.Last() != "Offline")
                {
                    Saving.InsertSave(folders, files, heroes, party, questsCompleted, consumables, Gold, Experience, dungeonsCompleted, weaponsImproved, armorsImproved, weaponsObtained, armorsObtained, consumablesUnlocked, mySqlConnection, Initializer.npcs);
                }
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
                MessageBox.Show("You can't name the save Weapon");
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
                StreamWriter streamWriter = new StreamWriter($@"{folders[9]}\{folders.Last()}\{files.Last()}");
                streamWriter.Write(Saving.MakeString(folders, files, heroes, party, questsCompleted, consumables, Gold, Experience, dungeonsCompleted, weaponsImproved, armorsImproved, weaponsObtained, armorsObtained, consumablesUnlocked));
                streamWriter.Close();
                MessageBox.Show("Game saved successfully");

                if (folders.Last() != "Offline")
                {
                    Saving.InsertSave(folders, files, heroes, party, questsCompleted, consumables, Gold, Experience, dungeonsCompleted, weaponsImproved, armorsImproved, weaponsObtained, armorsObtained, consumablesUnlocked, mySqlConnection, Initializer.npcs);
                }
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message);
            }
            SaveGameExit();
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

