﻿namespace AuNoty
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.polaczenie = new System.ComponentModel.BackgroundWorker();
            this.odbieranie = new System.ComponentModel.BackgroundWorker();
            this.txtLog = new System.Windows.Forms.RichTextBox();
            this.txtWysylanie = new System.Windows.Forms.RichTextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.pokażProgramToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.zakończToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.button2 = new System.Windows.Forms.Button();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // polaczenie
            // 
            this.polaczenie.DoWork += new System.ComponentModel.DoWorkEventHandler(this.polaczenie_DoWork);
            // 
            // odbieranie
            // 
            this.odbieranie.DoWork += new System.ComponentModel.DoWorkEventHandler(this.odbieranie_DoWork);
            // 
            // txtLog
            // 
            this.txtLog.Location = new System.Drawing.Point(65, 217);
            this.txtLog.Name = "txtLog";
            this.txtLog.Size = new System.Drawing.Size(283, 92);
            this.txtLog.TabIndex = 0;
            this.txtLog.Text = "";
            // 
            // txtWysylanie
            // 
            this.txtWysylanie.Location = new System.Drawing.Point(72, 51);
            this.txtWysylanie.Name = "txtWysylanie";
            this.txtWysylanie.Size = new System.Drawing.Size(275, 57);
            this.txtWysylanie.TabIndex = 1;
            this.txtWysylanie.Text = "";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(349, 128);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "Tray";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.Text = "notifyIcon1";
            this.notifyIcon1.Visible = true;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.pokażProgramToolStripMenuItem,
            this.zakończToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(180, 52);
            // 
            // pokażProgramToolStripMenuItem
            // 
            this.pokażProgramToolStripMenuItem.Name = "pokażProgramToolStripMenuItem";
            this.pokażProgramToolStripMenuItem.Size = new System.Drawing.Size(179, 24);
            this.pokażProgramToolStripMenuItem.Text = "Pokaż program";
            this.pokażProgramToolStripMenuItem.Click += new System.EventHandler(this.pokażProgramToolStripMenuItem_Click);
            // 
            // zakończToolStripMenuItem
            // 
            this.zakończToolStripMenuItem.Name = "zakończToolStripMenuItem";
            this.zakończToolStripMenuItem.Size = new System.Drawing.Size(179, 24);
            this.zakończToolStripMenuItem.Text = "Zakończ";
            this.zakończToolStripMenuItem.Click += new System.EventHandler(this.zakończToolStripMenuItem_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(362, 182);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 4;
            this.button2.Text = "Form2";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(459, 342);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.txtWysylanie);
            this.Controls.Add(this.txtLog);
            this.Name = "Form1";
            this.Text = "AuNoty";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.ComponentModel.BackgroundWorker polaczenie;
        private System.ComponentModel.BackgroundWorker odbieranie;
        private System.Windows.Forms.RichTextBox txtLog;
        private System.Windows.Forms.RichTextBox txtWysylanie;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem pokażProgramToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem zakończToolStripMenuItem;
        private System.Windows.Forms.Button button2;
    }
}

