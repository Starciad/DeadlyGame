namespace DG.Core.Utilities
{
    internal static class DGAttributesUtilities
    {
        internal static int GetAttributeModifier(int attributeValue)
        {
            return (attributeValue - 10) / 2;
        }
    }
}