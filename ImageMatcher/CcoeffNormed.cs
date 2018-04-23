﻿using Emgu.CV;
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
        private static ImageMatcher instance;

        /// <summary>
        /// Get an instance of ImageMatcher.
        /// </summary>
        public static ImageMatcher Instance
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
        /// <returns>A MinMax object.</returns>
        public override MinMax GetMinMax(Image<Bgr, byte> contextImg, Image<Bgr, byte> searchImg)
        {
            MinMax minMax = new MinMax();
            contextImg.MatchTemplate(searchImg, TemplateMatchingType.CcoeffNormed).MinMax(out double[] min, out double[] max, out Point[] minLoc, out Point[] maxLoc);
            minMax.Min = min[0];
            minMax.Max = max[0];
            minMax.MinLoc = minLoc[0];
            minMax.MaxLoc = maxLoc[0];
            return minMax;
        }

        /// <summary>
        /// Find all the matches above or equal the threshold.
        /// </summary>
        /// <param name="contextImg">The context on which the search will do.</param>
        /// <param name="searchImg">The target to find in the context.</param>
        /// <param name="threshold">Similarity threshold.</param>
        /// <returns>A MinMax object.</returns>
        public override List<Match> GetMatches(Image<Bgr, byte> contextImg, Image<Bgr, byte> searchImg, double threshold)
        {
            List<Match> rs = new List<Match>();
            Image<Gray, float> matchTemplate = contextImg.MatchTemplate(searchImg, TemplateMatchingType.CcoeffNormed);
            for (int row = 0; row < matchTemplate.Rows; row++)
            {
                for (int col = 0; col < matchTemplate.Cols; col++)
                {
                    if (matchTemplate[row, col].Intensity >= threshold)
                    {
                        rs.Add(new Match(new Rectangle(new Point(col, row), searchImg.Size), matchTemplate[row, col].Intensity));
                    }
                }
            }
            return rs;
        }
    }
}
