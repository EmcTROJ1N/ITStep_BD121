using System.ComponentModel;
using System.Windows;

namespace WPFWebApiCRUD;

public partial class GetId : Window
{
    private IdContainer id;
    public GetId(IdContainer container)
    {
        InitializeComponent();
        id = container;
    }

    private void ButtonBase_OnClick(object sender, RoutedEventArgs e) =>
        this.Close();
    private void GetId_OnClosing(object? sender, CancelEventArgs e) =>
        int.TryParse(IdTextBox.Text, out id.Id);
}