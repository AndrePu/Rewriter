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

    internal static class WindowConfiguration
    {
        public static WindowState windowState = WindowState.Normal;
        public static double Width = 640;
        public static double Height = 480;
        public static double Left = 0;
        public static double Top = 0;
    }
}
