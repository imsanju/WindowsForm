using CRM_Final.Business.Data;
using CRM_Final.Business.Models;
using CRM_Final.OrderForm.ViewModels;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace CRM_Final.OrderForm
{
    public partial class OrderDetailsForm : Form
    {
        private int _orderId;
        public OrderDetailsForm()
        {
            InitializeComponent();
        }

        public OrderDetailsForm(int orderId) : this()
        {
            this._orderId = orderId;
        }

        private void OrderDetailsForm_Load(object sender, EventArgs e)
        {
            IOrderUtility orderUtility = new DbOrderUtility();
            ICustomerUtility customerUtility = new DbCustomerUtility();
            IOrderItemUtility orderItemUtility = new DbOrderItemUtility();

            Order order = orderUtility.GetById(this._orderId);
            Customer customer = customerUtility.GetById(order.CustomerId);
            List<OrderItem> orderItems = orderItemUtility.GetByOrderId(this._orderId);
            order.OrderItems = orderItems;

            txtOrderNumber.Text = order.OrderNumber;
            txtOrderTotal.Text = Convert.ToString(order.Total);
            txtTax.Text = Convert.ToString(order.Tax);
            txtFreight.Text = Convert.ToString(order.Freight);
            txtOrderDate.Text = order.OrderDate.ToShortDateString();
            txtDueDate.Text = order.DueDate.ToShortDateString();
            txtShipDate.Text = order.ShipDate.ToShortDateString();

            string customerName = customer.FirstName + " " + customer.LastName;
            txtCustomerName.Text = customerName;
            txtCustomerEmail.Text = customer.Email;
            txtCompanyName.Text = customer.CompanyName;

            List<OrderItemViewModel> orderViewModelCollections = new List<OrderItemViewModel>();

            foreach (OrderItem o in orderItems)
            {
                OrderItemViewModel orderItemCollection = new OrderItemViewModel(o);

                orderViewModelCollections.Add(orderItemCollection);
            }

            dgvOrderItems.DataSource = null;
            dgvOrderItems.DataSource = orderViewModelCollections;

        }
    }
}
