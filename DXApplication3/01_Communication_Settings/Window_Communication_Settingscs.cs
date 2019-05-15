using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection;

using System.IO;
using System.IO.Ports;
using NationalInstruments.DAQmx;            //Eigeneintrag für NI-Karte

namespace ATIM_GUI._1_Communication_Settings
{
    public partial class Window_Communication_Settingscs : Form
    {

        //********************************************************************************************************************
        //                                    Eigenschaften der Klasse
        //********************************************************************************************************************
        public List<SerialCommunicationDivice> ListeCOMs { get; set; } = new List<SerialCommunicationDivice>();
        public List<EthernetCommunicationDevice> ListEthernet { get; set; } = new List<EthernetCommunicationDevice>();
        public List<NI_CommunicationDevice> ListNI { get; set; } = new List<NI_CommunicationDevice>();

      
        string IniFile = Properties.Resources.ATIM_Communication_Settings;

        private string[] ports;
        private string[] channels;

        Form1 myMainWindow;

        //********************************************************************************************************************
        //                                              Konstruktoren
        //********************************************************************************************************************

        public Window_Communication_Settingscs(Form1 calling)
        {
            InitializeComponent();

            myMainWindow = calling;

            //Alle ComPorts suchen
            ports = SerialPort.GetPortNames();

            //Alle Kanäle für NI suchen
            try
            {
                channels = DaqSystem.Local.GetPhysicalChannels(PhysicalChannelTypes.AI, PhysicalChannelAccess.External);
            }
            catch (Exception){}

            Read_IniFile();

        }

        //********************************************************************************************************************
        //                                               Buttons
        //********************************************************************************************************************

        private void BarButtonItem_Refresh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //Ports aktualisieren
            ports = SerialPort.GetPortNames();
            //Einfügen
            foreach (var element in ListeCOMs)
            {
                //Aktuellen Namen Herauslösen
                string helpname = element.comboBox_Port.Text;
                if (helpname.EndsWith(")"))
                    helpname = helpname.Substring(0, helpname.IndexOf(" "));

                //Liste aktuellisiern
                element.comboBox_Port.Items.Clear();
                element.comboBox_Port.Items.AddRange(ports);

                //Neuer Wert
                if (element.comboBox_Port.Items.IndexOf(helpname) < 0)
                    helpname += " (N/A)";
                element.comboBox_Port.Text = helpname;
            }

            //NI-Kanäle aktualisieren
            try
            {
                channels = DaqSystem.Local.GetPhysicalChannels(PhysicalChannelTypes.AI, PhysicalChannelAccess.External);
            }
            catch (Exception) { }
            //Einfügen
            foreach (var element in ListNI)
            {
                //Aktuellen Namen Herauslösen
                string helpname = element.comboBox_Channel.Text;
                if (helpname.EndsWith(")"))
                    helpname = helpname.Substring(0, helpname.IndexOf(" "));

                //Liste aktuellisiern
                element.comboBox_Channel.Items.Clear();
                element.comboBox_Channel.Items.AddRange(channels);

                //Neuer Wert
                if (element.comboBox_Channel.Items.IndexOf(helpname) < 0)
                    helpname += " (N/A)";
                element.comboBox_Channel.Text = helpname;
            }
        }

        private void BarButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            string newLine = Environment.NewLine;

            //String erzeugen
            string text = "#ATIM Communication Settings" + newLine +
                          "#Please don't change me, if you don't know how I work" + newLine + newLine;

            
            foreach (var element in ListeCOMs)
            {
                text += element.Name + "(Serial)" + newLine;
                //Bei Port muss das optionale (N/A) entfert werden
                if(element.comboBox_Port.Text.IndexOf(")") < 0)
                    text += element.Name + "_Port=" + element.comboBox_Port.Text + newLine;
                else
                    text += element.Name + "_Port=" + element.comboBox_Port.Text.Substring(0, element.comboBox_Port.Text.IndexOf(" ")) + newLine;

                text += element.Name + "_BaudRate=" + element.comboBox_BaudRate.Text + newLine;
                text += element.Name + "_DataBits=" + element.comboBox_DataBits.Text + newLine;
                text += element.Name + "_StopBits=" + element.comboBox_StopBits.Text + newLine;
                text += element.Name + "_Parity=" + element.comboBox_Parity.Text + newLine;
                text += element.Name + "_Timeout=" + element.numericUpDown_TimeOut.Value.ToString() + newLine + newLine;
            }
            foreach (var element in ListEthernet)
            {
                text += element.Name + "(Ethernet)" + newLine;
                text += element.Name + "_IP=" + element.textBox_IP.Text + newLine + newLine;
            }
            foreach (var element in ListNI)
            {
                text += element.Name + "(NI)" + newLine;
                text += element.Name + "_Channel=" + element.comboBox_Channel.Text + newLine + newLine;
            }

