namespace ThreasHoldWithThread
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.ThresholdBitmapImage = new System.Windows.Forms.PictureBox();
            this.OrjBitmapImage = new System.Windows.Forms.PictureBox();
            this.label3 = new System.Windows.Forms.Label();
            this.BtnOpenFile = new System.Windows.Forms.Button();
            this.BtnNotThreadOtsu = new System.Windows.Forms.Button();
            this.BtnWithThreadOtsu = new System.Windows.Forms.Button();
            this.LblProgressBarTop = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.lblStopWatch = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.ThresholdBitmapImage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.OrjBitmapImage)).BeginInit();
            this.SuspendLayout();
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // ThresholdBitmapImage
            // 
            this.ThresholdBitmapImage.BackColor = System.Drawing.Color.SlateGray;
            this.ThresholdBitmapImage.Location = new System.Drawing.Point(657, 111);
            this.ThresholdBitmapImage.Margin = new System.Windows.Forms.Padding(4);
            this.ThresholdBitmapImage.Name = "ThresholdBitmapImage";
            this.ThresholdBitmapImage.Size = new System.Drawing.Size(289, 257);
            this.ThresholdBitmapImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.ThresholdBitmapImage.TabIndex = 10;
            this.ThresholdBitmapImage.TabStop = false;
            // 
            // OrjBitmapImage
            // 
            this.OrjBitmapImage.BackColor = System.Drawing.Color.SlateGray;
            this.OrjBitmapImage.Location = new System.Drawing.Point(29, 111);
            this.OrjBitmapImage.Margin = new System.Windows.Forms.Padding(4);
            this.OrjBitmapImage.Name = "OrjBitmapImage";
            this.OrjBitmapImage.Size = new System.Drawing.Size(289, 257);
            this.OrjBitmapImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.OrjBitmapImage.TabIndex = 8;
            this.OrjBitmapImage.TabStop = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Century Gothic", 12F);
            this.label3.ForeColor = System.Drawing.Color.Snow;
            this.label3.Location = new System.Drawing.Point(27, 447);
            this.label3.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(106, 23);
            this.label3.TabIndex = 13;
            this.label3.Text = "Pass Time:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // BtnOpenFile
            // 
            this.BtnOpenFile.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(39)))), ((int)(((byte)(40)))));
            this.BtnOpenFile.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnOpenFile.Font = new System.Drawing.Font("Century Gothic", 11.25F);
            this.BtnOpenFile.ForeColor = System.Drawing.Color.White;
            this.BtnOpenFile.Image = ((System.Drawing.Image)(resources.GetObject("BtnOpenFile.Image")));
            this.BtnOpenFile.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.BtnOpenFile.Location = new System.Drawing.Point(29, 14);
            this.BtnOpenFile.Margin = new System.Windows.Forms.Padding(5);
            this.BtnOpenFile.Name = "BtnOpenFile";
            this.BtnOpenFile.Size = new System.Drawing.Size(157, 69);
            this.BtnOpenFile.TabIndex = 15;
            this.BtnOpenFile.Text = "Open File";
            this.BtnOpenFile.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.BtnOpenFile.UseVisualStyleBackColor = false;
            this.BtnOpenFile.Click += new System.EventHandler(this.BtnOpenFile_Click);
            // 
            // BtnNotThreadOtsu
            // 
            this.BtnNotThreadOtsu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(39)))), ((int)(((byte)(40)))));
            this.BtnNotThreadOtsu.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnNotThreadOtsu.Font = new System.Drawing.Font("Century Gothic", 11.25F);
            this.BtnNotThreadOtsu.ForeColor = System.Drawing.Color.White;
            this.BtnNotThreadOtsu.Image = ((System.Drawing.Image)(resources.GetObject("BtnNotThreadOtsu.Image")));
            this.BtnNotThreadOtsu.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.BtnNotThreadOtsu.Location = new System.Drawing.Point(353, 14);
            this.BtnNotThreadOtsu.Margin = new System.Windows.Forms.Padding(5);
            this.BtnNotThreadOtsu.Name = "BtnNotThreadOtsu";
            this.BtnNotThreadOtsu.Size = new System.Drawing.Size(169, 69);
            this.BtnNotThreadOtsu.TabIndex = 16;
            this.BtnNotThreadOtsu.Text = "Otsu Image";
            this.BtnNotThreadOtsu.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.BtnNotThreadOtsu.UseVisualStyleBackColor = false;
            this.BtnNotThreadOtsu.Click += new System.EventHandler(this.BtnNotThreadOtsu_Click);
            // 
            // BtnWithThreadOtsu
            // 
            this.BtnWithThreadOtsu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(39)))), ((int)(((byte)(40)))));
            this.BtnWithThreadOtsu.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnWithThreadOtsu.Font = new System.Drawing.Font("Century Gothic", 11.25F);
            this.BtnWithThreadOtsu.ForeColor = System.Drawing.Color.White;
            this.BtnWithThreadOtsu.Image = ((System.Drawing.Image)(resources.GetObject("BtnWithThreadOtsu.Image")));
            this.BtnWithThreadOtsu.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.BtnWithThreadOtsu.Location = new System.Drawing.Point(657, 14);
            this.BtnWithThreadOtsu.Margin = new System.Windows.Forms.Padding(5);
            this.BtnWithThreadOtsu.Name = "BtnWithThreadOtsu";
            this.BtnWithThreadOtsu.Size = new System.Drawing.Size(224, 69);
            this.BtnWithThreadOtsu.TabIndex = 17;
            this.BtnWithThreadOtsu.Text = "With Thread Otsu";
            this.BtnWithThreadOtsu.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.BtnWithThreadOtsu.UseVisualStyleBackColor = false;
            this.BtnWithThreadOtsu.Click += new System.EventHandler(this.BtnWithThreadOtsu_Click);
            // 
            // LblProgressBarTop
            // 
            this.LblProgressBarTop.AutoSize = true;
            this.LblProgressBarTop.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.LblProgressBarTop.ForeColor = System.Drawing.Color.Snow;
            this.LblProgressBarTop.Location = new System.Drawing.Point(285, 564);
            this.LblProgressBarTop.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.LblProgressBarTop.Name = "LblProgressBarTop";
            this.LblProgressBarTop.Size = new System.Drawing.Size(412, 20);
            this.LblProgressBarTop.TabIndex = 19;
            this.LblProgressBarTop.Text = "İŞLEM DEVAM EDİYOR LÜTFEN BEKLEYİNİZ...";
            this.LblProgressBarTop.Visible = false;
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(16, 614);
            this.progressBar1.Margin = new System.Windows.Forms.Padding(5);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(917, 50);
            this.progressBar1.TabIndex = 18;
            this.progressBar1.Visible = false;
            // 
            // lblStopWatch
            // 
            this.lblStopWatch.AutoSize = true;
            this.lblStopWatch.Font = new System.Drawing.Font("Century Gothic", 12F);
            this.lblStopWatch.ForeColor = System.Drawing.Color.Snow;
            this.lblStopWatch.Location = new System.Drawing.Point(181, 447);
            this.lblStopWatch.Name = "lblStopWatch";
            this.lblStopWatch.Size = new System.Drawing.Size(124, 23);
            this.lblStopWatch.TabIndex = 20;
            this.lblStopWatch.Text = "00:00:00:000";
            this.lblStopWatch.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(32)))), ((int)(((byte)(39)))));
            this.ClientSize = new System.Drawing.Size(988, 690);
            this.Controls.Add(this.lblStopWatch);
            this.Controls.Add(this.LblProgressBarTop);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.BtnWithThreadOtsu);
            this.Controls.Add(this.BtnNotThreadOtsu);
            this.Controls.Add(this.BtnOpenFile);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.ThresholdBitmapImage);
            this.Controls.Add(this.OrjBitmapImage);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.ThresholdBitmapImage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.OrjBitmapImage)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.PictureBox ThresholdBitmapImage;
        private System.Windows.Forms.PictureBox OrjBitmapImage;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button BtnOpenFile;
        private System.Windows.Forms.Button BtnNotThreadOtsu;
        private System.Windows.Forms.Button BtnWithThreadOtsu;
        private System.Windows.Forms.Label LblProgressBarTop;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Label lblStopWatch;
    }
}

