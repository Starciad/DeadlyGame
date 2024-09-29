using DeadlyGame.CLI.Interactivity;
using DeadlyGame.Core.Builders;
using DeadlyGame.Core.Mathematics;
using DeadlyGame.Core.Mathematics.Primitives;

using System;
using System.Collections.Generic;
using System.Text;

namespace DeadlyGame.CLI
{
    internal static partial class Program
    {
        private static readonly DGCommandRegistry commandRegistry = new();
        private static readonly DGRandomMath randomMath = new();

        #region Settings
        // Game
        private static readonly List<DGPlayerBuilder> playerBuilders = [];

        // World
        private static DGPoint worldSize;
        private static int worldTreeRate;
        private static int worldStoneRate;
        private static int worldShrubRate;
        #endregion

        [STAThread]
        private static int Main(string[] args)
        {
            args = ["--configuration", "./assets/configurations/game_01.ini"];

            Console.InputEncoding = Encoding.Unicode;
            Console.OutputEncoding = Encoding.Unicode;

            DGArgumentParser parser = new(args);

            if (args.Length == 0)
            {
                DisplayTitleInfo();
            }
            else
            {
                RegisterCommands();
                ExecuteCommands(parser);
                StartGame();
            }

            return 0;
        }
    }
}