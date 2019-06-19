using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

//Meine NameSpaces
using ATIM_GUI._0_Classes_Measurement;

using ATIM_GUI._3_Project;
using ATIM_GUI._4_Settings;

using Communication_Settings;
using AutoConnect;

namespace ATIM_GUI
{
    public partial class ATIM_MainWindow : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        //**************************************************************************************************
        //                                           GUI Events
        //**************************************************************************************************

        #region RibbonButtons

        private void Communication_Settings_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Window_Communication_Settings myComSettings = new Window_Communication_Settings(Adopt_Communication_settings);
            myComSettings.ShowDialog();
        }

        private void SettingsButton_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //Fenster öffnen und erst weitermachen wenn wieder geschlossen
            Form_Settings myForm_Settings = new Form_Settings(this);
            myForm_Settings.ShowDialog();
        }

        private void BarButtonItem_ProjectView_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Form_Project_Status myForm_Project_Status = new Form_Project_Status();
            myForm_Project_Status.Show();
        }

        private void AutoConnect_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            AutoConnect_ALL();
        }

        private void ProjectButton_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void RibbonButton_Save_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            myTTA.Output_File_Folder = myFileSetting.readBox_FileFolder1.MyPath;
            myTTA.Output_File_Name = myFileSetting.readBox_FileFolder1.MyFileName;

            myTTA.Save_AllFiles();
        }

        #endregion RibbonButtons

        //**************************************************************************************************
        //                                        AUTO CONNECT
        //**************************************************************************************************

        #region AutoConnect
        /// <summary>
        /// Tries to connect all available Devices in the GUI (when not already connected)
        /// Returns report.
        /// </summary>
        private void AutoConnect_ALL()
        {
            //Ausgabe String für MessageBox
            string errorText = "";

            //Fenster erzeugen
            AutoConnect_Window myAutoConnect_Window = new AutoConnect_Window();
            myAutoConnect_Window.Show();
            myAutoConnect_Window.Update();

            //Power-Supply
            myAutoConnect_Window.ChangeAll_newValue("Power Supply ...", "Starting Connection ...", 0);

            if (!myPowerSupply.IsConnected)
                errorText += myPowerSupply.AutoOpen(myAutoConnect_Window);


            //Spectrum
            myAutoConnect_Window.ChangeAll_newValue("Spectrum DAQ ...", "Starting Connection ...", 20);

            if (!DAQ_Unit.IsConnected)
                //errorText += DAQ_Unit.AutoOpen(myAutoConnect_Window);
                errorText += "DAQ fehlt.";

            //TEC Controller
            myAutoConnect_Window.ChangeAll_newValue("TEC Controller ...", "Starting Connection ...", 40);

            if (!myTEC.IsConnected)
                errorText += myTEC.AutoOpen(myAutoConnect_Window);

            //XYZ-Table
            myAutoConnect_Window.ChangeAll_newValue("XYZ table ...", "Starting Connection ...", 60);

            if (!myXYZ.IsConnected)
                errorText += myXYZ.AutoOpen(myAutoConnect_Window);

            //Rth-Rack
            myAutoConnect_Window.ChangeAll_newValue("Rth-Rack ...", "Starting Connection ...", 80);

            if (!rthTEC_Rack1.IsConnected)
                errorText += rthTEC_Rack1.AutoOpen(myAutoConnect_Window);

            //Fenster schließen
            myAutoConnect_Window.Close();

            //Bericht ausgeben
            if (errorText != "")
            {
                MessageBox.Show(errorText, "Following device(s) must be connected manueally:",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        #endregion AutoConnect

        //**************************************************************************************************
        //                                   COMMUNICATION SETTINGS
        //**************************************************************************************************

        #region Adopt Funktionen
       
        public string Adopt_Communication_settings(Window_Communication_Settings input)
        {
            foreach (var element in input.ListeCOMs)
            {
                if (element.Name.IndexOf("Rth") >= 0)
                {
                    rthTEC_Rack1.Serial_Interface = element.ToSerialPort();
                    rthTEC_Rack1.ComPort_select.Text = element.comboBox_Port.Text;
                }
                else if (element.Name.IndexOf("TEC") >= 0)                
                    myTEC.Update_settings(element);
                
                if (element.Name.IndexOf("PowerSupply") >= 0)                
                    myPowerSupply.Update_settings(element);
                
                if (element.Name.IndexOf("XYZ") >= 0)               
                    myXYZ.Update_settings(element);
                
            }

            foreach (var element in input.ListEthernet)
            {
                if (element.Name.IndexOf("DAQ") >= 0)
                {
                    DAQ_Unit.VISA_or_Channel_Name = element.textBox_IP.Text;
                }
            }

            foreach (var element in input.ListNI)
            {
                if (element.Name.IndexOf("DAQ") >= 0)
                {
                    DAQ_Unit.ComPort_select.Text = element.comboBox_Channel.Text;
                }
            }

            return "";
        }

        #endregion Adopt Funktionen
    }
}
