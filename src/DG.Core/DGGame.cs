using DG.Core.Builders;
using DG.Core.Dice;
using DG.Core.Information;
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

        // Managers
        private DGPlayerManager _playersManager = new();
        private DGWorldManager _worldManager = new();
        private DGRoundManager _roundManager = new();
        private DGGameStateManager _gameStateManager = new();
        private bool disposedValue;

        // System
        public void Initialize()
        {
            this._worldManager.SetGameInstance(this);
            this._playersManager.SetGameInstance(this);
            this._roundManager.SetGameInstance(this);

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
        public DGGameInfo UpdateGame()
        {
            // Round starts only when it is a new day.
            if (this._worldManager.CurrentDaylightCycle == DGWorldDaylightCycleState.Day)
            {
                this._roundManager.Begin();
            }

            // Update of all components.
            this._playersManager.Update();
            this._worldManager.Update();

            // Round ends only when it is one night.
            if (this._worldManager.CurrentDaylightCycle == DGWorldDaylightCycleState.Night)
            {
                this._roundManager.End();
            }

            // Get and configure information
            return new()
            {
                PlayersInfo = this._playersManager.GetInfo(),
                RoundInfo = this._roundManager.GetInfo(),
                WorldInfo = this._worldManager.GetInfo(),
            };
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
        public void GetGameWinner()
        {

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