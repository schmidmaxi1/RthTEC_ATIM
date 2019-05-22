namespace TEC_Controller
{
    partial class Meerstetter_4fach
    {
        /// <summary> 
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Komponenten-Designer generierter Code

        /// <summary> 
        /// Erforderliche Methode für die Designerunterstützung. 
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Meerstetter_4fach));
            this.groupBox_TEC = new System.Windows.Forms.GroupBox();
            this.UI_MeasuredTemp2 = new System.Windows.Forms.TextBox();
            this.ComPort_select2 = new System.Windows.Forms.ComboBox();
            this.OpenClose = new DevExpress.XtraEditors.DropDownButton();
            this.popupMenu_TEC = new DevExpress.XtraBars.PopupMenu(this.components);
            this.barButtonItem_OnOff = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem_Fan = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem_Detailed = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem_Log = new DevExpress.XtraBars.BarButtonItem();
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.label41 = new System.Windows.Forms.Label();
            this.label33 = new System.Windows.Forms.Label();
            this.label32 = new System.Windows.Forms.Label();
            this.UI_MeasuredTemp1 = new System.Windows.Forms.TextBox();
            this.UI_TargetTemp = new System.Windows.Forms.NumericUpDown();
            this.ComPort_select1 = new System.Windows.Forms.ComboBox();
            this.groupBox_TEC.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.popupMenu_TEC)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.UI_TargetTemp)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox_TEC
            // 
            this.groupBox_TEC.Controls.Add(this.UI_MeasuredTemp2);
            this.groupBox_TEC.Controls.Add(this.ComPort_select2);
            this.groupBox_TEC.Controls.Add(this.OpenClose);
            this.groupBox_TEC.Controls.Add(this.label41);
            this.groupBox_TEC.Controls.Add(this.label33);
            this.groupBox_TEC.Controls.Add(this.label32);
            this.groupBox_TEC.Controls.Add(this.UI_MeasuredTemp1);
            this.groupBox_TEC.Controls.Add(this.UI_TargetTemp);
            this.groupBox_TEC.Controls.Add(this.ComPort_select1);
            this.groupBox_TEC.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox_TEC.Location = new System.Drawing.Point(0, 0);
            this.groupBox_TEC.Name = "groupBox_TEC";
            this.groupBox_TEC.Size = new System.Drawing.Size(510, 87);
            this.groupBox_TEC.TabIndex = 46;
            this.groupBox_TEC.TabStop = false;
            this.groupBox_TEC.Text = "TEC-Controller: Meerstetter 4fach";
            // 
            // UI_MeasuredTemp2
            // 
            this.UI_MeasuredTemp2.Enabled = false;
            this.UI_MeasuredTemp2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.UI_MeasuredTemp2.Location = new System.Drawing.Point(333, 58);
            this.UI_MeasuredTemp2.Name = "UI_MeasuredTemp2";
            this.UI_MeasuredTemp2.ReadOnly = true;
            this.UI_MeasuredTemp2.Size = new System.Drawing.Size(169, 20);
            this.UI_MeasuredTemp2.TabIndex = 43;
            this.UI_MeasuredTemp2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // ComPort_select2
            // 
            this.ComPort_select2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ComPort_select2.FormattingEnabled = true;
            this.ComPort_select2.Location = new System.Drawing.Point(140, 57);
            this.ComPort_select2.Name = "ComPort_select2";
            this.ComPort_select2.Size = new System.Drawing.Size(95, 21);
            this.ComPort_select2.TabIndex = 42;
            // 
            // OpenClose
            // 
            this.OpenClose.DropDownControl = this.popupMenu_TEC;
            this.OpenClose.Location = new System.Drawing.Point(15, 30);
            this.OpenClose.MenuManager = this.barManager1;
            this.OpenClose.Name = "OpenClose";
            this.OpenClose.Size = new System.Drawing.Size(95, 23);
            this.OpenClose.TabIndex = 41;
            this.OpenClose.Text = "Open";
            this.OpenClose.Click += new System.EventHandler(this.OpenClose_Click);
            // 
            // popupMenu_TEC
            // 
            this.popupMenu_TEC.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem_OnOff),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem_Fan),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem_Detailed),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem_Log)});
            this.popupMenu_TEC.Manager = this.barManager1;
            this.popupMenu_TEC.Name = "popupMenu_TEC";
            // 
            // barButtonItem_OnOff
            // 
            this.barButtonItem_OnOff.Caption = "Switch TEC off";
            this.barButtonItem_OnOff.Enabled = false;
            this.barButtonItem_OnOff.Id = 2;
            this.barButtonItem_OnOff.ImageOptions.Image = global::TEC_Controller.Properties.Resources.OnOff_16x16;
            this.barButtonItem_OnOff.ImageOptions.LargeImage = global::TEC_Controller.Properties.Resources.OnOff_16x16;
            this.barButtonItem_OnOff.Name = "barButtonItem_OnOff";
            this.barButtonItem_OnOff.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.BarButtonItem_OnOff_ItemClick);
            // 
            // barButtonItem_Fan
            // 
            this.barButtonItem_Fan.Caption = "Switch fan off";
            this.barButtonItem_Fan.Enabled = false;
            this.barButtonItem_Fan.Id = 3;
            this.barButtonItem_Fan.ImageOptions.Image = global::TEC_Controller.Properties.Resources.fan_16x16;
            this.barButtonItem_Fan.Name = "barButtonItem_Fan";
            this.barButtonItem_Fan.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.BarButtonItem_Fan_ItemClick);
            // 
            // barButtonItem_Detailed
            // 
            this.barButtonItem_Detailed.Caption = "Open detailed";
            this.barButtonItem_Detailed.Id = 0;
            this.barButtonItem_Detailed.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barButtonItem_Detailed.ImageOptions.Image")));
            this.barButtonItem_Detailed.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("barButtonItem_Detailed.ImageOptions.LargeImage")));
            this.barButtonItem_Detailed.Name = "barButtonItem_Detailed";
            this.barButtonItem_Detailed.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.BarButtonItem_Detailed_ItemClick);
            // 
            // barButtonItem_Log
            // 
            this.barButtonItem_Log.Caption = "Show Log";
            this.barButtonItem_Log.Id = 1;
            this.barButtonItem_Log.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barButtonItem_Log.ImageOptions.Image")));
            this.barButtonItem_Log.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("barButtonItem_Log.ImageOptions.LargeImage")));
            this.barButtonItem_Log.Name = "barButtonItem_Log";
            this.barButtonItem_Log.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.BarButtonItem_LOG_ItemClick);
            // 
            // barManager1
            // 
            this.barManager1.DockControls.Add(this.barDockControlTop);
            this.barManager1.DockControls.Add(this.barDockControlBottom);
            this.barManager1.DockControls.Add(this.barDockControlLeft);
            this.barManager1.DockControls.Add(this.barDockControlRight);
            this.barManager1.Form = this;
            this.barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.barButtonItem_Detailed,
            this.barButtonItem_Log,
            this.barButtonItem_OnOff,
            this.barButtonItem_Fan});
            this.barManager1.MaxItemId = 4;
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Manager = this.barManager1;
            this.barDockControlTop.Size = new System.Drawing.Size(516, 0);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 93);
            this.barDockControlBottom.Manager = this.barManager1;
            this.barDockControlBottom.Size = new System.Drawing.Size(516, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 0);
            this.barDockControlLeft.Manager = this.barManager1;
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 93);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(516, 0);
            this.barDockControlRight.Manager = this.barManager1;
            this.barDockControlRight.Size = new System.Drawing.Size(0, 93);
            // 
            // label41
            // 
            this.label41.AutoSize = true;
            this.label41.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label41.Location = new System.Drawing.Point(309, 32);
            this.label41.Name = "label41";
            this.label41.Size = new System.Drawing.Size(18, 13);
            this.label41.TabIndex = 40;
            this.label41.Text = "°C";
            // 
            // label33
            // 
            this.label33.AutoSize = true;
            this.label33.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label33.Location = new System.Drawing.Point(332, 15);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(57, 13);
            this.label33.TabIndex = 37;
            this.label33.Text = "Measured:";
            // 
            // label32
            // 
            this.label32.AutoSize = true;
            this.label32.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label32.Location = new System.Drawing.Point(257, 15);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(41, 13);
            this.label32.TabIndex = 36;
            this.label32.Text = "Target:";
            // 
            // UI_MeasuredTemp1
            // 
            this.UI_MeasuredTemp1.Enabled = false;
            this.UI_MeasuredTemp1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.UI_MeasuredTemp1.Location = new System.Drawing.Point(333, 30);
            this.UI_MeasuredTemp1.Name = "UI_MeasuredTemp1";
            this.UI_MeasuredTemp1.ReadOnly = true;
            this.UI_MeasuredTemp1.Size = new System.Drawing.Size(169, 20);
            this.UI_MeasuredTemp1.TabIndex = 34;
            this.UI_MeasuredTemp1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // UI_TargetTemp
            // 
            this.UI_TargetTemp.Enabled = false;
            this.UI_TargetTemp.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.UI_TargetTemp.Location = new System.Drawing.Point(259, 30);
            this.UI_TargetTemp.Name = "UI_TargetTemp";
            this.UI_TargetTemp.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.UI_TargetTemp.Size = new System.Drawing.Size(47, 20);
            this.UI_TargetTemp.TabIndex = 33;
            this.UI_TargetTemp.Value = new decimal(new int[] {
            25,
            0,
            0,
            0});
            this.UI_TargetTemp.ValueChanged += new System.EventHandler(this.UI_TargetTemp_ValueChanged);
            // 
            // ComPort_select1
            // 
            this.ComPort_select1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ComPort_select1.FormattingEnabled = true;
            this.ComPort_select1.Location = new System.Drawing.Point(140, 30);
            this.ComPort_select1.Name = "ComPort_select1";
            this.ComPort_select1.Size = new System.Drawing.Size(95, 21);
            this.ComPort_select1.TabIndex = 26;
            // 
            // Meerstetter_4fach
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox_TEC);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "Meerstetter_4fach";
            this.Size = new System.Drawing.Size(516, 93);
            this.groupBox_TEC.ResumeLayout(false);
            this.groupBox_TEC.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.popupMenu_TEC)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.UI_TargetTemp)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        public System.Windows.Forms.GroupBox groupBox_TEC;
        public DevExpress.XtraEditors.DropDownButton OpenClose;
        private System.Windows.Forms.Label label41;
        private System.Windows.Forms.Label label33;
        private System.Windows.Forms.Label label32;
        private System.Windows.Forms.TextBox UI_MeasuredTemp1;
        public System.Windows.Forms.NumericUpDown UI_TargetTemp;
        public System.Windows.Forms.ComboBox ComPort_select1;
        private System.Windows.Forms.TextBox UI_MeasuredTemp2;
        public System.Windows.Forms.ComboBox ComPort_select2;
        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraBars.BarButtonItem barButtonItem_Detailed;
        private DevExpress.XtraBars.BarButtonItem barButtonItem_Log;
        public DevExpress.XtraBars.BarButtonItem barButtonItem_OnOff;
        public DevExpress.XtraBars.BarButtonItem barButtonItem_Fan;
        private DevExpress.XtraBars.PopupMenu popupMenu_TEC;
    }
}
