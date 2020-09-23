using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Collections;

namespace the_TGW_project
{
    class Program
    {
        //Setting up links fo configuration files
        static readonly string projectCofigurationFolder = @"C:\Users\kooky\Documents\DOTNET\TGW_task\tgw_project\the_TGW_project\the_TGW_project\configs\";
        static readonly string projectBaseConfiguration =  @"C:\Users\kooky\Documents\DOTNET\TGW_task\tgw_project\the_TGW_project\the_TGW_project\base_config\Base_Config.txt";
        static void Main(string[] args)
        {
            Dictionary<string, dynamic> Configuration = new Dictionary<string, dynamic>();
            Start:
                startMainMenu(Configuration);
            Console.WriteLine("Press enter to continue...");
            Console.ReadLine();
            Console.Clear();
            goto Start;

        }

        private static void startMainMenu(Dictionary<string, dynamic> Configuration)        //Starts main console menu
        {
            int menuChoice;
            Console.WriteLine("Please choose what you want to do:");
            Console.WriteLine("[1] Load base configuration");
            Console.WriteLine("[2] Show configuration settings");
            Console.WriteLine("[3] Load list of available configuration files");
            Console.WriteLine("[4] Load selected configuration file");
            Console.WriteLine("[5] Load all configuration files");
            Console.WriteLine("[6] Exit");
            menuChoice = Int32.Parse(Console.ReadLine());

            if (menuChoice > 0 && menuChoice <= 6)
            {
                switch (menuChoice)
                {
                    case 1:
                        Console.Clear();
                        LoadingBaseConfigurationSettings(Configuration);
                        break;

                    case 2:
                        Console.Clear();
                        ListingLoadedBaseConfiguration(Configuration);
                        break;

                    case 3:
                        Console.Clear();
                        GetListOfConfigurationFiles();
                        break;

                    case 4:
                        Console.Clear();
                        LoadSelectedConfigurationFile(Configuration);
                        break;

                    case 5:
                        Console.Clear();
                        LoadAllConfigurationFiles(Configuration);
                        break;

                    case 6:
                        Environment.Exit(1);
                        break;
                }

            }
            else
            {
                Console.WriteLine("Wrong input. The program will exit");
                Console.ReadLine();

            }
        }

        private static void GetListOfConfigurationFiles()       //Shows the list of all configuration files int the folder
        {
            string[] configurationFilesList = Directory.GetFiles(projectCofigurationFolder);
            Array.Sort(configurationFilesList, (x, y) => StringComparer.OrdinalIgnoreCase.Compare(File.GetLastWriteTime(x), File.GetLastWriteTime(y)));
            foreach (string item in configurationFilesList)
            {
                Console.WriteLine(item);
            }
        }

        private static void LoadSelectedConfigurationFile(Dictionary<string, dynamic> Configuration)        //Loads individual selected files
        {
            Console.WriteLine("Please enter file neme to load, for example 'file.txt':");
            string fileToLoad = Console.ReadLine();
            string fileToLoadLink = projectCofigurationFolder + fileToLoad;
            dynamic[] lines = File.ReadAllLines(fileToLoadLink);

            foreach (dynamic line in lines)
            {
                dynamic valueToFind = ":\t";
                if (line.Contains(valueToFind) == true)
                {
                    string configId = "";
                    dynamic configValue = "";
                    int x;

                    for (int i = 0; i < line.Length; i++)
                    {
                        dynamic symbolToAdd = line[i];
                        if (symbolToAdd != ':')
                        {
                            configId += symbolToAdd;
                        }
                        else
                        {
                            x = i + 2;
                            for (int j = x; j < line.Length; j++)
                            {
                                dynamic valueToAdd = line[j];
                                if (valueToAdd != '\t')
                                {
                                    if (valueToAdd != ' ')
                                    {
                                        if (valueToAdd != '/')
                                        {
                                            configValue += valueToAdd;
                                        }
                                        else
                                        {
                                            break;
                                        }
                                    }
                                }
                            }
                            break;
                        }
                    }
                    Console.WriteLine($"{configId} = {configValue}");
                    if (Configuration.ContainsKey(configId))
                    {
                        Configuration[configId] = configValue;
                    }
                    else
                        Configuration.Add(configId, configValue);
                }
            }
        }

