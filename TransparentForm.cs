using Quellatalo.Nin.TheEyes.Imaging;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Quellatalo.Nin.TheEyes
{
    internal class TransparentForm : Form
    {
        internal static Color TransparentKey { get; set; } = Color.LimeGreen;
        private Graphics g;
        private Action refreshAction;
        internal TransparentForm()
        {
            FormBorderStyle = FormBorderStyle.None;
            BackColor = TransparentKey;
            TransparencyKey = BackColor;
            ShowInTaskbar = false;
            TopMost = true;
            Size = SystemInformation.VirtualScreen.Size;
            Location = SystemInformation.VirtualScreen.Location;
            g = CreateGraphics();
            refreshAction = new Action(base.Refresh);
        }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                // turn on WS_EX_TOOLWINDOW style bit
                cp.ExStyle |= 0x80;
                return cp;
            }
        }

        internal void Clear()
        {
            Refresh();
            if (InvokeRequired)
            {
                BeginInvoke((MethodInvoker)delegate
                {
                    Visible = false;
                });
            }
            else
            {
                Visible = false;
            }
        }

        public override void Refresh()
        {
            if (InvokeRequired)
            {
                BeginInvoke(refreshAction);
            }
            else
            {
                base.Refresh();
            }
        }

        internal void Highlight(Area area, Pen pen)
        {
            if (InvokeRequired)
            {
                BeginInvoke((MethodInvoker)delegate
                {
                    Visible = true;
                });
            }
            else
            {
                Visible = true;
            }
            GraphicX.Instance.Highlight(g, area.Rectangle, pen);
        }

        internal void Caption(Point location, String str, Font font, Brush brush)
        {
            if (InvokeRequired)
            {
                BeginInvoke((MethodInvoker)delegate
                {
                    Visible = true;
                });
            }
            else
            {
                Visible = true;
            }
            GraphicX.Instance.Caption(g, location, str, font, brush);
        }

        internal void Highlight(Area area, Brush brush)
        {
            if (InvokeRequired)
            {
                BeginInvoke((MethodInvoker)delegate
                {
                    Visible = true;
                });
            }
            else
            {
                Visible = true;
            }
            using (Bitmap display = area.GetDisplayingImage())
            {
                g.DrawImage(display, area.Rectangle.Location);
            }
            GraphicX.Instance.Highlight(g, area.Rectangle, brush);
        }

        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);
            Clear();
        }

        protected override void Dispose(bool disposing)
        {
            g.Dispose();
            base.Dispose(disposing);
        }
    }
}
