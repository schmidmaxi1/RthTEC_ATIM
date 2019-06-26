using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Read_Coordinates
{
    public partial class FileSettings_Box : UserControl
    {
        public FileSettings_Box()
        {
            InitializeComponent();
        }

        public FileSettings_Box(Form callingForm, int x, int y)
        {
            InitializeComponent();

            //In GUI einfügen
            this.Location = new System.Drawing.Point(x, y);
            this.Name = "FillSetting_Box";
            this.Size = new System.Drawing.Size(520, 105);
            this.TabIndex = 32;

            //Hinzufügen
            callingForm.Controls.Add(this);
        }

        //**************************************************************************************************
        //                                    FCT für Setting-Files
        //**************************************************************************************************

        public override string ToString()
        {
            string text = "*Filesettings:" + Environment.NewLine;

            text += "Path: " + readBox_FileFolder1.textBox_Path.Text + Environment.NewLine;
            text += "File: " + readBox_FileFolder1.textBox_FileName.Text + Environment.NewLine;
            text += "Board-Design: " + readBox_Movement1.textBox_Gerber.Text + Environment.NewLine;

            return text;
        }

        public void FromString(string[] input)
        {
            for (int i = 0; i < input.Length; i++)
            {
                if (input[i].StartsWith("Path:"))
                    readBox_FileFolder1.textBox_Path.Text = input[i].Substring(6);
                else if (input[i].StartsWith("File:"))
                    readBox_FileFolder1.textBox_FileName.Text = input[i].Substring(6);
                else if (input[i].StartsWith("Board-Design:"))
                    readBox_Movement1.textBox_Gerber.Text = input[i].Substring(14);
            }
        }

    }
}
