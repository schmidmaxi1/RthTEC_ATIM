using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace ATIM_GUI._6_TempPlatte
{

    // Der Delegat für den Event
    public delegate void OnParaThreadHandler();

    class Parallel_Thread_TEC
    {

        public Timer timer_05sec = new Timer(500); //timer 0.50s


        public Parallel_Thread_TEC()
        {
            timer_05sec.Elapsed += (o, s) => System.Threading.Tasks.Task.Factory.StartNew(() => OnTimer_05secElapsed(o, s));
        }

        // Der Event an sich
        public OnParaThreadHandler Event_OnParallelThread;


        private void OnTimer_05secElapsed(object o, ElapsedEventArgs s)
        {
            //Do stuff
            if (this.Event_OnParallelThread != null)
            {
                this.Event_OnParallelThread.Invoke();
            }
        }




    }



}
