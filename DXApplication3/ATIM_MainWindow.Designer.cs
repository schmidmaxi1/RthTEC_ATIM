﻿namespace ATIM_GUI
{
    partial class ATIM_MainWindow
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ATIM_MainWindow));
            DevExpress.XtraCharts.ChartTitle chartTitle1 = new DevExpress.XtraCharts.ChartTitle();
            this.ribbonControl1 = new DevExpress.XtraBars.Ribbon.RibbonControl();
            this.ribbonButton_ComSet = new DevExpress.XtraBars.BarButtonItem();
            this.ribbonButton_AutoCon = new DevExpress.XtraBars.BarButtonItem();
            this.ribbonButton_MeasSet = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem4 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem5 = new DevExpress.XtraBars.BarButtonItem();
            this.ribbonButton_Save = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem_SaveSetting = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem_Load_Setting = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem_FileSavingOpt = new DevExpress.XtraBars.BarButtonItem();
            this.ribbonPage1 = new DevExpress.XtraBars.Ribbon.RibbonPage();
            this.ribbonPageGroup1 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.ribbonPageGroup2 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.ribbonPageGroup3 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.button_Single_Zth = new DevExpress.XtraEditors.SimpleButton();
            this.button_Single_Sensitivity = new DevExpress.XtraEditors.SimpleButton();
            this.button_Single_UI = new DevExpress.XtraEditors.SimpleButton();
            this.button_Cancel = new DevExpress.XtraEditors.SimpleButton();
            this.groupBox_Single_Meas = new System.Windows.Forms.GroupBox();
            this.groupBox_Automatic = new System.Windows.Forms.GroupBox();
            this.button_Auto_Zth = new DevExpress.XtraEditors.SimpleButton();
            this.button_Auto_Sensitivity = new DevExpress.XtraEditors.SimpleButton();
            this.button_Auto_UI = new DevExpress.XtraEditors.SimpleButton();
            this.groupBox_Cancel = new System.Windows.Forms.GroupBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.chartControl_RAW = new DevExpress.XtraCharts.ChartControl();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.chartControl_DATA_Top = new DevExpress.XtraCharts.ChartControl();
            this.chartControl_DATA_Bottom = new DevExpress.XtraCharts.ChartControl();
            this.statusStrip_MainWindow = new System.Windows.Forms.StatusStrip();
            this.statusBar_Headline = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusBar_ProgressBar = new System.Windows.Forms.ToolStripProgressBar();
            this.statusBar_Detailed = new System.Windows.Forms.ToolStripStatusLabel();
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl1)).BeginInit();
            this.groupBox_Single_Meas.SuspendLayout();
            this.groupBox_Automatic.SuspendLayout();
            this.groupBox_Cancel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartControl_RAW)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartControl_DATA_Top)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartControl_DATA_Bottom)).BeginInit();
            this.statusStrip_MainWindow.SuspendLayout();
            this.SuspendLayout();
            // 
            // ribbonControl1
            // 
            this.ribbonControl1.ExpandCollapseItem.Id = 0;
            this.ribbonControl1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.ribbonControl1.ExpandCollapseItem,
            this.ribbonButton_ComSet,
            this.ribbonButton_AutoCon,
            this.ribbonButton_MeasSet,
            this.barButtonItem4,
            this.barButtonItem5,
            this.ribbonButton_Save,
            this.barButtonItem_SaveSetting,
            this.barButtonItem_Load_Setting,
            this.barButtonItem_FileSavingOpt});
            this.ribbonControl1.Location = new System.Drawing.Point(0, 0);
            this.ribbonControl1.MaxItemId = 4;
            this.ribbonControl1.Name = "ribbonControl1";
            this.ribbonControl1.Pages.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPage[] {
            this.ribbonPage1});
            this.ribbonControl1.ShowApplicationButton = DevExpress.Utils.DefaultBoolean.False;
            this.ribbonControl1.ShowExpandCollapseButton = DevExpress.Utils.DefaultBoolean.False;
            this.ribbonControl1.ShowPageHeadersMode = DevExpress.XtraBars.Ribbon.ShowPageHeadersMode.Hide;
            this.ribbonControl1.ShowToolbarCustomizeItem = false;
            this.ribbonControl1.Size = new System.Drawing.Size(1670, 122);
            this.ribbonControl1.Toolbar.ShowCustomizeItem = false;
            // 
            // ribbonButton_ComSet
            // 
            this.ribbonButton_ComSet.Caption = "Communication Settings";
            this.ribbonButton_ComSet.Id = 8;
            this.ribbonButton_ComSet.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("ribbonButton_ComSet.ImageOptions.Image")));
            this.ribbonButton_ComSet.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("ribbonButton_ComSet.ImageOptions.LargeImage")));
            this.ribbonButton_ComSet.Name = "ribbonButton_ComSet";
            this.ribbonButton_ComSet.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.Communication_Settings_ItemClick);
            // 
            // ribbonButton_AutoCon
            // 
            this.ribbonButton_AutoCon.Caption = "Auto connect divices";
            this.ribbonButton_AutoCon.Id = 9;
            this.ribbonButton_AutoCon.ImageOptions.LargeImage = global::ATIM_GUI.Properties.Resources.Auto_Connect1;
            this.ribbonButton_AutoCon.Name = "ribbonButton_AutoCon";
            this.ribbonButton_AutoCon.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.AutoConnect_ItemClick);
            // 
            // ribbonButton_MeasSet
            // 
            this.ribbonButton_MeasSet.Caption = "Settings Load/Creat";
            this.ribbonButton_MeasSet.Id = 10;
            this.ribbonButton_MeasSet.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("ribbonButton_MeasSet.ImageOptions.Image")));
            this.ribbonButton_MeasSet.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("ribbonButton_MeasSet.ImageOptions.LargeImage")));
            this.ribbonButton_MeasSet.Name = "ribbonButton_MeasSet";
            this.ribbonButton_MeasSet.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.SettingsButton_ItemClick);
            // 
            // barButtonItem4
            // 
            this.barButtonItem4.Caption = "Project Load/Creat";
            this.barButtonItem4.Id = 11;
            this.barButtonItem4.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barButtonItem4.ImageOptions.Image")));
            this.barButtonItem4.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("barButtonItem4.ImageOptions.LargeImage")));
            this.barButtonItem4.Name = "barButtonItem4";
            // 
            // barButtonItem5
            // 
            this.barButtonItem5.Caption = "Project Status View";
            this.barButtonItem5.Id = 12;
            this.barButtonItem5.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barButtonItem5.ImageOptions.Image")));
            this.barButtonItem5.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("barButtonItem5.ImageOptions.LargeImage")));
            this.barButtonItem5.Name = "barButtonItem5";
            // 
            // ribbonButton_Save
            // 
            this.ribbonButton_Save.Caption = "Save ...";
            this.ribbonButton_Save.Id = 14;
            this.ribbonButton_Save.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("ribbonButton_Save.ImageOptions.Image")));
            this.ribbonButton_Save.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("ribbonButton_Save.ImageOptions.LargeImage")));
            this.ribbonButton_Save.Name = "ribbonButton_Save";
            this.ribbonButton_Save.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.RibbonButton_Save_ItemClick);
            // 
            // barButtonItem_SaveSetting
            // 
            this.barButtonItem_SaveSetting.Caption = "Save actual settings as ...";
            this.barButtonItem_SaveSetting.Id = 1;
            this.barButtonItem_SaveSetting.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barButtonItem_SaveSetting.ImageOptions.Image")));
            this.barButtonItem_SaveSetting.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("barButtonItem_SaveSetting.ImageOptions.LargeImage")));
            this.barButtonItem_SaveSetting.Name = "barButtonItem_SaveSetting";
            this.barButtonItem_SaveSetting.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.BarButtonItem_SaveSetting_ItemClick);
            // 
            // barButtonItem_Load_Setting
            // 
            this.barButtonItem_Load_Setting.Caption = "Load settings from ...";
            this.barButtonItem_Load_Setting.Id = 2;
            this.barButtonItem_Load_Setting.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barButtonItem_Load_Setting.ImageOptions.Image")));
            this.barButtonItem_Load_Setting.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("barButtonItem_Load_Setting.ImageOptions.LargeImage")));
            this.barButtonItem_Load_Setting.Name = "barButtonItem_Load_Setting";
            this.barButtonItem_Load_Setting.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.BarButtonItem_Load_Setting_ItemClick);
            // 
            // barButtonItem_FileSavingOpt
            // 
            this.barButtonItem_FileSavingOpt.Caption = "File saving options";
            this.barButtonItem_FileSavingOpt.Id = 3;
            this.barButtonItem_FileSavingOpt.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barButtonItem_FileSavingOpt.ImageOptions.Image")));
            this.barButtonItem_FileSavingOpt.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("barButtonItem_FileSavingOpt.ImageOptions.LargeImage")));
            this.barButtonItem_FileSavingOpt.Name = "barButtonItem_FileSavingOpt";
            this.barButtonItem_FileSavingOpt.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.BarButtonItem_FileSavingOpt_ItemClick);
            // 
            // ribbonPage1
            // 
            this.ribbonPage1.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] {
            this.ribbonPageGroup1,
            this.ribbonPageGroup2,
            this.ribbonPageGroup3});
            this.ribbonPage1.Name = "ribbonPage1";
            this.ribbonPage1.Text = "ribbonPage1";
            // 
            // ribbonPageGroup1
            // 
            this.ribbonPageGroup1.ItemLinks.Add(this.ribbonButton_ComSet);
            this.ribbonPageGroup1.ItemLinks.Add(this.ribbonButton_AutoCon);
            this.ribbonPageGroup1.Name = "ribbonPageGroup1";
            this.ribbonPageGroup1.Text = "Devices";
            // 
            // ribbonPageGroup2
            // 
            this.ribbonPageGroup2.ItemLinks.Add(this.barButtonItem_FileSavingOpt);
            this.ribbonPageGroup2.ItemLinks.Add(this.barButtonItem_Load_Setting);
            this.ribbonPageGroup2.ItemLinks.Add(this.barButtonItem_SaveSetting);
            this.ribbonPageGroup2.ItemLinks.Add(this.ribbonButton_MeasSet);
            this.ribbonPageGroup2.ItemLinks.Add(this.barButtonItem4);
            this.ribbonPageGroup2.ItemLinks.Add(this.barButtonItem5);
            this.ribbonPageGroup2.Name = "ribbonPageGroup2";
            this.ribbonPageGroup2.Text = "Settings";
            // 
            // ribbonPageGroup3
            // 
            this.ribbonPageGroup3.ItemLinks.Add(this.ribbonButton_Save);
            this.ribbonPageGroup3.Name = "ribbonPageGroup3";
            this.ribbonPageGroup3.Text = "Save";
            // 
            // button_Single_Zth
            // 
            this.button_Single_Zth.ImageOptions.Image = global::ATIM_GUI.Properties.Resources.Zth_Symbol_transparent1;
            this.button_Single_Zth.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.button_Single_Zth.Location = new System.Drawing.Point(15, 25);
            this.button_Single_Zth.Name = "button_Single_Zth";
            this.button_Single_Zth.Size = new System.Drawing.Size(80, 80);
            this.button_Single_Zth.TabIndex = 15;
            this.button_Single_Zth.Click += new System.EventHandler(this.Button_Zth_signle_Click);
            // 
            // button_Single_Sensitivity
            // 
            this.button_Single_Sensitivity.ImageOptions.Image = global::ATIM_GUI.Properties.Resources.kFaktor_Symbol_transparent1;
            this.button_Single_Sensitivity.Location = new System.Drawing.Point(15, 120);
            this.button_Single_Sensitivity.Name = "button_Single_Sensitivity";
            this.button_Single_Sensitivity.Size = new System.Drawing.Size(80, 80);
            this.button_Single_Sensitivity.TabIndex = 17;
            this.button_Single_Sensitivity.Click += new System.EventHandler(this.Button_Single_Sensitivity_Click);
            // 
            // button_Single_UI
            // 
            this.button_Single_UI.ImageOptions.Image = global::ATIM_GUI.Properties.Resources.U_I_Symbol_transparent1;
            this.button_Single_UI.Location = new System.Drawing.Point(15, 215);
            this.button_Single_UI.Name = "button_Single_UI";
            this.button_Single_UI.Size = new System.Drawing.Size(80, 80);
            this.button_Single_UI.TabIndex = 18;
            this.button_Single_UI.Click += new System.EventHandler(this.Button_Single_UI_Click);
            // 
            // button_Cancel
            // 
            this.button_Cancel.ImageOptions.Image = global::ATIM_GUI.Properties.Resources.Cancel_transparent1;
            this.button_Cancel.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.button_Cancel.Location = new System.Drawing.Point(15, 25);
            this.button_Cancel.Name = "button_Cancel";
            this.button_Cancel.Size = new System.Drawing.Size(80, 80);
            this.button_Cancel.TabIndex = 23;
            this.button_Cancel.Click += new System.EventHandler(this.Button_Cancel_Click);
            // 
            // groupBox_Single_Meas
            // 
            this.groupBox_Single_Meas.Controls.Add(this.button_Single_Zth);
            this.groupBox_Single_Meas.Controls.Add(this.button_Single_Sensitivity);
            this.groupBox_Single_Meas.Controls.Add(this.button_Single_UI);
            this.groupBox_Single_Meas.Font = new System.Drawing.Font("Tahoma", 9.5F, System.Drawing.FontStyle.Bold);
            this.groupBox_Single_Meas.Location = new System.Drawing.Point(1550, 135);
            this.groupBox_Single_Meas.Name = "groupBox_Single_Meas";
            this.groupBox_Single_Meas.Size = new System.Drawing.Size(110, 310);
            this.groupBox_Single_Meas.TabIndex = 25;
            this.groupBox_Single_Meas.TabStop = false;
            this.groupBox_Single_Meas.Text = "Single:";
            // 
            // groupBox_Automatic
            // 
            this.groupBox_Automatic.Controls.Add(this.button_Auto_Zth);
            this.groupBox_Automatic.Controls.Add(this.button_Auto_Sensitivity);
            this.groupBox_Automatic.Controls.Add(this.button_Auto_UI);
            this.groupBox_Automatic.Font = new System.Drawing.Font("Tahoma", 9.5F, System.Drawing.FontStyle.Bold);
            this.groupBox_Automatic.Location = new System.Drawing.Point(1550, 450);
            this.groupBox_Automatic.Name = "groupBox_Automatic";
            this.groupBox_Automatic.Size = new System.Drawing.Size(110, 310);
            this.groupBox_Automatic.TabIndex = 26;
            this.groupBox_Automatic.TabStop = false;
            this.groupBox_Automatic.Text = "Automatic:";
            // 
            // button_Auto_Zth
            // 
            this.button_Auto_Zth.ImageOptions.Image = global::ATIM_GUI.Properties.Resources.Zth_Symbol_transparent1;
            this.button_Auto_Zth.Location = new System.Drawing.Point(15, 25);
            this.button_Auto_Zth.Name = "button_Auto_Zth";
            this.button_Auto_Zth.Size = new System.Drawing.Size(80, 80);
            this.button_Auto_Zth.TabIndex = 15;
            this.button_Auto_Zth.Click += new System.EventHandler(this.Button_Auto_Zth_Click);
            // 
            // button_Auto_Sensitivity
            // 
            this.button_Auto_Sensitivity.ImageOptions.Image = global::ATIM_GUI.Properties.Resources.kFaktor_Symbol_transparent1;
            this.button_Auto_Sensitivity.Location = new System.Drawing.Point(15, 120);
            this.button_Auto_Sensitivity.Name = "button_Auto_Sensitivity";
            this.button_Auto_Sensitivity.Size = new System.Drawing.Size(80, 80);
            this.button_Auto_Sensitivity.TabIndex = 17;
            this.button_Auto_Sensitivity.Click += new System.EventHandler(this.Button_Auto_Sensitivity_Click);
            // 
            // button_Auto_UI
            // 
            this.button_Auto_UI.Appearance.Font = new System.Drawing.Font("Tahoma", 15F, System.Drawing.FontStyle.Bold);
            this.button_Auto_UI.Appearance.Options.UseFont = true;
            this.button_Auto_UI.Location = new System.Drawing.Point(15, 215);
            this.button_Auto_UI.Name = "button_Auto_UI";
            this.button_Auto_UI.Size = new System.Drawing.Size(80, 80);
            this.button_Auto_UI.TabIndex = 18;
            this.button_Auto_UI.Text = "HELLA \r\nALL";
            this.button_Auto_UI.Click += new System.EventHandler(this.Button_Auto_UI_Click);
            // 
            // groupBox_Cancel
            // 
            this.groupBox_Cancel.Controls.Add(this.button_Cancel);
            this.groupBox_Cancel.Font = new System.Drawing.Font("Tahoma", 9.5F, System.Drawing.FontStyle.Bold);
            this.groupBox_Cancel.Location = new System.Drawing.Point(1550, 770);
            this.groupBox_Cancel.Name = "groupBox_Cancel";
            this.groupBox_Cancel.Size = new System.Drawing.Size(110, 120);
            this.groupBox_Cancel.TabIndex = 27;
            this.groupBox_Cancel.TabStop = false;
            this.groupBox_Cancel.Text = "Cancel:";
            // 
            // splitContainer1
            // 
            this.splitContainer1.AllowDrop = true;
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainer1.Location = new System.Drawing.Point(530, 143);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.chartControl_RAW);
            this.splitContainer1.Panel1.SizeChanged += new System.EventHandler(this.SplitContainer1_Panel1_SizeChanged);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(1000, 840);
            this.splitContainer1.SplitterDistance = 192;
            this.splitContainer1.TabIndex = 29;
            // 
            // chartControl_RAW
            // 
            this.chartControl_RAW.Legend.Name = "Default Legend";
            this.chartControl_RAW.Location = new System.Drawing.Point(3, 3);
            this.chartControl_RAW.Name = "chartControl_RAW";
            this.chartControl_RAW.SeriesSerializable = new DevExpress.XtraCharts.Series[0];
            this.chartControl_RAW.Size = new System.Drawing.Size(991, 191);
            this.chartControl_RAW.TabIndex = 0;
            chartTitle1.Text = "Test";
            this.chartControl_RAW.Titles.AddRange(new DevExpress.XtraCharts.ChartTitle[] {
            chartTitle1});
            // 
            // splitContainer2
            // 
            this.splitContainer2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.chartControl_DATA_Top);
            this.splitContainer2.Panel1.SizeChanged += new System.EventHandler(this.SplitContainer2_Panel1_SizeChanged);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.chartControl_DATA_Bottom);
            this.splitContainer2.Panel2.SizeChanged += new System.EventHandler(this.SplitContainer2_Panel2_SizeChanged);
            this.splitContainer2.Size = new System.Drawing.Size(1000, 644);
            this.splitContainer2.SplitterDistance = 309;
            this.splitContainer2.TabIndex = 0;
            // 
            // chartControl_DATA_Top
            // 
            this.chartControl_DATA_Top.Legend.Name = "Default Legend";
            this.chartControl_DATA_Top.Location = new System.Drawing.Point(3, 3);
            this.chartControl_DATA_Top.Name = "chartControl_DATA_Top";
            this.chartControl_DATA_Top.SeriesSerializable = new DevExpress.XtraCharts.Series[0];
            this.chartControl_DATA_Top.Size = new System.Drawing.Size(991, 322);
            this.chartControl_DATA_Top.TabIndex = 1;
            // 
            // chartControl_DATA_Bottom
            // 
            this.chartControl_DATA_Bottom.Legend.Name = "Default Legend";
            this.chartControl_DATA_Bottom.Location = new System.Drawing.Point(3, 3);
            this.chartControl_DATA_Bottom.Name = "chartControl_DATA_Bottom";
            this.chartControl_DATA_Bottom.SeriesSerializable = new DevExpress.XtraCharts.Series[0];
            this.chartControl_DATA_Bottom.Size = new System.Drawing.Size(991, 322);
            this.chartControl_DATA_Bottom.TabIndex = 2;
            // 
            // statusStrip_MainWindow
            // 
            this.statusStrip_MainWindow.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusBar_Headline,
            this.statusBar_ProgressBar,
            this.statusBar_Detailed});
            this.statusStrip_MainWindow.Location = new System.Drawing.Point(0, 1013);
            this.statusStrip_MainWindow.Name = "statusStrip_MainWindow";
            this.statusStrip_MainWindow.Size = new System.Drawing.Size(1670, 22);
            this.statusStrip_MainWindow.TabIndex = 31;
            this.statusStrip_MainWindow.Text = "statusStrip1";
            // 
            // statusBar_Headline
            // 
            this.statusBar_Headline.Name = "statusBar_Headline";
            this.statusBar_Headline.Size = new System.Drawing.Size(42, 17);
            this.statusBar_Headline.Text = "Status:";
            // 
            // statusBar_ProgressBar
            // 
            this.statusBar_ProgressBar.Name = "statusBar_ProgressBar";
            this.statusBar_ProgressBar.Size = new System.Drawing.Size(100, 16);
            // 
            // statusBar_Detailed
            // 
            this.statusBar_Detailed.Name = "statusBar_Detailed";
            this.statusBar_Detailed.Size = new System.Drawing.Size(23, 17);
            this.statusBar_Detailed.Text = "0%";
            // 
            // ATIM_MainWindow
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(1670, 1035);
            this.Controls.Add(this.statusStrip_MainWindow);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.groupBox_Cancel);
            this.Controls.Add(this.groupBox_Automatic);
            this.Controls.Add(this.groupBox_Single_Meas);
            this.Controls.Add(this.ribbonControl1);
            this.Name = "ATIM_MainWindow";
            this.Ribbon = this.ribbonControl1;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Rth TEC - Measurement software";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.ATIM_MainWindow_FormClosed);
            this.Resize += new System.EventHandler(this.Form1_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl1)).EndInit();
            this.groupBox_Single_Meas.ResumeLayout(false);
            this.groupBox_Automatic.ResumeLayout(false);
            this.groupBox_Cancel.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chartControl_RAW)).EndInit();
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chartControl_DATA_Top)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartControl_DATA_Bottom)).EndInit();
            this.statusStrip_MainWindow.ResumeLayout(false);
            this.statusStrip_MainWindow.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraBars.Ribbon.RibbonControl ribbonControl1;
        private DevExpress.XtraBars.Ribbon.RibbonPage ribbonPage1;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup1;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup2;
        private DevExpress.XtraEditors.SimpleButton button_Single_Zth;
        private DevExpress.XtraEditors.SimpleButton button_Single_Sensitivity;
        private DevExpress.XtraEditors.SimpleButton button_Single_UI;
        private DevExpress.XtraEditors.SimpleButton button_Cancel;
        private System.Windows.Forms.GroupBox groupBox_Single_Meas;
        private System.Windows.Forms.GroupBox groupBox_Automatic;
        private DevExpress.XtraEditors.SimpleButton button_Auto_Zth;
        private DevExpress.XtraEditors.SimpleButton button_Auto_Sensitivity;
        private DevExpress.XtraEditors.SimpleButton button_Auto_UI;
        private System.Windows.Forms.GroupBox groupBox_Cancel;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private DevExpress.XtraCharts.ChartControl chartControl_DATA_Top;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private DevExpress.XtraCharts.ChartControl chartControl_DATA_Bottom;
        private System.Windows.Forms.StatusStrip statusStrip_MainWindow;
        private System.Windows.Forms.ToolStripStatusLabel statusBar_Headline;
        private System.Windows.Forms.ToolStripProgressBar statusBar_ProgressBar;
        private System.Windows.Forms.ToolStripStatusLabel statusBar_Detailed;
        private DevExpress.XtraCharts.ChartControl chartControl_RAW;
        private DevExpress.XtraBars.BarButtonItem ribbonButton_ComSet;
        private DevExpress.XtraBars.BarButtonItem ribbonButton_AutoCon;
        private DevExpress.XtraBars.BarButtonItem ribbonButton_MeasSet;
        private DevExpress.XtraBars.BarButtonItem barButtonItem4;
        private DevExpress.XtraBars.BarButtonItem barButtonItem5;
        private DevExpress.XtraBars.BarButtonItem ribbonButton_Save;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup3;
        private DevExpress.XtraBars.BarButtonItem barButtonItem_SaveSetting;
        private DevExpress.XtraBars.BarButtonItem barButtonItem_Load_Setting;
        private DevExpress.XtraBars.BarButtonItem barButtonItem_FileSavingOpt;
    }
}

