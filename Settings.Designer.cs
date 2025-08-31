namespace adsl_Auto_Interaction_App
{
    partial class Settings
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
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            numActiveDaysLeft = new NumericUpDown();
            numDownloadLimit = new NumericUpDown();
            label6 = new Label();
            numTimedDaysLeft = new NumericUpDown();
            label5 = new Label();
            numBillingLimit = new NumericUpDown();
            label7 = new Label();
            numUploadLimit = new NumericUpDown();
            label8 = new Label();
            label9 = new Label();
            label10 = new Label();
            label11 = new Label();
            label12 = new Label();
            numTolerance = new NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)numActiveDaysLeft).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numDownloadLimit).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numTimedDaysLeft).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numBillingLimit).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numUploadLimit).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numTolerance).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 9);
            label1.Name = "label1";
            label1.Size = new Size(91, 15);
            label1.TabIndex = 0;
            label1.Text = "Warm me when";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(26, 24);
            label2.Name = "label2";
            label2.Size = new Size(180, 15);
            label2.TabIndex = 0;
            label2.Text = "today's download is grather than";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(277, 24);
            label3.Name = "label3";
            label3.Size = new Size(25, 15);
            label3.TabIndex = 0;
            label3.Text = "MB";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(73, 76);
            label4.Name = "label4";
            label4.Size = new Size(217, 15);
            label4.TabIndex = 0;
            label4.Text = "days left from my active internet service";
            // 
            // numActiveDaysLeft
            // 
            numActiveDaysLeft.Location = new Point(26, 74);
            numActiveDaysLeft.Name = "numActiveDaysLeft";
            numActiveDaysLeft.Size = new Size(41, 23);
            numActiveDaysLeft.TabIndex = 2;
            // 
            // numDownloadLimit
            // 
            numDownloadLimit.Location = new Point(212, 21);
            numDownloadLimit.Maximum = new decimal(new int[] { 100000, 0, 0, 0 });
            numDownloadLimit.Name = "numDownloadLimit";
            numDownloadLimit.Size = new Size(61, 23);
            numDownloadLimit.TabIndex = 3;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(73, 105);
            label6.Name = "label6";
            label6.Size = new Size(217, 15);
            label6.TabIndex = 0;
            label6.Text = "days left from my timed internet service";
            // 
            // numTimedDaysLeft
            // 
            numTimedDaysLeft.Location = new Point(26, 103);
            numTimedDaysLeft.Name = "numTimedDaysLeft";
            numTimedDaysLeft.Size = new Size(41, 23);
            numTimedDaysLeft.TabIndex = 2;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(26, 129);
            label5.Name = "label5";
            label5.Size = new Size(142, 15);
            label5.TabIndex = 0;
            label5.Text = "my billing is grathar than ";
            // 
            // numBillingLimit
            // 
            numBillingLimit.Location = new Point(165, 127);
            numBillingLimit.Name = "numBillingLimit";
            numBillingLimit.Size = new Size(82, 23);
            numBillingLimit.TabIndex = 2;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(253, 129);
            label7.Name = "label7";
            label7.Size = new Size(28, 15);
            label7.TabIndex = 0;
            label7.Text = "rials";
            // 
            // numUploadLimit
            // 
            numUploadLimit.Location = new Point(192, 50);
            numUploadLimit.Maximum = new decimal(new int[] { 100000, 0, 0, 0 });
            numUploadLimit.Name = "numUploadLimit";
            numUploadLimit.Size = new Size(61, 23);
            numUploadLimit.TabIndex = 6;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(259, 52);
            label8.Name = "label8";
            label8.Size = new Size(25, 15);
            label8.TabIndex = 4;
            label8.Text = "MB";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(26, 52);
            label9.Name = "label9";
            label9.Size = new Size(164, 15);
            label9.TabIndex = 5;
            label9.Text = "today's upload is grather than";
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new Point(45, 153);
            label10.Name = "label10";
            label10.Size = new Size(249, 15);
            label10.TabIndex = 0;
            label10.Text = "days remainig and data remaining precentage";
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Location = new Point(180, 177);
            label11.Name = "label11";
            label11.Size = new Size(69, 15);
            label11.TabIndex = 0;
            label11.Text = "% tolerance";
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Location = new Point(45, 177);
            label12.Name = "label12";
            label12.Size = new Size(101, 15);
            label12.TabIndex = 0;
            label12.Text = "don't match with ";
            // 
            // numTolerance
            // 
            numTolerance.Location = new Point(142, 175);
            numTolerance.Name = "numTolerance";
            numTolerance.Size = new Size(39, 23);
            numTolerance.TabIndex = 7;
            numTolerance.Value = new decimal(new int[] { 100, 0, 0, 0 });
            // 
            // Settings
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(310, 204);
            Controls.Add(numTolerance);
            Controls.Add(numUploadLimit);
            Controls.Add(label8);
            Controls.Add(label9);
            Controls.Add(numDownloadLimit);
            Controls.Add(numBillingLimit);
            Controls.Add(numTimedDaysLeft);
            Controls.Add(numActiveDaysLeft);
            Controls.Add(label3);
            Controls.Add(label7);
            Controls.Add(label5);
            Controls.Add(label6);
            Controls.Add(label4);
            Controls.Add(label11);
            Controls.Add(label12);
            Controls.Add(label10);
            Controls.Add(label2);
            Controls.Add(label1);
            Name = "Settings";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Settings";
            FormClosing += Settings_FormClosing;
            ((System.ComponentModel.ISupportInitialize)numActiveDaysLeft).EndInit();
            ((System.ComponentModel.ISupportInitialize)numDownloadLimit).EndInit();
            ((System.ComponentModel.ISupportInitialize)numTimedDaysLeft).EndInit();
            ((System.ComponentModel.ISupportInitialize)numBillingLimit).EndInit();
            ((System.ComponentModel.ISupportInitialize)numUploadLimit).EndInit();
            ((System.ComponentModel.ISupportInitialize)numTolerance).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private NumericUpDown numActiveDaysLeft;
        private NumericUpDown numDownloadLimit;
        private Label label6;
        private NumericUpDown numTimedDaysLeft;
        private Label label5;
        private NumericUpDown numBillingLimit;
        private Label label7;
        private NumericUpDown numUploadLimit;
        private Label label8;
        private Label label9;
        private Label label10;
        private Label label11;
        private Label label12;
        private NumericUpDown numTolerance;
    }
}