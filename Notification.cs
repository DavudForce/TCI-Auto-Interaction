using adsl_Auto_Interaction_App.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;
using Timer = System.Windows.Forms.Timer;

namespace adsl_Auto_Interaction_App
{
    public enum NoticficationStyle
    {
        Info,
        Warning,
        Error
    }
    public partial class Notification : Form
    {
        private string _description;
        public string Description 
        {
            get => _description;
            set
            {
                if(_description != value)
                {
                    _description = value;
                    OnDescriptionChanged();
                }
            }
        }

        int _closeDelay = 5000;

        Timer _closeTimer = new Timer();

        public Notification()
        {
            _closeTimer.Tick += (s, e) => Down();
            InitializeComponent();
            PlaceLowerRight();

            pctrIcon.SizeMode = PictureBoxSizeMode.Zoom;
        }

        private void OnDescriptionChanged()
        {
            lblDescription.Text = _description;
        }

        private void PrepareNotification(NoticficationStyle style, string text)
        {
            switch (style)
            {
                case NoticficationStyle.Info:
                    pctrIcon.Image = Resources.info1;
                    lblStyle.Text = "INFO";
                    pnlColor.BackColor = Color.FromArgb(0, 119, 215);
                    break;
                case NoticficationStyle.Warning:
                    pctrIcon.Image = Resources.warning1;
                    lblStyle.Text = "WARNING";
                    pnlColor.BackColor = Color.FromArgb(255, 255, 0);
                    break;
                case NoticficationStyle.Error:
                    pctrIcon.Image = Resources.error1;
                    lblStyle.Text = "ERROR";
                    pnlColor.BackColor = Color.FromArgb(240, 57, 22);
                    break;
                default:
                    break;
            }

            lblDescription.Text = text;
        }

        public void Up(NoticficationStyle style, string text)
        {
            PrepareNotification(style, text);
            this.Show();
        }

        public void Up(NoticficationStyle style, string text, int closeDelay)
        {
            _closeDelay = closeDelay;
            _closeTimer.Interval = _closeDelay;
            _closeTimer.Start();
            PrepareNotification(style, text);
            this.Show();
        }

        public void Up(NoticficationStyle style, string text, int closeDelay, bool playSound)
        {
            _closeDelay = closeDelay;
            _closeTimer.Interval = _closeDelay;
            _closeTimer.Start();
            PrepareNotification(style, text);
            this.Show();

            if (playSound)
            {
                PlaySound(style);
            }
        }

        private void PlaySound(NoticficationStyle style)
        {
            switch (style)
            {
                case NoticficationStyle.Info:
                    SystemSounds.Asterisk.Play();
                    break;
                case NoticficationStyle.Warning:
                    SystemSounds.Exclamation.Play();
                    break;
                case NoticficationStyle.Error:
                    SystemSounds.Hand.Play();
                    break;
                default:
                    break;
            }
        }

        public void UpMost(NoticficationStyle style, string text)
        {
            PrepareNotification(style, text);
            this.ShowDialog();
        }

        public void UpMost(NoticficationStyle style, string text, int closeDelay)
        {
            _closeDelay = closeDelay;
            _closeTimer.Interval = _closeDelay;
            _closeTimer.Start();
            PrepareNotification(style, text);
            this.ShowDialog();
        }

        public void UpMost(NoticficationStyle style, string text, int closeDelay, bool playSounds)
        {
            _closeDelay = closeDelay;
            _closeTimer.Interval = _closeDelay;
            _closeTimer.Start();
            PrepareNotification(style, text);
            this.ShowDialog();
            if (playSounds) PlaySound(style);
        }

        public void Down()
        {
            this.Close();
        }

        private void PlaceLowerRight()
        {
            //Determine "rightmost" screen
            Screen rightmost = Screen.AllScreens[0];
            foreach (Screen screen in Screen.AllScreens)
            {
                if (screen.WorkingArea.Right > rightmost.WorkingArea.Right)
                    rightmost = screen;
            }

            this.Left = rightmost.WorkingArea.Right - this.Width;
            this.Top = rightmost.WorkingArea.Bottom - this.Height;
        }
    }
}
