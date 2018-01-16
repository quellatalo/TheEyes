using System;
using System.Drawing;
using TheEyes.ImageMatcher;

namespace Qellatalo.Nin.TheEyes
{
    /// <summary>
    /// Represents an image pattern.
    /// </summary>
    public class Pattern : IDisposable
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
        public Pattern(Bitmap bitmap, double threshold, ImageMatcher imageMatcher)
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
        public Pattern(string filename, ImageMatcher imageMatcher) : this(new Bitmap(filename), imageMatcher) { }
        /// <summary>
        /// Constructs a pattern.
        /// </summary>
        /// <param name="bitmap">Base image.</param>
        /// <param name="imageMatcher">Matching method.</param>
        public Pattern(Bitmap bitmap, ImageMatcher imageMatcher) : this(bitmap, DefaultThreshold, imageMatcher) { }
        /// <summary>
        /// Constructs a pattern.
        /// </summary>
        /// <param name="bitmap">Base image.</param>
        /// <param name="threshold">Similarity threshold.</param>
        public Pattern(Bitmap bitmap, double threshold) : this(bitmap, threshold, CcoeffNormed.Instance) { }
        /// <summary>
        /// Constructs a pattern.
        /// </summary>
        /// <param name="filename">Base image file path.</param>
        /// <param name="threshold">Similarity threshold.</param>
        public Pattern(string filename, double threshold) : this(new Bitmap(filename), threshold) { }
        /// <summary>
        /// Constructs a pattern, with default threshold.
        /// </summary>
        /// <param name="filename">Base image file path.</param>
        public Pattern(string filename) : this(filename, DefaultThreshold) { }
        /// <summary>
        /// Constructs a pattern, with default threshold.
        /// </summary>
        /// <param name="bitmap">Base image.</param>
        public Pattern(Bitmap bitmap) : this(bitmap, DefaultThreshold) { }
        /// <summary>
        /// Release image resource.
        /// </summary>
        public void Dispose()
        {
            Image.Dispose();
        }
    }
}
