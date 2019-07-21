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

        // creating an accsess variable

        private string SrtingName;
        public string MainWindowTitle
        {
            get { return SrtingName; }
            private set { SrtingName = value ;  }
        } 

        // initializing accsess variable through class constructor

        public ProgramStatiscisGetter()
        {
            GetWindowThreadProcessId(h, ref pid);
            Process process = Process.GetProcessById(pid);
            MainWindowTitle =  process.MainWindowTitle;
          
        }



    }
}
