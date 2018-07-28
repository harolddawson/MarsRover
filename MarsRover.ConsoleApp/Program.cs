using System;
using System.Collections.Generic;
using HaroldDawson.MarsRover.Core.Enums;
using HaroldDawson.MarsRover.Core.Exceptions;
using HaroldDawson.MarsRover.Core.Models;
using HaroldDawson.MarsRover.Core.Services;

namespace HaroldDawson.MarsRover.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            //Construct the navigation service that is the brains of the operation
            var navigationSvc = new MarsPlateauNavigationService();

            //Collect information about the plateau
            var corner = GetPlateauNorthEastCorner();
            navigationSvc.CreatePlateau(corner);

            //Now collect input about the rover(s)
            var rover = GetUserEnteredRover();
            while (rover != null)
            {
                try
                {
                    navigationSvc.AddRover(rover);

                    var roverInstructions = GetInstructionsForRover();

                    navigationSvc.GiveCurrentRoverInstructions(roverInstructions);
                }
                //This exception gets thrown if the starting coordinates for the rover are beyond the bounds of the plateau.
                catch (MarsPlateauOutOfBoundsException ex)
                {
                    Console.WriteLine(ex.Message);
                }

                rover = GetUserEnteredRover();
            }

            if (navigationSvc.RoverCount > 0)
            {
                foreach (var location in navigationSvc.GetRoverLocations())
                {
                    Console.WriteLine(location);
                }
            }
            else
            {
                Console.WriteLine("No rovers to report.");
            }

            Console.WriteLine("Press any key to continue...");
            Console.ReadKey(true);
        }

        /// <summary>
        /// Helper method that takes lines of text to display to the user and then returns the input from the console that was entered by the user in response to the prompt.
        /// </summary>
        /// <param name="promptText"></param>
        /// <returns></returns>
        static string PromptForUserInput(params string[] promptText)
        {
            foreach (var line in promptText)
            {
                Console.WriteLine(line);
            }

            return Console.ReadLine();
        }

        /// <summary>
        /// This method prompts the user for the coordinate that represents the northeast corner of the plateau. It loops indefinitely until the user enters valid information
        /// or the program in terminated.
        /// </summary>
        /// <returns></returns>
        static MarsCoordinate GetPlateauNorthEastCorner()
        {
            while (true)
            {
                var lineWidth = Console.WindowWidth;
                Console.WriteLine("".PadRight(75, '*'));
                var promptText = new List<string>();
                var userInput = PromptForUserInput("Please provide the northeast coordinate for the plateau.",
                "Input must be two two NON-NEGATIVE whole numbers, separated by a space,",
                    "horizontal (or x) coordinate first, then the vertical (or y) coordinate (i.e., 5 6)");

                var parseResult = InputParseService.ParsePlateauDimensions(userInput);
                if (parseResult.IsResultParsedSuccessfully)
                {
                    return parseResult.Results;
                }

                Console.WriteLine("Unable to parse input. Please try again.");
                Console.WriteLine(parseResult.ParsingException.Message);
            }
        }

        /// <summary>
        /// This method prompts the user for the starting coordinate and heading for the rover. If the user does not enter data, then this assumes they have no more rovers to track.
        /// If that is the case, it returns null. Otherwise it will loop infinitely until the user enters valid data or no data at all.
        /// </summary>
        /// <returns></returns>
        static NasaMarsRover GetUserEnteredRover()
        {
            while (true)
            {
                Console.WriteLine("".PadRight(75, '*'));
                var userInput = PromptForUserInput("Please provide the starting coordinate and heading for the rover.",
                    "Input must be two NON-NEGATIVE whole numbers and a cardinal direction, separated by a space,",
                    "horizontal (or x) coordinate first, then the vertical (or y) coordinate, ",
                    "then cardinal heading (i.e., 5 6 W)",
                    "Leave this empty and press ENTER if there are no more rovers to track.");

                if (string.IsNullOrWhiteSpace(userInput))
                {
                    return null;
                }

                var parseResult = InputParseService.ParseUserInputToCreateRover(userInput);
                if (parseResult.IsResultParsedSuccessfully)
                {
                    return parseResult.Results;
                }

                Console.WriteLine("Unable to parse input. Please try again.");
                Console.WriteLine(parseResult.ParsingException.Message);
            }
        }

        /// <summary>
        /// This method prompts the user for the instruction string. It will loop indefinitely until it receives valid data or the program is terminated
        /// </summary>
        /// <returns></returns>
        static List<RoverInstructionEnum> GetInstructionsForRover()
        {
            while (true)
            {
                Console.WriteLine("".PadRight(75, '*'));
                var userInput = PromptForUserInput("Please provide the instructions for the rover.",
                    "Input must be a string of letters with no spaces. Valid instruction letters are 'L', 'R', and 'M' only.");
                var parseResult = InputParseService.ParseRoverInstructionsFromUserInput(userInput);
                if (parseResult.IsResultParsedSuccessfully)
                {
                    return parseResult.Results;
                }

                Console.WriteLine("Unable to parse input. Please try again.");
                Console.WriteLine(parseResult.ParsingException.Message);
            }
        }
    }
}
