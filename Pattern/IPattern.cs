using Emgu.CV;
using Emgu.CV.Structure;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Quellatalo.Nin.TheEyes.Pattern
{
    /// <summary>
    /// Represent a pattern for template matching.
    /// </summary>
    public interface IPattern
    {
        /// <summary>
        /// Finds the match with highest similarity higher than the threshold.
        /// </summary>
        /// <param name="image"></param>
        /// <returns>A Match object.</returns>
        Match GetMax(Bitmap image);
        /// <summary>
        /// Finds all matches with similarity  higher than the threshold.
        /// </summary>
        /// <param name="image"></param>
        /// <returns>A Match object.</returns>
        List<Match> GetMatches(Bitmap image);
    }
}
