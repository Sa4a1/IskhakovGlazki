using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace IskhakovGlazki_Saves
{
    /// <summary>
    /// Логика взаимодействия для AddSalePage.xaml
    /// </summary>
    public partial class AddSalePage : Page
    {
        private ProductSale currentProductSale = new ProductSale();
        public List<Product> currentProduct = new List<Product>(Iskhakov_GlazkiEntities.GetContext().Product.ToList());
        Agent currentAgent;
        public AddSalePage(Agent SelectedAgent)
        {
            InitializeComponent();
            currentAgent = SelectedAgent;
            ProductComboBox.ItemsSource = currentProduct;
            DataContext = currentProductSale;
        }

        private void SaveSaleButton_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();
            if (string.IsNullOrWhiteSpace(currentAgent.Title))
                errors.AppendLine("Укажите наименование агента");
            if (string.IsNullOrWhiteSpace(currentAgent.Address))
                errors.AppendLine("Укажите адрес агента");
            if (string.IsNullOrWhiteSpace(currentAgent.DirectorName))
                errors.AppendLine("Укажите ФИО директора");
            if (ProductComboBox.SelectedItem == null)
                errors.AppendLine("Укажите тип агента");
            else
            {
                currentAgent.AgentTypeID = ProductComboBox.SelectedIndex + 1;
            }
            if (string.IsNullOrWhiteSpace(currentAgent.Priority.ToString()))
                errors.AppendLine("Укажите приоритет агента");
            if (currentAgent.Priority <= 0)
                errors.AppendLine("Укажите положительный приоритет агента");
            if (string.IsNullOrWhiteSpace(currentAgent.INN))
                errors.AppendLine("Укажите ИНН агента");
            if (string.IsNullOrWhiteSpace(currentAgent.KPP))
                errors.AppendLine("Укажите КПП агента");
            if (string.IsNullOrWhiteSpace(currentAgent.Phone))
                errors.AppendLine("Укажите телефон агента");
            else
            {
                string ph = currentAgent.Phone.Replace("(", "").Replace("-", "").Replace("+", "");
                if (((ph[1] == '9' || ph[1] == '4' || ph[1] == '8') && ph.Length != 11) || (ph[1] == '3' && ph.Length != 12))
                    errors.AppendLine("Укажите правильно телефон агента");
            }
            if (string.IsNullOrWhiteSpace(currentAgent.Email))
                errors.AppendLine("Укажите почту агента");
        }

        private void DeleteBtn_Click(object sender, RoutedEventArgs e)
        {

            var currentAgent = (sender as Button).DataContext as Agent;
            var currentAgent1 = Iskhakov_GlazkiEntities.GetContext().ProductSale.ToList();
            currentAgent1 = currentAgent1.Where(p => p.AgentID == currentAgent.ID).ToList();
            if (currentAgent1.Count != 0)
                MessageBox.Show("Невозможно выполнить удаление, т.к. существуют записи на эту услугу");
            else
            {
                if (MessageBox.Show("Вы точно хотите выполнить удаление?", "Внимание!", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    try
                    {
                        Iskhakov_GlazkiEntities.GetContext().Agent.Remove(currentAgent);
                        Iskhakov_GlazkiEntities.GetContext().SaveChanges();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message.ToString());
                    }
                }

            }




        }
        private void ProductsComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
                {
                    ProductsComboBox.IsDropDownOpen = true;
                    currentProduct = currentProduct.Where(p => p.Title.ToLower().Contains(ProductsComboBox.Text.ToLower())).ToList();
                    ProductsComboBox.ItemsSource = currentProduct;
                }
            }
        } 