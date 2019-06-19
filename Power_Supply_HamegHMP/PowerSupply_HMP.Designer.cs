namespace Power_Supply_HamegHMP
{
    partial class PowerSupply_HMP
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PowerSupply_HMP));
            this.ComPort_select = new System.Windows.Forms.ComboBox();
            this.Voltage_Heat = new System.Windows.Forms.NumericUpDown();
            this.groupBox_HMT = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.button_OpenClose = new DevExpress.XtraEditors.DropDownButton();
            this.popupMenu_OpenClose = new DevExpress.XtraBars.PopupMenu();
            this.barButtonItem_Detailed = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem_Log = new DevExpress.XtraBars.BarButtonItem();
            this.barManager1 = new DevExpress.XtraBars.BarManager();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            ((System.ComponentModel.ISupportInitialize)(this.Voltage_Heat)).BeginInit();
            this.groupBox_HMT.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.popupMenu_OpenClose)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            this.SuspendLayout();
            // 
            // ComPort_select
            // 
            this.ComPort_select.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ComPort_select.FormattingEnabled = true;
            this.ComPort_select.Location = new System.Drawing.Point(120, 34);
            this.ComPort_select.Name = "ComPort_select";
            this.ComPort_select.Size = new System.Drawing.Size(90, 21);
            this.ComPort_select.TabIndex = 8;
            // 
            // Voltage_Heat
            // 
            this.Voltage_Heat.BackColor = System.Drawing.SystemColors.Window;
            this.Voltage_Heat.DecimalPlaces = 2;
            this.Voltage_Heat.Enabled = false;
            this.Voltage_Heat.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Voltage_Heat.Location = new System.Drawing.Point(250, 34);
            this.Voltage_Heat.Maximum = new decimal(new int[] {
            40,
            0,
            0,
            0});
            this.Voltage_Heat.Name = "Voltage_Heat";
            this.Voltage_Heat.Size = new System.Drawing.Size(47, 20);
            this.Voltage_Heat.TabIndex = 4;
            this.Voltage_Heat.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.Voltage_Heat.UpDownAlign = System.Windows.Forms.LeftRightAlignment.Left;
            this.Voltage_Heat.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.Voltage_Heat.ValueChanged += new System.EventHandler(this.Voltage_Heat_ValueChanged);
            // 
            // groupBox_HMT
            // 
            this.groupBox_HMT.Controls.Add(this.label4);
            this.groupBox_HMT.Controls.Add(this.label3);
            this.groupBox_HMT.Controls.Add(this.label1);
            this.groupBox_HMT.Controls.Add(this.label2);
            this.groupBox_HMT.Controls.Add(this.button_OpenClose);
            this.groupBox_HMT.Controls.Add(this.ComPort_select);
            this.groupBox_HMT.Controls.Add(this.Voltage_Heat);
            this.groupBox_HMT.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox_HMT.Location = new System.Drawing.Point(0, 0);
            this.groupBox_HMT.Name = "groupBox_HMT";
            this.groupBox_HMT.Size = new System.Drawing.Size(510, 70);
            this.groupBox_HMT.TabIndex = 2;
            this.groupBox_HMT.TabStop = false;
            this.groupBox_HMT.Text = "Power Supply";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(299, 36);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(14, 13);
            this.label4.TabIndex = 13;
            this.label4.Text = "V";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(460, 19);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(40, 13);
            this.label3.TabIndex = 12;
            this.label3.Text = "Status:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(118, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(98, 13);
            this.label1.TabIndex = 11;
            this.label1.Text = "Selected Com-Port:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(250, 19);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(46, 13);
            this.label2.TabIndex = 10;
            this.label2.Text = "V_Heat:";
            // 
            // button_OpenClose
            // 
            this.button_OpenClose.DropDownControl = this.popupMenu_OpenClose;
            this.button_OpenClose.Location = new System.Drawing.Point(15, 34);
            this.button_OpenClose.MenuManager = this.barManager1;
            this.button_OpenClose.Name = "button_OpenClose";
            this.button_OpenClose.Size = new System.Drawing.Size(90, 21);
            this.button_OpenClose.TabIndex = 9;
            this.button_OpenClose.Text = "Open";
            this.button_OpenClose.Click += new System.EventHandler(this.OpenClose_Click);
            // 
            // popupMenu_OpenClose
            // 
            this.popupMenu_OpenClose.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem_Detailed),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem_Log)});
            this.popupMenu_OpenClose.Manager = this.barManager1;
            this.popupMenu_OpenClose.Name = "popupMenu_OpenClose";
            // 
            // barButtonItem_Detailed
            // 
            this.barButtonItem_Detailed.Caption = "Open detailed";
            this.barButtonItem_Detailed.Id = 1;
            this.barButtonItem_Detailed.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barButtonItem_Detailed.ImageOptions.Image")));
            this.barButtonItem_Detailed.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("barButtonItem_Detailed.ImageOptions.LargeImage")));
            this.barButtonItem_Detailed.Name = "barButtonItem_Detailed";
            this.barButtonItem_Detailed.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.BarButtonItem_Detailed_ItemClick);
            // 
            // barButtonItem_Log
            // 
            this.barButtonItem_Log.Caption = "Show Log";
            this.barButtonItem_Log.Id = 0;
            this.barButtonItem_Log.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barButtonItem_Log.ImageOptions.Image")));
            this.barButtonItem_Log.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("barButtonItem_Log.ImageOptions.LargeImage")));
            this.barButtonItem_Log.Name = "barButtonItem_Log";
            this.barButtonItem_Log.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.BarButtonItem_ShowLog_ItemClick);
            // 
            // barManager1
            // 
            this.barManager1.DockControls.Add(this.barDockControlTop);
            this.barManager1.DockControls.Add(this.barDockControlBottom);
            this.barManager1.DockControls.Add(this.barDockControlLeft);
            this.barManager1.DockControls.Add(this.barDockControlRight);
            this.barManager1.Form = this;
            this.barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.barButtonItem_Log,
            this.barButtonItem_Detailed});
            this.barManager1.MaxItemId = 2;
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Manager = this.barManager1;
            this.barDockControlTop.Size = new System.Drawing.Size(515, 0);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 75);
            this.barDockControlBottom.Manager = this.barManager1;
            this.barDockControlBottom.Size = new System.Drawing.Size(515, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 0);
            this.barDockControlLeft.Manager = this.barManager1;
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 75);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(515, 0);
            this.barDockControlRight.Manager = this.barManager1;
            this.barDockControlRight.Size = new System.Drawing.Size(0, 75);
            // 
            // PowerSupply_HMP
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox_HMT);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "PowerSupply_HMP";
            this.Size = new System.Drawing.Size(515, 75);
            ((System.ComponentModel.ISupportInitialize)(this.Voltage_Heat)).EndInit();
            this.groupBox_HMT.ResumeLayout(false);
            this.groupBox_HMT.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.popupMenu_OpenClose)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.ComboBox ComPort_select;
        public System.Windows.Forms.NumericUpDown Voltage_Heat;
        public System.Windows.Forms.GroupBox groupBox_HMT;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private DevExpress.XtraBars.PopupMenu popupMenu_OpenClose;
        private DevExpress.XtraBars.BarButtonItem barButtonItem_Log;
        private DevExpress.XtraBars.BarButtonItem barButtonItem_Detailed;
        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        public DevExpress.XtraEditors.DropDownButton button_OpenClose;
        private System.Windows.Forms.Label label4;
    }
}
