namespace Read_Coordinates
{
    partial class ReadBox_FileFolder
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
            this.pictureBox_Path = new System.Windows.Forms.PictureBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox_Path = new System.Windows.Forms.TextBox();
            this.pictureBox_FineName = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox_FileName = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Path)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_FineName)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox_Path
            // 
            this.pictureBox_Path.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox_Path.Location = new System.Drawing.Point(474, 5);
            this.pictureBox_Path.Name = "pictureBox_Path";
            this.pictureBox_Path.Size = new System.Drawing.Size(21, 21);
            this.pictureBox_Path.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox_Path.TabIndex = 14;
            this.pictureBox_Path.TabStop = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(2, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(33, 13);
            this.label3.TabIndex = 13;
            this.label3.Text = "Path:";
            // 
            // textBox_Path
            // 
            this.textBox_Path.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.textBox_Path.Location = new System.Drawing.Point(75, 5);
            this.textBox_Path.Name = "textBox_Path";
            this.textBox_Path.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.textBox_Path.Size = new System.Drawing.Size(394, 21);
            this.textBox_Path.TabIndex = 12;
            this.textBox_Path.Text = "Double click";
            this.textBox_Path.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.textBox_Path.TextChanged += new System.EventHandler(this.TextBox_Path_TextChanged);
            this.textBox_Path.DoubleClick += new System.EventHandler(this.TextBox_Path_DoubleClick);
            // 
            // pictureBox_FineName
            // 
            this.pictureBox_FineName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox_FineName.Location = new System.Drawing.Point(474, 30);
            this.pictureBox_FineName.Name = "pictureBox_FineName";
            this.pictureBox_FineName.Size = new System.Drawing.Size(21, 21);
            this.pictureBox_FineName.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox_FineName.TabIndex = 17;
            this.pictureBox_FineName.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(2, 34);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 13);
            this.label1.TabIndex = 16;
            this.label1.Text = "File name:";
            // 
            // textBox_FileName
            // 
            this.textBox_FileName.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.textBox_FileName.Location = new System.Drawing.Point(75, 30);
            this.textBox_FileName.Name = "textBox_FileName";
            this.textBox_FileName.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.textBox_FileName.Size = new System.Drawing.Size(394, 21);
            this.textBox_FileName.TabIndex = 15;
            this.textBox_FileName.Text = " (must contain %R, is afterwards replace by ID of DUT)";
            this.textBox_FileName.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.textBox_FileName.TextChanged += new System.EventHandler(this.TextBox_FileName_TextChanged);
            this.textBox_FileName.DoubleClick += new System.EventHandler(this.TextBox_FileName_DoubleClick);
            // 
            // ReadBox_FileFolder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pictureBox_FineName);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox_FileName);
            this.Controls.Add(this.pictureBox_Path);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBox_Path);
            this.Name = "ReadBox_FileFolder";
            this.Size = new System.Drawing.Size(500, 55);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Path)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_FineName)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox_Path;
        private System.Windows.Forms.Label label3;
        public System.Windows.Forms.TextBox textBox_Path;
        private System.Windows.Forms.PictureBox pictureBox_FineName;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.TextBox textBox_FileName;
    }
}
