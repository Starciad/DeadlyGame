using DeadlyGame.Core.Builders;
using DeadlyGame.Core.Databases.Crafting;
using DeadlyGame.Core.Dice;
using DeadlyGame.Core.GameContent.Entities.Players;
using DeadlyGame.Core.Localization;
using DeadlyGame.Core.Managers;
using DeadlyGame.Core.Mathematics;
using DeadlyGame.Core.Settings;

using System;

namespace DeadlyGame.Core
{
    public sealed class DGGame
    {
        public bool IsStarted => this._gameStateManager.IsStarted;
        public bool IsFinished => this._gameStateManager.IsFinished;
        public bool IsCanceled => this._gameStateManager.IsCanceled;

        public DGCraftingDatabase CraftingDatabase => this._craftingDatabase;

        public DGPlayerManager PlayerManager => this._playersManager;
        public DGWorldManager WorldManager => this._worldManager;
        public DGRoundManager RoundManager => this._roundManager;
        public DGGameStateManager GameStateManager => this._gameStateManager;

        public DGRandomMath RandomMath { get; }
        public DGDice Dice { get; }

        private readonly DGGameSettings _gameSettings;

        private readonly DGCraftingDatabase _craftingDatabase;

        private readonly DGPlayerManager _playersManager;
        private readonly DGWorldManager _worldManager;
        private readonly DGRoundManager _roundManager;
        private readonly DGGameStateManager _gameStateManager;

        private readonly Random _rnd;

        public DGGame(DGGameBuilder gameBuilder, DGWorldBuilder worldBuilder)
        {
            this._rnd = new();

            this.RandomMath = new(this._rnd);
            this.Dice = new(this._rnd);

            this._gameSettings = new(gameBuilder);

            this._craftingDatabase = new(this);
            this._roundManager = new(this);
            this._gameStateManager = new(this);
            this._worldManager = new(this, worldBuilder);
            this._playersManager = new(this, this._gameSettings.Players);

            this._craftingDatabase.Start();
            this._roundManager.Start();
            this._gameStateManager.Start();
            this._worldManager.Start();
            this._playersManager.Start();

            DGLocalization.Initialize("pt", "BR");
        }

        // Game
        public void StartGame()
        {
            this._gameStateManager.StartGame();
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
            this._gameStateManager.FinishGame();
        }
        public void CancelGame()
        {
            this._gameStateManager.CancelGame();
        }

        // Utilities
        public DGPlayer GetGameWinner()
        {
            return this._playersManager.LivingPlayers[0];
        }
    }
}