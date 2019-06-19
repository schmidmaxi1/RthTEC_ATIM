using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Threading;

namespace AutoConnect
{
    public partial class AutoConnect_Window : Form
    {
        //********************************************************************************************************************
        //                                               Constructors
        //********************************************************************************************************************
        public AutoConnect_Window()
        {
            InitializeComponent();
        }

        public AutoConnect_Window(string text)
        {
            InitializeComponent();

            this.Show();

            if (text == "DEMO")
            {
                for (int i = 0; i <= 100; i += 5)
                {
                    ChangeAll_newValue("Device" + i.ToString(), "Task" + i.ToString(), i);
                    Thread.Sleep(200);               
                }
                //Programm beenden
                System.Environment.Exit(1);               
            }
        }

        //********************************************************************************************************************
        //                                               Functions
        //********************************************************************************************************************

        public void ChangeAll_AddValue(string device, string task, int progressBar)
        {
            label_Device.Text = device;
            label_Task.Text = task;
            progressBar1.Value = (int)progressBar1.Value + progressBar;
            Update();
        }

        public void ChangeAll_newValue(string device, string task, int progressBar)
        {
            label_Device.Text = device;
            label_Task.Text = task;
            progressBar1.Value = progressBar;
            Update();
        }

        public void ChangeTask(string task, int progressBar)
        {
            label_Task.Text = task;
            progressBar1.Value = (int)progressBar1.Value + progressBar;
            Update();
        }


    }
}
