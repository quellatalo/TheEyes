using System;
using System.Drawing;

namespace Qellatalo.Nin.TheEyes
{
    /// <summary>
    /// Represent a match in finding patterns.
    /// </summary>
    public class Match
    {
        /// <summary>
        /// The area of the match.
        /// </summary>
        public Area Area { get; private set; }
        /// <summary>
        /// Match similarity.
        /// </summary>
        public double Similarity { get; private set; }
        /// <summary>
        /// Constructs a match object.
        /// </summary>
        /// <param name="area"></param>
        /// <param name="similarity"></param>
        public Match(Area area, double similarity)
        {
            Area = area;
            Similarity = similarity;
        }
        /// <summary>
        /// Highlight matching area based on similarity.
        /// </summary>
        public void Highlight()
        {
            Area.Highlight(Similarity);
        }
        /// <summary>
        /// Highlight matching area based on similarity.
        /// </summary>
        /// <param name="color">Brush color.</param>
        public void Highlight(Color color)
        {
            Area.Highlight(Similarity, color);
        }
        /// <summary>
        /// Caption the match area with similarity.
        /// </summary>
        public void Caption()
        {
            Area.Caption(Similarity.ToString());
        }
    }
}
