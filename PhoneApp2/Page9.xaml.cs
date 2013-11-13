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
namespace PhoneApp2
{
    public partial class Page9 : PhoneApplicationPage
    {
        public Page9()
        {
            InitializeComponent();
            te_1.MaxLength = cons.nameMaxLen;
            te_2.MaxLength = cons.nameMaxLen;
            
        }

        private void next_click(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            getName(te_1.Text,1);
            getName(te_2.Text,2);
            NavigationService.Navigate(new Uri("/Page5.xaml", UriKind.Relative));
        }
        private void keyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                /*s = ((TextBox)sender).Text;
                if(((TextBox)sender)==te_1)
                {
                    getName(s,1);
                }
                else
                {
                    getName(s, 2);
                }*/
                this.Focus();
            }
        }
        private void getName(string s,int p)
        {
            s.Trim();
            if (s.Length == 0)
                s = "Player_" + p;
            cons.pl_name[p - 1] = s;
        }
    }
}