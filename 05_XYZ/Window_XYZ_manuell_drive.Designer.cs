namespace ATIM_GUI._05_XYZ
{
    partial class Window_XYZ_manuell_drive
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Window_XYZ_manuell_drive));
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label15 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.button_Drive2Position = new System.Windows.Forms.Button();
            this.label12 = new System.Windows.Forms.Label();
            this.comboBox_Predifined = new System.Windows.Forms.ComboBox();
            this.numericUpDown_Angle = new System.Windows.Forms.NumericUpDown();
            this.label11 = new System.Windows.Forms.Label();
            this.numericUpDown_Y_Position = new System.Windows.Forms.NumericUpDown();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.numericUpDown_X_Position = new System.Windows.Forms.NumericUpDown();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.comboBox_Z = new System.Windows.Forms.ComboBox();
            this.manuell_Z_min = new System.Windows.Forms.Button();
            this.manuel_Z_max = new System.Windows.Forms.Button();
            this.manuel_Z_negativ = new System.Windows.Forms.Button();
            this.manuel_Z_positiv = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.comboBox_XY = new System.Windows.Forms.ComboBox();
            this.manuel_Y_negativ = new System.Windows.Forms.Button();
            this.manuel_Y_positiv = new System.Windows.Forms.Button();
            this.manuel_X_positiv = new System.Windows.Forms.Button();
            this.manuel_X_negativ = new System.Windows.Forms.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.Rotate_Clockwise = new DevExpress.XtraEditors.SimpleButton();
            this.Rotate_inverse_Clockwise = new DevExpress.XtraEditors.SimpleButton();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.comboBox_Angle = new System.Windows.Forms.ComboBox();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_Angle)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_Y_Position)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_X_Position)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label15);
            this.groupBox3.Controls.Add(this.label14);
            this.groupBox3.Controls.Add(this.label13);
            this.groupBox3.Controls.Add(this.button_Drive2Position);
            this.groupBox3.Controls.Add(this.label12);
            this.groupBox3.Controls.Add(this.comboBox_Predifined);
            this.groupBox3.Controls.Add(this.numericUpDown_Angle);
            this.groupBox3.Controls.Add(this.label11);
            this.groupBox3.Controls.Add(this.numericUpDown_Y_Position);
            this.groupBox3.Controls.Add(this.label8);
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Controls.Add(this.numericUpDown_X_Position);
            this.groupBox3.Location = new System.Drawing.Point(777, 4);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(174, 350);
            this.groupBox3.TabIndex = 12;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Drive to defined point";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(146, 158);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(23, 13);
            this.label15.TabIndex = 20;
            this.label15.Text = "mm";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(146, 121);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(23, 13);
            this.label14.TabIndex = 19;
            this.label14.Text = "mm";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(146, 85);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(23, 13);
            this.label13.TabIndex = 18;
            this.label13.Text = "mm";
            // 
            // button_Drive2Position
            // 
            this.button_Drive2Position.Location = new System.Drawing.Point(9, 193);
            this.button_Drive2Position.Name = "button_Drive2Position";
            this.button_Drive2Position.Size = new System.Drawing.Size(159, 31);
            this.button_Drive2Position.TabIndex = 17;
            this.button_Drive2Position.Text = "Drive to position";
            this.button_Drive2Position.UseVisualStyleBackColor = true;
            this.button_Drive2Position.Click += new System.EventHandler(this.Button_Drive2Position_Click);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(6, 33);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(101, 13);
            this.label12.TabIndex = 16;
            this.label12.Text = "Predifined positions:";
            // 
            // comboBox_Predifined
            // 
            this.comboBox_Predifined.FormattingEnabled = true;
            this.comboBox_Predifined.Location = new System.Drawing.Point(9, 49);
            this.comboBox_Predifined.Name = "comboBox_Predifined";
            this.comboBox_Predifined.Size = new System.Drawing.Size(159, 21);
            this.comboBox_Predifined.TabIndex = 15;
            this.comboBox_Predifined.SelectedIndexChanged += new System.EventHandler(this.ComboBox_Predifined_SelectedIndexChanged);
            // 
            // numericUpDown_Angle
            // 
            this.numericUpDown_Angle.Location = new System.Drawing.Point(67, 155);
            this.numericUpDown_Angle.Maximum = new decimal(new int[] {
            360,
            0,
            0,
            0});
            this.numericUpDown_Angle.Name = "numericUpDown_Angle";
            this.numericUpDown_Angle.Size = new System.Drawing.Size(77, 20);
            this.numericUpDown_Angle.TabIndex = 14;
            this.numericUpDown_Angle.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numericUpDown_Angle.UpDownAlign = System.Windows.Forms.LeftRightAlignment.Left;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(6, 157);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(37, 13);
            this.label11.TabIndex = 13;
            this.label11.Text = "Angle:";
            // 
            // numericUpDown_Y_Position
            // 
            this.numericUpDown_Y_Position.Location = new System.Drawing.Point(67, 118);
            this.numericUpDown_Y_Position.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numericUpDown_Y_Position.Minimum = new decimal(new int[] {
            1000,
            0,
            0,
            -2147483648});
            this.numericUpDown_Y_Position.Name = "numericUpDown_Y_Position";
            this.numericUpDown_Y_Position.Size = new System.Drawing.Size(77, 20);
            this.numericUpDown_Y_Position.TabIndex = 12;
            this.numericUpDown_Y_Position.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numericUpDown_Y_Position.UpDownAlign = System.Windows.Forms.LeftRightAlignment.Left;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 120);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(57, 13);
            this.label8.TabIndex = 11;
            this.label8.Text = "Y-Position:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 85);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(57, 13);
            this.label7.TabIndex = 10;
            this.label7.Text = "X-Position:";
            // 
            // numericUpDown_X_Position
            // 
            this.numericUpDown_X_Position.Location = new System.Drawing.Point(67, 83);
            this.numericUpDown_X_Position.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numericUpDown_X_Position.Minimum = new decimal(new int[] {
            1000,
            0,
            0,
            -2147483648});
            this.numericUpDown_X_Position.Name = "numericUpDown_X_Position";
            this.numericUpDown_X_Position.Size = new System.Drawing.Size(77, 20);
            this.numericUpDown_X_Position.TabIndex = 9;
            this.numericUpDown_X_Position.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numericUpDown_X_Position.UpDownAlign = System.Windows.Forms.LeftRightAlignment.Left;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.comboBox_Z);
            this.groupBox2.Controls.Add(this.manuell_Z_min);
            this.groupBox2.Controls.Add(this.manuel_Z_max);
            this.groupBox2.Controls.Add(this.manuel_Z_negativ);
            this.groupBox2.Controls.Add(this.manuel_Z_positiv);
            this.groupBox2.Location = new System.Drawing.Point(265, 4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(250, 350);
            this.groupBox2.TabIndex = 11;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Z - Axis";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(152, 313);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(69, 13);
            this.label6.TabIndex = 14;
            this.label6.Text = "Latch bottom";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(158, 128);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(52, 13);
            this.label5.TabIndex = 13;
            this.label5.Text = "Latch top";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(4, 39);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 13);
            this.label3.TabIndex = 12;
            this.label3.Text = "Distance Z";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(86, 57);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(23, 13);
            this.label4.TabIndex = 11;
            this.label4.Text = "mm";
            // 
            // comboBox_Z
            // 
            this.comboBox_Z.FormattingEnabled = true;
            this.comboBox_Z.Location = new System.Drawing.Point(6, 54);
            this.comboBox_Z.Name = "comboBox_Z";
            this.comboBox_Z.Size = new System.Drawing.Size(80, 21);
            this.comboBox_Z.TabIndex = 10;
            // 
            // manuell_Z_min
            // 
            this.manuell_Z_min.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("manuell_Z_min.BackgroundImage")));
            this.manuell_Z_min.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.manuell_Z_min.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.manuell_Z_min.Location = new System.Drawing.Point(145, 230);
            this.manuell_Z_min.Name = "manuell_Z_min";
            this.manuell_Z_min.Size = new System.Drawing.Size(80, 80);
            this.manuell_Z_min.TabIndex = 9;
            this.manuell_Z_min.Text = "Z- -";
            this.manuell_Z_min.UseVisualStyleBackColor = true;
            this.manuell_Z_min.Click += new System.EventHandler(this.manuell_Z_min_Click);
            // 
            // manuel_Z_max
            // 
            this.manuel_Z_max.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("manuel_Z_max.BackgroundImage")));
            this.manuel_Z_max.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.manuel_Z_max.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.manuel_Z_max.Location = new System.Drawing.Point(145, 144);
            this.manuel_Z_max.Name = "manuel_Z_max";
            this.manuel_Z_max.Size = new System.Drawing.Size(80, 80);
            this.manuel_Z_max.TabIndex = 8;
            this.manuel_Z_max.Text = "Z++";
            this.manuel_Z_max.UseVisualStyleBackColor = true;
            this.manuel_Z_max.Click += new System.EventHandler(this.manuel_Z_max_Click);
            // 
            // manuel_Z_negativ
            // 
            this.manuel_Z_negativ.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("manuel_Z_negativ.BackgroundImage")));
            this.manuel_Z_negativ.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.manuel_Z_negativ.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.manuel_Z_negativ.Location = new System.Drawing.Point(22, 230);
            this.manuel_Z_negativ.Name = "manuel_Z_negativ";
            this.manuel_Z_negativ.Size = new System.Drawing.Size(80, 80);
            this.manuel_Z_negativ.TabIndex = 7;
            this.manuel_Z_negativ.Text = "Z-";
            this.manuel_Z_negativ.UseVisualStyleBackColor = true;
            this.manuel_Z_negativ.Click += new System.EventHandler(this.manuel_Z_negativ_Click);
            // 
            // manuel_Z_positiv
            // 
            this.manuel_Z_positiv.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("manuel_Z_positiv.BackgroundImage")));
            this.manuel_Z_positiv.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.manuel_Z_positiv.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.manuel_Z_positiv.Location = new System.Drawing.Point(22, 144);
            this.manuel_Z_positiv.Name = "manuel_Z_positiv";
            this.manuel_Z_positiv.Size = new System.Drawing.Size(80, 80);
            this.manuel_Z_positiv.TabIndex = 7;
            this.manuel_Z_positiv.Text = "Z+";
            this.manuel_Z_positiv.UseVisualStyleBackColor = true;
            this.manuel_Z_positiv.Click += new System.EventHandler(this.manuel_Z_positiv_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.comboBox_XY);
            this.groupBox1.Controls.Add(this.manuel_Y_negativ);
            this.groupBox1.Controls.Add(this.manuel_Y_positiv);
            this.groupBox1.Controls.Add(this.manuel_X_positiv);
            this.groupBox1.Controls.Add(this.manuel_X_negativ);
            this.groupBox1.Location = new System.Drawing.Point(5, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(250, 350);
            this.groupBox1.TabIndex = 10;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "X / Y - Axis";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(4, 39);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Distance X/Y";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(86, 57);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(23, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "mm";
            // 
            // comboBox_XY
            // 
            this.comboBox_XY.FormattingEnabled = true;
            this.comboBox_XY.Location = new System.Drawing.Point(6, 54);
            this.comboBox_XY.Name = "comboBox_XY";
            this.comboBox_XY.Size = new System.Drawing.Size(80, 21);
            this.comboBox_XY.TabIndex = 4;
            // 
            // manuel_Y_negativ
            // 
            this.manuel_Y_negativ.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("manuel_Y_negativ.BackgroundImage")));
            this.manuel_Y_negativ.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.manuel_Y_negativ.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.manuel_Y_negativ.Location = new System.Drawing.Point(85, 264);
            this.manuel_Y_negativ.Name = "manuel_Y_negativ";
            this.manuel_Y_negativ.Size = new System.Drawing.Size(80, 80);
            this.manuel_Y_negativ.TabIndex = 3;
            this.manuel_Y_negativ.Text = "Y-";
            this.manuel_Y_negativ.UseVisualStyleBackColor = true;
            this.manuel_Y_negativ.Click += new System.EventHandler(this.manuel_Y_negativ_Click);
            // 
            // manuel_Y_positiv
            // 
            this.manuel_Y_positiv.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("manuel_Y_positiv.BackgroundImage")));
            this.manuel_Y_positiv.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.manuel_Y_positiv.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.manuel_Y_positiv.Location = new System.Drawing.Point(85, 104);
            this.manuel_Y_positiv.Name = "manuel_Y_positiv";
            this.manuel_Y_positiv.Size = new System.Drawing.Size(80, 80);
            this.manuel_Y_positiv.TabIndex = 2;
            this.manuel_Y_positiv.Text = "Y+";
            this.manuel_Y_positiv.UseVisualStyleBackColor = true;
            this.manuel_Y_positiv.Click += new System.EventHandler(this.manuel_Y_positiv_Click);
            // 
            // manuel_X_positiv
            // 
            this.manuel_X_positiv.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("manuel_X_positiv.BackgroundImage")));
            this.manuel_X_positiv.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.manuel_X_positiv.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.manuel_X_positiv.Location = new System.Drawing.Point(165, 184);
            this.manuel_X_positiv.Name = "manuel_X_positiv";
            this.manuel_X_positiv.Size = new System.Drawing.Size(80, 80);
            this.manuel_X_positiv.TabIndex = 1;
            this.manuel_X_positiv.Text = "X+";
            this.manuel_X_positiv.UseVisualStyleBackColor = true;
            this.manuel_X_positiv.Click += new System.EventHandler(this.manuel_X_positiv_Click);
            // 
            // manuel_X_negativ
            // 
            this.manuel_X_negativ.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("manuel_X_negativ.BackgroundImage")));
            this.manuel_X_negativ.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.manuel_X_negativ.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.manuel_X_negativ.Location = new System.Drawing.Point(5, 184);
            this.manuel_X_negativ.Name = "manuel_X_negativ";
            this.manuel_X_negativ.Size = new System.Drawing.Size(80, 80);
            this.manuel_X_negativ.TabIndex = 0;
            this.manuel_X_negativ.Text = "X-";
            this.manuel_X_negativ.UseVisualStyleBackColor = true;
            this.manuel_X_negativ.Click += new System.EventHandler(this.manuel_X_negativ_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.Rotate_Clockwise);
            this.groupBox4.Controls.Add(this.Rotate_inverse_Clockwise);
            this.groupBox4.Controls.Add(this.label9);
            this.groupBox4.Controls.Add(this.label10);
            this.groupBox4.Controls.Add(this.comboBox_Angle);
            this.groupBox4.Location = new System.Drawing.Point(521, 4);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(250, 350);
            this.groupBox4.TabIndex = 15;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Angle";
            // 
            // Rotate_Clockwise
            // 
            this.Rotate_Clockwise.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("Rotate_Clockwise.ImageOptions.Image")));
            this.Rotate_Clockwise.Location = new System.Drawing.Point(133, 199);
            this.Rotate_Clockwise.Name = "Rotate_Clockwise";
            this.Rotate_Clockwise.Size = new System.Drawing.Size(52, 55);
            this.Rotate_Clockwise.TabIndex = 14;
            this.Rotate_Clockwise.Click += new System.EventHandler(this.SimpleButton_Clockwise_Click);
            // 
            // Rotate_inverse_Clockwise
            // 
            this.Rotate_inverse_Clockwise.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("Rotate_inverse_Clockwise.ImageOptions.Image")));
            this.Rotate_inverse_Clockwise.Location = new System.Drawing.Point(75, 198);
            this.Rotate_inverse_Clockwise.Name = "Rotate_inverse_Clockwise";
            this.Rotate_inverse_Clockwise.Size = new System.Drawing.Size(52, 55);
            this.Rotate_inverse_Clockwise.TabIndex = 13;
            this.Rotate_inverse_Clockwise.Click += new System.EventHandler(this.SimpleButton_inverse_Clockwise_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(4, 39);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(34, 13);
            this.label9.TabIndex = 12;
            this.label9.Text = "Angle";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(86, 57);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(42, 13);
            this.label10.TabIndex = 11;
            this.label10.Text = "Degree";
            // 
            // comboBox_Angle
            // 
            this.comboBox_Angle.FormattingEnabled = true;
            this.comboBox_Angle.Location = new System.Drawing.Point(6, 54);
            this.comboBox_Angle.Name = "comboBox_Angle";
            this.comboBox_Angle.Size = new System.Drawing.Size(80, 21);
            this.comboBox_Angle.TabIndex = 10;
            // 
            // Window_XYZ_manuell_drive
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(956, 358);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "Window_XYZ_manuell_drive";
            this.Text = "Window_XYZ_manuell_drive";
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_Angle)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_Y_Position)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_X_Position)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox comboBox_Z;
        private System.Windows.Forms.Button manuell_Z_min;
        private System.Windows.Forms.Button manuel_Z_max;
        private System.Windows.Forms.Button manuel_Z_negativ;
        private System.Windows.Forms.Button manuel_Z_positiv;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBox_XY;
        private System.Windows.Forms.Button manuel_Y_negativ;
        private System.Windows.Forms.Button manuel_Y_positiv;
        private System.Windows.Forms.Button manuel_X_positiv;
        private System.Windows.Forms.Button manuel_X_negativ;
        private System.Windows.Forms.GroupBox groupBox4;
        private DevExpress.XtraEditors.SimpleButton Rotate_Clockwise;
        private DevExpress.XtraEditors.SimpleButton Rotate_inverse_Clockwise;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox comboBox_Angle;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Button button_Drive2Position;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.ComboBox comboBox_Predifined;
        private System.Windows.Forms.NumericUpDown numericUpDown_Angle;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.NumericUpDown numericUpDown_Y_Position;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.NumericUpDown numericUpDown_X_Position;
    }
}