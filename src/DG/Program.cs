using DG.Core;
using DG.Core.Builders;
using DG.Core.Information;

using System;
using System.IO;
using System.Text.Json;

namespace DG
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
                DGGameInfo info = game.GetGameInfo();

                Console.Clear();
                Console.WriteLine($"[ Round: {info.RoundInfo.CurrentRound} || Day: {info.WorldInfo.CurrentDay} ({info.WorldInfo.CurrentDaylightCycle}) ]");
                Console.WriteLine($"Players: {info.PlayersInfo.ActivePlayers.Length}/{info.PlayersInfo.Players.Length};");
            }

            game.FinishGame();
            game.Dispose();

            // ============================== //

            //Console.WriteLine("START");
            //using StreamWriter sw = new(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Ex", "DGGameInfo.json"));
            //string result = JsonSerializer.Serialize(gameInfo, JsonSerializerOptions.Default);
            //sw.Write(result);
            //sw.Close();

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
