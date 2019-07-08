using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

using DevExpress.XtraCharts;


using ATIM_GUI._01_TTA;
using ATIM_GUI._02_Sensitivity;


namespace ATIM_GUI
{
    public partial class ATIM_MainWindow : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        //**************************************************************************************************
        //                                           Graphs
        //**************************************************************************************************

        //Hilfs-Variable um aktuelles Graphen-Setup zu speichen
        string akt_Graph_Setup = "leer";


        #region init_Graphs

        public void Graph_new_Measurment_for_TTA(TTA_measurement_new myTTA)
        {
            if (akt_Graph_Setup.Contains("TTA"))
            {
                //Sonst nur RAW leeren
                chartControl_RAW.Series.Clear();

                //Hinzufügen leere unsichbare Serie (damit Achsen angezeigt werden)
                var neueSerie2 = new Series("Empty", ViewType.Line)
                {
                    LabelsVisibility = DevExpress.Utils.DefaultBoolean.False,
                    ShowInLegend = false,
                    Visible = true,
                };
                chartControl_RAW.Series.Add(neueSerie2);

                //Zeit neu eistellen
                XYDiagram xyDigaram_RAW = (XYDiagram)chartControl_RAW.Diagram;
                decimal zeit_max = (myTTA.MyRack.Time_Heat + myTTA.MyRack.Time_Meas) / 1000;

                xyDigaram_RAW.AxisX.WholeRange.MaxValue = zeit_max * 2;                         //Zugelassener Bereich
                xyDigaram_RAW.AxisX.WholeRange.MinValue = 0;
                xyDigaram_RAW.AxisX.VisualRange.MaxValue = zeit_max;                            //Sichtbarrer Bereich
                xyDigaram_RAW.AxisX.VisualRange.MinValue = 0;
                xyDigaram_RAW.AxisX.VisualRange.SideMarginsValue = 0;                           //Überstand Links und rechts
                xyDigaram_RAW.AxisX.NumericScaleOptions.GridSpacing = (double)zeit_max / 4;

            }
            else
            {
                Graph_Init_for_TTA(myTTA);
            }

        }

