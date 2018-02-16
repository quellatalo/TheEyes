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
        /// The area of the match.
        /// </summary>
        [Obsolete("Area property for Match will be removed in future releases. Please use Rectangle instead.")]
        public Area Area { get; private set; }
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
        /// <param name="area">The area of the match.</param>
        /// <param name="similarity">Match similarity.</param>
        [Obsolete("Area property for Match will be removed in future releases. Please use Rectangle instead.")]
        public Match(Area area, double similarity)
        {
            Area = area;
            Similarity = similarity;
        }
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
        /// <summary>
        /// Highlight matching area based on similarity.
        /// </summary>
        [Obsolete("Match highlight will be removed in future releases. Please highlight from Area instead.")]
        public void Highlight()
        {
            if (Area == null)
            {
                
            }
            else
            {
                Area.Highlight(Similarity);
            }
        }
        /// <summary>
        /// Highlight matching area based on similarity.
        /// </summary>
        /// <param name="color">Brush color.</param>
        [Obsolete("Match highlight will be removed in future releases. Please highlight from Area instead.")]
        public void Highlight(Color color)
        {
            Area.Highlight(Similarity, color);
        }
        /// <summary>
        /// Caption the match area with similarity.
        /// </summary>
        [Obsolete("Match caption will be removed in future releases. Please caption from Area instead.")]
        public void Caption()
        {
            Area.Caption(Similarity.ToString());
        }
    }
}
