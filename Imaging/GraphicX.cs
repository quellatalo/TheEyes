using Emgu.CV;
using Emgu.CV.OCR;
using Emgu.CV.Structure;
using Quellatalo.Nin.HOCRReader;
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
            Match max = pattern.Matcher.GetMax(image, pattern.Image);
            if (max.Similarity >= pattern.Threshold)
            {
                match = max;
            }
            return match;
        }

        /// <summary>
        /// Finds all occurences of a pattern in th area.
        /// </summary>
        /// <param name="image">Image to find in.</param>
        /// <param name="pattern">Pattern to find.</param>
        /// <returns>A list of Match objects.</returns>
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
        /// <returns>A list of Match objects.</returns>
        public List<Match> FindAll(Image<Bgr, byte> image, Pattern pattern)
        {
            return pattern.Matcher.GetMatches(image, pattern.Image, pattern.Threshold);
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
        /// Gets image HOCR from tesseract.
        /// </summary>
        /// <param name="image">The image to read.</param>
        /// <param name="tesseract">The tesseract instance.</param>
        /// <returns>HOCR instance.</returns>
        public HOCR GetHOCR(Image<Bgr, byte> image, Tesseract tesseract)
        {
            tesseract.SetImage(image);
            HOCR hOCR = new HOCR()
            {
                Data = tesseract.GetHOCRText()
            };
            return hOCR;
        }
        /// <summary>
        /// Gets image HOCR from tesseract.
        /// </summary>
        /// <param name="image">The image to read.</param>
        /// <param name="tesseract">The tesseract instance.</param>
        /// <returns>HOCR instance.</returns>
        public HOCR GetHOCR(Bitmap image, Tesseract tesseract)
        {
            using (Image<Bgr, byte> img = new Image<Bgr, byte>(image))
            {
                return GetHOCR(img, tesseract);
            }
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
