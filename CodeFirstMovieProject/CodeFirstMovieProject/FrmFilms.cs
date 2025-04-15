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
    public partial class FrmFilms : Form
    {
        public FrmFilms()
        {
            InitializeComponent();
        }

        CodeFirstMovieProjectContext context = new CodeFirstMovieProjectContext();

        void MovieList()
        {
            var values = (from x in context.Movies
                          select new
                          {
                              x.MovieID,
                              x.MovieTitle,
                              x.Description,
                              x.Duration,
                              x.CreatedDate,
                              x.CategoryID
                          }).ToList();

            dataGridView1.DataSource = values.ToList();
        }
        void CategoryList()
        {
            var values = context.Categories.ToList();
            cmbCategory.ValueMember = "CategoryID";
            cmbCategory.DisplayMember = "CategoryName";
            cmbCategory.DataSource = values;
        }
        void InsertMovie()
        {
            Movie movie = new Movie();
            movie.MovieTitle = MovieTitle.Text;
            movie.Description = txtDescription.Text;
            movie.Duration = int.Parse(txtDuration.Text);
            movie.CreatedDate = DateTime.Parse(txtCreatedDate.Text);
            movie.CategoryID = int.Parse(cmbCategory.SelectedValue.ToString());
            context.Movies.Add(movie);
            context.SaveChanges();
            MessageBox.Show("Ekleme Tamamlandı", "Ekleme İşlemi Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        void UpdateMovie()
        {
            var id = int.Parse(txtid.Text);
            var value = context.Movies.Find(id);
            value.MovieTitle = MovieTitle.Text;
            value.Description = txtDescription.Text;
            value.Duration = int.Parse(txtDuration.Text);
            value.CreatedDate = DateTime.Parse(txtCreatedDate.Text);
            value.CategoryID = int.Parse(cmbCategory.SelectedValue.ToString());
            context.Movies.AddOrUpdate(value);
            context.SaveChanges();
            MessageBox.Show("Güncelleme Tamamlandı", "Güncelleme İşlemi Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        void DeleteMovie()
        {
            var id = int.Parse(txtid.Text);
            var value = context.Movies.Find(id);
            context.Movies.Remove(value);
            context.SaveChanges();
            MessageBox.Show("Silme Tamamlandı", "Silme İşlemi Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        void SearchMovie()
        {
            var title = context.Movies.Where(x => x.MovieTitle == MovieTitle.Text).ToList();
            dataGridView1.DataSource = title;
        }
        void ClearMovie()
        {
            txtid.Text = "";
            MovieTitle.Text = "";
            txtDescription.Text = "";
            txtDuration.Text = "";
            txtCreatedDate.Text = "";
            cmbCategory.Text = "";
        }
        private void FrmFilms_Load(object sender, EventArgs e)
        {
            MovieList();
            CategoryList();
        }
        private void btnList_Click(object sender, EventArgs e)
        {
            MovieList();
            CategoryList();
        }
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtid.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            MovieTitle.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            txtDescription.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
            txtDuration.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
            txtCreatedDate.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
            cmbCategory.Text = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
        }
        private void btnInsert_Click(object sender, EventArgs e)
        {
            InsertMovie();
            MovieList();
            ClearMovie();
        }
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            UpdateMovie();
            MovieList();
            ClearMovie();
        }
        private void btnDelete_Click(object sender, EventArgs e)
        {
            DeleteMovie();
            MovieList();
            ClearMovie();
        }
        private void btnSearch_Click(object sender, EventArgs e)
        {
            SearchMovie();
            ClearMovie();
        }
    }
}
