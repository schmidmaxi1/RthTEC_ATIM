using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace ATIM_GUI._4_Settings
{
    public partial class Form_Settings : DevExpress.XtraEditors.XtraForm
    {

        public Form_Settings(ATIM_MainWindow calling)
        {
            //Calling Form übernehmen um später auf die Settings zuzugreifen
            callingForm = calling;

            InitializeComponent();

            //ComboBox füllen
            ComboBox_Settings_DUT_Init();
            ComboBox_Settings_Frequency_Init();
            ComboBox_Settings_Range_Init();
        }


        //**************************************************************************************************
        //                                            Variablen
        //**************************************************************************************************

        private string myPath = "";
        private Settings_ATIM mySettings;
        private ATIM_MainWindow callingForm;

        //**************************************************************************************************
        //                                Tool Bar -> Save & Open
        //**************************************************************************************************

        #region ToolBar

        private void BarOpen_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            
            //Dialog erzeugen
            OpenFileDialog myOpenFileDialoge = new OpenFileDialog()
            {
                Filter = "txt files (*.SET)|*.SET|All files (*.*)|*.*",
                FilterIndex = 0
            };

            if (myOpenFileDialoge.ShowDialog() == DialogResult.OK)
            {
                myPath = myOpenFileDialoge.FileName;
                mySettings = new Settings_ATIM();
                mySettings.Open(myPath);
            }

            //Werte in UI updaten
            Update_UI();
            
        }

        private void BarSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //Daten aus UI übernehen
            Update_mySettings();

            //Wenn kein Path angegeben Fenster öffnen 
            if (myPath == "")
            {
                //Dialog erzeugen
                SaveFileDialog saveFileDialog1 = new SaveFileDialog()
                {
                    Filter = "txt files (*.SET)|*.SET|All files (*.*)|*.*",
                    FilterIndex = 0,
                    RestoreDirectory = true
                };

                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    myPath = saveFileDialog1.FileName;
                }

            }

            else if (System.IO.File.Exists(myPath))
            {
                DialogResult myResult = MessageBox.Show("Do you want to override the setting-file?", "Warning!", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);               
            }
            
            mySettings.Save(myPath);
            
        }

        private void BarSaveAs_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //Daten aus UI übernehen
            Update_mySettings();

            //Dialog erzeugen
            //Dialog erzeugen
            SaveFileDialog saveFileDialog1 = new SaveFileDialog()
            {
                Filter = "txt files (*.SET)|*.SET|All files (*.*)|*.*",
                FilterIndex = 0,
                RestoreDirectory = true
            };

            //Wenn ok, dann Path übernehmen
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                myPath = saveFileDialog1.FileName;
            }

            //Speichern
            mySettings.Save(myPath);
        }

        #endregion ToolBar

        //**************************************************************************************************
        //                                    Apply & Cancel
        //**************************************************************************************************

        #region Apply&Cancel

        private void Button_Setting_Apply_Click(object sender, EventArgs e)
        {
            //Daten zurückgeben
            Update_mySettings();
            callingForm.mySettings = mySettings;
            callingForm.Adopt_Measurement_settings();          
            this.Close();
        }

        private void Button_Setting_Cancel_Click(object sender, EventArgs e)
        {
            //Schließen
            this.Close();
        }


        #endregion Apply&Cancel

        //**************************************************************************************************
        //                                   Path / File DoubleClick
        //**************************************************************************************************

        #region Path/File

        private void TextBox_Setting_Gerber_Path_DoubleClick(object sender, EventArgs e)
        {
            //Dialog erzeugen
            OpenFileDialog myOpenFileDialoge = new OpenFileDialog()
            {
                Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*",
                FilterIndex = 0
            };

            if (myOpenFileDialoge.ShowDialog() == DialogResult.OK)
            {
                textBox_Setting_Gerber_Path.Text = myOpenFileDialoge.FileName;
            }
        }

        private void TextBox_Folder_DoubleClick(object sender, EventArgs e)
        {
            //Dialog erzeugen
            FolderBrowserDialog myOpenFileDialoge = new FolderBrowserDialog()
            {
            };

            if (myOpenFileDialoge.ShowDialog() == DialogResult.OK)
            {
                textBox_Folder.Text = myOpenFileDialoge.SelectedPath;
            }
        }

        private void TextBox_FileName_DoubleClick(object sender, EventArgs e)
        {
            //Dialog erzeugen
            OpenFileDialog myOpenFileDialoge = new OpenFileDialog()
            {
                Filter = "tx1 files (*.tx1)|*.tx1|All files (*.*)|*.*",
                FilterIndex = 0,
                Multiselect = false,
                Title = "Select file (will be overwritten)",
                InitialDirectory = "C:",
                RestoreDirectory = false
            };

            if (myOpenFileDialoge.ShowDialog() == DialogResult.OK)
            {
                textBox_FileName.Text = myOpenFileDialoge.FileName;
            }
        }

        #endregion Path/File

        //**************************************************************************************************
        //                                      ComboBox
        //**************************************************************************************************

        #region ComboBox

        private void ComboBox_Settings_DUT_Init()
        {
            //Device Typen definieren
            List<string> device_Typs = new List<string>()
            {
                "Diode",
                "MOSFET",
                "Booster"
            };

            //ComboBox füllen
            foreach(string type in device_Typs)
            {
                comboBox_Setting_Device.Items.Add(type);
            }

            //Auswählen --> Erstes
            comboBox_Setting_Device.SelectedIndex = 0;
        }

        private void ComboBox_Settings_Range_Init()
        {
            //Mögliche Werte aus GUI holen
            foreach (var item in callingForm.DAQ_Unit.comboBox_Range.Items)
                comboBox_Setting_Spectrum_Range.Items.Add(item);

            comboBox_Setting_Spectrum_Range.SelectedItem = callingForm.DAQ_Unit.comboBox_Range.SelectedItem;
        }

        private void ComboBox_Settings_Frequency_Init()
        {
            //Mögliche Werte aus GUI holen
            foreach (var item in callingForm.DAQ_Unit.comboBox_Frequency.Items)
                comboBox_Setting_Spectrum_frequency.Items.Add(item);

            comboBox_Setting_Spectrum_frequency.SelectedItem = callingForm.DAQ_Unit.comboBox_Frequency.SelectedItem;
        }

        private void ComboBox_Settings_Device_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Je nach auswahl anpassen
            switch (comboBox_Setting_Device.Text)
            {
                case "Diode":
                    labelControl_heat_voltage.Visible = false;
                    labelControl_meas_voltage.Visible = false;
                    Number_Setting_Heat_Voltage.Visible = false;
                    Number_Setting_Meas_Voltage.Visible = false;

                    Number_Setting_Heat_Current.Maximum = 1500;
                    break;
                case "MOSFET":
                    labelControl_heat_voltage.Visible = true;
                    labelControl_meas_voltage.Visible = true;
                    Number_Setting_Heat_Voltage.Visible = true;
                    Number_Setting_Meas_Voltage.Visible = true;

                    Number_Setting_Heat_Current.Maximum = 5000;
                    break;
                case "Booster":
                    labelControl_heat_voltage.Visible = false;
                    labelControl_meas_voltage.Visible = false;
                    Number_Setting_Heat_Voltage.Visible = false;
                    Number_Setting_Meas_Voltage.Visible = false;

                    Number_Setting_Heat_Current.Maximum = 20000;
                    break;
                default:
                    break;
            }



        }

        #endregion ComboBox

        //**************************************************************************************************
        //                                   Hilfsfunktionen
        //**************************************************************************************************

        #region Hilfsfunktionen

        private void Update_mySettings()
        {
            mySettings = new Settings_ATIM()
            {
                //Eigenschaften TTA
                Device = this.comboBox_Setting_Device.Text,
                Heat_time = this.Number_Setting_Heat_Time.Value,
                Heat_current = this.Number_Setting_Heat_Current.Value,
                Heat_voltage = this.Number_Setting_Heat_Voltage.Value,
                Meas_time = this.Number_Setting_Meas_Time.Value,
                Meas_current = this.Number_Setting_Meas_Current.Value,
                Meas_voltage = this.Number_Setting_Meas_Voltage.Value,
                Offset_voltage = this.Number_Setting_Offset_Voltage.Value,
                Cycles = (UInt16)this.Number_Setting_Cycles.Value,

                //Eigenschaten Temperature
                Temperature_TTA = this.number_Setting_Temperature.Value,
                Default_Sensitivity = this.numericUpDown_DefaultSensitivity.Value,
                TEC_OnOff = this.checkBox_TEC_OnOff.Checked,
                FAN_Enable = this.checkBox_TEC_FAN_enable.Checked,

                Temperature_SEN_Start = this.number_Setting_Sensitivity_Start.Value,
                Temperature_SEN_End = this.number_Setting_Sensitivity_Stop.Value,
                Temperature_SEN_Step = (UInt16)this.number_Setting_Sensitivity_Step.Value,

                //Eigenschaften Save
                Save_Aver_Cool = this.checkBox_Setting_Save_aver_cool.Checked,
                Save_Aver_Heat = this.checkBox_Setting_Save_aver_heat.Checked,
                Save_Signle_Cool = this.checkBox_Setting_Save_single_cool.Checked,
                Save_Single_Heat = this.checkBox_Settings_Save_single_heat.Checked,
                Save_Raw = this.checkBox_Setting_Save_raw.Checked,
                
                Save_Folder = this.textBox_Folder.Text,
                Save_FileName = this.textBox_FileName.Text,

                //Eigenschaften XYZ
                GerberFile_Path = this.textBox_Setting_Gerber_Path.Text,

                //Eigenschaften Spectrum
                Spectrum_range = this.comboBox_Setting_Spectrum_Range.Text,
                Spectrum_frequency = this.comboBox_Setting_Spectrum_frequency.Text,
                Spectrum_triggerLevel = this.number_Setting_Spectrum_Trigger.Value
            };
        }

        private void Update_UI()
        {
            this.comboBox_Setting_Device.Text = mySettings.Device;
            this.Number_Setting_Heat_Time.Value = mySettings.Heat_time;
            this.Number_Setting_Heat_Current.Value = mySettings.Heat_current;
            this.Number_Setting_Heat_Voltage.Value = mySettings.Heat_voltage;
            this.Number_Setting_Meas_Time.Value = mySettings.Meas_time;
            this.Number_Setting_Meas_Current.Value = mySettings.Meas_current;
            this.Number_Setting_Meas_Voltage.Value = mySettings.Meas_voltage;
            this.Number_Setting_Offset_Voltage.Value = mySettings.Offset_voltage;
            this.Number_Setting_Cycles.Value = mySettings.Cycles;

            //Eigenschaten Temperature
            this.number_Setting_Temperature.Value = mySettings.Temperature_TTA;
            this.numericUpDown_DefaultSensitivity.Value = mySettings.Default_Sensitivity;
            this.checkBox_TEC_OnOff.Checked = mySettings.TEC_OnOff;
            this.checkBox_TEC_FAN_enable.Checked = mySettings.FAN_Enable;

            this.number_Setting_Sensitivity_Start.Value = mySettings.Temperature_SEN_Start;
            this.number_Setting_Sensitivity_Stop.Value = mySettings.Temperature_SEN_End;
            this.number_Setting_Sensitivity_Step.Value = mySettings.Temperature_SEN_Step;

            //Eigenschaften Save
            this.checkBox_Setting_Save_aver_cool.Checked = mySettings.Save_Aver_Cool;
            this.checkBox_Setting_Save_aver_heat.Checked = mySettings.Save_Aver_Heat;
            this.checkBox_Setting_Save_raw.Checked = mySettings.Save_Raw;
            this.checkBox_Settings_Save_single_heat.Checked = mySettings.Save_Single_Heat;
            this.checkBox_Setting_Save_single_cool.Checked = mySettings.Save_Signle_Cool;
            this.textBox_FileName.Text = mySettings.Save_FileName;
            this.textBox_Folder.Text = mySettings.Save_Folder;

            //Eigenschaften XYZ
            this.textBox_Setting_Gerber_Path.Text = mySettings.GerberFile_Path;

            //Eigenschaften Spectrum
            this.comboBox_Setting_Spectrum_Range.Text = mySettings.Spectrum_range;
            this.comboBox_Setting_Spectrum_frequency.Text = mySettings.Spectrum_frequency;
            this.number_Setting_Spectrum_Trigger.Value = mySettings.Spectrum_triggerLevel;
        }


        #endregion Hilfsfunktionen


    }
}