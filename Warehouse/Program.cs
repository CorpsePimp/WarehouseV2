using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Warehouse
{
    internal static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            using (var loginForm = new LoginForm())
            {
                // Показываем форму логина
                var result = loginForm.ShowDialog();

                // Если форма логина закрылась с DialogResult.OK:
                if (result == DialogResult.OK)
                {
                    // Берём роль, сохранённую в loginForm.CurrentUserRole
                    string userRole = loginForm.CurrentUserRole;

                    // Запускаем MainForm
                    Application.Run(new MainForm(userRole));
                }
                else
                {
                    // Иначе выходим (пользователь нажал «Отмена» или закрыл форму)
                    Application.Exit();
                }
            }
        }
    }
}
