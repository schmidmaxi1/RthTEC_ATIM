namespace ATIM_GUI._7_PowerSupply
{
    partial class Hameg_HMP
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Hameg_HMP));
            this.groupBox_HMT = new System.Windows.Forms.GroupBox();
            this.OpenClose = new DevExpress.XtraEditors.DropDownButton();
            this.popupMenu_Hameg = new DevExpress.XtraBars.PopupMenu(this.components);
            this.barButtonItem_Detailed = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem_LOG = new DevExpress.XtraBars.BarButtonItem();
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.ComPort_select = new System.Windows.Forms.ComboBox();
            this.Voltage_Supply = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.Voltage_Heat = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox_HMT.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.popupMenu_Hameg)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Voltage_Supply)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Voltage_Heat)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox_HMT
            // 
            this.groupBox_HMT.Controls.Add(this.OpenClose);
            this.groupBox_HMT.Controls.Add(this.ComPort_select);
            this.groupBox_HMT.Controls.Add(this.Voltage_Supply);
            this.groupBox_HMT.Controls.Add(this.label2);
            this.groupBox_HMT.Controls.Add(this.Voltage_Heat);
            this.groupBox_HMT.Controls.Add(this.label1);
            this.groupBox_HMT.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox_HMT.Location = new System.Drawing.Point(0, 0);
            this.groupBox_HMT.Name = "groupBox_HMT";
            this.groupBox_HMT.Size = new System.Drawing.Size(250, 120);
            this.groupBox_HMT.TabIndex = 1;
            this.groupBox_HMT.TabStop = false;
            this.groupBox_HMT.Text = "Power Supply";
            // 
            // OpenClose
            // 
            this.OpenClose.DropDownControl = this.popupMenu_Hameg;
            this.OpenClose.Location = new System.Drawing.Point(15, 20);
            this.OpenClose.MenuManager = this.barManager1;
            this.OpenClose.Name = "OpenClose";
            this.barManager1.SetPopupContextMenu(this.OpenClose, this.popupMenu_Hameg);
            this.OpenClose.Size = new System.Drawing.Size(95, 23);
            this.OpenClose.TabIndex = 9;
            this.OpenClose.Text = "Open";
            this.OpenClose.Click += new System.EventHandler(this.OpenClose_Click);
            // 
            // popupMenu_Hameg
            // 
            this.popupMenu_Hameg.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem_Detailed),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem_LOG)});
            this.popupMenu_Hameg.Manager = this.barManager1;
            this.popupMenu_Hameg.Name = "popupMenu_Hameg";
            // 
            // barButtonItem_Detailed
            // 
            this.barButtonItem_Detailed.Caption = "Open detailed";
            this.barButtonItem_Detailed.CategoryGuid = new System.Guid("5fd7419d-715d-4789-ab99-2dcc9b930a34");
            this.barButtonItem_Detailed.Enabled = false;
            this.barButtonItem_Detailed.Id = 0;
            this.barButtonItem_Detailed.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barButtonItem_Detailed.ImageOptions.Image")));
            this.barButtonItem_Detailed.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("barButtonItem_Detailed.ImageOptions.LargeImage")));
            this.barButtonItem_Detailed.Name = "barButtonItem_Detailed";
            this.barButtonItem_Detailed.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.BarButtonItem_Detailed_ItemClick);
            // 
            // barButtonItem_LOG
            // 
            this.barButtonItem_LOG.Caption = "Show Log";
            this.barButtonItem_LOG.CategoryGuid = new System.Guid("5fd7419d-715d-4789-ab99-2dcc9b930a34");
            this.barButtonItem_LOG.Id = 1;
            this.barButtonItem_LOG.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barButtonItem_LOG.ImageOptions.Image")));
            this.barButtonItem_LOG.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("barButtonItem_LOG.ImageOptions.LargeImage")));
            this.barButtonItem_LOG.Name = "barButtonItem_LOG";
            this.barButtonItem_LOG.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.BarButtonItem_ShowLog_ItemClick);
            // 
            // barManager1
            // 
            this.barManager1.Categories.AddRange(new DevExpress.XtraBars.BarManagerCategory[] {
            new DevExpress.XtraBars.BarManagerCategory("Allgemein", new System.Guid("5fd7419d-715d-4789-ab99-2dcc9b930a34"))});
            this.barManager1.DockControls.Add(this.barDockControlTop);
            this.barManager1.DockControls.Add(this.barDockControlBottom);
            this.barManager1.DockControls.Add(this.barDockControlLeft);
            this.barManager1.DockControls.Add(this.barDockControlRight);
            this.barManager1.Form = this;
            this.barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.barButtonItem_Detailed,
            this.barButtonItem_LOG});
            this.barManager1.MaxItemId = 2;
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Manager = this.barManager1;
            this.barDockControlTop.Size = new System.Drawing.Size(254, 0);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 130);
            this.barDockControlBottom.Manager = this.barManager1;
            this.barDockControlBottom.Size = new System.Drawing.Size(254, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 0);
            this.barDockControlLeft.Manager = this.barManager1;
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 130);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(254, 0);
            this.barDockControlRight.Manager = this.barManager1;
            this.barDockControlRight.Size = new System.Drawing.Size(0, 130);
            // 
            // ComPort_select
            // 
            this.ComPort_select.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ComPort_select.FormattingEnabled = true;
            this.ComPort_select.Location = new System.Drawing.Point(140, 20);
            this.ComPort_select.Name = "ComPort_select";
            this.ComPort_select.Size = new System.Drawing.Size(95, 21);
            this.ComPort_select.TabIndex = 8;
            // 
            // Voltage_Supply
            // 
            this.Voltage_Supply.DecimalPlaces = 3;
            this.Voltage_Supply.Enabled = false;
            this.Voltage_Supply.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Voltage_Supply.Location = new System.Drawing.Point(140, 90);
            this.Voltage_Supply.Maximum = new decimal(new int[] {
            32,
            0,
            0,
            0});
            this.Voltage_Supply.Name = "Voltage_Supply";
            this.Voltage_Supply.Size = new System.Drawing.Size(95, 20);
            this.Voltage_Supply.TabIndex = 6;
            this.Voltage_Supply.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.Voltage_Supply.UpDownAlign = System.Windows.Forms.LeftRightAlignment.Left;
            this.Voltage_Supply.Value = new decimal(new int[] {
            24,
            0,
            0,
            0});
            this.Voltage_Supply.ValueChanged += new System.EventHandler(this.Voltage_Supply_ValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(17, 92);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 15);
            this.label2.TabIndex = 5;
            this.label2.Text = "V_Supply:";
            // 
            // Voltage_Heat
            // 
            this.Voltage_Heat.BackColor = System.Drawing.SystemColors.Window;
            this.Voltage_Heat.DecimalPlaces = 3;
            this.Voltage_Heat.Enabled = false;
            this.Voltage_Heat.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Voltage_Heat.Location = new System.Drawing.Point(140, 55);
            this.Voltage_Heat.Maximum = new decimal(new int[] {
            40,
            0,
            0,
            0});
            this.Voltage_Heat.Name = "Voltage_Heat";
            this.Voltage_Heat.Size = new System.Drawing.Size(95, 20);
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
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(17, 57);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 15);
            this.label1.TabIndex = 3;
            this.label1.Text = "V_Heat:";
            // 
            // Hameg_HMP
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox_HMT);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "Hameg_HMP";
            this.Size = new System.Drawing.Size(254, 130);
            this.groupBox_HMT.ResumeLayout(false);
            this.groupBox_HMT.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.popupMenu_Hameg)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Voltage_Supply)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Voltage_Heat)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.GroupBox groupBox_HMT;
        private System.Windows.Forms.NumericUpDown Voltage_Supply;
        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.NumericUpDown Voltage_Heat;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.ComboBox ComPort_select;
        private DevExpress.XtraEditors.DropDownButton OpenClose;
        private DevExpress.XtraBars.PopupMenu popupMenu_Hameg;
        private DevExpress.XtraBars.BarButtonItem barButtonItem_Detailed;
        private DevExpress.XtraBars.BarButtonItem barButtonItem_LOG;
        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
    }
}
