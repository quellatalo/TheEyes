using Quellatalo.Nin.TheEyes.Pattern.CV.Image.Matcher;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace Quellatalo.Nin.TheEyes.Pattern.CV.Image
{
    /// <summary>
    /// Represents an image pattern.
    /// </summary>
    public class ImagePattern : IPattern, IDisposable
    {
        /// <summary>
        /// Default pattern similarity threshold.
        /// </summary>
        public static double DefaultThreshold { get; set; } = 0.75;
        /// <summary>
        /// Matching mediator.
        /// </summary>
        public ImageMatcher Matcher { get; set; }
        /// <summary>
        /// Base image.
        /// </summary>
        public Bitmap Image { get; set; }
        /// <summary>
        /// Similarity threshold.
        /// </summary>
        public double Threshold { get; set; }
        /// <summary>
        /// Constructs a pattern.
        /// </summary>
        /// <param name="bitmap">Base image.</param>
        /// <param name="threshold">Similarity threshold.</param>
        /// <param name="imageMatcher">Matching method.</param>
        public ImagePattern(Bitmap bitmap, double threshold, ImageMatcher imageMatcher)
        {
            Image = bitmap;
            Threshold = threshold;
            Matcher = imageMatcher;
        }
        /// <summary>
        /// Constructs a pattern.
        /// </summary>
        /// <param name="filename">Base image file path.</param>
        /// <param name="imageMatcher">Matching method.</param>
        public ImagePattern(string filename, ImageMatcher imageMatcher) : this(new Bitmap(filename), imageMatcher) { }
        /// <summary>
        /// Constructs a pattern.
        /// </summary>
        /// <param name="bitmap">Base image.</param>
        /// <param name="imageMatcher">Matching method.</param>
        public ImagePattern(Bitmap bitmap, ImageMatcher imageMatcher) : this(bitmap, DefaultThreshold, imageMatcher) { }
        /// <summary>
        /// Constructs a pattern.
        /// </summary>
        /// <param name="bitmap">Base image.</param>
        /// <param name="threshold">Similarity threshold.</param>
        public ImagePattern(Bitmap bitmap, double threshold) : this(bitmap, threshold, CcoeffNormed.Instance) { }
        /// <summary>
        /// Constructs a pattern.
        /// </summary>
        /// <param name="filename">Base image file path.</param>
        /// <param name="threshold">Similarity threshold.</param>
        public ImagePattern(string filename, double threshold) : this(new Bitmap(filename), threshold) { }
        /// <summary>
        /// Constructs a pattern, with default threshold.
        /// </summary>
        /// <param name="filename">Base image file path.</param>
        public ImagePattern(string filename) : this(filename, DefaultThreshold) { }
        /// <summary>
        /// Constructs a pattern, with default threshold.
        /// </summary>
        /// <param name="bitmap">Base image.</param>
        public ImagePattern(Bitmap bitmap) : this(bitmap, DefaultThreshold) { }
        /// <summary>
        /// Release image resource.
        /// </summary>
        public void Dispose()
        {
            Image.Dispose();
        }
        /// <summary>
        /// Finds the match with highest similarity higher than the threshold.
        /// </summary>
        /// <param name="image"></param>
        /// <returns>A Match object.</returns>
        public Match GetMax(Bitmap image)
        {
            Match match = Matcher.GetMax(image, Image);
            if (match.Similarity < Threshold) match = null;
            return match;
        }
        /// <summary>
        /// Finds all matches with similarity  higher than the threshold.
        /// </summary>
        /// <param name="image"></param>
        /// <returns>A Match object.</returns>
        public List<Match> GetMatches(Bitmap image)
        {
            return Matcher.GetMatches(image, Image, Threshold);
        }
    }
}
