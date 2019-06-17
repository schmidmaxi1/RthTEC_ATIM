namespace XYZ_Table
{
    partial class Test_Form_XYZ
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
            this.iseL_4Axis1 = new XYZ_Table.ISEL_4Axis();
            this.iseL_3Axis1 = new XYZ_Table.ISEL_3Axis();
            this.SuspendLayout();
            // 
            // iseL_4Axis1
            // 
            this.iseL_4Axis1.A_ges_schnell = new decimal(new int[] {
            8000,
            0,
            0,
            0});
            this.iseL_4Axis1.Grenze_A_neg = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.iseL_4Axis1.Grenze_A_pos = new decimal(new int[] {
            360,
            0,
            0,
            0});
            this.iseL_4Axis1.Grenze_X_neg = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.iseL_4Axis1.Grenze_X_pos = new decimal(new int[] {
            260,
            0,
            0,
            0});
            this.iseL_4Axis1.Grenze_Y_pos = new decimal(new int[] {
            259,
            0,
            0,
            0});
            this.iseL_4Axis1.Grenze_Z_neg = new decimal(new int[] {
            100,
            0,
            0,
            -2147483648});
            this.iseL_4Axis1.Grenze_Z_pos = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.iseL_4Axis1.Location = new System.Drawing.Point(13, 13);
            this.iseL_4Axis1.Name = "iseL_4Axis1";
            this.iseL_4Axis1.Size = new System.Drawing.Size(520, 74);
            this.iseL_4Axis1.TabIndex = 0;
            this.iseL_4Axis1.X_ges_schnell = new decimal(new int[] {
            8000,
            0,
            0,
            0});
            this.iseL_4Axis1.Y_ges_schnell = new decimal(new int[] {
            8000,
            0,
            0,
            0});
            this.iseL_4Axis1.Z_ges_schnell = new decimal(new int[] {
            8000,
            0,
            0,
            0});
            // 
            // iseL_3Axis1
            // 
            this.iseL_3Axis1.A_ges_schnell = new decimal(new int[] {
            8000,
            0,
            0,
            0});
            this.iseL_3Axis1.Grenze_A_neg = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.iseL_3Axis1.Grenze_A_pos = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.iseL_3Axis1.Grenze_X_neg = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.iseL_3Axis1.Grenze_X_pos = new decimal(new int[] {
            530,
            0,
            0,
            0});
            this.iseL_3Axis1.Grenze_Y_pos = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.iseL_3Axis1.Grenze_Z_neg = new decimal(new int[] {
            40,
            0,
            0,
            -2147483648});
            this.iseL_3Axis1.Grenze_Z_pos = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.iseL_3Axis1.Location = new System.Drawing.Point(13, 94);
            this.iseL_3Axis1.Name = "iseL_3Axis1";
            this.iseL_3Axis1.Size = new System.Drawing.Size(528, 85);
            this.iseL_3Axis1.TabIndex = 1;
            this.iseL_3Axis1.X_ges_schnell = new decimal(new int[] {
            8000,
            0,
            0,
            0});
            this.iseL_3Axis1.Y_ges_schnell = new decimal(new int[] {
            8000,
            0,
            0,
            0});
            this.iseL_3Axis1.Z_ges_schnell = new decimal(new int[] {
            8000,
            0,
            0,
            0});
            // 
            // Test_Form_XYZ
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(545, 178);
            this.Controls.Add(this.iseL_3Axis1);
            this.Controls.Add(this.iseL_4Axis1);
            this.Name = "Test_Form_XYZ";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private ISEL_4Axis iseL_4Axis1;
        private ISEL_3Axis iseL_3Axis1;
    }
}

