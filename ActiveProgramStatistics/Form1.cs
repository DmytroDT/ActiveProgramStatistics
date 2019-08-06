using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Timers;
//using alias to prevent namespace collisions of thread and timers namespaces
using tTimer = System.Timers.Timer;


namespace ActiveProgramStatistics
{
    public delegate void UpdateDelegate();

    public partial class Form1 : Form
    {
      

        public Form1()
        {
          InitializeComponent();
            StatWatcher st = new StatWatcher(panel1);

            //timer that invokes form  and ProcessInfo update  every 10 second 

            tTimer timer = new tTimer(10000);
            timer.AutoReset = true;
            timer.Enabled = true;
            timer.Elapsed += new ElapsedEventHandler(TimerEvent);
           



            void TimerEvent(object source, ElapsedEventArgs e)
            {
                ProcessInfo.UpdateValues();
                UpdateDelegate TimerDelegate = StatWatcher.ActiveTimer;
                Invoke(TimerDelegate);
            

            }
          
        }
        
    }


    public class StatWatcher
    {
        //declare a static list of all class instances

        static List<StatWatcher> StatWatcherList = new List<StatWatcher>();

        int Pid = ProcessInfo.Pid;
        int time = 10;
        Label label = new Label();
        static int PointY = 0;
        Panel panel = new Panel();
        Panel ExternalPanel = new Panel();

        //creating visual component representing foreground opened program 

        public StatWatcher(Panel ExternalPanel, int y = 0)
        {
           
            panel.Size = new Size(ExternalPanel.Width, 50);
            panel.BorderStyle = BorderStyle.Fixed3D;
          
            
            this.ExternalPanel = ExternalPanel;

            label.Text = ProcessInfo.MainWindowTitle + " has been opened for 0 seconds";
            label.TextAlign = ContentAlignment.MiddleCenter ;
            panel.Controls.Add(label);
            label.Dock = DockStyle.Fill;
           // adding this component to external panel
            ExternalPanel.Controls.Add(panel);
            panel.Location = new Point(0, y);
            
            StatWatcherList.Add(this);
            ExternalPanel.Resize += new EventHandler(PanelResizeHandler);
        }

        private void PanelResizeHandler(Object sender , EventArgs e)
        {
            panel.Size = new Size(ExternalPanel.Width, 50);
        }


        static   public   void ActiveTimer()
        {
            //checking if there are new active windows by comparing PID's in list to ProcessInfo current PID , if there is -catching exeption and creating new component , if not - incrementing time field

            try
            {
            int i= StatWatcherList.FindIndex(Watcher => Watcher.Pid == ProcessInfo.Pid);
              
                StatWatcherList[i].label.Text = ProcessInfo.MainWindowTitle +  $" has been opened for {(StatWatcherList[i].time < 60 ? StatWatcherList[i].time + " seconds" : (StatWatcherList[i].time < 3600 ? (StatWatcherList[i].time / 60) + " minutes" : (StatWatcherList[i].time < 216000 ? (StatWatcherList[i].time / 3600) + " hours" : "")))}";
                StatWatcherList[i].time += 10;
                StatWatcherList[i].panel.Update();
            }
            catch (ArgumentOutOfRangeException )
            {
                PointY += 50;
                StatWatcher NewInstance = new StatWatcher(StatWatcherList[0].ExternalPanel, PointY);
            }
            
        }
        
    }
   
}
