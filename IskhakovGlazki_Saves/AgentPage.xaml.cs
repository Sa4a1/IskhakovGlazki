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
        int CountRecords;
        int CountPage;
        int CurrentPage = 0;
        List<Agent> CurrentPageList =new List<Agent>();
        List<Agent> TableList;
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

            TableList = currentAgents;

            ChangePage(0, 0);
        }


        private void ChangePage(int direction, int? selectedPage)
        {
            CurrentPageList.Clear();
            CountRecords = TableList.Count();
            if (CountRecords % 10 > 0) CountPage = CountRecords / 10 + 1;
            else CountPage = CountRecords / 10;

            Boolean ifUpdate = true;

            int min;

            if (selectedPage.HasValue)
            {
                if (selectedPage >= 0 && selectedPage <= CountPage)
                {
                    CurrentPage = (int)selectedPage;
                    min = CurrentPage * 10 + 10 < CountRecords ? CurrentPage * 10 + 10 : CountRecords;
                    for (int i = CurrentPage * 10; i < min; i++) CurrentPageList.Add(TableList[i]);
                }
            }

            else
            {
                switch (direction)
                {
                    case 1:
                        if (CurrentPage > 0)
                        {
                            CurrentPage--;
                            min = CurrentPage * 10 + 10 < CountRecords ? CurrentPage * 10 + 10 : CountRecords;
                            for (int i = CurrentPage * 10; i < min; i++) CurrentPageList.Add(TableList[i]);
                        }
                        else ifUpdate = false;
                        break;
                    case 2:
                        if (CurrentPage < CountPage - 1)
                        {
                            CurrentPage++;
                            min = CurrentPage * 10 + 10 < CountRecords ? CurrentPage * 10 + 10 : CountRecords;
                            for (int i = CurrentPage * 10; i < min; i++) CurrentPageList.Add(TableList[i]);

                        }
                        else ifUpdate = false;
                        break;
                }
            }
            if (ifUpdate)
            {
                PageListBox.Items.Clear();
                for (int i = 1; i <= CountPage; i++) PageListBox.Items.Add(i);
                PageListBox.SelectedIndex = CurrentPage;

                AgentListView.ItemsSource = CurrentPageList;
                AgentListView.Items.Refresh();
            }
        }

        private void SortCmbТ1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateAgents();
        }

        private void RDirBtn_Click(object sender, RoutedEventArgs e)
        {
            ChangePage(2, null);
        }

        private void LDirBtn_Click(object sender, RoutedEventArgs e)
        {
            ChangePage(1, null);
        }

        private void PageListBox_MouseUp(object sender, MouseButtonEventArgs e)
        {
            ChangePage(0, Convert.ToInt32(PageListBox.SelectedItem.ToString())-1);
        }

        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
