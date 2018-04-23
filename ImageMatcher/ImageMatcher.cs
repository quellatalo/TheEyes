using Emgu.CV;
using Emgu.CV.Structure;
using System.Collections.Generic;
using System.Drawing;

namespace Quellatalo.Nin.TheEyes.ImageMatcher
{
    /// <summary>
    /// ImageMatcher abstraction.
    /// </summary>
    public abstract class ImageMatcher
    {
        /// <summary>
        /// Find min and max values and locations.
        /// </summary>
        /// <param name="contextImg">The context on which the search will do.</param>
        /// <param name="searchImg">The target to find in the context.</param>
        /// <returns>A MinMax object.</returns>
        public MinMax GetMinMax(Bitmap contextImg, Bitmap searchImg)
        {
            using
                (
                Image<Bgr, byte> image = new Image<Bgr, byte>(searchImg),
                reg = new Image<Bgr, byte>(contextImg)
                )
            {
                return GetMinMax(reg, image);
            }
        }

        /// <summary>
        /// Find min and max values and locations.
        /// </summary>
        /// <param name="contextImg">The context on which the search will do.</param>
        /// <param name="searchImg">The target to find in the context.</param>
        /// <returns>A MinMax object.</returns>
        public MinMax GetMinMax(Image<Bgr, byte> contextImg, Bitmap searchImg)
        {
            using (Image<Bgr, byte> image = new Image<Bgr, byte>(searchImg))
            {
                return GetMinMax(contextImg, image);
            }
        }

        /// <summary>
        /// Find min and max values and locations.
        /// </summary>
        /// <param name="contextImg">The context on which the search will do.</param>
        /// <param name="searchImg">The target to find in the context.</param>
        /// <returns>A MinMax object.</returns>
        public MinMax GetMinMax(Bitmap contextImg, Image<Bgr, byte> searchImg)
        {
            using (Image<Bgr, byte> reg = new Image<Bgr, byte>(contextImg))
            {
                return GetMinMax(reg, searchImg);
            }
        }

        /// <summary>
        /// Find min and max values and locations.
        /// </summary>
        /// <param name="contextImg">The context on which the search will do.</param>
        /// <param name="searchImg">The target to find in the context.</param>
        /// <returns>A MinMax object.</returns>
        public abstract MinMax GetMinMax(Image<Bgr, byte> contextImg, Image<Bgr, byte> searchImg);

        /// <summary>
        /// Find all the matches above or equal the threshold.
        /// </summary>
        /// <param name="contextImg">The context on which the search will do.</param>
        /// <param name="searchImg">The target to find in the context.</param>
        /// <param name="threshold">Similarity threshold.</param>
        /// <returns>A MinMax object.</returns>
        public List<Match> GetMatches(Bitmap contextImg, Bitmap searchImg, double threshold)
        {
            using
                (
                Image<Bgr, byte> image = new Image<Bgr, byte>(searchImg),
                reg = new Image<Bgr, byte>(contextImg)
                )
            {
                return GetMatches(reg, image, threshold);
            }
        }

        /// <summary>
        /// Find all the matches above or equal the threshold.
        /// </summary>
        /// <param name="contextImg">The context on which the search will do.</param>
        /// <param name="searchImg">The target to find in the context.</param>
        /// <param name="threshold">Similarity threshold.</param>
        /// <returns>A MinMax object.</returns>
        public List<Match> GetMatches(Image<Bgr, byte> contextImg, Bitmap searchImg, double threshold)
        {
            using (Image<Bgr, byte> image = new Image<Bgr, byte>(searchImg))
            {
                return GetMatches(contextImg, image, threshold);
            }
        }

        /// <summary>
        /// Find all the matches above or equal the threshold.
        /// </summary>
        /// <param name="contextImg">The context on which the search will do.</param>
        /// <param name="searchImg">The target to find in the context.</param>
        /// <param name="threshold">Similarity threshold.</param>
        /// <returns>A MinMax object.</returns>
        public List<Match> GetMatches(Bitmap contextImg, Image<Bgr, byte> searchImg, double threshold)
        {
            using (Image<Bgr, byte> reg = new Image<Bgr, byte>(contextImg))
            {
                return GetMatches(reg, searchImg, threshold);
            }
        }

        /// <summary>
        /// Find all the matches above or equal the threshold.
        /// </summary>
        /// <param name="contextImg">The context on which the search will do.</param>
        /// <param name="searchImg">The target to find in the context.</param>
        /// <param name="threshold">Similarity threshold.</param>
        /// <returns>A MinMax object.</returns>
        public abstract List<Match> GetMatches(Image<Bgr, byte> contextImg, Image<Bgr, byte> searchImg, double threshold);
    }
}
