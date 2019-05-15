namespace ATIM_GUI._05_XYZ
{
    partial class XYZ_table
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(XYZ_table));
            this.groupBox_XYZ = new System.Windows.Forms.GroupBox();
            this.button_OpenClose = new DevExpress.XtraEditors.DropDownButton();
            this.popupMenu_OpenColse = new DevExpress.XtraBars.PopupMenu(this.components);
            this.barButtonItem_Init = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem_REF = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem_Manual = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem_LOG = new DevExpress.XtraBars.BarButtonItem();
            this.barManager_OpenClose = new DevExpress.XtraBars.BarManager(this.components);
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.label9 = new System.Windows.Forms.Label();
            this.akt_Postition = new System.Windows.Forms.TextBox();
            this.ComPort_select = new System.Windows.Forms.ComboBox();
            this.bar1 = new DevExpress.XtraBars.Bar();
            this.groupBox_XYZ.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.popupMenu_OpenColse)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager_OpenClose)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox_XYZ
            // 
            this.groupBox_XYZ.Controls.Add(this.button_OpenClose);
            this.groupBox_XYZ.Controls.Add(this.label9);
            this.groupBox_XYZ.Controls.Add(this.akt_Postition);
            this.groupBox_XYZ.Controls.Add(this.ComPort_select);
            this.groupBox_XYZ.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox_XYZ.Location = new System.Drawing.Point(0, 0);
            this.groupBox_XYZ.Name = "groupBox_XYZ";
            this.groupBox_XYZ.Size = new System.Drawing.Size(250, 95);
            this.groupBox_XYZ.TabIndex = 34;
            this.groupBox_XYZ.TabStop = false;
            this.groupBox_XYZ.Text = "xyz-Table:";
            // 
            // button_OpenClose
            // 
            this.button_OpenClose.DropDownArrowStyle = DevExpress.XtraEditors.DropDownArrowStyle.SplitButton;
            this.button_OpenClose.DropDownControl = this.popupMenu_OpenColse;
            this.button_OpenClose.Location = new System.Drawing.Point(15, 20);
            this.button_OpenClose.Name = "button_OpenClose";
            this.button_OpenClose.Size = new System.Drawing.Size(95, 21);
            this.button_OpenClose.TabIndex = 17;
            this.button_OpenClose.Text = "Open";
            this.button_OpenClose.Click += new System.EventHandler(this.Button_OpenClose_Click);
            // 
            // popupMenu_OpenColse
            // 
            this.popupMenu_OpenColse.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem_Init),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem_REF),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem_Manual),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem_LOG)});
            this.popupMenu_OpenColse.Manager = this.barManager_OpenClose;
            this.popupMenu_OpenColse.Name = "popupMenu_OpenColse";
            // 
            // barButtonItem_Init
            // 
            this.barButtonItem_Init.Caption = "Initialization";
            this.barButtonItem_Init.CategoryGuid = new System.Guid("e10ed4f8-b1e1-4270-abd4-f3014416f927");
            this.barButtonItem_Init.Enabled = false;
            this.barButtonItem_Init.Id = 0;
            this.barButtonItem_Init.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barButtonItem_Init.ImageOptions.Image")));
            this.barButtonItem_Init.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("barButtonItem_Init.ImageOptions.LargeImage")));
            this.barButtonItem_Init.Name = "barButtonItem_Init";
            this.barButtonItem_Init.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.BarButtonItem_Init_ItemClick);
            // 
            // barButtonItem_REF
            // 
            this.barButtonItem_REF.Caption = "Reference drive";
            this.barButtonItem_REF.CategoryGuid = new System.Guid("e10ed4f8-b1e1-4270-abd4-f3014416f927");
            this.barButtonItem_REF.Enabled = false;
            this.barButtonItem_REF.Id = 4;
            this.barButtonItem_REF.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barButtonItem_REF.ImageOptions.Image")));
            this.barButtonItem_REF.Name = "barButtonItem_REF";
            this.barButtonItem_REF.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.BarButtonItem_REF_ItemClick);
            // 
            // barButtonItem_Manual
            // 
            this.barButtonItem_Manual.Caption = "Manual drive";
            this.barButtonItem_Manual.CategoryGuid = new System.Guid("e10ed4f8-b1e1-4270-abd4-f3014416f927");
            this.barButtonItem_Manual.Enabled = false;
            this.barButtonItem_Manual.Id = 2;
            this.barButtonItem_Manual.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barButtonItem_Manual.ImageOptions.Image")));
            this.barButtonItem_Manual.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("barButtonItem_Manual.ImageOptions.LargeImage")));
            this.barButtonItem_Manual.Name = "barButtonItem_Manual";
            this.barButtonItem_Manual.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.BarButtonItem_Manual_ItemClick);
            // 
            // barButtonItem_LOG
            // 
            this.barButtonItem_LOG.Caption = "Show LOG ...";
            this.barButtonItem_LOG.CategoryGuid = new System.Guid("e10ed4f8-b1e1-4270-abd4-f3014416f927");
            this.barButtonItem_LOG.Id = 3;
            this.barButtonItem_LOG.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barButtonItem_LOG.ImageOptions.Image")));
            this.barButtonItem_LOG.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("barButtonItem_LOG.ImageOptions.LargeImage")));
            this.barButtonItem_LOG.Name = "barButtonItem_LOG";
            this.barButtonItem_LOG.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.BarButtonItem_LOG_ItemClick);
            // 
            // barManager_OpenClose
            // 
            this.barManager_OpenClose.Categories.AddRange(new DevExpress.XtraBars.BarManagerCategory[] {
            new DevExpress.XtraBars.BarManagerCategory("Allgemein", new System.Guid("e10ed4f8-b1e1-4270-abd4-f3014416f927"))});
            this.barManager_OpenClose.DockControls.Add(this.barDockControlTop);
            this.barManager_OpenClose.DockControls.Add(this.barDockControlBottom);
            this.barManager_OpenClose.DockControls.Add(this.barDockControlLeft);
            this.barManager_OpenClose.DockControls.Add(this.barDockControlRight);
            this.barManager_OpenClose.Form = this;
            this.barManager_OpenClose.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.barButtonItem_Init,
            this.barButtonItem_Manual,
            this.barButtonItem_LOG,
            this.barButtonItem_REF});
            this.barManager_OpenClose.MaxItemId = 5;
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Manager = this.barManager_OpenClose;
            this.barDockControlTop.Size = new System.Drawing.Size(253, 0);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 101);
            this.barDockControlBottom.Manager = this.barManager_OpenClose;
            this.barDockControlBottom.Size = new System.Drawing.Size(253, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 0);
            this.barDockControlLeft.Manager = this.barManager_OpenClose;
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 101);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(253, 0);
            this.barDockControlRight.Manager = this.barManager_OpenClose;
            this.barDockControlRight.Size = new System.Drawing.Size(0, 101);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(15, 50);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(47, 13);
            this.label9.TabIndex = 13;
            this.label9.Text = "Position:";
            // 
            // akt_Postition
            // 
            this.akt_Postition.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.akt_Postition.Location = new System.Drawing.Point(15, 65);
            this.akt_Postition.Name = "akt_Postition";
            this.akt_Postition.ReadOnly = true;
            this.akt_Postition.Size = new System.Drawing.Size(221, 20);
            this.akt_Postition.TabIndex = 11;
            this.akt_Postition.Text = "Not connected!";
            // 
            // ComPort_select
            // 
            this.ComPort_select.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ComPort_select.Location = new System.Drawing.Point(141, 20);
            this.ComPort_select.Name = "ComPort_select";
            this.ComPort_select.Size = new System.Drawing.Size(95, 21);
            this.ComPort_select.TabIndex = 16;
            // 
            // bar1
            // 
            this.bar1.BarName = "Allgemein";
            this.bar1.DockCol = 0;
            this.bar1.DockRow = 0;
            this.bar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem_Init),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem_REF),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem_Manual),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem_LOG)});
            this.bar1.Text = "Allgemein";
            // 
            // XYZ_table
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox_XYZ);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "XYZ_table";
            this.Size = new System.Drawing.Size(253, 101);
            this.groupBox_XYZ.ResumeLayout(false);
            this.groupBox_XYZ.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.popupMenu_OpenColse)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager_OpenClose)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion



        


        private System.Windows.Forms.Label label9;
        public System.Windows.Forms.ComboBox ComPort_select;
        internal System.Windows.Forms.TextBox akt_Postition;
        internal DevExpress.XtraEditors.DropDownButton button_OpenClose;
        private DevExpress.XtraBars.PopupMenu popupMenu_OpenColse;
        private DevExpress.XtraBars.Bar bar1;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        internal DevExpress.XtraBars.BarManager barManager_OpenClose;
        internal DevExpress.XtraBars.BarButtonItem barButtonItem_Init;
        internal DevExpress.XtraBars.BarButtonItem barButtonItem_Manual;
        internal DevExpress.XtraBars.BarButtonItem barButtonItem_LOG;
        internal DevExpress.XtraBars.BarButtonItem barButtonItem_REF;
        public System.Windows.Forms.GroupBox groupBox_XYZ;
    }
}
