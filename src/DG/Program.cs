using DG.Core;
using DG.Core.Builders;
using DG.Core.Information;

using System;

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

            while (game.ShouldUpdateGame())
            {
                game.UpdateGame();

                DGGameInfo gameInfo = game.GetGameInfo();
                Console.Clear();
                Console.WriteLine($"Rodada: {gameInfo.RoundInfo.CurrentRound} || Dia: {gameInfo.WorldInfo.CurrentDay} ({gameInfo.WorldInfo.CurrentDaylightCycle})");
                Console.WriteLine($"Jogadores: {gameInfo.PlayersInfo.ActivePlayers.Length}\n");
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
