using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AuNoty
{
    public partial class Form2 : Form
    {
        public Form2(string strEdit)
        {
            InitializeComponent();
            richTextBox1.AppendText(strEdit);
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            MessageBox.Show(e.CloseReason.ToString());

            if (e.CloseReason == CloseReason.UserClosing)
            {
                this.DialogResult = DialogResult.OK;

            }
        }
    }
}
