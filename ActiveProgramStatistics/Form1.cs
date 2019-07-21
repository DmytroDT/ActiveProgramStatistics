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
//using alias to prevent namespace collisions
using tTimer = System.Timers.Timer;


namespace ActiveProgramStatistics
{
    

    public partial class Form1 : Form
    {
        public Form1()
        {
          InitializeComponent();
             int y = 50;
            tTimer timer = new tTimer(10000);

          ProgramStatiscisGetter Pr = new ProgramStatiscisGetter();

          StatWatcher St = new StatWatcher(panel1,Pr.MainWindowTitle);

            //creating timed event of checking current active window

          timer.Elapsed += new ElapsedEventHandler(TimerEvent);

             void TimerEvent(object source, ElapsedEventArgs e)
            {
                ProgramStatiscisGetter Pt = new ProgramStatiscisGetter();

                //checking if new instance of ProgramStatiscisGetter has different window title than previous one

                if (Pt.MainWindowTitle != Pr.MainWindowTitle)
                {// if it has , creating new component and changing it's location
                
                    StatWatcher Sy = new StatWatcher(panel1, Pt.MainWindowTitle,y);
                    y += 50;
                }
                else
                { //if not , incrementing value of variable tracking amount of time that window is opened
                    Pr.Time += 10;
                }
               
            }
        }

        

        
        //placeholder class
        public class StatWatcher
        {
            //declare a static list of all class instances

            static List<StatWatcher> StatWatcherList = new List<StatWatcher>();

            

            //creating visual component representing foreground opened program 

            public StatWatcher(Panel ExternalPanel, String StringName = "Placeholder", int y=0,int time =0)
            {
                Panel panel = new Panel();
                panel.Size = new Size(406, 50);
                panel.BorderStyle = BorderStyle.Fixed3D;

                //creating lable that will get data from ProgramStatisticsGetter Class
                Label lb = new Label();
                lb.Text = StringName +$" has been opened for {(time<60? time+"seconds":(time<3600? (time / 60)+"minutes":(time<216000?(time/3600)+"hours":"")))}";
                panel.Controls.Add(lb);

                //adding this component to external panel
                ExternalPanel.Controls.Add(panel);
                panel.Location = new Point(0,y);


                StatWatcherList.Add(this);
            }

            

        }
    }
}
