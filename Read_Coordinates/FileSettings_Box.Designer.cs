namespace Read_Coordinates
{
    partial class FileSettings_Box
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
            this.groupBox = new System.Windows.Forms.GroupBox();
            this.readBox_FileFolder1 = new Read_Coordinates.ReadBox_FileFolder();
            this.readBox_Movement1 = new Read_Coordinates.ReadBox_Movement();
            this.groupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox
            // 
            this.groupBox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.groupBox.Controls.Add(this.readBox_FileFolder1);
            this.groupBox.Controls.Add(this.readBox_Movement1);
            this.groupBox.Location = new System.Drawing.Point(0, 0);
            this.groupBox.Name = "groupBox";
            this.groupBox.Size = new System.Drawing.Size(510, 100);
            this.groupBox.TabIndex = 0;
            this.groupBox.TabStop = false;
            this.groupBox.Text = "File Settings:";
            // 
            // readBox_FileFolder1
            // 
            this.readBox_FileFolder1.Location = new System.Drawing.Point(5, 15);
            this.readBox_FileFolder1.Name = "readBox_FileFolder1";
            this.readBox_FileFolder1.Size = new System.Drawing.Size(500, 55);
            this.readBox_FileFolder1.TabIndex = 1;
            // 
            // readBox_Movement1
            // 
            this.readBox_Movement1.Location = new System.Drawing.Point(5, 65);
            this.readBox_Movement1.Name = "readBox_Movement1";
            this.readBox_Movement1.Size = new System.Drawing.Size(500, 30);
            this.readBox_Movement1.TabIndex = 0;
            // 
            // FileSettings_Box
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox);
            this.Name = "FileSettings_Box";
            this.Size = new System.Drawing.Size(520, 110);
            this.groupBox.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox;
        public ReadBox_FileFolder readBox_FileFolder1;
        public ReadBox_Movement readBox_Movement1;
    }
}
