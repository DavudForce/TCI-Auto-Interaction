using System;
using System.Drawing;
using System.Windows.Forms;
using Timer = System.Windows.Forms.Timer;

namespace adsl_Auto_Interaction_App
{
    public partial class AgressiveNotification : Form
    {
        enum AnimationPhase
        {
            FadeOutText,
            ChangeText,
            FadeInText,
            Wait,
            FinalFadeOut
        }

        string[] _texts;
        int _currentIndex = 0;
        int _fadeStep = 15;       // how much to change RGB each tick
        int _animationInterval = 30; // ms
        int _delay;
        AnimationPhase _phase;

        Timer animationTimer = new Timer();
        Timer translationTimer = new Timer();

        public AgressiveNotification(int delay, params string[] texts)
        {
            InitializeComponent();

            _texts = texts;
            _delay = delay < 1000 ? 1000 : delay;

            // timers
            animationTimer.Interval = _animationInterval;
            animationTimer.Tick += AnimationTimer_Tick;

            translationTimer.Interval = _delay;
            translationTimer.Tick += TranslationTimer_Tick;

            lblDescription.Text = texts.Length > 0 ? texts[0] : "";
            _phase = AnimationPhase.Wait;
        }

        private void AgressiveNotification_Load(object sender, EventArgs e)
        {
            translationTimer.Start();
        }

        private void TranslationTimer_Tick(object sender, EventArgs e)
        {
            if (_phase == AnimationPhase.Wait)
            {
                animationTimer.Start();
                _phase = AnimationPhase.FadeOutText;
            }
        }

        private void AnimationTimer_Tick(object sender, EventArgs e)
        {
            switch (_phase)
            {
                case AnimationPhase.FadeOutText:
                    if (FadeOutLabel(lblDescription))
                    {
                        _phase = AnimationPhase.ChangeText;
                    }
                    break;

                case AnimationPhase.ChangeText:
                    if (_currentIndex < _texts.Length - 1)
                    {
                        _currentIndex++;
                        lblDescription.Text = _texts[_currentIndex];
                        _phase = AnimationPhase.FadeInText;
                    }
                    else
                    {
                        // last cycle finished
                        lblDescription.Text = "Consider this as a \"friendly\" warning";
                        _phase = AnimationPhase.FadeInText;
                    }
                    break;

                case AnimationPhase.FadeInText:
                    if (FadeInLabel(lblDescription))
                    {
                        if (_currentIndex < _texts.Length - 1)
                        {
                            _phase = AnimationPhase.Wait;
                            animationTimer.Stop();
                        }
                        else
                        {
                            // after the "friendly warning" fade-in
                            translationTimer.Stop();
                            Timer delay = new Timer();
                            delay.Interval = 3000;
                            delay.Tick += (s, ev) =>
                            {
                                delay.Stop();
                                _phase = AnimationPhase.FinalFadeOut;
                            };
                            delay.Start();
                        }
                    }
                    break;

                case AnimationPhase.FinalFadeOut:
                    if (FadeOutForm())
                    {
                        animationTimer.Stop();
                        this.Close();
                    }
                    break;
            }
        }

        // --- fade helpers ---
        private bool FadeOutLabel(Label lbl)
        {
            int r = Math.Max(0, lbl.ForeColor.R - _fadeStep);
            int g = Math.Max(0, lbl.ForeColor.G - _fadeStep);
            int b = Math.Max(0, lbl.ForeColor.B - _fadeStep);

            lbl.ForeColor = Color.FromArgb(r, g, b);
            return (r == 0 && g == 0 && b == 0);
        }

        private bool FadeInLabel(Label lbl)
        {
            int r = Math.Min(255, lbl.ForeColor.R + _fadeStep);
            int g = Math.Min(255, lbl.ForeColor.G + _fadeStep);
            int b = Math.Min(255, lbl.ForeColor.B + _fadeStep);

            lbl.ForeColor = Color.FromArgb(r, g, b);
            return (r == 255 && g == 255 && b == 255);
        }

        private bool FadeOutForm()
        {
            this.Opacity -= 0.05;
            return this.Opacity <= 0;
        }
    }
}
