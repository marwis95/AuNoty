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
using System.Runtime.InteropServices;
using System.Threading;
using System.Collections;



namespace AuNoty
{
    public enum ClickType
    {
        click = 0,
        rightClick = 1,
        doubleClick = 2,
        SendKeys = 3
    }
    public partial class Form1 : Form
    {

        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern void mouse_event(int dwFlags, int dx, int dy, int cButtons, int dwExtraInfo);

        [DllImport("user32")]
        public static extern int SetCursorPos(int x, int y);

        #region Fields
        private const int MOUSEEVENTF_MOVE = 0x0001; /* mouse move */
        private const int MOUSEEVENTF_LEFTDOWN = 0x0002; /* left button down */
        private const int MOUSEEVENTF_LEFTUP = 0x0004; /* left button up */
        private const int MOUSEEVENTF_RIGHTDOWN = 0x0008; /* right button down */
        private const int MOUSEEVENTF_RIGHTUP = 0x0010; /* right button up */
        private const int MOUSEEVENTF_MIDDLEDOWN = 0x0020; /* middle button down */
        private const int MOUSEEVENTF_MIDDLEUP = 0x0040; /* middle button up */
        private const int MOUSEEVENTF_XDOWN = 0x0080; /* x button down */
        private const int MOUSEEVENTF_XUP = 0x0100; /* x button down */
        private const int MOUSEEVENTF_WHEEL = 0x0800; /* wheel button rolled */
        private const int MOUSEEVENTF_VIRTUALDESK = 0x4000; /* map to entire virtual desktop */
        private const int MOUSEEVENTF_ABSOLUTE = 0x8000; /* absolute move */

        private SynchronizationContext context = null;
        private DateTime start, end;
        private bool first = true;
        private List<ActionEntry> actions;
        private Thread runActionThread;
        private bool byTextEntry;
        private Hashtable schedualeList;
        #endregion



        public Form1()
        {
            InitializeComponent();
            context = SynchronizationContext.Current;
            actions = new List<ActionEntry>();
            schedualeList = new Hashtable();
        }

        private TcpListener listener = null;
        private TcpClient klient = null;
        private bool czypolaczono = false;
        private BinaryReader r = null;
        private BinaryWriter w = null;
        public int counter;
        public int rem_pos;
        public Color color = System.Drawing.ColorTranslator.FromHtml("#000000");
        public String strColor;
        public bool checkMessage = false;
        public bool visible = false;


        private void WorkClick(object state)
        {
            this.context.Send(new SendOrPostCallback(delegate(object _state)
            {
                ActionEntry action = state as ActionEntry;
                SetCursorPos(action.X, action.Y);
                Thread.Sleep(100);
                if (action.Type.Equals(ClickType.click))
                {
                    mouse_event(MOUSEEVENTF_LEFTDOWN, 0, 0, 0, 0);
                    mouse_event(MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);
                }
                else if (action.Type.Equals(ClickType.doubleClick))
                {
                    mouse_event(MOUSEEVENTF_LEFTDOWN, 0, 0, 0, 0);
                    mouse_event(MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);
                    Thread.Sleep(100);
                    mouse_event(MOUSEEVENTF_LEFTDOWN, 0, 0, 0, 0);
                    mouse_event(MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);
                }
                else //if (action.Type.Equals(ClickType.rightClick))
                {
                    mouse_event(MOUSEEVENTF_RIGHTDOWN, 0, 0, 0, 0);
                    mouse_event(MOUSEEVENTF_RIGHTUP, 0, 0, 0, 0);
                }
            }), state);
        }

        public string wytnij(String txt, string start, string end)
        {
            try
            {
                return txt.Substring(txt.IndexOf(start) + start.Length, txt.IndexOf(end) - (txt.IndexOf(start) + start.Length));
            }
            catch (ArgumentOutOfRangeException e)
            {
                return "error";
            }
        }

