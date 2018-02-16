using System;
using System.Drawing;

namespace Quellatalo.Nin.TheEyes
{
    /// <summary>
    /// Represent a match in finding patterns.
    /// </summary>
    public class Match
    {
        /// <summary>
        /// The rectangle of the match.
        /// </summary>
        public Rectangle Rectangle { get; set; }
        /// <summary>
        /// Match similarity.
        /// </summary>
        public double Similarity { get; private set; }
        /// <summary>
        /// Constructs a match object.
        /// </summary>
        /// <param name="rectangle">The rectangle of the match.</param>
        /// <param name="similarity">Match similarity.</param>
        public Match(Rectangle rectangle, double similarity)
        {
            Rectangle = rectangle;
            Similarity = similarity;
        }
    }
}
