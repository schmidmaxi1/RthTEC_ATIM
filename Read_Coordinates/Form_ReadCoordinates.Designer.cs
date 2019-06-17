namespace Read_Coordinates
{
    partial class Form_ReadCoordinates
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
            this.fileSettings_Box1 = new Read_Coordinates.FileSettings_Box();
            this.SuspendLayout();
            // 
            // fileSettings_Box1
            // 
            this.fileSettings_Box1.Location = new System.Drawing.Point(2, 3);
            this.fileSettings_Box1.Name = "fileSettings_Box1";
            this.fileSettings_Box1.Size = new System.Drawing.Size(520, 110);
            this.fileSettings_Box1.TabIndex = 0;
            // 
            // Form_ReadCoordinates
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(522, 111);
            this.Controls.Add(this.fileSettings_Box1);
            this.Name = "Form_ReadCoordinates";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private FileSettings_Box fileSettings_Box1;
    }
}

