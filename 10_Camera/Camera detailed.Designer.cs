namespace DXApplication3._10_Camera
{
    partial class Camera_detailed
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
            this.pictureBox_Main = new System.Windows.Forms.PictureBox();
            this.trackBar_RED = new System.Windows.Forms.TrackBar();
            this.trackBar_GREEN = new System.Windows.Forms.TrackBar();
            this.trackBar_BLUE = new System.Windows.Forms.TrackBar();
            this.trackBar_Radius = new System.Windows.Forms.TrackBar();
            this.pictureBox_ColorFilter = new System.Windows.Forms.PictureBox();
            this.pictureBox_GreyScale = new System.Windows.Forms.PictureBox();
            this.pictureBox_Edge = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.pictureBox_Threshold = new System.Windows.Forms.PictureBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.trackBar_Threshold = new System.Windows.Forms.TrackBar();
            this.label10 = new System.Windows.Forms.Label();
            this.pictureBox_Origninal = new System.Windows.Forms.PictureBox();
            this.label_red = new System.Windows.Forms.Label();
            this.label_green = new System.Windows.Forms.Label();
            this.label_blue = new System.Windows.Forms.Label();
            this.label_radius = new System.Windows.Forms.Label();
            this.label_threshold = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.pictureBox_Marked = new System.Windows.Forms.PictureBox();
            this.textBox_Objects = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Main)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_RED)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_GREEN)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_BLUE)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_Radius)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_ColorFilter)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_GreyScale)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Edge)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Threshold)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_Threshold)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Origninal)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Marked)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox_Main
            // 
            this.pictureBox_Main.Location = new System.Drawing.Point(434, 9);
            this.pictureBox_Main.Name = "pictureBox_Main";
            this.pictureBox_Main.Size = new System.Drawing.Size(600, 480);
            this.pictureBox_Main.TabIndex = 0;
            this.pictureBox_Main.TabStop = false;
            // 
            // trackBar_RED
            // 
            this.trackBar_RED.Location = new System.Drawing.Point(12, 24);
            this.trackBar_RED.Maximum = 255;
            this.trackBar_RED.Name = "trackBar_RED";
            this.trackBar_RED.Size = new System.Drawing.Size(195, 45);
            this.trackBar_RED.TabIndex = 1;
            this.trackBar_RED.Scroll += new System.EventHandler(this.TrackBar_RED_Scroll);
            // 
            // trackBar_GREEN
            // 
            this.trackBar_GREEN.Location = new System.Drawing.Point(12, 75);
            this.trackBar_GREEN.Maximum = 255;
            this.trackBar_GREEN.Name = "trackBar_GREEN";
            this.trackBar_GREEN.Size = new System.Drawing.Size(195, 45);
            this.trackBar_GREEN.TabIndex = 2;
            this.trackBar_GREEN.Scroll += new System.EventHandler(this.TrackBar_GREEN_Scroll);
            // 
            // trackBar_BLUE
            // 
            this.trackBar_BLUE.Location = new System.Drawing.Point(12, 136);
            this.trackBar_BLUE.Maximum = 255;
            this.trackBar_BLUE.Name = "trackBar_BLUE";
            this.trackBar_BLUE.Size = new System.Drawing.Size(195, 45);
            this.trackBar_BLUE.TabIndex = 3;
            this.trackBar_BLUE.Scroll += new System.EventHandler(this.TrackBar_BLUE_Scroll);
            // 
            // trackBar_Radius
            // 
            this.trackBar_Radius.Location = new System.Drawing.Point(12, 187);
            this.trackBar_Radius.Maximum = 255;
            this.trackBar_Radius.Name = "trackBar_Radius";
            this.trackBar_Radius.Size = new System.Drawing.Size(195, 45);
            this.trackBar_Radius.TabIndex = 4;
            this.trackBar_Radius.Scroll += new System.EventHandler(this.TrackBar_Radius_Scroll);
            // 
            // pictureBox_ColorFilter
            // 
            this.pictureBox_ColorFilter.Location = new System.Drawing.Point(214, 516);
            this.pictureBox_ColorFilter.Name = "pictureBox_ColorFilter";
            this.pictureBox_ColorFilter.Size = new System.Drawing.Size(200, 160);
            this.pictureBox_ColorFilter.TabIndex = 5;
            this.pictureBox_ColorFilter.TabStop = false;
            this.pictureBox_ColorFilter.Click += new System.EventHandler(this.PictureBox_RGB_Filter_Click);
            // 
            // pictureBox_GreyScale
            // 
            this.pictureBox_GreyScale.Location = new System.Drawing.Point(420, 516);
            this.pictureBox_GreyScale.Name = "pictureBox_GreyScale";
            this.pictureBox_GreyScale.Size = new System.Drawing.Size(200, 160);
            this.pictureBox_GreyScale.TabIndex = 6;
            this.pictureBox_GreyScale.TabStop = false;
            this.pictureBox_GreyScale.Click += new System.EventHandler(this.PictureBox_Greyscale_Click);
            // 
            // pictureBox_Edge
            // 
            this.pictureBox_Edge.Location = new System.Drawing.Point(626, 516);
            this.pictureBox_Edge.Name = "pictureBox_Edge";
            this.pictureBox_Edge.Size = new System.Drawing.Size(200, 160);
            this.pictureBox_Edge.TabIndex = 7;
            this.pictureBox_Edge.TabStop = false;
            this.pictureBox_Edge.Click += new System.EventHandler(this.PictureBox_Edge_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(212, 500);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "Color-Filter";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(417, 500);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(54, 13);
            this.label2.TabIndex = 9;
            this.label2.Text = "Grayscale";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(623, 500);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(81, 13);
            this.label3.TabIndex = 10;
            this.label3.Text = "Edge-Detection";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(831, 501);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(54, 13);
            this.label4.TabIndex = 12;
            this.label4.Text = "Threshold";
            // 
            // pictureBox_Threshold
            // 
            this.pictureBox_Threshold.Location = new System.Drawing.Point(834, 517);
            this.pictureBox_Threshold.Name = "pictureBox_Threshold";
            this.pictureBox_Threshold.Size = new System.Drawing.Size(200, 159);
            this.pictureBox_Threshold.TabIndex = 11;
            this.pictureBox_Threshold.TabStop = false;
            this.pictureBox_Threshold.Click += new System.EventHandler(this.PictureBox_Threshold_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 9);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(30, 13);
            this.label5.TabIndex = 13;
            this.label5.Text = "RED";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 59);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(45, 13);
            this.label6.TabIndex = 14;
            this.label6.Text = "GREEN";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(16, 120);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(35, 13);
            this.label7.TabIndex = 15;
            this.label7.Text = "BLUE";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(16, 171);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(48, 13);
            this.label8.TabIndex = 16;
            this.label8.Text = "RADIUS";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(16, 260);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(74, 13);
            this.label9.TabIndex = 18;
            this.label9.Text = "THRESHOLD";
            // 
            // trackBar_Threshold
            // 
            this.trackBar_Threshold.Location = new System.Drawing.Point(12, 276);
            this.trackBar_Threshold.Maximum = 255;
            this.trackBar_Threshold.Name = "trackBar_Threshold";
            this.trackBar_Threshold.Size = new System.Drawing.Size(195, 45);
            this.trackBar_Threshold.TabIndex = 17;
            this.trackBar_Threshold.Scroll += new System.EventHandler(this.TrackBar_Threshold_Scroll);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(6, 500);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(42, 13);
            this.label10.TabIndex = 20;
            this.label10.Text = "Original";
            // 
            // pictureBox_Origninal
            // 
            this.pictureBox_Origninal.Location = new System.Drawing.Point(8, 516);
            this.pictureBox_Origninal.Name = "pictureBox_Origninal";
            this.pictureBox_Origninal.Size = new System.Drawing.Size(200, 160);
            this.pictureBox_Origninal.TabIndex = 19;
            this.pictureBox_Origninal.TabStop = false;
            this.pictureBox_Origninal.Click += new System.EventHandler(this.PictureBox_Original_Click);
            // 
            // label_red
            // 
            this.label_red.AutoSize = true;
            this.label_red.Location = new System.Drawing.Point(215, 24);
            this.label_red.Name = "label_red";
            this.label_red.Size = new System.Drawing.Size(50, 13);
            this.label_red.TabIndex = 21;
            this.label_red.Text = "label_red";
            // 
            // label_green
            // 
            this.label_green.AutoSize = true;
            this.label_green.Location = new System.Drawing.Point(215, 75);
            this.label_green.Name = "label_green";
            this.label_green.Size = new System.Drawing.Size(62, 13);
            this.label_green.TabIndex = 22;
            this.label_green.Text = "label_green";
            // 
            // label_blue
            // 
            this.label_blue.AutoSize = true;
            this.label_blue.Location = new System.Drawing.Point(215, 136);
            this.label_blue.Name = "label_blue";
            this.label_blue.Size = new System.Drawing.Size(55, 13);
            this.label_blue.TabIndex = 23;
            this.label_blue.Text = "label_blue";
            // 
            // label_radius
            // 
            this.label_radius.AutoSize = true;
            this.label_radius.Location = new System.Drawing.Point(215, 187);
            this.label_radius.Name = "label_radius";
            this.label_radius.Size = new System.Drawing.Size(63, 13);
            this.label_radius.TabIndex = 24;
            this.label_radius.Text = "label_radius";
            // 
            // label_threshold
            // 
            this.label_threshold.AutoSize = true;
            this.label_threshold.Location = new System.Drawing.Point(215, 276);
            this.label_threshold.Name = "label_threshold";
            this.label_threshold.Size = new System.Drawing.Size(41, 13);
            this.label_threshold.TabIndex = 25;
            this.label_threshold.Text = "label15";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(1037, 501);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(82, 13);
            this.label11.TabIndex = 27;
            this.label11.Text = "Marked Objects";
            // 
            // pictureBox_Marked
            // 
            this.pictureBox_Marked.Location = new System.Drawing.Point(1040, 517);
            this.pictureBox_Marked.Name = "pictureBox_Marked";
            this.pictureBox_Marked.Size = new System.Drawing.Size(200, 159);
            this.pictureBox_Marked.TabIndex = 26;
            this.pictureBox_Marked.TabStop = false;
            this.pictureBox_Marked.Click += new System.EventHandler(this.PictureBox_Marked_Click);
            // 
            // textBox_Objects
            // 
            this.textBox_Objects.Location = new System.Drawing.Point(1040, 24);
            this.textBox_Objects.Multiline = true;
            this.textBox_Objects.Name = "textBox_Objects";
            this.textBox_Objects.Size = new System.Drawing.Size(204, 465);
            this.textBox_Objects.TabIndex = 28;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(1040, 9);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(76, 13);
            this.label12.TabIndex = 29;
            this.label12.Text = "Found Objects";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(8, 401);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(204, 78);
            this.textBox1.TabIndex = 30;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(5, 385);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(29, 13);
            this.label13.TabIndex = 31;
            this.label13.Text = "Pixel";
            // 
            // Camera_detailed
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1256, 681);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.textBox_Objects);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.pictureBox_Marked);
            this.Controls.Add(this.label_threshold);
            this.Controls.Add(this.label_radius);
            this.Controls.Add(this.label_blue);
            this.Controls.Add(this.label_green);
            this.Controls.Add(this.label_red);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.pictureBox_Origninal);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.trackBar_Threshold);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.pictureBox_Threshold);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictureBox_Edge);
            this.Controls.Add(this.pictureBox_GreyScale);
            this.Controls.Add(this.pictureBox_ColorFilter);
            this.Controls.Add(this.trackBar_Radius);
            this.Controls.Add(this.trackBar_BLUE);
            this.Controls.Add(this.trackBar_GREEN);
            this.Controls.Add(this.trackBar_RED);
            this.Controls.Add(this.pictureBox_Main);
            this.Name = "Camera_detailed";
            this.Text = "Camera_detailed";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Main)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_RED)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_GREEN)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_BLUE)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_Radius)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_ColorFilter)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_GreyScale)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Edge)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Threshold)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_Threshold)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Origninal)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Marked)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox_Main;
        private System.Windows.Forms.TrackBar trackBar_RED;
        private System.Windows.Forms.TrackBar trackBar_GREEN;
        private System.Windows.Forms.TrackBar trackBar_BLUE;
        private System.Windows.Forms.TrackBar trackBar_Radius;
        private System.Windows.Forms.PictureBox pictureBox_ColorFilter;
        private System.Windows.Forms.PictureBox pictureBox_GreyScale;
        private System.Windows.Forms.PictureBox pictureBox_Edge;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.PictureBox pictureBox_Threshold;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TrackBar trackBar_Threshold;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.PictureBox pictureBox_Origninal;
        private System.Windows.Forms.Label label_red;
        private System.Windows.Forms.Label label_green;
        private System.Windows.Forms.Label label_blue;
        private System.Windows.Forms.Label label_radius;
        private System.Windows.Forms.Label label_threshold;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.PictureBox pictureBox_Marked;
        private System.Windows.Forms.TextBox textBox_Objects;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label13;
    }
}