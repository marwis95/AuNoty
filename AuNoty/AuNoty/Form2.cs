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
           
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            textBox1.UseSystemPasswordChar = PasswordPropertyTextAttribute.Yes.Password;  
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                textBox1.UseSystemPasswordChar = PasswordPropertyTextAttribute.No.Password;
            }
            else
            {
                textBox1.UseSystemPasswordChar = PasswordPropertyTextAttribute.Yes.Password;
            }
            textBox1.Focus();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text == "Aumatic2018")
            {
                button1.Enabled = true;
            }
        }
    }
}
