namespace adsl_Auto_Interaction_App
{
    partial class Notification
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
            pnlColor = new Panel();
            pnlIcon = new Panel();
            pctrIcon = new PictureBox();
            pnlText = new Panel();
            pnlDescription = new Panel();
            lblDescription = new Label();
            pnlType = new Panel();
            btnClose = new Label();
            lblStyle = new Label();
            pnlIcon.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pctrIcon).BeginInit();
            pnlText.SuspendLayout();
            pnlDescription.SuspendLayout();
            pnlType.SuspendLayout();
            SuspendLayout();
            // 
            // pnlColor
            // 
            pnlColor.BackColor = Color.RoyalBlue;
            pnlColor.Dock = DockStyle.Left;
            pnlColor.Location = new Point(0, 0);
            pnlColor.Name = "pnlColor";
            pnlColor.Size = new Size(10, 114);
            pnlColor.TabIndex = 0;
            // 
            // pnlIcon
            // 
            pnlIcon.Controls.Add(pctrIcon);
            pnlIcon.Dock = DockStyle.Left;
            pnlIcon.Location = new Point(10, 0);
            pnlIcon.Name = "pnlIcon";
            pnlIcon.Size = new Size(75, 114);
            pnlIcon.TabIndex = 1;
            // 
            // pctrIcon
            // 
            pctrIcon.BackgroundImageLayout = ImageLayout.Zoom;
            pctrIcon.Dock = DockStyle.Fill;
            pctrIcon.Location = new Point(0, 0);
            pctrIcon.Name = "pctrIcon";
            pctrIcon.Size = new Size(75, 114);
            pctrIcon.TabIndex = 0;
            pctrIcon.TabStop = false;
            // 
            // pnlText
            // 
            pnlText.Controls.Add(pnlDescription);
            pnlText.Controls.Add(pnlType);
            pnlText.Dock = DockStyle.Fill;
            pnlText.Location = new Point(85, 0);
            pnlText.Name = "pnlText";
            pnlText.Size = new Size(249, 114);
            pnlText.TabIndex = 2;
            // 
            // pnlDescription
            // 
            pnlDescription.Controls.Add(lblDescription);
            pnlDescription.Dock = DockStyle.Fill;
            pnlDescription.Location = new Point(0, 50);
            pnlDescription.Name = "pnlDescription";
            pnlDescription.Size = new Size(249, 64);
            pnlDescription.TabIndex = 1;
            // 
            // lblDescription
            // 
            lblDescription.Dock = DockStyle.Fill;
            lblDescription.Font = new Font("Segoe UI", 10F);
            lblDescription.Location = new Point(0, 0);
            lblDescription.Name = "lblDescription";
            lblDescription.Size = new Size(249, 64);
            lblDescription.TabIndex = 0;
            lblDescription.Text = "Descriptioanous";
            // 
            // pnlType
            // 
            pnlType.Controls.Add(btnClose);
            pnlType.Controls.Add(lblStyle);
            pnlType.Dock = DockStyle.Top;
            pnlType.Location = new Point(0, 0);
            pnlType.Name = "pnlType";
            pnlType.Size = new Size(249, 50);
            pnlType.TabIndex = 0;
            // 
            // btnClose
            // 
            btnClose.AutoSize = true;
            btnClose.Cursor = Cursors.Hand;
            btnClose.Font = new Font("Segoe UI", 15F);
            btnClose.Location = new Point(220, -2);
            btnClose.Name = "btnClose";
            btnClose.Size = new Size(26, 28);
            btnClose.TabIndex = 0;
            btnClose.Text = "×";
            btnClose.Visible = false;
            btnClose.Click += btnClose_Click;
            // 
            // lblStyle
            // 
            lblStyle.Dock = DockStyle.Fill;
            lblStyle.Font = new Font("Segoe UI", 16F);
            lblStyle.Location = new Point(0, 0);
            lblStyle.Name = "lblStyle";
            lblStyle.Size = new Size(249, 50);
            lblStyle.TabIndex = 0;
            lblStyle.Text = "INFO";
            lblStyle.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // Notification
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(334, 114);
            ControlBox = false;
            Controls.Add(pnlText);
            Controls.Add(pnlIcon);
            Controls.Add(pnlColor);
            FormBorderStyle = FormBorderStyle.None;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "Notification";
            ShowIcon = false;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.Manual;
            TopMost = true;
            pnlIcon.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pctrIcon).EndInit();
            pnlText.ResumeLayout(false);
            pnlDescription.ResumeLayout(false);
            pnlType.ResumeLayout(false);
            pnlType.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel pnlColor;
        private Panel pnlIcon;
        private Panel pnlText;
        private Panel pnlDescription;
        private Panel pnlType;
        private PictureBox pctrIcon;
        private Label lblDescription;
        private Label lblStyle;
        private Label btnClose;
    }
}