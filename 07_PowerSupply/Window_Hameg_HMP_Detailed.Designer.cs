namespace ATIM_GUI._7_PowerSupply
{
    partial class Window_Hameg_HMP_Detailed
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.timer_1500ms = new System.Windows.Forms.Timer(this.components);
            this.CurrentMeas4 = new System.Windows.Forms.TextBox();
            this.VoltageMeas4 = new System.Windows.Forms.TextBox();
            this.SwitchOnOff4 = new System.Windows.Forms.CheckBox();
            this.CurrentSet4 = new System.Windows.Forms.NumericUpDown();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.VoltageSet4 = new System.Windows.Forms.NumericUpDown();
            this.CurrentMeas3 = new System.Windows.Forms.TextBox();
            this.VoltageMeas3 = new System.Windows.Forms.TextBox();
            this.SwitchOnOff3 = new System.Windows.Forms.CheckBox();
            this.CurrentSet3 = new System.Windows.Forms.NumericUpDown();
            this.VoltageSet3 = new System.Windows.Forms.NumericUpDown();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.CurrentMeas2 = new System.Windows.Forms.TextBox();
            this.VoltageMeas2 = new System.Windows.Forms.TextBox();
            this.SwitchOnOff2 = new System.Windows.Forms.CheckBox();
            this.CurrentSet2 = new System.Windows.Forms.NumericUpDown();
            this.VoltageSet2 = new System.Windows.Forms.NumericUpDown();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.CurrentMeas1 = new System.Windows.Forms.TextBox();
            this.VoltageMeas1 = new System.Windows.Forms.TextBox();
            this.SwitchOnOff1 = new System.Windows.Forms.CheckBox();
            this.CurrentSet1 = new System.Windows.Forms.NumericUpDown();
            this.VoltageSet1 = new System.Windows.Forms.NumericUpDown();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.CurrentSet4)).BeginInit();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.VoltageSet4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CurrentSet3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.VoltageSet3)).BeginInit();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.CurrentSet2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.VoltageSet2)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.CurrentSet1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.VoltageSet1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // timer_1500ms
            // 
            this.timer_1500ms.Enabled = true;
            this.timer_1500ms.Interval = 1500;
            this.timer_1500ms.Tick += new System.EventHandler(this.Timer1_Tick);
            // 
            // CurrentMeas4
            // 
            this.CurrentMeas4.Enabled = false;
            this.CurrentMeas4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CurrentMeas4.Location = new System.Drawing.Point(25, 110);
            this.CurrentMeas4.Name = "CurrentMeas4";
            this.CurrentMeas4.Size = new System.Drawing.Size(70, 20);
            this.CurrentMeas4.TabIndex = 6;
            // 
            // VoltageMeas4
            // 
            this.VoltageMeas4.Enabled = false;
            this.VoltageMeas4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.VoltageMeas4.Location = new System.Drawing.Point(25, 80);
            this.VoltageMeas4.Name = "VoltageMeas4";
            this.VoltageMeas4.Size = new System.Drawing.Size(70, 20);
            this.VoltageMeas4.TabIndex = 5;
            // 
            // SwitchOnOff4
            // 
            this.SwitchOnOff4.AutoSize = true;
            this.SwitchOnOff4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SwitchOnOff4.Location = new System.Drawing.Point(35, 140);
            this.SwitchOnOff4.Name = "SwitchOnOff4";
            this.SwitchOnOff4.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.SwitchOnOff4.Size = new System.Drawing.Size(59, 17);
            this.SwitchOnOff4.TabIndex = 4;
            this.SwitchOnOff4.Text = "On/Off";
            this.SwitchOnOff4.UseVisualStyleBackColor = true;
            this.SwitchOnOff4.CheckedChanged += new System.EventHandler(this.SwitchOnOff4_CheckedChanged);
            // 
            // CurrentSet4
            // 
            this.CurrentSet4.DecimalPlaces = 3;
            this.CurrentSet4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CurrentSet4.Location = new System.Drawing.Point(10, 50);
            this.CurrentSet4.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.CurrentSet4.Name = "CurrentSet4";
            this.CurrentSet4.Size = new System.Drawing.Size(85, 20);
            this.CurrentSet4.TabIndex = 1;
            this.CurrentSet4.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.CurrentSet4.UpDownAlign = System.Windows.Forms.LeftRightAlignment.Left;
            this.CurrentSet4.ValueChanged += new System.EventHandler(this.CurrentSet4_ValueChanged);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.CurrentMeas4);
            this.groupBox4.Controls.Add(this.VoltageMeas4);
            this.groupBox4.Controls.Add(this.SwitchOnOff4);
            this.groupBox4.Controls.Add(this.CurrentSet4);
            this.groupBox4.Controls.Add(this.VoltageSet4);
            this.groupBox4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox4.Location = new System.Drawing.Point(437, 9);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(105, 165);
            this.groupBox4.TabIndex = 16;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Channel 4";
            // 
            // VoltageSet4
            // 
            this.VoltageSet4.DecimalPlaces = 3;
            this.VoltageSet4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.VoltageSet4.Location = new System.Drawing.Point(10, 20);
            this.VoltageSet4.Maximum = new decimal(new int[] {
            32,
            0,
            0,
            0});
            this.VoltageSet4.Name = "VoltageSet4";
            this.VoltageSet4.Size = new System.Drawing.Size(85, 20);
            this.VoltageSet4.TabIndex = 0;
            this.VoltageSet4.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.VoltageSet4.UpDownAlign = System.Windows.Forms.LeftRightAlignment.Left;
            this.VoltageSet4.ValueChanged += new System.EventHandler(this.VoltageSet4_ValueChanged);
            // 
            // CurrentMeas3
            // 
            this.CurrentMeas3.Enabled = false;
            this.CurrentMeas3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CurrentMeas3.Location = new System.Drawing.Point(25, 110);
            this.CurrentMeas3.Name = "CurrentMeas3";
            this.CurrentMeas3.Size = new System.Drawing.Size(70, 20);
            this.CurrentMeas3.TabIndex = 6;
            // 
            // VoltageMeas3
            // 
            this.VoltageMeas3.Enabled = false;
            this.VoltageMeas3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.VoltageMeas3.Location = new System.Drawing.Point(25, 80);
            this.VoltageMeas3.Name = "VoltageMeas3";
            this.VoltageMeas3.Size = new System.Drawing.Size(70, 20);
            this.VoltageMeas3.TabIndex = 5;
            // 
            // SwitchOnOff3
            // 
            this.SwitchOnOff3.AutoSize = true;
            this.SwitchOnOff3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SwitchOnOff3.Location = new System.Drawing.Point(35, 140);
            this.SwitchOnOff3.Name = "SwitchOnOff3";
            this.SwitchOnOff3.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.SwitchOnOff3.Size = new System.Drawing.Size(59, 17);
            this.SwitchOnOff3.TabIndex = 4;
            this.SwitchOnOff3.Text = "On/Off";
            this.SwitchOnOff3.UseVisualStyleBackColor = true;
            this.SwitchOnOff3.CheckedChanged += new System.EventHandler(this.SwitchOnOff3_CheckedChanged);
            // 
            // CurrentSet3
            // 
            this.CurrentSet3.DecimalPlaces = 3;
            this.CurrentSet3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CurrentSet3.Location = new System.Drawing.Point(10, 50);
            this.CurrentSet3.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.CurrentSet3.Name = "CurrentSet3";
            this.CurrentSet3.Size = new System.Drawing.Size(85, 20);
            this.CurrentSet3.TabIndex = 1;
            this.CurrentSet3.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.CurrentSet3.UpDownAlign = System.Windows.Forms.LeftRightAlignment.Left;
            this.CurrentSet3.ValueChanged += new System.EventHandler(this.CurrentSet3_ValueChanged);
            // 
            // VoltageSet3
            // 
            this.VoltageSet3.DecimalPlaces = 3;
            this.VoltageSet3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.VoltageSet3.Location = new System.Drawing.Point(10, 20);
            this.VoltageSet3.Maximum = new decimal(new int[] {
            32,
            0,
            0,
            0});
            this.VoltageSet3.Name = "VoltageSet3";
            this.VoltageSet3.Size = new System.Drawing.Size(85, 20);
            this.VoltageSet3.TabIndex = 0;
            this.VoltageSet3.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.VoltageSet3.UpDownAlign = System.Windows.Forms.LeftRightAlignment.Left;
            this.VoltageSet3.ValueChanged += new System.EventHandler(this.VoltageSet3_ValueChanged);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.CurrentMeas3);
            this.groupBox3.Controls.Add(this.VoltageMeas3);
            this.groupBox3.Controls.Add(this.SwitchOnOff3);
            this.groupBox3.Controls.Add(this.CurrentSet3);
            this.groupBox3.Controls.Add(this.VoltageSet3);
            this.groupBox3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox3.Location = new System.Drawing.Point(326, 9);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(105, 165);
            this.groupBox3.TabIndex = 17;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Channel 3";
            // 
            // CurrentMeas2
            // 
            this.CurrentMeas2.Enabled = false;
            this.CurrentMeas2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CurrentMeas2.Location = new System.Drawing.Point(25, 110);
            this.CurrentMeas2.Name = "CurrentMeas2";
            this.CurrentMeas2.Size = new System.Drawing.Size(70, 20);
            this.CurrentMeas2.TabIndex = 6;
            // 
            // VoltageMeas2
            // 
            this.VoltageMeas2.Enabled = false;
            this.VoltageMeas2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.VoltageMeas2.Location = new System.Drawing.Point(25, 80);
            this.VoltageMeas2.Name = "VoltageMeas2";
            this.VoltageMeas2.Size = new System.Drawing.Size(70, 20);
            this.VoltageMeas2.TabIndex = 5;
            // 
            // SwitchOnOff2
            // 
            this.SwitchOnOff2.AutoSize = true;
            this.SwitchOnOff2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SwitchOnOff2.Location = new System.Drawing.Point(35, 140);
            this.SwitchOnOff2.Name = "SwitchOnOff2";
            this.SwitchOnOff2.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.SwitchOnOff2.Size = new System.Drawing.Size(59, 17);
            this.SwitchOnOff2.TabIndex = 4;
            this.SwitchOnOff2.Text = "On/Off";
            this.SwitchOnOff2.UseVisualStyleBackColor = true;
            this.SwitchOnOff2.CheckedChanged += new System.EventHandler(this.SwitchOnOff2_CheckedChanged);
            // 
            // CurrentSet2
            // 
            this.CurrentSet2.DecimalPlaces = 3;
            this.CurrentSet2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CurrentSet2.Location = new System.Drawing.Point(10, 50);
            this.CurrentSet2.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.CurrentSet2.Name = "CurrentSet2";
            this.CurrentSet2.Size = new System.Drawing.Size(85, 20);
            this.CurrentSet2.TabIndex = 1;
            this.CurrentSet2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.CurrentSet2.UpDownAlign = System.Windows.Forms.LeftRightAlignment.Left;
            this.CurrentSet2.ValueChanged += new System.EventHandler(this.CurrentSet2_ValueChanged);
            // 
            // VoltageSet2
            // 
            this.VoltageSet2.DecimalPlaces = 3;
            this.VoltageSet2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.VoltageSet2.Location = new System.Drawing.Point(10, 20);
            this.VoltageSet2.Maximum = new decimal(new int[] {
            32,
            0,
            0,
            0});
            this.VoltageSet2.Name = "VoltageSet2";
            this.VoltageSet2.Size = new System.Drawing.Size(85, 20);
            this.VoltageSet2.TabIndex = 0;
            this.VoltageSet2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.VoltageSet2.UpDownAlign = System.Windows.Forms.LeftRightAlignment.Left;
            this.VoltageSet2.ValueChanged += new System.EventHandler(this.VoltageSet2_ValueChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.CurrentMeas2);
            this.groupBox2.Controls.Add(this.VoltageMeas2);
            this.groupBox2.Controls.Add(this.SwitchOnOff2);
            this.groupBox2.Controls.Add(this.CurrentSet2);
            this.groupBox2.Controls.Add(this.VoltageSet2);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(215, 9);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(105, 165);
            this.groupBox2.TabIndex = 15;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Channel 2";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(4, 150);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(88, 13);
            this.label5.TabIndex = 14;
            this.label5.Text = "Switch On/Off";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(4, 120);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(89, 13);
            this.label4.TabIndex = 13;
            this.label4.Text = "Current (meas)";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(4, 90);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(91, 13);
            this.label3.TabIndex = 12;
            this.label3.Text = "Voltage (meas)";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(4, 60);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 13);
            this.label2.TabIndex = 11;
            this.label2.Text = "Current (set)";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(4, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(79, 13);
            this.label1.TabIndex = 10;
            this.label1.Text = "Voltage (set)";
            // 
            // CurrentMeas1
            // 
            this.CurrentMeas1.Enabled = false;
            this.CurrentMeas1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CurrentMeas1.Location = new System.Drawing.Point(25, 110);
            this.CurrentMeas1.Name = "CurrentMeas1";
            this.CurrentMeas1.Size = new System.Drawing.Size(70, 20);
            this.CurrentMeas1.TabIndex = 6;
            // 
            // VoltageMeas1
            // 
            this.VoltageMeas1.Enabled = false;
            this.VoltageMeas1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.VoltageMeas1.Location = new System.Drawing.Point(25, 80);
            this.VoltageMeas1.Name = "VoltageMeas1";
            this.VoltageMeas1.Size = new System.Drawing.Size(70, 20);
            this.VoltageMeas1.TabIndex = 5;
            // 
            // SwitchOnOff1
            // 
            this.SwitchOnOff1.AutoSize = true;
            this.SwitchOnOff1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SwitchOnOff1.Location = new System.Drawing.Point(35, 140);
            this.SwitchOnOff1.Name = "SwitchOnOff1";
            this.SwitchOnOff1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.SwitchOnOff1.Size = new System.Drawing.Size(59, 17);
            this.SwitchOnOff1.TabIndex = 4;
            this.SwitchOnOff1.Text = "On/Off";
            this.SwitchOnOff1.UseVisualStyleBackColor = true;
            this.SwitchOnOff1.CheckedChanged += new System.EventHandler(this.SwitchOnOff1_CheckedChanged);
            // 
            // CurrentSet1
            // 
            this.CurrentSet1.DecimalPlaces = 3;
            this.CurrentSet1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CurrentSet1.Location = new System.Drawing.Point(10, 50);
            this.CurrentSet1.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.CurrentSet1.Name = "CurrentSet1";
            this.CurrentSet1.Size = new System.Drawing.Size(85, 20);
            this.CurrentSet1.TabIndex = 1;
            this.CurrentSet1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.CurrentSet1.UpDownAlign = System.Windows.Forms.LeftRightAlignment.Left;
            this.CurrentSet1.ValueChanged += new System.EventHandler(this.CurrentSet1_ValueChanged);
            // 
            // VoltageSet1
            // 
            this.VoltageSet1.DecimalPlaces = 3;
            this.VoltageSet1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.VoltageSet1.Location = new System.Drawing.Point(10, 20);
            this.VoltageSet1.Maximum = new decimal(new int[] {
            32,
            0,
            0,
            0});
            this.VoltageSet1.Name = "VoltageSet1";
            this.VoltageSet1.Size = new System.Drawing.Size(85, 20);
            this.VoltageSet1.TabIndex = 0;
            this.VoltageSet1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.VoltageSet1.UpDownAlign = System.Windows.Forms.LeftRightAlignment.Left;
            this.VoltageSet1.ValueChanged += new System.EventHandler(this.VoltageSet1_ValueChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.CurrentMeas1);
            this.groupBox1.Controls.Add(this.VoltageMeas1);
            this.groupBox1.Controls.Add(this.SwitchOnOff1);
            this.groupBox1.Controls.Add(this.CurrentSet1);
            this.groupBox1.Controls.Add(this.VoltageSet1);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(104, 7);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(105, 165);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Channel 1";
            // 
            // Window_Hameg_HMP_Detailed
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(546, 177);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.groupBox1);
            this.Name = "Window_Hameg_HMP_Detailed";
            this.Text = "Window_Hameg_HMP_Detailed";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Window_Hameg_HMP_Detailed_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.CurrentSet4)).EndInit();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.VoltageSet4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CurrentSet3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.VoltageSet3)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.CurrentSet2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.VoltageSet2)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.CurrentSet1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.VoltageSet1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer timer_1500ms;
        private System.Windows.Forms.TextBox CurrentMeas4;
        private System.Windows.Forms.TextBox VoltageMeas4;
        private System.Windows.Forms.CheckBox SwitchOnOff4;
        private System.Windows.Forms.NumericUpDown CurrentSet4;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.NumericUpDown VoltageSet4;
        private System.Windows.Forms.TextBox CurrentMeas3;
        private System.Windows.Forms.TextBox VoltageMeas3;
        private System.Windows.Forms.CheckBox SwitchOnOff3;
        private System.Windows.Forms.NumericUpDown CurrentSet3;
        private System.Windows.Forms.NumericUpDown VoltageSet3;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox CurrentMeas2;
        private System.Windows.Forms.TextBox VoltageMeas2;
        private System.Windows.Forms.CheckBox SwitchOnOff2;
        private System.Windows.Forms.NumericUpDown CurrentSet2;
        private System.Windows.Forms.NumericUpDown VoltageSet2;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox CurrentMeas1;
        private System.Windows.Forms.TextBox VoltageMeas1;
        private System.Windows.Forms.CheckBox SwitchOnOff1;
        private System.Windows.Forms.NumericUpDown CurrentSet1;
        private System.Windows.Forms.NumericUpDown VoltageSet1;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}