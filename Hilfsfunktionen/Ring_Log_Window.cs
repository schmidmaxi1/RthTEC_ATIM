using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hilfsfunktionen
{
    public partial class Ring_Log_Window : Form
    {
        public Ring_Log_Window(string text)
        {
            InitializeComponent();

            //Text einfügen mit automatischen scrollen zum Ende
            //textBox1.AppendText(text);
            textBox1.Text = text;

            textBox1.SelectionStart = textBox1.Text.Length;
            textBox1.ScrollToCaret();
            textBox1.Refresh();
        }
    }
}
