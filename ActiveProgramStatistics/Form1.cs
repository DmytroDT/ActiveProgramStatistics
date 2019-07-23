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
    

    public partial class Form1 : Form
    {
        public Form1()
        {
          InitializeComponent();
          

          StatWatcher St = new StatWatcher(panel1);

         
           
        }





        //placeholder class
        public class StatWatcher
        {
            //declare a static list of all class instances

            static List<StatWatcher> StatWatcherList = new List<StatWatcher>();

            int Pid = ProcessInfo.Pid;
            string StringName = ProcessInfo.MainWindowTitle;
            int  time=0;
            Label label = new Label();
           static int PointY=0;
            //creating visual component representing foreground opened program 

            public StatWatcher(Panel ExternalPanel, int y = 0)
            {
                Panel panel = new Panel();
                panel.Size = new Size(600, 50);
                panel.BorderStyle = BorderStyle.Fixed3D;



                label.AutoSize = true;
                label.Text = StringName;
                panel.Controls.Add(label);

                //adding this component to external panel
                ExternalPanel.Controls.Add(panel);
                panel.Location = new Point(0,y);
              
               
                StatWatcherList.Add(this);
                ActiveTimer(panel,ExternalPanel, time);



            }

            public void ActiveTimer(Panel panel,Panel ExternalPanel, int time=10)
            {
                tTimer timer = new tTimer(10000);

                timer.Elapsed += new ElapsedEventHandler(TimerEvent);
                //checking if there are new active windows , if there is , creating new component , if not - incrementing time field

                void TimerEvent(object source, ElapsedEventArgs e)
                {
                    if (Pid == ProcessInfo.Pid)
                    {
                        label.Text += $" has been opened for {(time < 60 ? time + "seconds" : (time < 3600 ? (time / 60) + "minutes" : (time < 216000 ? (time / 3600) + "hours" : "")))}";
                        time += 10;
                       
                    }
                    else
                    {
                        PointY += 50;
                        StatWatcher NewInstance = new StatWatcher(ExternalPanel,PointY);
                        StatWatcherList.Add(NewInstance);
                    }
                    panel.Update();

                }

               
            }


        }
    }
}
