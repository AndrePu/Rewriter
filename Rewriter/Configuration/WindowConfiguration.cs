using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rewriter.Configuration
{
    enum WindowState
    {
        Normal,
        Minimized,
        Maximized
    }

    internal class WindowConfiguration
    {
        public WindowState windowState { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }
        public double Left { get; set; }
        public double Top { get; set; }

        public WindowConfiguration()
        {
            windowState = WindowState.Normal;
            Width = 640;
            Height = 480;
            Left = 0;
            Top = 0;
        }
    }
}
