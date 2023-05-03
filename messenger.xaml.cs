using System;
using System.Collections.Generic;
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
using static System.Net.Mime.MediaTypeNames;

namespace kazuscara
{
    /// <summary>
    /// Логика взаимодействия для messenger.xaml
    /// </summary>
    public partial class messenger : Window
    {
        private Socket server;
        public messenger(string ipText)
        {
            InitializeComponent();
            server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            server.ConnectAsync(ipText, 8888);
            RecieveMessage();

        }
        private async Task RecieveMessage()
        {
            while (true)
            {
                byte[] bytes = new byte[1024];
                await server.ReceiveAsync(bytes, SocketFlags.None);
                string message = Encoding.UTF8.GetString(bytes);

                messagelist.Items.Add(message);
            }
        }

        private async Task SendMessage(string message)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(message);
            await server.SendAsync(bytes, SocketFlags.None);
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

                SendMessage(chatting.Text);
                ((TextBox)sender).Clear();

            }
        }

        private void send_Click_1(object sender, RoutedEventArgs e)
        {
            string input = ((TextBox)sender).Text;

            SendMessage(chatting.Text);
            ((TextBox)sender).Clear();

        }
        private void AddUserToWindow(messenger window, string username)
        {
            user newUser = new user();
            newUser.name.Text = username;
            window.userlist.Items.Add(newUser);
        }
        private void UpdateUsersInWindow(messenger window, List<string> usernames)
        {
            window.userlist.Items.Clear();
            foreach (string username in usernames)
            {
                AddUserToWindow(window, username);
            }
        }

    }
}
