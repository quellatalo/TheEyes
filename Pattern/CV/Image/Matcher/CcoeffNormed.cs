using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using System.Collections.Generic;
using System.Drawing;

namespace Quellatalo.Nin.TheEyes.Pattern.CV.Image.Matcher
{
    /// <summary>
    /// OpenCV CcoeffNormed matching system.
    /// </summary>
    public class CcoeffNormed : ImageMatcher
    {
        private static CcoeffNormed? _instance;

        /// <summary>
        /// Get the instance of CcoeffNormed.
        /// </summary>
        public static CcoeffNormed Instance => _instance ??= new CcoeffNormed();

        private CcoeffNormed() { }

        /// <summary>
        /// Finds the match with highest similarity.
        /// </summary>
        /// <param name="contextImg">The context on which the search will do.</param>
        /// <param name="searchImg">The target to find in the context.</param>
        /// <returns>A Match object.</returns>
        public override Match GetMax(Image<Bgr, byte> contextImg, Image<Bgr, byte> searchImg)
        {
            Match result;
            using Image<Gray, float> matchTemplate = contextImg.MatchTemplate(searchImg, TemplateMatchingType.CcoeffNormed);
            matchTemplate.MinMax(out double[] min, out double[] max, out Point[] minP, out Point[] maxP);
            result = new Match(new Rectangle(maxP[0], searchImg.Size), max[0]);
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
            int halfWidth = searchImg.Width / 2;
            int halfHeight = searchImg.Height / 2;
            int foundX, foundY, foundWidth, foundHeight;
            List<Match> rs = new List<Match>();
            using (Image<Gray, float> matchTemplate = contextImg.MatchTemplate(searchImg, TemplateMatchingType.CcoeffNormed))
            {
                matchTemplate.MinMax(out double[] min, out double[] max, out Point[] minPos, out Point[] maxPos);
                while (max[0] >= threshold)
                {
                    rs.Add(new Match(new Rectangle(maxPos[0].X, maxPos[0].Y, searchImg.Width, searchImg.Height), max[0]));
                    foundX = maxPos[0].X - halfWidth;
                    foundY = maxPos[0].Y - halfHeight;
                    foundWidth = searchImg.Width;
                    foundHeight = searchImg.Height;
                    if (foundX < 0)
                    {
                        foundWidth += foundX;
                        foundX = 0;
                    }
                    if (foundY < 0)
                    {
                        foundHeight += foundY;
                        foundY = 0;
                    }
                    matchTemplate.GetSubRect(new Rectangle(foundX, foundY, foundWidth, foundHeight)).SetZero();
                    matchTemplate.MinMax(out min, out max, out minPos, out maxPos);
                }
            }
            return rs;
        }
    }
}
