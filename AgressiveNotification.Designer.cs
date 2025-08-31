namespace adsl_Auto_Interaction_App
{
    partial class AgressiveNotification
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
            lblDescription = new Label();
            SuspendLayout();
            // 
            // lblDescription
            // 
            lblDescription.Dock = DockStyle.Fill;
            lblDescription.Font = new Font("Segoe UI", 30F);
            lblDescription.ForeColor = Color.White;
            lblDescription.Location = new Point(0, 0);
            lblDescription.Name = "lblDescription";
            lblDescription.Size = new Size(800, 510);
            lblDescription.TabIndex = 0;
            lblDescription.Text = "Reason";
            lblDescription.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // AgressiveNotification
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Black;
            ClientSize = new Size(800, 510);
            ControlBox = false;
            Controls.Add(lblDescription);
            Font = new Font("Segoe UI", 10F);
            ForeColor = Color.WhiteSmoke;
            FormBorderStyle = FormBorderStyle.None;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "AgressiveNotification";
            ShowIcon = false;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Why did you do this?";
            TopMost = true;
            WindowState = FormWindowState.Maximized;
            FormClosing += AgressiveNotification_FormClosing;
            Load += AgressiveNotification_Load;
            ResumeLayout(false);
        }

        #endregion

        private Label lblDescription;
    }
}