using System.Collections.Generic;
using System.Linq;

namespace DeadlyGame.Core.Components.Common
{
    internal sealed class DGRelationshipsComponent : DGComponent
    {
        internal DGRelationship[] Relationships => this.relationships.Values.ToArray();

        private readonly Dictionary<DGEntity, DGRelationship> relationships = [];

        internal DGRelationship AddRelationship(DGEntity anotherEntity, float value = 50)
        {
            DGRelationship relationship = new(this.Entity, anotherEntity, value);
            _ = this.relationships.TryAdd(anotherEntity, relationship);
            return relationship;
        }

        internal DGRelationship GetRelationship(DGEntity anotherEntity)
        {
            return this.relationships.TryGetValue(anotherEntity, out DGRelationship value) ? value : AddRelationship(anotherEntity);
        }

        internal float GetRelationshipValue(DGEntity anotherEntity)
        {
            return GetRelationship(anotherEntity).RelationshipValue;
        }

        internal DGRelationshipLevelType GetRelationshipLevelType(DGEntity anotherEntity)
        {
            return GetRelationship(anotherEntity).GetRelationshipLevelType();
        }

        internal DGRelationship[] GetBetterRelationships()
        {
            return
            [
                .. this.relationships.Values.OrderByDescending(x => x.RelationshipValue)
            ];
        }

        internal DGRelationship[] GetWorstRelationships()
        {
            return
            [
                .. this.relationships.Values.OrderBy(x => x.RelationshipValue)
            ];
        }

        internal void SetRelationshipValue(DGEntity anotherEntity, float value)
        {
            GetRelationship(anotherEntity).SetRelationshipValue(value);
        }

        internal void IncreaseRelationshipValue(DGEntity anotherEntity, float value)
        {
            GetRelationship(anotherEntity).IncreaseRelationshipValue(value);
        }

        internal void DecreaseRelationshipValue(DGEntity anotherEntity, float value)
        {
            GetRelationship(anotherEntity).DecreaseRelationshipValue(value);
        }
    }
}
