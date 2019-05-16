using CRM_Final.Business.Data;
using CRM_Final.Business.Helpers;
using CRM_Final.Business.Models;
using CRM_Final.OrderForm;
using CRM_Final.OrderForm.ViewModels;
using Microsoft.Maps.MapControl.WPF;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace CRM_Final.CustomerForm
{
    public partial class CustomerUpdateForm : Form
    {
        private int customerId;
        private Customer _customer;

        public CustomerUpdateForm()
        {
            InitializeComponent();

            InitializeMap(mapUserControl1);
        }

        public CustomerUpdateForm(int customerId) : this()
        {
            this.customerId = customerId;

            InitializeMap(mapUserControl1);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Customer customerToUpdate = new Customer()
            {
                CustomerID = this.customerId,
                FirstName = txtFirstName.Text,
                MiddleName = txtMiddleName.Text,
                LastName = txtLastName.Text,
                Suffix = txtSuffix.Text,
                Email = txtEmail.Text,
                Phone = txtPhone.Text,
                CompanyName = txtCompanyName.Text,
                SalesPerson = txtSalesPerson.Text.Replace(@"\", "/")
        };

            try
            {
                ICustomerUtility customerUtility = DependencyInjector.GetCustomerUtility();
                customerUtility.UpdateCustomer(customerToUpdate);
                MessageBox.Show("Successfully updated customer information");
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            if (this._customer.Addresses.Count > 0)
            {
               Address addressToUpdate = new Address()
                {
                    AddressID = this._customer.Addresses[cmbAddressType.SelectedIndex].AddressID,
                    Line1 = txtAddressLine1.Text,
                    Line2 = txtAddressLine2.Text,
                    City = txtCity.Text,
                    State = txtState.Text,
                    CountryRegion = txtCountry.Text,
                    PostalCode = txtPostalCode.Text,
                    Type = this._customer.Addresses[cmbAddressType.SelectedIndex].Type
               };

                IAddressUtility addressUtility = DependencyInjector.GetAddressUtility();

                try
                {       
                    addressUtility.UpdateAddress(addressToUpdate);
                    MessageBox.Show("Successfully Updated Custome Address");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }   
            this.Close();
        }

        private void InitializeMap(MapUserControl muc)
        {
            muc.Map.CredentialsProvider = new ApplicationIdCredentialsProvider("AlpNZq6Dq5_7gFOinuqRbe4DNRB9foCCBotHO0gz1Wn1wbyjwP95SrSp4rIxFels");
            muc.Map.Center = new Location(39.50, -98.35);
            muc.Map.ZoomLevel = 3.8;
        }

        private void CustomerUpdateForm_Load(object sender, EventArgs e)
        {
            ICustomerUtility customerUtility = DependencyInjector.GetCustomerUtility();

            _customer = customerUtility.GetById(this.customerId);

            txtFirstName.Text = _customer.FirstName;
            txtLastName.Text = _customer.LastName;
            txtMiddleName.Text = _customer.MiddleName;
            txtSuffix.Text = _customer.Suffix;
            txtEmail.Text = _customer.Email;
            txtPhone.Text = _customer.Phone;
            txtSalesPerson.Text = _customer.SalesPerson;
            txtCompanyName.Text = _customer.CompanyName;

            try
            {
                UpdateCustomerAddresses();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            try
            {
                UpdateCustomerOrders();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void cmbAddressType_SelectedIndexChanged(object sender, EventArgs e)
        {
            Address selectedAddress = (Address)cmbAddressType.SelectedItem;
            txtAddressLine1.Text = selectedAddress.Line1;
            txtAddressLine2.Text = selectedAddress.Line2;
            txtCity.Text = selectedAddress.City;
            txtState.Text = selectedAddress.State;
            txtCountry.Text = selectedAddress.CountryRegion;
            txtPostalCode.Text = selectedAddress.PostalCode;

            // Geocode the address
            LatLong geocodes = GeocodeHelper.Geocode(selectedAddress);

            mapUserControl1.Map.Center = new Location(geocodes.Latitude, geocodes.Longitude);
            mapUserControl1.Map.ZoomLevel = 15;
        }

        private void dgvCustomerOrders_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            OrderSearchViewModel orderVM = null;
            if (dgvCustomerOrders.SelectedRows.Count > 0)
            {
                orderVM = (OrderSearchViewModel)dgvCustomerOrders.SelectedRows[0].DataBoundItem;
            }

            if (orderVM == null)
            {
                return;
            }

            OrderDetailsForm orderDetailsForm = new OrderDetailsForm(orderVM.SalesOrderID);
            orderDetailsForm.ShowDialog();
        }

        private void btnAddAddressForm_Click(object sender, EventArgs e)
        {
            NewAddressForm newAddressForm = new NewAddressForm(this.customerId);
            if (newAddressForm.ShowDialog() == DialogResult.OK) {
                UpdateCustomerAddresses();
                MessageBox.Show("Successfully added Customer Address");
            }
            this.Close();
        }

        private void btnStartNewOrder_Click(object sender, EventArgs e)
        {
            OrderAddForm newOrderForm = new OrderAddForm(this.customerId);
            if (newOrderForm.ShowDialog() == DialogResult.OK)
            {
                // update Grid
                UpdateCustomerOrders();
            }
        }

        private void UpdateCustomerAddresses()
        {
            IAddressUtility addressUtility = DependencyInjector.GetAddressUtility();
            List<Address> customerAddresses = addressUtility.GetForCustomer(this.customerId);
            _customer.Addresses = customerAddresses;
            cmbAddressType.DataSource = _customer.Addresses;
        }

        private void UpdateCustomerOrders()
        {
            IOrderUtility orderUtility = DependencyInjector.GetOrderUtility();
            List<Order> customerOrders = orderUtility.GetByCustomerId(this.customerId);
            _customer.ExistingOrders = customerOrders;

            List<OrderSearchViewModel> ordersViewModelCollection = new List<OrderSearchViewModel>();

            foreach (Order o in _customer.ExistingOrders)
            {
                OrderSearchViewModel orderVM = new OrderSearchViewModel(o);

                ordersViewModelCollection.Add(orderVM);
            }

            dgvCustomerOrders.DataSource = null;
            dgvCustomerOrders.DataSource = ordersViewModelCollection;

        }

        private void btnCustomerDelete_Click(object sender, EventArgs e)
        {
            
           ICustomerUtility customerUtilityUtil = DependencyInjector.GetCustomerUtility();

            IAddressUtility addressUtility = DependencyInjector.GetAddressUtility();
            Customer customerToDelete = new Customer()
              {
                CustomerID = this.customerId
              };

            
              try
                {
                //TO-DO delete associated address when deleting customer
                    customerUtilityUtil.DeleteCustomer(customerToDelete);
                    MessageBox.Show("Successfully delete Customer! ");
                }
              catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            
              this.Close();
            
        }
    }
}
