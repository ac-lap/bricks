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
    public partial class Page3 : PhoneApplicationPage
    {
        public Page3()
        {
            InitializeComponent();
        }

        private void c_new(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            App.start_type = true;
            NavigationService.Navigate(new Uri("/Page5.xaml", UriKind.Relative));
        }

        private void c_load(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            App.start_type = false;

            if (Page1.save_g.Contains(cons.term[0]))
                cons.n_r = (int)Page1.save_g[cons.term[0]];
            else
                return;
// pop info
            if (Page1.save_g.Contains(cons.term[1]))
                cons.n_c = (int)Page1.save_g[cons.term[1]];

            cons.n = (cons.n_r + 1) * cons.n_c + (cons.n_c + 1) * cons.n_r;
            cons.n_boxes = cons.n_r * cons.n_c;

            if (Page1.save_g.Contains(cons.term[2]))
                cons.n = (int)Page1.save_g[cons.term[2]];

            if (Page1.save_g.Contains(cons.term[3]))
                cons.n_boxes = (int)Page1.save_g[cons.term[3]];

            NavigationService.Navigate(new Uri("/Page1.xaml", UriKind.Relative));
        }
    }
}
