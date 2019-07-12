using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.IO;

//Meine NameSpaces


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
            myTTA_new.Output_File_Folder = myFileSetting.readBox_FileFolder1.MyPath;
            myTTA_new.Output_File_Name = myFileSetting.readBox_FileFolder1.MyFileName;

            myTTA_new.Save_AllFiles();
        }

        //NEW
        private void BarButtonItem_FileSavingOpt_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Form_Saving_Options myForm_Saving = new Form_Saving_Options(mySaving_Options);
            myForm_Saving.ShowDialog();
        }

        private void BarButtonItem_Load_Setting_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Read_N_Adopte_Settings();
        }

        private void BarButtonItem_SaveSetting_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Generate_N_Save_Settings();
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
            myAutoConnect_Window.ChangeAll_newValue("DAQ-Unit ...", "Starting Connection ...", 20);

            if (!myDAQ.IsConnected)
                errorText += myDAQ.AutoOpen(myAutoConnect_Window);

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

            if (!myRthTEC.IsConnected)
                errorText += myRthTEC.AutoOpen(myAutoConnect_Window);

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
                    myRthTEC.Update_settings(element);
                    //rthTEC_Rack1.Serial_Interface = element.ToSerialPort();
                    //rthTEC_Rack1.ComPort_select.Text = element.comboBox_Port.Text;
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
                    myDAQ.Change_ADR(element.textBox_IP.Text);
                }
            }

            foreach (var element in input.ListNI)
            {
                if (element.Name.IndexOf("DAQ") >= 0)
                {
                    myDAQ.Change_ADR(element.comboBox_Channel.Text);                    
                }
            }

            return "";
        }

        #endregion Adopt Funktionen

        //**************************************************************************************************
        //                                 Device & Measurment Settings
        //**************************************************************************************************

        #region Device & Measurment Settings

        private void Generate_N_Save_Settings()
        {
            string text = "";

            //Alle Geräte und einstellungen nacheinander abspeichen
            text += myFileSetting.ToString();
            text += mySaving_Options.ToString();
            text += myRthTEC.ToString();

            //Seicher Dialog öffnen
            SaveFileDialog saveFileDialog1 = new SaveFileDialog()
            {
                Filter = "txt files (*.SET)|*.SET|All files (*.*)|*.*",
                FilterIndex = 0,
                RestoreDirectory = true
            };
            //Fals korrekt --> abspeichern
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                File.WriteAllText(saveFileDialog1.FileName, text);
            }
          
        }

        private void Read_N_Adopte_Settings()
        {
            OpenFileDialog myOpenFileDialoge = new OpenFileDialog()
            {
                Filter = "txt files (*.SET)|*.SET|All files (*.*)|*.*",
                FilterIndex = 0
            };

            if (myOpenFileDialoge.ShowDialog() == DialogResult.OK)
            {
                //File Lesen
                string[] datei_Inhalt = File.ReadAllLines(myOpenFileDialoge.FileName);

                //Nach * als begrenzung für neues Gerät suchen
                List<int> stern_Zeilen = new List<int>();

                for (int i = 0; i < datei_Inhalt.Length; i++)               
                    if (datei_Inhalt[i].StartsWith("*"))
                        stern_Zeilen.Add(i);

                //Inhalt zwischen Sternzeilen verteilen
                for (int i = 0; i < stern_Zeilen.Count; i++)
                {
                    int akt_stern = stern_Zeilen.ElementAt(i);
                    int next_stern = 0;
                    //Aufpassen wenn es keinen letzten Stern mehr gibt
                    if(i == stern_Zeilen.Count - 1)
                        next_stern = datei_Inhalt.Length;
                    else
                        next_stern = stern_Zeilen.ElementAt(i + 1);

                    //Teil Array an Gerät oder Ähnliches schicken
                    if (datei_Inhalt[akt_stern].Contains("Filesetting"))
                        myFileSetting.FromString(datei_Inhalt.Skip(akt_stern + 1).Take(next_stern- akt_stern).ToArray());

                    else if (datei_Inhalt[akt_stern].Contains("SaveOptions"))
                        mySaving_Options.FromString(datei_Inhalt.Skip(akt_stern + 1).Take(next_stern - akt_stern).ToArray());

                    else if (datei_Inhalt[akt_stern].Contains("Rth-Rack"))
                        myRthTEC.FromString(datei_Inhalt.Skip(akt_stern + 1).Take(next_stern - akt_stern).ToArray());
                }


            }
        }

        #endregion Device & Measurment Settings

    }
}
