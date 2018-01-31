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

        public void wyswietl(RichTextBox o, string tekst)
        {
            o.Focus();
            o.AppendText(tekst);
            o.ScrollToCaret();
            txtWysylanie.Focus();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            polaczenie.RunWorkerAsync();
            MessageBox.Show("czekam na połaczenie");
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
                MessageBox.Show("Połaczono");
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
                MessageBox.Show(tekst);

                w.Write("\rDostałem");

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

    }
}
