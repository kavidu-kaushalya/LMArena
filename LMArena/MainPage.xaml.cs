using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Microsoft.Web.WebView2.Core; // for CoreWebView2 types if needed

namespace LMArena
{
    public sealed partial class MainPage : Page
    {
        const string Url = "https://lmarena.ai/?mode=direct";

        public MainPage()
        {
            this.InitializeComponent();
            this.Loaded += MainPage_Loaded;
        }

        private async void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            await Web.EnsureCoreWebView2Async();

            Web.CoreWebView2.Settings.AreDevToolsEnabled = true;
            Web.CoreWebView2.Settings.AreDefaultContextMenusEnabled = true;

            Web.CoreWebView2.NewWindowRequested += (s, args) =>
            {
                args.Handled = true;
                Web.CoreWebView2.Navigate(args.Uri);
            };

            Web.Source = new Uri(Url);
        }
    }
}