        private void Graph_Init_for_TTA(TTA_measurement_new myTTA)
        {
            //Zwischenspeicher umschreiben
            akt_Graph_Setup = "TTA";

            //Allgeimein für alle Charts...............................................................................................
            Font myFont_Headline = new Font("Tahoma", 12, FontStyle.Bold);
            Font myFont_Axis = new Font("Tahoma", 10);
            DevExpress.XtraCharts.ChartTitle myChartTitle;

            //Ober Graph für RAW.......................................................................................................

            //Alle alten Graphen (Series) löschen & eine leere hinzufügen
            //Muss am Anfang geschehen, da sonst das änderen der Diagram-Eigenschaften nicht übernommen wird
            chartControl_RAW.Series.Clear();
            Series neueSerie = new Series("Empty", ViewType.Line)
            {
                LabelsVisibility = DevExpress.Utils.DefaultBoolean.False,
                ShowInLegend = false,
                Visible = true,
            };
            chartControl_RAW.Series.Add(neueSerie);

            //Alten Titel löschen, neuen erzeugen und hinzufügen
            chartControl_RAW.Titles.Clear();
            myChartTitle = new DevExpress.XtraCharts.ChartTitle()
            {
                Text = "Raw Values DAC",
                Font = myFont_Headline,
                Dock = DevExpress.XtraCharts.ChartTitleDockStyle.Top,
            };
            chartControl_RAW.Titles.Add(myChartTitle);

            //Neues XY-Diagramm erzeugen und dan ChartControl anhängen (Darf nicht zur laufzeit geschenen --> BeginInit())
            XYDiagram xyDigaram_RAW = new XYDiagram();
            ((System.ComponentModel.ISupportInitialize)(this.chartControl_RAW)).BeginInit();
            chartControl_RAW.Diagram = xyDigaram_RAW;
            ((System.ComponentModel.ISupportInitialize)(this.chartControl_RAW)).EndInit();

            //Achsen-Titel setzen
            xyDigaram_RAW.AxisX.Title.Text = "Time in [s]";
            xyDigaram_RAW.AxisX.Title.Visibility = DevExpress.Utils.DefaultBoolean.True;
            xyDigaram_RAW.AxisX.Title.Font = myFont_Axis;
            xyDigaram_RAW.AxisY.Title.Text = "Bit-Value";
            xyDigaram_RAW.AxisY.Title.Visibility = DevExpress.Utils.DefaultBoolean.True;
            xyDigaram_RAW.AxisY.Title.Font = myFont_Axis;

            // Min / Max /GridSpacing / ... - Achse
            decimal zeit_max = (myTTA.MyRack.Time_Heat + myTTA.MyRack.Time_Meas) / 1000;

            xyDigaram_RAW.AxisX.VisibleInPanesSerializable = "-1";
            xyDigaram_RAW.AxisX.VisualRange.Auto = false;
            xyDigaram_RAW.AxisX.WholeRange.MaxValue = zeit_max * 2;                         //Zugelassener Bereich
            xyDigaram_RAW.AxisX.WholeRange.MinValue = 0;
            xyDigaram_RAW.AxisX.VisualRange.MaxValue = zeit_max;                            //Sichtbarrer Bereich
            xyDigaram_RAW.AxisX.VisualRange.MinValue = 0;
            xyDigaram_RAW.AxisX.VisualRange.SideMarginsValue = 0;                           //Überstand Links und rechts
            xyDigaram_RAW.AxisX.NumericScaleOptions.GridSpacing = (double)zeit_max / 4;
            xyDigaram_RAW.AxisX.Tickmarks.MinorVisible = false;

            xyDigaram_RAW.AxisY.VisibleInPanesSerializable = "-1";
            xyDigaram_RAW.AxisY.VisualRange.Auto = false;
            xyDigaram_RAW.AxisY.WholeRange.MaxValue = 32768;
            xyDigaram_RAW.AxisY.WholeRange.MinValue = -32768;
            xyDigaram_RAW.AxisY.VisualRange.MaxValue = 32768;                               //Sichtbarrer Bereich
            xyDigaram_RAW.AxisY.VisualRange.MinValue = -32768;
            xyDigaram_RAW.AxisY.NumericScaleOptions.AutoGrid = false;
            xyDigaram_RAW.AxisY.NumericScaleOptions.GridSpacing = 16384;
            xyDigaram_RAW.AxisY.Interlaced = true;
            xyDigaram_RAW.AxisY.VisualRange.SideMarginsValue = 1;                           //Überstand Links und rechts

            //Label & Legend
            chartControl_RAW.CrosshairOptions.GroupHeaderPattern = "{A} sec";
            chartControl_RAW.CrosshairOptions.HighlightPoints = false;

            chartControl_RAW.Legend.MarkerMode = LegendMarkerMode.CheckBox;

            //Mittlerer Graph für Z_th Heat.....................................................................................

            //Alle alten Graphen (Series) löschen & eine leere hinzufügen
            //Muss am Anfang geschehen, da sonst das änderen der Diagram-Eigenschaften nicht übernommen wird
            chartControl_DATA_Top.Series.Clear();
            neueSerie = new Series("Empty", ViewType.Line)
            {
                LabelsVisibility = DevExpress.Utils.DefaultBoolean.False,
                ShowInLegend = false,
                Visible = true,
            };
            chartControl_DATA_Top.Series.Add(neueSerie);

            //Alten Titel löschen, neuen erzeugen und hinzufügen
            chartControl_DATA_Top.Titles.Clear();
            myChartTitle = new DevExpress.XtraCharts.ChartTitle()
            {
                Text = "Z_th curve for heating",
                Font = myFont_Headline,
                Dock = DevExpress.XtraCharts.ChartTitleDockStyle.Top
            };
            chartControl_DATA_Top.Titles.Add(myChartTitle);

            //Diagramm erzeugen (Muss wegen laufzeit in Begin Init und EndInit)
            XYDiagram xyDiagram_DATA_Top = new XYDiagram(); ;
            ((System.ComponentModel.ISupportInitialize)(this.chartControl_DATA_Top)).BeginInit();
            chartControl_DATA_Top.Diagram = xyDiagram_DATA_Top;
            ((System.ComponentModel.ISupportInitialize)(this.chartControl_DATA_Top)).EndInit();

            //Achsen-Titel setzen
            xyDiagram_DATA_Top.AxisX.Title.Text = "Time in [s]";
            xyDiagram_DATA_Top.AxisX.Title.Visibility = DevExpress.Utils.DefaultBoolean.True;
            xyDiagram_DATA_Top.AxisX.Title.Font = myFont_Axis;
            xyDiagram_DATA_Top.AxisY.Title.Text = "Voltage [V]";
            xyDiagram_DATA_Top.AxisY.Title.Visibility = DevExpress.Utils.DefaultBoolean.True;
            xyDiagram_DATA_Top.AxisY.Title.Font = myFont_Axis;

            // Min / Max /GridSpacing / ... - Achse
            xyDiagram_DATA_Top.AxisX.VisibleInPanesSerializable = "-1";
            xyDiagram_DATA_Top.AxisX.VisualRange.Auto = false;
            xyDiagram_DATA_Top.AxisX.WholeRange.MaxValue = myTTA.MyRack.Time_Heat / 1000;             //Zugelassener Bereich
            xyDiagram_DATA_Top.AxisX.WholeRange.MinValue = 1m / myTTA.MyDAQ.Frequency;
            xyDiagram_DATA_Top.AxisX.VisualRange.MaxValue = myTTA.MyRack.Time_Heat / 1000;            //Sichtbarrer Bereich
            xyDiagram_DATA_Top.AxisX.VisualRange.MinValue = 1m / myTTA.MyDAQ.Frequency;
            xyDiagram_DATA_Top.AxisX.VisualRange.SideMarginsValue = 0;                               //Überstand Links und rechts
            xyDiagram_DATA_Top.AxisX.Logarithmic = true;

            xyDiagram_DATA_Top.AxisY.VisibleInPanesSerializable = "-1";
            xyDiagram_DATA_Top.AxisY.VisualRange.Auto = false;
            xyDiagram_DATA_Top.AxisY.WholeRange.MaxValue = 10;
            xyDiagram_DATA_Top.AxisY.WholeRange.MinValue = 0;
            xyDiagram_DATA_Top.AxisY.VisualRange.MaxValue = 10;            //Sichtbarrer Bereich
            xyDiagram_DATA_Top.AxisY.VisualRange.MinValue = 0;
            xyDiagram_DATA_Top.AxisY.NumericScaleOptions.GridSpacing = 1;
            xyDiagram_DATA_Top.AxisY.VisualRange.SideMarginsValue = 0;     //Überstand Links und rechts
           
            //Label & Legend
            chartControl_DATA_Top.CrosshairOptions.GroupHeaderPattern = "{A} sec";
            chartControl_DATA_Top.CrosshairOptions.HighlightPoints = false;

            chartControl_DATA_Top.Legend.MarkerMode = LegendMarkerMode.CheckBox;


            //Unterer Graph für Zth Cool.....................................................................................

            //Alle alten Graphen (Series) löschen & eine leere hinzufügen
            //Muss am Anfang geschehen, da sonst das änderen der Diagram-Eigenschaften nicht übernommen wird
            chartControl_DATA_Bottom.Series.Clear();
            neueSerie = new Series("Empty", ViewType.Line)
            {
                LabelsVisibility = DevExpress.Utils.DefaultBoolean.False,
                ShowInLegend = false,
                Visible = true,
            };
            chartControl_DATA_Bottom.Series.Add(neueSerie);

            //Alten Titel löschen, neuen erzeugen und hinzufügen
            chartControl_DATA_Bottom.Titles.Clear();
            myChartTitle = new DevExpress.XtraCharts.ChartTitle()
            {
                Text = "Z_th curve for cooling",
                Font = myFont_Headline,
                Dock = DevExpress.XtraCharts.ChartTitleDockStyle.Top
            };           
            chartControl_DATA_Bottom.Titles.Add(myChartTitle);

            //Diagramm erzeugen (Muss wegen laufzeit in Begin Init und EndInit)
            XYDiagram xyDiagram_DATA_Bottom = new XYDiagram(); ;
            ((System.ComponentModel.ISupportInitialize)(this.chartControl_DATA_Bottom)).BeginInit();
            chartControl_DATA_Bottom.Diagram = xyDiagram_DATA_Bottom;
            ((System.ComponentModel.ISupportInitialize)(this.chartControl_DATA_Bottom)).EndInit();

            //Achsen-Titel setzen
            xyDiagram_DATA_Bottom.AxisX.Title.Text = "Time in [s]";
            xyDiagram_DATA_Bottom.AxisX.Title.Visibility = DevExpress.Utils.DefaultBoolean.True;
            xyDiagram_DATA_Bottom.AxisX.Title.Font = myFont_Axis;
            xyDiagram_DATA_Bottom.AxisY.Title.Text = "Voltage [V]";
            xyDiagram_DATA_Bottom.AxisY.Title.Visibility = DevExpress.Utils.DefaultBoolean.True;
            xyDiagram_DATA_Bottom.AxisY.Title.Font = myFont_Axis;

            // Min / Max /GridSpacing / ... - Achse
            xyDiagram_DATA_Bottom.AxisX.VisibleInPanesSerializable = "-1";
            xyDiagram_DATA_Bottom.AxisX.VisualRange.Auto = false;
            xyDiagram_DATA_Bottom.AxisX.WholeRange.MaxValue = myTTA.MyRack.Time_Heat / 1000;             //Zugelassener Bereich
            xyDiagram_DATA_Bottom.AxisX.WholeRange.MinValue = 1m / myTTA.MyDAQ.Frequency;
            xyDiagram_DATA_Bottom.AxisX.VisualRange.MaxValue = myTTA.MyRack.Time_Heat / 1000;            //Sichtbarrer Bereich
            xyDiagram_DATA_Bottom.AxisX.VisualRange.MinValue = 1m / myTTA.MyDAQ.Frequency;
            xyDiagram_DATA_Bottom.AxisX.VisualRange.SideMarginsValue = 0;                               //Überstand Links und rechts
            xyDiagram_DATA_Bottom.AxisX.Logarithmic = true;

            xyDiagram_DATA_Bottom.AxisY.VisibleInPanesSerializable = "-1";
            xyDiagram_DATA_Bottom.AxisY.VisualRange.Auto = true;
            xyDiagram_DATA_Bottom.AxisY.WholeRange.MaxValue = 10;
            xyDiagram_DATA_Bottom.AxisY.WholeRange.MinValue = 0;
            xyDiagram_DATA_Bottom.AxisY.VisualRange.MaxValue = 10;            //Sichtbarrer Bereich
            xyDiagram_DATA_Bottom.AxisY.VisualRange.MinValue = 0;
            xyDiagram_DATA_Bottom.AxisY.NumericScaleOptions.GridSpacing = 1;
            xyDiagram_DATA_Bottom.AxisY.VisualRange.SideMarginsValue = 0;     //Überstand Links und rechts


            //Label & Legend
            chartControl_DATA_Bottom.CrosshairOptions.GroupHeaderPattern = "{A} sec";
            chartControl_DATA_Bottom.CrosshairOptions.HighlightPoints = false;

            chartControl_DATA_Bottom.Legend.MarkerMode = LegendMarkerMode.CheckBox;
        }

