using System;
using System.Windows.Forms;

namespace Warehouse
{
    public partial class MainForm : Form
    {
        private readonly string _currentRole;

        public MainForm(string userRole)
        {
            _currentRole = userRole;
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            lblRole.Text = "Ваша роль: " + _currentRole;

            // Примеры ограничений по ролям:
            if (_currentRole == "Кладовщик")
            {
                btnOrders.Enabled = false; // Кладовщику заказы не нужны
            }

            if (_currentRole == "Менеджер")
            {
                btnSupplies.Enabled = false; // Менеджеру не нужны поставки
            }
        }

        private void btnProducts_Click(object sender, EventArgs e)
        {
            using (var f = new ProductsForm())
            {
                f.ShowDialog();
            }
        }

        private void btnSupplies_Click(object sender, EventArgs e)
        {
            using (var f = new SuppliesForm())
            {
                f.ShowDialog();
            }
        }

        private void btnOrders_Click(object sender, EventArgs e)
        {
            using (var f = new OrdersForm())
            {
                f.ShowDialog();
            }
        }

        private void btnReports_Click(object sender, EventArgs e)
        {
            using (var f = new ReportsForm())
            {
                f.ShowDialog();
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
