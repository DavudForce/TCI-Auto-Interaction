using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;

namespace adsl_Auto_Interaction_App
{
    public enum Sound
    {
        Fatal
    }
    internal class MySoundPlayer
    {
        public static void Play(Sound sound)
        {
            Task.Run(() =>
            {
                SoundPlayer player = null;
                switch (sound)
                {
                    case Sound.Fatal:
                        player = new SoundPlayer(Properties.Resources.alarm_fatal);
                        break;
                }
                player?.PlaySync();
            });
        }
    }
}
