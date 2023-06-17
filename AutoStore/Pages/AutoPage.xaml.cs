using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;

namespace AutoStore.Pages
{
    /// <summary>
    /// Логика взаимодействия для AutoPage.xaml
    /// </summary>
    public partial class AutoPage : Page
    {
        public AutoPage()
        {
            InitializeComponent();
        }

        private void DGridCars_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void UpdateBtn_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new AddCarsPage((sender as System.Windows.Controls.Button).DataContext as Car));
        }

        private void AddBtn_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new AddCarsPage(null));
        }

        private void AutoIsVisible(object sender, System.Windows.DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible)
            {
                AutoStoreEntities.GetContext().ChangeTracker.Entries().ToList().ForEach(c => c.Reload());
                DGridCars.ItemsSource = AutoStoreEntities.GetContext().Cars.ToList();
            }
        }

        private void DeleteBtn_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var carsForRemove = DGridCars.SelectedItems.Cast<Car>().ToList();
            if (System.Windows.MessageBox.Show("Вы точно хотите удалить?", "Внимание!", (MessageBoxButton)MessageBoxButtons.YesNo, (MessageBoxImage)MessageBoxIcon.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    AutoStoreEntities.GetContext().Cars.RemoveRange(carsForRemove);
                    AutoStoreEntities.GetContext().SaveChanges();
                    System.Windows.MessageBox.Show("Данные удалены");

                    DGridCars.ItemsSource = AutoStoreEntities.GetContext().Cars.ToList();
                }
                catch (Exception ex)
                {
                    System.Windows.MessageBox.Show(ex.Message.ToString());
                }
            }
        }
    }
}