        private void Graph_Init_for_Sensitivity(Sensitivity_Measurement_new mySen)
        {
            //Zwischenspeicher umschreiben
            akt_Graph_Setup = "Sensitivity";

            //Allgeimein für alle Charts...............................................................................................
            Font myFont_Headline = new Font("Tahoma", 12, FontStyle.Bold);
            Font myFont_Axis = new Font("Tahoma", 10);
            DevExpress.XtraCharts.ChartTitle myChartTitle;

            //Ober Graph für RAW.......................................................................................................

            //Alle alten Graphen (Series) löschen & eine leere hinzufügen
            //Muss am Anfang geschehen, da sonst das änderen der Diagram-Eigenschaften nicht übernommen wird
            chartControl_RAW.Series.Clear();
            Series neueSerie = new Series("Empty", ViewType.Line)
            {
                LabelsVisibility = DevExpress.Utils.DefaultBoolean.False,
                ShowInLegend = false,
                Visible = true,
            };
            chartControl_RAW.Series.Add(neueSerie);

            //Alten Titel löschen, neuen erzeugen und hinzufügen
            chartControl_RAW.Titles.Clear();
            myChartTitle = new DevExpress.XtraCharts.ChartTitle()
            {
                Text = "Raw Values DAC",
                Font = myFont_Headline,
                Dock = DevExpress.XtraCharts.ChartTitleDockStyle.Top,
            };
            chartControl_RAW.Titles.Add(myChartTitle);

            //Neues XY-Diagramm erzeugen und dan ChartControl anhängen (Darf nicht zur laufzeit geschenen --> BeginInit())
            XYDiagram xyDigaram_RAW = new XYDiagram();
            ((System.ComponentModel.ISupportInitialize)(this.chartControl_RAW)).BeginInit();
            chartControl_RAW.Diagram = xyDigaram_RAW;
            ((System.ComponentModel.ISupportInitialize)(this.chartControl_RAW)).EndInit();

            //Achsen-Titel setzen
            xyDigaram_RAW.AxisX.Title.Text = "Time in [s]";
            xyDigaram_RAW.AxisX.Title.Visibility = DevExpress.Utils.DefaultBoolean.True;
            xyDigaram_RAW.AxisX.Title.Font = myFont_Axis;
            xyDigaram_RAW.AxisY.Title.Text = "Bit-Value";
            xyDigaram_RAW.AxisY.Title.Visibility = DevExpress.Utils.DefaultBoolean.True;
            xyDigaram_RAW.AxisY.Title.Font = myFont_Axis;

            // Min / Max /GridSpacing / ... - Achse
            double zeit_max = (double)mySen.Nr_of_samples / mySen.MyDAQ.Frequency;

            xyDigaram_RAW.AxisX.VisibleInPanesSerializable = "-1";
            xyDigaram_RAW.AxisX.VisualRange.Auto = false;
            xyDigaram_RAW.AxisX.WholeRange.MaxValue = zeit_max * 2;             //Zugelassener Bereich
            xyDigaram_RAW.AxisX.WholeRange.MinValue = 0;
            xyDigaram_RAW.AxisX.VisualRange.MaxValue = zeit_max;            //Sichtbarrer Bereich
            xyDigaram_RAW.AxisX.VisualRange.MinValue = 0;
            xyDigaram_RAW.AxisX.VisualRange.SideMarginsValue = 0;     //Überstand Links und rechts
            xyDigaram_RAW.AxisX.NumericScaleOptions.GridSpacing = (double)zeit_max / 4;

            xyDigaram_RAW.AxisY.VisibleInPanesSerializable = "-1";
            xyDigaram_RAW.AxisY.VisualRange.Auto = false;
            xyDigaram_RAW.AxisY.WholeRange.MaxValue = 32768;
            xyDigaram_RAW.AxisY.WholeRange.MinValue = -32768;
            xyDigaram_RAW.AxisY.VisualRange.MaxValue = 32768;            //Sichtbarrer Bereich
            xyDigaram_RAW.AxisY.VisualRange.MinValue = -32768;
            xyDigaram_RAW.AxisY.NumericScaleOptions.AutoGrid = false;
            xyDigaram_RAW.AxisY.NumericScaleOptions.GridSpacing = 16384;
            xyDigaram_RAW.AxisY.Interlaced = true;
            xyDigaram_RAW.AxisY.VisualRange.SideMarginsValue = 1;     //Überstand Links und rechts

            //Label & Legend
            chartControl_RAW.CrosshairOptions.GroupHeaderPattern = "{A} sec";
            chartControl_RAW.CrosshairOptions.HighlightPoints = false;

            chartControl_RAW.Legend.MarkerMode = LegendMarkerMode.CheckBox;

            //Mittlerer Graph für Voltage @ Temperature.................................................................................

            //Alle alten Graphen (Series) löschen & eine leere hinzufügen
            //Muss am Anfang geschehen, da sonst das änderen der Diagram-Eigenschaften nicht übernommen wird
            chartControl_DATA_Top.Series.Clear();
            neueSerie = new Series("Empty", ViewType.Line)
            {
                LabelsVisibility = DevExpress.Utils.DefaultBoolean.False,
                ShowInLegend = false,
                Visible = true,
            };
            chartControl_DATA_Top.Series.Add(neueSerie);

            //Alten Titel löschen, neuen erzeugen und hinzufügen
            chartControl_DATA_Top.Titles.Clear();
            myChartTitle = new DevExpress.XtraCharts.ChartTitle()
            {
                Text = "Voltage @ Temperature",
                Font = myFont_Headline,
                Dock = DevExpress.XtraCharts.ChartTitleDockStyle.Top
            };
            chartControl_DATA_Top.Titles.Add(myChartTitle);

            //Diagramm erzeugen (Muss wegen laufzeit in Begin Init und EndInit)
            XYDiagram xyDiagram_DATA_Top = new XYDiagram(); ;
            ((System.ComponentModel.ISupportInitialize)(this.chartControl_DATA_Top)).BeginInit();
            chartControl_DATA_Top.Diagram = xyDiagram_DATA_Top;
            ((System.ComponentModel.ISupportInitialize)(this.chartControl_DATA_Top)).EndInit();

            //Achsen-Titel setzen
            xyDiagram_DATA_Top.AxisX.Title.Text = "Temperature in [°C]";
            xyDiagram_DATA_Top.AxisX.Title.Visibility = DevExpress.Utils.DefaultBoolean.True;
            xyDiagram_DATA_Top.AxisX.Title.Font = myFont_Axis;
            xyDiagram_DATA_Top.AxisY.Title.Text = "Voltage [V]";
            xyDiagram_DATA_Top.AxisY.Title.Visibility = DevExpress.Utils.DefaultBoolean.True;
            xyDiagram_DATA_Top.AxisY.Title.Font = myFont_Axis;

            // Min / Max /GridSpacing / ... - Achse
            xyDiagram_DATA_Top.AxisX.VisibleInPanesSerializable = "-1";
            xyDiagram_DATA_Top.AxisX.VisualRange.Auto = false;
            xyDiagram_DATA_Top.AxisX.WholeRange.MaxValue = (double)mySen.TempSteps[mySen.TempSteps.Count - 1];                          //Zugelassener Bereich
            xyDiagram_DATA_Top.AxisX.WholeRange.MinValue = (double)mySen.TempSteps[0];
            xyDiagram_DATA_Top.AxisX.VisualRange.MaxValue = (double)mySen.TempSteps[mySen.TempSteps.Count - 1];                         //Sichtbarrer Bereich
            xyDiagram_DATA_Top.AxisX.VisualRange.MinValue = (double)mySen.TempSteps[0];
            xyDiagram_DATA_Top.AxisX.NumericScaleOptions.AutoGrid = false;
            xyDiagram_DATA_Top.AxisX.NumericScaleOptions.GridSpacing = (double)Math.Abs(mySen.TempSteps[0] - mySen.TempSteps[1]);       //Abstand
            xyDiagram_DATA_Top.AxisX.NumericScaleOptions.GridOffset = 5;                                                                //Offset wegen Margin
            xyDiagram_DATA_Top.AxisX.VisualRange.SideMarginsValue = 5;                                                                  //Überstand Links und rechts
            xyDiagram_DATA_Top.AxisX.Logarithmic = false;


            xyDiagram_DATA_Top.AxisY.VisibleInPanesSerializable = "-1";
            xyDiagram_DATA_Top.AxisY.VisualRange.Auto = true;
            xyDiagram_DATA_Top.AxisY.WholeRange.MaxValue = 10;
            xyDiagram_DATA_Top.AxisY.WholeRange.MinValue = 0;
            xyDiagram_DATA_Top.AxisY.VisualRange.MaxValue = 10;                                                                         //Sichtbarrer Bereich
            xyDiagram_DATA_Top.AxisY.VisualRange.MinValue = 0;
            xyDiagram_DATA_Top.AxisY.NumericScaleOptions.GridSpacing = 2;
            xyDiagram_DATA_Top.AxisY.VisualRange.SideMarginsValue = 0;                                                                  //Überstand Links und rechts


            //Label & Legend
            chartControl_DATA_Top.CrosshairOptions.GroupHeaderPattern = "{A} °C";
            chartControl_DATA_Top.CrosshairOptions.HighlightPoints = false;

            chartControl_DATA_Top.Legend.MarkerMode = LegendMarkerMode.CheckBox;


            //Unterer Graph für Temperature Verlauf.....................................................................................

            //Alle alten Graphen (Series) löschen & eine leere hinzufügen
            //Muss am Anfang geschehen, da sonst das änderen der Diagram-Eigenschaften nicht übernommen wird
            chartControl_DATA_Bottom.Series.Clear();
            neueSerie = new Series("Empty", ViewType.Line)
            {
                LabelsVisibility = DevExpress.Utils.DefaultBoolean.False,
                ShowInLegend = false,
                Visible = true,
            };
            chartControl_DATA_Bottom.Series.Add(neueSerie);

            //Alten Titel löschen, neuen erzeugen und hinzufügen
            chartControl_DATA_Bottom.Titles.Clear();
            myChartTitle = new DevExpress.XtraCharts.ChartTitle()
            {
                Text = "Temperature progress",
                Font = myFont_Headline,
                Dock = DevExpress.XtraCharts.ChartTitleDockStyle.Top
            };
            chartControl_DATA_Bottom.Titles.Add(myChartTitle);

            //Diagramm erzeugen (Muss wegen laufzeit in Begin Init und EndInit)
            XYDiagram xyDiagram_DATA_Bottom = new XYDiagram(); ;
            ((System.ComponentModel.ISupportInitialize)(this.chartControl_DATA_Bottom)).BeginInit();
            chartControl_DATA_Bottom.Diagram = xyDiagram_DATA_Bottom;
            ((System.ComponentModel.ISupportInitialize)(this.chartControl_DATA_Bottom)).EndInit();

            //Achsen-Titel setzen
            xyDiagram_DATA_Bottom.AxisX.Title.Text = "Time in [s]";
            xyDiagram_DATA_Bottom.AxisX.Title.Visibility = DevExpress.Utils.DefaultBoolean.True;
            xyDiagram_DATA_Bottom.AxisX.Title.Font = myFont_Axis;
            xyDiagram_DATA_Bottom.AxisY.Title.Text = "Temperature [°C]";
            xyDiagram_DATA_Bottom.AxisY.Title.Visibility = DevExpress.Utils.DefaultBoolean.True;
            xyDiagram_DATA_Bottom.AxisY.Title.Font = myFont_Axis;


            xyDiagram_DATA_Bottom.AxisX.VisibleInPanesSerializable = "-1";
            xyDiagram_DATA_Bottom.AxisX.VisualRange.Auto = false;
            xyDiagram_DATA_Bottom.AxisX.WholeRange.MaxValue = 60;                                                   //Zugelassener Bereich
            xyDiagram_DATA_Bottom.AxisX.WholeRange.MinValue = 0;
            xyDiagram_DATA_Bottom.AxisX.VisualRange.MaxValue = 60;                                                  //Sichtbarrer Bereich
            xyDiagram_DATA_Bottom.AxisX.VisualRange.MinValue = 0;
            xyDiagram_DATA_Bottom.AxisX.VisualRange.SideMarginsValue = 0;                                           //Überstand Links und rechts
            xyDiagram_DATA_Bottom.AxisX.Logarithmic = false;


            xyDiagram_DATA_Bottom.AxisY.VisibleInPanesSerializable = "-1";
            xyDiagram_DATA_Bottom.AxisY.VisualRange.Auto = true;
            xyDiagram_DATA_Bottom.AxisY.WholeRange.MaxValue = 85;
            xyDiagram_DATA_Bottom.AxisY.WholeRange.MinValue = 15;
            xyDiagram_DATA_Bottom.AxisY.VisualRange.MaxValue = (double)mySen.TempSteps[mySen.TempSteps.Count - 1];      //Sichtbarrer Bereich
            xyDiagram_DATA_Bottom.AxisY.VisualRange.MinValue = (double)mySen.TempSteps[0];
            xyDiagram_DATA_Bottom.AxisY.NumericScaleOptions.GridSpacing = 5;
            xyDiagram_DATA_Bottom.AxisY.VisualRange.SideMarginsValue = 5;                                               //Überstand Links und rechts

            //Label & Legend
            chartControl_DATA_Bottom.CrosshairOptions.GroupHeaderPattern = "{A} sec";
            chartControl_DATA_Bottom.CrosshairOptions.HighlightPoints = false;

            chartControl_DATA_Bottom.Legend.MarkerMode = LegendMarkerMode.CheckBox;

        }

