﻿#pragma checksum "M:\new\PhoneApp2\PhoneApp2\Start.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "A3D80E1E4DDB8DB307DD45B5E9F51E4F"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.17929
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Microsoft.Phone.Controls;
using System;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Resources;
using System.Windows.Shapes;
using System.Windows.Threading;


namespace PhoneApp2 {
    
    
    public partial class Page2 : Microsoft.Phone.Controls.PhoneApplicationPage {
        
        internal System.Windows.Controls.Grid LayoutRoot;
        
        internal System.Windows.Controls.Grid ContentPanel;
        
        internal System.Windows.Controls.Image img;
        
        internal System.Windows.Controls.Canvas can;
        
        internal System.Windows.Controls.Image single;
        
        internal System.Windows.Controls.Image options;
        
        internal System.Windows.Controls.Image multi;
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Windows.Application.LoadComponent(this, new System.Uri("/PhoneApp2;component/Start.xaml", System.UriKind.Relative));
            this.LayoutRoot = ((System.Windows.Controls.Grid)(this.FindName("LayoutRoot")));
            this.ContentPanel = ((System.Windows.Controls.Grid)(this.FindName("ContentPanel")));
            this.img = ((System.Windows.Controls.Image)(this.FindName("img")));
            this.can = ((System.Windows.Controls.Canvas)(this.FindName("can")));
            this.single = ((System.Windows.Controls.Image)(this.FindName("single")));
            this.options = ((System.Windows.Controls.Image)(this.FindName("options")));
            this.multi = ((System.Windows.Controls.Image)(this.FindName("multi")));
        }
    }
}
