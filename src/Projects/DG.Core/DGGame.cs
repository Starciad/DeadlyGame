using DeadlyGame.Core.Builders;
using DeadlyGame.Core.Databases.Items;
using DeadlyGame.Core.Dice;
using DeadlyGame.Core.Localization;
using DeadlyGame.Core.Managers;
using DeadlyGame.Core.Mathematics;
using DeadlyGame.Core.Settings;
using DeadlyGame.Core.Databases.Crafting;

using System;
using DeadlyGame.Core.GameContent.Entities.Players;

namespace DeadlyGame.Core
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
        public DGPlayerManager PlayerManager => this._playersManager;
        public DGWorldManager WorldManager => this._worldManager;
        public DGRoundManager RoundManager => this._roundManager;
        public DGGameStateManager GameStateManager => this._gameStateManager;

        // Utilities
        internal DGRandomMath Random { get; } = new();
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
            DGLocalization.Initialize("pt", "BR");

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
            return !this._playersManager.OnlyOnePlayerAlive;
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
        public DGPlayer GetGameWinner()
        {
            return this._playersManager.LivingPlayers[0];
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