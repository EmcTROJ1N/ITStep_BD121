   M S B A M L   `   `   `  ����D  @Snowballs, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:Snowballs	Snowballs  5   	       Snowballs.MainWindow   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   )localclr-namespace:Snowballs     5   	   -   5          � Title$  
MainWindow��5   	   $	��450��6   $	��800��6)    � WindowState$ 	Maximized=�5	   	   .��5
      � ���	#FF08002D � RowDefinitions 5   
   �� 5      $	��13* �6   �� 5      .+�5   
   �� -   5         ��Name$ Field��5      $��0��6    .+�5      �� -   5         ��Name$ 
Background��5      $��/Resource/bgsnow.jpg��6(    K� ZIndex$ -1��6|   ��6F   ��  �� ElementName$ Field�� �� Path$ ActualWidth�6|   5      � 5   
   $��1��6   $��10q�6    � ColumnDefinitions 5      �� 5      �� 5      �� 5      .+�5      �� -   5        	 ��Name$	 HealthLabel��5      
�������$��Health: ��6=   $��Arial)�6P   $��40(�6c   $��0��6q   $��Center=�6�   ��6�   Z� � 5      -   5        
 �Name$
 	HealthBar��5      $��1��6-   �� 5      -   5        $	 
ShotsLabel��5      
�������$��Count shots: ��6;   $��Arial)�6S   $��40(�6f   $��2��6t   $��Center=�6�   5      5       5!      