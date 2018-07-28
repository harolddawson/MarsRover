using System;
using System.Collections.Generic;
using System.Linq;
using HaroldDawson.MarsRover.Core.Enums;
using HaroldDawson.MarsRover.Core.Exceptions;
using HaroldDawson.MarsRover.Core.Extensions;
using HaroldDawson.MarsRover.Core.Models;

namespace HaroldDawson.MarsRover.Core.Services
{
    /// <summary>
    /// The navigation service is the intelligence behind moving the rover around the plateau.
    /// This service is responsible for constructing the correct size plateau
    /// and ensuring the rover stays within the bounds of the plateau.
    /// It also is how a program should provide instructions to the rover.
    /// </summary>
    public class MarsPlateauNavigationService
    {
        #region Fields
        //A running list of all the rovers on the plateau.
        private readonly List<NasaMarsRover> rovers;
        private MarsPlateau plateau;
        #endregion

        #region Properties
        /// <summary>
        /// Gets the number of rovers currently on the plateau.
        /// </summary>
        public int RoverCount => rovers?.Count ?? 0;
        #endregion

        public MarsPlateauNavigationService()
        {
            rovers = new List<NasaMarsRover>();
        }
        /// <summary>
        /// This method creates the rectangular plateau based on the coordinate provided.
        /// The corner provided is expected to be the northeast corner of the plateau.
        /// </summary>
        /// <param name="northEastCornerCoordinate"></param>
        /// <exception cref="System.ArgumentOutOfRangeException">Thrown if X or Y coordinate provided is less than 0.</exception>
        public void CreatePlateau(MarsCoordinate northEastCornerCoordinate)
        {
            if (northEastCornerCoordinate == null)
            {
                throw new ArgumentNullException(nameof(northEastCornerCoordinate));
            }
            if (northEastCornerCoordinate.X < 0)
            {
                throw new ArgumentOutOfRangeException("The X coordinate for the northeast corner cannot be less than 0.");
            }
            if (northEastCornerCoordinate.Y < 0)
            {
                throw new ArgumentOutOfRangeException("The Y coordinate for the northeast corner cannot be less than 0.");
            }
            plateau = new MarsPlateau(northEastCornerCoordinate.X, northEastCornerCoordinate.Y);
        }

        /// <summary>
        /// Allows a rover to be placed on the Plateau at the rover's CurrentLocation.
        /// </summary>
        /// <param name="rover"></param>
        /// <exception cref="MarsPlateauOutOfBoundsException">Thrown if the rover has a CurrentLocation value outside of the bounds of the Plateau.</exception>
        public void AddRover(NasaMarsRover rover)
        {
            if (plateau == null)
            {
                throw new Exception("Plateau is null. CreatePlateau must be called before you can place a rover on it.");
            }
            if (!plateau.IsPointWithinPlateauBounds(rover.CurrentLocation))
            {
                throw new MarsPlateauOutOfBoundsException(
                    $"Rover cannot be placed on Plateau because its starting location ({rover.CurrentLocation}) " +
                    $"is outside of the bounds of the Plateau (0 0, {plateau.MinX} {plateau.MaxX}).");
            }

            rovers.Add(rover);
        }

        /// <summary>
        /// Allows the caller to pass an <see cref="IEnumerable{RoverInstructionEnum}"/> of instructions to the current rover.
        /// Also, an action can be passed by the caller if the result of each action is desired.
        /// </summary>
        /// <param name="instructions">IEnumerable of instructions for the current rover</param>
        /// <param name="actionResultReport">The action to be executed after each action that reports the result of that action. If null, results are not reported.</param>
        public void GiveCurrentRoverInstructions(IEnumerable<RoverInstructionEnum> instructions, Action<string> actionResultReport = null)
        {
            if (instructions == null)
            {
                throw new ArgumentNullException(nameof(instructions));
            }
            foreach (var i in instructions)
            {
                GiveCurrentRoverInstruction(i, actionResultReport);
            }
        }

        /// <summary>
        /// Allows the caller to pass one <see cref="RoverInstructionEnum"/> to the current rover.
        /// Also, an action can be passed by the caller if the result of each action is desired.
        /// </summary>
        /// <param name="instruction">One instruction for the current rover</param>
        /// <param name="actionResultReport">The action to be executed after the action that reports the result of that action. If null, results are not reported.</param>
        public void GiveCurrentRoverInstruction(RoverInstructionEnum instruction, Action<string> actionResultReport = null)
        {
            var rover = rovers.LastOrDefault();
            if (rover == null)
            {
                throw new Exception("Rover cannot accept instructions because it has not be established yet.");
            }

            switch (instruction)
            {
                case RoverInstructionEnum.TurnLeft:
                    rover.TurnRoverLeft();
                    actionResultReport?.Invoke($"{rover.CurrentLocation.X} {rover.CurrentLocation.Y} {rover.CurrentHeading}");
                    break;
                case RoverInstructionEnum.TurnRight:
                    rover.TurnRoverRight();
                    actionResultReport?.Invoke($"{rover.CurrentLocation.X} {rover.CurrentLocation.Y} {rover.CurrentHeading}");
                    break;
                case RoverInstructionEnum.MoveForward:
                    var fwdCoordinate = rover.GetForwardCoordinate();
                    if (plateau.IsPointWithinPlateauBounds(fwdCoordinate))
                    {
                        rover.MoveRoverForward();
                        actionResultReport?.Invoke(
                            $"{rover.CurrentLocation.X} {rover.CurrentLocation.Y} {rover.CurrentHeading}");
                    }
                    else
                    {
                        actionResultReport?.Invoke($"Rover cannot move to {fwdCoordinate.X} {fwdCoordinate.Y} because it is outside of the bounds of the Plateau!");
                    }
                    break;
            }
        }

        /// <summary>
        /// Returns an <see cref="IEnumerable{string}"/> to the caller that gives the current location of each rover in the format of "X Y Z"
        /// Where X and Y are the coordinates and Z is the cardinal heading
        /// If there are no rovers, then an empty Enumerable is returned.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<string> GetRoverLocations()
        {
            if (!rovers.Any())
            {
                return new string[0];
            }
            return rovers.Select(r =>
                $"{r.CurrentLocation.X} {r.CurrentLocation.Y} {r.CurrentHeading.ConvertCardinalDirectionToChar()}");
        }
    }
}