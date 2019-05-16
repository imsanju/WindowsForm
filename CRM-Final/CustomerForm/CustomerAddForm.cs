using System;
using System.Windows.Forms;
using CRM_Final.Business.Data;
using CRM_Final.Business.Models;

namespace CRM_Final.CustomerForm
{
    public partial class CustomerAddForm : Form
    {
        public CustomerAddForm()
        {
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            Customer newCustomer = new Customer()
            {
                FirstName = txtFirstName.Text,
                MiddleName = txtMiddleName.Text,
                LastName = txtLastName.Text,
                Suffix = txtSuffix.Text,
                Email = txtEmail.Text,
                Phone = txtPhone.Text,
                CompanyName = txtCompanyName.Text,
                SalesPerson = txtSalesPerson.Text,
                PasswordHash = string.Empty,
                PasswordSalt = string.Empty,
                RowGuid = Guid.NewGuid()
            };


            ICustomerUtility custUtil = DependencyInjector.GetCustomerUtility();

            try
            {
                custUtil.CreateNewCustomer(newCustomer);
                MessageBox.Show("Successfully added customer");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            this.Close();
        }
    }
}
