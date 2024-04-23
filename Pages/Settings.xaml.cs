using System.Net.Sockets;
using System.Net;
using Windows.UI.Xaml.Controls;
using System.Text;
using System;
using Windows.UI.Xaml.Navigation;

namespace CardLearnApp.Pages
{
    public sealed partial class Settings : Page, IDisposable
    {
        private TcpClient tcpClient;
        private NetworkStream stream;

        public Settings()
        {
            InitializeComponent();

            Loaded += (x, y) =>
            {
                tcpClient = new TcpClient();
                tcpClient.Connect(new IPEndPoint(IPAddress.Parse("192.168.1.77"), 8888));
                stream = tcpClient.GetStream();
            };
        }

        public void Dispose()
        {
            stream?.Dispose();
            tcpClient?.Dispose();
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);

            stream?.Dispose();
            tcpClient?.Dispose();
        }

        private void Button_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            PingServer();
        }

        private async void PingServer()
        {
            await stream.WriteAsync(Encoding.UTF8.GetBytes("ping"), 0, 4);

            byte[] message = new byte[32];
            await stream.ReadAsync(message, 0, 32);

            PingStatus.Text = $"Server sent message: {Encoding.UTF8.GetString(message)}";
        }
    }
}