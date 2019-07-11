namespace Rth_Rack_Window
{
    partial class Window_RthTEC_Rack
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Window_RthTEC_Rack));
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.comboBox_Mode = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.numericUpDown_count_DPA = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.numericUpDown_t_DPA = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.numericUpDown_t_Sense = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.numericUpDown_t_Heat = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.pictureBox_Mode = new System.Windows.Forms.PictureBox();
            this.button1 = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.statusBar_label_Status = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusBar_Label_Status_Indikator = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusBar_Label_Message = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusBar_TextBox_Message = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusBar_Label_Answer = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusBar_TextBox_Answer = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_count_DPA)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_t_DPA)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_t_Sense)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_t_Heat)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Mode)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::Rth_Rack_Window.Properties.Resources.Gehause;
            this.pictureBox1.InitialImage = ((System.Drawing.Image)(resources.GetObject("pictureBox1.InitialImage")));
            this.pictureBox1.Location = new System.Drawing.Point(35, 64);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(880, 456);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.comboBox_Mode);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.numericUpDown_count_DPA);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.numericUpDown_t_DPA);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.numericUpDown_t_Sense);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.numericUpDown_t_Heat);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.pictureBox_Mode);
            this.groupBox1.Location = new System.Drawing.Point(35, 532);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(363, 237);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Pulse mode:";
            // 
            // comboBox_Mode
            // 
            this.comboBox_Mode.FormattingEnabled = true;
            this.comboBox_Mode.Items.AddRange(new object[] {
            "std. TTA",
            "DPA TTA",
            "Sensitivity",
            "Pre Pulse"});
            this.comboBox_Mode.Location = new System.Drawing.Point(115, 23);
            this.comboBox_Mode.Name = "comboBox_Mode";
            this.comboBox_Mode.Size = new System.Drawing.Size(121, 21);
            this.comboBox_Mode.TabIndex = 20;
            this.comboBox_Mode.SelectedIndexChanged += new System.EventHandler(this.ComboBox_Mode_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.SystemColors.Control;
            this.label5.Location = new System.Drawing.Point(14, 26);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(37, 13);
            this.label5.TabIndex = 19;
            this.label5.Text = "Mode:";
            // 
            // numericUpDown_count_DPA
            // 
            this.numericUpDown_count_DPA.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericUpDown_count_DPA.Location = new System.Drawing.Point(258, 207);
            this.numericUpDown_count_DPA.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numericUpDown_count_DPA.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown_count_DPA.Name = "numericUpDown_count_DPA";
            this.numericUpDown_count_DPA.Size = new System.Drawing.Size(93, 20);
            this.numericUpDown_count_DPA.TabIndex = 18;
            this.numericUpDown_count_DPA.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericUpDown_count_DPA.ValueChanged += new System.EventHandler(this.NumericUpDown_count_DPA_ValueChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.SystemColors.Control;
            this.label4.Location = new System.Drawing.Point(256, 192);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(66, 13);
            this.label4.TabIndex = 17;
            this.label4.Text = "Pulse count:";
            // 
            // numericUpDown_t_DPA
            // 
            this.numericUpDown_t_DPA.DecimalPlaces = 1;
            this.numericUpDown_t_DPA.Location = new System.Drawing.Point(258, 157);
            this.numericUpDown_t_DPA.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericUpDown_t_DPA.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numericUpDown_t_DPA.Name = "numericUpDown_t_DPA";
            this.numericUpDown_t_DPA.Size = new System.Drawing.Size(93, 20);
            this.numericUpDown_t_DPA.TabIndex = 16;
            this.numericUpDown_t_DPA.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown_t_DPA.ValueChanged += new System.EventHandler(this.NumericUpDown_t_DPA_ValueChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.SystemColors.Control;
            this.label3.Location = new System.Drawing.Point(256, 142);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(106, 13);
            this.label3.TabIndex = 15;
            this.label3.Text = "Singe pulse time [ms]";
            // 
            // numericUpDown_t_Sense
            // 
            this.numericUpDown_t_Sense.Increment = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.numericUpDown_t_Sense.Location = new System.Drawing.Point(258, 107);
            this.numericUpDown_t_Sense.Maximum = new decimal(new int[] {
            120000,
            0,
            0,
            0});
            this.numericUpDown_t_Sense.Name = "numericUpDown_t_Sense";
            this.numericUpDown_t_Sense.Size = new System.Drawing.Size(93, 20);
            this.numericUpDown_t_Sense.TabIndex = 14;
            this.numericUpDown_t_Sense.Value = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numericUpDown_t_Sense.ValueChanged += new System.EventHandler(this.NumericUpDown_t_Sense_ValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.SystemColors.Control;
            this.label2.Location = new System.Drawing.Point(256, 92);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(81, 13);
            this.label2.TabIndex = 13;
            this.label2.Text = "Sense time [ms]";
            // 
            // numericUpDown_t_Heat
            // 
            this.numericUpDown_t_Heat.Increment = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.numericUpDown_t_Heat.Location = new System.Drawing.Point(258, 57);
            this.numericUpDown_t_Heat.Maximum = new decimal(new int[] {
            120000,
            0,
            0,
            0});
            this.numericUpDown_t_Heat.Name = "numericUpDown_t_Heat";
            this.numericUpDown_t_Heat.Size = new System.Drawing.Size(93, 20);
            this.numericUpDown_t_Heat.TabIndex = 12;
            this.numericUpDown_t_Heat.Value = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numericUpDown_t_Heat.ValueChanged += new System.EventHandler(this.NumericUpDown_t_Heat_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.SystemColors.Control;
            this.label1.Location = new System.Drawing.Point(256, 42);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 13);
            this.label1.TabIndex = 11;
            this.label1.Text = "Heat time [ms]:";
            // 
            // pictureBox_Mode
            // 
            this.pictureBox_Mode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox_Mode.Image = global::Rth_Rack_Window.Properties.Resources.PrePulse;
            this.pictureBox_Mode.Location = new System.Drawing.Point(16, 57);
            this.pictureBox_Mode.Name = "pictureBox_Mode";
            this.pictureBox_Mode.Size = new System.Drawing.Size(220, 170);
            this.pictureBox_Mode.TabIndex = 0;
            this.pictureBox_Mode.TabStop = false;
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(34, 775);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(851, 39);
            this.button1.TabIndex = 13;
            this.button1.Text = "Pulse";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.Button_StartPulse_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusBar_label_Status,
            this.statusBar_Label_Status_Indikator,
            this.toolStripStatusLabel1,
            this.statusBar_Label_Message,
            this.statusBar_TextBox_Message,
            this.statusBar_Label_Answer,
            this.statusBar_TextBox_Answer,
            this.toolStripStatusLabel2});
            this.statusStrip1.Location = new System.Drawing.Point(0, 826);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(956, 22);
            this.statusStrip1.TabIndex = 14;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // statusBar_label_Status
            // 
            this.statusBar_label_Status.Name = "statusBar_label_Status";
            this.statusBar_label_Status.Size = new System.Drawing.Size(42, 17);
            this.statusBar_label_Status.Text = "Status:";
            // 
            // statusBar_Label_Status_Indikator
            // 
            this.statusBar_Label_Status_Indikator.Name = "statusBar_Label_Status_Indikator";
            this.statusBar_Label_Status_Indikator.Size = new System.Drawing.Size(16, 17);
            this.statusBar_Label_Status_Indikator.Text = "   ";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(0, 17);
            // 
            // statusBar_Label_Message
            // 
            this.statusBar_Label_Message.Name = "statusBar_Label_Message";
            this.statusBar_Label_Message.Size = new System.Drawing.Size(89, 17);
            this.statusBar_Label_Message.Text = "| Last Message: ";
            // 
            // statusBar_TextBox_Message
            // 
            this.statusBar_TextBox_Message.Name = "statusBar_TextBox_Message";
            this.statusBar_TextBox_Message.Size = new System.Drawing.Size(70, 17);
            this.statusBar_TextBox_Message.Text = "no Message";
            // 
            // statusBar_Label_Answer
            // 
            this.statusBar_Label_Answer.Name = "statusBar_Label_Answer";
            this.statusBar_Label_Answer.Size = new System.Drawing.Size(82, 17);
            this.statusBar_Label_Answer.Text = "| Last Answer: ";
            // 
            // statusBar_TextBox_Answer
            // 
            this.statusBar_TextBox_Answer.Name = "statusBar_TextBox_Answer";
            this.statusBar_TextBox_Answer.Size = new System.Drawing.Size(63, 17);
            this.statusBar_TextBox_Answer.Text = "no Answer";
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(10, 17);
            this.toolStripStatusLabel2.Text = "|";
            // 
            // Window_RthTEC_Rack
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(956, 848);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.pictureBox1);
            this.Name = "Window_RthTEC_Rack";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Window_RthTEC_Rack_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_count_DPA)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_t_DPA)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_t_Sense)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_t_Heat)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Mode)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.PictureBox pictureBox_Mode;
        internal System.Windows.Forms.Label label5;
        internal System.Windows.Forms.NumericUpDown numericUpDown_count_DPA;
        internal System.Windows.Forms.Label label4;
        internal System.Windows.Forms.NumericUpDown numericUpDown_t_DPA;
        internal System.Windows.Forms.Label label3;
        internal System.Windows.Forms.NumericUpDown numericUpDown_t_Sense;
        internal System.Windows.Forms.Label label2;
        internal System.Windows.Forms.NumericUpDown numericUpDown_t_Heat;
        internal System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBox_Mode;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel statusBar_label_Status;
        private System.Windows.Forms.ToolStripStatusLabel statusBar_Label_Status_Indikator;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel statusBar_Label_Message;
        private System.Windows.Forms.ToolStripStatusLabel statusBar_TextBox_Message;
        private System.Windows.Forms.ToolStripStatusLabel statusBar_Label_Answer;
        private System.Windows.Forms.ToolStripStatusLabel statusBar_TextBox_Answer;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
    }
}

