namespace RthTEC_Rack
{
    partial class Card_Amplifier
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Card_Amplifier));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.checkBox = new System.Windows.Forms.CheckBox();
            this.numericUpDown_Offset = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.numericUpDown_Gain = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.pictureBox = new System.Windows.Forms.PictureBox();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_Offset)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_Gain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.SystemColors.Control;
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.checkBox);
            this.groupBox1.Controls.Add(this.numericUpDown_Offset);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.numericUpDown_Gain);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(4, 439);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(106, 237);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Amplifier:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.SystemColors.Control;
            this.label5.Location = new System.Drawing.Point(4, 19);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(51, 13);
            this.label5.TabIndex = 10;
            this.label5.Text = "Included:";
            // 
            // checkBox
            // 
            this.checkBox.AutoSize = true;
            this.checkBox.BackColor = System.Drawing.SystemColors.Control;
            this.checkBox.Location = new System.Drawing.Point(48, 18);
            this.checkBox.Name = "checkBox";
            this.checkBox.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.checkBox.Size = new System.Drawing.Size(50, 17);
            this.checkBox.TabIndex = 9;
            this.checkBox.Text = "        ";
            this.checkBox.UseVisualStyleBackColor = false;
            this.checkBox.CheckedChanged += new System.EventHandler(this.CheckBox_CheckedChanged);
            // 
            // numericUpDown_Offset
            // 
            this.numericUpDown_Offset.Location = new System.Drawing.Point(5, 109);
            this.numericUpDown_Offset.Name = "numericUpDown_Offset";
            this.numericUpDown_Offset.Size = new System.Drawing.Size(93, 20);
            this.numericUpDown_Offset.TabIndex = 3;
            this.numericUpDown_Offset.ValueChanged += new System.EventHandler(this.NumericUpDown_Offset_ValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.SystemColors.Control;
            this.label2.Location = new System.Drawing.Point(3, 94);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(64, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "V_Offset [V]";
            // 
            // numericUpDown_Gain
            // 
            this.numericUpDown_Gain.Location = new System.Drawing.Point(5, 59);
            this.numericUpDown_Gain.Name = "numericUpDown_Gain";
            this.numericUpDown_Gain.Size = new System.Drawing.Size(93, 20);
            this.numericUpDown_Gain.TabIndex = 1;
            this.numericUpDown_Gain.ValueChanged += new System.EventHandler(this.NumericUpDown_Gain_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.SystemColors.Control;
            this.label1.Location = new System.Drawing.Point(3, 44);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(32, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Gain:";
            // 
            // pictureBox
            // 
            this.pictureBox.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox.Image")));
            this.pictureBox.Location = new System.Drawing.Point(0, 0);
            this.pictureBox.Name = "pictureBox";
            this.pictureBox.Size = new System.Drawing.Size(116, 397);
            this.pictureBox.TabIndex = 5;
            this.pictureBox.TabStop = false;
            // 
            // Card_Amplifier
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.pictureBox);
            this.Name = "Card_Amplifier";
            this.Size = new System.Drawing.Size(116, 680);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_Offset)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_Gain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.GroupBox groupBox1;
        internal System.Windows.Forms.Label label5;
        internal System.Windows.Forms.CheckBox checkBox;
        internal System.Windows.Forms.NumericUpDown numericUpDown_Offset;
        internal System.Windows.Forms.Label label2;
        internal System.Windows.Forms.NumericUpDown numericUpDown_Gain;
        internal System.Windows.Forms.Label label1;
        internal System.Windows.Forms.PictureBox pictureBox;
    }
}
