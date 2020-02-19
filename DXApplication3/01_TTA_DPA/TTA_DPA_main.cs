using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DAQ_Units;
using XYZ_Table;
using RthTEC_Rack;

using Read_Coordinates;
using Hilfsfunktionen;

using ATIM_GUI._01_TTA;

namespace ATIM_GUI
{
    public partial class TTA_DPA
    {
        //*****************************************************************************************************
        //                                        Variablen
        //*****************************************************************************************************

        //Devices
        public I_RthTEC MyRthTEC_Rack { get; set; }
        public I_CardType_Amp MyFrontEnd { get; set; }          //im Rack
        public I_CardType_Power MyHeatSource { get; set; }      //im Rack
        public I_DAQ MyDAQ { get; set; }
        public I_XYZ MyXYZ { get; set; }

        //Main Window
        public ATIM_MainWindow GUI { get; set; }

        //Movement
        public Movement_Infos MyMovement { get; internal set; }


        //Datei-Namen usw.
        public string Output_File_Name { get; internal set; }
        public string Output_File_Folder { get; internal set; }


        //Measurment Data ----------------------------------------------------------------------------------------------------

        /// <summary>
        /// 16bit binary values of DAQ-System
        /// </summary>
        public short[] Binary_Raw_Values { get; set; }

        /// <summary>
        /// Binary and Filtered response of ths std. TTA sensing pulse
        /// evtl auflösung auf Int32
        /// </summary>
        public double[] Binary_STD_Respons { get; internal set; }

        /// <summary>
        /// Binary values of the averaged and corrected DPA pulses
        /// </summary>
        public double[] Binary_DPA_Average { get; internal set; }

        /// <summary>
        /// Compressed measurment Data
        /// </summary>
        public List<TTA_DataPoint> Compressed_Data { get; internal set; }

        #region SwitchingPoints

        /// <summary>
        /// All Indices of switching Points form Sense to HEAT while DPA-Sequence
        /// </summary>
        public long[] SwPo_2Heat_DPA { get; internal set; }

        /// <summary>
        /// All Indices of switching Points Heat to SENSE while DPA-Sequence
        /// </summary>
        public long[] SwPo_2Sense_DPA { get; internal set; }

        /// <summary>
        /// The Index of the switching Points from Sense to HEAT while std. Sequence
        /// </summary>
        public long SwPo_2Heat_STD { get; internal set; }

        /// <summary>
        /// The Index of the switching Points from Heat to SENSE while std. Sequence
        /// </summary>
        public long SwPo_2Sense_STD { get; internal set; }

        /// <summary>
        /// The Index of the switching Points from Sense to OFF while std. Sequence
        /// </summary>
        public long SwPo_2Off_STD { get; internal set; }

        #endregion SwitchingPoints

        #region Pulse_Lengthes

        /// <summary>
        /// Average length of DPA Heat-Pulse in Samples
        /// </summary>
        public long Aver_DPA_Heat_Length { get; internal set; }

        /// <summary>
        /// Average length of DPA Heat-Sense in Samples
        /// </summary>
        public long Aver_DPA_Sense_Length { get; internal set; }

        #endregion Pulse_Lengthes

        #region PowerSteps

        /// <summary>
        /// PowerSteps in W while DPA (Sense --> Heat)
        /// </summary>
        public decimal[] PowerSteps_2Heat_DPA { get; internal set; }

        /// <summary>
        /// PowerSteps in W while DPA (Heat --> Sense)
        /// </summary>
        public decimal[] PowerSteps_2Sense_DPA { get; internal set; }

        /// <summary>
        /// PowerStep form sense to heat in W while std TTA 
        /// </summary>
        public decimal PowerStep_2Heat_STD { get; internal set; }

        /// <summary>
        /// PowerStep from heat to sense in W while std TTA
        /// </summary>
        public decimal PowerStep_2Sense_STD { get; internal set; }

        #endregion PowerSteps

        #region PowerCorrection

        /// <summary>
        /// Correction Vector A
        /// </summary>
        public decimal[] CorrVec_A { get; internal set; }

        /// <summary>
        /// Correction Vector A
        /// </summary>
        public decimal[] CorrVec_B { get; internal set; }

        /*
        Rechenung: Z_th_DPA_aver = 1/n * (   A   * T_DPA +   B   * Z_th_STD )

        Größen:         (1x1)    = 1/n * ( (1xn) * (nx1) + (1xm) *   (mx1)  )

        mit m = 2n - 1
        */

        #endregion  PowerCorrection


        //Settings
        private static long Puffer_Messpunkte { get; } = 5;             //Messpunkte vor eigentlichem Signal
        private static long Max_daten_dichte { get; } = 2000;


        public decimal V_f_at_Imeas_Troom { get; set; } = 2;

        //Flags ----------------------------------------------------------------------------------------------------
        /// <summary>
        /// Indicator if single or Auto-Measurment
        /// </summary>
        private bool AutoRun { get; set; }


        //********************************************************************************************************************
        //                                           Konstruktor
        //********************************************************************************************************************

        public TTA_DPA() { }


    }
}
