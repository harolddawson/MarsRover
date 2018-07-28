namespace HaroldDawson.MarsRover.Core.Exceptions
{
    /// <summary>
    /// This exception class allows the InputParseService to decipher a parsing exception from another exception
    /// so that only the exception that are expected get consumed and anything outside if its scope bubbles out to the caller.
    /// </summary>
    internal class MarsRoverInputParseException : System.Exception
    {
        public MarsRoverInputParseException(string message) : base(message) {}
    }
}