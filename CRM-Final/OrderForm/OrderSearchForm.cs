using System;
using System.Collections.Generic;
using System.Windows.Forms;
using CRM_Final.Business.Data;
using CRM_Final.Business.Models;
using CRM_Final.OrderForm.ViewModels;

namespace CRM_Final.OrderForm
{
    public partial class OrderSearchForm : Form
    {
        public OrderSearchForm()
        {
            InitializeComponent();
        }

        private void btnOrderSearch_Click(object sender, EventArgs e)
        {
            IOrderUtility orderUtility = DependencyInjector.GetOrderUtility();

            if (string.IsNullOrWhiteSpace(txtOrderSearch.Text)) { MessageBox.Show("Order Search value required"); return; }

            List<Order> searchResults = orderUtility.OrderSearch(txtOrderSearch.Text);

            List<OrderSearchViewModel> csvmCollection = new List<OrderSearchViewModel>();

            foreach (Order c in searchResults)
            {
                OrderSearchViewModel osvm = new OrderSearchViewModel(c);

                csvmCollection.Add(osvm);
            }

            dgvOrders.DataSource = null;
            dgvOrders.DataSource = csvmCollection;
        }

        private void dgvOrders_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            OrderSearchViewModel orderVM = null;
            if (dgvOrders.SelectedRows.Count > 0)
            {
                orderVM = (OrderSearchViewModel)dgvOrders.SelectedRows[0].DataBoundItem;
            }

            if (orderVM == null)
            {
                return;
            }

            OrderDetailsForm orderDetailsForm = new OrderDetailsForm(orderVM.SalesOrderID);
            orderDetailsForm.ShowDialog();
        }

        private void OrderSearchForm_Load(object sender, EventArgs e)
        {
            IOrderUtility orderUtility = DependencyInjector.GetOrderUtility();
            List<Order> orders = orderUtility.GetList();

            List<OrderSearchViewModel> orderViewModelCollections = new List<OrderSearchViewModel>();

            foreach (Order o in orders)
            {
                OrderSearchViewModel osvm = new OrderSearchViewModel(o);

                orderViewModelCollections.Add(osvm);
            }

            dgvOrders.DataSource = null;
            dgvOrders.DataSource = orderViewModelCollections;
        }
    }
}
