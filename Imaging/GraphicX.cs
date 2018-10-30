using Quellatalo.Nin.TheEyes.Pattern;
using System.Collections.Generic;
using System.Drawing;

namespace Quellatalo.Nin.TheEyes.Imaging
{
    /// <summary>
    /// Operator of Find, highlight, and caption in images.
    /// </summary>
    public class GraphicX
    {
        private static GraphicX instance;
        /// <summary>
        /// Gets GraphicX instance.
        /// </summary>
        public static GraphicX Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new GraphicX();
                }

                return instance;
            }
        }
        /// <summary>
        /// Finds a pattern in an image.
        /// </summary>
        /// <param name="image">Image to find in.</param>
        /// <param name="pattern">Pattern to find.</param>
        /// <returns>A Match object, or null if not found.</returns>
        public Match Find(Bitmap image, IPattern pattern)
        {
            return pattern.GetMax(image);
        }

        /// <summary>
        /// Finds all occurences of a pattern in th area.
        /// </summary>
        /// <param name="image">Image to find in.</param>
        /// <param name="pattern">Pattern to find.</param>
        /// <returns>A list of Match objects.</returns>
        public List<Match> FindAll(Bitmap image, IPattern pattern)
        {
            return pattern.GetMatches(image);
        }
        /// <summary>
        /// Highlight a rectangle in provided graphics.
        /// </summary>
        /// <param name="g">The graphics to be highlighted on.</param>
        /// <param name="rectangle">The rectangle to be highlighted.</param>
        /// <param name="pen">Highlight pen.</param>
        public void Highlight(Graphics g, Rectangle rectangle, Pen pen)
        {
            g.DrawRectangle(pen, rectangle);
        }

        /// <summary>
        /// Highlight a rectangle in provided graphics.
        /// </summary>
        /// <param name="g">The graphics to be highlighted on.</param>
        /// <param name="rectangle">The rectangle to be highlighted.</param>
        /// <param name="brush">Highlight brush.</param>
        public void Highlight(Graphics g, Rectangle rectangle, Brush brush)
        {
            g.FillRectangle(brush, rectangle);
        }

        /// <summary>
        /// Add caption in provided graphics.
        /// </summary>
        /// <param name="g">The graphics to be highlighted on.</param>
        /// <param name="loc">Caption location in the graphics.</param>
        /// <param name="str">Caption text.</param>
        /// <param name="font">Caption font.</param>
        /// <param name="brush">Caption brush.</param>
        public void Caption(Graphics g, Point loc, string str, Font font, Brush brush)
        {
            g.DrawString(str, font, brush, loc);
        }
    }
}