        #endregion init_Graphs

        #region resize_Graphs

        private void SplitContainer1_Panel1_SizeChanged(object sender, EventArgs e)
        {
            chartControl_RAW.Size = splitContainer1.Panel1.ClientSize - new Size(6, 6);
        }

        private void SplitContainer2_Panel1_SizeChanged(object sender, EventArgs e)
        {
            chartControl_DATA_Top.Size = splitContainer2.Panel1.ClientSize - new Size(6, 6);
        }

        private void SplitContainer2_Panel2_SizeChanged(object sender, EventArgs e)
        {
            chartControl_DATA_Bottom.Size = splitContainer2.Panel2.ClientSize - new Size(6, 6);
        }


        #endregion resize_Graphs

        #region plot_in_Graphs

        public void Add_Series_to_RAW(TTA_measurement_new myTTA, int Messung)
        {
            //Es dürfen nich alle Pukte übertragen werden (Datenmenge)
            //ungefähr 20Tausend DatenPunkte
            int steps = myTTA.Binary_Raw_Files.GetLength(1) / 20000;
            //Auf 10^x Wert runden
            steps = (int)Math.Pow(10, Math.Round(Math.Log10(steps)));

            //Daten in Passende Liste einfügen
            List<RAW_DataPoint> myRawDataList = new List<RAW_DataPoint>();

            for (int sample = 0; sample < myTTA.Binary_Raw_Files.GetLength(1); sample += steps)
            {
                myRawDataList.Add(
                    new RAW_DataPoint()
                    {
                        Value = myTTA.Binary_Raw_Files[Messung, sample],
                        Time = (decimal)sample / myTTA.MyDAQ.Frequency,
                    }
                    );
            }

            //Muss so kompliziert sein, da UI nicht im Thread liegt
            chartControl_RAW.Invoke((MethodInvoker)delegate
            {
                var neueSerie = new Series("Cycle " + (Messung + 1).ToString(), ViewType.Line)
                {
                    CheckableInLegend = true,



                    DataSource = myRawDataList,
                    ArgumentScaleType = ScaleType.Numerical,
                    ArgumentDataMember = "Time",
                    ValueScaleType = ScaleType.Numerical,
                };

                neueSerie.ValueDataMembers.AddRange(new string[] { "Value" });

                chartControl_RAW.Series.Add(neueSerie);

                //Resize x-Achse 

                //((LineSeriesView)neueSerie.View).LineMarkerOptions.Kind = MarkerKind.Triangle;
                //((LineSeriesView)neueSerie.View).MarkerVisibility = DevExpress.Utils.DefaultBoolean.True;
            });

        }

