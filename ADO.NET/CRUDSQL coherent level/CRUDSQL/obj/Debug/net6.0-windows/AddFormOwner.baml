   M S B A M L   `   `   `  ����B  >CRUDSQL, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null!clr-namespace:CRUDSQLCRUDSQL  5   	       CRUDSQL.AddFormOwner   5   	   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   'localclr-namespace:CRUDSQL     5   	     � Title$  AddFormOwner��5   	   $	��200��6   $	��400��6+   .��5	      � $��10q�6    � ColumnDefinitions 5
   
   �� 5      �� 5       � RowDefinitions 5   
   �� 5      �� 5      �� 5      �� 5      .+�5   
   �� $��	Add owner��6   $��0��6%   $��0��62   $��2��6B    �� TextAlignment$ Center=�6V   �� 5   
   $��	OwnerID: ��6   $��0��6%   $��1��65   �� 5   
   -   5         ��Name$ OwnerIDTextBox��5      $��1��6*   $��1��6:   �� 5   
   $��
Fullname: ��6   $��0��6&   $��2��66   �� 5   
   -   5        $ FullNameTextBox��5      $��1��6+   $��2��6;   �� 5   
   -   5         ��Name$ Complete��5      $
��Okay��6#   $��0��6I   $��3��6Y   $��2��6f   5      5      