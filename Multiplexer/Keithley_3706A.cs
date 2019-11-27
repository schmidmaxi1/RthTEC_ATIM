using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Hilfsfunktionen;

using Ivi.Visa;
using Ivi.Visa.Interop;

namespace Multiplexer
{
    public partial class Keithley_3706A : UserControl
    {
        //********************************************************************************************************************
        //                                    Eigenschaften der Klasse
        //********************************************************************************************************************

        #region Variables
        /// <summary>
        /// Flag if Keithley 3706A is connected
        /// </summary>
        public Boolean IsConnected { get; internal set; } = false;


        /// <summary>
        /// Address of Device: "USB0::0x05E6::0x3706::04076987::INSTR"
        /// </summary>
        public string IVI_Adresse { get; internal set; } = "";

        /// <summary>
        /// Response to "*IDN?"
        /// </summary>
        public string ID { get; internal set; }

        /// <summary>
        /// Log of Communication
        /// </summary>
        public Ring_Log MyLog { get; internal set; } = new Ring_Log(50);


        /// <summary>
        /// Instrument Object for Keithley devices
        /// </summary>
        private FormattedIO488 My_Instrument;

        /// <summary>
        /// Resource Manager for Keithley devices
        /// </summary>
        private ResourceManager My_RM;



        #endregion Variables

        string timeFormat = "dd.MM.yy  hh:mm:ss:fF";


        //********************************************************************************************************************
        //                                                Konstruktoren
        //********************************************************************************************************************

        public Keithley_3706A()
        {
            InitializeComponent();
        }

        public Keithley_3706A(Form callingForm, int x, int y)
        {
            InitializeComponent();

            //HelpFCT.SetComPortBox(ComPort_select);

            //In GUI einfügen
            this.Location = new System.Drawing.Point(x, y);
            this.Name = "Keithley_3706A";
            this.Size = new System.Drawing.Size(515, 75);
            this.TabIndex = 33;

            //Hinzufügen
            callingForm.Controls.Add(this);

            //Temporär 
            textBox_ADR.Text = "USB0::0x05E6::0x3706::04076987::INSTR";



            
            My_RM = new Ivi.Visa.Interop.ResourceManager();
            My_Instrument = new Ivi.Visa.Interop.FormattedIO488();
        }

        //********************************************************************************************************************
        //                                                GUI
        //********************************************************************************************************************

        #region GUI

        private void Button_OpenClose_Click(object sender, EventArgs e)
        {
            if (!IsConnected)
                Open();
            else
                Close();
        }

        private void BarButtonItem_Log_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            MyLog.ShowLog();
        }

