using DeadlyGame.CLI.Interactivity;
using DeadlyGame.Core.Builders;
using DeadlyGame.Core.Enums.Characters;
using DeadlyGame.Core.Mathematics.Primitives;
using DeadlyGame.Core.Serializers.Ini;

using System;
using System.Collections.Generic;
using System.IO;

namespace DeadlyGame.CLI
{
    internal static partial class Program
    {
        private static void RegisterCommands()
        {
            commandRegistry.RegisterCommand(new DGCommand(
                "help",
                "Displays available commands and their descriptions.",
                parser =>
                {
                    DisplayTitleInfo();
                    commandRegistry.DisplayHelp();
                    Environment.Exit(0);
                },
                "h", "?"
            ));

            // ============================================================== //

            commandRegistry.RegisterCommand(new DGCommand(
                "configuration",
                "define the path to the configuration file that will be used.",
                parser =>
                {
                    string filename = parser.GetOption("configuration");

                    if (string.IsNullOrEmpty(filename))
                    {
                        Console.WriteLine("No input file was specified.");
                        Environment.Exit(1);
                    }

                    if (!File.Exists(filename))
                    {
                        Console.WriteLine("The specified input file does not exist.");
                        Environment.Exit(1);
                    }

                    DGIni configurationIni = DGIniSerializer.Deserialize(File.ReadAllText(filename));

                    ConfigureGeneral(configurationIni);
                    ConfigureGame(configurationIni);
                    ConfigureWorld(configurationIni);
                }
            ));
        }

        private static void ExecuteCommands(DGArgumentParser parser)
        {
            foreach (KeyValuePair<string, string> option in parser.GetAllOptions())
            {
                DGCommand command = commandRegistry.GetCommand(option.Key);
                command?.Execute(parser);
            }
        }

        // ========================================================= //

        private static void ConfigureGeneral(DGIni configurationIni)
        {
            EPIniSection section = configurationIni.GetSection("general");

            string[] localizationCodeTokens = section.GetKey("localizationCode").Split('-', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

            generalLocalizationCode = (localizationCodeTokens[0], localizationCodeTokens[1]);
            generalSeed = int.Parse(section.GetKey("seed"));
        }
        private static void ConfigureGame(DGIni configurationIni)
        {
            // Players
            foreach ((string key, string value) in configurationIni.GetSection("players").GetItems())
            {
                string[] args = value.Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

                gamePlayerBuilders.Add(new()
                {
                    Name = args[0],
                    BiologicalSex = GetBiologicalSex(args[1]),
                });
            }

            // Utilities
            DGBiologicalSexType GetBiologicalSex(string value)
            {
                switch (value.ToLower())
                {
                    case "random":
                        if (randomMath.Chance(50, 100))
                        {
                            return DGBiologicalSexType.Male;
                        }
                        else
                        {
                            return DGBiologicalSexType.Female;
                        }

                    case "m" or "male":
                        return DGBiologicalSexType.Male;

                    case "f" or "female":
                        return DGBiologicalSexType.Female;

                    default:
                        return DGBiologicalSexType.None;
                }
            }
        }
        private static void ConfigureWorld(DGIni configurationIni)
        {
            EPIniSection section = configurationIni.GetSection("world");

            string[] sizeTokens = section.GetKey("size").Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

            worldSize = new(int.Parse(sizeTokens[0]), int.Parse(sizeTokens[1]));
            worldTreeRate = int.Parse(section.GetKey("tree_rate"));
            worldStoneRate = int.Parse(section.GetKey("stone_rate"));
            worldShrubRate = int.Parse(section.GetKey("shrub_rate"));
        }
    }
}
