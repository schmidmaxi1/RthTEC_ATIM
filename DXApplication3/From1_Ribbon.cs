using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

//Meine NameSpaces
using ATIM_GUI._0_Classes_Measurement;
using ATIM_GUI._1_Communication_Settings;
using ATIM_GUI._2_AutoConnect;
using ATIM_GUI._3_Project;
using ATIM_GUI._4_Settings;

namespace ATIM_GUI
{
    public partial class Form1 : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        //**************************************************************************************************
        //                                      Ribbon Buttons
        //**************************************************************************************************

        #region RibbonButtons

        private void Communication_Settings_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Window_Communication_Settingscs myComSettings = new Window_Communication_Settingscs(this);
            myComSettings.ShowDialog();
        }

        private void SettingsButton_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //Fenster öffnen und erst weitermachen wenn wieder geschlossen
            Form_Settings myForm_Settings = new Form_Settings(this);
            myForm_Settings.ShowDialog();

            //Werte übertragen

        }

        private void BarButtonItem_ProjectView_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Form_Project_Status myForm_Project_Status = new Form_Project_Status();
            myForm_Project_Status.Show();
        }

        private void AutoConnect_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

            string errorText = "";

            //Fenster erzeugen
            Load_Screen myLoadScreen = new Load_Screen();
            myLoadScreen.Show();
            myLoadScreen.Update();

            //Power-Supply
            myLoadScreen.ChangeAll_newValue("Power Supply ...", "Starting Connection ...", 0);

            if (!hameg_HMP1.IsConnected)
                errorText += hameg_HMP1.AutoOpen(myLoadScreen);

            //Spectrum
            myLoadScreen.ChangeAll_newValue("Spectrum DAQ ...", "Starting Connection ...", 20);

            if (!DAQ_Unit.IsConnected)
                errorText += DAQ_Unit.AutoOpen(myLoadScreen);

            //TEC Controller
            myLoadScreen.ChangeAll_newValue("TEC Controller ...", "Starting Connection ...", 40);

            if (!teC_Meerstetter1.IsConnected)
                errorText += teC_Meerstetter1.AutoOpen(myLoadScreen);

            //XYZ-Table
            myLoadScreen.ChangeAll_newValue("XYZ table ...", "Starting Connection ...", 60);

            if (!xyZ_table1.IsConnected)
                errorText += xyZ_table1.AutoOpen(myLoadScreen);

            //XYZ-Table
            myLoadScreen.ChangeAll_newValue("Rth-Rack ...", "Starting Connection ...", 80);

            if (!rthTEC_Rack1.IsConnected)
                errorText += rthTEC_Rack1.AutoOpen(myLoadScreen);

            //Fenster schließen
            myLoadScreen.Close();

            //Bericht ausgeben
            if (errorText != "")
            {
                MessageBox.Show(errorText, "Following device(s) must be connected manueally:",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }


        }

        private void ProjectButton_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void RibbonButton_Save_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            myTTA.Output_File_Folder = this.textBox_Path.Text;
            myTTA.Output_File_Name = this.textBox_File.Text;

            myTTA.Save_AllFiles();
        }

        #endregion RibbonButtons
    }
}
