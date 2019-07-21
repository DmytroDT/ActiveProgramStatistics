using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ActiveProgramStatistics
{
    

    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            ProgramStatiscisGetter Pr = new ProgramStatiscisGetter();
            StatWatcher St = new StatWatcher(panel1,Pr.MainWindowTitle);
        }




        //placeholder class
     public   class StatWatcher
        {
            //declare a static list of all class instances

            static List<StatWatcher> StatWatcherList = new List<StatWatcher>();


            //creating visual component representing foreground opened program 

            public StatWatcher(Panel ExternalPanel, String StringName = "Placeholder")
            {
                Panel panel = new Panel();
                panel.Size = new Size(406, 50);
                panel.BorderStyle = BorderStyle.Fixed3D;

                //creating lable that will get data from ProgramStatisticsGetter Class
                Label lb = new Label();
                lb.Text = StringName;
                panel.Controls.Add(lb);

                //adding this component to external panel
                ExternalPanel.Controls.Add(panel);
                panel.Location = new Point(0,0);


                StatWatcherList.Add(this);
            }

            

        }
    }
}
