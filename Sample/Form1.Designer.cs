namespace Sample
{
    partial class MainFrm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainFrm));
            this.lblPrompt = new System.Windows.Forms.Label();
            this.txtPrompt = new System.Windows.Forms.TextBox();
            this.txtStatus = new System.Windows.Forms.TextBox();
            this.lblStatus = new System.Windows.Forms.Label();
            this.txtQuality = new System.Windows.Forms.TextBox();
            this.lblQuality = new System.Windows.Forms.Label();
            this.btnConnect = new System.Windows.Forms.Button();
            this.btnRegister = new System.Windows.Forms.Button();
            this.btnVerify = new System.Windows.Forms.Button();
            this.picFP = new System.Windows.Forms.PictureBox();
            this.lbUserNum = new System.Windows.Forms.Label();
            this.lbName = new System.Windows.Forms.Label();
            this.tbUserID = new System.Windows.Forms.TextBox();
            this.tbName = new System.Windows.Forms.TextBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.tssLbl1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.tssLbl2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.tssLbl3 = new System.Windows.Forms.ToolStripStatusLabel();
            ((System.ComponentModel.ISupportInitialize)(this.picFP)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblPrompt
            // 
            this.lblPrompt.AutoSize = true;
            this.lblPrompt.Location = new System.Drawing.Point(435, 18);
            this.lblPrompt.Name = "lblPrompt";
            this.lblPrompt.Size = new System.Drawing.Size(47, 12);
            this.lblPrompt.TabIndex = 0;
            this.lblPrompt.Text = "Prompt:";
            // 
            // txtPrompt
            // 
            this.txtPrompt.Location = new System.Drawing.Point(438, 41);
            this.txtPrompt.Name = "txtPrompt";
            this.txtPrompt.Size = new System.Drawing.Size(220, 21);
            this.txtPrompt.TabIndex = 1;
            // 
            // txtStatus
            // 
            this.txtStatus.Location = new System.Drawing.Point(438, 93);
            this.txtStatus.Name = "txtStatus";
            this.txtStatus.Size = new System.Drawing.Size(220, 21);
            this.txtStatus.TabIndex = 3;
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(435, 70);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(47, 12);
            this.lblStatus.TabIndex = 2;
            this.lblStatus.Text = "Status:";
            // 
            // txtQuality
            // 
            this.txtQuality.Location = new System.Drawing.Point(438, 150);
            this.txtQuality.Name = "txtQuality";
            this.txtQuality.Size = new System.Drawing.Size(220, 21);
            this.txtQuality.TabIndex = 5;
            // 
            // lblQuality
            // 
            this.lblQuality.AutoSize = true;
            this.lblQuality.Location = new System.Drawing.Point(435, 127);
            this.lblQuality.Name = "lblQuality";
            this.lblQuality.Size = new System.Drawing.Size(89, 12);
            this.lblQuality.TabIndex = 4;
            this.lblQuality.Text = "Image Quality:";
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(438, 189);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(127, 21);
            this.btnConnect.TabIndex = 6;
            this.btnConnect.Text = "Connect Sensor";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // btnRegister
            // 
            this.btnRegister.Enabled = false;
            this.btnRegister.Location = new System.Drawing.Point(438, 232);
            this.btnRegister.Name = "btnRegister";
            this.btnRegister.Size = new System.Drawing.Size(127, 21);
            this.btnRegister.TabIndex = 7;
            this.btnRegister.Text = "Register";
            this.btnRegister.UseVisualStyleBackColor = true;
            this.btnRegister.Click += new System.EventHandler(this.btnRegister_Click);
            // 
            // btnVerify
            // 
            this.btnVerify.Enabled = false;
            this.btnVerify.Location = new System.Drawing.Point(438, 272);
            this.btnVerify.Name = "btnVerify";
            this.btnVerify.Size = new System.Drawing.Size(127, 21);
            this.btnVerify.TabIndex = 8;
            this.btnVerify.Text = "Verify(1:N)";
            this.btnVerify.UseVisualStyleBackColor = true;
            this.btnVerify.Click += new System.EventHandler(this.btnVerify_Click);
            // 
            // picFP
            // 
            this.picFP.Location = new System.Drawing.Point(3, 3);
            this.picFP.Name = "picFP";
            this.picFP.Size = new System.Drawing.Size(338, 332);
            this.picFP.TabIndex = 9;
            this.picFP.TabStop = false;
            // 
            // lbUserNum
            // 
            this.lbUserNum.AutoSize = true;
            this.lbUserNum.Location = new System.Drawing.Point(693, 18);
            this.lbUserNum.Name = "lbUserNum";
            this.lbUserNum.Size = new System.Drawing.Size(41, 12);
            this.lbUserNum.TabIndex = 10;
            this.lbUserNum.Text = "工号：";
            // 
            // lbName
            // 
            this.lbName.AutoSize = true;
            this.lbName.Location = new System.Drawing.Point(693, 73);
            this.lbName.Name = "lbName";
            this.lbName.Size = new System.Drawing.Size(41, 12);
            this.lbName.TabIndex = 11;
            this.lbName.Text = "姓名：";
            // 
            // tbUserID
            // 
            this.tbUserID.Location = new System.Drawing.Point(695, 41);
            this.tbUserID.Name = "tbUserID";
            this.tbUserID.Size = new System.Drawing.Size(100, 21);
            this.tbUserID.TabIndex = 12;
            // 
            // tbName
            // 
            this.tbName.Location = new System.Drawing.Point(695, 93);
            this.tbName.Name = "tbName";
            this.tbName.Size = new System.Drawing.Size(100, 21);
            this.tbName.TabIndex = 13;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tssLbl1,
            this.tssLbl2,
            this.tssLbl3});
            this.statusStrip1.Location = new System.Drawing.Point(0, 377);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(850, 22);
            this.statusStrip1.TabIndex = 14;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // tssLbl1
            // 
            this.tssLbl1.Image = ((System.Drawing.Image)(resources.GetObject("tssLbl1.Image")));
            this.tssLbl1.Name = "tssLbl1";
            this.tssLbl1.Size = new System.Drawing.Size(60, 17);
            this.tssLbl1.Text = "状态栏";
            // 
            // tssLbl2
            // 
            this.tssLbl2.Name = "tssLbl2";
            this.tssLbl2.Size = new System.Drawing.Size(16, 17);
            this.tssLbl2.Text = "  ";
            // 
            // tssLbl3
            // 
            this.tssLbl3.Image = ((System.Drawing.Image)(resources.GetObject("tssLbl3.Image")));
            this.tssLbl3.Name = "tssLbl3";
            this.tssLbl3.Size = new System.Drawing.Size(28, 17);
            this.tssLbl3.Text = " ";
            // 
            // MainFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(850, 399);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.tbName);
            this.Controls.Add(this.tbUserID);
            this.Controls.Add(this.lbName);
            this.Controls.Add(this.lbUserNum);
            this.Controls.Add(this.picFP);
            this.Controls.Add(this.btnVerify);
            this.Controls.Add(this.btnRegister);
            this.Controls.Add(this.btnConnect);
            this.Controls.Add(this.txtQuality);
            this.Controls.Add(this.lblQuality);
            this.Controls.Add(this.txtStatus);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.txtPrompt);
            this.Controls.Add(this.lblPrompt);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "MainFrm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "指纹注册";
            this.Load += new System.EventHandler(this.MainFrm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.picFP)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblPrompt;
        private System.Windows.Forms.TextBox txtPrompt;
        private System.Windows.Forms.TextBox txtStatus;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.TextBox txtQuality;
        private System.Windows.Forms.Label lblQuality;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.Button btnRegister;
        private System.Windows.Forms.Button btnVerify;
        private System.Windows.Forms.PictureBox picFP;
        private System.Windows.Forms.Label lbUserNum;
        private System.Windows.Forms.Label lbName;
        private System.Windows.Forms.TextBox tbUserID;
        private System.Windows.Forms.TextBox tbName;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel tssLbl1;
        private System.Windows.Forms.ToolStripStatusLabel tssLbl2;
        private System.Windows.Forms.ToolStripStatusLabel tssLbl3;
    }
}

