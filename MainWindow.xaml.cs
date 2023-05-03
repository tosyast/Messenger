using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace kazuscara
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<user> users = new List<user>();
        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
         
        }
       
        private void bxod_Click(object sender, RoutedEventArgs e)
        {
            dialog window= new dialog();
            window.Show();
            this.Close();
        }
        public static bool IsValidIPAddress(string ipAddress)
        {
            if (string.IsNullOrWhiteSpace(ipAddress))
                return false;

            Regex regex = new Regex(@"^(([01]?\d?\d|2[0-4]\d|25[0-5])\.){3}([01]?\d?\d|2[0-4]\d|25[0-5])$");
            return regex.IsMatch(ipAddress);
        }
       

        private void ip_LostFocus(object sender, RoutedEventArgs e)
        {
            string ipAddress = ((TextBox)sender).Text;
            bool isValid = IsValidIPAddress(ipAddress);
            if (!isValid)
            {
                MessageBox.Show("Неправильно введен IP");
            }
        }

        private void name_LostFocus(object sender, RoutedEventArgs e)
        {
            string input = ((TextBox)sender).Text;
            if (!Regex.IsMatch(input, @"^[a-zA-Zа-яА-Я]{1,15}$"))
            {
                MessageBox.Show("Вы ввели неккоректно имя");
            }
        }


       

        private void join_Click(object sender, RoutedEventArgs e)
        {
            string IP = ip.Text;
            string nae = name.Text;
            messenger window = new messenger(IP);
            window.Show();

            // Добавляем всех существующих пользователей в список
            foreach (var item in window.userlist.Items)
            {
                if (item is user existingUser)
                {
                    users.Add(existingUser);
                }
            }

            // Создаем нового пользователя и добавляем его в список
            user us = new user();
            us.name.Text = nae;
            window.userlist.Items.Add(us);
            users.Add(us);

            // Обновляем список пользователей в окне
            UpdateUsersInWindow(window, users.Select(u => u.name.Text).ToList());
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


        private void ip_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox ipTexBob = (TextBox)sender;
        }
    }
}
