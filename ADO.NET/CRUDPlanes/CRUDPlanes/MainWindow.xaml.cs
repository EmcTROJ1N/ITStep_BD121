using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace CRUDPlanes
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Planes _Planes = new Planes();

        public MainWindow()
        {
            InitializeComponent();
            Update();
        }

        public void Update()
        {
            var result = from t in _Planes.plane
                         select new plane
                         {
                             id = t.id,
                             brand = t.brand,
                             model = t.model,
                             seats = t.seats,
                             volume = t.volume,
                             photo = t.photo,
                         };

            // Коллекция используется для поддержки фильтрации
            ObservableCollection<plane> observableCollection = new ObservableCollection<plane>(result);

            CollectionViewSource collection = new CollectionViewSource() { Source = observableCollection };
            //collection.GroupDescriptions.Add(new PropertyGroupDescription("State"));
            Table.ItemsSource = collection.View;
        }


        // запускается перед окончанием редактирования
        private void dataGrid1_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            // Выделенная строка в таблице с НОВЫМИ данными (режим UpdateSourceTrigger=PropertyChanged)
            plane selectedRow = Table.SelectedItem as plane;

            // Получить из БД ссылку на редактируемый объект в базе
            plane selectedDB = (from p in _Planes.plane
                                where p.id == selectedRow.id
                                select p).FirstOrDefault();

            if (selectedDB == null)
            {
                _Planes.plane.InsertOnSubmit(selectedRow);
                _Planes.SubmitChanges();
                Update();
                return;
            }

            // Перенести данные из таблицы в объект в БД
            selectedDB.id = selectedRow.id;
            selectedDB.brand = selectedRow.brand;
            selectedDB.model = selectedRow.model;
            selectedDB.seats = selectedRow.seats;
            selectedDB.volume = selectedRow.volume;
            selectedDB.photo = selectedRow.photo;

            // Синхронизировать изменения с БД
            _Planes.SubmitChanges();
            Update();

            // Перечитать данные из БД
        }
    }
}
