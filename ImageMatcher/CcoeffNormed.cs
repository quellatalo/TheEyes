using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using System.Collections.Generic;
using System.Drawing;

namespace Quellatalo.Nin.TheEyes.ImageMatcher
{
    /// <summary>
    /// OpenCV CcoeffNormed matching system.
    /// </summary>
    public class CcoeffNormed : ImageMatcher
    {
        private static CcoeffNormed instance;

        /// <summary>
        /// Get an instance of ImageMatcher.
        /// </summary>
        public static CcoeffNormed Instance
        {
            get
            {
                if (instance == null) instance = new CcoeffNormed();
                return instance;
            }
            private set { instance = value; }
        }

        private CcoeffNormed() { }

        /// <summary>
        /// Find min and max values and locations.
        /// </summary>
        /// <param name="contextImg">The context on which the search will do.</param>
        /// <param name="searchImg">The target to find in the context.</param>
        /// <returns>A Match object.</returns>
        public override Match GetMax(Image<Bgr, byte> contextImg, Image<Bgr, byte> searchImg)
        {
            Match result = null;
            using (Image<Gray, float> matchTemplate = contextImg.MatchTemplate(searchImg, TemplateMatchingType.CcoeffNormed))
            {
                matchTemplate.MinMax(out double[] min, out double[] max, out Point[] minP, out Point[] maxP);
                result = new Match(new Rectangle(maxP[0], searchImg.Size), max[0]);
            }
            return result;
        }

        /// <summary>
        /// Find all the matches above or equal the threshold.
        /// </summary>
        /// <param name="contextImg">The context on which the search will do.</param>
        /// <param name="searchImg">The target to find in the context.</param>
        /// <param name="threshold">Similarity threshold.</param>
        /// <returns>A List of Match objects.</returns>
        public override List<Match> GetMatches(Image<Bgr, byte> contextImg, Image<Bgr, byte> searchImg, double threshold)
        {
            List<Match> rs = new List<Match>();
            using (Image<Gray, float> matchTemplate = contextImg.MatchTemplate(searchImg, TemplateMatchingType.CcoeffNormed))
            {
                for (int row = 0; row < matchTemplate.Rows; row++)
                {
                    for (int col = 0; col < matchTemplate.Cols; col++)
                    {
                        if (matchTemplate[row, col].Intensity >= threshold)
                        {
                            Rectangle foundRect = new Rectangle(col, row, searchImg.Width, searchImg.Height);
                            using (Image<Gray, float> found = matchTemplate.Copy(foundRect))
                            {
                                found.MinMax(out double[] min, out double[] max, out Point[] minPos, out Point[] maxPos);
                                rs.Add(new Match(new Rectangle(col + maxPos[0].X, row + maxPos[0].Y, searchImg.Width, searchImg.Height), max[0]));
                                col += maxPos[0].X + searchImg.Width - 1;
                            }
                            matchTemplate.GetSubRect(foundRect).SetZero();
                        }
                    }
                }
            }
            return rs;
        }
    }
}
