using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ShopProject
{
    public partial class FrmStatistic : Form
    {
        public FrmStatistic()
        {
            InitializeComponent();
        }

        UrunVtEntities entities = new UrunVtEntities();

        private void FrmStatistic_Load(object sender, EventArgs e)
        {
            // Toplam Kategori sayısı
            lblCategoryCount.Text = entities.TblCategory.Count().ToString();

            // Toplam Ürün sayısı
            lblProductCount.Text = entities.TblProduct.Count().ToString();

            // Toplam Müşteri sayısı
            lblCustomerCount.Text = entities.TblCustomer.Count().ToString();

            // Toplam Sipariş sayısı
            lblOrderCount.Text = entities.TblOrder.Count().ToString();

            // Toplam Stok sayısı
            var totalStockCount = entities.TblProduct.Sum(x => x.ProductStock);
            lblTotalStockCount.Text = totalStockCount.ToString();

            //Ortalama Ürün Fiyatı
            var avgProductCount = entities.TblProduct.Average(x => x.ProductPrice);
            lblavgProductPrice.Text = avgProductCount.ToString() + " ₺";

            //Toplam Gömlek Stok
            var TotalGomlekStock = entities.TblProduct.Where(x => x.ProductName == "Gömlek").Sum(x => x.ProductStock);
            lblTotalGomlekStock.Text = TotalGomlekStock.ToString();

            //Buzdolabı Toplam Fiyatı
            var buzdolabiStock = entities.TblProduct.Where(x => x.ProductName == "Buzdolabı").Select(x => x.ProductStock).FirstOrDefault();
            var buzdolabiUnitPrice = entities.TblProduct.Where(x => x.ProductName == "Buzdolabı").Select(x => x.ProductPrice).FirstOrDefault();
            var buzdolabiTotalPrice = buzdolabiStock * buzdolabiUnitPrice;
            lblBuzdolabiTotalPrice.Text = buzdolabiTotalPrice.ToString();

            //Stok Sayısı 50'den az olan ürünler
            var stock50 = entities.TblProduct.Select(x => x.ProductStock <= 50).Count();
            lblStock50.Text = stock50.ToString();

            //Kadın Giyime ait olan ürünlerin stok sayısı
            var ProductCategory = entities.TblProduct.Where(x => x.ProductCategory == 1).Sum(y => y.ProductStock);
            lblKadınGiyimStock.Text = ProductCategory.ToString();

            //Ülkesi Türkiye olan müşterilerin toplam sipariş sayısı(hem sql ile hem ef ile getirilecek)
            var country = entities.TblCustomer.Where(x => x.CustomerCountry == "Türkiye").Select(y => y.CustomerID).ToList();
            var orderTotal = entities.TblOrder.Count(x => country.Contains(x.CustomerId.Value));
            label14.Text = orderTotal.ToString();

            //Kadın Giyime ait olan ürünlerin toplam kazancı(hem sql ile hem ef ile getirilecek)
            var category = entities.TblCategory.Select(x => x.CategoryName == "Kadın Giyim").ToList();
            var product = entities.TblProduct.Select(x => x.ProductCategory == 1).ToList();
            var order = entities.TblOrder.Where(x => x.ProductId == 1).Sum(y => y.TotalPrice);
            lblKadınGiyimTotalPrice.Text = order.ToString();

            //Son eklenen ürünün adı
            var productId = entities.TblProduct.OrderByDescending(x => x.ProductId).Select(y => y.ProductName).FirstOrDefault();
            lblLastProduct.Text = productId.ToString();

            //Son eklenen ürün kategorisi
            var lastpProductId = entities.TblProduct.OrderByDescending(x => x.ProductId).Select(y => y.ProductCategory).FirstOrDefault();
            var lastProductCategory = entities.TblCategory.Where(x => x.CategoryId == lastpProductId).Select(y => y.CategoryName).FirstOrDefault();
            lastCategoryName.Text = lastProductCategory.ToString();

            //Aktif Ürün Sayısı
            var status1 = entities.TblProduct.Count(x => x.ProductStatus == true);
            lblStatus1.Text = status1.ToString();

            //Toplam Redmi Note 10 Kazanç Tutarı
            var stock = entities.TblProduct.Where(x => x.ProductName == "Redmi Note 10").Select(x => x.ProductStock).FirstOrDefault();
            var productPrice = entities.TblProduct.Where(x => x.ProductName == "Redmi Note 10").Select(x => x.ProductPrice).FirstOrDefault();
            var totalPrice = productPrice * stock;

            var orderPrice = entities.TblOrder.Where(x => x.ProductId == 6).Select(x => x.TotalPrice).FirstOrDefault();
            lblRedmi10.Text = (orderPrice - totalPrice).ToString();

            //Son siparişi veren müşterinin Adı
            var lastCustomerId = entities.TblOrder.OrderByDescending(x => x.OrderID).Select(y => y.CustomerId).FirstOrDefault();
            var customerName = entities.TblCustomer.Where(x => x.CustomerID == lastCustomerId).Select(y => y.CustomerName).FirstOrDefault();
            lblLastCustomerName.Text = customerName;

            //Ülke Çeşitliliği Sayısı
            var differentCount = entities.TblCustomer.Select(x => x.CustomerCountry).Distinct().Count();
            lblCountryCount.Text = differentCount.ToString();

        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
