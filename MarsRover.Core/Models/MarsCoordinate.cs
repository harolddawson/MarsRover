namespace HaroldDawson.MarsRover.Core.Models
{
    /// <summary>
    /// Represents on point on the Mars Plateau.
    /// </summary>
    public class MarsCoordinate
    {
        /// <summary>
        /// The horizontal coordinate
        /// </summary>
        public int X { get; set; }
        
        /// <summary>
        /// The vertical coordinate
        /// </summary>
        public int Y { get; set; }

        public override string ToString()
        {
            return $"{X} {Y}";
        }
    }
}