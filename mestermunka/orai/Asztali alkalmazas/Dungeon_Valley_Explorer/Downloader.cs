using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using MySql.Data.MySqlClient;

namespace Dungeon_Valley_Explorer
{
    static class Downloader
    {
        public static void Download(List<string>folders, List<string>files, MySqlConnection mySqlConnection)
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

            if (!Directory.Exists($@"{folders[9]}"))
            {
                Directory.CreateDirectory($@"{folders[9]}");
            }

            if (!Directory.Exists($@"{folders[9]}\{folders[10]}"))
            {
                Directory.CreateDirectory($@"{folders[9]}\{folders[10]}");
            }

            if (!File.Exists($@"{folders[0]}\{folders[1]}\{files[0]}"))
            {
                DownloadMonsters(folders, files, mySqlConnection);
            }

            if (!File.Exists($@"{folders[0]}\{folders[1]}\{files[1]}"))
            {
                DownloadAis(folders, files, mySqlConnection);
            }

            if (!File.Exists($@"{folders[0]}\{folders[4]}\{files[2]}"))
            {
                DownloadNPCs(folders, files, mySqlConnection);
            }

            if (!File.Exists($@"{folders[0]}\{folders[2]}\{files[3]}"))
            {
                DownloadDungeons(folders, files, mySqlConnection);
            }

            if (!File.Exists($@"{folders[0]}\{folders[7]}\{files[5]}"))
            {
                DownloadPassives(folders, files, mySqlConnection);
            }

            if (!File.Exists($@"{folders[0]}\{folders[7]}\{files[6]}"))
            {
                DownloadBuffsDebuffs(folders, files, mySqlConnection);
            }

            if (!File.Exists($@"{folders[0]}\{folders[7]}\{files[7]}"))
            {
                DownloadSpecialEffects(folders, files, mySqlConnection);
            }

            if (!File.Exists($@"{folders[0]}\{folders[8]}\{files[10]}"))
            {
                DownloadRaces(folders, files, mySqlConnection);
            }

            if (!File.Exists($@"{folders[0]}\{folders[7]}\{files[4]}"))
            {
                DownloadEnvironmentHazards(folders, files, mySqlConnection);
            }

            if (!File.Exists($@"{folders[0]}\{folders[6]}\{files[8]}"))
            {
                DownloadSkills(folders, files, mySqlConnection);
            }

            if (!File.Exists($@"{folders[0]}\{folders[6]}\{files[9]}"))
            {
                DownloadMagics(folders, files, mySqlConnection);
            }

            if (!File.Exists($@"{folders[0]}\{folders[5]}\{files[11]}"))
            {
                DownloadConsumables(folders, files, mySqlConnection);
            }

            if (!File.Exists($@"{folders[0]}\{folders[5]}\{files[13]}"))
            {
                DownloadWeapons(folders, files, mySqlConnection);
            }

            if (!File.Exists($@"{folders[0]}\{folders[5]}\{files[12]}"))
            {
                DownloadArmors(folders, files, mySqlConnection);
            }
        }

        public static void DownloadMonsters(List<string> folders, List<string> files, MySqlConnection mySqlConnection)
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
        public static void DownloadAis(List<string> folders, List<string> files, MySqlConnection mySqlConnection)
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

        public static void DownloadDungeons(List<string> folders, List<string> files, MySqlConnection mySqlConnection)
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

        public static void DownloadEnvironmentHazards(List<string> folders, List<string> files, MySqlConnection mySqlConnection)
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

        public static void DownloadBuffsDebuffs(List<string> folders, List<string> files, MySqlConnection mySqlConnection)
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

        public static void DownloadPassives(List<string> folders, List<string> files, MySqlConnection mySqlConnection)
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

        public static void DownloadSpecialEffects(List<string> folders, List<string> files, MySqlConnection mySqlConnection)
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

        public static void DownloadNPCs(List<string> folders, List<string> files, MySqlConnection mySqlConnection)
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

        public static void DownloadConsumables(List<string> folders, List<string> files, MySqlConnection mySqlConnection)
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

        public static void DownloadArmors(List<string> folders, List<string> files, MySqlConnection mySqlConnection)
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
                    armorsDownloader.Add($"{mySqlDataReader.GetInt32(0)}@{mySqlDataReader.GetString(1)}@{mySqlDataReader.GetString(2)}@{mySqlDataReader.GetInt32(3)}@{mySqlDataReader.GetInt32(4)}@{mySqlDataReader.GetString(5)}@{mySqlDataReader.GetInt32(6)}@{mySqlDataReader.GetInt32(7)}@{mySqlDataReader.GetBoolean(8)}");
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

        public static void DownloadWeapons(List<string> folders, List<string> files, MySqlConnection mySqlConnection)
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
                    weaponsDownloader.Add($"{mySqlDataReader.GetInt32(0)}@{mySqlDataReader.GetString(1)}@{mySqlDataReader.GetString(2)}@{mySqlDataReader.GetInt32(3)}@{mySqlDataReader.GetString(4)}@{mySqlDataReader.GetInt32(5)}@{mySqlDataReader.GetDouble(6)}@{mySqlDataReader.GetString(7)}@{mySqlDataReader.GetString(8)}@{mySqlDataReader.GetString(9)}@{mySqlDataReader.GetInt32(10)}@{mySqlDataReader.GetBoolean(11)}");
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

        public static void DownloadSkills(List<string> folders, List<string> files, MySqlConnection mySqlConnection)
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

        public static void DownloadMagics(List<string> folders, List<string> files, MySqlConnection mySqlConnection)
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

        public static void DownloadRaces(List<string> folders, List<string> files, MySqlConnection mySqlConnection)
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
    }
}