        public void Add_Series_to_RAW(Sensitivity_Measurement_new mySensitivity)
        {
            //Daten in Passende Liste einfügen
            List<RAW_DataPoint> myRawDataList = new List<RAW_DataPoint>();

            //Daten umschreiben
            for (int sample = 0; sample < mySensitivity.RawData.Length; sample++)
            {
                myRawDataList.Add(
                    new RAW_DataPoint()
                    {
                        Value = mySensitivity.RawData[sample],
                        Time = (decimal)sample / mySensitivity.MyDAQ.Frequency,
                    }
                    );
            }

            //Muss so kompliziert sein, da UI nicht im Thread liegt
            chartControl_RAW.Invoke((MethodInvoker)delegate
            {

                chartControl_RAW.Series.Clear();

                var neueSerie = new Series("RAW_Data", ViewType.Line)
                {
                    CheckableInLegend = true,



                    DataSource = myRawDataList,
                    ArgumentScaleType = ScaleType.Numerical,
                    ArgumentDataMember = "Time",
                    ValueScaleType = ScaleType.Numerical,
                };

                neueSerie.ValueDataMembers.AddRange(new string[] { "Value" });

                chartControl_RAW.Series.Add(neueSerie);

                //Resize x-Achse 

                //((LineSeriesView)neueSerie.View).LineMarkerOptions.Kind = MarkerKind.Triangle;
                //((LineSeriesView)neueSerie.View).MarkerVisibility = DevExpress.Utils.DefaultBoolean.True;
            });

        }


