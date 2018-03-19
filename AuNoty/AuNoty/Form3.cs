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
        public String oldPass;

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
            try
            {   
                using (StreamReader sr = new StreamReader("txt.txt"))
                {
                    oldPass = sr.ReadToEnd();
                }
            }
            catch (Exception a)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(a.Message);
            }

            oldPass = decode(oldPass);
            

            //this.Location = new Point(Screen.PrimaryScreen.WorkingArea.Width - this.Width, Screen.PrimaryScreen.WorkingArea.Height - this.Height);
            this.Location = new Point((int)(Screen.PrimaryScreen.WorkingArea.Width * 0.5) - (int)(this.Width * 0.5), (int)(Screen.PrimaryScreen.WorkingArea.Height * 0.5) - (int)(this.Height * 0.5));
            textBox1.UseSystemPasswordChar = PasswordPropertyTextAttribute.Yes.Password;
            textBox2.UseSystemPasswordChar = PasswordPropertyTextAttribute.Yes.Password;
            textBox3.UseSystemPasswordChar = PasswordPropertyTextAttribute.Yes.Password;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //MessageBox.Show(encode("qwe"));
            //MessageBox.Show(decode("qwe"));
            //MessageBox.Show(decode(encode("qwe")));

            if ((textBox2.Text == textBox3.Text) && ((textBox1.Text == "Aumatic2018") || (textBox1.Text == oldPass)))
            {
                File.WriteAllText("txt.txt", encode(textBox3.Text));
                MessageBox.Show("Password changed");
            }
            else
            {
                MessageBox.Show("Error");
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



        private void button2_MouseDown(object sender, MouseEventArgs e)
        {
            textBox1.UseSystemPasswordChar = PasswordPropertyTextAttribute.No.Password;
        }

        private void button2_MouseUp(object sender, MouseEventArgs e)
        {
            textBox1.UseSystemPasswordChar = PasswordPropertyTextAttribute.Yes.Password;
        }

        private void button3_MouseDown(object sender, MouseEventArgs e)
        {
            textBox2.UseSystemPasswordChar = PasswordPropertyTextAttribute.No.Password;
        }

        private void button3_MouseUp(object sender, MouseEventArgs e)
        {
            textBox2.UseSystemPasswordChar = PasswordPropertyTextAttribute.Yes.Password;
        }

        private void button4_MouseDown(object sender, MouseEventArgs e)
        {
            textBox3.UseSystemPasswordChar = PasswordPropertyTextAttribute.No.Password;
        }

        private void button4_MouseUp(object sender, MouseEventArgs e)
        {
            textBox3.UseSystemPasswordChar = PasswordPropertyTextAttribute.Yes.Password;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if ((textBox1.Text == "Aumatic2018") || (textBox1.Text == oldPass))
            {
                textBox2.Enabled = true;
                textBox3.Enabled = true;
            }
            else
            {
                textBox2.Enabled = false;
                textBox3.Enabled = false;
            }
        }
    }
}
