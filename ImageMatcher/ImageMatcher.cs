using Emgu.CV;
using Emgu.CV.Structure;
using System.Drawing;

namespace TheEyes.ImageMatcher
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
    }
}
