using System.Collections.Generic;
using HaroldDawson.MarsRover.Core.Enums;
using HaroldDawson.MarsRover.Core.Exceptions;
using HaroldDawson.MarsRover.Core.Extensions;
using HaroldDawson.MarsRover.Core.Models;

namespace HaroldDawson.MarsRover.Core.Services
{
    /// <summary>
    /// This service takes the collected input and converts it into classes (or enums)
    /// These methods returning a generic InputParseResult, which contains a boolean to indicate success/fail
    /// a property to hold the results if parsed successfully,
    /// and a property to hold the parsing exception, if one was thrown and parsing failed.
    /// </summary>
    public class InputParseService
    {
        /// <summary>
        /// Takes the user input string and attempts to parse it into a <see cref="MarsCoordinate"/>.
        /// </summary>
        /// <param name="userInput"></param>
        /// <returns></returns>
        public static InputParseResult<MarsCoordinate> ParsePlateauDimensions(string userInput)
        {
            var parseResult = new InputParseResult<MarsCoordinate>();
            try
            {
                if (userInput == null)
                {
                    throw new MarsRoverInputParseException("Unable to parse. Input is null.");
                }

                var inputPieces = userInput.Split(' ');

                if (inputPieces.Length != 2)
                {
                    throw new MarsRoverInputParseException(
                        "Unable to parse. Input is not in the expected format of \"X Y\".");
                }

                //Attempt to parse the input for valid non-negative integers
                if (!int.TryParse(inputPieces[0], out var xCoordinate)
                    || !int.TryParse(inputPieces[1], out var yCoordinate)
                    || xCoordinate < 0
                    || yCoordinate < 0)
                {
                    throw new MarsRoverInputParseException(
                        "Unable to parse. Input cannot be parsed into non-negative integers.");
                }

                parseResult.Results = new MarsCoordinate() {X = xCoordinate, Y = yCoordinate};
                parseResult.IsResultParsedSuccessfully = true;
            }
            catch (MarsRoverInputParseException ex)
            {
                parseResult.ParsingException = ex;
                parseResult.IsResultParsedSuccessfully = false;
                parseResult.Results = null;
            }

            return parseResult;
        }

        /// <summary>
        /// Takes the user input string and attempts to parse it into a <see cref="NasaMarsRover"/>.
        /// </summary>
        /// <param name="userInput"></param>
        /// <returns></returns>
        public static InputParseResult<NasaMarsRover> ParseUserInputToCreateRover(string userInput)
        {
            var parseResult = new InputParseResult<NasaMarsRover>();
            try
            {
                if (userInput == null)
                {
                    throw new MarsRoverInputParseException("Unable to parse. Input is null.");
                }

                var inputPieces = userInput.Split(' ');

                if (inputPieces.Length != 3)
                {
                    throw new MarsRoverInputParseException(
                        "Unable to parse. Input is not in the expected format of \"X Y Z\".");
                }

                //Attempt to parse the input for valid positive integers
                if (!int.TryParse(inputPieces[0], out var xCoordinate)
                    || !int.TryParse(inputPieces[1], out var yCoordinate)
                    || xCoordinate < 0
                    || yCoordinate < 0)
                {
                    throw new MarsRoverInputParseException(
                        "Unable to parse. Input cannot be parsed into non-negative integers.");
                }

                if (!char.TryParse(inputPieces[2], out var result))
                {
                    throw new MarsRoverInputParseException(
                        "Unable to parse. Third character input cannot be parsed into a cardinal direction.");
                }

                var roverHeading = result.ConvertCharInputToCardinalDirectionEnum();
                parseResult.Results = new NasaMarsRover(xCoordinate, yCoordinate, roverHeading);
                parseResult.IsResultParsedSuccessfully = true;
            }
            catch (MarsRoverInputParseException ex)
            {
                parseResult.ParsingException = ex;
                parseResult.IsResultParsedSuccessfully = false;
                parseResult.Results = null;
            }

            return parseResult;
        }

        /// <summary>
        /// Takes the user input string and attempts to parse it into a <see cref="List{RoverInstructionEnum}"/>.
        /// </summary>
        /// <param name="userInput"></param>
        /// <returns></returns>
        public static InputParseResult<List<RoverInstructionEnum>> ParseRoverInstructionsFromUserInput(string userInput)
        {
            var parseResult = new InputParseResult<List<RoverInstructionEnum>>();
            try
            {
                var instructions = new List<RoverInstructionEnum>();
                if (userInput == null)
                {
                    throw new MarsRoverInputParseException("Unable to parse. Input is null.");
                }

                foreach (var letter in userInput)
                {
                    instructions.Add(letter.ConvertCharInputToInstructionEnum());
                }

                parseResult.Results = instructions;
                parseResult.IsResultParsedSuccessfully = true;
            }
            catch (MarsRoverInputParseException ex)
            {
                parseResult.ParsingException = ex;
                parseResult.IsResultParsedSuccessfully = false;
                parseResult.Results = null;
            }

            return parseResult;
        }
    }
}