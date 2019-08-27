namespace Read_Coordinates
{
    partial class ReadBox_Movement
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
            this.pictureBox_GUI = new System.Windows.Forms.PictureBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox_Gerber = new System.Windows.Forms.TextBox();
            this.button_Select = new System.Windows.Forms.Button();
            this.textBox_Seleted = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_GUI)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox_GUI
            // 
            this.pictureBox_GUI.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox_GUI.Location = new System.Drawing.Point(474, 5);
            this.pictureBox_GUI.Name = "pictureBox_GUI";
            this.pictureBox_GUI.Size = new System.Drawing.Size(21, 21);
            this.pictureBox_GUI.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox_GUI.TabIndex = 11;
            this.pictureBox_GUI.TabStop = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(2, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(73, 13);
            this.label3.TabIndex = 10;
            this.label3.Text = "Board design:";
            // 
            // textBox_Gerber
            // 
            this.textBox_Gerber.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.textBox_Gerber.Location = new System.Drawing.Point(75, 5);
            this.textBox_Gerber.Name = "textBox_Gerber";
            this.textBox_Gerber.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.textBox_Gerber.Size = new System.Drawing.Size(394, 21);
            this.textBox_Gerber.TabIndex = 9;
            this.textBox_Gerber.Text = "Double click";
            this.textBox_Gerber.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.textBox_Gerber.TextChanged += new System.EventHandler(this.TextBox_Gerber_TextChanged);
            this.textBox_Gerber.DoubleClick += new System.EventHandler(this.TextBox_Gerber_DoubleClick);
            // 
            // button_Select
            // 
            this.button_Select.Location = new System.Drawing.Point(394, 32);
            this.button_Select.Name = "button_Select";
            this.button_Select.Size = new System.Drawing.Size(75, 21);
            this.button_Select.TabIndex = 12;
            this.button_Select.Text = "Select";
            this.button_Select.UseVisualStyleBackColor = true;
            this.button_Select.Click += new System.EventHandler(this.Button_Select_Click);
            // 
            // textBox_Seleted
            // 
            this.textBox_Seleted.Location = new System.Drawing.Point(331, 32);
            this.textBox_Seleted.Name = "textBox_Seleted";
            this.textBox_Seleted.ReadOnly = true;
            this.textBox_Seleted.Size = new System.Drawing.Size(57, 20);
            this.textBox_Seleted.TabIndex = 13;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(244, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(81, 13);
            this.label1.TabIndex = 14;
            this.label1.Text = "Devices to test:";
            // 
            // ReadBox_Movement
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox_Seleted);
            this.Controls.Add(this.button_Select);
            this.Controls.Add(this.pictureBox_GUI);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBox_Gerber);
            this.Name = "ReadBox_Movement";
            this.Size = new System.Drawing.Size(500, 60);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_GUI)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox_GUI;
        private System.Windows.Forms.Label label3;
        public System.Windows.Forms.TextBox textBox_Gerber;
        private System.Windows.Forms.Button button_Select;
        private System.Windows.Forms.TextBox textBox_Seleted;
        private System.Windows.Forms.Label label1;
    }
}
