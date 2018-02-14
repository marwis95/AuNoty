using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.IO;
using komunikaty;

namespace AuNoty
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Form1.CheckForIllegalCrossThreadCalls = false;
        }

        private TcpListener listener = null;
        private TcpClient klient = null;
        private bool czypolaczono = false;
        private BinaryReader r = null;
        private BinaryWriter w = null;
        public int counter;
        public int rem_pos;


        public string wytnij(String txt, string start, string end)
        {
            return txt.Substring(txt.IndexOf(start) + start.Length, txt.IndexOf(end) - (txt.IndexOf(start) + start.Length));
        }

        public void wyswietl(RichTextBox o, string tekst)
        {
            o.Focus();
            o.AppendText(tekst);
            o.ScrollToCaret();
            txtWysylanie.Focus();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            notifyIcon1.Visible = true;
            notifyIcon1.Text = "Minimize";
            notifyIcon1.Icon = this.Icon;
            notifyIcon1.ContextMenuStrip = contextMenuStrip1;

            this.ShowInTaskbar = false;
            this.Visible = false;
                 
            polaczenie.RunWorkerAsync();
            pictureBox1.Image = Image.FromFile("alert.gif");
            MessageBox.Show("czekam na połaczenie");
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            MessageBox.Show(e.CloseReason.ToString());

            if (e.CloseReason == CloseReason.UserClosing)
            {
                this.DialogResult = DialogResult.OK;
                e.Cancel = true;
                notifyIcon1.Visible = true;
                notifyIcon1.Text = "Minimize";
                notifyIcon1.Icon = this.Icon;
                notifyIcon1.ContextMenuStrip = contextMenuStrip1;
                this.ShowInTaskbar = false;
                this.Visible = false;



                w.Write("CLOSED");
                //counter = this.Width;
                //MessageBox.Show(this.Location.Y.ToString());
                //timer2.Interval = 5;
                //timer2.Start();



            }
        }

        private void polaczenie_DoWork(object sender, DoWorkEventArgs e)
        {
            //txtLog.SelectionFont = new Font(txtLog.Font, FontStyle.Bold);
            wyswietl(txtLog, "Czekam na połaczenie\n");
            listener = new TcpListener(8000);
            listener.Start();
            while (!listener.Pending())
            {
                if (this.polaczenie.CancellationPending)
                {
                    if (klient != null) klient.Close();
                    listener.Stop();
                    czypolaczono = false;
                    return;
                }
            }
            klient = listener.AcceptTcpClient();
            wyswietl(txtLog, "Zarządano połączenia\n");
            NetworkStream stream = klient.GetStream();
            w = new BinaryWriter(stream);
            r = new BinaryReader(stream);
            if (r.ReadString() == KomunikatyKlienta.Zadaj)
            {
                w.Write(KomunikatySerwera.OK);
                wyswietl(txtLog, "Połczono\n");
                //MessageBox.Show("Połaczono");
                czypolaczono = true;
                odbieranie.RunWorkerAsync();
            }
            else
            {
                wyswietl(txtLog, "Klient odrzucony\nRozlaczono\n");
                if (klient != null) klient.Close();
                listener.Stop();
                czypolaczono = false;
                polaczenie.CancelAsync();
                polaczenie.RunWorkerAsync();
            }
        }

        private void odbieranie_DoWork(object sender, DoWorkEventArgs e)
        {
            string tekst;
            while ((tekst = r.ReadString()) != KomunikatyKlienta.Rozlacz)
            {
                wyswietl(txtLog, "===== Rozmówca =====\n" + tekst + '\n');

                //MessageBox.Show(wytnij(tekst, "<type>", "</type>"));
                //MessageBox.Show(wytnij(tekst, "<caption>", "</caption>"));
                //MessageBox.Show(wytnij(tekst, "<txt>", "</txt>"));
                //MessageBox.Show(wytnij(tekst, "<stime>", "</stime>"));
                


                w.Write("\rDostałem");

                this.Invoke((Action)(() => this.ShowInTaskbar = true));
                this.Invoke((Action)(() => this.Visible = true));
                this.Invoke((Action)(() => notifyIcon1.Visible = false));

                //this.ShowInTaskbar = true;
                //this.Visible = true;
                //notifyIcon1.Visible = false;
                //    MessageBox.Show(tekst);
                

            }
            wyswietl(txtLog, "Rozlaczono\n");
            czypolaczono = false;
            klient.Close();
            listener.Stop();
            polaczenie.RunWorkerAsync();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            notifyIcon1.Visible = true;
            notifyIcon1.Text = "Minimize";
            notifyIcon1.Icon = this.Icon;
            notifyIcon1.ContextMenuStrip = contextMenuStrip1;
            this.ShowInTaskbar = false;
            this.Visible = false;
        }

        private void pokażProgramToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.ShowInTaskbar = true;
            this.Visible = true;
            notifyIcon1.Visible = false;

            this.Location = new Point(Screen.PrimaryScreen.WorkingArea.Width - this.Width, Screen.PrimaryScreen.WorkingArea.Height);
            //MessageBox.Show(this.Location.Y.ToString());
            counter = 0;
            timer1.Interval = 2;
            timer1.Start();
        }

        private void zakończToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form2 f = new Form2("123");
            if (f.ShowDialog() == DialogResult.OK) {
                MessageBox.Show("ok");
            
            }
            ;
            
            
        }

        private void timer1_Tick(object sender, EventArgs e)
        {

            if (this.Location.Y > Screen.PrimaryScreen.WorkingArea.Height - this.Height)
            {
                counter++;
                this.Location = new Point(Screen.PrimaryScreen.WorkingArea.Width - this.Width, Screen.PrimaryScreen.WorkingArea.Height - counter);

            }
            else
            {
                timer1.Stop();
            }


        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            if (this.Location.Y > 0)
            {
                counter--;
                this.Location = new Point(Screen.PrimaryScreen.WorkingArea.Width - this.Width, Screen.PrimaryScreen.WorkingArea.Height - counter);

            }

        }

    }
}
