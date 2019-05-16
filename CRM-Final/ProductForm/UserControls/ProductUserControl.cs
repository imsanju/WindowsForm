using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using CRM_Final.Business.Models;

namespace CRM_Final.ProductForm.UserControls
{
    public partial class ProductUserControl : UserControl
    {
        private Product _product;

        public ProductUserControl()
        {
            InitializeComponent();
        }

        public ProductUserControl(Product product) : this()
        {
            _product = product;
        }
        private void ProductUserControl_Load(object sender, EventArgs e)
        {
            lblName.Text = _product.Name;
            lblDescription.Text = _product.Description;
            lblProductNumber.Text = _product.ProductNumber;
            lblListPrice.Text = String.Format("{0:c}", _product.ListPrice);

            Stream picStream = new MemoryStream(_product.ThumbNailPhoto);
            productPictureBox.Image = new Bitmap(picStream);

            foreach (Control child in this.Controls)
            {
                child.MouseDoubleClick += ProductUserControl_MouseDoubleClick;
            }
        }

        private void ProductUserControl_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Console.Out.WriteLine("here");
            Console.WriteLine("here");
            ProductUpdateForm puf = new ProductUpdateForm(_product.ProductId);
            puf.ShowDialog();
        }

        private void ProductUserControl_MouseHover(object sender, EventArgs e)
        {
           this.BackColor = Color.MediumAquamarine;
        }

        private void ProductUserControl_MouseLeave(object sender, EventArgs e)
        {
            this.BackColor = SystemColors.Control;
        }

        private void lblDescription_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ProductUpdateForm puc = new ProductUpdateForm(_product.ProductId);
            puc.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ProductUpdateForm puc = new ProductUpdateForm(_product.ProductId);
            puc.ShowDialog();
        }
    }
}
