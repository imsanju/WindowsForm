using System;
using System.Windows.Forms;
using CRM_Final.Business.Data;
using CRM_Final.Business.Models;

namespace CRM_Final.OrderForm
{
    public partial class OrderAddForm : Form
    {
        private Order _order;
        private decimal TAX_RATE = 0.05m;
        private decimal FREIGHT_RATE = 0.06m;
        private int customerId;

        public OrderAddForm()
        {
            InitializeComponent();

            _order = new Order();
        }

        public OrderAddForm(int customerId) : this()
        {
            this.customerId = customerId;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            this._order.CustomerId = this.customerId;
            this._order.OrderDate = dtpOrderDate.Value.Date;
            this._order.DueDate = dtpDueDate.Value.Date;
            this._order.ShipDate = dtpShipDate.Value.Date;
            this._order.Status = 1; // from the Column description, 1=in process
            this._order.ShipMethod = txtShipMethod.Text;
            this._order.Tax = Convert.ToDecimal(txtTax.Text);
            this._order.Freight = Convert.ToDecimal(txtFreight.Text);
            this._order.Total = Convert.ToDecimal(txtTotal.Text);

            IOrderUtility orderUtil = DependencyInjector.GetOrderUtility();
            IOrderItemUtility orderItemUtil = DependencyInjector.GetOrderItemUtility();

            try
            {
                Order createdOrder = orderUtil.CreateNewOrder(this._order);

                foreach (OrderItem item in this._order.OrderItems)
                {
                    item.SalesOrderID = createdOrder.SalesOrderID;
                    orderItemUtil.CreateNewOrderItem(item);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            this.DialogResult = DialogResult.OK;
            this.Close();
        
    }

        private void btnAddLineItem_Click(object sender, EventArgs e)
        {
            AddOrderItemForm addOrderItemForm = new AddOrderItemForm();
            if (addOrderItemForm.ShowDialog() == DialogResult.OK) {
                this._order.OrderItems.Add(addOrderItemForm.newOrderItem);

                UpdateOrder();
                MessageBox.Show("Successfuly update order line item");
            }
        }

        private void UpdateOrder()
        {
            lstOrderItems.DataSource = null;
            lstOrderItems.DataSource = this._order.OrderItems;

            CalculateTax(TAX_RATE);
            CalculateFreight(FREIGHT_RATE);
            CalculateTotal();

            txtSubtotal.Text = Convert.ToString(_order.Subtotal);
            txtTax.Text = Convert.ToString(this._order.Tax);
            txtFreight.Text = Convert.ToString(this._order.Freight);
            txtTotal.Text = Convert.ToString(this._order.Total);
        }

        private void CalculateTax(decimal taxRate)
        {
            this._order.Tax = this._order.Subtotal * taxRate;
        }

        private void CalculateTotal()
        {
            this._order.Total =  this._order.Subtotal + this._order.Tax + this._order.Freight;
        }

        private void CalculateFreight(decimal freightRate)
        {
            this._order.Freight = this._order.Subtotal * freightRate;
        }
    }
}
