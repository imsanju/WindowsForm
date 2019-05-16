namespace CRM_Final.ProductForm.UserControls
{
    partial class ProductUserControl
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.productPictureBox = new System.Windows.Forms.PictureBox();
            this.lblName = new System.Windows.Forms.Label();
            this.lblProductNumber = new System.Windows.Forms.Label();
            this.lblListPrice = new System.Windows.Forms.Label();
            this.lblDescription = new System.Windows.Forms.RichTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.productPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // productPictureBox
            // 
            this.productPictureBox.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.productPictureBox.Location = new System.Drawing.Point(16, 3);
            this.productPictureBox.Name = "productPictureBox";
            this.productPictureBox.Size = new System.Drawing.Size(135, 125);
            this.productPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.productPictureBox.TabIndex = 0;
            this.productPictureBox.TabStop = false;
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.BackColor = System.Drawing.SystemColors.ControlLight;
            this.lblName.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblName.ForeColor = System.Drawing.Color.DarkRed;
            this.lblName.Location = new System.Drawing.Point(169, 3);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(0, 29);
            this.lblName.TabIndex = 1;
            // 
            // lblProductNumber
            // 
            this.lblProductNumber.AutoSize = true;
            this.lblProductNumber.Location = new System.Drawing.Point(171, 50);
            this.lblProductNumber.Name = "lblProductNumber";
            this.lblProductNumber.Size = new System.Drawing.Size(0, 13);
            this.lblProductNumber.TabIndex = 3;
            // 
            // lblListPrice
            // 
            this.lblListPrice.AutoSize = true;
            this.lblListPrice.Location = new System.Drawing.Point(171, 79);
            this.lblListPrice.Name = "lblListPrice";
            this.lblListPrice.Size = new System.Drawing.Size(0, 13);
            this.lblListPrice.TabIndex = 4;
            // 
            // lblDescription
            // 
            this.lblDescription.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lblDescription.Enabled = false;
            this.lblDescription.Location = new System.Drawing.Point(174, 104);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(261, 79);
            this.lblDescription.TabIndex = 5;
            this.lblDescription.Text = "";
            this.lblDescription.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lblDescription_MouseDoubleClick);
            // 
            // ProductUserControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lblDescription);
            this.Controls.Add(this.lblListPrice);
            this.Controls.Add(this.lblProductNumber);
            this.Controls.Add(this.lblName);
            this.Controls.Add(this.productPictureBox);
            this.ForeColor = System.Drawing.SystemColors.Control;
            this.Name = "ProductUserControl";
            this.Size = new System.Drawing.Size(451, 200);
            this.Load += new System.EventHandler(this.ProductUserControl_Load);
            this.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.ProductUserControl_MouseDoubleClick);
            this.MouseLeave += new System.EventHandler(this.ProductUserControl_MouseLeave);
            this.MouseHover += new System.EventHandler(this.ProductUserControl_MouseHover);
            ((System.ComponentModel.ISupportInitialize)(this.productPictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox productPictureBox;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Label lblProductNumber;
        private System.Windows.Forms.Label lblListPrice;
        private System.Windows.Forms.RichTextBox lblDescription;
    }
}
