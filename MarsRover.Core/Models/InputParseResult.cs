using System;

namespace HaroldDawson.MarsRover.Core.Models
{
    /// <summary>
    /// Represents the result from the InputParseService.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class InputParseResult<T>
    {
        /// <summary>
        /// Tells if the result was able to be parsed successfully.
        /// </summary>
        public bool IsResultParsedSuccessfully { get; internal set; }
        /// <summary>
        /// The object parsed by the InputParseService.
        /// If the parsing was unsuccessful, this object will be the default value of T.
        /// </summary>
        public T Results { get; internal set; }
        
        /// <summary>
        /// The exception thrown by the InputParseService, if parsing was not successful.
        /// If the parsing was successful, this exception will be null.
        /// </summary>
        public Exception ParsingException { get; internal set; }
    }
}