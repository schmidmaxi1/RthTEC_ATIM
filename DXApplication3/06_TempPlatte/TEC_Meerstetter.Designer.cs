namespace ATIM_GUI._6_TempPlatte
{
    partial class TEC_Meerstetter
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TEC_Meerstetter));
            this.groupBox_TEC = new System.Windows.Forms.GroupBox();
            this.OpenClose = new DevExpress.XtraEditors.DropDownButton();
            this.popupMenu_TEC = new DevExpress.XtraBars.PopupMenu(this.components);
            this.barButtonItem_Detailed = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem2 = new DevExpress.XtraBars.BarButtonItem();
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.label41 = new System.Windows.Forms.Label();
            this.Switch_OnOff = new System.Windows.Forms.Button();
            this.label33 = new System.Windows.Forms.Label();
            this.label32 = new System.Windows.Forms.Label();
            this.UI_MeasuredTemp = new System.Windows.Forms.TextBox();
            this.UI_TargetTemp = new System.Windows.Forms.NumericUpDown();
            this.ComPort_select = new System.Windows.Forms.ComboBox();
            this.groupBox_TEC.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.popupMenu_TEC)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.UI_TargetTemp)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox_TEC
            // 
            this.groupBox_TEC.Controls.Add(this.OpenClose);
            this.groupBox_TEC.Controls.Add(this.label41);
            this.groupBox_TEC.Controls.Add(this.Switch_OnOff);
            this.groupBox_TEC.Controls.Add(this.label33);
            this.groupBox_TEC.Controls.Add(this.label32);
            this.groupBox_TEC.Controls.Add(this.UI_MeasuredTemp);
            this.groupBox_TEC.Controls.Add(this.UI_TargetTemp);
            this.groupBox_TEC.Controls.Add(this.ComPort_select);
            this.groupBox_TEC.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox_TEC.Location = new System.Drawing.Point(0, 0);
            this.groupBox_TEC.Name = "groupBox_TEC";
            this.groupBox_TEC.Size = new System.Drawing.Size(250, 130);
            this.groupBox_TEC.TabIndex = 41;
            this.groupBox_TEC.TabStop = false;
            this.groupBox_TEC.Text = "TEC-Controller:";
            // 
            // OpenClose
            // 
            this.OpenClose.DropDownControl = this.popupMenu_TEC;
            this.OpenClose.Location = new System.Drawing.Point(15, 20);
            this.OpenClose.MenuManager = this.barManager1;
            this.OpenClose.Name = "OpenClose";
            this.OpenClose.Size = new System.Drawing.Size(95, 23);
            this.OpenClose.TabIndex = 41;
            this.OpenClose.Text = "Open & init";
            this.OpenClose.Click += new System.EventHandler(this.OpenClose_Click);
            // 
            // popupMenu_TEC
            // 
            this.popupMenu_TEC.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem_Detailed),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem2)});
            this.popupMenu_TEC.Manager = this.barManager1;
            this.popupMenu_TEC.Name = "popupMenu_TEC";
            // 
            // barButtonItem_Detailed
            // 
            this.barButtonItem_Detailed.Caption = "Open detailed";
            this.barButtonItem_Detailed.Enabled = false;
            this.barButtonItem_Detailed.Id = 0;
            this.barButtonItem_Detailed.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barButtonItem_Detailed.ImageOptions.Image")));
            this.barButtonItem_Detailed.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("barButtonItem_Detailed.ImageOptions.LargeImage")));
            this.barButtonItem_Detailed.Name = "barButtonItem_Detailed";
            this.barButtonItem_Detailed.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.BarButtonItem1_ItemClick);
            // 
            // barButtonItem2
            // 
            this.barButtonItem2.Caption = "Show Log";
            this.barButtonItem2.Id = 1;
            this.barButtonItem2.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barButtonItem2.ImageOptions.Image")));
            this.barButtonItem2.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("barButtonItem2.ImageOptions.LargeImage")));
            this.barButtonItem2.Name = "barButtonItem2";
            this.barButtonItem2.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.BarButtonItem2_ItemClick);
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
            this.barButtonItem2});
            this.barManager1.MaxItemId = 2;
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Manager = this.barManager1;
            this.barDockControlTop.Size = new System.Drawing.Size(256, 0);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 135);
            this.barDockControlBottom.Manager = this.barManager1;
            this.barDockControlBottom.Size = new System.Drawing.Size(256, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 0);
            this.barDockControlLeft.Manager = this.barManager1;
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 135);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(256, 0);
            this.barDockControlRight.Manager = this.barManager1;
            this.barDockControlRight.Size = new System.Drawing.Size(0, 135);
            // 
            // label41
            // 
            this.label41.AutoSize = true;
            this.label41.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label41.Location = new System.Drawing.Point(92, 62);
            this.label41.Name = "label41";
            this.label41.Size = new System.Drawing.Size(18, 13);
            this.label41.TabIndex = 40;
            this.label41.Text = "°C";
            // 
            // Switch_OnOff
            // 
            this.Switch_OnOff.Enabled = false;
            this.Switch_OnOff.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Switch_OnOff.Location = new System.Drawing.Point(140, 57);
            this.Switch_OnOff.Name = "Switch_OnOff";
            this.Switch_OnOff.Size = new System.Drawing.Size(95, 23);
            this.Switch_OnOff.TabIndex = 39;
            this.Switch_OnOff.Text = "Switch On";
            this.Switch_OnOff.UseVisualStyleBackColor = true;
            this.Switch_OnOff.Click += new System.EventHandler(this.Switch_OnOff_Click);
            // 
            // label33
            // 
            this.label33.AutoSize = true;
            this.label33.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label33.Location = new System.Drawing.Point(13, 85);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(57, 13);
            this.label33.TabIndex = 37;
            this.label33.Text = "Measured:";
            // 
            // label32
            // 
            this.label32.AutoSize = true;
            this.label32.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label32.Location = new System.Drawing.Point(13, 45);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(41, 13);
            this.label32.TabIndex = 36;
            this.label32.Text = "Target:";
            // 
            // UI_MeasuredTemp
            // 
            this.UI_MeasuredTemp.Enabled = false;
            this.UI_MeasuredTemp.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.UI_MeasuredTemp.Location = new System.Drawing.Point(15, 100);
            this.UI_MeasuredTemp.Name = "UI_MeasuredTemp";
            this.UI_MeasuredTemp.ReadOnly = true;
            this.UI_MeasuredTemp.Size = new System.Drawing.Size(220, 20);
            this.UI_MeasuredTemp.TabIndex = 34;
            this.UI_MeasuredTemp.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // UI_TargetTemp
            // 
            this.UI_TargetTemp.Enabled = false;
            this.UI_TargetTemp.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.UI_TargetTemp.Location = new System.Drawing.Point(15, 60);
            this.UI_TargetTemp.Name = "UI_TargetTemp";
            this.UI_TargetTemp.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.UI_TargetTemp.Size = new System.Drawing.Size(71, 20);
            this.UI_TargetTemp.TabIndex = 33;
            this.UI_TargetTemp.Value = new decimal(new int[] {
            25,
            0,
            0,
            0});
            this.UI_TargetTemp.ValueChanged += new System.EventHandler(this.UI_TargetTemp_ValueChanged);
            // 
            // ComPort_select
            // 
            this.ComPort_select.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ComPort_select.FormattingEnabled = true;
            this.ComPort_select.Location = new System.Drawing.Point(140, 20);
            this.ComPort_select.Name = "ComPort_select";
            this.ComPort_select.Size = new System.Drawing.Size(95, 21);
            this.ComPort_select.TabIndex = 26;
            // 
            // TEC_Meerstetter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox_TEC);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "TEC_Meerstetter";
            this.Size = new System.Drawing.Size(256, 135);
            this.groupBox_TEC.ResumeLayout(false);
            this.groupBox_TEC.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.popupMenu_TEC)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.UI_TargetTemp)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox_TEC;
        private System.Windows.Forms.Label label41;
        public System.Windows.Forms.Button Switch_OnOff;
        private System.Windows.Forms.Label label33;
        private System.Windows.Forms.Label label32;
        private System.Windows.Forms.TextBox UI_MeasuredTemp;
        private System.Windows.Forms.NumericUpDown UI_TargetTemp;
        public System.Windows.Forms.ComboBox ComPort_select;
        private DevExpress.XtraEditors.DropDownButton OpenClose;
        private DevExpress.XtraBars.PopupMenu popupMenu_TEC;
        private DevExpress.XtraBars.BarButtonItem barButtonItem2;
        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        public DevExpress.XtraBars.BarButtonItem barButtonItem_Detailed;
    }
}
