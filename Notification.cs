using adsl_Auto_Interaction_App.Properties;
using System.Media;
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
                if (_description != value)
                {
                    _description = value;
                    OnDescriptionChanged();
                }
            }
        }

        int _closeDelay = 5000;

        Timer _closeTimer = new Timer();

        // fade timers
        Timer _fadeInTimer = new Timer();
        Timer _fadeOutTimer = new Timer();

        public Notification()
        {
            _closeTimer.Tick += (s, e) => StartFadeOut();

            _fadeInTimer.Interval = 30;
            _fadeInTimer.Tick += FadeInTimer_Tick;

            _fadeOutTimer.Interval = 30;
            _fadeOutTimer.Tick += FadeOutTimer_Tick;

            InitializeComponent();
            PlaceLowerRight();

            this.Opacity = 0; // start invisible
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

        // ---------- Fade In/Out ----------
        private void FadeInTimer_Tick(object sender, EventArgs e)
        {
            if (this.Opacity < 1.0)
                this.Opacity += 0.1;
            else
                _fadeInTimer.Stop();
        }

        private void FadeOutTimer_Tick(object sender, EventArgs e)
        {
            if (this.Opacity > 0.0)
                this.Opacity -= 0.1;
            else
            {
                _fadeOutTimer.Stop();
                this.Close();
            }
        }

        private void StartFadeOut()
        {
            _closeTimer.Stop();
            _fadeOutTimer.Start();
        }

        // ---------- Show ----------
        public void Up(NoticficationStyle style, string text)
        {
            PrepareNotification(style, text);
            this.Show();
            _fadeInTimer.Start();
        }

        public void Up(NoticficationStyle style, string text, int closeDelay)
        {
            _closeDelay = closeDelay;
            _closeTimer.Interval = _closeDelay;
            _closeTimer.Start();
            PrepareNotification(style, text);
            this.Show();
            _fadeInTimer.Start();
        }

        public void Up(NoticficationStyle style, string text, int closeDelay, bool playSound)
        {
            _closeDelay = closeDelay;
            _closeTimer.Interval = _closeDelay;
            _closeTimer.Start();
            PrepareNotification(style, text);
            this.Show();
            _fadeInTimer.Start();

            if (playSound)
            {
                PlaySound(style);
            }
        }

        public void Up(NoticficationStyle style, string text, bool showClose)
        {
            PrepareNotification(style, text);
            this.Show();
            _fadeInTimer.Start();

            if (showClose)
                btnClose.Visible = true;
        }

        public void Up(NoticficationStyle style, string text, bool showClose, bool playSound)
        {
            PrepareNotification(style, text);
            this.Show();
            _fadeInTimer.Start();

            if (showClose)
                btnClose.Visible = true;

            if (playSound)
                PlaySound(style);
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
            _fadeInTimer.Start();
        }

        public void UpMost(NoticficationStyle style, string text, int closeDelay)
        {
            _closeDelay = closeDelay;
            _closeTimer.Interval = _closeDelay;
            _closeTimer.Start();
            PrepareNotification(style, text);
            this.ShowDialog();
            _fadeInTimer.Start();
        }

        public void UpMost(NoticficationStyle style, string text, int closeDelay, bool playSounds)
        {
            _closeDelay = closeDelay;
            _closeTimer.Interval = _closeDelay;
            _closeTimer.Start();
            PrepareNotification(style, text);
            this.ShowDialog();
            _fadeInTimer.Start();
            if (playSounds) PlaySound(style);
        }

        public void Down()
        {
            StartFadeOut();
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

        private void btnClose_Click(object sender, EventArgs e)
        {
            _fadeOutTimer.Start();
        }
    }
}
