using System;
using System.Globalization;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace Contract.Import
{
    /// <summary>
    ///     Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            Dispatcher.UnhandledException += OnDispatcherUnhandledException;
            AppDomain.CurrentDomain.UnhandledException += CurrentDomainUnhandledException;

            var ruCulture = new CultureInfo("ru-RU");
            Thread.CurrentThread.CurrentCulture = ruCulture;
            Thread.CurrentThread.CurrentUICulture = ruCulture;

        }

        private static void OnDispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            MessageBox.Show(string.Format("An unhandled exception occurred: {0}", e.Exception.Message), "Error", MessageBoxButton.OK, MessageBoxImage.Error);

            e.Handled = true;
        }

        private static void CurrentDomainUnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            var ex = e.ExceptionObject as Exception;

            if (ex == null)
                return;

            MessageBox.Show(string.Format("An unhandled exception occurred: {0}", ex.Message), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}