using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.Windows.Input;
using Microsoft.Phone.Tasks;

namespace PhoneApp2
{
    public partial class Page10 : PhoneApplicationPage
    {
        MarketplaceDetailTask _marketPlaceDetailTask = new MarketplaceDetailTask();

        public Page10()
        {
            InitializeComponent();
        }

        private void easy(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            cons.level = 1;
            d1.Visibility = Visibility.Visible;
            d2.Visibility = Visibility.Collapsed;
            d3.Visibility = Visibility.Collapsed;

            save();
        }

        private void med(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            cons.level = 2;
            d1.Visibility = Visibility.Collapsed;
            d2.Visibility = Visibility.Visible;
            d3.Visibility = Visibility.Collapsed;

            save();
        }

        private void hard(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if ((Application.Current as App).IsTrial)
            {
                pop_trial.Visibility = Visibility.Visible;
            }
            else
            {
                cons.level = 3;
                d1.Visibility = Visibility.Collapsed;
                d2.Visibility = Visibility.Collapsed;
                d3.Visibility = Visibility.Visible;

                save();
            }
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            switch (cons.level)
            {
                case 1:
                    d1.Visibility = Visibility.Visible;
                    break;
                case 2:
                    d2.Visibility = Visibility.Visible;
                    break;
                case 3:
                    d3.Visibility = Visibility.Visible;
                    break;
            }
        }

        public void save()
        {
            if (Page1.save_g.Contains(cons.term[13]))
                Page1.save_g[cons.term[13]] = cons.level;
            else
            {
                Page1.save_g.Add(cons.term[13], cons.level);
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