        public void Add_Series_to_Data(TTA_measurement_new myTTA, string SeriesName)
        {

            chartControl_DATA_Top.Invoke((MethodInvoker)delegate
            {
                var neueSerie = new Series(SeriesName, ViewType.Line)
                {
                    CheckableInLegend = true,

                    DataSource = myTTA.Average_Heat_Compressed,
                    ArgumentScaleType = ScaleType.Numerical,
                    ArgumentDataMember = "Time",
                    ValueScaleType = ScaleType.Numerical,
                    CrosshairLabelPattern = "{S} : {V:0.000} V"
                };

                neueSerie.ValueDataMembers.AddRange(new string[] { "Voltage" });

                chartControl_DATA_Top.Series.Add(neueSerie);
            });

            chartControl_DATA_Bottom.Invoke((MethodInvoker)delegate
            {
                var neueSerie = new Series(SeriesName, ViewType.Line)
                {
                    CheckableInLegend = true,

                    DataSource = myTTA.Average_Meas_Compressed,
                    ArgumentScaleType = ScaleType.Numerical,
                    ArgumentDataMember = "Time",
                    ValueScaleType = ScaleType.Numerical,
                    CrosshairLabelPattern = "{S} : {V:0.000} V"
                };

                neueSerie.ValueDataMembers.AddRange(new string[] { "Voltage" });

                chartControl_DATA_Bottom.Series.Add(neueSerie);
            });


        }

