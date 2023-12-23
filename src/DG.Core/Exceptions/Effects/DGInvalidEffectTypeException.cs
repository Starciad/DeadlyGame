using System;

namespace DG.Core.Exceptions.Effects
{
    public sealed class DGInvalidEffectTypeException : Exception
    {
        public DGInvalidEffectTypeException()
        {
        }

        public DGInvalidEffectTypeException(string message)
            : base(message)
        {
        }

        public DGInvalidEffectTypeException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
