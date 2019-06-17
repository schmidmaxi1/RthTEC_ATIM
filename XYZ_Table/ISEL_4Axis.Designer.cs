namespace XYZ_Table
{
    partial class ISEL_4Axis
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ISEL_4Axis));
            this.label33 = new System.Windows.Forms.Label();
            this.akt_Position = new System.Windows.Forms.TextBox();
            this.ComPort_select = new System.Windows.Forms.ComboBox();
            this.groupBox_XYZ = new System.Windows.Forms.GroupBox();
            this.button_OpenClose = new DevExpress.XtraEditors.DropDownButton();
            this.popupMenu_XYZ = new DevExpress.XtraBars.PopupMenu(this.components);
            this.barButtonItem_Init = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem_REF = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem_Manual = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem_Log = new DevExpress.XtraBars.BarButtonItem();
            this.barManager_XYZ = new DevExpress.XtraBars.BarManager(this.components);
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.groupBox_XYZ.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.popupMenu_XYZ)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager_XYZ)).BeginInit();
            this.SuspendLayout();
            // 
            // label33
            // 
            this.label33.AutoSize = true;
            this.label33.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label33.Location = new System.Drawing.Point(238, 14);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(47, 13);
            this.label33.TabIndex = 37;
            this.label33.Text = "Position:";
            // 
            // akt_Position
            // 
            this.akt_Position.Enabled = false;
            this.akt_Position.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.akt_Position.Location = new System.Drawing.Point(241, 30);
            this.akt_Position.Name = "akt_Position";
            this.akt_Position.ReadOnly = true;
            this.akt_Position.Size = new System.Drawing.Size(261, 20);
            this.akt_Position.TabIndex = 34;
            this.akt_Position.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // ComPort_select
            // 
            this.ComPort_select.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ComPort_select.FormattingEnabled = true;
            this.ComPort_select.Location = new System.Drawing.Point(140, 30);
            this.ComPort_select.Name = "ComPort_select";
            this.ComPort_select.Size = new System.Drawing.Size(95, 21);
            this.ComPort_select.TabIndex = 26;
            // 
            // groupBox_XYZ
            // 
            this.groupBox_XYZ.Controls.Add(this.button_OpenClose);
            this.groupBox_XYZ.Controls.Add(this.label33);
            this.groupBox_XYZ.Controls.Add(this.akt_Position);
            this.groupBox_XYZ.Controls.Add(this.ComPort_select);
            this.groupBox_XYZ.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox_XYZ.Location = new System.Drawing.Point(3, 3);
            this.groupBox_XYZ.Name = "groupBox_XYZ";
            this.groupBox_XYZ.Size = new System.Drawing.Size(510, 66);
            this.groupBox_XYZ.TabIndex = 43;
            this.groupBox_XYZ.TabStop = false;
            this.groupBox_XYZ.Text = "XYZ: Isel 4Axis";
            // 
            // button_OpenClose
            // 
            this.button_OpenClose.DropDownControl = this.popupMenu_XYZ;
            this.button_OpenClose.Location = new System.Drawing.Point(6, 27);
            this.button_OpenClose.MenuManager = this.barManager_XYZ;
            this.button_OpenClose.Name = "button_OpenClose";
            this.button_OpenClose.Size = new System.Drawing.Size(115, 23);
            this.button_OpenClose.TabIndex = 38;
            this.button_OpenClose.Text = "Open";
            this.button_OpenClose.Click += new System.EventHandler(this.Button_OpenClose_Click);
            // 
            // popupMenu_XYZ
            // 
            this.popupMenu_XYZ.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem_Init),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem_REF),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem_Manual),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem_Log)});
            this.popupMenu_XYZ.Manager = this.barManager_XYZ;
            this.popupMenu_XYZ.Name = "popupMenu_XYZ";
            // 
            // barButtonItem_Init
            // 
            this.barButtonItem_Init.Caption = "Initialization";
            this.barButtonItem_Init.Id = 0;
            this.barButtonItem_Init.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barButtonItem_Init.ImageOptions.Image")));
            this.barButtonItem_Init.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("barButtonItem_Init.ImageOptions.LargeImage")));
            this.barButtonItem_Init.Name = "barButtonItem_Init";
            this.barButtonItem_Init.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.BarButtonItem_Init_ItemClick);
            // 
            // barButtonItem_REF
            // 
            this.barButtonItem_REF.Caption = "Reference drive";
            this.barButtonItem_REF.Id = 1;
            this.barButtonItem_REF.ImageOptions.Image = global::XYZ_Table.Properties.Resources._16px_REF;
            this.barButtonItem_REF.Name = "barButtonItem_REF";
            this.barButtonItem_REF.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.BarButtonItem_Manual_ItemClick);
            // 
            // barButtonItem_Manual
            // 
            this.barButtonItem_Manual.Caption = "Manuell drive";
            this.barButtonItem_Manual.Id = 2;
            this.barButtonItem_Manual.Name = "barButtonItem_Manual";
            this.barButtonItem_Manual.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.BarButtonItem_Manual_ItemClick);
            // 
            // barButtonItem_Log
            // 
            this.barButtonItem_Log.Caption = "Log ...";
            this.barButtonItem_Log.Id = 3;
            this.barButtonItem_Log.Name = "barButtonItem_Log";
            this.barButtonItem_Log.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.BarButtonItem_LOG_ItemClick);
            // 
            // barManager_XYZ
            // 
            this.barManager_XYZ.DockControls.Add(this.barDockControlTop);
            this.barManager_XYZ.DockControls.Add(this.barDockControlBottom);
            this.barManager_XYZ.DockControls.Add(this.barDockControlLeft);
            this.barManager_XYZ.DockControls.Add(this.barDockControlRight);
            this.barManager_XYZ.Form = this;
            this.barManager_XYZ.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.barButtonItem_Init,
            this.barButtonItem_REF,
            this.barButtonItem_Manual,
            this.barButtonItem_Log});
            this.barManager_XYZ.MaxItemId = 4;
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Manager = this.barManager_XYZ;
            this.barDockControlTop.Size = new System.Drawing.Size(520, 0);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 74);
            this.barDockControlBottom.Manager = this.barManager_XYZ;
            this.barDockControlBottom.Size = new System.Drawing.Size(520, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 0);
            this.barDockControlLeft.Manager = this.barManager_XYZ;
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 74);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(520, 0);
            this.barDockControlRight.Manager = this.barManager_XYZ;
            this.barDockControlRight.Size = new System.Drawing.Size(0, 74);
            // 
            // ISEL_4Axis
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox_XYZ);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "ISEL_4Axis";
            this.Size = new System.Drawing.Size(520, 74);
            this.groupBox_XYZ.ResumeLayout(false);
            this.groupBox_XYZ.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.popupMenu_XYZ)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager_XYZ)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label33;
        private System.Windows.Forms.TextBox akt_Position;
        public System.Windows.Forms.ComboBox ComPort_select;
        public System.Windows.Forms.GroupBox groupBox_XYZ;
        private DevExpress.XtraEditors.DropDownButton button_OpenClose;
        private DevExpress.XtraBars.PopupMenu popupMenu_XYZ;
        private DevExpress.XtraBars.BarManager barManager_XYZ;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraBars.BarButtonItem barButtonItem_Init;
        private DevExpress.XtraBars.BarButtonItem barButtonItem_REF;
        private DevExpress.XtraBars.BarButtonItem barButtonItem_Manual;
        private DevExpress.XtraBars.BarButtonItem barButtonItem_Log;
    }
}
