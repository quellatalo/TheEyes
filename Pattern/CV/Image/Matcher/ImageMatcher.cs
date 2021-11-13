using Emgu.CV;
using Emgu.CV.Structure;

namespace Quellatalo.Nin.TheEyes.Pattern.CV.Image.Matcher
{
    /// <summary>
    /// ImageMatcher abstraction.
    /// </summary>
    public abstract class ImageMatcher
    {
        /// <summary>
        /// Finds the match with highest similarity.
        /// </summary>
        /// <param name="contextImg">The context on which the search will do.</param>
        /// <param name="searchImg">The target to find in the context.</param>
        /// <returns>A Match object.</returns>
        public Match GetMax(Bitmap contextImg, Bitmap searchImg)
        {
            using Image<Bgr, byte> image = searchImg.ToImage<Bgr, byte>(),
                reg = contextImg.ToImage<Bgr, byte>();
            return GetMax(reg, image);
        }

        /// <summary>
        /// Finds the match with highest similarity.
        /// </summary>
        /// <param name="contextImg">The context on which the search will do.</param>
        /// <param name="searchImg">The target to find in the context.</param>
        /// <returns>A Match object.</returns>
        public Match GetMax(Image<Bgr, byte> contextImg, Bitmap searchImg)
        {
            using var image = searchImg.ToImage<Bgr, byte>();
            return GetMax(contextImg, image);
        }

        /// <summary>
        /// Finds the match with highest similarity.
        /// </summary>
        /// <param name="contextImg">The context on which the search will do.</param>
        /// <param name="searchImg">The target to find in the context.</param>
        /// <returns>A Match object.</returns>
        public Match GetMax(Bitmap contextImg, Image<Bgr, byte> searchImg)
        {
            using var reg = contextImg.ToImage<Bgr, byte>();
            return GetMax(reg, searchImg);
        }

        /// <summary>
        /// Finds the match with highest similarity.
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
            using Image<Bgr, byte> image = searchImg.ToImage<Bgr, byte>(),
                reg = contextImg.ToImage<Bgr, byte>();
            return GetMatches(reg, image, threshold);
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
            using var image = searchImg.ToImage<Bgr, byte>();
            return GetMatches(contextImg, image, threshold);
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
            using var reg = contextImg.ToImage<Bgr, byte>();
            return GetMatches(reg, searchImg, threshold);
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
