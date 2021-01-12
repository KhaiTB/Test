namespace AutoDownloadImageAmz
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
            this.textBoxLink = new System.Windows.Forms.TextBox();
            this.textBoxDownloadFoler = new System.Windows.Forms.TextBox();
            this.labelStatus = new System.Windows.Forms.Label();
            this.buttonStart = new System.Windows.Forms.Button();
            this.numericUpDownMaxImage = new System.Windows.Forms.NumericUpDown();
            this.buttonStop = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.numericUpDownThread = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMaxImage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownThread)).BeginInit();
            this.SuspendLayout();
            // 
            // textBoxLink
            // 
            this.textBoxLink.Location = new System.Drawing.Point(94, 38);
            this.textBoxLink.Name = "textBoxLink";
            this.textBoxLink.Size = new System.Drawing.Size(171, 20);
            this.textBoxLink.TabIndex = 0;
            this.textBoxLink.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // textBoxDownloadFoler
            // 
            this.textBoxDownloadFoler.Location = new System.Drawing.Point(354, 38);
            this.textBoxDownloadFoler.Name = "textBoxDownloadFoler";
            this.textBoxDownloadFoler.Size = new System.Drawing.Size(157, 20);
            this.textBoxDownloadFoler.TabIndex = 1;
            this.textBoxDownloadFoler.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.textBoxDownloadFoler.TextChanged += new System.EventHandler(this.textBoxDownloadFoler_TextChanged);
            // 
            // labelStatus
            // 
            this.labelStatus.AutoSize = true;
            this.labelStatus.Location = new System.Drawing.Point(35, 178);
            this.labelStatus.Name = "labelStatus";
            this.labelStatus.Size = new System.Drawing.Size(49, 13);
            this.labelStatus.TabIndex = 2;
            this.labelStatus.Text = "Status: --";
            // 
            // buttonStart
            // 
            this.buttonStart.Location = new System.Drawing.Point(254, 139);
            this.buttonStart.Name = "buttonStart";
            this.buttonStart.Size = new System.Drawing.Size(116, 37);
            this.buttonStart.TabIndex = 3;
            this.buttonStart.Text = "START";
            this.buttonStart.UseVisualStyleBackColor = true;
            this.buttonStart.Click += new System.EventHandler(this.buttonStart_Click);
            // 
            // numericUpDownMaxImage
            // 
            this.numericUpDownMaxImage.Location = new System.Drawing.Point(94, 85);
            this.numericUpDownMaxImage.Maximum = new decimal(new int[] {
            100000000,
            0,
            0,
            0});
            this.numericUpDownMaxImage.Name = "numericUpDownMaxImage";
            this.numericUpDownMaxImage.Size = new System.Drawing.Size(171, 20);
            this.numericUpDownMaxImage.TabIndex = 4;
            this.numericUpDownMaxImage.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numericUpDownMaxImage.Value = new decimal(new int[] {
            200,
            0,
            0,
            0});
            // 
            // buttonStop
            // 
            this.buttonStop.Location = new System.Drawing.Point(395, 139);
            this.buttonStop.Name = "buttonStop";
            this.buttonStop.Size = new System.Drawing.Size(116, 37);
            this.buttonStop.TabIndex = 5;
            this.buttonStop.Text = "STOP";
            this.buttonStop.UseVisualStyleBackColor = true;
            this.buttonStop.Click += new System.EventHandler(this.buttonStop_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(33, 88);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Max Image";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(271, 42);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(81, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Download Foler";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(16, 41);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(76, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Link From Amz";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(142, 226);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(371, 15);
            this.label1.TabIndex = 9;
            this.label1.Text = "Author: Pham Sy Truong - 096.290.2397 - https://fb.com/truongps9x";
            // 
            // numericUpDownThread
            // 
            this.numericUpDownThread.Location = new System.Drawing.Point(354, 85);
            this.numericUpDownThread.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numericUpDownThread.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownThread.Name = "numericUpDownThread";
            this.numericUpDownThread.Size = new System.Drawing.Size(157, 20);
            this.numericUpDownThread.TabIndex = 10;
            this.numericUpDownThread.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numericUpDownThread.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(311, 88);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 13);
            this.label5.TabIndex = 11;
            this.label5.Text = "Thread";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(525, 262);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.numericUpDownThread);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.buttonStop);
            this.Controls.Add(this.numericUpDownMaxImage);
            this.Controls.Add(this.buttonStart);
            this.Controls.Add(this.labelStatus);
            this.Controls.Add(this.textBoxDownloadFoler);
            this.Controls.Add(this.textBoxLink);
            this.Name = "Form1";
            this.Text = "Auto Download Image From Amazon - v1.0 - PST";
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMaxImage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownThread)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxLink;
        private System.Windows.Forms.TextBox textBoxDownloadFoler;
        private System.Windows.Forms.Label labelStatus;
        private System.Windows.Forms.Button buttonStart;
        private System.Windows.Forms.NumericUpDown numericUpDownMaxImage;
        private System.Windows.Forms.Button buttonStop;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown numericUpDownThread;
        private System.Windows.Forms.Label label5;
    }
}

