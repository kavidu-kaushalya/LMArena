// App.xaml.cs

using LMArena;
using System;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

// Optional: alias to avoid long type name
// using XamlUnhandledExceptionEventArgs = Windows.UI.Xaml.UnhandledExceptionEventArgs;

namespace LMArena
{
    sealed partial class App : Application
    {
        public App()
        {
            this.InitializeComponent();
            this.Suspending += OnSuspending;

            // ✅ XAML UnhandledException (not System)
            this.UnhandledException += App_UnhandledException;
            // or explicitly:
            // this.UnhandledException += new Windows.UI.Xaml.UnhandledExceptionEventHandler(App_UnhandledException);
        }

        protected override void OnLaunched(LaunchActivatedEventArgs e)
        {
            Frame rootFrame = Window.Current.Content as Frame;

            if (rootFrame == null)
            {
                rootFrame = new Frame();
                rootFrame.NavigationFailed += OnNavigationFailed;
                Window.Current.Content = rootFrame;
            }

            if (!e.PrelaunchActivated)
            {
                if (rootFrame.Content == null)
                {
                    rootFrame.Navigate(typeof(MainPage), e.Arguments);
                }
                Window.Current.Activate();
            }
        }

        private void OnNavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            throw new Exception("Failed to load Page " + e.SourcePageType.FullName);
        }

        private void OnSuspending(object sender, SuspendingEventArgs e)
        {
            var deferral = e.SuspendingOperation.GetDeferral();
            deferral.Complete();
        }

        // ✅ Correct signature (Windows.UI.Xaml.UnhandledExceptionEventArgs)
        private void App_UnhandledException(object sender, Windows.UI.Xaml.UnhandledExceptionEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine($"Unhandled XAML exception: {e.Exception}");
            // Optionally prevent crash:
            // e.Handled = true;
        }

        // If you ALSO want AppDomain-level exceptions (separate, optional):
        // AppDomain.CurrentDomain.UnhandledException += (s, e) => { /* System.UnhandledExceptionEventArgs */ };
    }
}