using DG.Core.Builders;
using DG.Core.Settings;
using DG.Core.Managers;

using System.Threading.Tasks;

namespace DG.Core
{
    public sealed class DGGame(DGGameBuilder gameBuilder, DGWorldBuilder worldBuilder)
    {
        internal static DGPlayerManager PlayerManager { get; private set; }
        internal static DGWorldManager WorldManager { get; private set; }

        // Settings
        private readonly DGGameSettings _gameSettings = new(gameBuilder);

        // Managers
        private readonly DGPlayerManager _playersManager = new();
        private readonly DGWorldManager _worldManager = new();

        public async Task InitializeAsync()
        {
            await _worldManager.InitializeAsync(worldBuilder);
            await _playersManager.InitializeAsync(this._gameSettings.Players);

            PlayerManager = _playersManager;
            WorldManager = _worldManager;
        }

        public async Task StartAsync()
        {
            while (!_playersManager.OnlyOneActivePlayer)
            {
                await _playersManager.UpdateAsync();
                await _worldManager.UpdateAsync();
            }

            await Task.CompletedTask;
        }
    }
}
