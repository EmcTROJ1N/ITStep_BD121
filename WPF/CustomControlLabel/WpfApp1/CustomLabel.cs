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

namespace WpfApp1
{
    /// <summary>
    /// Выполните шаги 1a или 1b, а затем 2, чтобы использовать этот пользовательский элемент управления в файле XAML.
    ///
    /// Шаг 1a. Использование пользовательского элемента управления в файле XAML, существующем в текущем проекте.
    /// Добавьте атрибут XmlNamespace в корневой элемент файла разметки, где он 
    /// будет использоваться:
    ///
    ///     xmlns:MyNamespace="clr-namespace:WpfApp1"
    ///
    ///
    /// Шаг 1б. Использование пользовательского элемента управления в файле XAML, существующем в другом проекте.
    /// Добавьте атрибут XmlNamespace в корневой элемент файла разметки, где он 
    /// будет использоваться:
    ///
    ///     xmlns:MyNamespace="clr-namespace:WpfApp1;assembly=WpfApp1"
    ///
    /// Потребуется также добавить ссылку из проекта, в котором находится файл XAML,
    /// на данный проект и пересобрать во избежание ошибок компиляции:
    ///
    ///     Щелкните правой кнопкой мыши нужный проект в обозревателе решений и выберите
    ///     "Добавить ссылку"->"Проекты"->[Поиск и выбор проекта]
    ///
    ///
    /// Шаг 2)
    /// Теперь можно использовать элемент управления в файле XAML.
    ///
    ///     <MyNamespace:CustomLabel/>
    ///
    /// </summary>
    public class CustomLabel : Label
    {
        public int VowelsCount
        {
            get
            {
                int count = 0;
                foreach (var symb in (Content as string).ToLower())
                    if (new char[] { 'a', 'e', 'i', 'o', 'u', 'y' }.Contains(symb))
                        count++;
                return count;
            }
        }
        public int DigitsCount
        {
            get
            {
                int count = 0;
                foreach (var symb in (Content as string))
                    if (new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' }.Contains(symb))
                        count++;
                return count;
            }
        }
        public int LettersCount { get => (Content as string).Length; }

        static CustomLabel()
        {
            //DefaultStyleKeyProperty.OverrideMetadata(typeof(CustomLabel), new FrameworkPropertyMetadata(typeof(CustomLabel)));
        }
    }
}
