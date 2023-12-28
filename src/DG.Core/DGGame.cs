using DG.Core.Builders;
using DG.Core.Databases;
using DG.Core.Databases.Crafting;
using DG.Core.Databases.Items;
using DG.Core.Dice;
using DG.Core.Information;
using DG.Core.Information.Players;
using DG.Core.Localization;
using DG.Core.Managers;
using DG.Core.Settings;
using DG.Core.Utilities;

using System;

namespace DG.Core
{
    public sealed class DGGame(DGGameBuilder gameBuilder, DGWorldBuilder worldBuilder) : IDisposable
    {
        public bool IsStarted => this._gameStateManager.IsStarted;
        public bool IsFinished => this._gameStateManager.IsFinished;
        public bool IsCanceled => this._gameStateManager.IsCanceled;
        public bool IsDisposed => this.disposedValue;

        // Databases
        internal DGCraftingDatabase CraftingDatabase => this._craftingDatabase;
        internal DGItemDatabase ItemDatabase => this._itemDatabase;

        // Managers
        internal DGPlayerManager PlayerManager => this._playersManager;
        internal DGWorldManager WorldManager => this._worldManager;
        internal DGRoundManager RoundManager => this._roundManager;
        internal DGGameStateManager GameStateManager => this._gameStateManager;

        // Utilities
        internal DGRandomUtilities Random { get; } = new();
        internal DGDice Dice { get; } = new();

        // Settings
        private readonly DGGameSettings _gameSettings = new(gameBuilder);

        // Database
        private readonly DGCraftingDatabase _craftingDatabase = new();
        private readonly DGItemDatabase _itemDatabase = new();

        // Managers
        private DGPlayerManager _playersManager = new();
        private DGWorldManager _worldManager = new();
        private DGRoundManager _roundManager = new();
        private DGGameStateManager _gameStateManager = new();
        private bool disposedValue;

        // System
        public void Initialize()
        {
            DGLocalization.Initialize(gameBuilder.LocalizationFilename);

            this._itemDatabase.SetGameInstance(this);
            this._craftingDatabase.SetGameInstance(this);
            this._worldManager.SetGameInstance(this);
            this._playersManager.SetGameInstance(this);
            this._roundManager.SetGameInstance(this);

            this._itemDatabase.Initialize();
            this._craftingDatabase.Initialize();

            this._worldManager.Initialize(worldBuilder);
            this._playersManager.Initialize(this._gameSettings.Players);
        }

        // Game
        public void StartGame()
        {
            this._gameStateManager.Start();
        }
        public bool ShouldUpdateGame()
        {
            return !this._playersManager.OnlyOneActivePlayer;
        }
        public void UpdateGame()
        {
            // Update of all components.
            this._roundManager.Begin();
            this._playersManager.Update();
            this._worldManager.Update();
        }
        public void FinishGame()
        {
            this._gameStateManager.Finish();
        }
        public void CancelGame()
        {
            this._gameStateManager.Cancel();
        }

        // Utilities
        public DGGameInfo GetGameInfo(bool includeActions = true, bool includePlayers = true, bool includeRound = true, bool includeWorld = true)
        {
            DGGameInfo gameInfo = new();

            if (includeActions)
            {
                gameInfo.ActionsInfo = this._playersManager.GetPlayersActionsInfo();
            }

            if (includePlayers)
            {
                gameInfo.PlayersInfo = this._playersManager.GetPlayersInfo();
            }

            if (includeRound)
            {
                gameInfo.RoundInfo = this._roundManager.GetInfo();
            }

            if (includeWorld)
            {
                gameInfo.WorldInfo = this._worldManager.GetInfo();
            }

            return gameInfo;
        }
        public DGPlayerInfo GetGameWinner()
        {
            return DGPlayerInfo.Create(this._playersManager.ActivePlayers[0]);
        }

        // Tools
        public void Dispose()
        {
            if (!this.disposedValue)
            {
                this._playersManager = null;
                this._worldManager = null;
                this._roundManager = null;
                this._gameStateManager = null;

                this.disposedValue = true;
            }

            GC.Collect();
            GC.SuppressFinalize(this);
        }
    }
}