namespace HaroldDawson.MarsRover.Core.Models
{
    /// <summary>
    /// This class represents the rectangular Mars plateau.
    /// </summary>
    public class MarsPlateau
    {
        public int MinX = 0;
        public int MinY = 0;
        public int MaxX { get; }
        public int MaxY { get; }

        public MarsPlateau(int gridMaxX, int gridMaxY)
        {
            MaxX = gridMaxX;
            MaxY = gridMaxY;
        }

        /// <summary>
        /// Checks and returns true or false based on if the coordinate provided falls within the bounds of the plateau, with the boundaries themselves being inclusive.
        /// </summary>
        /// <param name="coordinate"></param>
        /// <returns></returns>
        public bool IsPointWithinPlateauBounds(MarsCoordinate coordinate)
        {
            if (coordinate == null)
            {
                return false;
            }

            return coordinate.X >= MinX 
                   && coordinate.X <= MaxX 
                   && coordinate.Y >= MinY 
                   && coordinate.Y <= MaxY;
        }
    }
}