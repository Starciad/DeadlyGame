using DG.Core.Builders;
using DG.Core.Dice;
using DG.Core.Managers;
using DG.Core.Settings;
using DG.Core.Utilities;

using System.Threading.Tasks;

namespace DG.Core
{
    public sealed class DGGame(DGGameBuilder gameBuilder, DGWorldBuilder worldBuilder)
    {
        internal DGPlayerManager PlayerManager => this._playersManager;
        internal DGWorldManager WorldManager => this._worldManager;
        internal DGRandom Random { get; } = new();
        internal DGDice Dice { get; } = new();

        // Settings
        private readonly DGGameSettings _gameSettings = new(gameBuilder);

        // Managers
        private readonly DGPlayerManager _playersManager = new();
        private readonly DGWorldManager _worldManager = new();

        public async Task InitializeAsync()
        {
            this._worldManager.SetGameInstance(this);
            this._playersManager.SetGameInstance(this);

            await this._worldManager.InitializeAsync(worldBuilder);
            await this._playersManager.InitializeAsync(this._gameSettings.Players);
        }

        public async Task StartAsync()
        {
            while (!this._playersManager.OnlyOneActivePlayer)
            {
                await this._playersManager.UpdateAsync();
                await this._worldManager.UpdateAsync();
            }

            await Task.CompletedTask;
        }
    }
}
