using CRM_Final.Business.Data;
using CRM_Final.Business.Models;
using System;
using System.Windows.Forms;

namespace CRM_Final.OrderForm
{
    public partial class OrderUpdateForm : Form
    {
        private int _salesOrderID;

        public OrderUpdateForm()
        {
            InitializeComponent();
        }

        public OrderUpdateForm(int salesOrderID)
        {
            this._salesOrderID = salesOrderID;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Order orderToUpdate = new Order()
            {
                OrderNumber = txtOrderNumber.Text,
                OrderDate = Convert.ToDateTime(txtOrderDate.Text),
                DueDate = Convert.ToDateTime(txtDueDate.Text),
                ShipDate = Convert.ToDateTime(txtShipDate.Text),
                //SalesOrderID = this.salesOrderId,
                Status = Convert.ToInt32(txtStatus.Text),
                Tax = Convert.ToDecimal(txtTax.Text),
                Freight = Convert.ToDecimal(txtFreight.Text),
                Total = Convert.ToDecimal(txtTotal.Text),
                //OrderItems = drpdwnOrderItems.Text,
                ModifiedDate = Convert.ToDateTime(txtModifiedDate.Text)
            };

            IOrderUtility orderUtil = DependencyInjector.GetOrderUtility();

            try
            {
                orderUtil.UpdateOrder(orderToUpdate);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            this.Close();
        }
    }
}
