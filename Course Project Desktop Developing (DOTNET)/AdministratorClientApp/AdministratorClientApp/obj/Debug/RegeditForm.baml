   M S B A M L   `   `   `  ����Q  MAdministratorClientApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null?$clr-namespace:AdministratorClientAppAdministratorClientApp  5   	   (    "AdministratorClientApp.RegeditForm   5   	   _ [MaterialDesignThemes.Wpf, Version=4.8.0.0, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   6local$clr-namespace:AdministratorClientApp     5   	     � Title$  RegeditForm��5	   	   $	��450��6   $	��800��6*    � WindowState$ 	Maximized=�66    � Icon$ /AdminIcon.ico��6N   .��5
      �  � RowDefinitions 5   
   �� 5      $��* �6   �� 5      $
��Auto �6    � ColumnDefinitions 5   
   �� 5      $
��0.3* �6   �� 5      $
��0.6* �6   .+�5   
   R� -   5         R�Name$ FoldersTree��5   0   $��0��6   $��0��6     � ContextMenu 5      �� 5      .@�5      m� -   5        $��Delete��5      6/   m� 5      -   5        $��Rename��5      6/   5      5      &   System.Windows.Controls.DataGrid  5   
   -   5          Name$ ValuesDataGrid��5   0   $��0��6   $��1��6       
IsReadOnly . 	   AutoGenerateColumns	 .  
   CanUserAddRows
 .     CanUserDeleteRows .     Columns 5!      0  *System.Windows.Controls.DataGridTextColumn  5"      ,  &System.Windows.Controls.DataGridColumn   Header$
 Name��6%   1  +System.Windows.Controls.DataGridBoundColumn   Binding 63   �� *Name+  5#      $
 Type��6%    63   �� *Type+ �� UpdateSourceTrigger$ PropertyChanged=�  5$      $ Value��6%    64   �� *Value+$ PropertyChanged=� 5&      �� 5'      .@�5(      m� -   5        $��Delete��5(      6/   m� 5)      -   5        $��Rename��5)      6/   5*      5,      �� 5-   
   -   5         ��Name$ PathTextBlock��5-   O   $��10q�6   $��15(�6    $��2��6.   $��2��6;   $��30��6f   5.      5/      