        public bool checkConn(TcpClient k)
        {
            try
            {
                if (klient.Connected == true)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (NullReferenceException)
            {
                return false;
            }
        }


        public void wyswietl(RichTextBox o, string tekst)
        {
            o.Invoke((Action)(() => o.Clear() ));
            o.Invoke((Action)(() => o.Focus() ));
            o.Invoke((Action)(() => o.AppendText(tekst) ));
            o.Invoke((Action)(() => o.ScrollToCaret() ));
            txtWysylanie.Focus();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            

            notifyIcon1.Visible = true;
            notifyIcon1.Text = "AuNoty";
            notifyIcon1.Icon = this.Icon;
            notifyIcon1.ContextMenuStrip = contextMenuStrip1;
            this.ShowInTaskbar = false;
            this.Visible = false;
            
            polaczenie.RunWorkerAsync();
            //MessageBox.Show("czekam na połaczenie");
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            //MessageBox.Show(e.CloseReason.ToString());

            if (e.CloseReason == CloseReason.UserClosing)
            {
                this.DialogResult = DialogResult.OK;
                e.Cancel = true;
                notifyIcon1.Visible = true;
                notifyIcon1.Text = "AuNoty";
                notifyIcon1.Icon = this.Icon;
                notifyIcon1.ContextMenuStrip = contextMenuStrip1;
                this.ShowInTaskbar = false;
                this.Visible = false;



                w.Write("CLOSED");



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
            try
            {
                while (((tekst = r.ReadString()) != KomunikatyKlienta.Rozlacz) && (checkConn(klient) == true))
                {
                    System.Text.Encoding utf_8 = System.Text.Encoding.UTF8;
                    string s_unicode = tekst;
                    byte[] utf8Bytes = System.Text.Encoding.UTF8.GetBytes(s_unicode);
                    string s_unicode2 = System.Text.Encoding.UTF8.GetString(utf8Bytes);

                    tekst=s_unicode2;



                    wyswietl(txtLog, "===== Rozmówca =====\n" + tekst + '\n');

                    //MessageBox.Show(wytnij(tekst, "<type>", "</type>"));
                    //MessageBox.Show(wytnij(tekst, "<caption>", "</caption>"));
                    //MessageBox.Show(wytnij(tekst, "<txt>", "</txt>"));
                    //MessageBox.Show(wytnij(tekst, "<stime>", "</stime>"))


                    if (
                        (wytnij(tekst, "<type>", "</type>") != "error") &&
                        (wytnij(tekst, "<caption>", "</caption>") != "error") &&
                        (wytnij(tekst, "<txt>", "</txt>") != "error") &&
                        (wytnij(tekst, "<stime>", "</stime>") != "error"))
                        w.Write("<msg>ok</msg>");
                    else
                        w.Write("<msg>nok</msg>" + tekst);



                    //color = System.Drawing.ColorTranslator.FromHtml("#29ba7b");



                    mouse_event(MOUSEEVENTF_LEFTDOWN, 0, 0, 0, 0);
                    mouse_event(MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);


                    //strColor = "#29ba7b";

                    //richTextBox1.Invoke((Action)(() => richTextBox1.Clear()));
                    wyswietl(richTextBox1, wytnij(tekst, "<txt>", "</txt>"));


                    if (wytnij(tekst, "<caption>", "</caption>") != "error")
                    {
                        textBox1.Invoke((Action)(() => textBox1.Text = "  " + wytnij(tekst, "<caption>", "</caption>")));
                    }

                    if (wytnij(tekst, "<type>", "</type>") != "error")
                    {
                        if (wytnij(tekst, "<type>", "</type>") == "inf")
                        {
                            pictureBox1.Invoke((Action)(() => pictureBox1.Image = Image.FromFile("info.png")));
                            color = System.Drawing.ColorTranslator.FromHtml("#2e79b4");
                        }

                        if (wytnij(tekst, "<type>", "</type>") == "err")
                        {
                            pictureBox1.Invoke((Action)(() => pictureBox1.Image = Image.FromFile("error.png")));
                            color = System.Drawing.ColorTranslator.FromHtml("#ca2121");
                        }

                        if (wytnij(tekst, "<type>", "</type>") == "war")
                        {
                            pictureBox1.Invoke((Action)(() => pictureBox1.Image = Image.FromFile("warning.png")));
                            color = System.Drawing.ColorTranslator.FromHtml("#ce812e");
                        }

                        if (wytnij(tekst, "<type>", "</type>") == "que")
                        {
                            pictureBox1.Invoke((Action)(() => pictureBox1.Image = Image.FromFile("question.png")));
                            color = System.Drawing.ColorTranslator.FromHtml("#2e79b4");
                        }

                        richTextBox1.Invoke((Action)(() => richTextBox1.BackColor = color));
                        this.Invoke((Action)(() => this.BackColor = color));
                    }

                    //richTextBox1.Invoke((Action)(() =>  pokażProgramToolStripMenuItem.Enabled = true));
                    

                    if (wytnij(tekst, "<stime>", "</stime>") != "0")
                    {
                        int time = Int32.Parse(wytnij(tekst, "<stime>", "</stime>"));
                        //MessageBox.Show(time.ToString());
                        this.Invoke((Action)(() => this.timer2.Stop()));
                        this.Invoke((Action)(() => this.timer2.Interval = time * 1000));
                        this.Invoke((Action)(() => this.timer2.Start()));
                    }

                    this.Invoke((Action)(() => this.BackColor = color));
                    this.Invoke((Action)(() => this.TopMost = true));
                    this.Invoke((Action)(() => this.Visible = true));
                    this.Invoke((Action)(() => notifyIcon1.Visible = false));

                    this.Invoke((Action)(() =>
                    this.Location = new Point(Screen.PrimaryScreen.WorkingArea.Width - this.Width, Screen.PrimaryScreen.WorkingArea.Height)));
                    counter = 0;
                    this.Invoke((Action)(() => timer1.Interval = 2));
                    this.Invoke((Action)(() => timer1.Start()));


                }
            }
            catch (IOException a)
            {

            }
            wyswietl(txtLog, "Rozlaczono\n");
            czypolaczono = false;
            klient.Close();
            listener.Stop();
            polaczenie.RunWorkerAsync();

            notifyIcon1.Visible = true;
            notifyIcon1.Text = "AuNoty";
            notifyIcon1.Icon = this.Icon;
            notifyIcon1.ContextMenuStrip = contextMenuStrip1;
            this.Invoke((Action)(() => this.ShowInTaskbar = false));
            this.Invoke((Action)(() =>  this.Visible = false));
        }

        private void button1_Click(object sender, EventArgs e)
        {
            notifyIcon1.Visible = true;
            notifyIcon1.Text = "AuNoty";
            notifyIcon1.Icon = this.Icon;
            notifyIcon1.ContextMenuStrip = contextMenuStrip1;
            this.ShowInTaskbar = false;
            this.Visible = false;
        }

        private void pokażProgramToolStripMenuItem_Click(object sender, EventArgs e)
        {
            visible = true;
            //this.ShowInTaskbar = true;
            this.TopMost = true;
            //this.Visible = true;
            notifyIcon1.Visible = false;

            this.Location = new Point(Screen.PrimaryScreen.WorkingArea.Width - this.Width, Screen.PrimaryScreen.WorkingArea.Height);

            counter = 0;
            timer1.Interval = 2;
            timer1.Start();
        }

        private void zakończToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form2 f = new Form2("123");
            f.ShowDialog();
            //Application.Exit();
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
            this.Visible = true;
            if (visible == true)
            {
                counter = this.Height -1 ;
            }
            if (this.Location.Y > Screen.PrimaryScreen.WorkingArea.Height - this.Height)
            {
                counter++;
                this.Location = new Point(Screen.PrimaryScreen.WorkingArea.Width - this.Width, Screen.PrimaryScreen.WorkingArea.Height - counter);

            }
            else
            {
                timer1.Stop();
                visible = true;
            }


        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            this.TopMost = true;
            notifyIcon1.Visible = true;
            notifyIcon1.Text = "AuNoty";
            notifyIcon1.Icon = this.Icon;
            notifyIcon1.ContextMenuStrip = contextMenuStrip1;
            this.ShowInTaskbar = false;
            this.Visible = false;
            visible = false;
            timer2.Stop();

            if (checkConn(klient) == true)
            {
                w.Write("<msg>closed</msg>");
            }

        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            
            using (Graphics g = CreateGraphics())
            {
                richTextBox1.Height = (int)g.MeasureString(richTextBox1.Text,
                    richTextBox1.Font, richTextBox1.Width).Height;
                richTextBox1.Height = richTextBox1.Height +  (int)(richTextBox1.Height * 0.2);
            }


            richTextBox1.SelectionStart = 0;
            richTextBox1.SelectAll();
            richTextBox1.SelectionColor = Color.White;
            richTextBox1.DeselectAll();


            this.Height = richTextBox1.Height + 90;
            richTextBox1.Location = new Point(richTextBox1.Location.X, ((int)(0.5 * (this.Height)) - (int)(0.5 * richTextBox1.Height)) + 10);
            pictureBox1.Location = new Point(pictureBox1.Location.X, ((int)(0.5 * (this.Height)) - (int)(0.5 * pictureBox1.Height)) +10 );


        }

        private void richTextBox1_Enter(object sender, EventArgs e)
        {
            label1.Focus();
        }

        private void textBox1_Enter(object sender, EventArgs e)
        {
            label1.Focus();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            notifyIcon1.Visible = true;
            notifyIcon1.Text = "AuNoty";
            notifyIcon1.Icon = this.Icon;
            notifyIcon1.ContextMenuStrip = contextMenuStrip1;
            this.ShowInTaskbar = false;
            this.Visible = false;
            timer2.Stop();

            if (checkConn(klient) == true)
            {
                w.Write("<msg>closed</msg>");
            }
        }

        private void sprawdzToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //MessageBox.Show(checkConn(klient).ToString());
            if (checkConn(klient) == true)
            {
                MessageBox.Show("Connected");
            }
            else
            {
                MessageBox.Show("Disconnected");
            }
        }
         
    }
}
   