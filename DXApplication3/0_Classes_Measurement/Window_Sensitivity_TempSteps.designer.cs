namespace ATIM_GUI._0_Classes_Measurement
{
    partial class Window_Sensitivity_TempStepSelect
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Title title1 = new System.Windows.Forms.DataVisualization.Charting.Title();
            this.textBox_Time = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.button_Cancel = new System.Windows.Forms.Button();
            this.button_start = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.k_factor_current = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.numberOfSteps = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.stop_Temp = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.start_Temp = new System.Windows.Forms.NumericUpDown();
            this.k_factor_preview = new System.Windows.Forms.DataVisualization.Charting.Chart();
            ((System.ComponentModel.ISupportInitialize)(this.k_factor_current)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numberOfSteps)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stop_Temp)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.start_Temp)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.k_factor_preview)).BeginInit();
            this.SuspendLayout();
            // 
            // textBox_Time
            // 
            this.textBox_Time.Location = new System.Drawing.Point(12, 580);
            this.textBox_Time.Name = "textBox_Time";
            this.textBox_Time.ReadOnly = true;
            this.textBox_Time.Size = new System.Drawing.Size(98, 20);
            this.textBox_Time.TabIndex = 31;
            this.textBox_Time.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(12, 566);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(82, 13);
            this.label8.TabIndex = 30;
            this.label8.Text = "Estimated Time:";
            // 
            // button_Cancel
            // 
            this.button_Cancel.Location = new System.Drawing.Point(272, 572);
            this.button_Cancel.Name = "button_Cancel";
            this.button_Cancel.Size = new System.Drawing.Size(95, 30);
            this.button_Cancel.TabIndex = 29;
            this.button_Cancel.Text = "Cancel";
            this.button_Cancel.UseVisualStyleBackColor = true;
            this.button_Cancel.Click += new System.EventHandler(this.Button_Cancel_Click);
            // 
            // button_start
            // 
            this.button_start.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.button_start.Location = new System.Drawing.Point(142, 572);
            this.button_start.Name = "button_start";
            this.button_start.Size = new System.Drawing.Size(100, 30);
            this.button_start.TabIndex = 28;
            this.button_start.Text = "Start";
            this.button_start.UseVisualStyleBackColor = true;
            this.button_start.Click += new System.EventHandler(this.Button_start_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(92, 91);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(22, 13);
            this.label5.TabIndex = 26;
            this.label5.Text = "mA";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(12, 69);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(44, 13);
            this.label7.TabIndex = 25;
            this.label7.Text = "Current:";
            // 
            // k_factor_current
            // 
            this.k_factor_current.Increment = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.k_factor_current.Location = new System.Drawing.Point(12, 89);
            this.k_factor_current.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.k_factor_current.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.k_factor_current.Name = "k_factor_current";
            this.k_factor_current.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.k_factor_current.Size = new System.Drawing.Size(80, 20);
            this.k_factor_current.TabIndex = 24;
            this.k_factor_current.Value = new decimal(new int[] {
            20,
            0,
            0,
            0});
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(272, 9);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(89, 13);
            this.label6.TabIndex = 23;
            this.label6.Text = "Number of Steps:";
            // 
            // numberOfSteps
            // 
            this.numberOfSteps.Location = new System.Drawing.Point(272, 29);
            this.numberOfSteps.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.numberOfSteps.Name = "numberOfSteps";
            this.numberOfSteps.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.numberOfSteps.Size = new System.Drawing.Size(95, 20);
            this.numberOfSteps.TabIndex = 22;
            this.numberOfSteps.Value = new decimal(new int[] {
            4,
            0,
            0,
            0});
            this.numberOfSteps.ValueChanged += new System.EventHandler(this.TempSetting_ValueChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(222, 31);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(18, 13);
            this.label3.TabIndex = 21;
            this.label3.Text = "°C";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(142, 9);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(95, 13);
            this.label4.TabIndex = 20;
            this.label4.Text = "Stop-Temperature:";
            // 
            // stop_Temp
            // 
            this.stop_Temp.Increment = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.stop_Temp.Location = new System.Drawing.Point(142, 29);
            this.stop_Temp.Maximum = new decimal(new int[] {
            85,
            0,
            0,
            0});
            this.stop_Temp.Minimum = new decimal(new int[] {
            15,
            0,
            0,
            0});
            this.stop_Temp.Name = "stop_Temp";
            this.stop_Temp.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.stop_Temp.Size = new System.Drawing.Size(80, 20);
            this.stop_Temp.TabIndex = 19;
            this.stop_Temp.Value = new decimal(new int[] {
            55,
            0,
            0,
            0});
            this.stop_Temp.ValueChanged += new System.EventHandler(this.TempSetting_ValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(92, 31);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(18, 13);
            this.label2.TabIndex = 18;
            this.label2.Text = "°C";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(95, 13);
            this.label1.TabIndex = 17;
            this.label1.Text = "Start-Temperature:";
            // 
            // start_Temp
            // 
            this.start_Temp.Increment = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.start_Temp.Location = new System.Drawing.Point(12, 29);
            this.start_Temp.Maximum = new decimal(new int[] {
            85,
            0,
            0,
            0});
            this.start_Temp.Minimum = new decimal(new int[] {
            15,
            0,
            0,
            0});
            this.start_Temp.Name = "start_Temp";
            this.start_Temp.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.start_Temp.Size = new System.Drawing.Size(80, 20);
            this.start_Temp.TabIndex = 16;
            this.start_Temp.Value = new decimal(new int[] {
            25,
            0,
            0,
            0});
            this.start_Temp.ValueChanged += new System.EventHandler(this.TempSetting_ValueChanged);
            // 
            // k_factor_preview
            // 
            this.k_factor_preview.BackColor = System.Drawing.Color.Transparent;
            chartArea1.AxisX.ArrowStyle = System.Windows.Forms.DataVisualization.Charting.AxisArrowStyle.Triangle;
            chartArea1.AxisX.LineWidth = 2;
            chartArea1.AxisX.Minimum = 1D;
            chartArea1.AxisX.Title = "Temperature step";
            chartArea1.AxisX.TitleFont = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            chartArea1.AxisY.ArrowStyle = System.Windows.Forms.DataVisualization.Charting.AxisArrowStyle.Triangle;
            chartArea1.AxisY.Crossing = 15D;
            chartArea1.AxisY.Interval = 5D;
            chartArea1.AxisY.LineWidth = 2;
            chartArea1.AxisY.MajorGrid.Interval = 5D;
            chartArea1.AxisY.MajorGrid.LineColor = System.Drawing.Color.Silver;
            chartArea1.AxisY.Maximum = 85D;
            chartArea1.AxisY.Minimum = 15D;
            chartArea1.AxisY.Title = "Temperature in °C";
            chartArea1.AxisY.TitleFont = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            chartArea1.BackColor = System.Drawing.Color.Transparent;
            chartArea1.Name = "ChartArea1";
            this.k_factor_preview.ChartAreas.Add(chartArea1);
            legend1.Enabled = false;
            legend1.Name = "Legend1";
            this.k_factor_preview.Legends.Add(legend1);
            this.k_factor_preview.Location = new System.Drawing.Point(-1, 110);
            this.k_factor_preview.Name = "k_factor_preview";
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Point;
            series1.Legend = "Legend1";
            series1.MarkerBorderColor = System.Drawing.Color.Red;
            series1.MarkerBorderWidth = 0;
            series1.MarkerColor = System.Drawing.Color.Red;
            series1.MarkerSize = 15;
            series1.MarkerStyle = System.Windows.Forms.DataVisualization.Charting.MarkerStyle.Cross;
            series1.Name = "Temp_Points";
            this.k_factor_preview.Series.Add(series1);
            this.k_factor_preview.Size = new System.Drawing.Size(367, 460);
            this.k_factor_preview.TabIndex = 27;
            this.k_factor_preview.Text = "chart1";
            title1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold);
            title1.Name = "Title";
            title1.Text = "Preview:";
            this.k_factor_preview.Titles.Add(title1);
            // 
            // Window_Sensitivity_TempStepSelect
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(382, 615);
            this.ControlBox = false;
            this.Controls.Add(this.k_factor_preview);
            this.Controls.Add(this.textBox_Time);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.button_Cancel);
            this.Controls.Add(this.button_start);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.k_factor_current);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.numberOfSteps);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.stop_Temp);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.start_Temp);
            this.Name = "Window_Sensitivity_TempStepSelect";
            this.Text = "K_Factor_Window";
            ((System.ComponentModel.ISupportInitialize)(this.k_factor_current)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numberOfSteps)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stop_Temp)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.start_Temp)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.k_factor_preview)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox_Time;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button button_Cancel;
        private System.Windows.Forms.Button button_start;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.NumericUpDown k_factor_current;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown numberOfSteps;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown stop_Temp;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown start_Temp;
        private System.Windows.Forms.DataVisualization.Charting.Chart k_factor_preview;
    }
}