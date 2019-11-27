namespace Multiplexer
{
    partial class PopUp_3706A_UserCommunication
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
            this.textBox_Write = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox_Read = new System.Windows.Forms.TextBox();
            this.button_Send = new System.Windows.Forms.Button();
            this.checkBox_Read = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // textBox_Write
            // 
            this.textBox_Write.Location = new System.Drawing.Point(12, 29);
            this.textBox_Write.Name = "textBox_Write";
            this.textBox_Write.Size = new System.Drawing.Size(416, 20);
            this.textBox_Write.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Write:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 96);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(36, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Read:";
            // 
            // textBox_Read
            // 
            this.textBox_Read.Location = new System.Drawing.Point(12, 111);
            this.textBox_Read.Name = "textBox_Read";
            this.textBox_Read.ReadOnly = true;
            this.textBox_Read.Size = new System.Drawing.Size(416, 20);
            this.textBox_Read.TabIndex = 2;
            // 
            // button_Send
            // 
            this.button_Send.Location = new System.Drawing.Point(300, 67);
            this.button_Send.Name = "button_Send";
            this.button_Send.Size = new System.Drawing.Size(128, 23);
            this.button_Send.TabIndex = 4;
            this.button_Send.Text = "Send";
            this.button_Send.UseVisualStyleBackColor = true;
            this.button_Send.Click += new System.EventHandler(this.Button_Send_Click);
            // 
            // checkBox_Read
            // 
            this.checkBox_Read.AutoSize = true;
            this.checkBox_Read.Checked = true;
            this.checkBox_Read.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_Read.Location = new System.Drawing.Point(193, 71);
            this.checkBox_Read.Name = "checkBox_Read";
            this.checkBox_Read.Size = new System.Drawing.Size(89, 17);
            this.checkBox_Read.TabIndex = 5;
            this.checkBox_Read.Text = "Read answer";
            this.checkBox_Read.UseVisualStyleBackColor = true;
            // 
            // PopUp_3706A_UserCommunication
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(440, 145);
            this.Controls.Add(this.checkBox_Read);
            this.Controls.Add(this.button_Send);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBox_Read);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox_Write);
            this.Name = "PopUp_3706A_UserCommunication";
            this.Text = "PopUp_3706A_UserCommunication";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox_Write;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox_Read;
        private System.Windows.Forms.Button button_Send;
        private System.Windows.Forms.CheckBox checkBox_Read;
    }
}