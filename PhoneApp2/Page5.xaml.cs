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
    public partial class Page5 : PhoneApplicationPage
    {
        MarketplaceDetailTask _marketPlaceDetailTask = new MarketplaceDetailTask();

        public Page5()
        {
            InitializeComponent();
        }

        public void goto_game(int x,int y, int s,int e)
        {
            Random rnd = new Random();
            int t=0;

            t = rnd.Next(e + 1 - s) + s;
            cons.num_mines = t;
            cons.n_c = x;
            cons.n_r = y;
            cons.n = (cons.n_r + 1) * cons.n_c + (cons.n_c + 1) * cons.n_r;
            cons.n_boxes = cons.n_r * cons.n_c;
            NavigationService.Navigate(new Uri("/Page1.xaml", UriKind.Relative));
        }

        private void b1_Click(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            goto_game(4, 4, 1, 3);
        }

        private void b2_Click(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            goto_game(5, 5, 2, 4);
        }

        private void b3_Click(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if ((Application.Current as App).IsTrial)
            {
                pop_trial.Visibility = Visibility.Visible;
            }
            else
            {
                goto_game(5, 6, 3, 5);
            }
        }

        private void b4_Click(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            goto_game(6, 9, 6, 9);
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