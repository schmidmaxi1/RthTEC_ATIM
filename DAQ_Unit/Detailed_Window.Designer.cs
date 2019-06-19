namespace DAQ_Unit
{
    partial class Detailed_Window
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
            this.comboBox_Range = new System.Windows.Forms.ComboBox();
            this.comboBox_Freq = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // comboBox_Range
            // 
            this.comboBox_Range.FormattingEnabled = true;
            this.comboBox_Range.Location = new System.Drawing.Point(93, 14);
            this.comboBox_Range.Name = "comboBox_Range";
            this.comboBox_Range.Size = new System.Drawing.Size(121, 21);
            this.comboBox_Range.TabIndex = 0;
            this.comboBox_Range.SelectedIndexChanged += new System.EventHandler(this.ComboBox_Range_SelectedIndexChanged);
            // 
            // comboBox_Freq
            // 
            this.comboBox_Freq.FormattingEnabled = true;
            this.comboBox_Freq.Location = new System.Drawing.Point(93, 55);
            this.comboBox_Freq.Name = "comboBox_Freq";
            this.comboBox_Freq.Size = new System.Drawing.Size(121, 21);
            this.comboBox_Freq.TabIndex = 1;
            this.comboBox_Freq.SelectedIndexChanged += new System.EventHandler(this.ComboBox_Freq_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(42, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Range:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 58);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Frequency:";
            // 
            // Detailed_Window
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(225, 92);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.comboBox_Freq);
            this.Controls.Add(this.comboBox_Range);
            this.Name = "Detailed_Window";
            this.Text = "Detailed_Window";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBox_Range;
        private System.Windows.Forms.ComboBox comboBox_Freq;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}