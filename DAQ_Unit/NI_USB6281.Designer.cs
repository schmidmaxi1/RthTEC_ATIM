namespace DAQ_Units
{
    partial class NI_USB6281
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NI_USB6281));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.Voltage_Trigger = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.Channel_select = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.button_OpenClose = new DevExpress.XtraEditors.DropDownButton();
            this.popupMenu1 = new DevExpress.XtraBars.PopupMenu(this.components);
            this.barButtonItem_Detailed = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem_Log = new DevExpress.XtraBars.BarButtonItem();
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Voltage_Trigger)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupMenu1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.Voltage_Trigger);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.Channel_select);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.button_OpenClose);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(510, 70);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "DAQ: NI-USB6281";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(298, 36);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(14, 13);
            this.label4.TabIndex = 48;
            this.label4.Text = "V";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(249, 19);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 13);
            this.label2.TabIndex = 47;
            this.label2.Text = "V_Trigger:";
            // 
            // Voltage_Trigger
            // 
            this.Voltage_Trigger.BackColor = System.Drawing.SystemColors.Window;
            this.Voltage_Trigger.DecimalPlaces = 2;
            this.Voltage_Trigger.Enabled = false;
            this.Voltage_Trigger.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Voltage_Trigger.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.Voltage_Trigger.Location = new System.Drawing.Point(249, 34);
            this.Voltage_Trigger.Maximum = new decimal(new int[] {
            40,
            0,
            0,
            0});
            this.Voltage_Trigger.Name = "Voltage_Trigger";
            this.Voltage_Trigger.Size = new System.Drawing.Size(47, 20);
            this.Voltage_Trigger.TabIndex = 46;
            this.Voltage_Trigger.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.Voltage_Trigger.UpDownAlign = System.Windows.Forms.LeftRightAlignment.Left;
            this.Voltage_Trigger.Value = new decimal(new int[] {
            28,
            0,
            0,
            65536});
            this.Voltage_Trigger.ValueChanged += new System.EventHandler(this.Voltage_Trigger_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(118, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(94, 13);
            this.label1.TabIndex = 45;
            this.label1.Text = "Selected Channel:";
            // 
            // Channel_select
            // 
            this.Channel_select.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Channel_select.FormattingEnabled = true;
            this.Channel_select.Location = new System.Drawing.Point(120, 34);
            this.Channel_select.Name = "Channel_select";
            this.Channel_select.Size = new System.Drawing.Size(90, 21);
            this.Channel_select.TabIndex = 44;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(453, 18);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(40, 13);
            this.label3.TabIndex = 43;
            this.label3.Text = "Status:";
            // 
            // button_OpenClose
            // 
            this.button_OpenClose.DropDownControl = this.popupMenu1;
            this.button_OpenClose.Location = new System.Drawing.Point(15, 34);
            this.button_OpenClose.MenuManager = this.barManager1;
            this.button_OpenClose.Name = "button_OpenClose";
            this.button_OpenClose.Size = new System.Drawing.Size(90, 21);
            this.button_OpenClose.TabIndex = 0;
            this.button_OpenClose.Text = "Open";
            this.button_OpenClose.Click += new System.EventHandler(this.Button_OpenClose_Click);
            // 
            // popupMenu1
            // 
            this.popupMenu1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem_Detailed),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem_Log)});
            this.popupMenu1.Manager = this.barManager1;
            this.popupMenu1.Name = "popupMenu1";
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
            this.barButtonItem_Log.Id = 2;
            this.barButtonItem_Log.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barButtonItem_Log.ImageOptions.Image")));
            this.barButtonItem_Log.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("barButtonItem_Log.ImageOptions.LargeImage")));
            this.barButtonItem_Log.Name = "barButtonItem_Log";
            this.barButtonItem_Log.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.BarButtonItem_Log_ItemClick);
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
            this.barButtonItem_Log});
            this.barManager1.MaxItemId = 3;
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
            // NI_USB6281
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "NI_USB6281";
            this.Size = new System.Drawing.Size(515, 75);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Voltage_Trigger)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupMenu1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private DevExpress.XtraEditors.DropDownButton button_OpenClose;
        private DevExpress.XtraBars.PopupMenu popupMenu1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.ComboBox Channel_select;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.NumericUpDown Voltage_Trigger;
        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.BarButtonItem barButtonItem_Detailed;
        private DevExpress.XtraBars.BarButtonItem barButtonItem_Log;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
    }
}
