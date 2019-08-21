namespace RthTEC_Rack
{
    partial class Card_LED_Source
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.checkBox = new System.Windows.Forms.CheckBox();
            this.numericUpDown_I_Meas = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.numericUpDown_I_Heat = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.pictureBox = new System.Windows.Forms.PictureBox();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_I_Meas)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_I_Heat)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.SystemColors.Control;
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.checkBox);
            this.groupBox1.Controls.Add(this.numericUpDown_I_Meas);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.numericUpDown_I_Heat);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(4, 439);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(106, 237);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "LED Source:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.SystemColors.Control;
            this.label5.Location = new System.Drawing.Point(4, 18);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(51, 13);
            this.label5.TabIndex = 10;
            this.label5.Text = "Included:";
            // 
            // checkBox
            // 
            this.checkBox.AutoSize = true;
            this.checkBox.BackColor = System.Drawing.SystemColors.Control;
            this.checkBox.Location = new System.Drawing.Point(48, 17);
            this.checkBox.Name = "checkBox";
            this.checkBox.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.checkBox.Size = new System.Drawing.Size(50, 17);
            this.checkBox.TabIndex = 9;
            this.checkBox.Text = "        ";
            this.checkBox.UseVisualStyleBackColor = false;
            this.checkBox.CheckedChanged += new System.EventHandler(this.CheckBox_CheckedChanged);
            // 
            // numericUpDown_I_Meas
            // 
            this.numericUpDown_I_Meas.Location = new System.Drawing.Point(5, 108);
            this.numericUpDown_I_Meas.Name = "numericUpDown_I_Meas";
            this.numericUpDown_I_Meas.Size = new System.Drawing.Size(93, 20);
            this.numericUpDown_I_Meas.TabIndex = 3;
            this.numericUpDown_I_Meas.ValueChanged += new System.EventHandler(this.NumericUpDown_I_Meas_ValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.SystemColors.Control;
            this.label2.Location = new System.Drawing.Point(3, 93);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(69, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "I_Meas [mA]:";
            // 
            // numericUpDown_I_Heat
            // 
            this.numericUpDown_I_Heat.Location = new System.Drawing.Point(5, 58);
            this.numericUpDown_I_Heat.Name = "numericUpDown_I_Heat";
            this.numericUpDown_I_Heat.Size = new System.Drawing.Size(93, 20);
            this.numericUpDown_I_Heat.TabIndex = 1;
            this.numericUpDown_I_Heat.ValueChanged += new System.EventHandler(this.NnumericUpDown_I_Heat_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.SystemColors.Control;
            this.label1.Location = new System.Drawing.Point(3, 43);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(66, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "I_Heat [mA]:";
            // 
            // pictureBox
            // 
            this.pictureBox.Image = global::RthTEC_Rack.Properties.Resources.LED_Source;
            this.pictureBox.Location = new System.Drawing.Point(0, 0);
            this.pictureBox.Name = "pictureBox";
            this.pictureBox.Size = new System.Drawing.Size(116, 397);
            this.pictureBox.TabIndex = 5;
            this.pictureBox.TabStop = false;
            // 
            // Card_LED_Source
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.pictureBox);
            this.Name = "Card_LED_Source";
            this.Size = new System.Drawing.Size(116, 680);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_I_Meas)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_I_Heat)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.GroupBox groupBox1;
        internal System.Windows.Forms.Label label5;
        internal System.Windows.Forms.CheckBox checkBox;
        internal System.Windows.Forms.NumericUpDown numericUpDown_I_Meas;
        internal System.Windows.Forms.Label label2;
        internal System.Windows.Forms.NumericUpDown numericUpDown_I_Heat;
        internal System.Windows.Forms.Label label1;
        internal System.Windows.Forms.PictureBox pictureBox;
    }
}
