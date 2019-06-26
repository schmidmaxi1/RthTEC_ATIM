using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ATIM_GUI
{
    public partial class Form_Saving_Options : Form
    {
        //**************************************************************************************************
        //                                        Variablen
        //**************************************************************************************************

        private Saving_Options calling_Saving_Options;

        //**************************************************************************************************
        //                                        Konstruktor
        //**************************************************************************************************

        public Form_Saving_Options(Saving_Options calling)
        {
            InitializeComponent();

            calling_Saving_Options = calling;

            //Aktuelle Werte in GUI übernehmen
            checkBox_Setting_Save_aver_cool.Checked = calling_Saving_Options.Save_Aver_Cool;
            checkBox_Setting_Save_aver_heat.Checked = calling_Saving_Options.Save_Aver_Heat;

            checkBox_Setting_Save_single_cool.Checked = calling_Saving_Options.Save_Signle_Cool;
            checkBox_Settings_Save_single_heat.Checked = calling_Saving_Options.Save_Single_Heat;

            checkBox_Setting_Save_raw.Checked = calling_Saving_Options.Save_Raw;                
        }




        //**************************************************************************************************
        //                                          GUI
        //**************************************************************************************************

        #region GUI

        private void Button_Apply_Click(object sender, EventArgs e)
        {
            //Werte übernehemen
            calling_Saving_Options.Save_Aver_Cool = checkBox_Setting_Save_aver_cool.Checked;

            calling_Saving_Options.Save_Aver_Heat = checkBox_Setting_Save_aver_heat.Checked;

            calling_Saving_Options.Save_Signle_Cool = checkBox_Setting_Save_single_cool.Checked;

            calling_Saving_Options.Save_Single_Heat = checkBox_Settings_Save_single_heat.Checked;

            calling_Saving_Options.Save_Raw = checkBox_Setting_Save_raw.Checked;

            //Schließen
            this.Close();
        }


        private void Button_Cancel_Click(object sender, EventArgs e)
        {
            //Schließen
            this.Close();
        }


        #endregion GUI


    }
}
