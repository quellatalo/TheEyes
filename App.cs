using System;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;

namespace Qellatalo.Nin.TheEyes
{
    /// <summary>
    /// Represents a running application with an active window.
    /// </summary>
    public class App
    {
        /// <summary>
        /// Bring a window to front.
        /// </summary>
        /// <param name="hWnd">The handle of the target window.</param>
        /// <returns>bool (successful or not).</returns>
        [DllImport("user32.dll")]
        private static extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImport("user32.dll")]
        private static extern bool ShowWindow(IntPtr handle, int nCmdShow);
        /// <summary>
        /// Gets a window rectangle data.
        /// </summary>
        /// <param name="hwnd">The handle of the target window.</param>
        /// <param name="rectangle">The rectangle variable to receive the data</param>
        /// <returns>bool (successful or not).</returns>
        [DllImport("user32.dll")]
        private static extern bool GetWindowRect(IntPtr hwnd, ref Rectangle rectangle);

        /// <summary>
        /// The window process behind the application.
        /// </summary>
        public Process Process { get; private set; }

        /// <summary>
        /// The rectangle of the application's main window on the screen.
        /// </summary>
        public Rectangle GetMainWindowRectangle()
        {
            Rectangle rectangle = Rectangle.Empty;
            GetWindowRect(Process.MainWindowHandle, ref rectangle);
            rectangle.Size = new Size(rectangle.Size.Width - rectangle.Location.X, rectangle.Size.Height - rectangle.Location.Y);
            return rectangle;
        }

        private App() { }

        /// <summary>
        /// Gets the first found application which title contains the specified string.
        /// Default caseSensitive is false.
        /// </summary>
        /// <param name="title">A string which which is contained in the application's window title.</param>
        /// <param name="caseSensitive">Specifies the search's case sensitive.</param>
        /// <returns>An App object, or null if not found.</returns>
        public static App GetAppByWindowTitle(String title, bool caseSensitive = false)
        {
            App app = null;
            Process[] sys = Process.GetProcesses();
            foreach (Process p in sys)
            {
                if (caseSensitive ? p.MainWindowTitle.Contains(title) : p.MainWindowTitle.ToLower().Contains(title.ToLower()))
                {
                    app = new App
                    {
                        Process = p
                    };
                    break;
                }
            }
            return app;
        }

        /// <summary>
        /// Bring the application's main window to front.
        /// </summary>
        public void ToFront()
        {
            // restore if minimized
            ShowWindow(Process.MainWindowHandle, 9);
            SetForegroundWindow(Process.MainWindowHandle);
            SetForegroundWindow(Area.highlightForm.Handle);
        }
    }
}
