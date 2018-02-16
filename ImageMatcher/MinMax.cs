using System.Drawing;

namespace Quellatalo.Nin.TheEyes.ImageMatcher
{
    /// <summary>
    /// Contains the values of a minmax search.
    /// </summary>
    public class MinMax
    {
        /// <summary>
        /// Minimum matching value.
        /// </summary>
        public double Min { get; set; }
        /// <summary>
        /// Maximum matching value.
        /// </summary>
        public double Max { get; set; }
        /// <summary>
        /// Minimum matching location.
        /// </summary>
        public Point MinLoc { get; set; }
        /// <summary>
        /// Maximum matching location.
        /// </summary>
        public Point MaxLoc { get; set; }
    }
}
