using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace WpfApp1
{
    public partial class login
    {
        public static string UserLogin { get; set; }
        public static RoutedCommand SignUp = new RoutedCommand();
        public static RoutedCommand SignIn = new RoutedCommand();
        MenuItem userName;

        public login()
        {
            this.CommandBindings.Add(new CommandBinding(SignUp, Registration_Click));
            this.CommandBindings.Add(new CommandBinding(SignIn, BtnSubmit_OnClick));
            InitializeComponent();
        }

            public login(MenuItem userName)
        {

            this.CommandBindings.Add(new CommandBinding(SignUp, Registration_Click));
            this.CommandBindings.Add(new CommandBinding(SignIn, BtnSubmit_OnClick));
            this.userName = userName;
            InitializeComponent();
        }

        private void Registration_Click(object sender, ExecutedRoutedEventArgs e)
        {
            RegistrationWindow registrationWindow = new RegistrationWindow();
            this.Close();
            registrationWindow.Show();
        }


        private void BtnSubmit_OnClick(object sender, ExecutedRoutedEventArgs e)
        {
            using (Editor3DEntities db = new Editor3DEntities())
            {
                if (txtUsername.Text != "" && txtPassword.Password != "")
                {
                    SaltedHash sh = new SaltedHash(txtPassword.Password);
                    Users usr = db.Users.Find(txtUsername.Text);

                    if (usr != null)
                    {
                        bool verify = sh.Verify(usr.Salt, usr.HashedPassword, txtPassword.Password);
                        if (verify == true)
                        {
                            if(userName == null)
                            {
                                MainWindow mainWindow = new MainWindow();
                                UserLogin = usr.Login;
                                mainWindow.DB_Enter_Button.Header = usr.Login;
                                this.Close();
                                mainWindow.Show();
                            }
                            else
                            {
                                UserLogin = usr.Login;
                                userName.Header = usr.Login;
                                this.Close();
                            }
                        }
                        else
                        {
                            MessageBox.Show("Неверно введён логин или пароль", "Ошибка", MessageBoxButton.OK,
                                MessageBoxImage.Error);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Неверно введён логин или пароль", "Ошибка", MessageBoxButton.OK,
                            MessageBoxImage.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Все поля должны быть заполнены", "Ошибка", MessageBoxButton.OK,
                        MessageBoxImage.Error);
                }
            }
        }
    }
}

