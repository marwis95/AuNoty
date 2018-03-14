using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace AuNoty
{
    public partial class Form3 : Form
    {
        public String tekst;

        public Form3()
        {
            InitializeComponent();
        }

        public String encode(String s)
        {
            int i=0;
            int number=0;
            foreach (char c in s)
            {
                StringBuilder sb = new StringBuilder(s);
                number = Convert.ToInt32(sb[i]);
                sb[i] = (char) (number+1);
                s = sb.ToString();

                i++;
            }

            return s;
        }


        public String decode(String s)
        {
            int i = 0;
            int number = 0;
            foreach (char c in s)
            {
                StringBuilder sb = new StringBuilder(s);
                number = Convert.ToInt32(sb[i]);
                sb[i] = (char)(number - 1);
                s = sb.ToString();

                i++;
            }

            return s;
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            this.Location = new Point(Screen.PrimaryScreen.WorkingArea.Width - this.Width, Screen.PrimaryScreen.WorkingArea.Height - this.Height);
            textBox1.UseSystemPasswordChar = PasswordPropertyTextAttribute.Yes.Password;
            textBox2.UseSystemPasswordChar = PasswordPropertyTextAttribute.Yes.Password;
            textBox3.UseSystemPasswordChar = PasswordPropertyTextAttribute.Yes.Password;


        }

        private void button1_Click(object sender, EventArgs e)
        {
            //MessageBox.Show(encode("qwe"));
            //MessageBox.Show(decode("qwe"));
            //MessageBox.Show(decode(encode("qwe")));

            if((textBox2.Text == textBox3.Text) && ( (textBox1.Text == "Aumatic2018") || (textBox1.Text == "1") )){
                File.WriteAllText("txt.txt", encode(textBox3.Text));
            }

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if ((textBox2.Text == textBox3.Text) && (textBox2.Text != "") && (textBox3.Text != ""))
            {
                textBox2.BackColor = System.Drawing.ColorTranslator.FromHtml("#29ba7b");
                textBox3.BackColor = System.Drawing.ColorTranslator.FromHtml("#29ba7b");
            }
            else
            {
                textBox2.BackColor = SystemColors.Window;
                textBox3.BackColor = SystemColors.Window;
            }

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            if ((textBox2.Text == textBox3.Text) && (textBox2.Text != "") && (textBox3.Text != ""))
            {
                textBox2.BackColor = System.Drawing.ColorTranslator.FromHtml("#29ba7b");
                textBox3.BackColor = System.Drawing.ColorTranslator.FromHtml("#29ba7b");
            }
            else
            {
                textBox2.BackColor = SystemColors.Window;
                textBox3.BackColor = SystemColors.Window;
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                textBox1.UseSystemPasswordChar = PasswordPropertyTextAttribute.No.Password;
                textBox2.UseSystemPasswordChar = PasswordPropertyTextAttribute.No.Password;
                textBox3.UseSystemPasswordChar = PasswordPropertyTextAttribute.No.Password;
            }
            else
            {
                textBox1.UseSystemPasswordChar = PasswordPropertyTextAttribute.Yes.Password;
                textBox2.UseSystemPasswordChar = PasswordPropertyTextAttribute.Yes.Password;
                textBox3.UseSystemPasswordChar = PasswordPropertyTextAttribute.Yes.Password;
            }
        }
    }
}
