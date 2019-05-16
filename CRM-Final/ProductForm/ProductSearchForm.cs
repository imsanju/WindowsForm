using CRM_Final.Business.Data;
using CRM_Final.Business.Models;
using CRM_Final.ProductForm.ViewModels;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using CRM_Final.ProductForm.UserControls;

namespace CRM_Final.ProductForm
{
    public partial class ProductSearchForm : Form
    {
        public ProductSearchForm()
        {
            InitializeComponent();
        }

        private void btnProductSearch_Click(object sender, EventArgs e)
        {
            prodFlowlayout.Controls.Clear();
            IProductInventoryUtility invUtil = DependencyInjector.GetProductInventoryUtility();

            if (string.IsNullOrWhiteSpace(txtProductSearch.Text))
            {
                MessageBox.Show("Product Search value required"); return;
            }

            List<Product> searchResults = invUtil.ProductInventorySearch(txtProductSearch.Text);

            List<ProductSearchViewModel> psvmCollection = new List<ProductSearchViewModel>();

            foreach (Product p in searchResults)
            {
               ProductUserControl puc = new ProductUserControl(p);

               prodFlowlayout.Controls.Add(puc);
            }
          

        }
    }
}
