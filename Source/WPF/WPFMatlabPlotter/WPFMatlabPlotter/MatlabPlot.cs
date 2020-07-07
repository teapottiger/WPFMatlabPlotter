using System;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace WPFMatlabPlotter
{
    public class MatlabPlot : UserControl
    {
        [DllImport("User32.Dll")]
        private static extern void SetWindowText(IntPtr hwnd, String lpString);

        [DllImport("User32.Dll")]
        private static extern IntPtr FindWindow(String lpClassName, String lpWindowName);

        [DllImport("User32.dll")]
        private static extern int SendMessage(IntPtr hWnd, uint Msg, int wParam, int lParam);

        [DllImport("User32.Dll")]
        private static extern void SetParent(IntPtr hWndChild, IntPtr hWndNewParent);

        [DllImport("User32.Dll")]
        private static extern bool MoveWindow(IntPtr hWnd, int X, int Y, int nWidth, int nHeight, bool bRepaint);

        [DllImport("User32.Dll")]
        private static extern long SetWindowLong(IntPtr hWnd, int nIndex, long dwNewLong);

        [DllImport("User32.Dll")]
        private static extern long GetWindowLong(IntPtr hWnd, int nIndex);

        [DllImport("User32.Dll")]
        private static extern ulong SetClassLongPtr(IntPtr hWnd, int nIndex, long dwNewLong);

        private IntPtr _foundWindow = IntPtr.Zero;
        private Window _parentWindow = null;


        public MatlabPlot()
        {
            this.LayoutUpdated += MatlabPlot_LayoutUpdated;
        }

        public void BuildGraph(string figureName)
        {
            _parentWindow = Window.GetWindow(this);
            _foundWindow = FindWindow("SunAwtFrame", figureName);

            long lStyle = GetWindowLong(_foundWindow, -16);
            lStyle &= ~(0x00C00000L | 0x00040000L | 0x00020000L | 0x00010000L | 0x00080000L);
            SetWindowLong(_foundWindow, -16, lStyle);

            SetParent(_foundWindow, new System.Windows.Interop.WindowInteropHelper(_parentWindow).Handle);
            SetClassLongPtr(_foundWindow, 26, 0x0002 | 0x0001); //reducing flickering
        }

        public void DestroyGraph()
        {
            SendMessage(_foundWindow, 0x0112, 0xF060, 0);
        }

        private void MatlabPlot_LayoutUpdated(object sender, EventArgs e)
        {
            var relativeCoordinates = GetRelativeCoordinates();
            MoveWindow(_foundWindow, Convert.ToInt32(relativeCoordinates.X), Convert.ToInt32(relativeCoordinates.Y),
            Convert.ToInt32(this.ActualWidth), Convert.ToInt32(this.ActualHeight), true);
        }

        private Point GetRelativeCoordinates()
        {
            Point relativeCoordinates = new Point(0, 0);

            var parent = VisualTreeHelper.GetParent(this);

            var location = this.TransformToAncestor((Visual)parent).Transform(new Point(0, 0));
            relativeCoordinates.X += location.X;
            relativeCoordinates.Y += location.Y;

            while (parent != null)
            {
                var foo = (Visual)parent;
                parent = VisualTreeHelper.GetParent(parent);
                if (parent == null) break;
                location = foo.TransformToAncestor((Visual)parent).Transform(new Point(0, 0));
                relativeCoordinates.X += location.X;
                relativeCoordinates.Y += location.Y;

            }
            return relativeCoordinates;
        }

    }
}
