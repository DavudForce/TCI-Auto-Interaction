namespace adsl_Auto_Interaction_App
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            web = new Microsoft.Web.WebView2.WinForms.WebView2();
            splitContainer1 = new SplitContainer();
            panel1 = new Panel();
            groupBox4 = new GroupBox();
            label17 = new Label();
            lblTimedTrafficPercentage = new Label();
            lblTimedDaysPercentage = new Label();
            lblActiveDaysPercentage = new Label();
            lblActiveTrafficPercentage = new Label();
            label21 = new Label();
            label20 = new Label();
            label16 = new Label();
            groupBox3 = new GroupBox();
            txtUploaded = new TextBox();
            txtDownloaded = new TextBox();
            label15 = new Label();
            label13 = new Label();
            label14 = new Label();
            label12 = new Label();
            txtBilling = new TextBox();
            groupBox2 = new GroupBox();
            txtTimedServiceTrafficLeft = new TextBox();
            label8 = new Label();
            txtTimedServiceDaysLeft = new TextBox();
            label9 = new Label();
            txtTimedServiceName = new TextBox();
            label10 = new Label();
            groupBox1 = new GroupBox();
            txtActiveServiceTraficLeft = new TextBox();
            txtActiveServiceDaysLeft = new TextBox();
            txtActiveServiceName = new TextBox();
            label5 = new Label();
            label6 = new Label();
            label7 = new Label();
            label11 = new Label();
            label4 = new Label();
            btnOpenSettings = new Button();
            btnRetrieveData = new Button();
            btnClearText = new Button();
            btnSend = new Button();
            txtPass = new TextBox();
            txtUsername = new TextBox();
            txtConnectionState = new Label();
            label3 = new Label();
            label2 = new Label();
            label1 = new Label();
            connectionCheckTimer = new System.Windows.Forms.Timer(components);
            ((System.ComponentModel.ISupportInitialize)web).BeginInit();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            panel1.SuspendLayout();
            groupBox4.SuspendLayout();
            groupBox3.SuspendLayout();
            groupBox2.SuspendLayout();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // web
            // 
            web.AllowExternalDrop = true;
            web.CreationProperties = null;
            web.DefaultBackgroundColor = Color.White;
            web.Dock = DockStyle.Fill;
            web.Location = new Point(0, 0);
            web.Name = "web";
            web.Size = new Size(262, 423);
            web.TabIndex = 0;
            web.ZoomFactor = 1D;
            // 
            // splitContainer1
            // 
            splitContainer1.Dock = DockStyle.Fill;
            splitContainer1.Location = new Point(0, 0);
            splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(web);
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(panel1);
            splitContainer1.Panel2.Controls.Add(btnOpenSettings);
            splitContainer1.Panel2.Controls.Add(btnRetrieveData);
            splitContainer1.Panel2.Controls.Add(btnClearText);
            splitContainer1.Panel2.Controls.Add(btnSend);
            splitContainer1.Panel2.Controls.Add(txtPass);
            splitContainer1.Panel2.Controls.Add(txtUsername);
            splitContainer1.Panel2.Controls.Add(txtConnectionState);
            splitContainer1.Panel2.Controls.Add(label3);
            splitContainer1.Panel2.Controls.Add(label2);
            splitContainer1.Panel2.Controls.Add(label1);
            splitContainer1.Size = new Size(649, 423);
            splitContainer1.SplitterDistance = 262;
            splitContainer1.TabIndex = 1;
            // 
            // panel1
            // 
            panel1.AutoScroll = true;
            panel1.Controls.Add(groupBox4);
            panel1.Controls.Add(groupBox3);
            panel1.Controls.Add(txtBilling);
            panel1.Controls.Add(groupBox2);
            panel1.Controls.Add(groupBox1);
            panel1.Controls.Add(label11);
            panel1.Controls.Add(label4);
            panel1.Location = new Point(29, 176);
            panel1.Name = "panel1";
            panel1.Size = new Size(346, 235);
            panel1.TabIndex = 3;
            // 
            // groupBox4
            // 
            groupBox4.Controls.Add(label17);
            groupBox4.Controls.Add(lblTimedTrafficPercentage);
            groupBox4.Controls.Add(lblTimedDaysPercentage);
            groupBox4.Controls.Add(lblActiveDaysPercentage);
            groupBox4.Controls.Add(lblActiveTrafficPercentage);
            groupBox4.Controls.Add(label21);
            groupBox4.Controls.Add(label20);
            groupBox4.Controls.Add(label16);
            groupBox4.Location = new Point(5, 339);
            groupBox4.Name = "groupBox4";
            groupBox4.Size = new Size(319, 99);
            groupBox4.TabIndex = 10;
            groupBox4.TabStop = false;
            groupBox4.Text = "Percentages";
            // 
            // label17
            // 
            label17.AutoSize = true;
            label17.Location = new Point(17, 38);
            label17.Name = "label17";
            label17.Size = new Size(127, 15);
            label17.TabIndex = 0;
            label17.Text = "Active days remaining:";
            // 
            // lblTimedTrafficPercentage
            // 
            lblTimedTrafficPercentage.AutoSize = true;
            lblTimedTrafficPercentage.Location = new Point(141, 56);
            lblTimedTrafficPercentage.Name = "lblTimedTrafficPercentage";
            lblTimedTrafficPercentage.Size = new Size(35, 15);
            lblTimedTrafficPercentage.TabIndex = 0;
            lblTimedTrafficPercentage.Text = "000%";
            // 
            // lblTimedDaysPercentage
            // 
            lblTimedDaysPercentage.AutoSize = true;
            lblTimedDaysPercentage.Location = new Point(141, 75);
            lblTimedDaysPercentage.Name = "lblTimedDaysPercentage";
            lblTimedDaysPercentage.Size = new Size(35, 15);
            lblTimedDaysPercentage.TabIndex = 0;
            lblTimedDaysPercentage.Text = "000%";
            // 
            // lblActiveDaysPercentage
            // 
            lblActiveDaysPercentage.AutoSize = true;
            lblActiveDaysPercentage.Location = new Point(141, 38);
            lblActiveDaysPercentage.Name = "lblActiveDaysPercentage";
            lblActiveDaysPercentage.Size = new Size(35, 15);
            lblActiveDaysPercentage.TabIndex = 0;
            lblActiveDaysPercentage.Text = "000%";
            // 
            // lblActiveTrafficPercentage
            // 
            lblActiveTrafficPercentage.AutoSize = true;
            lblActiveTrafficPercentage.Location = new Point(141, 20);
            lblActiveTrafficPercentage.Name = "lblActiveTrafficPercentage";
            lblActiveTrafficPercentage.Size = new Size(35, 15);
            lblActiveTrafficPercentage.TabIndex = 0;
            lblActiveTrafficPercentage.Text = "000%";
            // 
            // label21
            // 
            label21.AutoSize = true;
            label21.Location = new Point(17, 75);
            label21.Name = "label21";
            label21.Size = new Size(127, 15);
            label21.TabIndex = 0;
            label21.Text = "Timed days remaining:";
            // 
            // label20
            // 
            label20.AutoSize = true;
            label20.Location = new Point(10, 56);
            label20.Name = "label20";
            label20.Size = new Size(134, 15);
            label20.TabIndex = 0;
            label20.Text = "Timed traffic remaining:";
            // 
            // label16
            // 
            label16.AutoSize = true;
            label16.Location = new Point(10, 19);
            label16.Name = "label16";
            label16.Size = new Size(134, 15);
            label16.TabIndex = 0;
            label16.Text = "Active traffic remaining:";
            // 
            // groupBox3
            // 
            groupBox3.Controls.Add(txtUploaded);
            groupBox3.Controls.Add(txtDownloaded);
            groupBox3.Controls.Add(label15);
            groupBox3.Controls.Add(label13);
            groupBox3.Controls.Add(label14);
            groupBox3.Controls.Add(label12);
            groupBox3.Location = new Point(5, 257);
            groupBox3.Name = "groupBox3";
            groupBox3.Size = new Size(319, 76);
            groupBox3.TabIndex = 9;
            groupBox3.TabStop = false;
            groupBox3.Text = "Details";
            // 
            // txtUploaded
            // 
            txtUploaded.Location = new Point(97, 45);
            txtUploaded.Name = "txtUploaded";
            txtUploaded.ReadOnly = true;
            txtUploaded.Size = new Size(90, 23);
            txtUploaded.TabIndex = 1;
            // 
            // txtDownloaded
            // 
            txtDownloaded.Location = new Point(110, 16);
            txtDownloaded.Name = "txtDownloaded";
            txtDownloaded.ReadOnly = true;
            txtDownloaded.Size = new Size(90, 23);
            txtDownloaded.TabIndex = 1;
            // 
            // label15
            // 
            label15.AutoSize = true;
            label15.Location = new Point(193, 48);
            label15.Name = "label15";
            label15.Size = new Size(65, 15);
            label15.TabIndex = 0;
            label15.Text = "MB of data";
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.Location = new Point(201, 19);
            label13.Name = "label13";
            label13.Size = new Size(65, 15);
            label13.TabIndex = 0;
            label13.Text = "MB of data";
            // 
            // label14
            // 
            label14.AutoSize = true;
            label14.Location = new Point(3, 48);
            label14.Name = "label14";
            label14.Size = new Size(91, 15);
            label14.TabIndex = 0;
            label14.Text = "Today uploaded";
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Location = new Point(3, 19);
            label12.Name = "label12";
            label12.Size = new Size(107, 15);
            label12.TabIndex = 0;
            label12.Text = "Today downloaded";
            // 
            // txtBilling
            // 
            txtBilling.Location = new Point(50, 6);
            txtBilling.Name = "txtBilling";
            txtBilling.ReadOnly = true;
            txtBilling.Size = new Size(155, 23);
            txtBilling.TabIndex = 1;
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(txtTimedServiceTrafficLeft);
            groupBox2.Controls.Add(label8);
            groupBox2.Controls.Add(txtTimedServiceDaysLeft);
            groupBox2.Controls.Add(label9);
            groupBox2.Controls.Add(txtTimedServiceName);
            groupBox2.Controls.Add(label10);
            groupBox2.ForeColor = SystemColors.GrayText;
            groupBox2.Location = new Point(5, 147);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(319, 104);
            groupBox2.TabIndex = 6;
            groupBox2.TabStop = false;
            groupBox2.Text = "Timed Service";
            // 
            // txtTimedServiceTrafficLeft
            // 
            txtTimedServiceTrafficLeft.ForeColor = SystemColors.GrayText;
            txtTimedServiceTrafficLeft.Location = new Point(135, 73);
            txtTimedServiceTrafficLeft.Name = "txtTimedServiceTrafficLeft";
            txtTimedServiceTrafficLeft.ReadOnly = true;
            txtTimedServiceTrafficLeft.Size = new Size(175, 23);
            txtTimedServiceTrafficLeft.TabIndex = 1;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(3, 77);
            label8.Name = "label8";
            label8.Size = new Size(132, 15);
            label8.TabIndex = 0;
            label8.Text = "Trafic Left From Service:";
            // 
            // txtTimedServiceDaysLeft
            // 
            txtTimedServiceDaysLeft.ForeColor = SystemColors.GrayText;
            txtTimedServiceDaysLeft.Location = new Point(135, 44);
            txtTimedServiceDaysLeft.Name = "txtTimedServiceDaysLeft";
            txtTimedServiceDaysLeft.ReadOnly = true;
            txtTimedServiceDaysLeft.Size = new Size(175, 23);
            txtTimedServiceDaysLeft.TabIndex = 1;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(6, 47);
            label9.Name = "label9";
            label9.Size = new Size(129, 15);
            label9.TabIndex = 0;
            label9.Text = "Days Left From Service:";
            // 
            // txtTimedServiceName
            // 
            txtTimedServiceName.ForeColor = SystemColors.GrayText;
            txtTimedServiceName.Location = new Point(135, 15);
            txtTimedServiceName.Name = "txtTimedServiceName";
            txtTimedServiceName.ReadOnly = true;
            txtTimedServiceName.Size = new Size(175, 23);
            txtTimedServiceName.TabIndex = 1;
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new Point(53, 18);
            label10.Name = "label10";
            label10.Size = new Size(82, 15);
            label10.TabIndex = 0;
            label10.Text = "Service Name:";
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(txtActiveServiceTraficLeft);
            groupBox1.Controls.Add(txtActiveServiceDaysLeft);
            groupBox1.Controls.Add(txtActiveServiceName);
            groupBox1.Controls.Add(label5);
            groupBox1.Controls.Add(label6);
            groupBox1.Controls.Add(label7);
            groupBox1.ForeColor = SystemColors.GrayText;
            groupBox1.Location = new Point(5, 37);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(319, 104);
            groupBox1.TabIndex = 7;
            groupBox1.TabStop = false;
            groupBox1.Text = "Active Service";
            // 
            // txtActiveServiceTraficLeft
            // 
            txtActiveServiceTraficLeft.ForeColor = SystemColors.GrayText;
            txtActiveServiceTraficLeft.Location = new Point(135, 74);
            txtActiveServiceTraficLeft.Name = "txtActiveServiceTraficLeft";
            txtActiveServiceTraficLeft.ReadOnly = true;
            txtActiveServiceTraficLeft.Size = new Size(175, 23);
            txtActiveServiceTraficLeft.TabIndex = 1;
            // 
            // txtActiveServiceDaysLeft
            // 
            txtActiveServiceDaysLeft.ForeColor = SystemColors.GrayText;
            txtActiveServiceDaysLeft.Location = new Point(135, 45);
            txtActiveServiceDaysLeft.Name = "txtActiveServiceDaysLeft";
            txtActiveServiceDaysLeft.ReadOnly = true;
            txtActiveServiceDaysLeft.Size = new Size(175, 23);
            txtActiveServiceDaysLeft.TabIndex = 1;
            // 
            // txtActiveServiceName
            // 
            txtActiveServiceName.ForeColor = SystemColors.GrayText;
            txtActiveServiceName.Location = new Point(135, 16);
            txtActiveServiceName.Name = "txtActiveServiceName";
            txtActiveServiceName.ReadOnly = true;
            txtActiveServiceName.Size = new Size(175, 23);
            txtActiveServiceName.TabIndex = 1;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(53, 19);
            label5.Name = "label5";
            label5.Size = new Size(82, 15);
            label5.TabIndex = 0;
            label5.Text = "Service Name:";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(6, 48);
            label6.Name = "label6";
            label6.Size = new Size(129, 15);
            label6.TabIndex = 0;
            label6.Text = "Days Left From Service:";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(3, 78);
            label7.Name = "label7";
            label7.Size = new Size(132, 15);
            label7.TabIndex = 0;
            label7.Text = "Trafic Left From Service:";
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Location = new Point(207, 9);
            label11.Name = "label11";
            label11.Size = new Size(23, 15);
            label11.TabIndex = 5;
            label11.Text = "rial";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(8, 9);
            label4.Name = "label4";
            label4.Size = new Size(43, 15);
            label4.TabIndex = 5;
            label4.Text = "Billing:";
            // 
            // btnOpenSettings
            // 
            btnOpenSettings.Location = new Point(131, 22);
            btnOpenSettings.Name = "btnOpenSettings";
            btnOpenSettings.Size = new Size(75, 23);
            btnOpenSettings.TabIndex = 2;
            btnOpenSettings.Text = "Settings";
            btnOpenSettings.UseVisualStyleBackColor = true;
            btnOpenSettings.Click += btnOpenSettings_Click;
            // 
            // btnRetrieveData
            // 
            btnRetrieveData.Location = new Point(202, 147);
            btnRetrieveData.Name = "btnRetrieveData";
            btnRetrieveData.Size = new Size(156, 23);
            btnRetrieveData.TabIndex = 2;
            btnRetrieveData.Text = "Retrieve Data";
            btnRetrieveData.UseVisualStyleBackColor = true;
            btnRetrieveData.Click += btnRetrieveData_Click;
            // 
            // btnClearText
            // 
            btnClearText.Location = new Point(202, 118);
            btnClearText.Name = "btnClearText";
            btnClearText.Size = new Size(75, 23);
            btnClearText.TabIndex = 2;
            btnClearText.Text = "Clear";
            btnClearText.UseVisualStyleBackColor = true;
            // 
            // btnSend
            // 
            btnSend.Location = new Point(283, 118);
            btnSend.Name = "btnSend";
            btnSend.Size = new Size(75, 23);
            btnSend.TabIndex = 2;
            btnSend.Text = "Submit";
            btnSend.UseVisualStyleBackColor = true;
            btnSend.Click += btnSend_Click;
            // 
            // txtPass
            // 
            txtPass.Location = new Point(101, 89);
            txtPass.Name = "txtPass";
            txtPass.Size = new Size(257, 23);
            txtPass.TabIndex = 1;
            txtPass.Text = "92747253";
            // 
            // txtUsername
            // 
            txtUsername.Location = new Point(101, 60);
            txtUsername.Name = "txtUsername";
            txtUsername.Size = new Size(257, 23);
            txtUsername.TabIndex = 1;
            txtUsername.Text = "4134792252 ";
            // 
            // txtConnectionState
            // 
            txtConnectionState.AutoSize = true;
            txtConnectionState.Location = new Point(94, 26);
            txtConnectionState.Name = "txtConnectionState";
            txtConnectionState.Size = new Size(31, 15);
            txtConnectionState.TabIndex = 0;
            txtConnectionState.Text = "false";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(29, 92);
            label3.Name = "label3";
            label3.Size = new Size(60, 15);
            label3.TabIndex = 0;
            label3.Text = "Password:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(29, 63);
            label2.Name = "label2";
            label2.Size = new Size(66, 15);
            label2.TabIndex = 0;
            label2.Text = "User name:";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(29, 26);
            label1.Name = "label1";
            label1.Size = new Size(71, 15);
            label1.TabIndex = 0;
            label1.Text = "Connected: ";
            // 
            // connectionCheckTimer
            // 
            connectionCheckTimer.Interval = 10000;
            connectionCheckTimer.Tick += timer_Tick;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(649, 423);
            Controls.Add(splitContainer1);
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Form1";
            FormClosing += Form1_FormClosing;
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)web).EndInit();
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel2.ResumeLayout(false);
            splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            groupBox4.ResumeLayout(false);
            groupBox4.PerformLayout();
            groupBox3.ResumeLayout(false);
            groupBox3.PerformLayout();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Microsoft.Web.WebView2.WinForms.WebView2 web;
        private SplitContainer splitContainer1;
        private Label txtConnectionState;
        private Label label1;
        private System.Windows.Forms.Timer connectionCheckTimer;
        private Button btnClearText;
        private Button btnSend;
        private TextBox txtPass;
        private TextBox txtUsername;
        private Label label2;
        private Label label3;
        private Button btnRetrieveData;
        private Button btnOpenSettings;
        private Panel panel1;
        private GroupBox groupBox2;
        private TextBox txtTimedServiceTrafficLeft;
        private Label label8;
        private TextBox txtTimedServiceDaysLeft;
        private Label label9;
        private TextBox txtTimedServiceName;
        private Label label10;
        private GroupBox groupBox1;
        private TextBox txtActiveServiceTraficLeft;
        private TextBox txtActiveServiceDaysLeft;
        private TextBox txtActiveServiceName;
        private Label label5;
        private Label label6;
        private Label label7;
        private Label label4;
        private GroupBox groupBox3;
        private Label label12;
        private TextBox txtUploaded;
        private TextBox txtDownloaded;
        private Label label15;
        private Label label13;
        private Label label14;
        private TextBox txtBilling;
        private Label label11;
        private GroupBox groupBox4;
        private Label label17;
        private Label lblActiveDaysPercentage;
        private Label lblActiveTrafficPercentage;
        private Label label21;
        private Label label20;
        private Label label16;
        private Label lblTimedTrafficPercentage;
        private Label lblTimedDaysPercentage;
    }
}
