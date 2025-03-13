using System;
using System.Windows.Forms;
using WarehouseProject;


namespace Warehouse
{
    public partial class LoginForm : Form
    {
        // Хранит роль текущего пользователя после успешного входа
        public string CurrentUserRole { get; private set; } = "";

        public LoginForm()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string login = txtLogin.Text;
            string pass = txtPassword.Text;

            // Проверка в БД (CheckUserCredentials возвращает (bool success, string roleName))
            var (success, roleName) = DatabaseHelper.CheckUserCredentials(login, pass);

            if (!success)
            {
                // Если пользователь не найден или пароль неверный
                MessageBox.Show("Неверный логин или пароль!");
                return;
            }

            // Если всё ОК, сохраняем роль в свойство LoginForm
            CurrentUserRole = roleName;

            // КЛЮЧЕВОЙ МОМЕНТ:
            // устанавливаем DialogResult = OK
            this.DialogResult = DialogResult.OK;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            // Выходим из приложения
            Application.Exit();
        }
    }
}
