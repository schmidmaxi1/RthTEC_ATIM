using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;

namespace ATIM_GUI._1_Communication_Settings
{
    public partial class SerialCommunicationDivice : UserControl
    {
        
        public SerialCommunicationDivice(string name)
        {
            InitializeComponent();

            //Name
            groupBox1.Text = name;

            //Listen füllen
            comboBox_BaudRate.Items.AddRange(new String[]
            {
                "50",
                "110",
                "150",
                "300",
                "1200",
                "2400",
                "4800",
                "9600",
                "19200",
                "38400",
                "57600",
                "115200",
                "230400",
                "460800",
                "500000",
            });
            comboBox_DataBits.Items.AddRange(new String[]
            {
                "8",
                "16,"
            });
            comboBox_StopBits.Items.AddRange(new String[]
            {
                StopBits.None.ToString(),
                StopBits.One.ToString(),
                StopBits.OnePointFive.ToString(),
                StopBits.Two.ToString(),
            });
            comboBox_Parity.Items.AddRange(new String[]
            {
                Parity.Even.ToString(),
                Parity.Mark.ToString(),
                Parity.None.ToString(),
                Parity.Odd.ToString(),
                Parity.Space.ToString(),
            });

        }

        public SerialCommunicationDivice() {
            InitializeComponent();
        }

        public SerialPort ToSerialPort()
        {
            SerialPort neu = new SerialPort()
            {
                BaudRate = Convert.ToInt32(comboBox_BaudRate.Text),
                DataBits = Convert.ToInt32(comboBox_DataBits.Text),
                ReadTimeout = (int)numericUpDown_TimeOut.Value,
                PortName = comboBox_Port.Text,
            };

            switch (comboBox_Parity.Text)
            {
                case "Even":
                    neu.Parity = Parity.Even;
                    break;
                case "Mark":
                    neu.Parity = Parity.Mark;
                    break;
                case "None":
                    neu.Parity = Parity.None;
                    break;
                case "Odd":
                    neu.Parity = Parity.Odd;
                    break;
                case "Space":
                    neu.Parity = Parity.Space;
                    break;
            }

            switch (comboBox_StopBits.Text)
            {
                case "None":
                    neu.StopBits = StopBits.None;
                    break;
                case "One":
                    neu.StopBits = StopBits.One;
                    break;
                case "OnePointFive":
                    neu.StopBits = StopBits.OnePointFive;
                    break;
                case "Two":
                    neu.StopBits = StopBits.Two;
                    break;
            }

            return neu;
        }
    }

    
}
