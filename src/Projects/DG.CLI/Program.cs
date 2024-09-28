using DeadlyGame.CLI.Tools;
using DeadlyGame.Core;
using DeadlyGame.Core.Builders;
using DeadlyGame.Core.Information;

using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;

namespace DeadlyGame.CLI
{
    internal static class Program
    {
        [RequiresUnreferencedCode("Calls DeadlyGame.CLI.Tools.DGJsonSerializer.Serialize(String)")]
        private static void Main()
        {
            DGGameBuilder gameBuilder = BuildGame();
            DGWorldBuilder worldBuilder = BuildWorld();

            DGGame game = new(gameBuilder, worldBuilder);
            game.Initialize();

            // ======== Game Routine ======== //

            Console.WriteLine("START");

            game.StartGame();
            DGGameInfo info = new();

            while (game.ShouldUpdateGame())
            {
                game.UpdateGame();
                info = game.GetGameInfo();

                Console.Clear();
                Console.WriteLine($"[ Round: {info.RoundInfo.CurrentRound} || Day: {info.WorldInfo.CurrentDay} ({info.WorldInfo.CurrentDaylightCycle}) ]");
                Console.WriteLine($"Players: {info.PlayersInfo.ActivePlayers.Length}/{info.PlayersInfo.Players.Length};");

                if (info.PlayersInfo.ActivePlayers.Length < 50)
                {
                    break;
                }
            }

            game.FinishGame();
            game.Dispose();

            // ============================== //
            Console.WriteLine("START (Serializer)");

            string filename = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Ex", "DGGameInfo.json");
            DGJsonSerializer serializer = new(info);
            serializer.Serialize(filename);

            Console.WriteLine("Finished");
        }

        // ===== GAME ===== //
        private static DGGameBuilder BuildGame()
        {
            return new()
            {
                Players = BuildPlayers(100)
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
