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
    public partial class Page7 : PhoneApplicationPage
    {
        public Page7()
        {
            InitializeComponent();
        }

        private void b_1(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var task = new Microsoft.Phone.Tasks.WebBrowserTask
            {
                URL = "www.facebook.com/pages/LAPspot-Studios/542003805840039?ref=ts&fref=ts"
            };

            task.Show();
        }
    }
}