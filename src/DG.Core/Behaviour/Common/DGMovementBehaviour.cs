using DG.Core.Behaviour.Models;
using DG.Core.Entities;

namespace DG.Core.Behaviour.Common
{
    internal sealed class DGMovementBehaviour : IDGBehaviour
    {
        public DGBehaviourWeight GetWeight(DGEntity entity, DGGame game)
        {
            DGBehaviourWeight weight = new();

            // === ADIÇÕES ===
            // Perigo [+3]
            // Necessidade de Status (fome, medo, etc) [+2]
            // Andarilho ou Explorador [+1]
            // Procurando Itens/Loot [+2]
            // Procurando Jogadores [+1]
            // Procurando Recursos [+1]

            // === SUBTRAÇÕES ===
            // Tímido ou Medroso [-1]
            // Confortavel [-1]
            // Tem uma Base [-1]

            return weight;
        }

        public DGBehaviourActInfos Act(DGEntity entity, DGGame game)
        {
            DGBehaviourActInfos infos = new();

            // === LOCAIS PARA SE MOVIMENTAR ===
            // Base (inimiga ou amiga)
            // Outros jogadores (inimigos ou amigos)
            // Loot
            // Recurso (animais, objetos, etc)
            // Aleatorio (fugindo, explorando/caminhando/andando)

            infos.WithTitle(string.Empty);
            infos.WithDescription(string.Empty);

            return infos;
        }
    }
}
