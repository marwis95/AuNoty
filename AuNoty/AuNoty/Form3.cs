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
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show(encode("qwe"));
            MessageBox.Show(decode("qwe"));
            MessageBox.Show(decode(encode("qwe")));
        }
    }
}
