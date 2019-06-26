namespace ATIM_GUI
{
    partial class Form_Saving_Options
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
            this.labelControl11 = new DevExpress.XtraEditors.LabelControl();
            this.checkBox_Setting_Save_single_cool = new System.Windows.Forms.CheckBox();
            this.checkBox_Setting_Save_raw = new System.Windows.Forms.CheckBox();
            this.checkBox_Setting_Save_aver_heat = new System.Windows.Forms.CheckBox();
            this.checkBox_Settings_Save_single_heat = new System.Windows.Forms.CheckBox();
            this.checkBox_Setting_Save_aver_cool = new System.Windows.Forms.CheckBox();
            this.button_Apply = new System.Windows.Forms.Button();
            this.button_Cancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // labelControl11
            // 
            this.labelControl11.Appearance.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(1)), true);
            this.labelControl11.Appearance.Options.UseFont = true;
            this.labelControl11.Location = new System.Drawing.Point(12, 12);
            this.labelControl11.Name = "labelControl11";
            this.labelControl11.Size = new System.Drawing.Size(180, 17);
            this.labelControl11.TabIndex = 22;
            this.labelControl11.Text = "Which files should be saved:";
            // 
            // checkBox_Setting_Save_single_cool
            // 
            this.checkBox_Setting_Save_single_cool.AutoSize = true;
            this.checkBox_Setting_Save_single_cool.Location = new System.Drawing.Point(12, 91);
            this.checkBox_Setting_Save_single_cool.Name = "checkBox_Setting_Save_single_cool";
            this.checkBox_Setting_Save_single_cool.Size = new System.Drawing.Size(138, 17);
            this.checkBox_Setting_Save_single_cool.TabIndex = 21;
            this.checkBox_Setting_Save_single_cool.Text = "Singel measurment cool";
            this.checkBox_Setting_Save_single_cool.UseVisualStyleBackColor = true;
            // 
            // checkBox_Setting_Save_raw
            // 
            this.checkBox_Setting_Save_raw.AutoSize = true;
            this.checkBox_Setting_Save_raw.Location = new System.Drawing.Point(12, 141);
            this.checkBox_Setting_Save_raw.Name = "checkBox_Setting_Save_raw";
            this.checkBox_Setting_Save_raw.Size = new System.Drawing.Size(72, 17);
            this.checkBox_Setting_Save_raw.TabIndex = 20;
            this.checkBox_Setting_Save_raw.Text = "Raw data";
            this.checkBox_Setting_Save_raw.UseVisualStyleBackColor = true;
            // 
            // checkBox_Setting_Save_aver_heat
            // 
            this.checkBox_Setting_Save_aver_heat.AutoSize = true;
            this.checkBox_Setting_Save_aver_heat.Location = new System.Drawing.Point(12, 66);
            this.checkBox_Setting_Save_aver_heat.Name = "checkBox_Setting_Save_aver_heat";
            this.checkBox_Setting_Save_aver_heat.Size = new System.Drawing.Size(90, 17);
            this.checkBox_Setting_Save_aver_heat.TabIndex = 19;
            this.checkBox_Setting_Save_aver_heat.Text = "Average heat";
            this.checkBox_Setting_Save_aver_heat.UseVisualStyleBackColor = true;
            // 
            // checkBox_Settings_Save_single_heat
            // 
            this.checkBox_Settings_Save_single_heat.AutoSize = true;
            this.checkBox_Settings_Save_single_heat.Location = new System.Drawing.Point(12, 116);
            this.checkBox_Settings_Save_single_heat.Name = "checkBox_Settings_Save_single_heat";
            this.checkBox_Settings_Save_single_heat.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.checkBox_Settings_Save_single_heat.Size = new System.Drawing.Size(145, 17);
            this.checkBox_Settings_Save_single_heat.TabIndex = 18;
            this.checkBox_Settings_Save_single_heat.Text = "Single measurement heat";
            this.checkBox_Settings_Save_single_heat.UseVisualStyleBackColor = true;
            // 
            // checkBox_Setting_Save_aver_cool
            // 
            this.checkBox_Setting_Save_aver_cool.AutoSize = true;
            this.checkBox_Setting_Save_aver_cool.Checked = true;
            this.checkBox_Setting_Save_aver_cool.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_Setting_Save_aver_cool.Location = new System.Drawing.Point(12, 41);
            this.checkBox_Setting_Save_aver_cool.Name = "checkBox_Setting_Save_aver_cool";
            this.checkBox_Setting_Save_aver_cool.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.checkBox_Setting_Save_aver_cool.Size = new System.Drawing.Size(89, 17);
            this.checkBox_Setting_Save_aver_cool.TabIndex = 17;
            this.checkBox_Setting_Save_aver_cool.Text = "Average cool";
            this.checkBox_Setting_Save_aver_cool.UseVisualStyleBackColor = true;
            // 
            // button_Apply
            // 
            this.button_Apply.Location = new System.Drawing.Point(112, 177);
            this.button_Apply.Name = "button_Apply";
            this.button_Apply.Size = new System.Drawing.Size(80, 23);
            this.button_Apply.TabIndex = 23;
            this.button_Apply.Text = "Apply";
            this.button_Apply.UseVisualStyleBackColor = true;
            this.button_Apply.Click += new System.EventHandler(this.Button_Apply_Click);
            // 
            // button_Cancel
            // 
            this.button_Cancel.Location = new System.Drawing.Point(12, 177);
            this.button_Cancel.Name = "button_Cancel";
            this.button_Cancel.Size = new System.Drawing.Size(80, 23);
            this.button_Cancel.TabIndex = 24;
            this.button_Cancel.Text = "Cancel";
            this.button_Cancel.UseVisualStyleBackColor = true;
            this.button_Cancel.Click += new System.EventHandler(this.Button_Cancel_Click);
            // 
            // Form_Saving_Options
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(202, 212);
            this.ControlBox = false;
            this.Controls.Add(this.button_Cancel);
            this.Controls.Add(this.button_Apply);
            this.Controls.Add(this.labelControl11);
            this.Controls.Add(this.checkBox_Setting_Save_single_cool);
            this.Controls.Add(this.checkBox_Setting_Save_raw);
            this.Controls.Add(this.checkBox_Setting_Save_aver_heat);
            this.Controls.Add(this.checkBox_Settings_Save_single_heat);
            this.Controls.Add(this.checkBox_Setting_Save_aver_cool);
            this.Name = "Form_Saving_Options";
            this.Text = "Saving options";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl labelControl11;
        private System.Windows.Forms.CheckBox checkBox_Setting_Save_single_cool;
        private System.Windows.Forms.CheckBox checkBox_Setting_Save_raw;
        private System.Windows.Forms.CheckBox checkBox_Setting_Save_aver_heat;
        private System.Windows.Forms.CheckBox checkBox_Settings_Save_single_heat;
        private System.Windows.Forms.CheckBox checkBox_Setting_Save_aver_cool;
        private System.Windows.Forms.Button button_Apply;
        private System.Windows.Forms.Button button_Cancel;
    }
}