using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace ATIM_GUI._0_Classes_Measurement
{

    // Der Delegat für den Event
    public delegate void OnParaThreadHandler();

    class Sensitivity_Parallel_Thread
    {

        public Timer timer_1sec = new Timer(1000); //jede Sekunde


        public Sensitivity_Parallel_Thread()
        {
            timer_1sec.Elapsed += (o, s) => System.Threading.Tasks.Task.Factory.StartNew(() => OnTimer_1secElapsed(o, s));
        }

        // Der Event an sich
        public OnParaThreadHandler Event_OnParallelThread;


        private void OnTimer_1secElapsed(object o, ElapsedEventArgs s)
        {
            //Do stuff
            if (this.Event_OnParallelThread != null)
            {
                this.Event_OnParallelThread.Invoke();
            }
        }
        

    }
}
