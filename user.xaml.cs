using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    /// Логика взаимодействия для user.xaml
    /// </summary>
    public partial class user : UserControl
    {
        public user()
        {
            InitializeComponent();
        }
        //чет для добавления    
        public event EventHandler UsersChanged;

       public void RaiseUsersChangedEvent()
        {
            UsersChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
