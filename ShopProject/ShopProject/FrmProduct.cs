using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity.Migrations;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ShopProject
{
    public partial class FrmProduct : Form
    {
        public FrmProduct()
        {
            InitializeComponent();
        }

        UrunVtEntities entities = new UrunVtEntities();

        void GetByCategoryWithProduct()
        {
            var categories = (from x in entities.TblCategory
                              select new
                              {
                                  x.CategoryId,
                                  x.CategoryName
                              }).ToList();

            cmbList.ValueMember = "CategoryId";
            cmbList.DisplayMember = "CategoryName";
            cmbList.DataSource = categories;
        }
        void ProductList()
        {
            dataGridView1.DataSource = (from x in entities.TblProduct
                                        select new
                                        {
                                            x.ProductId,
                                            x.ProductName,
                                            x.ProductStock,
                                            x.ProductPrice,
                                            x.ProductStatus,
                                            x.ProductCategory
                                        }).ToList();
            GetByCategoryWithProduct();
        }
        void InsertProduct()
        {
            TblProduct product = new TblProduct();
            product.ProductName = txtName.Text;
            product.ProductStock = Convert.ToDecimal(txtStock.Text);
            product.ProductPrice = Convert.ToDecimal(txtPrice.Text);
            if (radioButton1.Checked == true)
            {
                product.ProductStatus = Convert.ToBoolean(1);
            }
            else if (radioButton2.Checked == true)
            {
                product.ProductStatus = Convert.ToBoolean(0);
            }
            product.ProductCategory = Convert.ToInt32(cmbList.SelectedValue);

            entities.TblProduct.Add(product);
            entities.SaveChanges();
        }
        void DeleteProduct()
        {
            var id = Convert.ToInt32(txtid.Text);
            var values = entities.TblProduct.Find(id);
            entities.TblProduct.Remove(values);
            entities.SaveChanges();
        }
        void UpdateProduct()
        {
            var id = Convert.ToInt32(txtid.Text);
            var values = entities.TblProduct.Find(id);
            values.ProductName = txtName.Text;
            values.ProductStock = Convert.ToDecimal(txtStock.Text);
            values.ProductPrice = Convert.ToDecimal(txtPrice.Text);
            if (radioButton1.Checked == true)
            {
                values.ProductStatus = Convert.ToBoolean(1);
            }
            else if (radioButton2.Checked == true)
            {
                values.ProductStatus = Convert.ToBoolean(0);
            }

            values.ProductCategory = Convert.ToInt32(cmbList.SelectedValue);

            entities.TblProduct.AddOrUpdate(values);
            entities.SaveChanges();
        }
        void SearchProduct()
        {
            var values = entities.TblProduct.Where(x => x.ProductName == txtName.Text).ToList();
            dataGridView1.DataSource = values;
        }
        void ClearProduct()
        {
            txtid.Text = "";
            txtName.Text = "";
            txtStock.Text = "";
            txtPrice.Text = "";
            cmbList.Text = "";
            radioButton1.Checked = false;
            radioButton2.Checked = false;
        }
        private void FrmProduct_Load(object sender, EventArgs e)
        {
            ProductList();
            GetByCategoryWithProduct();
        }
        private void btnList_Click(object sender, EventArgs e)
        {
            ProductList();
        }
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtid.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            txtName.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            txtStock.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
            txtPrice.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
            cmbList.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
        }
        private void btnInsert_Click(object sender, EventArgs e)
        {
            InsertProduct();
            MessageBox.Show("Ekleme tamamlandı", "İŞLEM BAŞARILI", MessageBoxButtons.OK, MessageBoxIcon.Information);
            ProductList();
            ClearProduct();
        }
        private void btnDelete_Click(object sender, EventArgs e)
        {
            DeleteProduct();
            MessageBox.Show("Silme tamamlandı", "İŞLEM BAŞARILI", MessageBoxButtons.OK, MessageBoxIcon.Information);
            ProductList();
            ClearProduct();
        }
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            UpdateProduct();
            MessageBox.Show("Güncelleme tamamlandı", "İŞLEM BAŞARILI", MessageBoxButtons.OK, MessageBoxIcon.Information);
            ProductList();
            ClearProduct();
        }
        private void btnSearch_Click(object sender, EventArgs e)
        {
            SearchProduct();
            ClearProduct();
        }
        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearProduct();
        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
