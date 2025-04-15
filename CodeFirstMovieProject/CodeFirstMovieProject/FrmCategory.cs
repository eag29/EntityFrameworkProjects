using CodeFirstMovieProject.DAL.Context;
using CodeFirstMovieProject.DAL.Entities;
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

namespace CodeFirstMovieProject
{
    public partial class FrmCategory : Form
    {
        public FrmCategory()
        {
            InitializeComponent();
        }

        CodeFirstMovieProjectContext context = new CodeFirstMovieProjectContext();

        void CategoryList()
        {
            dataGridView1.DataSource = context.Categories.ToList();
        }
        void InsertCategory()
        {
            Category category = new Category();
            category.CategoryName = txtName.Text;
            context.Categories.Add(category);
            context.SaveChanges();
            MessageBox.Show("Kategori Eklendi", "Ekleme İşlemi Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        void UpdateCategory()
        {
            var id = int.Parse(txtid.Text);
            var value = context.Categories.Find(id);
            value.CategoryName = txtName.Text;
            context.Categories.AddOrUpdate(value);
            context.SaveChanges();
            MessageBox.Show("Kategori Güncellendi", "Güncelleme İşlemi Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        void DeleteCategory()
        {
            var id = int.Parse(txtid.Text);
            var value = context.Categories.Find(id);
            context.Categories.Remove(value);
            context.SaveChanges();
            MessageBox.Show("Kategori Silindi", "Silme İşlemi Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        void ClearCategory()
        {
            txtid.Text = "";
            txtName.Text = "";
        }
        void CategorySearch()
        {
            var values = context.Categories.Where(x=>x.CategoryName==txtName.Text).ToList();
            dataGridView1.DataSource = values;
        }
        private void FrmCategory_Load(object sender, EventArgs e)
        {
            CategoryList();
        }
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtid.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            txtName.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
        }
        private void btnList_Click_1(object sender, EventArgs e)
        {
            CategoryList();
        }
        private void btnInsert_Click(object sender, EventArgs e)
        {
            InsertCategory();
            CategoryList();
            ClearCategory();
        }
        private void btnUpdate_Click_1(object sender, EventArgs e)
        {
            UpdateCategory();
            CategoryList();
            ClearCategory();
        }
        private void btnDelete_Click_1(object sender, EventArgs e)
        {
            DeleteCategory();
            CategoryList();
            ClearCategory();
        }
        private void btnSearch_Click_1(object sender, EventArgs e)
        {
            CategorySearch();
            ClearCategory();
        }
        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearCategory();
        }
        private void pictureBox1_Click_1(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
