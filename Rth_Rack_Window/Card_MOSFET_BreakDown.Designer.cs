namespace RthTEC_Rack
{
    partial class Card_MOSFET_BreakDown
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
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.numericUpDown_U_GS = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.pictureBox = new System.Windows.Forms.PictureBox();
            this.comboBox_Relais = new System.Windows.Forms.ComboBox();
            this.button1 = new System.Windows.Forms.Button();
            this.textBox_U_DS = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.textBox_I_DS = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_U_GS)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.SystemColors.Control;
            this.groupBox1.Controls.Add(this.button2);
            this.groupBox1.Controls.Add(this.textBox_I_DS);
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.textBox_U_DS);
            this.groupBox1.Controls.Add(this.comboBox_Relais);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.checkBox);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.numericUpDown_U_GS);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(4, 439);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(106, 237);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "BreakDown:";
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
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.SystemColors.Control;
            this.label4.Location = new System.Drawing.Point(3, 193);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(47, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "I_DS [A]";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.SystemColors.Control;
            this.label3.Location = new System.Drawing.Point(3, 143);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(52, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "U_DS [V]";
            // 
            // numericUpDown_U_GS
            // 
            this.numericUpDown_U_GS.Location = new System.Drawing.Point(5, 108);
            this.numericUpDown_U_GS.Name = "numericUpDown_U_GS";
            this.numericUpDown_U_GS.Size = new System.Drawing.Size(93, 20);
            this.numericUpDown_U_GS.TabIndex = 3;
            this.numericUpDown_U_GS.ValueChanged += new System.EventHandler(this.NumericUpDown_U_GS_ValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.SystemColors.Control;
            this.label2.Location = new System.Drawing.Point(3, 93);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(52, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "U_GS [V]";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.SystemColors.Control;
            this.label1.Location = new System.Drawing.Point(3, 43);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(39, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Relais:";
            // 
            // pictureBox
            // 
            this.pictureBox.Image = global::RthTEC_Rack.Properties.Resources.Leer;
            this.pictureBox.Location = new System.Drawing.Point(0, 0);
            this.pictureBox.Name = "pictureBox";
            this.pictureBox.Size = new System.Drawing.Size(116, 397);
            this.pictureBox.TabIndex = 5;
            this.pictureBox.TabStop = false;
            // 
            // comboBox_Relais
            // 
            this.comboBox_Relais.FormattingEnabled = true;
            this.comboBox_Relais.Location = new System.Drawing.Point(5, 58);
            this.comboBox_Relais.Name = "comboBox_Relais";
            this.comboBox_Relais.Size = new System.Drawing.Size(93, 21);
            this.comboBox_Relais.TabIndex = 11;
            this.comboBox_Relais.SelectedIndexChanged += new System.EventHandler(this.ComboBox_Relais_SelectedIndexChanged);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(6, 158);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(20, 20);
            this.button1.TabIndex = 15;
            this.button1.Text = "R";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.Button_Update_UDS_Click);
            // 
            // textBox_U_DS
            // 
            this.textBox_U_DS.Location = new System.Drawing.Point(34, 159);
            this.textBox_U_DS.Name = "textBox_U_DS";
            this.textBox_U_DS.ReadOnly = true;
            this.textBox_U_DS.Size = new System.Drawing.Size(64, 20);
            this.textBox_U_DS.TabIndex = 14;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(6, 207);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(20, 20);
            this.button2.TabIndex = 17;
            this.button2.Text = "R";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.Button_Update_IDS_Click);
            // 
            // textBox_I_DS
            // 
            this.textBox_I_DS.Location = new System.Drawing.Point(34, 208);
            this.textBox_I_DS.Name = "textBox_I_DS";
            this.textBox_I_DS.ReadOnly = true;
            this.textBox_I_DS.Size = new System.Drawing.Size(64, 20);
            this.textBox_I_DS.TabIndex = 16;
            // 
            // Card_MOSFET_BreakDown
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.pictureBox);
            this.Name = "Card_MOSFET_BreakDown";
            this.Size = new System.Drawing.Size(118, 679);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_U_GS)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.GroupBox groupBox1;
        internal System.Windows.Forms.Label label5;
        internal System.Windows.Forms.CheckBox checkBox;
        internal System.Windows.Forms.Label label4;
        internal System.Windows.Forms.Label label3;
        internal System.Windows.Forms.NumericUpDown numericUpDown_U_GS;
        internal System.Windows.Forms.Label label2;
        internal System.Windows.Forms.Label label1;
        internal System.Windows.Forms.PictureBox pictureBox;
        private System.Windows.Forms.ComboBox comboBox_Relais;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox textBox_I_DS;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox textBox_U_DS;
    }
}
