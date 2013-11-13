using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using System.Windows.Media.Imaging;
using System.Diagnostics;
using System.Windows.Navigation;
using Microsoft.Phone.Tasks;

namespace PhoneApp2
{
    class cons_l
    {
        public static int height = 97;
        public static int width = 456;
    }

    public partial class Page2 : PhoneApplicationPage
    {
        MarketplaceDetailTask _marketPlaceDetailTask = new MarketplaceDetailTask();

        public Page2()
        {
            InitializeComponent();
            SoundsInitialiser.initialiseSounds();
            if(cons.music)
                BackgroundMusic.play();
        }

        private void c_si(object sender, MouseButtonEventArgs e)
        {
            cons.multi = false;
            NavigationService.Navigate(new Uri("/Page3.xaml", UriKind.Relative));
        }

        private void c_options(object sender, MouseButtonEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Page4.xaml", UriKind.Relative));
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            NavigationService.RemoveBackEntry();
            if ((Application.Current as App).IsTrial)
            {
                buy1.Visibility = Visibility.Visible;
            }
        }

        private void c_multi(object sender, MouseButtonEventArgs e)
        {
            if ((Application.Current as App).IsTrial)
            {
                pop_trial.Visibility = Visibility.Visible;
            }
            else
            {
                cons.multi = true;
                App.start_type = true;
                NavigationService.Navigate(new Uri("/Page9.xaml", UriKind.Relative));
                //NavigationService.Navigate(new Uri("/GetName.xaml",UriKind.Relative));
            }
        }

        private void trial_cl(object sender, MouseButtonEventArgs e)
        {
            pop_trial.Visibility = Visibility.Collapsed;
        }

        private void buy_cl(object sender, MouseButtonEventArgs e)
        {
            _marketPlaceDetailTask.Show();
        }

    }
}