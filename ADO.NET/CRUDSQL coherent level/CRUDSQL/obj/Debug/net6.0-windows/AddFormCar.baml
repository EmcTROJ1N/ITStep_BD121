   M S B A M L   `   `   `  ����B  >CRUDSQL, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null!clr-namespace:CRUDSQLCRUDSQL  5   	       CRUDSQL.AddFormCar   5   	   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   'localclr-namespace:CRUDSQL     5   	     � Title$  AddForm��5   	   $	��200��6   $	��400��6&   .��5	      � $��10q�6    � ColumnDefinitions 5
   
   �� 5      �� 5       � RowDefinitions 5   
   �� 5      �� 5      �� 5      �� 5      �� 5      �� 5      .+�5   
   �� $��Add car��6   $��0��6#   $��0��60   $��2��6@    �� TextAlignment$ Center=�6T   �� 5   
   $��	OwnerID: ��6   $��0��6%   $��1��65   �� 5   
   -   5         ��Name$ CarNameListBox��5      $��1��6+   $��1��6;   �� 5   
   $��Car ID: ��6   $��0��6$   $��2��64   �� 5   
   -   5         ��Name$ CarIDTextBox��5      $��1��6(   $��2��68   �� 5   
   $��Car Model: ��6   $��0��6'   $��3��67   �� 5   
   -   5        $ CarModelTextBox��5      $��1��6+   $��3��6;   �� 5!   
   $��Car max speed: ��6   $��0��6+   $��4��6;   �� 5"   
   -   5        $ CarMaxSpeedTextBox��5"      $��1��6.   $��4��6>   �� 5$   
   -   5         ��Name$ Complete��5$      $
��Okay��6#   $��0��6I   $��5��6Y   $��2��6f   5%      5&      