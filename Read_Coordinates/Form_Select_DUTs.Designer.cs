namespace Read_Coordinates
{
    partial class Form_Select_DUTs
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
            this.listBox_selected = new System.Windows.Forms.ListBox();
            this.listBox_NOT_selected = new System.Windows.Forms.ListBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.popup_delete_all = new System.Windows.Forms.Button();
            this.popup_add_all = new System.Windows.Forms.Button();
            this.popup_cancel_button = new System.Windows.Forms.Button();
            this.popup_ok_button = new System.Windows.Forms.Button();
            this.popup_delete = new System.Windows.Forms.Button();
            this.Window_Select_LEDs = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // listBox_selected
            // 
            this.listBox_selected.FormattingEnabled = true;
            this.listBox_selected.Location = new System.Drawing.Point(332, 34);
            this.listBox_selected.Name = "listBox_selected";
            this.listBox_selected.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple;
            this.listBox_selected.Size = new System.Drawing.Size(200, 342);
            this.listBox_selected.Sorted = true;
            this.listBox_selected.TabIndex = 37;
            // 
            // listBox_NOT_selected
            // 
            this.listBox_NOT_selected.FormattingEnabled = true;
            this.listBox_NOT_selected.Location = new System.Drawing.Point(12, 34);
            this.listBox_NOT_selected.Name = "listBox_NOT_selected";
            this.listBox_NOT_selected.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple;
            this.listBox_NOT_selected.Size = new System.Drawing.Size(200, 342);
            this.listBox_NOT_selected.Sorted = true;
            this.listBox_NOT_selected.TabIndex = 36;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(332, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(121, 17);
            this.label2.TabIndex = 35;
            this.label2.Text = "Selected DUTs:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(149, 17);
            this.label1.TabIndex = 34;
            this.label1.Text = "Not selected DUTs:";
            // 
            // popup_delete_all
            // 
            this.popup_delete_all.Location = new System.Drawing.Point(232, 304);
            this.popup_delete_all.Margin = new System.Windows.Forms.Padding(2);
            this.popup_delete_all.Name = "popup_delete_all";
            this.popup_delete_all.Size = new System.Drawing.Size(80, 40);
            this.popup_delete_all.TabIndex = 33;
            this.popup_delete_all.Text = "<-- Delete all";
            this.popup_delete_all.UseVisualStyleBackColor = true;
            this.popup_delete_all.Click += new System.EventHandler(this.Popup_delete_all_Click);
            // 
            // popup_add_all
            // 
            this.popup_add_all.Location = new System.Drawing.Point(232, 124);
            this.popup_add_all.Margin = new System.Windows.Forms.Padding(2);
            this.popup_add_all.Name = "popup_add_all";
            this.popup_add_all.Size = new System.Drawing.Size(80, 40);
            this.popup_add_all.TabIndex = 32;
            this.popup_add_all.Text = "Add all -->";
            this.popup_add_all.UseVisualStyleBackColor = true;
            this.popup_add_all.Click += new System.EventHandler(this.Popup_add_all_Click);
            // 
            // popup_cancel_button
            // 
            this.popup_cancel_button.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.popup_cancel_button.Location = new System.Drawing.Point(442, 394);
            this.popup_cancel_button.Margin = new System.Windows.Forms.Padding(2);
            this.popup_cancel_button.Name = "popup_cancel_button";
            this.popup_cancel_button.Size = new System.Drawing.Size(90, 25);
            this.popup_cancel_button.TabIndex = 31;
            this.popup_cancel_button.Text = "Cancel";
            this.popup_cancel_button.UseVisualStyleBackColor = true;
            this.popup_cancel_button.Click += new System.EventHandler(this.Popup_cancel_button_Click);
            // 
            // popup_ok_button
            // 
            this.popup_ok_button.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.popup_ok_button.Location = new System.Drawing.Point(332, 394);
            this.popup_ok_button.Margin = new System.Windows.Forms.Padding(2);
            this.popup_ok_button.Name = "popup_ok_button";
            this.popup_ok_button.Size = new System.Drawing.Size(90, 25);
            this.popup_ok_button.TabIndex = 30;
            this.popup_ok_button.Text = "Ok";
            this.popup_ok_button.UseVisualStyleBackColor = true;
            this.popup_ok_button.Click += new System.EventHandler(this.Popup_ok_button_Click);
            // 
            // popup_delete
            // 
            this.popup_delete.Location = new System.Drawing.Point(232, 244);
            this.popup_delete.Margin = new System.Windows.Forms.Padding(2);
            this.popup_delete.Name = "popup_delete";
            this.popup_delete.Size = new System.Drawing.Size(80, 40);
            this.popup_delete.TabIndex = 29;
            this.popup_delete.Text = "<-- Delete";
            this.popup_delete.UseVisualStyleBackColor = true;
            this.popup_delete.Click += new System.EventHandler(this.Popup_delete_Click);
            // 
            // Window_Select_LEDs
            // 
            this.Window_Select_LEDs.Location = new System.Drawing.Point(232, 64);
            this.Window_Select_LEDs.Margin = new System.Windows.Forms.Padding(2);
            this.Window_Select_LEDs.Name = "Window_Select_LEDs";
            this.Window_Select_LEDs.Size = new System.Drawing.Size(80, 40);
            this.Window_Select_LEDs.TabIndex = 28;
            this.Window_Select_LEDs.Text = "Add -->";
            this.Window_Select_LEDs.UseVisualStyleBackColor = true;
            this.Window_Select_LEDs.Click += new System.EventHandler(this.Popup_add_Click);
            // 
            // Form_Select_DUTs
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(541, 428);
            this.ControlBox = false;
            this.Controls.Add(this.listBox_selected);
            this.Controls.Add(this.listBox_NOT_selected);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.popup_delete_all);
            this.Controls.Add(this.popup_add_all);
            this.Controls.Add(this.popup_cancel_button);
            this.Controls.Add(this.popup_ok_button);
            this.Controls.Add(this.popup_delete);
            this.Controls.Add(this.Window_Select_LEDs);
            this.Name = "Form_Select_DUTs";
            this.Text = "Form_Select_DUTs";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox listBox_selected;
        private System.Windows.Forms.ListBox listBox_NOT_selected;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button popup_delete_all;
        private System.Windows.Forms.Button popup_add_all;
        private System.Windows.Forms.Button popup_cancel_button;
        private System.Windows.Forms.Button popup_ok_button;
        private System.Windows.Forms.Button popup_delete;
        private System.Windows.Forms.Button Window_Select_LEDs;
    }
}