using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Timer = System.Windows.Forms.Timer;

namespace adsl_Auto_Interaction_App
{
    public partial class AgressiveNotification : Form
    {
        enum AnimationStyle
        {
            FadeIn,
            FadeOut
        }
        string[] _texts;
        int animationDelay = 10;
        bool shouldStopAnimation = false;
        Timer animationTimer = new Timer();
        Timer transationTimer = new Timer();

        public AgressiveNotification()
        {
            InitializeComponent();
        }

        public AgressiveNotification(int delay, params string[] texts)
        {
            InitializeComponent();
            transationTimer.Interval = delay < 1000 ? 1000 : delay;
            animationTimer.Interval = animationDelay;
            animationTimer.Tick += (s, e) => Animate(lblDescription, AnimationStyle.FadeOut);
        }

        private void Animate(Control control, AnimationStyle style)
        {
            if (shouldStopAnimation)
            {
                style = AnimationStyle.FadeIn;
            }
            switch (style)
            {
                case AnimationStyle.FadeIn:
                    FadeIn(control);
                    break;
                case AnimationStyle.FadeOut:
                    FadeOut(control);
                    break;
                default:
                    break;
            }
        }

        public void FadeOut(Control control)
        {
            try
            {
                control.ForeColor = Color.FromArgb(control.ForeColor.R - 5, control.ForeColor.G - 5, control.ForeColor.B - 5);
            }
            catch
            {
                shouldStopAnimation = true;
            }
        }

        public void FadeIn(Control control)
        {
            try
            {
                control.ForeColor = Color.FromArgb(control.ForeColor.R + 5, control.ForeColor.G + 5, control.ForeColor.B + 5);
            }
            catch
            {
                shouldStopAnimation = true;
            }
        }

        private void AgressiveNotification_Load(object sender, EventArgs e)
        {
            animationTimer.Start();
        }
    }
}
