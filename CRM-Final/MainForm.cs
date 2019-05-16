using CRM_Final.ProductForm;
using CRM_Final.CustomerForm;
using System;
using System.Windows.Forms;
using CRM_Final.OrderForm;

namespace CRM_Final
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tsbExit_Click(object sender, EventArgs e)
        {
            this.exitToolStripMenuItem_Click(sender, e);
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ProductAddForm prodAddForm = new ProductAddForm();
            prodAddForm.ShowDialog();
        }

        private void searchToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ProductSearchForm prodSearchForm = new ProductSearchForm();
            prodSearchForm.ShowDialog();
        }

        private void searchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CustomerSearchForm customerSearchForm = new CustomerSearchForm();
            customerSearchForm.ShowDialog();
        }

        private void createNewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CustomerAddForm customerAddForm = new CustomerAddForm();
            customerAddForm.ShowDialog();
        }

        private void findToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OrderSearchForm orderSearchForm = new OrderSearchForm();
            orderSearchForm.ShowDialog();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            CustomerUpdateForm customerUpdate = new CustomerUpdateForm(30050);
            customerUpdate.ShowDialog();
        }

        private void newOrderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OrderAddForm orderAddForm = new OrderAddForm();
            orderAddForm.ShowDialog();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutForm aboutForm = new AboutForm();
            aboutForm.ShowDialog();
        }

        private void linkLabel2_MouseClick(object sender, MouseEventArgs e)
        {
            ProductSearchForm psf = new ProductSearchForm();
            psf.ShowDialog();
        }

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ProductUpdateForm puf = new ProductUpdateForm(713);
            puf.ShowDialog();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ProductAddForm paf = new ProductAddForm();
            paf.ShowDialog();

        }

        private void linkLabel4_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            CustomerAddForm customerAddForm = new CustomerAddForm();
            customerAddForm.ShowDialog();
        }

        private void linkLabel5_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            CustomerSearchForm customerSearchForm = new CustomerSearchForm();
            customerSearchForm.ShowDialog();
        }

        private void linkLabel6_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            CustomerUpdateForm customerUpdateForm = new CustomerUpdateForm(20);
            customerUpdateForm.ShowDialog();
        }

        private void linkLabel6_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
        {
            OrderAddForm orderItemForm = new OrderAddForm();
            orderItemForm.ShowDialog();
        }

        private void linkLabel7_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            OrderSearchForm orderSearchForm = new OrderSearchForm();
            orderSearchForm.ShowDialog();
        }

        private void linkLabel6_LinkClicked_2(object sender, LinkLabelLinkClickedEventArgs e)
        {
            OrderAddForm orderAddForm = new OrderAddForm();
            orderAddForm.ShowDialog();
        }
    }
}
