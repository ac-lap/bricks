using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.Windows.Media.Imaging;

namespace PhoneApp2
{
    public partial class Page4 : PhoneApplicationPage
    {
        public Page4()
        {
            InitializeComponent();
        }

        private void sound(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (cons.sound)
            {
                cons.sound = false;
                b2.Source = new BitmapImage(new Uri("sound-off.png", UriKind.Relative));
            }
            else
            {
                cons.sound = true;
                b2.Source = new BitmapImage(new Uri("sound-on.png", UriKind.Relative));
            }
            if (Page1.save_g.Contains(cons.term[12]))
                Page1.save_g[cons.term[12]] = cons.sound;
            else
            {
                Page1.save_g.Add(cons.term[12], cons.sound);
            }
        }

        private void music(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (cons.music)
            {
                cons.music = false;
                BackgroundMusic.stop();
                b1.Source = new BitmapImage(new Uri("music-off.png", UriKind.Relative));
            }
            else
            {
                cons.music = true;
                BackgroundMusic.play();
                b1.Source = new BitmapImage(new Uri("music-on.png", UriKind.Relative));
            }
            if (Page1.save_g.Contains(cons.term[11]))
                Page1.save_g[cons.term[11]] = cons.music;
            else
            {
                Page1.save_g.Add(cons.term[11], cons.music);
            }
        }
        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            if (cons.sound == false)
            {
                b2.Source = new BitmapImage(new Uri("sound-off.png", UriKind.Relative));
            }
            else
            {
                b2.Source = new BitmapImage(new Uri("sound-on.png", UriKind.Relative));
            }

            if (cons.music == false)
            {
                b1.Source = new BitmapImage(new Uri("music-off.png", UriKind.Relative));
            }
            else
            {
                b1.Source = new BitmapImage(new Uri("music-on.png", UriKind.Relative));
            }
        }
    }
}