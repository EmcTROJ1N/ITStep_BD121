   M S B A M L   `   `   `  ����H  DWPFWebApiCRUD, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null-clr-namespace:WPFWebApiCRUDWPFWebApiCRUD  5   	   ;"clr-namespace:WPFWebApiCRUD.ModelsWPFWebApiCRUD.Models  5   	       WPFWebApiCRUD.MainWindow   5   	   W SPresentationCore, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   -localclr-namespace:WPFWebApiCRUD     5   	   5models"clr-namespace:WPFWebApiCRUD.Models     5   	     � Title$  
MainWindow��5	   	   $	��450��6   $	��800��6)   .��5
   
   �� .@�5      �� $
��Cars��6   .��5      ]� $
��True��6$   .+�5   "   o� $	��Top=�6'   $��22��6<   .@�5   *   m� -   5        
 � Tag$ api/cars��5   3   $��Update��6[   m� 5   *   -   5        $ api/cars��5   3   $
��Edit��6[   m� 5   *   -   5        $ api/cars��5   3   $��Delete��6[   m� 5   *   -   5        $ api/cars��5   3   $��Create��6[   5   #   &   System.Windows.Controls.DataGrid  5   "   -   5        $��CarsDataGrid��5   +   $��
0, 0, 0, 0q�5   +   
�������   HorizontalGridLinesBrush
 �����   RowBackground
 �����   AlternatingRowBackground
 �����5   #   5      5      �� 5      $��Owners��6   .��5      ]� $
��True��6$   .+�5   "   o� $	��Top=�6'   .@�5   *   m� -   5        $ 
api/owners��5   3   $��Update��6]   m� 5   *   -   5        $ 
api/owners��5   3   $
��Edit��6]   m� 5   *   -   5        $ 
api/owners��5   3   $��Delete��6]   m� 5   *   -	   5        $ 
api/owners��5   3   $��Create��6]   5    #     5!   "   -
   5        $��OwnersDataGrid��5"   +   $��
0, 0, 0, 0q�5!   +   
�������
 �����
 �����
 �����5$   #   5%      5&      5'      5(      