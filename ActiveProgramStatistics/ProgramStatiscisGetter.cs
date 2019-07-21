using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;


namespace ActiveProgramStatistics
{
    class ProgramStatiscisGetter
    {
        //Importing external dll to monitor windows foreground processes

        [DllImport("user32.dll")]
        static extern IntPtr GetForegroundWindow();
        [DllImport("user32.dll")]
        public static extern UInt32 GetWindowThreadProcessId(IntPtr hwnd, ref Int32 pid);



        IntPtr h = GetForegroundWindow();
        int pid = 0;

        // creating variable with properties for getting the title of an active window

        string SrtingName;
        string MainWindowTitle
        {
            get { return SrtingName; }
            set { GetWindowThreadProcessId(h, ref pid); Process process = Process.GetProcessById(pid); SrtingName = process.MainWindowTitle; }
        }

    
      

        
    }
}
