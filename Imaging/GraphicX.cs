using Emgu.CV;
using Emgu.CV.OCR;
using Emgu.CV.Structure;
using Quellatalo.Nin.HOCRReader;
using Quellatalo.Nin.TheEyes.ImageMatcher;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace Quellatalo.Nin.TheEyes.Imaging
{
    /// <summary>
    /// Operator of Find, highlight, and caption in images.
    /// </summary>
    public class GraphicX
    {
        private static GraphicX instance;
        /// <summary>
        /// Gets GraphicX instance.
        /// </summary>
        public static GraphicX Instance
        {
            get
            {
                if (instance == null) instance = new GraphicX();
                return instance;
            }
        }
        /// <summary>
        /// Finds a pattern in an image.
        /// </summary>
        /// <param name="image">Image to find in.</param>
        /// <param name="pattern">Pattern to find.</param>
        /// <returns>A Match object, or null if not found.</returns>
        public Match Find(Bitmap image, Pattern pattern)
        {
            using (Image<Bgr, byte> img = new Image<Bgr, byte>(image))
            {
                return Find(img, pattern);
            }
        }

        /// <summary>
        /// Finds a pattern in an image.
        /// </summary>
        /// <param name="image">Image to find in.</param>
        /// <param name="pattern">Pattern to find.</param>
        /// <returns>A Match object, or null if not found.</returns>
        public Match Find(Image<Bgr, byte> image, Pattern pattern)
        {
            Match match = null;
            MinMax minMax = pattern.Matcher.GetMinMax(image, pattern.Image);
            if (minMax.Max >= pattern.Threshold)
            {
                Rectangle ma = new Rectangle(minMax.MaxLoc.X, minMax.MaxLoc.Y, pattern.Image.Size.Width, pattern.Image.Height);
                match = new Match(ma, minMax.Max);
            }
            return match;
        }

        /// <summary>
        /// Finds all occurences of a pattern in th area.
        /// </summary>
        /// <param name="image">Image to find in.</param>
        /// <param name="pattern">Pattern to find.</param>
        /// <returns>A list of Match.</returns>
        public List<Match> FindAll(Bitmap image, Pattern pattern)
        {
            using (Image<Bgr, byte> reg = new Image<Bgr, byte>(image))
            {
                return FindAll(reg, pattern);
            }
        }

        /// <summary>
        /// Finds all occurences of a pattern in th area.
        /// </summary>
        /// <param name="image">Image to find in.</param>
        /// <param name="pattern">Pattern to find.</param>
        /// <returns>A list of Match.</returns>
        public List<Match> FindAll(Image<Bgr, byte> image, Pattern pattern)
        {
            List<Match> result = new List<Match>();
            using (Image<Bgr, byte> img = new Image<Bgr, byte>(pattern.Image))
            {
                MinMax gMinMax = pattern.Matcher.GetMinMax(image, img);
                Image<Bgr, byte> different;
                if (gMinMax.Min < pattern.Threshold)
                {
                    different = image.GetSubRect(new Rectangle(gMinMax.MinLoc.X, gMinMax.MinLoc.Y, img.Size.Width, img.Size.Height)).Copy();
                }
                else
                {
                    Point[] pts =
                    {
                        Point.Empty,
                        new Point(0, img.Width),
                        new Point(img.Width, img.Height),
                        new Point(img.Height, 0)
                    };
                    different = new Image<Bgr, byte>(img.Size);
                    Bgr fillColor1 = new Bgr(Color.White);
                    different.FillConvexPoly(pts, fillColor1);
                    MinMax iMinMax = pattern.Matcher.GetMinMax(different, img);
                    if (iMinMax.Max >= pattern.Threshold)
                    {
                        Point[] ulPts = { pts[0], pts[1], pts[3] };
                        different.FillConvexPoly(ulPts, fillColor1);
                        Point[] brPts = { pts[1], pts[2], pts[3] };
                        different.FillConvexPoly(brPts, new Bgr(Color.Black));
                        iMinMax = pattern.Matcher.GetMinMax(different, img);
                        if (iMinMax.Max >= pattern.Threshold)
                        {
                            throw new InvalidPatternException("Invalid pattern image.");
                        }
                    }
                }
                while (gMinMax.Max >= pattern.Threshold)
                {
                    Rectangle ma = new Rectangle(gMinMax.MaxLoc.X, gMinMax.MaxLoc.Y, img.Size.Width, img.Size.Height);
                    Match match = new Match(ma, gMinMax.Max);
                    result.Add(match);
                    different.CopyTo(image.GetSubRect(new Rectangle(gMinMax.MaxLoc, different.Size)));
                    gMinMax = pattern.Matcher.GetMinMax(image, img);
                }
                different.Dispose();
            }
            return result;
        }
        /// <summary>
        /// Highlight a rectangle in provided graphics.
        /// </summary>
        /// <param name="g">The graphics to be highlighted on.</param>
        /// <param name="rectangle">The rectangle to be highlighted.</param>
        /// <param name="pen">Highlight pen.</param>
        public void Highlight(Graphics g, Rectangle rectangle, Pen pen)
        {
            g.DrawRectangle(pen, rectangle);
        }

        /// <summary>
        /// Highlight a rectangle in provided graphics.
        /// </summary>
        /// <param name="g">The graphics to be highlighted on.</param>
        /// <param name="rectangle">The rectangle to be highlighted.</param>
        /// <param name="brush">Highlight brush.</param>
        public void Highlight(Graphics g, Rectangle rectangle, Brush brush)
        {
            g.FillRectangle(brush, rectangle);
        }

        /// <summary>
        /// Add caption in provided graphics.
        /// </summary>
        /// <param name="g">The graphics to be highlighted on.</param>
        /// <param name="loc">Caption location in the graphics.</param>
        /// <param name="str">Caption text.</param>
        /// <param name="font">Caption font.</param>
        /// <param name="brush">Caption brush.</param>
        public void Caption(Graphics g, Point loc, String str, Font font, Brush brush)
        {
            g.DrawString(str, font, brush, loc);
        }
        /// <summary>
        /// Find all lines which contains/match a specified text.
        /// </summary>
        /// <param name="text">The text to find.</param>
        /// /// <param name="image">The image to find in.</param>
        /// /// <param name="tesseract">The tesseract instance.</param>
        /// <param name="searchOption">Whether the line contains the text, or match the text.</param>
        /// <returns>A list of OCRLine.</returns>
        public List<OCRLine> FindAllText(string text, Image<Bgr, byte> image, Tesseract tesseract, SearchOptions searchOption = SearchOptions.Containing)
        {
            tesseract.SetImage(image);
            HOCR hOCR = new HOCR()
            {
                Data = tesseract.GetHOCRText()
            };
            return hOCR.FindAllText(text);
        }
        /// <summary>
        /// Find all lines which contains/match a specified text.
        /// </summary>
        /// <param name="text">The text to find.</param>
        /// /// <param name="image">The image to find in.</param>
        /// /// <param name="tesseract">The tesseract instance.</param>
        /// <param name="searchOption">Whether the line contains the text, or match the text.</param>
        /// <returns>A list of OCRLine.</returns>
        public List<OCRLine> FindAllText(string text, Bitmap image, Tesseract tesseract, SearchOptions searchOption = SearchOptions.Containing)
        {
            using (Image<Bgr, byte> img = new Image<Bgr, byte>(image))
            {
                return FindAllText(text, img, tesseract, searchOption);
            }
        }
        /// <summary>
        /// Find the first line which contains/match a specified text.
        /// </summary>
        /// <param name="text">The text to find.</param>
        /// /// <param name="image">The image to find in.</param>
        /// /// <param name="tesseract">The tesseract instance.</param>
        /// <param name="searchOption">Whether the line contains the text, or match the text.</param>
        /// <returns>An instance of OCRLine.</returns>
        public OCRLine FindText(string text, Image<Bgr, byte> image, Tesseract tesseract, SearchOptions searchOption = SearchOptions.Containing)
        {
            tesseract.SetImage(image);
            HOCR hOCR = new HOCR()
            {
                Data = tesseract.GetHOCRText()
            };
            return hOCR.FindText(text);
        }
        /// <summary>
        /// Find the first line which contains/match a specified text.
        /// </summary>
        /// <param name="text">The text to find.</param>
        /// /// <param name="image">The image to find in.</param>
        /// /// <param name="tesseract">The tesseract instance.</param>
        /// <param name="searchOption">Whether the line contains the text, or match the text.</param>
        /// <returns>An instance of OCRLine.</returns>
        public OCRLine FindText(string text, Bitmap image, Tesseract tesseract, SearchOptions searchOption = SearchOptions.Containing)
        {
            using (Image<Bgr, byte> img = new Image<Bgr, byte>(image))
            {
                return FindText(text, img, tesseract, searchOption);
            }
        }
    }
}
