using DeadlyGame.Core.Behaviors;
using DeadlyGame.Core.Components;
using DeadlyGame.Core.Constants;
using DeadlyGame.Core.Models.Infos.Actions;

using System.Collections.Generic;
using System.Linq;

namespace DeadlyGame.Core.GameContent.Components
{
    public sealed class DGBehaviourComponent : DGComponent
    {
        private struct DGAction
        {
            public IDGBehaviour Behaviour { get; private set; }
            public float Weight { get; private set; }

            public DGAction(IDGBehaviour behaviour, float weight)
            {
                this.Behaviour = behaviour;
                this.Weight = weight;
            }
        }

        public DGPlayerActionInfo LastActionInfos { get; private set; }

        private readonly List<IDGBehaviour> definedBehaviors = [];

        public void RegisterBehaviour(IDGBehaviour behaviour)
        {
            this.definedBehaviors.Add(behaviour);
        }

        public void Act()
        {
            // Get all behaviors with their respective weights.
            DGAction[] possibleActions = GetPossibleActions();

            // Get action with higher weight.
            DGAction[] bestActions = possibleActions.Take(DGBehaviourConstants.MAXIMUM_SELECTION_OF_BEST_BEHAVIORS).ToArray();
            DGAction bestAction = bestActions[this.Game.Random.Range(0, bestActions.Length)];

            // Take action.
            this.LastActionInfos = ExecuteAction(bestAction);
        }

        private DGAction[] GetPossibleActions()
        {
            IDGBehaviour[] behaviours = this.definedBehaviors.Where(x => x.CanAct(this.Entity, this.Game)).ToArray();
            int behavioursLength = behaviours.Length;

            DGAction[] actions = new DGAction[behavioursLength];
            for (int i = 0; i < behavioursLength; i++)
            {
                IDGBehaviour behaviour = behaviours[i];
                actions[i] = new(behaviour, behaviour.GetWeight().Value);
            }

            return [.. actions.OrderByDescending(a => a.Weight)];
        }

        private static DGPlayerActionInfo ExecuteAction(DGAction action)
        {
            return action.Behaviour.Act();
        }
    }
}
