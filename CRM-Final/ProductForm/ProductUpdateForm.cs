using CRM_Final.Business.Data;
using CRM_Final.Business.Models;
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace CRM_Final.ProductForm
{
    public partial class ProductUpdateForm : Form
    {
        private int productId;

        public ProductUpdateForm()
        {
            InitializeComponent();
        }

        public ProductUpdateForm(int productId) : this()
        {
            this.productId = productId;
        }

        private void ProductUpdateForm_Load(object sender, EventArgs e)
        {
            Product product;

            IProductInventoryUtility invUtil = DependencyInjector.GetProductInventoryUtility();
            product = invUtil.GetInventory(productId);

            txtName.Text = product.Name;
            txtProdNumber.Text = product.ProductNumber;
            txtListPrice.Text = product.ListPrice.ToString();
            txtDescription.Text = product.Description;
            dateTimePicker1.Value = product.SellStartDate;
            txtSize.Text = product.Size.ToString();
            txtStandardCost.Text = product.StandardCost.ToString();
            txtWeight.Text = product.Weight.ToString();
            txtCategory.Text = product.Category;

            
            ShowThumbnail(product.ThumbNailPhoto);
        }

        private void ShowThumbnail(byte[] pictureBuffer)
        {
            Stream picStream = new MemoryStream(pictureBuffer);
            productPictureBox.Image = new Bitmap(picStream);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Product prodToUpdate = new Product()
            {
                Description = txtDescription.Text,
                Name = txtName.Text,
                ProductNumber = txtProdNumber.Text,
                ListPrice = Convert.ToDecimal(txtListPrice.Text),
                ProductId = this.productId,
                ModifiedDate = DateTime.Now,
                SellStartDate = dateTimePicker1.Value,
                Size = txtSize.Text,
                StandardCost = Convert.ToDecimal(txtStandardCost.Text),
                Weight = Convert.ToDecimal(txtWeight.Text),
                Category = txtCategory.Text
            };

            IProductInventoryUtility invUtil = DependencyInjector.GetProductInventoryUtility();

            try
            {
                invUtil.UpdateProduct(prodToUpdate);
                MessageBox.Show("Successfully updated product information");
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            this.Close();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            IProductInventoryUtility pUtil = DependencyInjector.GetProductInventoryUtility();

            Product prodToDelete = new Product()
            {
                ProductId = this.productId
            };

            try
            {
                pUtil.DeleteProduct(prodToDelete);
                MessageBox.Show("Successfully deleted product!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
            this.Close();
        }

        private void changePicture_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            openFileDialog1.Filter = "Image Files(*.BMP;*.JPG;*.GIF)|*.BMP;*.JPG;*.GIF|All files (*.*)|*.* ";
            var dialogResult = openFileDialog1.ShowDialog();

            string filePath = string.Empty;
            if (dialogResult == DialogResult.OK)
            {
                openFileDialog1.CheckFileExists = true;
                filePath = openFileDialog1.FileName;
            }
            else
            {
                MessageBox.Show("No File Selected");
                return;
            }

            byte[] bufferData;
            using (FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                bufferData = new byte[fileStream.Length];
                fileStream.Read(bufferData, 0, bufferData.Length);
            }

            IProductInventoryUtility invUtility = DependencyInjector.GetProductInventoryUtility();
            invUtility.UpdateProductPicture(productId, bufferData);
            ShowThumbnail(bufferData);

        }
    }
}