        public void Add_Series_to_Data(TTA_measurement_new myTTA)
        {
            Add_Series_to_Data(myTTA, "DUT " + chartControl_DATA_Top.Series.Count.ToString());
        }

        public void Add_Series_to_Data(Sensitivity_Measurement_new mySensitivity)
        {

            chartControl_DATA_Top.Invoke((MethodInvoker)delegate
            {
                //*******************Haupt-Temperature-Graph************************************
                for (int i = 0; i < mySensitivity.Nr_of_LEDs; i++)
                {
                    var voltage_Series = new Series(mySensitivity.MyMovement_Infos.MyMeasurment_Point[i].Name, ViewType.Line)
                    {
                        CheckableInLegend = true,                       
                        DataSource = mySensitivity.Voltage_Values[i],
                        ArgumentScaleType = ScaleType.Numerical,
                        ArgumentDataMember = "Temperature",
                        ValueScaleType = ScaleType.Numerical,
                        CrosshairLabelPattern = "{S} : {V:0.000} V",                       
                    };

                    voltage_Series.ValueDataMembers.AddRange(new string[] { "Voltage" });

                    //Punkte sichtbar und so dick wie Linie
                    ((LineSeriesView)voltage_Series.View).MarkerVisibility = DevExpress.Utils.DefaultBoolean.True;
                    ((LineSeriesView)voltage_Series.View).LineMarkerOptions.Size = ((LineSeriesView)voltage_Series.View).LineStyle.Thickness;


                    chartControl_DATA_Top.Series.Add(voltage_Series);
                }
            });


            chartControl_DATA_Bottom.Invoke((MethodInvoker)delegate
            {
                //*******************Haupt-Temperature-Graph************************************
                var mainTemp_Series = new Series("TEC-Temperature", ViewType.Line)
                {
                    CheckableInLegend = true,
                    ShowInLegend = true,

                    DataSource = mySensitivity.Data_Temp_at_Time,
                    ArgumentScaleType = ScaleType.Numerical,
                    ArgumentDataMember = "Time_in_s",
                    ValueScaleType = ScaleType.Numerical,
                    CrosshairLabelPattern = "{S} : {V:0.00} °C",
                };
                mainTemp_Series.View.Color = Color.Red;
                mainTemp_Series.ValueDataMembers.AddRange(new string[] { "Temperature" });
                chartControl_DATA_Bottom.Series.Add(mainTemp_Series);

                //*******************Markierungen für Messungen*********************************
                for (int i = 0; i < mySensitivity.TempSteps.Count; i++)
                {
                    var marking_Series = new Series("Meas.-Window " + (i + 1).ToString(), ViewType.Area)
                    {
                        CheckableInLegend = true,
                        ShowInLegend = true,
                        CrosshairEnabled = DevExpress.Utils.DefaultBoolean.False,
                        DataSource = mySensitivity.Data_MeasurementMarker[i],
                        ArgumentScaleType = ScaleType.Numerical,
                        ArgumentDataMember = "Time_in_s",
                        ValueScaleType = ScaleType.Numerical,
                    };
                    marking_Series.View.Color = Color.Gray;
                    ((AreaSeriesView)marking_Series.View).Transparency = 135;
                    marking_Series.ValueDataMembers.AddRange(new string[] { "Temperature" });
                    chartControl_DATA_Bottom.Series.Add(marking_Series);
                }


            });
        }


