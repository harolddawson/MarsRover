using System;

namespace HaroldDawson.MarsRover.Core.Exceptions
{
    /// <inheritdoc />
    /// <summary>
    /// This exception is to be thrown when a coordinate or a rover is to be found outside of the bounds of the plateau.
    /// </summary>
    public class MarsPlateauOutOfBoundsException : Exception
    {
        public MarsPlateauOutOfBoundsException(string message) : base(message) { }
    }
}