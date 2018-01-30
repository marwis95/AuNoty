namespace AuNoty
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
            this.polaczenie = new System.ComponentModel.BackgroundWorker();
            this.odbieranie = new System.ComponentModel.BackgroundWorker();
            this.txtLog = new System.Windows.Forms.RichTextBox();
            this.txtWysylanie = new System.Windows.Forms.RichTextBox();
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
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(459, 342);
            this.Controls.Add(this.txtWysylanie);
            this.Controls.Add(this.txtLog);
            this.Name = "Form1";
            this.Text = "AuNoty";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.ComponentModel.BackgroundWorker polaczenie;
        private System.ComponentModel.BackgroundWorker odbieranie;
        private System.Windows.Forms.RichTextBox txtLog;
        private System.Windows.Forms.RichTextBox txtWysylanie;
    }
}

