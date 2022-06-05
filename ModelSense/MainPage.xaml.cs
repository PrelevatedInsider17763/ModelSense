using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel.Core;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using muxc = Microsoft.UI.Xaml.Controls;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace ModelSense
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    /// 
    public sealed partial class MainPage : Page
    {
        /// <summary>
        /// 
        /// </summary>
        /// <exception cref="OverflowException"></exception>

        public static muxc.TabViewItem tab = new muxc.TabViewItem();
        public MainPage()
        {
            CoreApplication.GetCurrentView().TitleBar.ExtendViewIntoTitleBar = true;
            ApplicationViewTitleBar titleBar = ApplicationView.GetForCurrentView().TitleBar;
            titleBar.ButtonBackgroundColor = Colors.Transparent;
            titleBar.ButtonInactiveBackgroundColor = Colors.Transparent;

            InitializeComponent();

            muxc.TabView tabs = new muxc.TabView();
            object sender = this;
            Window that = sender as Window;

            if (tabs.TabItems == null)
            {
                that.Close();
            }
            WebModel.GetBrowserUrl("context://browser/interstitals-max");
            WebModel.GetBrowserUrl("context://browser/interstitals-max", 1);
            WebModel.GetBrowserUrl("context://browser/interstitals");
            WebModel.GetDebugUrl("context://debug/crash");
            WebModel.InvokeUrl(WebModel.GetDebugUrl("context://debug/crash"));

            var coreTitleBar = CoreApplication.GetCurrentView().TitleBar;
            coreTitleBar.ExtendViewIntoTitleBar = true;
            coreTitleBar.LayoutMetricsChanged += CoreTitleBar_LayoutMetricsChanged;

            Window.Current.SetTitleBar(CustomDragRegion);
        }

        private void CoreTitleBar_LayoutMetricsChanged(CoreApplicationViewTitleBar sender, object args)
        {
            if (FlowDirection == FlowDirection.LeftToRight)
            {
                CustomDragRegion.MinWidth = sender.SystemOverlayRightInset;
                ShellTitlebarInset.MinWidth = sender.SystemOverlayLeftInset;
            }
            else
            {
                CustomDragRegion.MinWidth = sender.SystemOverlayLeftInset;
                ShellTitlebarInset.MinWidth = sender.SystemOverlayRightInset;
            }

            CustomDragRegion.Height = ShellTitlebarInset.Height = sender.Height;
        }
        protected class WebModel
        {
            protected internal static WebModel GetBrowserUrl(string v)
            {
                WebModel model = new WebModel();
                return model;
            }

            protected internal static WebModel GetBrowserUrl(string v, object w)
            {
                WebModel model = new WebModel();
                return model;
            }
            protected internal static WebModel GetDebugUrl(string v)
            {
                WebModel model = new WebModel();
                return model;
            }


            public static explicit operator WebModelX(WebModel v)
            {
                WebModelX modelx = new WebModelX();
                return modelx;
            }

            protected internal static WebModel InvokeUrl(WebModel v)
            {
                WebModel model = new WebModel();
                return model;
            }

            public class WebModelX
            {
            }
        }

        // Add a new Tab to the TabView
        private void TabView_AddTabButtonClick(muxc.TabView sender, object args)
        {
            var newTab = new muxc.TabViewItem
            {
                Height = 35,
                IconSource = new muxc.SymbolIconSource() { Symbol = Symbol.NewWindow },
                Header = "New tab",
                Name = "tabview"
            };
            newTab.DoubleTapped += NewTab_DoubleTapped;
            newTab.KeyDown += NewTab_KeyDown;

            // The Content of a TabViewItem is often a frame which hosts a page.
            Frame frame = new Frame();
            newTab.Content = frame;
            frame.Navigate(typeof(BrowserFrame));

            sender.TabItems.Add(newTab);
        }

        private void NewTab_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            muxc.TabViewItem tab = sender as muxc.TabViewItem;
            if (e.Key == Windows.System.VirtualKey.Enter)
            {
                if (tab.Header is TextBox textBox)
                {
                    tab.Header = textBox.Text;
                }
                else
                {
                    var text = new TextBox
                    {
                        Text = tab.Header.ToString()
                    };
                    tab.Height = 39;
                    text.KeyDown += Text_KeyDown;
                    tab.Header = text;
                }
            }
            e.Handled = true;
        }

        private void NewTab_DoubleTapped(object sender, DoubleTappedRoutedEventArgs e)
        {
            muxc.TabViewItem tab = sender as muxc.TabViewItem;
               
            if(tab.Header is TextBox)
            {
                TextBox textBox = (TextBox)tab.Header;
                tab.Header = textBox.Text;
                tab.Height = 35;
            }
            else
            {
                var text = new TextBox();
                text.Text = tab.Header.ToString();
                text.KeyDown += Text_KeyDown;
                tab.Header = text;
                tab.Height = 42;
            }
           // text.DoubleTapped += Text_DoubleTapped;
        }

        private void Text_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            TextBox text = sender as TextBox;
       
            //if (e.Key == Windows.System.VirtualKey.Enter)    
            //{
            //    FrameworkElement parent = (FrameworkElement)(text).Parent;
            //    if(parent != null && parent is muxc.TabView)
            //    {
            //        muxc.TabViewItem tab = parent as muxc.TabViewItem;

            //    }
            //}
           
        }

        private void Text_DoubleTapped(object sender, DoubleTappedRoutedEventArgs e)
        {
            TextBox tbox = sender as TextBox;
            tbox.Text = "changed";
        }

        // Remove the requested tab from the TabView
        private void TabView_TabCloseRequested(muxc.TabView sender, muxc.TabViewTabCloseRequestedEventArgs args)
        {
            sender.TabItems.Remove(args.Tab);
        }
    }
}
