namespace Power_Supply_HamegHMP
{
    partial class MainWindow_HMP
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

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWindow_HMP));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.CurrentMeas1 = new System.Windows.Forms.TextBox();
            this.VoltageMeas1 = new System.Windows.Forms.TextBox();
            this.SwitchOnOff1 = new System.Windows.Forms.CheckBox();
            this.CurrentSet1 = new System.Windows.Forms.NumericUpDown();
            this.VoltageSet1 = new System.Windows.Forms.NumericUpDown();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.CurrentMeas2 = new System.Windows.Forms.TextBox();
            this.VoltageMeas2 = new System.Windows.Forms.TextBox();
            this.SwitchOnOff2 = new System.Windows.Forms.CheckBox();
            this.CurrentSet2 = new System.Windows.Forms.NumericUpDown();
            this.VoltageSet2 = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.VoltageSet3 = new System.Windows.Forms.NumericUpDown();
            this.CurrentMeas3 = new System.Windows.Forms.TextBox();
            this.VoltageMeas3 = new System.Windows.Forms.TextBox();
            this.SwitchOnOff3 = new System.Windows.Forms.CheckBox();
            this.CurrentSet3 = new System.Windows.Forms.NumericUpDown();
            this.VoltageSet4 = new System.Windows.Forms.NumericUpDown();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.CurrentMeas4 = new System.Windows.Forms.TextBox();
            this.VoltageMeas4 = new System.Windows.Forms.TextBox();
            this.SwitchOnOff4 = new System.Windows.Forms.CheckBox();
            this.CurrentSet4 = new System.Windows.Forms.NumericUpDown();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.timer_1500ms = new System.Windows.Forms.Timer(this.components);
            this.ComPort_select = new System.Windows.Forms.ComboBox();
            this.button_ShowLog = new DevExpress.XtraEditors.SimpleButton();
            this.button_OpenClose = new DevExpress.XtraEditors.SimpleButton();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.CurrentSet1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.VoltageSet1)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.CurrentSet2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.VoltageSet2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.VoltageSet3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CurrentSet3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.VoltageSet4)).BeginInit();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.CurrentSet4)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.CurrentMeas1);
            this.groupBox1.Controls.Add(this.VoltageMeas1);
            this.groupBox1.Controls.Add(this.SwitchOnOff1);
            this.groupBox1.Controls.Add(this.CurrentSet1);
            this.groupBox1.Controls.Add(this.VoltageSet1);
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // CurrentMeas1
            // 
            resources.ApplyResources(this.CurrentMeas1, "CurrentMeas1");
            this.CurrentMeas1.Name = "CurrentMeas1";
            // 
            // VoltageMeas1
            // 
            resources.ApplyResources(this.VoltageMeas1, "VoltageMeas1");
            this.VoltageMeas1.Name = "VoltageMeas1";
            // 
            // SwitchOnOff1
            // 
            resources.ApplyResources(this.SwitchOnOff1, "SwitchOnOff1");
            this.SwitchOnOff1.Name = "SwitchOnOff1";
            this.SwitchOnOff1.UseVisualStyleBackColor = true;
            this.SwitchOnOff1.CheckedChanged += new System.EventHandler(this.SwitchOnOff1_CheckedChanged);
            // 
            // CurrentSet1
            // 
            this.CurrentSet1.DecimalPlaces = 3;
            resources.ApplyResources(this.CurrentSet1, "CurrentSet1");
            this.CurrentSet1.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.CurrentSet1.Name = "CurrentSet1";
            this.CurrentSet1.ValueChanged += new System.EventHandler(this.CurrentSet1_ValueChanged);
            // 
            // VoltageSet1
            // 
            this.VoltageSet1.DecimalPlaces = 3;
            resources.ApplyResources(this.VoltageSet1, "VoltageSet1");
            this.VoltageSet1.Maximum = new decimal(new int[] {
            32,
            0,
            0,
            0});
            this.VoltageSet1.Name = "VoltageSet1";
            this.VoltageSet1.ValueChanged += new System.EventHandler(this.VoltageSet1_ValueChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.CurrentMeas2);
            this.groupBox2.Controls.Add(this.VoltageMeas2);
            this.groupBox2.Controls.Add(this.SwitchOnOff2);
            this.groupBox2.Controls.Add(this.CurrentSet2);
            this.groupBox2.Controls.Add(this.VoltageSet2);
            resources.ApplyResources(this.groupBox2, "groupBox2");
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.TabStop = false;
            // 
            // CurrentMeas2
            // 
            resources.ApplyResources(this.CurrentMeas2, "CurrentMeas2");
            this.CurrentMeas2.Name = "CurrentMeas2";
            // 
            // VoltageMeas2
            // 
            resources.ApplyResources(this.VoltageMeas2, "VoltageMeas2");
            this.VoltageMeas2.Name = "VoltageMeas2";
            // 
            // SwitchOnOff2
            // 
            resources.ApplyResources(this.SwitchOnOff2, "SwitchOnOff2");
            this.SwitchOnOff2.Name = "SwitchOnOff2";
            this.SwitchOnOff2.UseVisualStyleBackColor = true;
            this.SwitchOnOff2.CheckedChanged += new System.EventHandler(this.SwitchOnOff2_CheckedChanged);
            // 
            // CurrentSet2
            // 
            this.CurrentSet2.DecimalPlaces = 3;
            resources.ApplyResources(this.CurrentSet2, "CurrentSet2");
            this.CurrentSet2.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.CurrentSet2.Name = "CurrentSet2";
            this.CurrentSet2.ValueChanged += new System.EventHandler(this.CurrentSet2_ValueChanged);
            // 
            // VoltageSet2
            // 
            this.VoltageSet2.DecimalPlaces = 3;
            resources.ApplyResources(this.VoltageSet2, "VoltageSet2");
            this.VoltageSet2.Maximum = new decimal(new int[] {
            32,
            0,
            0,
            0});
            this.VoltageSet2.Name = "VoltageSet2";
            this.VoltageSet2.ValueChanged += new System.EventHandler(this.VoltageSet2_ValueChanged);
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // VoltageSet3
            // 
            this.VoltageSet3.DecimalPlaces = 3;
            resources.ApplyResources(this.VoltageSet3, "VoltageSet3");
            this.VoltageSet3.Maximum = new decimal(new int[] {
            32,
            0,
            0,
            0});
            this.VoltageSet3.Name = "VoltageSet3";
            this.VoltageSet3.ValueChanged += new System.EventHandler(this.VoltageSet3_ValueChanged);
            // 
            // CurrentMeas3
            // 
            resources.ApplyResources(this.CurrentMeas3, "CurrentMeas3");
            this.CurrentMeas3.Name = "CurrentMeas3";
            // 
            // VoltageMeas3
            // 
            resources.ApplyResources(this.VoltageMeas3, "VoltageMeas3");
            this.VoltageMeas3.Name = "VoltageMeas3";
            // 
            // SwitchOnOff3
            // 
            resources.ApplyResources(this.SwitchOnOff3, "SwitchOnOff3");
            this.SwitchOnOff3.Name = "SwitchOnOff3";
            this.SwitchOnOff3.UseVisualStyleBackColor = true;
            this.SwitchOnOff3.CheckedChanged += new System.EventHandler(this.SwitchOnOff3_CheckedChanged);
            // 
            // CurrentSet3
            // 
            this.CurrentSet3.DecimalPlaces = 3;
            resources.ApplyResources(this.CurrentSet3, "CurrentSet3");
            this.CurrentSet3.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.CurrentSet3.Name = "CurrentSet3";
            this.CurrentSet3.ValueChanged += new System.EventHandler(this.CurrentSet3_ValueChanged);
            // 
            // VoltageSet4
            // 
            this.VoltageSet4.DecimalPlaces = 3;
            resources.ApplyResources(this.VoltageSet4, "VoltageSet4");
            this.VoltageSet4.Maximum = new decimal(new int[] {
            32,
            0,
            0,
            0});
            this.VoltageSet4.Name = "VoltageSet4";
            this.VoltageSet4.ValueChanged += new System.EventHandler(this.VoltageSet4_ValueChanged);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.CurrentMeas4);
            this.groupBox4.Controls.Add(this.VoltageMeas4);
            this.groupBox4.Controls.Add(this.SwitchOnOff4);
            this.groupBox4.Controls.Add(this.CurrentSet4);
            this.groupBox4.Controls.Add(this.VoltageSet4);
            resources.ApplyResources(this.groupBox4, "groupBox4");
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.TabStop = false;
            // 
            // CurrentMeas4
            // 
            resources.ApplyResources(this.CurrentMeas4, "CurrentMeas4");
            this.CurrentMeas4.Name = "CurrentMeas4";
            // 
            // VoltageMeas4
            // 
            resources.ApplyResources(this.VoltageMeas4, "VoltageMeas4");
            this.VoltageMeas4.Name = "VoltageMeas4";
            // 
            // SwitchOnOff4
            // 
            resources.ApplyResources(this.SwitchOnOff4, "SwitchOnOff4");
            this.SwitchOnOff4.Name = "SwitchOnOff4";
            this.SwitchOnOff4.UseVisualStyleBackColor = true;
            this.SwitchOnOff4.CheckedChanged += new System.EventHandler(this.SwitchOnOff4_CheckedChanged);
            // 
            // CurrentSet4
            // 
            this.CurrentSet4.DecimalPlaces = 3;
            resources.ApplyResources(this.CurrentSet4, "CurrentSet4");
            this.CurrentSet4.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.CurrentSet4.Name = "CurrentSet4";
            this.CurrentSet4.ValueChanged += new System.EventHandler(this.CurrentSet4_ValueChanged);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.CurrentMeas3);
            this.groupBox3.Controls.Add(this.VoltageMeas3);
            this.groupBox3.Controls.Add(this.SwitchOnOff3);
            this.groupBox3.Controls.Add(this.CurrentSet3);
            this.groupBox3.Controls.Add(this.VoltageSet3);
            resources.ApplyResources(this.groupBox3, "groupBox3");
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.TabStop = false;
            // 
            // timer_1500ms
            // 
            this.timer_1500ms.Interval = 1500;
            this.timer_1500ms.Tick += new System.EventHandler(this.Timer1_Tick);
            // 
            // ComPort_select
            // 
            resources.ApplyResources(this.ComPort_select, "ComPort_select");
            this.ComPort_select.FormattingEnabled = true;
            this.ComPort_select.Name = "ComPort_select";
            // 
            // button_ShowLog
            // 
            resources.ApplyResources(this.button_ShowLog, "button_ShowLog");
            this.button_ShowLog.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("button_ShowLog.ImageOptions.Image")));
            this.button_ShowLog.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.button_ShowLog.Name = "button_ShowLog";
            this.button_ShowLog.Click += new System.EventHandler(this.Button_ShowLog_Click);
            // 
            // button_OpenClose
            // 
            resources.ApplyResources(this.button_OpenClose, "button_OpenClose");
            this.button_OpenClose.Name = "button_OpenClose";
            this.button_OpenClose.Click += new System.EventHandler(this.Button_OpenClose_Click);
            // 
            // MainWindow_HMP
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.button_OpenClose);
            this.Controls.Add(this.button_ShowLog);
            this.Controls.Add(this.ComPort_select);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "MainWindow_HMP";
            this.ShowIcon = false;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Window_Hameg_HMP_Detailed_FormClosing);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.CurrentSet1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.VoltageSet1)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.CurrentSet2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.VoltageSet2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.VoltageSet3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CurrentSet3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.VoltageSet4)).EndInit();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.CurrentSet4)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox CurrentMeas1;
        private System.Windows.Forms.TextBox VoltageMeas1;
        private System.Windows.Forms.CheckBox SwitchOnOff1;
        private System.Windows.Forms.NumericUpDown CurrentSet1;
        private System.Windows.Forms.NumericUpDown VoltageSet1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox CurrentMeas2;
        private System.Windows.Forms.TextBox VoltageMeas2;
        private System.Windows.Forms.CheckBox SwitchOnOff2;
        private System.Windows.Forms.NumericUpDown CurrentSet2;
        private System.Windows.Forms.NumericUpDown VoltageSet2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown VoltageSet3;
        private System.Windows.Forms.TextBox CurrentMeas3;
        private System.Windows.Forms.TextBox VoltageMeas3;
        private System.Windows.Forms.CheckBox SwitchOnOff3;
        private System.Windows.Forms.NumericUpDown CurrentSet3;
        private System.Windows.Forms.NumericUpDown VoltageSet4;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.TextBox CurrentMeas4;
        private System.Windows.Forms.TextBox VoltageMeas4;
        private System.Windows.Forms.CheckBox SwitchOnOff4;
        private System.Windows.Forms.NumericUpDown CurrentSet4;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Timer timer_1500ms;
        public System.Windows.Forms.ComboBox ComPort_select;
        private DevExpress.XtraEditors.SimpleButton button_ShowLog;
        private DevExpress.XtraEditors.SimpleButton button_OpenClose;
    }
}

