using Emgu.CV;
using Emgu.CV.Structure;
using System.Collections.Generic;
using System.Drawing;

namespace Quellatalo.Nin.TheEyes.Pattern.CV.Image.Matcher
{
    /// <summary>
    /// ImageMatcher abstraction.
    /// </summary>
    public abstract class ImageMatcher
    {
        /// <summary>
        /// Finds max value and location.
        /// </summary>
        /// <param name="contextImg">The context on which the search will do.</param>
        /// <param name="searchImg">The target to find in the context.</param>
        /// <returns>A Match object.</returns>
        public Match GetMax(Bitmap contextImg, Bitmap searchImg)
        {
            using
                (
                Image<Bgr, byte> image = new Image<Bgr, byte>(searchImg),
                reg = new Image<Bgr, byte>(contextImg)
                )
            {
                return GetMax(reg, image);
            }
        }

        /// <summary>
        /// Finds max value and location.
        /// </summary>
        /// <param name="contextImg">The context on which the search will do.</param>
        /// <param name="searchImg">The target to find in the context.</param>
        /// <returns>A Match object.</returns>
        public Match GetMax(Image<Bgr, byte> contextImg, Bitmap searchImg)
        {
            using (Image<Bgr, byte> image = new Image<Bgr, byte>(searchImg))
            {
                return GetMax(contextImg, image);
            }
        }

        /// <summary>
        /// Finds max value and location.
        /// </summary>
        /// <param name="contextImg">The context on which the search will do.</param>
        /// <param name="searchImg">The target to find in the context.</param>
        /// <returns>A Match object.</returns>
        public Match GetMax(Bitmap contextImg, Image<Bgr, byte> searchImg)
        {
            using (Image<Bgr, byte> reg = new Image<Bgr, byte>(contextImg))
            {
                return GetMax(reg, searchImg);
            }
        }

        /// <summary>
        /// Finds max value and location.
        /// </summary>
        /// <param name="contextImg">The context on which the search will do.</param>
        /// <param name="searchImg">The target to find in the context.</param>
        /// <returns>A Match object.</returns>
        public abstract Match GetMax(Image<Bgr, byte> contextImg, Image<Bgr, byte> searchImg);

        /// <summary>
        /// Find all the matches above or equal the threshold.
        /// </summary>
        /// <param name="contextImg">The context on which the search will do.</param>
        /// <param name="searchImg">The target to find in the context.</param>
        /// <param name="threshold">Similarity threshold.</param>
        /// <returns>A List of Match objects.</returns>
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
        /// <returns>A List of Match objects.</returns>
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
        /// <returns>A List of Match objects.</returns>
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
        /// <returns>A List of Match objects.</returns>
        public abstract List<Match> GetMatches(Image<Bgr, byte> contextImg, Image<Bgr, byte> searchImg, double threshold);
    }
}
