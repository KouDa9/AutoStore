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

namespace AutoStore.Pages
{
    /// <summary>
    /// Логика взаимодействия для AddCarsPage.xaml
    /// </summary>
    public partial class AddCarsPage : Page
    {
        private Car car = new Car();
        public AddCarsPage(Car selectedCar)
        {
            InitializeComponent();
            if (selectedCar != null)
                car = selectedCar;
            DataContext = car;
        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();
            if (string.IsNullOrEmpty(car.Name))
                errors.AppendLine("Заполите строку1");
            if (string.IsNullOrEmpty(car.Year.ToString()))
                errors.AppendLine("Заполите строку2");
            if (string.IsNullOrEmpty(car.Price.ToString()))
                errors.AppendLine("Заполите строку3");
            if(errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }
            if (car.Id == 0)
                AutoStoreEntities.GetContext().Cars.Add(car);

            try
            {
                AutoStoreEntities.GetContext().SaveChanges();
                MessageBox.Show("Ифнормация сохранена");
                Manager.MainFrame.GoBack();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
