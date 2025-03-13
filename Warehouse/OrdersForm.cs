using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WarehouseProject;

namespace Warehouse
{
    public partial class OrdersForm : Form
    {
        public OrdersForm()
        {
            InitializeComponent();
        }

        private void OrdersForm_Load(object sender, EventArgs e)
        {
            RefreshData();
            LoadProductsToCombo();
        }

        private void RefreshData()
        {
            DataTable dt = DatabaseHelper.GetAllOrders();
            dataGridViewOrders.DataSource = dt;
        }

        private void LoadProductsToCombo()
        {
            DataTable dt = DatabaseHelper.GetAllProducts();
            comboProducts.DataSource = dt;
            comboProducts.DisplayMember = "name";
            comboProducts.ValueMember = "id";
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (comboProducts.SelectedValue == null) return;

            int productId = (int)comboProducts.SelectedValue;
            int qty = (int)numericQuantity.Value;
            string status = "В процессе";

            DatabaseHelper.InsertOrder(productId, qty, status);
            RefreshData();
        }

        private void btnSetDone_Click(object sender, EventArgs e)
        {
            if (dataGridViewOrders.SelectedRows.Count == 0) return;

            var row = dataGridViewOrders.SelectedRows[0];
            int orderId = Convert.ToInt32(row.Cells["id"].Value);

            DatabaseHelper.UpdateOrder(orderId, "Выполнен");
            RefreshData();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dataGridViewOrders.SelectedRows.Count == 0) return;

            var row = dataGridViewOrders.SelectedRows[0];
            int orderId = Convert.ToInt32(row.Cells["id"].Value);

            DatabaseHelper.DeleteOrder(orderId);
            RefreshData();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}