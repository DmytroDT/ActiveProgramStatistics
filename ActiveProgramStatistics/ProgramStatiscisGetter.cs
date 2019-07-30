using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Timers;


namespace ActiveProgramStatistics
{
    //static class that hanles extracting data of foreground processes

    static class ProcessInfo
    {
        


        //Importing external dll to monitor windows foreground processes

        [DllImport("user32.dll")]
        static extern IntPtr GetForegroundWindow();
        [DllImport("user32.dll")]
        public static extern UInt32 GetWindowThreadProcessId(IntPtr hwnd, ref Int32 pid);


        static IntPtr h = GetForegroundWindow();
        static  int pid = 0;
        static public int Pid 
        {
            get { return pid; }
            private set { value =  pid; }
        }
        // creating an read only PID variable


        static public string MainWindowTitle { get;set; }

        // initializing accsess variable through class constructor
        
        static ProcessInfo()
        {
            GetWindowThreadProcessId(h, ref pid);
            Process process = Process.GetProcessById(pid);
            MainWindowTitle = process.MainWindowTitle;
       }
        //method  checking if pid have changed
    public  static void UpdateValues()
        {

            h = GetForegroundWindow();
            GetWindowThreadProcessId(h, ref pid);
            Process process = Process.GetProcessById(pid);
            MainWindowTitle = process.MainWindowTitle;

          
        }
      
    }

    
}


