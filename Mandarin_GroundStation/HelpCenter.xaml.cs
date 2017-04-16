//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading;
using System;
using System.Threading.Tasks;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using CefSharp.Wpf;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Media;

namespace Mandarin_GroundStation
{
    /// <summary>
    /// HelpCenter.xaml 的交互逻辑
    /// </summary>
    public partial class HelpCenter : UserControl
    {
        public HelpCenter(string dictionarypath)
        {
            InitializeComponent();
        }
        private void HelpCenterBrowser_LayoutUpdated(object sender, EventArgs e)
        {
            BrowserProgressRing.IsActive = HelpCenterBrowser.IsLoading;
        }
        private void HelpCenterHome_Click(object sender, RoutedEventArgs e)
        {
            HelpCenterBrowser.WebBrowser.Load("http://github.com");
        }
    }
}
