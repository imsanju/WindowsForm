using CRM_Final.Business.Data;
using CRM_Final.Business.Models;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace CRM_Final.OrderForm
{
    public partial class AddOrderItemForm : Form
    {
        public AddOrderItemForm()
        {
            InitializeComponent();
        }

        private void AddOrderItemForm_Load(object sender, EventArgs e)
        {
            IProductInventoryUtility inventoryUtility = new DbProductInventoryUtility();
            List<Product> products = inventoryUtility.GetInventory();

            cmbProduct.DataSource = products;

            UpdateProductInfo();
        }

        private void cmbProduct_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateProductInfo();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void UpdateProductInfo()
        {
            Product selectedProduct = (Product)cmbProduct.SelectedItem;
            lblPPrice.Text = string.Format("{0:c}", selectedProduct.ListPrice);

            txtQuantity.Text = "1";
            txtDiscount.Text = "0";
        }

        private void btnAddItem_Click(object sender, EventArgs e)
        {
            Product product = (Product)cmbProduct.SelectedItem;
            int productID = product.ProductId;

            OrderItem orderItem = new OrderItem()
            {
                Product = (Product)cmbProduct.SelectedItem,
                ProductID = productID,
                Quantity = Convert.ToInt32(txtQuantity.Text),
                Discount = Convert.ToDecimal(txtDiscount.Text)
            };

            this.newOrderItem = orderItem;

            this.DialogResult = DialogResult.OK;

            this.Close();
        }

        public OrderItem newOrderItem { get; set; }

    }
}
