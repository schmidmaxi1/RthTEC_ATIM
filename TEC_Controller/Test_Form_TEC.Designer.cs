namespace TEC_Controller
{
    partial class Test_Form_TEC
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.meerstetter_2fach1 = new TEC_Controller.Meerstetter_2fach();
            this.meerstetter_4fach1 = new TEC_Controller.Meerstetter_4fach();
            this.SuspendLayout();
            // 
            // meerstetter_2fach1
            // 
            this.meerstetter_2fach1.Location = new System.Drawing.Point(12, 12);
            this.meerstetter_2fach1.Name = "meerstetter_2fach1";
            this.meerstetter_2fach1.Size = new System.Drawing.Size(516, 73);
            this.meerstetter_2fach1.TabIndex = 0;
            // 
            // meerstetter_4fach1
            // 
            this.meerstetter_4fach1.Location = new System.Drawing.Point(12, 92);
            this.meerstetter_4fach1.Name = "meerstetter_4fach1";
            this.meerstetter_4fach1.Size = new System.Drawing.Size(516, 93);
            this.meerstetter_4fach1.TabIndex = 1;
            // 
            // Test_Form_TEC
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(534, 202);
            this.Controls.Add(this.meerstetter_4fach1);
            this.Controls.Add(this.meerstetter_2fach1);
            this.Name = "Test_Form_TEC";
            this.Text = "Test_Form_TEC";
            this.ResumeLayout(false);

        }

        #endregion

        private Meerstetter_2fach meerstetter_2fach1;
        private Meerstetter_4fach meerstetter_4fach1;
    }
}