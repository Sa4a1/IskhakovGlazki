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
    /// Логика взаимодействия для AgentPage.xaml
    /// </summary>
    public partial class AgentPage : Page
    {
        public AgentPage()
        {
            InitializeComponent();
            var currentAgents = Iskhakov_GlazkiEntities.GetContext().Agent.ToList();
            AgentListView.ItemsSource = currentAgents;
            SortCmb.SelectedIndex = 0;
            SortCmbТ1.SelectedIndex = 0;
        }

        private void txtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateAgents();
        }

        private void SortCmb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateAgents();
        }

        private void UpdateAgents()
        {
            var currentAgents = Iskhakov_GlazkiEntities.GetContext().Agent.ToList();
            if (SortCmb.SelectedIndex == 0) currentAgents = currentAgents.Where(p => (p.AgentTypeID >= 0 && p.AgentTypeID <= 6)).ToList();
            if (SortCmb.SelectedIndex == 1) currentAgents = currentAgents.Where(p => (p.AgentTypeID == 1)).ToList();
            if(SortCmb.SelectedIndex == 2) currentAgents = currentAgents.Where(p => (p.AgentTypeID == 2)).ToList();
            if(SortCmb.SelectedIndex == 3) currentAgents = currentAgents.Where(p => (p.AgentTypeID == 3)).ToList();
            if(SortCmb.SelectedIndex == 4) currentAgents = currentAgents.Where(p => (p.AgentTypeID == 4)).ToList();
            if(SortCmb.SelectedIndex == 5) currentAgents = currentAgents.Where(p => (p.AgentTypeID == 5)).ToList();
            if(SortCmb.SelectedIndex == 6) currentAgents = currentAgents.Where(p => (p.AgentTypeID == 6)).ToList();
            if (SortCmbТ1.SelectedIndex == 0) { }
            if (SortCmbТ1.SelectedIndex == 1) currentAgents = currentAgents.OrderBy(p => p.Title).ToList();
            if (SortCmbТ1.SelectedIndex == 2) currentAgents = currentAgents.OrderByDescending(p => p.Title).ToList();
            if (SortCmbТ1.SelectedIndex == 3) currentAgents = currentAgents.OrderBy(p => p.Priority).ToList();
            if (SortCmbТ1.SelectedIndex == 4) currentAgents = currentAgents.OrderByDescending(p => p.Priority).ToList();

            currentAgents = currentAgents.Where(p =>
            p.Title.ToLower().Contains(TxtSearch.Text.ToLower())
            || p.Phone.Replace("+7", "8").Replace("(", "").Replace(")", "").Replace(" ", "").Replace("-", "")
            .Contains(TxtSearch.Text.Replace("+7", "8").Replace("(", "").Replace(")", "").Replace(" ", "").Replace("-", ""))
            || p.Email.ToLower().Contains(TxtSearch.Text.ToLower())).ToList();
            AgentListView.ItemsSource = currentAgents;
        }


        private void SortCmbТ1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateAgents();
        }
    }
}
