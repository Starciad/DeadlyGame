using DG.Core.Behaviour;
using DG.Core.Entities;

using System.Collections.Generic;
using System.Linq;

namespace DG.Core.Components.Common
{
    internal sealed class DGBehaviourComponent : DGComponent
    {
        private sealed class DGAction
        {
            internal IDGBehaviour Behaviour { get; private set; }
            internal float Weight { get; private set; }

            internal DGAction(IDGBehaviour behaviour, float weight)
            {
                this.Behaviour = behaviour;
                this.Weight = weight;
            }
        }

        private readonly List<IDGBehaviour> definedBehaviors = [];

        internal void RegisterBehaviour(IDGBehaviour behaviour)
        {
            this.definedBehaviors.Add(behaviour);
        }

        internal void Act()
        {
            // Get all behaviors with their respective weights.
            DGAction[] possibleActions = GetPossibleActions(this.Entity, this.Game);

            // Get action with higher weight.
            DGAction[] bestActions = possibleActions.OrderByDescending(a => a.Weight).Take(5).ToArray();
            DGAction bestAction = bestActions[this.Game.Random.Range(0, bestActions.Length)];

            // Take action.
            ExecuteAction(bestAction, this.Entity, this.Game);
        }

        private DGAction[] GetPossibleActions(DGEntity entity, DGGame game)
        {
            int count = this.definedBehaviors.Count;

            DGAction[] actions = new DGAction[count];
            for (int i = 0; i < count; i++)
            {
                IDGBehaviour behaviour = this.definedBehaviors[i];
                actions[i] = new(behaviour, behaviour.GetWeight(entity, game).Value);
            }

            return actions;
        }

        private static void ExecuteAction(DGAction action, DGEntity entity, DGGame game)
        {
            _ = action.Behaviour.Act(entity, game);
        }
    }
}