            //Override aktuellen File
            //System.IO.File.WriteAllText(path_Init_File, text);

        }

        private void Window_Communication_Settingscs_FormClosing(object sender, FormClosingEventArgs e)
        {     
            myMainWindow.Adopt_Communication_settings(ListeCOMs, ListEthernet, ListNI);
        }

        //********************************************************************************************************************
        //                                               Funktionen
        //********************************************************************************************************************



        public void Read_IniFile()
        {

            //Sting in Zeilen aufteilen
            string[] lines = IniFile.Split(
                new[] { "\r\n", "\r", "\n" },
                StringSplitOptions.None
            );


            int elementCounter = 0;
            foreach (string line in lines)
            {
                //Wenn Zeile mit '#' anfängt ist es ein Kommentar                    
                if (line.StartsWith("#") | line == "")
                {
                    //-> Zeile überspringen
                }
                else if (line.IndexOf("(Serial)") > 0)
                {
                    //Neues Element erzeugen
                    SerialCommunicationDivice element = new SerialCommunicationDivice(line + ":")
                    {
                        Location = new Point(10, 25 + (80 * elementCounter)),
                        Name = line.Substring(0, line.IndexOf('(')),
                        Size = new Size(654, 71),
                        TabIndex = 0,
                    };
                    //Counter hochzählen
                    elementCounter++;
                    //Zur GUI hinzufügen
                    Controls.Add(element);
                    //Zur internen Liste
                    ListeCOMs.Add(element);
                }
                else if (line.IndexOf("(Ethernet)") > 0)
                {
                    //Neues Element erzeugen
                    EthernetCommunicationDevice element = new EthernetCommunicationDevice(line + ":")
                    {
                        Location = new Point(10, 25 + (80 * elementCounter)),
                        Name = line.Substring(0, line.IndexOf('(')),
                        Size = new Size(654, 71),
                        TabIndex = 0,
                    };
                    //Counter hochzählen
                    elementCounter++;
                    //Zur GUI hinzufügen
                    Controls.Add(element);
                    //Zur internen Liste
                    ListEthernet.Add(element);
                }
                else if (line.IndexOf("(NI)") > 0)
                {
                    //Neues Element erzeugen
                    NI_CommunicationDevice element = new NI_CommunicationDevice(line + ":")
                    {
                        Location = new Point(10, 25 + (80 * elementCounter)),
                        Name = line.Substring(0, line.IndexOf('(')),
                        Size = new Size(654, 71),
                        TabIndex = 0,
                    };
                    //Counter hochzählen
                    elementCounter++;
                    //Zur GUI hinzufügen
                    Controls.Add(element);
                    //Zur internen Liste
                    ListNI.Add(element);
                }
                else
                {
                    //Zeile mit Informationen
                    string element = line.Substring(0, line.IndexOf("_"));
                    string parameter = line.Substring(line.IndexOf("_") + 1, line.IndexOf("=") - line.IndexOf("_") - 1);
                    string value = line.Substring(line.IndexOf("=") + 1);

                    var aktCOM = ListeCOMs.Find(item => item.Name == element);
                    var aktEthernet = ListEthernet.Find(item => item.Name == element);
                    var aktNI = ListNI.Find(item => item.Name == element);

                    switch (parameter)
                    {
                        case "Port":
                            aktCOM.comboBox_Port.Items.AddRange(ports);
                            //Wenn Port nicht in der Liste, dann markieren
                            if (aktCOM.comboBox_Port.Items.IndexOf(value) < 0)
                                value += " (N/A)";
                            aktCOM.comboBox_Port.Text = value;
                            break;
                        case "BaudRate":
                            aktCOM.comboBox_BaudRate.Text = value;
                            break;
                        case "DataBits":
                            aktCOM.comboBox_DataBits.Text = value;
                            break;
                        case "StopBits":
                            aktCOM.comboBox_StopBits.Text = value;
                            break;
                        case "Parity":
                            aktCOM.comboBox_Parity.Text = value;
                            break;
                        case "Timeout":
                            aktCOM.numericUpDown_TimeOut.Text = value;
                            break;
                        case "IP":
                            aktEthernet.textBox_IP.Text = value;
                            break;
                        case "Channel":
                            aktNI.comboBox_Channel.Items.AddRange(channels);
                            //Wenn Port nicht in der Liste, dann markieren
                            if (aktNI.comboBox_Channel.Items.IndexOf(value) < 0)
                                value += " (N/A)";
                            aktNI.comboBox_Channel.Text = value;
                            break;
                        default:
                            break;
                    }

                }

                //Größe Fenster anpassen                    
                this.Size = new Size(690, 65 + (80 * elementCounter));
            }


        }

    }
}
