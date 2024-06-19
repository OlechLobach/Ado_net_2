using System.Windows;

namespace Ado_net_2
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            LoadData();
        }

            private void LoadData()
            {
                dgProducts.ItemsSource = DBManager.GetAllProducts().DefaultView;
            }

        private void AddProduct_Click(object sender, RoutedEventArgs e)
        {
            DBManager.InsertProduct("Sample Product", "Sample Type", 1, 100);
            LoadData();
        }

        private void UpdateProduct_Click(object sender, RoutedEventArgs e)
        {
            // Here you need to add code to update the product
            // For example:
            DBManager.UpdateProduct(1, "Updated Product", "Updated Type", 1, 200);
            LoadData();
        }

        private void DeleteProduct_Click(object sender, RoutedEventArgs e)
        {
            // Here you need to add code to delete the product
            // For example:
            DBManager.DeleteProduct(1);
            LoadData();
        }

        private void ShowSuppliersInfo_Click(object sender, RoutedEventArgs e)
        {
            // Here you need to add code to show supplier information
            // For example:
            var supplierInfo = DBManager.GetSupplierWithMostProducts();
            MessageBox.Show($"Supplier with most products: {supplierInfo}");
        }
    }
}                                                                           