using DG.Core;
using DG.Core.Builders;
using DG.Core.Information;
using DG.Settings;
using DG.Utilities;
using DG.Core.Managers;

using System;
using System.IO;
using System.Threading;
using DG.Core.Information.Players;
using DG.Core.Information.Actions;
using System.Diagnostics;

namespace DG
{
    internal sealed class Startup
    {
        // System
        private readonly DGIniReader _settings = new(Path.Combine(Directory.GetCurrentDirectory(), "System", "Settings.ini"));
        private readonly DGIniReader _messages = new(Path.Combine(Directory.GetCurrentDirectory(), "System", "Messages.ini"));

        // Settings
        private readonly float _shortMessageDelay;
        private readonly float _longMessageDelay;

        // Infos
        private readonly DGGame _game;
        private readonly Stopwatch _stopwatch;

        // States
        private bool isIntroSent;

        // Consts
        private static readonly string _break = Environment.NewLine;
        private static readonly string _border = new('=', 32);

        internal Startup()
        {
            this.isIntroSent = false;

            // ===== SYSTEM =====
            this._stopwatch = new();
            this._shortMessageDelay = float.Parse(this._settings.Read("Messages", "Short_Delay"));
            this._longMessageDelay = float.Parse(this._settings.Read("Messages", "Long_Delay"));

            // ===== GAME =====
            DGGameBuilder gameBuilder = new()
            {
                Players = PlayersUtilities.CreatePlayers(this._settings),
                LocalizationFilename = this._settings.Read("General", "LocalizationFilePath")
            };

            // ===== WORLD =====
            int worldWidth = int.Parse(this._settings.Read("World", "World_Width"));
            int worldHeight = int.Parse(this._settings.Read("World", "World_Height"));
            int treeCount = int.Parse(this._settings.Read("World", "Tree_Count"));
            int stoneCount = int.Parse(this._settings.Read("World", "Stone_Count"));
            int shrubCount = int.Parse(this._settings.Read("World", "Shrub_Count"));

            DGWorldBuilder worldBuilder = new()
            {
                Size = new(worldWidth, worldHeight),
                Resources = new()
                {
                    TreeCount = treeCount,
                    StoneCount = stoneCount,
                    ShrubCount = shrubCount,
                }
            };

            // ===== FINAL =====
            this._game = new(gameBuilder, worldBuilder);
        }

        #region ROUTINE
        internal void Start()
        {
            this._game.Initialize();
        }
        internal void Update()
        {
            this._stopwatch.Start();
            while (this._game.ShouldUpdateGame())
            {
                this._game.UpdateGame();
                PrintGameInfos(this._game.GetGameInfo());
            }
            this._stopwatch.Stop();
        }
        internal void End()
        {
            PrintEndOfGame(this._game.GetGameInfo(false, true, false, false));
        }
        #endregion

