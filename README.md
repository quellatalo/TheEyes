# TheEyes

This TheEyes library provides methods to read and find images on the screen. A part of alternative tools to [sikulix](http://sikulix.com/).

_This library was built and tested on Windows 10 (64bit) with .NET Framework 3.5._

### Installation

The package is available on Nuget: [Quellatalo.Nin.TheEyes](https://www.nuget.org/packages/Quellatalo.Nin.TheEyes/)

## Example code
_(Working together with [TheHands](https://github.com/quellatalo/TheHands) library)_
```cs
/// <summary>
/// A test to have TheHands and TheEyes working together
/// Preparation: Open Microsoft Paint, with full canvas ready
/// This code will:
/// - focus on Paint, demonstrate the recognition
/// - draw something on canvas
/// - duplicate the drawing
/// - once again find all similar drawings on the canvas
/// </summary>
void HandsAndEyesTest()
{
    // Look for a window which has "paint" in title and bring it to front
    App app = App.GetAppByWindowTitle("paint");
    app.ToFront();

    using (Brush brush = new SolidBrush(Color.FromArgb(63, Color.Red)))
    {
        // Highlight the Paint's window area
        Area area = new Area(app.GetMainWindowRectangle());
        area.Highlight(brush);
        Thread.Sleep(2222); // wait, just to observer
        Area.ClearHighlight(); // clear the highlight

        // Prepare some points to draw in sequence
        Point[] points1 = new Point[] // 1st stroke
        {
        new Point(area.Center.X-10,area.Center.Y-10),
        new Point(area.Center.X+10,area.Center.Y-10),
        new Point(area.Center.X+10,area.Center.Y+10),
        new Point(area.Center.X-10,area.Center.Y+10)
        };
        Point[] points2 = new Point[] // 2nd stroke
        {
        new Point(area.Center.X-14,area.Center.Y-14),
        new Point(area.Center.X+14,area.Center.Y-14),
        new Point(area.Center.X+14,area.Center.Y+14),
        new Point(area.Center.X-14,area.Center.Y+14)
        };

        MouseHandler mouse = new MouseHandler();
        KeyboardHandler keyboard = new KeyboardHandler();

        mouse.DefaultMouseActionDelay = 111; // set some delay after each mouse action, it's easier to observe

        // Draw on Paint's canvas
        mouse.LeftDrag(points1);
        mouse.LeftDrag(points2);

        keyboard.DefaultKeyboardActionDelay = 41; // set some delay after each keyboard action

        // Switch to Paint's select mode
        keyboard.KeyDown(Keys.LControlKey);
        keyboard.KeyTyping(Keys.A);
        keyboard.KeyUp(Keys.LControlKey);
        keyboard.KeyTyping(Keys.Escape);

        // The drawing's area
        Area myDrawingArea = new Area(
            new Rectangle(
                new Point(area.Center.X - 16, area.Center.Y - 16),
                new Size(32, 32)));

        // In Paint, select the drawing
        mouse.LeftDrag(
            myDrawingArea.TopLeft,
            myDrawingArea.BottomRight);

        // Make a copy of the drawing
        keyboard.KeyDown(Keys.LControlKey);
        keyboard.KeyTyping(Keys.C);
        keyboard.KeyTyping(Keys.V);
        keyboard.KeyUp(Keys.LControlKey);
        keyboard.KeyTyping(Keys.Escape);

        // Find all the same drawings on Paint's window
        // (with 75% threshold by using default Pattern constructor)
        using (Pattern pattern = new Pattern(myDrawingArea.GetDisplayingImage()))
        {
            List<Match> matches = area.FindAll(pattern);
            foreach (Match match in matches)
            {
                area.SubArea(match.Rectangle).Highlight(brush);
            }
            Thread.Sleep(2222);
            Area.ClearHighlight();
        }
    }
}
```

# Dependencies

- [Emgu CV](http://www.emgu.com/)


License
----

MIT


**It's free. El Psy Congroo!**
