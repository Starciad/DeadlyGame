using DeadlyGame.Core;
using DeadlyGame.Core.Builders;
using DeadlyGame.Core.GameContent.Components;
using DeadlyGame.Core.GameContent.Entities.Players;
using DeadlyGame.Core.Models.Infos.Actions;

using System;
using System.Threading;

namespace DeadlyGame.CLI
{
    internal static partial class Program
    {
        private static void StartGame()
        {
            DGGeneralBuilder generalBuilder = new()
            {
                LocalizationCode = (generalLocalizationCode.language, generalLocalizationCode.region),
                Seed = generalSeed
            };

            DGGameBuilder gameBuilder = new()
            {
                Players = [.. gamePlayerBuilders],
            };

            DGWorldBuilder worldBuilder = new()
            {
                Size = worldSize,
                Resources = new()
                {
                    TreeRate = worldTreeRate,
                    StoneRate = worldStoneRate,
                    ShrubRate = worldShrubRate,
                },
            };

            InitializeGameRoutine(generalBuilder, gameBuilder, worldBuilder);
        }

        private static void InitializeGameRoutine(DGGeneralBuilder generalBuilder, DGGameBuilder gameBuilder, DGWorldBuilder worldBuilder)
        {
            DGGame game = new(generalBuilder, gameBuilder, worldBuilder);

            // ================================ //

            Console.WriteLine("START");

            game.StartGame();

            while (game.ShouldUpdateGame())
            {
                game.UpdateGame();

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"[ Round: {game.RoundManager.CurrentRound} || Day: {game.WorldManager.CurrentDay} ({game.WorldManager.CurrentDaylightCycle}) ]");
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine($"- Players: {game.PlayerManager.LivingPlayers.Length}/{game.PlayerManager.TotalPlayerCount};");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine();

                foreach (DGPlayer livingPlayer in game.PlayerManager.LivingPlayers)
                {
                    DGPlayerActionInfo playerActionInfo = livingPlayer.ComponentContainer.GetComponent<DGBehaviourComponent>().LastActionInfos;

                    if (playerActionInfo.IsEmpty)
                    {
                        continue;
                    }

                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.WriteLine(playerActionInfo.Name);
                    Console.WriteLine(playerActionInfo.Title);
                    Console.WriteLine(playerActionInfo.Description);
                    Console.WriteLine();
                    Console.ForegroundColor = ConsoleColor.White;

                    Console.ReadKey();
                }

                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine(new string('=', 32));
                Console.ForegroundColor = ConsoleColor.White;
                Console.ReadKey();
            }

            game.FinishGame();

            Console.WriteLine("FINISHED");

            // ================================ //
        }

        private static void DisplayTitleInfo()
        {
            string line = new('-', Console.WindowWidth - 1);

            Console.WriteLine(line);
            Console.WriteLine();
            Console.WriteLine("[ DEADLY GAME ]");
            Console.WriteLine();
            Console.WriteLine("v1.0.0.0");
            Console.WriteLine("Deadly Game (c) Starciad <davilsfernandes.starciad.comu@gmail.com>");
            Console.WriteLine();
            Console.WriteLine("SPT is a utility that allows you to pixelate images, allowing you to apply dozens of different filters and settings.");
            Console.WriteLine();
            Console.WriteLine(line);
            Console.WriteLine();
            Console.WriteLine("Use the '--help' or '-h' command for more details on what can be done!");
        }
    }
}