        private static void LoadAllConfigurationFiles(Dictionary<string, dynamic> Configuration)        //Loads all cofiguration files in Configuration folder in order by modification time
        {
            string[] configurationFilesList = Directory.GetFiles(projectCofigurationFolder);
            Array.Sort(configurationFilesList, (x, y) => StringComparer.OrdinalIgnoreCase.Compare(File.GetLastWriteTime(x), File.GetLastWriteTime(y)));
            foreach (string item in configurationFilesList)
            {
                Console.WriteLine(item);
            }
            Console.Clear();

            foreach (string item in configurationFilesList)
            {
                dynamic[] lines = File.ReadAllLines(item);

                foreach (dynamic line in lines)
                {
                    dynamic valueToFind = ":\t";
                    if (line.Contains(valueToFind) == true)
                    {
                        string configId = "";
                        dynamic configValue = "";
                        int x;

                        for (int i = 0; i < line.Length; i++)
                        {
                            dynamic symbolToAdd = line[i];
                            if (symbolToAdd != ':')
                            {
                                configId += symbolToAdd;
                            }
                            else
                            {
                                x = i + 2;
                                for (int j = x; j < line.Length; j++)
                                {
                                    dynamic valueToAdd = line[j];
                                    if (valueToAdd != '\t')
                                    {
                                        if (valueToAdd != ' ')
                                        {
                                            if (valueToAdd != '/')
                                            {
                                                configValue += valueToAdd;
                                            }
                                            else
                                            {
                                                break;
                                            }
                                        }
                                    }
                                }
                                break;
                            }
                        }
                        if (Configuration.ContainsKey(configId))
                        {
                            Configuration[configId] = configValue;
                        }
                        else
                            Configuration.Add(configId, configValue);
                    }
                }
            }

            foreach(KeyValuePair<string, dynamic> item in Configuration)
            {
                Console.WriteLine($"Config ID = {item.Key}, Config Value = {item.Value}");
            }
            Console.ReadLine();

        }

        private static void ListingLoadedBaseConfiguration(Dictionary<string, dynamic> Configuration)       //Shows loaded base configuration settings
        {
            Console.WriteLine();
            foreach (KeyValuePair<string, dynamic> item in Configuration)
            {
                Console.WriteLine($"Config ID = {item.Key}, Config Value = {item.Value}");
            }
            Console.ReadLine();
        }

        private static void LoadingBaseConfigurationSettings(Dictionary<string, dynamic> Configuration)     //Loads base configuration folder settings
        {
            dynamic[] lines = File.ReadAllLines(projectBaseConfiguration);

            foreach (dynamic line in lines)
            {
                dynamic valueToFind = ":\t";
                if (line.Contains(valueToFind) == true)
                {
                    string configId = "";
                    dynamic configValue = "";
                    int x;

                    for (int i = 0; i < line.Length; i++)
                    {
                        dynamic symbolToAdd = line[i];
                        if (symbolToAdd != ':')
                        {
                            configId += symbolToAdd;
                        }
                        else
                        {
                            x = i + 2;
                            for (int j = x; j < line.Length; j++)
                            {
                                dynamic valueToAdd = line[j];
                                if (valueToAdd != '\t')
                                {
                                    if (valueToAdd != ' ')
                                    {
                                        if (valueToAdd != '/')
                                        {
                                            configValue += valueToAdd;
                                        }
                                        else
                                        {
                                            break;
                                        }
                                    }
                                }
                            }
                            break;
                        }
                    }
                    Console.WriteLine($"{configId} = {configValue}");
                    if (Configuration.ContainsKey(configId))
                    {
                        Configuration[configId] = configValue;
                    }
                    else
                    Configuration.Add(configId, configValue);
                }
            }
        }
    }
}
