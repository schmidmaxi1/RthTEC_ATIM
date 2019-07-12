namespace RthTEC_Rack
{
    partial class RthTEC_V1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RthTEC_V1));
            this.groupBox_XYZ = new System.Windows.Forms.GroupBox();
            this.button_Enable = new System.Windows.Forms.Button();
            this.Button_OpenClose = new DevExpress.XtraEditors.DropDownButton();
            this.popupMenu1 = new DevExpress.XtraBars.PopupMenu(this.components);
            this.barButtonItem_Reset = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem_Detailed = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem_Log = new DevExpress.XtraBars.BarButtonItem();
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.akt_Position = new System.Windows.Forms.TextBox();
            this.ComPort_select = new System.Windows.Forms.ComboBox();
            this.numericUpDown_Repetations = new System.Windows.Forms.NumericUpDown();
            this.groupBox_XYZ.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.popupMenu1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_Repetations)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox_XYZ
            // 
            this.groupBox_XYZ.Controls.Add(this.numericUpDown_Repetations);
            this.groupBox_XYZ.Controls.Add(this.button_Enable);
            this.groupBox_XYZ.Controls.Add(this.Button_OpenClose);
            this.groupBox_XYZ.Controls.Add(this.label3);
            this.groupBox_XYZ.Controls.Add(this.label1);
            this.groupBox_XYZ.Controls.Add(this.akt_Position);
            this.groupBox_XYZ.Controls.Add(this.ComPort_select);
            this.groupBox_XYZ.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox_XYZ.Location = new System.Drawing.Point(3, 3);
            this.groupBox_XYZ.Name = "groupBox_XYZ";
            this.groupBox_XYZ.Size = new System.Drawing.Size(510, 100);
            this.groupBox_XYZ.TabIndex = 44;
            this.groupBox_XYZ.TabStop = false;
            this.groupBox_XYZ.Text = "RthTEC:";
            // 
            // button_Enable
            // 
            this.button_Enable.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_Enable.Location = new System.Drawing.Point(250, 34);
            this.button_Enable.Name = "button_Enable";
            this.button_Enable.Size = new System.Drawing.Size(90, 21);
            this.button_Enable.TabIndex = 42;
            this.button_Enable.Text = "Enable Output";
            this.button_Enable.UseVisualStyleBackColor = true;
            this.button_Enable.Click += new System.EventHandler(this.Button_Enable_Click);
            // 
            // Button_OpenClose
            // 
            this.Button_OpenClose.DropDownControl = this.popupMenu1;
            this.Button_OpenClose.Location = new System.Drawing.Point(15, 34);
            this.Button_OpenClose.MenuManager = this.barManager1;
            this.Button_OpenClose.Name = "Button_OpenClose";
            this.Button_OpenClose.Size = new System.Drawing.Size(90, 21);
            this.Button_OpenClose.TabIndex = 41;
            this.Button_OpenClose.Text = "Open";
            this.Button_OpenClose.Click += new System.EventHandler(this.Button_OpenClose_Click);
            // 
            // popupMenu1
            // 
            this.popupMenu1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem_Reset),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem_Detailed),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem_Log)});
            this.popupMenu1.Manager = this.barManager1;
            this.popupMenu1.Name = "popupMenu1";
            // 
            // barButtonItem_Reset
            // 
            this.barButtonItem_Reset.Caption = "Reset ...";
            this.barButtonItem_Reset.Enabled = false;
            this.barButtonItem_Reset.Id = 2;
            this.barButtonItem_Reset.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barButtonItem_Reset.ImageOptions.Image")));
            this.barButtonItem_Reset.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("barButtonItem_Reset.ImageOptions.LargeImage")));
            this.barButtonItem_Reset.Name = "barButtonItem_Reset";
            this.barButtonItem_Reset.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.BarButtonItem_Reset_ItemClick);
            // 
            // barButtonItem_Detailed
            // 
            this.barButtonItem_Detailed.Caption = "Detailed ...";
            this.barButtonItem_Detailed.Enabled = false;
            this.barButtonItem_Detailed.Id = 0;
            this.barButtonItem_Detailed.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barButtonItem_Detailed.ImageOptions.Image")));
            this.barButtonItem_Detailed.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("barButtonItem_Detailed.ImageOptions.LargeImage")));
            this.barButtonItem_Detailed.Name = "barButtonItem_Detailed";
            this.barButtonItem_Detailed.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.BarButtonItem_Detailed_ItemClick);
            // 
            // barButtonItem_Log
            // 
            this.barButtonItem_Log.Caption = "Show Log ...";
            this.barButtonItem_Log.Id = 1;
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
            this.barButtonItem_Log,
            this.barButtonItem_Reset});
            this.barManager1.MaxItemId = 3;
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
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 107);
            this.barDockControlBottom.Manager = this.barManager1;
            this.barDockControlBottom.Size = new System.Drawing.Size(516, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 0);
            this.barDockControlLeft.Manager = this.barManager1;
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 107);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(516, 0);
            this.barDockControlRight.Manager = this.barManager1;
            this.barDockControlRight.Size = new System.Drawing.Size(0, 107);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(460, 19);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(40, 13);
            this.label3.TabIndex = 40;
            this.label3.Text = "Status:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(118, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(98, 13);
            this.label1.TabIndex = 39;
            this.label1.Text = "Selected Com-Port:";
            // 
            // akt_Position
            // 
            this.akt_Position.Enabled = false;
            this.akt_Position.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.akt_Position.Location = new System.Drawing.Point(15, 65);
            this.akt_Position.Name = "akt_Position";
            this.akt_Position.ReadOnly = true;
            this.akt_Position.Size = new System.Drawing.Size(485, 20);
            this.akt_Position.TabIndex = 34;
            // 
            // ComPort_select
            // 
            this.ComPort_select.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ComPort_select.FormattingEnabled = true;
            this.ComPort_select.Location = new System.Drawing.Point(120, 34);
            this.ComPort_select.Name = "ComPort_select";
            this.ComPort_select.Size = new System.Drawing.Size(90, 21);
            this.ComPort_select.TabIndex = 26;
            // 
            // numericUpDown_Repetations
            // 
            this.numericUpDown_Repetations.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numericUpDown_Repetations.Location = new System.Drawing.Point(350, 34);
            this.numericUpDown_Repetations.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown_Repetations.Name = "numericUpDown_Repetations";
            this.numericUpDown_Repetations.Size = new System.Drawing.Size(90, 20);
            this.numericUpDown_Repetations.TabIndex = 43;
            this.numericUpDown_Repetations.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown_Repetations.ValueChanged += new System.EventHandler(this.NumericUpDown_Repetations_ValueChanged);
            // 
            // RthTEC_V1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox_XYZ);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "RthTEC_V1";
            this.Size = new System.Drawing.Size(516, 107);
            this.groupBox_XYZ.ResumeLayout(false);
            this.groupBox_XYZ.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.popupMenu1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_Repetations)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.GroupBox groupBox_XYZ;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox akt_Position;
        public System.Windows.Forms.ComboBox ComPort_select;
        private DevExpress.XtraEditors.DropDownButton Button_OpenClose;
        private DevExpress.XtraBars.PopupMenu popupMenu1;
        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.BarButtonItem barButtonItem_Reset;
        private DevExpress.XtraBars.BarButtonItem barButtonItem_Detailed;
        private DevExpress.XtraBars.BarButtonItem barButtonItem_Log;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private System.Windows.Forms.Button button_Enable;
        private System.Windows.Forms.NumericUpDown numericUpDown_Repetations;
    }
}
