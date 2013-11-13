using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace PhoneApp2
{
    public partial class Page6 : PhoneApplicationPage
    {
        public Page6()
        {
            InitializeComponent();
        }

        public void update()
        {
            if (Page1.save_g.Contains(cons.term[7]))
            {
                tb1.Text = " games played - " + Convert.ToString(Page1.save_g[cons.term[7]]);
                tb2.Text = " games won - " + Convert.ToString(Page1.save_g[cons.term[8]]);
                tb3.Text = " games tied - " + Convert.ToString(Page1.save_g[cons.term[9]]);
                tb4.Text = " Winning % - " + String.Format("{0:F2}", (double)Page1.save_g[cons.term[10]]);
            }
            else
            {
                tb1.Text = " games played - " + "0";
                tb2.Text = " games won - " + "0";
                tb3.Text = " games tied - " + "0";
                tb4.Text = " Winning % - " + "0.0";
            }
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            update();
        }

        private void reset(object sender, RoutedEventArgs e)
        {
            string message = "This will delete all your Game Statistics. Are you sure about that ?";
            if (MessageBox.Show(message, "Warning: Reset Stats !!",
                 MessageBoxButton.OKCancel) == MessageBoxResult.OK)
            {
                if (Page1.save_g.Contains(cons.term[7]))
                {
                    Page1.save_g[cons.term[7]] = 0.0;

                    Page1.save_g[cons.term[8]] = 0.0;

                    Page1.save_g[cons.term[9]] = 0;

                    Page1.save_g[cons.term[10]] = 0.0;
                }
            
            }
            update();

        }

    }
}