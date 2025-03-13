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
    public partial class SuppliesForm : Form
    {
        public SuppliesForm()
        {
            InitializeComponent();
        }

        private void SuppliesForm_Load(object sender, EventArgs e)
        {
            RefreshData();
            LoadProductsToCombo();
        }

        private void RefreshData()
        {
            DataTable dt = DatabaseHelper.GetAllSupplies();
            dataGridViewSupplies.DataSource = dt;
        }

        private void LoadProductsToCombo()
        {
            // Загрузим список товаров (id, name)
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
            string supplier = txtSupplier.Text;

            DatabaseHelper.InsertSupply(productId, qty, supplier);
            RefreshData();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dataGridViewSupplies.SelectedRows.Count == 0) return;

            DataGridViewRow row = dataGridViewSupplies.SelectedRows[0];
            int supplyId = Convert.ToInt32(row.Cells["id"].Value);

            DatabaseHelper.DeleteSupply(supplyId);
            RefreshData();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
