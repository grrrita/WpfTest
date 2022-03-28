using System;
using System.Linq;
using System.Windows;

namespace WpfTest
{
    public partial class UserWindow : Window
    {
        public UserWindow()
        {
            InitializeComponent();
        }
        private void AddUser(User Adduser)
        {
            using (myUsersContext db = new myUsersContext())
            {
                // Добавление
                db.Users.Add(Adduser);
                db.SaveChanges();
            }
        }
        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            if (tbName.Text.Length < 1)
            {
                MessageBox.Show("Введите имя");
                return;
            }
            if (tbAge.Text.Length < 1)
            {
                MessageBox.Show("Введите возраст");
                return;
            }
            string name = tbName.Text;
            int age = Convert.ToInt32(tbAge.Text);
            User Adduser = new User { Name = name, Age = age };
            AddUser(Adduser);
            this.Hide();
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
        }
    }
}