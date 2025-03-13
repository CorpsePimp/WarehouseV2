using System;
using System.Data;
using System.Windows.Forms;
using WarehouseProject;

namespace Warehouse
{
    public partial class ProductsForm : Form
    {
        public ProductsForm()
        {
            InitializeComponent();
        }

        private void ProductsForm_Load(object sender, EventArgs e)
        {
            RefreshData();
        }

        private void RefreshData()
        {
            DataTable dt = DatabaseHelper.GetAllProducts();
            dataGridViewProducts.DataSource = dt;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            // Пример: жёстко зашиваем значения
            string newName = "Новый товар";
            int newQty = 10;

            DatabaseHelper.InsertProduct(newName, newQty);
            RefreshData();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dataGridViewProducts.SelectedRows.Count == 0) return;

            DataGridViewRow row = dataGridViewProducts.SelectedRows[0];
            int id = Convert.ToInt32(row.Cells["id"].Value);
            string oldName = row.Cells["name"].Value.ToString();
            int oldQty = Convert.ToInt32(row.Cells["quantity"].Value);

            // Условно меняем
            string newName = oldName + " (ред.)";
            int newQty = oldQty + 5;

            DatabaseHelper.UpdateProduct(id, newName, newQty);
            RefreshData();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dataGridViewProducts.SelectedRows.Count == 0) return;

            DataGridViewRow row = dataGridViewProducts.SelectedRows[0];
            int id = Convert.ToInt32(row.Cells["id"].Value);

            DatabaseHelper.DeleteProduct(id);
            RefreshData();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
