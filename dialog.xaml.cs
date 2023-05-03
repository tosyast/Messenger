using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace kazuscara
{
    /// <summary>
    /// Логика взаимодействия для dialog.xaml
    /// </summary>
    public partial class dialog : Window

    {
        private Socket socket;
       
        private List<Socket> clients = new List<Socket>();

        public dialog()
        {
            InitializeComponent();
            IPEndPoint ipPont = new IPEndPoint(IPAddress.Any, 8888);
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            socket.Bind(ipPont);
            socket.Listen(1000);
            ListenToClients();
           

        }
       
       


        private async Task ListenToClients()
        {
            while (true)
            {
                Socket client = await socket.AcceptAsync();
                clients.Add(client);
                ReciveMessage(client);
            }
        }


        private async void ReciveMessage(Socket client)
        {
            while (true)
            {
                byte[] bytes = new byte[1024];
                await client.ReceiveAsync(bytes, SocketFlags.None);
                string message = Encoding.UTF8.GetString(bytes);

                messagelist.Items.Add($"[Сообщение от {client.RemoteEndPoint}]: {message}");

                foreach (var item in clients)
                {
                    SendMessage(item, message);
                }
            }
        }



        private async Task SendMessage(Socket client, string message)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(message);
            await client.SendAsync(bytes, SocketFlags.None);
        }


        private void exit_Click(object sender, RoutedEventArgs e)
        {
            MainWindow window = new MainWindow();
            window.Show();
            this.Close();
        }

        private void chatting_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                string input = ((TextBox)sender).Text;
     
                
                ((TextBox)sender).Clear();

            }
        }

        private void enter_Click(object sender, RoutedEventArgs e)
        {
            string input = ((TextBox)sender).Text;
     
            ((TextBox)sender).Clear();

        }
    }
}
