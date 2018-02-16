using Quellatalo.Nin.TheEyes.ImageMatcher;
using Quellatalo.Nin.TheEyes.Imaging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace Quellatalo.Nin.TheEyes
{
    /// <summary>
    /// Represents an area on screen.
    /// </summary>
    public class Area
    {
        /// <summary>
        /// Default wait time in milliseconds.
        /// </summary>
        public const int DEFAULT_WAITTIME_MILLISECONDS = 10000;

        /// <summary>
        /// Default hightlight color.
        /// </summary>
        public static readonly Color DEFAULT_HIGHLIGHT_COLOR = Color.DarkRed;

        /// <summary>
        /// Default front.
        /// </summary>
        public static readonly Font DEFAULT_FONT = SystemFonts.CaptionFont;

        /// <summary>
        /// TransparencyKey for highlight background.
        /// </summary>
        public static Color TransparencyKey { get { return TransparentForm.TransparentKey; } set { TransparentForm.TransparentKey = value; } }

        internal static TransparentForm highlightForm = new TransparentForm();

        /// <summary>
        /// The wait time (milliseconds) for image matching in this area.
        /// </summary>
        public int WaitTimeMilliseconds { get; set; }

        /// <summary>
        /// Highlight color to use in this area.
        /// </summary>
        public Color HighlightColor { get; set; }

        /// <summary>
        /// Font for text highlighting in this area.
        /// </summary>
        public Font HighlightFont { get; set; }

        /// <summary>
        /// Gets screen point from a point relative to the area's upper left.
        /// </summary>
        /// <param name="x">x relative to the area's upper left.</param>
        /// <param name="y">y relative to the area's upper left.</param>
        /// <returns>Point.</returns>
        public Point Offset(int x, int y)
        {
            Point point = Point.Empty;
            point.X = Rectangle.X + x;
            point.Y = Rectangle.Y + y;
            return point;
        }

        /// <summary>
        /// Gets screen point from a point relative to the area's upper left.
        /// </summary>
        /// <param name="point">Offset point.</param>
        /// <returns>Point.</returns>
        public Point Offset(Point point)
        {
            return Offset(point.X,point.Y);
        }

        /// <summary>
        /// Gets the area's center point.
        /// </summary>
        public Point Center { get; private set; }

        /// <summary>
        /// Gets the area's upper left point.
        /// </summary>
        public Point TopLeft { get; private set; }

        /// <summary>
        /// Gets the area's upper right point.
        /// </summary>
        public Point TopRight { get; private set; }

        /// <summary>
        /// Gets the area's bottom left point.
        /// </summary>
        public Point BottomLeft { get; private set; }

        /// <summary>
        /// Gets the area's bottom right point.
        /// </summary>
        public Point BottomRight { get; private set; }

        private Rectangle rectangle;

        /// <summary>
        /// Area's rectangle data.
        /// </summary>
        public Rectangle Rectangle
        {
            get { return rectangle; }
            set
            {
                rectangle = value;
                Center = new Point(rectangle.Left + rectangle.Width / 2, rectangle.Top + rectangle.Height / 2);
                TopLeft = rectangle.Location;
                TopRight = new Point(rectangle.Right, rectangle.Top);
                BottomLeft = new Point(rectangle.Left, rectangle.Bottom);
                BottomRight = new Point(rectangle.Right, rectangle.Bottom);
            }
        }

        /// <summary>
        /// Constructs a new Area.
        /// </summary>
        /// <param name="rectangle">Rectangle data.</param>
        public Area(Rectangle rectangle)
        {
            Rectangle = rectangle;
            WaitTimeMilliseconds = DEFAULT_WAITTIME_MILLISECONDS;
            HighlightFont = DEFAULT_FONT;
            HighlightColor = DEFAULT_HIGHLIGHT_COLOR;
        }

        /// <summary>
        /// Constructs a new Area.
        /// </summary>
        public Area() : this(SystemInformation.VirtualScreen.Location, SystemInformation.VirtualScreen.Size) { }

        /// <summary>
        /// Constructs a new Area.
        /// </summary>
        /// <param name="screen"></param>
        public Area(Screen screen) : this(screen.Bounds) { }

        /// <summary>
        /// Constructs a new Area.
        /// </summary>
        /// <param name="x">Rectangle upper left x.</param>
        /// <param name="y">Rectangle upper left y.</param>
        /// <param name="width">Rectangle width.</param>
        /// <param name="height">Rectangle height.</param>
        public Area(int x, int y, int width, int height) : this(new Rectangle(x, y, width, height)) { }

        /// <summary>
        /// Constructs a new Area.
        /// </summary>
        /// <param name="point">Upper left point.</param>
        /// <param name="size">Rectangle size.</param>
        public Area(Point point, Size size) : this(new Rectangle(point, size)) { }

        /// <summary>
        /// Constructs a new Area.
        /// </summary>
        /// <param name="point">Upper left point.</param>
        /// <param name="Width">Width.</param>
        /// <param name="Height">Height.</param>
        public Area(Point point, int Width, int Height) : this(point, new Size(Width, Height)) { }

        /// <summary>
        /// Constructs a new Area.
        /// </summary>
        /// <param name="x">Upper left point x.</param>
        /// <param name="y">Upper left point y.</param>
        /// <param name="size">Size.</param>
        public Area(int x, int y, Size size) : this(new Point(x, y), size) { }

        /// <summary>
        /// Current bitmap data of the area.
        /// </summary>
        public Bitmap GetDisplayingImage()
        {
            Bitmap bitmap = new Bitmap(Rectangle.Width, Rectangle.Height);
            using (Graphics g = Graphics.FromImage(bitmap))
            {
                g.CopyFromScreen(Rectangle.Location, SystemInformation.VirtualScreen.Location, Rectangle.Size);
            }
            return bitmap;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="rectangle"></param>
        /// <returns></returns>
        public Area SubArea(Rectangle rectangle)
        {
            return new Area(Offset(rectangle.Location), rectangle.Size);
        }

        /// <summary>
        /// Finds a pattern in th area.
        /// </summary>
        /// <param name="pattern">Pattern to find.</param>
        /// <returns>A Match object, or null if not found.</returns>
        public Match Find(Pattern pattern)
        {
            using (Bitmap display = GetDisplayingImage())
            {
                return GraphicX.Instance.Find(display, pattern);
            }
        }

        /// <summary>
        /// Finds all occurences of a pattern in th area.
        /// </summary>
        /// <param name="pattern">Pattern to find.</param>
        /// <returns>A list of Match.</returns>
        public List<Match> FindAll(Pattern pattern)
        {
            using (Bitmap display = GetDisplayingImage())
            {
                return GraphicX.Instance.FindAll(display, pattern);
            }
        }

        /// <summary>
        /// Highlights the border of area.
        /// </summary>
        public void Highlight()
        {
            using (Pen p = new Pen(HighlightColor))
            {
                Highlight(p);
            }
        }

        /// <summary>
        /// Highlights the whole area based on
        /// </summary>
        /// <param name="similarity"></param>
        public void Highlight(double similarity)
        {
            Highlight(similarity, HighlightColor);
        }

        /// <summary>
        /// Highlights the whole area based on
        /// </summary>
        /// <param name="similarity"></param>
        /// <param name="color">Brush color.</param>
        public void Highlight(double similarity, Color color)
        {
            using (SolidBrush brush = new SolidBrush(Color.FromArgb((int)(similarity * 127), color)))
            {
                Highlight(brush);
            }
        }

        /// <summary>
        /// Highlights the border of area.
        /// </summary>
        /// <param name="pen">Highlight pen.</param>
        public void Highlight(Pen pen)
        {
            highlightForm.Highlight(this, pen);
        }

        /// <summary>
        /// Captions the area.
        /// </summary>
        /// <param name="str">Caption.</param>
        /// <param name="font">Font.</param>
        /// <param name="brush">Brush</param>
        public void Caption(String str, Font font, Brush brush)
        {
            highlightForm.Caption(TopLeft, str, font, brush);
        }

        /// <summary>
        /// Captions the area.
        /// </summary>
        /// <param name="str">Caption</param>
        public void Caption(String str)
        {
            using (SolidBrush brush = new SolidBrush(HighlightColor))
            {
                Caption(str, HighlightFont, brush);
            }
        }

        /// <summary>
        /// Captions the area.
        /// </summary>
        /// <param name="str">Caption.</param>
        /// <param name="font">Font.</param>
        public void Caption(String str, Font font)
        {
            using (SolidBrush brush = new SolidBrush(HighlightColor))
            {
                Caption(str, font, brush);
            }
        }

        /// <summary>
        /// Captions the area.
        /// </summary>
        /// <param name="str">Caption.</param>
        /// <param name="brush">Brush.</param>
        public void Caption(String str, Brush brush)
        {
            Caption(str, HighlightFont, brush);
        }

        /// <summary>
        /// Highlights the area.
        /// </summary>
        /// <param name="brush">Brush.</param>
        public void Highlight(Brush brush)
        {
            highlightForm.Highlight(this, brush);
        }

        /// <summary>
        /// Clears all highlights
        /// </summary>
        public static void ClearHighlight()
        {
            highlightForm.Clear();
        }

        /// <summary>
        /// Waits for an occurence of a pattern.
        /// </summary>
        /// <param name="pattern">The pattern to wait for.</param>
        /// <param name="timeoutMilliseconds">Timeout limit.</param>
        /// <returns>A match if found, else null.</returns>
        public Match WaitFor(Pattern pattern, long timeoutMilliseconds)
        {
            Stopwatch watch = Stopwatch.StartNew();
            Match result = null;
            while (watch.ElapsedMilliseconds < timeoutMilliseconds && result == null)
            {
                result = Find(pattern);
            }
            return result;
        }

        /// <summary>
        /// Waits for an occurence of a pattern.
        /// </summary>
        /// <param name="pattern">The pattern to wait for.</param>
        /// <returns>A match if found, else null.</returns>
        public Match WaitFor(Pattern pattern)
        {
            return WaitFor(pattern, WaitTimeMilliseconds);
        }

        /// <summary>
        /// Waits for an amount of occurences of a pattern.
        /// </summary>
        /// <param name="pattern">The pattern to wait for.</param>
        /// <param name="count">The occurences to wait for.</param>
        /// <param name="timeoutMilliseconds">Timeout limit.</param>
        /// <returns>A list of Match(es) found.</returns>
        public List<Match> WaitFor(Pattern pattern, uint count, long timeoutMilliseconds)
        {
            Stopwatch watch = Stopwatch.StartNew();
            List<Match> result = new List<Match>();
            while (watch.ElapsedMilliseconds < timeoutMilliseconds && result.Count >= count)
            {
                result = FindAll(pattern);
            }
            return result;
        }

        /// <summary>
        /// Waits for an amount of occurences of a pattern.
        /// </summary>
        /// <param name="pattern">The pattern to wait for.</param>
        /// <param name="count">The occurences to wait for.</param>
        /// <returns>A list of Match(es) found.</returns>
        public List<Match> WaitFor(Pattern pattern, uint count)
        {
            return WaitFor(pattern, count, WaitTimeMilliseconds);
        }

        /// <summary>
        /// Waits for a pattern to vanish.
        /// </summary>
        /// <param name="pattern">The specified pattern.</param>
        /// <param name="timeoutMilliseconds">Wait timeout.</param>
        public void WaitVanish(Pattern pattern, long timeoutMilliseconds)
        {
            Stopwatch watch = Stopwatch.StartNew();
            Match match = Find(pattern);
            while (watch.ElapsedMilliseconds < timeoutMilliseconds && match != null)
            {
                match = Find(pattern);
            }
        }

        /// <summary>
        /// Waits for a pattern to vanish. With default wait time.
        /// </summary>
        /// <param name="pattern">The specified pattern.</param>
        public void WaitVanish(Pattern pattern)
        {
            WaitVanish(pattern, WaitTimeMilliseconds);
        }

        /// <summary>
        /// Waits for any of the provided patterns, returns when first is found
        /// </summary>
        /// <param name="patterns">Patterns to find.</param>
        /// <param name="timeoutMilliseconds">Timeout limit.</param>
        /// <returns>A Match.</returns>
        public Match WaitAny(Pattern[] patterns, long timeoutMilliseconds)
        {
            Match result = null;
            Stopwatch watch = Stopwatch.StartNew();
            while (watch.ElapsedMilliseconds < timeoutMilliseconds && result == null)
            {
                using (Bitmap display = GetDisplayingImage())
                {
                    foreach (Pattern pattern in patterns)
                    {
                        MinMax minMax = pattern.Matcher.GetMinMax(display, pattern.Image);
                        if (minMax.Max >= pattern.Threshold)
                        {
                            Rectangle rec = new Rectangle(Rectangle.X + minMax.MaxLoc.X, Rectangle.Y + minMax.MaxLoc.Y, pattern.Image.Size.Width, pattern.Image.Size.Height);
                            result = new Match(rec, minMax.Max);
                            break;
                        }
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// Waits for any of the provided patterns, returns when first is found
        /// </summary>
        /// <param name="patterns">Patterns to find.</param>
        /// <param name="timeoutMilliseconds">Timeout limit.</param>
        /// <returns>A Match.</returns>
        public Match WaitAny(List<Pattern> patterns, long timeoutMilliseconds)
        {
            return WaitAny(patterns.ToArray(), timeoutMilliseconds);
        }

        /// <summary>
        /// Waits for any of the provided patterns, returns when first is found
        /// </summary>
        /// <param name="patterns">Patterns to find.</param>
        /// <returns>A Match.</returns>
        public Match WaitAny(Pattern[] patterns)
        {
            return WaitAny(patterns, WaitTimeMilliseconds);
        }

        /// <summary>
        /// Waits for any of the provided patterns, returns when first is found
        /// </summary>
        /// <param name="patterns">Patterns to find.</param>
        /// <returns>A Match.</returns>
        public Match WaitAny(List<Pattern> patterns)
        {
            return WaitAny(patterns.ToArray(), WaitTimeMilliseconds);
        }
    }
}
