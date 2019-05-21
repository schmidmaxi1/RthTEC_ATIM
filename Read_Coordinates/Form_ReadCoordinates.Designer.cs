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
            this.read_Movement1 = new Read_Coordinates.ReadBox_Movement();
            this.SuspendLayout();
            // 
            // read_Movement1
            // 
            this.read_Movement1.Location = new System.Drawing.Point(0, 0);
            this.read_Movement1.Name = "read_Movement1";
            this.read_Movement1.Size = new System.Drawing.Size(510, 40);
            this.read_Movement1.TabIndex = 3;
            // 
            // Form_ReadCoordinates
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(514, 41);
            this.Controls.Add(this.read_Movement1);
            this.Name = "Form_ReadCoordinates";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion
        private ReadBox_Movement read_Movement1;
    }
}

