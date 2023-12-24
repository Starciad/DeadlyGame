using System;

namespace DG.Core.Exceptions.Effects
{
    internal sealed class DGInvalidEffectTypeException : Exception
    {
        internal DGInvalidEffectTypeException()
        {
        }

        internal DGInvalidEffectTypeException(string message)
            : base(message)
        {
        }

        internal DGInvalidEffectTypeException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
