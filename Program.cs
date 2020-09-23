using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace the_TGW_project
{
    class Program
    {
        //static readonly string rootFolder = @"C:\Users\kooky\Documents\DOTNET\TGW_task\tgw_project\the_TGW_project\the_TGW_project\";
        static readonly string baseConfiguration =  @"C:\Users\kooky\Documents\DOTNET\TGW_task\tgw_project\the_TGW_project\the_TGW_project\Base_Config.txt";
        static void Main(string[] args)
        {
            string text = File.ReadAllText(baseConfiguration);
            Console.WriteLine(text);
            Console.ReadLine();

            Dictionary<string, string> Configuration = new Dictionary<string, string>();

            string[] lines = File.ReadAllLines(baseConfiguration);

            LoadingBaseConfigurationSettings(Configuration, lines);

            foreach (KeyValuePair<string, string> item in Configuration)
            {
                Console.WriteLine($"Config ID = {item.Key}, Config Value = {item.Value}");
            }
            Console.ReadLine();
        }

        private static void LoadingBaseConfigurationSettings(Dictionary<string, string> Configuration, string[] lines)
        {
            foreach (string line in lines)
            {
                string valueToFind = ":\t";
                if (line.Contains(valueToFind) == true)
                {
                    string configId = "";
                    var configValue = "";
                    int x;

                    for (int i = 0; i < line.Length; i++)
                    {
                        char symbolToAdd = line[i];
                        if (symbolToAdd != ':')
                        {
                            configId = configId + symbolToAdd;
                        }
                        else
                        {
                            x = i + 2;
                            for (int j = x; j < line.Length; j++)
                            {
                                char valueToAdd = line[j];
                                if (valueToAdd != '\t')
                                {
                                    if (valueToAdd != ' ')
                                    {
                                        if (valueToAdd != '/')
                                        {
                                            configValue = configValue + valueToAdd;
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
                    Configuration.Add(configId, configValue);
                }
            }
        }
    }
}
