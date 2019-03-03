using System;
using System.Runtime.InteropServices;

namespace BatchRenamer.Helpers
{
    internal static class WinAPIHelper
    {
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        internal static extern IntPtr SendMessage(IntPtr hWnd, UInt32 Msg, IntPtr wParam, IntPtr lParam);

        // Constants for ListView's label edit feature

        internal const UInt32 EM_SETSEL = 0xB1; // http://msdn.microsoft.com/en-us/library/windows/desktop/bb761661(v=vs.85).aspx
        internal const UInt32 LVM_FIRST = 0x1000;
        internal const UInt32 LVM_GETEDITCONTROL = (LVM_FIRST + 24);
    }
}