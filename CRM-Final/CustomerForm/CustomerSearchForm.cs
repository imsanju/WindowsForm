using CRM_Final.Business.Data;
using CRM_Final.Business.Models;
using CRM_Final.CustomerForm.ViewModels;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace CRM_Final.CustomerForm
{
    public partial class CustomerSearchForm : Form
    {
        public CustomerSearchForm()
        {
            InitializeComponent();
        }

        private void btnCustomerSearch_Click(object sender, EventArgs e)
        {
            ICustomerUtility invUtil = DependencyInjector.GetCustomerUtility();

            if (string.IsNullOrWhiteSpace(txtCustomerSearch.Text)) { MessageBox.Show("Customer Search value required"); return; }

            List<Customer> searchResults = invUtil.CustomerSearch(txtCustomerSearch.Text);

            List<CustomerSearchViewModel> csvmCollection = new List<CustomerSearchViewModel>();

            foreach (Customer c in searchResults)
            {
                CustomerSearchViewModel csvm = new CustomerSearchViewModel(c);

                csvmCollection.Add(csvm);
            }

            dgvCustomers.DataSource = null;
            dgvCustomers.DataSource = csvmCollection;
        }

        private void dgvCustomers_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            {
                CustomerSearchViewModel customerVM = null;
                if (dgvCustomers.SelectedRows.Count > 0)
                {
                    customerVM = (CustomerSearchViewModel)dgvCustomers.SelectedRows[0].DataBoundItem;
                }

                if (customerVM == null)
                {
                    return;
                }

                CustomerUpdateForm prodUpdateForm = new CustomerUpdateForm(customerVM.CustomerID);
                prodUpdateForm.ShowDialog();

                //refresh
                btnCustomerSearch_Click(sender, e);
            }
        }
    }
}
