using CRM_Final.Business.Data;
using CRM_Final.Business.Models;
using System;
using System.Windows.Forms;

namespace CRM_Final.CustomerForm
{
    public partial class NewAddressForm : Form
    {
        private int _customerId;

        public NewAddressForm()
        {
            InitializeComponent();
        }

        public NewAddressForm(int customerId) : this()
        {
            this._customerId = customerId;
        }

        private void btnSaveAddress_Click(object sender, EventArgs e)
        {
            Address addressToUpdate = new Address()
            {
                Line1 = txtAddressLine1.Text,
                Line2 = txtAddressLine2.Text,
                City = txtCity.Text,
                State = txtState.Text,
                CountryRegion = txtCountry.Text,
                PostalCode = txtZipCode.Text,
                Type = txtAddressType.Text,
                CustomerID = this._customerId
            };

            IAddressUtility addressUtility = DependencyInjector.GetAddressUtility();

            try
            {
                addressUtility.CreateNewAddress(addressToUpdate);
                MessageBox.Show("Successfully added address");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancelNewAddress_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
