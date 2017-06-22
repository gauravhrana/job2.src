using Framework.Components.DataAccess;
using System;
using System.Windows;

namespace PrismSample
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            SetupConfiguration.SetConnectionList(100);
            SetupConfiguration.UserMachineName = Environment.MachineName;

            Bootstrapper bs = new Bootstrapper();
            bs.Run();
        }
    }
}
