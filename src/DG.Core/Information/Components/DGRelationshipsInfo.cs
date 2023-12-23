using DG.Core.Components.Common;
using DG.Core.Relationships;

namespace DG.Core.Information.Components
{
    public struct DGRelationshipsInfo
    {
        public DGRelationshipInfo[] Relationships { get; set; }

        internal static DGRelationshipsInfo Create(DGRelationshipsComponent component)
        {
            DGRelationship[] relationships = component.Relationships;
            int relationshipsLength = relationships.Length;

            DGRelationshipInfo[] relationshipsInfo = new DGRelationshipInfo[relationshipsLength];
            for (int i = 0; i < relationshipsLength; i++)
            {
                relationshipsInfo[i] = new()
                {
                    RelationshipValue = relationships[i].RelationshipValue,
                    CurrentEntityName = relationships[i].CurrentEntity.Name,
                    AnotherEntityName = relationships[i].AnotherEntity.Name,
                };
            }

            return new()
            {
                Relationships = relationshipsInfo,
            };
        }
    }

    public struct DGRelationshipInfo
    {
        public float RelationshipValue { get; set; }
        public string CurrentEntityName { get; set; }
        public string AnotherEntityName { get; set; }
    }
}