        private void BarButtonItem_Detailed_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            PopUp_3706A myPopUp = new PopUp_3706A(this);
            myPopUp.Show();
        }

        #endregion GUI

        //********************************************************************************************************************
        //                                            Global FCT
        //********************************************************************************************************************


        #region Global

        /// <summary>
        /// Returns the Information of the Card in the Slot
        /// </summary>
        public string Get_SlotInfo(int slotNr)
        {
            return Instrument_Query(My_Instrument, "print(slot[" + slotNr.ToString() + "].idn)");
        }

        /// <summary>
        /// Opens all Relais in Array. Syntax: [Slot]0[Relais-Nr] z.B. "1003"
        /// </summary>
        public void Open_Relais(string [] relais)
        {
            //Sting aneinander reihen
            string all_relais = string.Join(", ", relais);

            //Nachricht senden
            Instrument_Write(My_Instrument, "channel.open(\"" + all_relais + "\")");

            textBox_Setup.Text = "Relays opened: " + all_relais;
        }

        /// <summary>
        /// Opens all Relais
        /// </summary>
        public void Open_ALL_Relais()
        {
            Instrument_Write(My_Instrument, "channel.open(\"allslots\")");
            textBox_Setup.Text = "All relays open";
        }

        /// <summary>
        /// Closes all Relais in Array. Syntax: [Slot]0[Relais-Nr] z.B. "1003"
        /// </summary>
        public void Close_Relais(string[] relais)
        {
            //Sting aneinander reihen
            string all_relais = string.Join(", ", relais);

            //Nachricht senden
            Instrument_Write(My_Instrument, "channel.close(\"" + all_relais + "\")");

            textBox_Setup.Text = "Relays closed: " + all_relais;
        }

        /// <summary>
        /// Returns the state of all Relais
        /// </summary>
        public Boolean[] Get_Relais_State(string[] relais)
        {
            //Creat Output Field
            Boolean[] output = new Boolean[relais.Length];

            //Sting aneinander reihen
            string all_relais = string.Join(", ", relais);

            //Nachricht senden
            string answer = Instrument_Query(My_Instrument, "print(channel.getstate(\"" + all_relais + "\"))");
            answer = answer.Replace("\n", "");

            //Aufteile nach ,
            string[] answers = answer.Split(',');

            for(int i = 0; i<relais.Length; i++)
            {
                if (answers[i] == "1")
                    output[i] = true;
                else
                    output[i] = false;
            }

            return output;
        }

        #endregion Global

        //********************************************************************************************************************
        //                                            Local FCT
        //********************************************************************************************************************

        #region lokal

        private void Open()
        {
            //Öffnen
            MyLog.Add_Line(DateTime.Now.ToString(timeFormat) + ": " + "Try to connect Keithley 3706A:\n");

            //Öffnen   
            IVI_Adresse = textBox_ADR.Text;
            try
            {
                Connect_To_Instrument(ref My_RM, ref My_Instrument, IVI_Adresse, 500);
            }
            catch (Exception)
            {
                MessageBox.Show("Conection to Keithley 3706A MUX was NOT found!\nTry again.", "Warning",
                          MessageBoxButtons.OK, MessageBoxIcon.Warning);
                MyLog.Add_Line( "Connection FAILED!\n");
                return;
            }

            //Get Instrumetn ID
            ID = Instrument_Query(My_Instrument, "*IDN?");

            //Is device a 3706A
            if(!ID.Contains("3706A"))
            {
                MessageBox.Show("Instrument is not a Keithley 3706A!\nTry again.", "Warning",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                MyLog.Add_Line("Connection FAILED!\n");
                return;
            }

            MyLog.Add_Line("Connection Seccessful\n");

            //Reseten
            //ErrorCode_Spectrum += Reset();

            //Ab hier verbunden
            IsConnected = true;

            //UI anpassen
            Button_OpenClose.Text = "Close";
            textBox_ADR.Enabled = false;

            //Alle Relais öffnen
            Open_ALL_Relais();

        }

        //muss global zugreifbar sein um die verbindung zu schließen (läuft sonst weiter)
        public void Close()
        {
            //Schließen
            Disconnect_From_Instrument(ref My_Instrument);
            MyLog.Add_Line("Connenction canceled\n");

            //Ab hier verbunden
            IsConnected = false;

            //UI anpassen
            Button_OpenClose.Text = "Open";
            textBox_ADR.Enabled = true;
        }

        #endregion lokal

        //********************************************************************************************************************
        //                                            FCT form Example
        //********************************************************************************************************************
        //C:\Users\schmidm\Desktop\THI-MAXI\1_PTTA\40_AP4_InLine\
        //4_MultiPlexer\keithley-master\keithley-master\Instrument_Examples\Instructables\Get_Started_with_Intsr_Control_CSharp
        //My_First_C_Sharp_Project

        #region Manufacturer_FCT

        private void Connect_To_Instrument(ref ResourceManager resource_manager, ref FormattedIO488 instrument_control_object, string instrument_id_string, Int16 timeout)
        {
            /*
             *  Purpose: Open an instance of an instrument object for remote communication and establish the communication attributes.
             *  
             *  Parameters:
             *      resource_manager - The reference to the resource manager object created external to this function. It is passed in 
             *                         by reference so that any internal attributes that are updated when using to connect to the 
             *                         instrument are updated to the caller. 
             *                         
             *      instrument_control_object - The reference to the instrument object created external to this function. It is passed
             *                                  in by reference so that it retains all values upon exiting this function, making it
             *                                  consumable to all other calling functions. 
             *                                  
             *      instrument_id_string - The instrument VISA resource string used to identify the equipment at the underlying driver 
             *                             level. This string can be obtained per making a call to Find_Resources() VISA function and 
             *                             extracted from the reported list.
             *                             
             *      timeout - This is used to define the duration of wait time that will transpire with respect to VISA read/query calls 
             *                prior to an error being reported.
             *                
             *  Returns:
             *      None
             *      
             *  Revisions: 
             *      2019-06-04      JJB     Initial revision.
             */
            instrument_control_object.IO = (IMessage)resource_manager.Open(instrument_id_string, AccessMode.NO_LOCK, 20000);
            // Instrument ID String examples...
            //       LAN -> TCPIP0::134.63.71.209::inst0::INSTR
            //       USB -> USB0::0x05E6::0x2450::01419962::INSTR
            //       GPIB -> GPIB0::16::INSTR
            //       Serial -> ASRL4::INSTR
            instrument_control_object.IO.Clear();
            int myTO = instrument_control_object.IO.Timeout;
            instrument_control_object.IO.Timeout = timeout;
            myTO = instrument_control_object.IO.Timeout;
            instrument_control_object.IO.TerminationCharacterEnabled = true;
            instrument_control_object.IO.TerminationCharacter = 0x0A;
            return;
        }

        private void Disconnect_From_Instrument(ref FormattedIO488 instrument_control_object)
        {
            /*
             *  Purpose: Closes an instance of and instrument object previously opened for remote communication.
             * 
             *  Parameters:
             *      instrument_control_object - The reference to the instrument object created external to this function. It is passed
             *                                  in by reference so that it retains all values upon exiting this function, making it
             *                                  consumable to all other calling functions. 
             *                
             *  Returns:
             *      None
             *      
             *  Revisions: 
             *      2019-06-04      JJB     Initial revision.
             */
            instrument_control_object.IO.Close();
            return;
        }

        private void Instrument_Write(FormattedIO488 instrument_control_object, string command)
        {
            /*
             *  Purpose: Used to send commands to the instrument.
             *  
             *  Parameters:
             *      instrument_control_object - The reference to the instrument object created external to this function. It is passed
             *                                  in by reference so that it retains all values upon exiting this function, making it
             *                                  consumable to all other calling functions. 
             *                                  
             *      command - The command string issued to the instrument in order to perform an action.
             *      
             *  Returns:
             *      None
             *      
             *  Revisions: 
             *      2019-06-04      JJB     Initial revision.
             */

             /* NOT NECCESSARY
            if (Echo_command == true)
            {
                Console.WriteLine("{0}", command);
            }
            */

            instrument_control_object.WriteString(command + "\n");

            MyLog.Add_Line(
                DateTime.Now.ToString(timeFormat) + " Write: " + command);

            return;
        }

        private string Instrument_Read(FormattedIO488 instrument_control_object)
        {
            /*
             *  Purpose: Used to read commands from the instrument.
             *  
             *  Parameters:
             *      instrument_control_object - The reference to the instrument object created external to this function. It is passed
             *                                  in by reference so that it retains all values upon exiting this function, making it
             *                                  consumable to all other calling functions. 
             *      
             *  Returns:
             *      The string obtained from the instrument.
             *      
             *  Revisions: 
             *      2019-06-04      JJB     Initial revision.
             */
            string temp_str = instrument_control_object.ReadString();

            MyLog.Add_Line(
                DateTime.Now.ToString(timeFormat) + " Read:  " + temp_str);

            return temp_str;
        }

        private string Instrument_Query(FormattedIO488 instrument_control_object, string command)
        {
            /*
             *  Purpose: Used to send commands to the instrument  and obtain an information string from the instrument.
             *           Note that the information received will depend on the command sent and will be in string
             *           format.
             *  
             *  Parameters:
             *      instrument_control_object - The reference to the instrument object created external to this function. It is passed
             *                                  in by reference so that it retains all values upon exiting this function, making it
             *                                  consumable to all other calling functions. 
             *                                  
             *      command - The command string issued to the instrument in order to perform an action.
             *      
             *  Returns:
             *      The string obtained from the instrument.
             *      
             *  Revisions: 
             *      2019-06-04      JJB     Initial revision.
             */            
            Instrument_Write(instrument_control_object, command);
            return Instrument_Read(instrument_control_object);
        }

        #endregion Manufacturer_FCT

    }
}
