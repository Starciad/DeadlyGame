using System;

namespace DeadlyGame.Core.Exceptions.Components
{
    public sealed class DGInvalidComponentTypeException : Exception
    {
        public DGInvalidComponentTypeException()
        {
        }

        public DGInvalidComponentTypeException(string message)
            : base(message)
        {
        }

        public DGInvalidComponentTypeException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
