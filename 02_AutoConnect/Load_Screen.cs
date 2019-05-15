using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ATIM_GUI._2_AutoConnect
{
    public partial class Load_Screen : Form
    {
        public Load_Screen()
        {
            InitializeComponent();
            Update();
        }

        public void ChangeAll_AddValue(string device, string task, int progressBar)
        {
            label_Device.Text = device;
            label_Task.Text = task;
            progressBarControl1.EditValue = (int)progressBarControl1.EditValue + progressBar;
            Update();
        }

        public void ChangeAll_newValue(string device, string task, int progressBar)
        {
            label_Device.Text = device;
            label_Task.Text = task;
            progressBarControl1.EditValue = progressBar;
            Update();
        }

        public void ChangeTask(string task, int progressBar)
        {
            label_Task.Text = task;
            progressBarControl1.EditValue = (int)progressBarControl1.EditValue + progressBar;
            Update();
        }
    }
}