        #region PRINT
        private void PrintGameInfos(DGGameInfo gameInfo)
        {
            Console.Clear();

            // ================================ //
            // Game introduction message.
            if (!this.isIntroSent)
            {
                PrintBorder();
                Console.WriteLine(string.Concat(this._messages.Read("Game", "Title"), _break));
                PrintBorder();

                Console.WriteLine(this._messages.Read("Game", "Message_01"));
                Console.WriteLine(string.Format(this._messages.Read("Game", "Message_02"), gameInfo.PlayersInfo.Players.Length));
                Console.WriteLine();

                this.isIntroSent = true;
            }

            Thread.Sleep(TimeSpan.FromSeconds(this._longMessageDelay));

            // ================================ //
            // Round introduction messages.
            PrintBorder();

            if (gameInfo.WorldInfo.CurrentDaylightCycle == DGWorldDaylightCycleState.Day)
            {
                SetForefroundColor(ConsoleColor.Yellow);
                Console.WriteLine(string.Concat(this._messages.Read("Rounds", "Intro_Day"), _break));
                SetForefroundColor(ConsoleColor.White);
                Console.WriteLine(string.Format(this._messages.Read("Rounds", "Intro_Day_Description"), gameInfo.WorldInfo.CurrentDay, gameInfo.PlayersInfo.ActivePlayers.Length));
            }
            else
            {
                SetForefroundColor(ConsoleColor.DarkBlue);
                Console.WriteLine(string.Concat(this._messages.Read("Rounds", "Intro_Night"), _break));
                SetForefroundColor(ConsoleColor.White);
                Console.WriteLine(this._messages.Read("Rounds", "Intro_Night_Description"));
            }

            Console.WriteLine();
            PrintBorder();
            Thread.Sleep(TimeSpan.FromSeconds(this._longMessageDelay));

            // ================================ //
            // Information about game players.
            PrintBorder();
            SetForefroundColor(ConsoleColor.Magenta);
            Console.WriteLine(string.Concat(this._messages.Read("Players", "Title"), _break));
            SetForefroundColor(ConsoleColor.White);

            DGPlayerInfo[] activePlayers = gameInfo.PlayersInfo.ActivePlayers;
            DGPlayerInfo[] disablePlayers = gameInfo.PlayersInfo.DisabledPlayers;
            int activePlayersLength = activePlayers.Length;
            int disablePlayersLength = disablePlayers.Length;

            Console.WriteLine(string.Format(this._messages.Read("Players", "Info_Remaining_Players"), activePlayersLength));
            Console.WriteLine(string.Format(this._messages.Read("Players", "Info_Dead_Players"), disablePlayersLength));
            Console.WriteLine(this._messages.Read("Players", "Tributes_List_Title"));

            for (int i = 0; i < activePlayersLength; i++)
            {
                Console.WriteLine(string.Concat("> ", activePlayers[i].Name));
            }
            Console.WriteLine();
            PrintBorder();

            Thread.Sleep(TimeSpan.FromSeconds(this._longMessageDelay));

            // ================================ //
            // Messages about what happened in the round.
            DGPlayerActionInfo[] actions = gameInfo.ActionsInfo.Actions;
            int actionsLength = actions.Length;

            for (int i = 0; i < actionsLength; i++)
            {
                Thread.Sleep(TimeSpan.FromSeconds(this._shortMessageDelay));

                DGPlayerActionInfo targetAction = actions[i];

                Console.WriteLine(string.Concat("- ", targetAction.Description));
                Console.WriteLine();
            }

            Console.WriteLine();
            PrintBorder();

            Thread.Sleep(TimeSpan.FromSeconds(this._longMessageDelay));
        }
        private void PrintEndOfGame(DGGameInfo gameInfo)
        {
            Console.Clear();

            PrintBorder();
            SetForefroundColor(ConsoleColor.Yellow);
            Console.WriteLine(this._messages.Read("Victory", "Title"));
            SetForefroundColor(ConsoleColor.White);
            Console.WriteLine(string.Concat(string.Format(this._messages.Read("Victory", "Description"), gameInfo.PlayersInfo.ActivePlayers[0].Name), _break));
            PrintBorder();

            Thread.Sleep(TimeSpan.FromSeconds(this._longMessageDelay));
            Console.WriteLine(this._messages.Read("Finish", "Description"));

            TimeSpan totalGameTime = this._stopwatch.Elapsed;
            Console.WriteLine();
            Console.WriteLine(string.Concat("Tempo total de jogo: ", totalGameTime.ToString(@"hh\:mm\:ss")));
        }
        #endregion

        #region Utilities
        private static void SetForefroundColor(ConsoleColor color)
        {
            Console.ForegroundColor = color;
        }
        private static void PrintBorder()
        {
            SetForefroundColor(ConsoleColor.Gray);
            Console.WriteLine(string.Concat(_border, _break));
            SetForefroundColor(ConsoleColor.White);
        }
        #endregion
    }
}
