using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using CRM_Final.Business.Data;
using CRM_Final.Business.Models;

namespace CRM_Final.ProductForm
{
    public partial class ProductAddForm : Form
    {
        private Product newProduct;

        public ProductAddForm()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            newProduct.Name = txtName.Text;
            newProduct.ProductNumber = txtProdNumber.Text;
            newProduct.ListPrice = Convert.ToDecimal(txtListPrice.Text);
            newProduct.SellStartDate = sellDateTime.Value;
            newProduct.Size = txtSize.Text;
            newProduct.StandardCost = Convert.ToDecimal(txtStandardCost.Text);
            newProduct.Weight = Convert.ToDecimal(txtWeight.Text);

            IProductInventoryUtility prodUtil = DependencyInjector.GetProductInventoryUtility();

            try
            {
                prodUtil.AddProductToInventory(newProduct);
                MessageBox.Show("Successfully Added New Product!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            this.Close();
        }

        private void pictureLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
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

            newProduct.ThumbNailPhoto = bufferData;
            newProduct.ThumbnailFileName = filePath;

            ShowThumbnail(bufferData);
        }

        private void ProductAddForm_Load(object sender, EventArgs e)
        {
            newProduct = new Product();
        }

        private void ShowThumbnail(byte[] pictureBuffer)
        {
            Stream picStream = new MemoryStream(pictureBuffer);
            productPictureBox.Image = new Bitmap(picStream);
        }
    }
}
