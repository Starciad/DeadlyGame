using DeadlyGame.Core;
using DeadlyGame.Core.Builders;

using System;
using System.Threading;

namespace DeadlyGame.CLI
{
    internal static partial class Program
    {
        private static void StartGame()
        {
            DGGameBuilder gameBuilder = new()
            {
                Players = [.. playerBuilders],
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

            DGGame game = new(gameBuilder, worldBuilder);

            Console.WriteLine("START");

            game.StartGame();

            while (game.ShouldUpdateGame())
            {
                game.UpdateGame();

                Console.Clear();
                Console.WriteLine($"[ Round: {game.RoundManager.CurrentRound} || Day: {game.WorldManager.CurrentDay} ({game.WorldManager.CurrentDaylightCycle}) ]");
                Console.WriteLine($"Players: {game.PlayerManager.LivingPlayers.Length}/{game.PlayerManager.TotalPlayerCount};");

                Thread.Sleep(TimeSpan.FromMilliseconds(25));
            }

            game.FinishGame();

            Console.WriteLine("Finished");
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
