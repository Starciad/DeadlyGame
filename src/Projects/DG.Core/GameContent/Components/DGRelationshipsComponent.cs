using DeadlyGame.Core.Components;
using DeadlyGame.Core.Entities;
using DeadlyGame.Core.Enums.Relationships;
using DeadlyGame.Core.Relationships;

using System.Collections.Generic;
using System.Linq;

namespace DeadlyGame.Core.GameContent.Components
{
    public sealed class DGRelationshipsComponent : DGComponent
    {
        public DGRelationship[] Relationships => this.relationships.Values.ToArray();

        private readonly Dictionary<DGEntity, DGRelationship> relationships = [];

        public DGRelationshipsComponent(DGGame game, DGEntity entity) : base(game, entity)
        {

        }

        public DGRelationship AddRelationship(DGEntity anotherEntity, float value = 50)
        {
            DGRelationship relationship = new(this.DGEntityInstance, anotherEntity, value);
            _ = this.relationships.TryAdd(anotherEntity, relationship);
            return relationship;
        }

        public DGRelationship GetRelationship(DGEntity anotherEntity)
        {
            return this.relationships.TryGetValue(anotherEntity, out DGRelationship value) ? value : AddRelationship(anotherEntity);
        }

        public float GetRelationshipValue(DGEntity anotherEntity)
        {
            return GetRelationship(anotherEntity).RelationshipValue;
        }

        public DGRelationshipLevelType GetRelationshipLevelType(DGEntity anotherEntity)
        {
            return GetRelationship(anotherEntity).GetRelationshipLevelType();
        }

        public DGRelationship[] GetBetterRelationships()
        {
            return
            [
                .. this.relationships.Values.OrderByDescending(x => x.RelationshipValue)
            ];
        }

        public DGRelationship[] GetWorstRelationships()
        {
            return
            [
                .. this.relationships.Values.OrderBy(x => x.RelationshipValue)
            ];
        }

        public void SetRelationshipValue(DGEntity anotherEntity, float value)
        {
            GetRelationship(anotherEntity).SetRelationshipValue(value);
        }

        public void IncreaseRelationshipValue(DGEntity anotherEntity, float value)
        {
            GetRelationship(anotherEntity).IncreaseRelationshipValue(value);
        }

        public void DecreaseRelationshipValue(DGEntity anotherEntity, float value)
        {
            GetRelationship(anotherEntity).DecreaseRelationshipValue(value);
        }
    }
}
