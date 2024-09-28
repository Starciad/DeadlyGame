using System;

namespace DeadlyGame.Core.Exceptions.Components
{
    internal sealed class DGInvalidComponentTypeException : Exception
    {
        internal DGInvalidComponentTypeException()
        {
        }

        internal DGInvalidComponentTypeException(string message)
            : base(message)
        {
        }

        internal DGInvalidComponentTypeException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
