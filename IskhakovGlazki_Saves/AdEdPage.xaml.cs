﻿using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
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
    /// Логика взаимодействия для AdEdPage.xaml
    /// </summary>
    public partial class AdEdPage : Page
    {
       
        private Agent currentAgent = new Agent();
        private List<AgentType> agentTypeDblist = Iskhakov_GlazkiEntities.GetContext().AgentType.ToList();
        public AdEdPage(Agent SelectedAgent = null )
        {
            InitializeComponent();
        
            if (SelectedAgent != null)
            {
                this.currentAgent = SelectedAgent;
                Combotype.SelectedItem = currentAgent.AgentTypeID -1;
            }
            DataContext = currentAgent;
        }

        private void DeleteBtn_Click(object sender, RoutedEventArgs e)
        {
            var currentAgent = (sender as Button).DataContext as Agent; 
            var currentProductSaleCount = Iskhakov_GlazkiEntities.GetContext().ProductSale.ToList();
            currentProductSaleCount= currentProductSaleCount.Where(p=>p.AgentID == currentAgent.ID).ToList();
            if (currentProductSaleCount.Count == 0)
            {
                if (MessageBox.Show("Вы точно хотите выполнить удаление?!", "Внимание!!!", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    try
                    {
                        Iskhakov_GlazkiEntities.GetContext().Agent.Remove(currentAgent);
                        Iskhakov_GlazkiEntities.GetContext().SaveChanges();
                        Manager.MainFrame.GoBack();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message.ToString());
                    }
                }
            }
            else MessageBox.Show("Невозможно удаление записи, из-за существования реализации продукции!!");
        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errs = new StringBuilder();
            if (string.IsNullOrWhiteSpace(currentAgent.Title)) errs.AppendLine("Введите имя агента!");
            if (string.IsNullOrWhiteSpace(currentAgent.Address)) errs.AppendLine("Введите адрес Агента!");
            if (string.IsNullOrWhiteSpace(currentAgent.DirectorName)) errs.AppendLine("Введите имя Директора!");
            if (string.IsNullOrWhiteSpace(currentAgent.KPP)) errs.AppendLine("");
            if (Combotype.SelectedItem == null) errs.AppendLine("Укажите тип Агента!!!!");
            if (string.IsNullOrWhiteSpace(currentAgent.Priority.ToString())) errs.AppendLine("Укажите приорететность!");
            if (currentAgent.Priority <= 0)errs.AppendLine("укажите приорететность агента свыше нуля!");
            if (string.IsNullOrWhiteSpace(currentAgent.INN)) errs.AppendLine("Введите ИНН");
            if (string.IsNullOrWhiteSpace(currentAgent.Phone)) errs.AppendLine("Введите номеp телефона!");
            else
            {
                string phone = currentAgent.Phone.Replace("(","").Replace("-","").Replace("+","");
                if (((phone[1] == '9' || phone[1] == '4' || phone[1] == '8') && phone.Length != 11)
                    || (phone[1] == '3' && phone.Length != 12)) errs.AppendLine("Укажите телефон верно!") ;
            }
            if (string.IsNullOrWhiteSpace(currentAgent.Email)) errs.AppendLine("Укажите почту!");
            if(errs.Length >0)
            {
                MessageBox.Show(errs.ToString());
                return;
            }
            if (currentAgent.ID == 0) Iskhakov_GlazkiEntities.GetContext().Agent.Add(currentAgent);

            try
            {
                Iskhakov_GlazkiEntities.GetContext().SaveChanges();
                MessageBox.Show("Информация сохранена");
                Manager.MainFrame.GoBack();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message.ToString());}
        }

        private void ChangePictureBtn_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog myOpenFile = new OpenFileDialog();
            if(myOpenFile.ShowDialog() == true)
            {
                currentAgent.Logo = myOpenFile.FileName;
                LogoImage.Source = new BitmapImage(new Uri(myOpenFile.FileName));
            }
        }
    }
}
