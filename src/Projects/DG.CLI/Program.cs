using DeadlyGame.Core;
using DeadlyGame.Core.Builders;

using System;

namespace DeadlyGame.CLI
{
    internal static class Program
    {
        private static void Main()
        {
            DGGameBuilder gameBuilder = BuildGame();
            DGWorldBuilder worldBuilder = BuildWorld();

            DGGame game = new(gameBuilder, worldBuilder);
            game.Initialize();

            // ======== Game Routine ======== //

            Console.WriteLine("START");

            game.StartGame();

            while (game.ShouldUpdateGame())
            {
                game.UpdateGame();

                Console.Clear();
                Console.WriteLine($"[ Round: {game.RoundManager.CurrentRound} || Day: {game.WorldManager.CurrentDay} ({game.WorldManager.CurrentDaylightCycle}) ]");
                Console.WriteLine($"Players: {game.PlayerManager.LivingPlayers.Length}/{game.PlayerManager.TotalPlayerCount};");
            }

            game.FinishGame();
            game.Dispose();

            // ============================== //

            Console.WriteLine("Finished");
        }

        // ===== GAME ===== //
        private static DGGameBuilder BuildGame()
        {
            return new()
            {
                Players = BuildPlayers(50)
            };
        }

        // ===== PLAYERS ===== //
        private static DGPlayerBuilder[] BuildPlayers(int amount)
        {
            DGPlayerBuilder[] players = new DGPlayerBuilder[amount];

            for (int i = 0; i < amount; i++)
            {
                players[i] = new()
                {
                    Name = $"Player_{i}",
                };
            }

            return players;
        }

        // ===== WORLD ===== //
        private static DGWorldBuilder BuildWorld()
        {
            return new()
            {
                Size = new(80),
                Resources = new()
                {
                    TreeCount = 100,
                    StoneCount = 100,
                    ShrubCount = 100
                },
            };
        }
    }
}
