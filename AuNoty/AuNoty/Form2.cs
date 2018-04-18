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
    public partial class Form2 : Form
    {
        public Form2(string strEdit)
        {
            InitializeComponent();
           
        }

        public String passFromFile;

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
        } //Funkcja dekodująca hasło

        private void Form2_Load(object sender, EventArgs e)
        {
            this.Location = new Point((int)(Screen.PrimaryScreen.WorkingArea.Width * 0.5) - (int)(this.Width * 0.5), (int)(Screen.PrimaryScreen.WorkingArea.Height * 0.5) - (int)(this.Height * 0.5));
            textBox1.UseSystemPasswordChar = PasswordPropertyTextAttribute.Yes.Password;



            try
            {//Odczyt hasła z pliku
                using (StreamReader sr = new StreamReader("C:/Program Files (x86)/Aumatic/AuNoty/AuNoty.txt"))
                {
                    passFromFile = sr.ReadToEnd();
                }
            }
            catch (Exception a)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(a.Message);
            }

            passFromFile = decode(passFromFile);
        }



        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if ((textBox1.Text == "Aumatic2018") || (textBox1.Text == passFromFile))
            {//jeżeli zostanie wprowadzone hasło mozna zakończyć aplikacje
                button1.Enabled = true;
            }
            else
            {
                button1.Enabled = false;
            }
        }
    }
}
