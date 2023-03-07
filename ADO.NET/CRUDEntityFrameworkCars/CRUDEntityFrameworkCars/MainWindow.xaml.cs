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

namespace CRUDEntityFrameworkCars
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        CarsEntities Context = new CarsEntities();
        public MainWindow()
        {
            InitializeComponent();
            Table.ItemsSource = (from car in Context.Cars select car).ToList();
        }

        private void Table_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            // Выделенная строка в таблице с НОВЫМИ данными (режим UpdateSourceTrigger=PropertyChanged)
            Car selectedRow = Table.SelectedItem as Car;

            // Получить из БД ссылку на редактируемый объект в базе
            Car selectedDB = (from p in Context.Cars
                                where p.id == selectedRow.id
                                select p).FirstOrDefault();

            if (selectedDB == null)
            {
                Context.Cars.Add(selectedRow);
                Context.SaveChanges();
                Table.ItemsSource = (from car in Context.Cars select car).ToList();
                return;
            }

            // Перенести данные из таблицы в объект в БД
            selectedDB.id = selectedRow.id;
            selectedDB.brand = selectedRow.brand;
            selectedDB.model = selectedRow.model;
            selectedDB.speed = selectedRow.speed;
            selectedDB.price = selectedRow.price;
            selectedDB.year = selectedRow.year;
            selectedDB.photo = selectedRow.photo;

            Context.SaveChanges();
            Table.ItemsSource = (from car in Context.Cars select car).ToList();
        }
    }
}
