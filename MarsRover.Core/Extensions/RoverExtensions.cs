using System;
using System.Linq;
using HaroldDawson.MarsRover.Core.Enums;
using HaroldDawson.MarsRover.Core.Exceptions;
using HaroldDawson.MarsRover.Core.Models;

namespace HaroldDawson.MarsRover.Core.Extensions
{
    public static class RoverExtensions
    {
        /// <summary>
        /// Takes a single character and will convert it into a <see cref="RoverInstructionEnum"/>.
        /// </summary>
        /// <param name="instructionChar"></param>
        /// <returns>The resulting enum that matches the char provided</returns>
        /// <exception cref="MarsRoverInputParseException">Thrown if the character provided is not within the range of parsable values.</exception>
        public static RoverInstructionEnum ConvertCharInputToInstructionEnum(this char instructionChar)
        {
            switch (instructionChar)
            {
                case 'L':
                    return RoverInstructionEnum.TurnLeft;
                case 'R':
                    return RoverInstructionEnum.TurnRight;
                case 'M':
                    return RoverInstructionEnum.MoveForward;
                default:
                    throw new MarsRoverInputParseException($"The char '{instructionChar}' is out of range of valid values. The valid values are 'L', 'M', and 'R', and are case-sensitive.");
            }
        }

        /// <summary>
        /// Takes a single character and will convert it into a <see cref="RoverCardinalDirectionEnum"/>.
        /// </summary>
        /// <param name="instructionChar"></param>
        /// <returns>The resulting enum that matches the char provided</returns>
        /// <exception cref="MarsRoverInputParseException">Thrown if the character provided is not within the range of parsable values.</exception>
        public static RoverCardinalDirectionEnum ConvertCharInputToCardinalDirectionEnum(this char instructionChar)
        {
            switch (instructionChar)
            {
                case 'N':
                    return RoverCardinalDirectionEnum.North;
                case 'E':
                    return RoverCardinalDirectionEnum.East;
                case 'W':
                    return RoverCardinalDirectionEnum.West;
                case 'S':
                    return RoverCardinalDirectionEnum.South;
                default:
                    throw new MarsRoverInputParseException($"The char '{instructionChar}' is out of range of valid values. The valid values are 'N', 'E', 'W, and 'S', and are case-sensitive.");
            }
        }

        /// <summary>
        /// Converts the <see cref="RoverCardinalDirectionEnum"/> into a single char for text output.
        /// This method gets the first char of the ToString() and returns it.
        /// </summary>
        /// <param name="directionEnum"></param>
        /// <returns></returns>
        public static char ConvertCardinalDirectionToChar(this RoverCardinalDirectionEnum directionEnum)
        {
            //Grab the first char from the char array
            return directionEnum.ToString().First();
        }
    }
}