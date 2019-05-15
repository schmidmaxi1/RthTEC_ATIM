namespace ATIM_GUI._09_DAQ_Unit
{
    partial class DAQ_Unit
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
            this.groupBox_DAQ = new System.Windows.Forms.GroupBox();
            this.numericUpDown_Trigger = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.comboBox_Frequency = new System.Windows.Forms.ComboBox();
            this.comboBox_Range = new System.Windows.Forms.ComboBox();
            this.button_OpenClose = new DevExpress.XtraEditors.DropDownButton();
            this.label9 = new System.Windows.Forms.Label();
            this.ComPort_select = new System.Windows.Forms.ComboBox();
            this.groupBox_DAQ.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_Trigger)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox_DAQ
            // 
            this.groupBox_DAQ.Controls.Add(this.numericUpDown_Trigger);
            this.groupBox_DAQ.Controls.Add(this.label2);
            this.groupBox_DAQ.Controls.Add(this.label1);
            this.groupBox_DAQ.Controls.Add(this.comboBox_Frequency);
            this.groupBox_DAQ.Controls.Add(this.comboBox_Range);
            this.groupBox_DAQ.Controls.Add(this.button_OpenClose);
            this.groupBox_DAQ.Controls.Add(this.label9);
            this.groupBox_DAQ.Controls.Add(this.ComPort_select);
            this.groupBox_DAQ.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox_DAQ.Location = new System.Drawing.Point(0, 0);
            this.groupBox_DAQ.Name = "groupBox_DAQ";
            this.groupBox_DAQ.Size = new System.Drawing.Size(250, 156);
            this.groupBox_DAQ.TabIndex = 35;
            this.groupBox_DAQ.TabStop = false;
            this.groupBox_DAQ.Text = "DAQ-Unit:";
            // 
            // numericUpDown_Trigger
            // 
            this.numericUpDown_Trigger.DecimalPlaces = 2;
            this.numericUpDown_Trigger.Enabled = false;
            this.numericUpDown_Trigger.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numericUpDown_Trigger.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numericUpDown_Trigger.Location = new System.Drawing.Point(140, 125);
            this.numericUpDown_Trigger.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericUpDown_Trigger.Name = "numericUpDown_Trigger";
            this.numericUpDown_Trigger.Size = new System.Drawing.Size(95, 20);
            this.numericUpDown_Trigger.TabIndex = 23;
            this.numericUpDown_Trigger.Value = new decimal(new int[] {
            28,
            0,
            0,
            65536});
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(17, 127);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(86, 13);
            this.label2.TabIndex = 22;
            this.label2.Text = "Trigger-Level:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(17, 93);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 13);
            this.label1.TabIndex = 21;
            this.label1.Text = "Range:";
            // 
            // comboBox_Frequency
            // 
            this.comboBox_Frequency.Enabled = false;
            this.comboBox_Frequency.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBox_Frequency.FormattingEnabled = true;
            this.comboBox_Frequency.Location = new System.Drawing.Point(140, 55);
            this.comboBox_Frequency.Name = "comboBox_Frequency";
            this.comboBox_Frequency.Size = new System.Drawing.Size(95, 21);
            this.comboBox_Frequency.TabIndex = 20;
            this.comboBox_Frequency.SelectedIndexChanged += new System.EventHandler(this.ComboBox_Frequency_SelectedIndexChanged);
            // 
            // comboBox_Range
            // 
            this.comboBox_Range.Enabled = false;
            this.comboBox_Range.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBox_Range.FormattingEnabled = true;
            this.comboBox_Range.Location = new System.Drawing.Point(140, 90);
            this.comboBox_Range.Name = "comboBox_Range";
            this.comboBox_Range.Size = new System.Drawing.Size(95, 21);
            this.comboBox_Range.TabIndex = 19;
            this.comboBox_Range.SelectedIndexChanged += new System.EventHandler(this.ComboBox_Range_SelectedIndexChanged);
            // 
            // button_OpenClose
            // 
            this.button_OpenClose.DropDownArrowStyle = DevExpress.XtraEditors.DropDownArrowStyle.SplitButton;
            this.button_OpenClose.Location = new System.Drawing.Point(15, 20);
            this.button_OpenClose.Name = "button_OpenClose";
            this.button_OpenClose.Size = new System.Drawing.Size(95, 21);
            this.button_OpenClose.TabIndex = 17;
            this.button_OpenClose.Text = "Open";
            this.button_OpenClose.Click += new System.EventHandler(this.OpenClose_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(17, 58);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(70, 13);
            this.label9.TabIndex = 13;
            this.label9.Text = "Frequency:";
            // 
            // ComPort_select
            // 
            this.ComPort_select.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ComPort_select.Location = new System.Drawing.Point(140, 20);
            this.ComPort_select.Name = "ComPort_select";
            this.ComPort_select.Size = new System.Drawing.Size(95, 21);
            this.ComPort_select.TabIndex = 16;
            // 
            // DAQ_Unit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox_DAQ);
            this.Name = "DAQ_Unit";
            this.Size = new System.Drawing.Size(255, 160);
            this.groupBox_DAQ.ResumeLayout(false);
            this.groupBox_DAQ.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_Trigger)).EndInit();
            this.ResumeLayout(false);

        }


        #endregion

        public System.Windows.Forms.GroupBox groupBox_DAQ;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        internal DevExpress.XtraEditors.DropDownButton button_OpenClose;
        private System.Windows.Forms.Label label9;
        public System.Windows.Forms.ComboBox ComPort_select;
        public System.Windows.Forms.ComboBox comboBox_Frequency;
        public System.Windows.Forms.ComboBox comboBox_Range;
        public System.Windows.Forms.NumericUpDown numericUpDown_Trigger;
    }
}