        public void Update_Voltage_Plots_for_TTA()
        {
            chartControl_DATA_Top.Invoke((MethodInvoker)delegate
            {
                //Punkte aktualisieren
                chartControl_DATA_Top.RefreshData();

                //Bei bedarf die y-Achse verlängern
                //Min und Max finden
                //Es wird nur Punkt bei 50µs (Einschwingen vorbei) und der letze Punkt angeschaut
                int point_50µs = 1;
                while (chartControl_DATA_Top.Series[1].Points[point_50µs].NumericalArgument < 0.00005)
                    point_50µs++;

                double help_max = Double.MinValue;
                double help_min = Double.MaxValue;
                double help_max_time = 0;

                foreach (Series serie in chartControl_DATA_Top.Series)
                {
                    if (serie.Name != "Empty")
                    {
                        if (serie.Points[point_50µs].Values[0] > help_max) { help_max = serie.Points[point_50µs].Values[0]; }
                        if (serie.Points[serie.Points.Count - 1].Values[0] < help_min) { help_min = serie.Points[serie.Points.Count - 1].Values[0]; }
                        if (serie.Points[serie.Points.Count - 1].NumericalArgument > help_max_time) { help_max_time = serie.Points[serie.Points.Count - 1].NumericalArgument; }
                    }
                }

                XYDiagram test = (XYDiagram)chartControl_DATA_Top.Diagram;
                test.AxisY.WholeRange.MaxValue = help_max;          //Zugelassener Bereich
                test.AxisY.VisualRange.MaxValue = help_max;            //Sichtbarrer Bereich
                test.AxisY.WholeRange.MinValue = help_min;          //Zugelassener Bereich
                test.AxisY.VisualRange.MinValue = help_min;            //Sichtbarrer Bereich
                test.AxisX.WholeRange.MaxValue = help_max_time;          //Zugelassener Bereich
                test.AxisX.VisualRange.MaxValue = help_max_time;            //Sichtbarrer Bereich

            });

            chartControl_DATA_Bottom.Invoke((MethodInvoker)delegate
            {
                //Punkte aktualisieren
                chartControl_DATA_Bottom.RefreshData();

                //Bei bedarf die y-Achse verlängern
                //Min und Max finden
                //Es wird nur Punkt bei 50µs (Einschwingen vorbei) und der letze Punkt angeschaut
                int point_50µs = 1;
                while (chartControl_DATA_Top.Series[1].Points[point_50µs].NumericalArgument < 0.00005)
                    point_50µs++;

                double help_max = Double.MinValue;
                double help_min = Double.MaxValue;
                double help_max_time = 0;

                foreach (Series serie in chartControl_DATA_Bottom.Series)
                {
                    if (serie.Name != "Empty")
                    {
                        if (serie.Points[point_50µs].Values[0] < help_min) { help_min = serie.Points[point_50µs].Values[0]; }
                        if (serie.Points[serie.Points.Count - 1].Values[0] > help_max) { help_max = serie.Points[serie.Points.Count - 1].Values[0]; }
                        if (serie.Points[serie.Points.Count - 1].NumericalArgument > help_max_time) { help_max_time = serie.Points[serie.Points.Count - 1].NumericalArgument; }
                    }
                }

                XYDiagram test = (XYDiagram)chartControl_DATA_Bottom.Diagram;
                test.AxisY.WholeRange.MaxValue = help_max;          //Zugelassener Bereich
                test.AxisY.VisualRange.MaxValue = help_max;            //Sichtbarrer Bereich
                test.AxisY.WholeRange.MinValue = help_min;          //Zugelassener Bereich
                test.AxisY.VisualRange.MinValue = help_min;            //Sichtbarrer Bereich
                test.AxisX.WholeRange.MaxValue = help_max_time;          //Zugelassener Bereich
                test.AxisX.VisualRange.MaxValue = help_max_time;            //Sichtbarrer Bereich

            });
        }

        public void Update_Temperature_Plot_for_Sensitivity(Sensitivity_Measurement_new mySensitivity)
        {
            chartControl_DATA_Bottom.Invoke((MethodInvoker)delegate
            {
                //Punkte aktualisieren
                chartControl_DATA_Bottom.RefreshData();

                //Bei bedarf die x-Achse verlängern (TempPlot ist immer Series 1)
                if (chartControl_DATA_Bottom.Series["TEC-Temperature"].Points.LastOrDefault().ArgumentX.NumericalArgument % 60 == 0)
                {
                    XYDiagram test = (XYDiagram)chartControl_DATA_Bottom.Diagram;
                    int neues_Maximum = (int)chartControl_DATA_Bottom.Series["TEC-Temperature"].Points.LastOrDefault().ArgumentX.NumericalArgument + 60;
                    test.AxisX.WholeRange.MaxValue = neues_Maximum;             //Zugelassener Bereich
                    test.AxisX.VisualRange.MaxValue = neues_Maximum;            //Sichtbarrer Bereich
                }
            });
        }

        public void Update_Voltage_Plot_for_Sensitivity(Sensitivity_Measurement_new mySensitivity)
        {
            chartControl_DATA_Top.Invoke((MethodInvoker)delegate
            {
                //Punkte aktualisieren
                chartControl_DATA_Top.RefreshData();

                //Bei bedarf die y-Achse verlängern
                //Min und Max finden
                decimal help_min = decimal.MaxValue;
                decimal help_max = decimal.MinValue;

                foreach (var messreihe in mySensitivity.Voltage_Values)
                    foreach (var messpunkt in messreihe)
                    {
                        if (messpunkt.Voltage < help_min) { help_min = messpunkt.Voltage; }
                        if (messpunkt.Voltage > help_max) { help_max = messpunkt.Voltage; }
                    }

                //Aufrunden bzw. abrunden auf 100mV
                help_min = Decimal.Floor(help_min * 10) / 10;
                help_max = Decimal.Ceiling(help_max * 10) / 10;

                XYDiagram temp_Diagram = (XYDiagram)chartControl_DATA_Top.Diagram;
                temp_Diagram.AxisY.WholeRange.MaxValue = help_max;          //Zugelassener Bereich
                temp_Diagram.AxisY.VisualRange.MaxValue = help_max;            //Sichtbarrer Bereich
                temp_Diagram.AxisY.WholeRange.MinValue = help_min;          //Zugelassener Bereich
                temp_Diagram.AxisY.VisualRange.MinValue = help_min;            //Sichtbarrer Bereich
                temp_Diagram.AxisY.NumericScaleOptions.GridSpacing = 0.1;

            });
        }

        #endregion plot_in_Graphs
    }
}
