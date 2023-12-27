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

            game.StartGame();
            DGGameInfo gameInfo = default;

            while (game.ShouldUpdateGame())
            {
                game.UpdateGame();
                gameInfo = game.GetGameInfo();

                Console.Clear();
                Console.WriteLine(gameInfo.PlayersInfo.ActivePlayers.Length);
                if (gameInfo.PlayersInfo.ActivePlayers.Length <= 50)
                {
                    break;
                }
            }

            game.FinishGame();
            game.Dispose();

            // ============================== //

            Console.WriteLine("START");
            StreamWriter sw = new(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Ex", "DGGameInfo.json"));

            string result = JsonSerializer.Serialize(gameInfo, JsonSerializerOptions.Default);

            sw.Write(result);
            sw.Close();

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
                Size = new(100),
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
