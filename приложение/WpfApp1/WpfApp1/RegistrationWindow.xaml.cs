using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace WpfApp1
{
    public partial class RegistrationWindow
    {
        public static RoutedCommand Register = new RoutedCommand();
        public static RoutedCommand Back = new RoutedCommand();

        public RegistrationWindow()
        {
            InitializeComponent();
            this.CommandBindings.Add(new CommandBinding(Register, RegisterCommand));
            this.CommandBindings.Add(new CommandBinding(Back, BackArrow));
        }

        private void RegisterCommand(object sender, ExecutedRoutedEventArgs e)
        {
            if (txtUserPassword.Password == txtUserRePassword.Password)
            {
                if (txtUsername.Text != "" && txtUserLogin.Text != "" && txtSecretWord.Text != "" &&
                    txtUserRePassword.Password != "" &&
                    txtUserSurname.Text != "" && txtUserSurname.Text != "" && txtUserRePassword.Password != "")
                {
                    using (Editor3DEntities db = new Editor3DEntities())
                    {
                        using (var transaction = db.Database.BeginTransaction())
                            try
                            {
                                SaltedHash sh = new SaltedHash(txtUserPassword.Password);
                                login nw = new login();
                                UserInfo uInfo = new UserInfo();
                                Users users = new Users();
                                uInfo.Name = txtUsername.Text;
                                uInfo.Surname = txtUserSurname.Text;
                                uInfo.LastName = txtUserMiddleName.Text;
                                uInfo.SecretWord = txtSecretWord.Text;
                                uInfo.CreationTime = DateTime.Now;
                                users.Login = txtUserLogin.Text;
                                users.HashedPassword = sh.Hash;
                                users.Salt = sh.Salt;
                                db.Users.Add(users);
                                uInfo.UserLogin = users.Login;
                                db.UserInfo.Add(uInfo);
                                db.SaveChanges();
                                transaction.Commit();
                                MessageBox.Show("Пользователь зарегистрирован", "Ok!", MessageBoxButton.OK,
                                    MessageBoxImage.Asterisk);
                                this.Close();
                                nw.Show();
                            }
                            catch (Exception ex)
                            {
                                transaction.Rollback();
                                MessageBox.Show("Учётная запись не была создана", "Ошибка!", MessageBoxButton.OK,
                                    MessageBoxImage.Error);
                            }
                    }
                }
                else MessageBox.Show("Не все поля заполнены!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Asterisk);
            }
            else MessageBox.Show("Пароли не совпадают!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Asterisk);
        }

        private void BackArrow(object sender, ExecutedRoutedEventArgs e)
        {
            login loginScreen = new login();
            loginScreen.Show();
            this.Close();
        }
    }
}
