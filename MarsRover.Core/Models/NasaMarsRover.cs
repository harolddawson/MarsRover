using System.Collections.Generic;
using HaroldDawson.MarsRover.Core.Enums;

namespace HaroldDawson.MarsRover.Core.Models
{
    /// <summary>
    /// This class represents the NASA Mars Rover.
    /// </summary>
    public class NasaMarsRover
    {
        #region Properties
        /// <summary>
        /// The rover's current X Y coordinate
        /// </summary>
        public MarsCoordinate CurrentLocation { get; set; }

        /// <summary>
        /// The direction the rover is currently facing
        /// </summary>
        public RoverCardinalDirectionEnum CurrentHeading { get; set; }
        #endregion

        #region Constructor
        public NasaMarsRover(int startingX, int startingY, RoverCardinalDirectionEnum startingHeading)
        {
            CurrentLocation = new MarsCoordinate()
            {
                X = startingX,
                Y = startingY
            };
            CurrentHeading = startingHeading;
        }
        #endregion

        #region Internal Methods -- used to give the Rover instructions
        /// <summary>
        /// Turns the rover left by 90 degrees, without changing its coordinates. For example, if the rover is facing North, this method will cause it to face West.
        /// </summary>
        internal void TurnRoverLeft()
        {
            switch (CurrentHeading)
            {
                case RoverCardinalDirectionEnum.North:
                    CurrentHeading = RoverCardinalDirectionEnum.West;
                    break;
                case RoverCardinalDirectionEnum.East:
                    CurrentHeading = RoverCardinalDirectionEnum.North;
                    break;
                case RoverCardinalDirectionEnum.South:
                    CurrentHeading = RoverCardinalDirectionEnum.East;
                    break;
                case RoverCardinalDirectionEnum.West:
                    CurrentHeading = RoverCardinalDirectionEnum.South;
                    break;
            }
        }
        /// <summary>
        /// Turns the rover right by 90 degrees, without changing its coordinates. For example, if the rover is facing North, this method will cause it to face East.
        /// </summary>
        internal void TurnRoverRight()
        {
            switch (CurrentHeading)
            {
                case RoverCardinalDirectionEnum.North:
                    CurrentHeading = RoverCardinalDirectionEnum.East;
                    break;
                case RoverCardinalDirectionEnum.East:
                    CurrentHeading = RoverCardinalDirectionEnum.South;
                    break;
                case RoverCardinalDirectionEnum.South:
                    CurrentHeading = RoverCardinalDirectionEnum.West;
                    break;
                case RoverCardinalDirectionEnum.West:
                    CurrentHeading = RoverCardinalDirectionEnum.North;
                    break;
            }
        }

        /// <summary>
        /// This returns the coordinate that is directly in front of the rover. For example, if the rover is at (1, 1), and the rover is facing north, it will return a coordinate of (1, 2).
        /// This method does NOT move the rover.
        /// </summary>
        /// <returns></returns>
        internal MarsCoordinate GetForwardCoordinate()
        {
            var newX = CurrentLocation.X;
            var newY = CurrentLocation.Y;

            switch (CurrentHeading)
            {
                case RoverCardinalDirectionEnum.North:
                    newY++;
                    break;
                case RoverCardinalDirectionEnum.East:
                    newX++;
                    break;
                case RoverCardinalDirectionEnum.South:
                    newY--;
                    break;
                case RoverCardinalDirectionEnum.West:
                    newX--;
                    break;
            }

            return new MarsCoordinate()
            {
                X = newX,
                Y = newY
            };
        }

        /// <summary>
        /// This method moves the rover to the coordinate directy in front of it.
        /// There is no safety to prevent the rover from moving beyond the bounds of the plateau,
        /// so that check is the responsibility of the caller.
        /// </summary>
        internal void MoveRoverForward()
        {
            CurrentLocation = GetForwardCoordinate();
        }
        #endregion
    }
}