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
    public partial class FrmCategory : Form
    {
        public FrmCategory()
        {
            InitializeComponent();
        }

        UrunVtEntities entities = new UrunVtEntities();

        void CategoryList()
        {
            dataGridView1.DataSource = entities.TblCategory.ToList();
        }
        void InsertCategory()
        {
            TblCategory values = new TblCategory();
            values.CategoryName = txtCategory.Text;
            entities.TblCategory.Add(values);
            entities.SaveChanges();
        }
        void UpdateCategory()
        {
            var id = Convert.ToInt32(txtCategoryid.Text);
            var values = entities.TblCategory.Find(id);
            values.CategoryName = txtCategory.Text;
            entities.TblCategory.AddOrUpdate(values);
            entities.SaveChanges();
        }
        void DeleteCategory()
        {
            var id = Convert.ToInt32(txtCategoryid.Text);
            var values = entities.TblCategory.Find(id);
            entities.TblCategory.Remove(values);
            entities.SaveChanges();
        }
        void SearchCategory()
        {
            var values = entities.TblCategory.Where(x => x.CategoryName == txtCategory.Text).ToList();
            dataGridView1.DataSource = values;
        }
        void Clear()
        {
            txtCategoryid.Text = "";
            txtCategory.Text = "";
        }
        private void FrmCategory_Load(object sender, EventArgs e)
        {
            CategoryList();
        }
        private void btnList_Click(object sender, EventArgs e)
        {
            CategoryList();
        }
        private void btnInsert_Click(object sender, EventArgs e)
        {
            InsertCategory();
            MessageBox.Show("Ekleme işlemi tamamlandı", "İŞLEM BAŞARILI", MessageBoxButtons.OK, MessageBoxIcon.Information);
            CategoryList();
            Clear();
        }
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            UpdateCategory();
            MessageBox.Show("Güncelleme işlemi tamamlandı", "İŞLEM BAŞARILI", MessageBoxButtons.OK, MessageBoxIcon.Information);
            CategoryList();
            Clear();
        }
        private void btnDelete_Click(object sender, EventArgs e)
        {
            DeleteCategory();
            MessageBox.Show("Silme işlemi tamamlandı", "İŞLEM BAŞARILI", MessageBoxButtons.OK, MessageBoxIcon.Information);
            CategoryList();
            Clear();
        }
        private void btnSearch_Click(object sender, EventArgs e)
        {
            SearchCategory();
            Clear();
        }
        private void btnClear_Click(object sender, EventArgs e)
        {
            Clear();
        }
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtCategoryid.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            txtCategory.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
