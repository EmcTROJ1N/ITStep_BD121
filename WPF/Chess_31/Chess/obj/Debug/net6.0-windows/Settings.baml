   M S B A M L   `   `   `  ����@  <Chess, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:ChessChess  5   	       Chess.Settings   5   	   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   %localclr-namespace:Chess     5   	   ] YXceed.Wpf.Toolkit.NET5, Version=4.5.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f47toolkit)http://schemas.xceed.com/wpf/xaml/toolkit  5   	     � Title$  Настройки��5	   	   $	��300��6   $	��480��6(   ���#ADD8E6 � 
ResizeMode$ NoResize=�6I    � Icon$ 	/icon.png��6_   .��5
      �  � RowDefinitions 5   
   �� 5      �� 5      �� 5      �� 5      �� 5      �� 5      �� 5      �� 5       � ColumnDefinitions 5   
   �� 5      �� 5      .+�5   
   �� $��0��6   $��0��6!   $��15(�61   $&�� Цвет выделенного:��6?   $��Book Antiqua)�6X   
���   �$��5q�6�   #  Xceed.Wpf.Toolkit.ColorPicker  5   
   -   5          Name$ SelectedFigureColor��5      $��0��6;   $��1��6H   $��5q�6X   �� 5   
   $��1��6   $��0��6!   $��15(�61   $"��Посветка ходов:��6?   $��Book Antiqua)�6V   
���   �$��5q�6�     5   
   -   5        $ SelectedStepColor��5      $��1��69   $��1��6F   $��5q�6V   �� 5    
   $��2��6   $��0��6!   $��15(�61   $��Цвет тегов:��6?   $��Book Antiqua)�6R   
���   �$��5q�6     5!   
   -   5        $ 	TagsColor��5!      $��2��61   $��1��6>   $��5q�6N   �� 5#   
   $��3��6   $��0��6!   $��15(�61   $)��#Цвет темной ячейки:��6?   $��Book Antiqua)�6Z   
���   �$��5q�6�     5$   
   -   5        $ DarkCellColor��5$      $��3��65   $��1��6B   $��5q�6R   �� 5&   
   $��4��6   $��0��6!   $��15(�61   $+��%Цвет светлой ячейки:��6?   $��Book Antiqua)�6[   
���   �$��5q�6�     5'   
   -   5        $ LightCellColor��5'      $��4��66   $��1��6C   $��5q�6S   �� 5)   
   $��5��6   $��0��6!   $��15(�61   $-��'Выберите набор фигур:��6?   $��Book Antiqua)�6\   
���   �$��5q�6�   �� 5*   
   -   5         ��Name$ ComboboxSprites��5*      $��5��6,   $��1��69   $��5q�6I   ��5+      �� 5,      .R�5-      ��  �� Orientation$ 
Horizontal=�6!   .+�5.      �� ��6    �� *Image+��69   ��  �� ElementName$ ComboboxSprites��	 �� Path$	 ActualHeight��� 5/      ��6$   �� *Title+50      51      53      �� 55   
   -   5        $��6��55      $��0��6   $��Применить��6.      shadowButtonTemplate#��[  6B   $��Book Antiqua)�6s   
���   �$��5q�6�   �� 56   
   -   5        $��6��56      $��1��6   $��Отмена��6.   #��[  6?   $��Book Antiqua)�6p   
���   �$��5q�6�   �� 57   
   -	   5        $��7��57      $��2��6   $?��9Сбросить к исходным настройкам��62   #��[  6[   $��Book Antiqua)�6�   
���   �$��5q�6�   58      59      