using System;

namespace DG.Core.Utilities
{
    internal static class DGAttributesUtilities
    {
        internal static int GetAttributeModifier(int attributeValue)
        {
            return (int)Math.Floor((attributeValue - 10) / 2f);
        }
    }
}