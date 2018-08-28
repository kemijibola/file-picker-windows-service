using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;

using System.Threading.Tasks;
using System.Timers;

namespace ICMS.Lite.Service
{
    public partial class Scheduler : ServiceBase
    {
        private Timer timer = null;
        public Scheduler()
        {
            
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            timer = new Timer();
            this.timer.Interval = Convert.ToInt32(FilePicker.GetConfigValue("Timer")); //every 30 secs
            this.timer.Elapsed += new System.Timers.ElapsedEventHandler(this.timer_Tick);
            timer.Enabled = true;
            Logger.WriteLog($"ICMS Lite windows service started at {DateTime.Now }");
            
        }
        private async void timer_Tick(object sender, ElapsedEventArgs e)
        {
            await FilePicker.Locator();
        }
        protected override void OnStop()
        {
            timer.Enabled = false;
            Logger.WriteLog($"ICMS Lite windows service stopped at {DateTime.Now }");

        }
    }
}
