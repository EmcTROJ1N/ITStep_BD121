   M S B A M L   `   `   `  ����B  >CRUDSQL, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null!clr-namespace:CRUDSQLCRUDSQL  5   	       CRUDSQL.MainWindow   5   	   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   'localclr-namespace:CRUDSQL     5   	     � Title$  
MainWindow��5   	   $	��450��6   $	��800��6)   .��5	      �  � ColumnDefinitions 5
   
   �� 5      �� 5      .+�5   
   �� -   5         ��Name$
 Tabs��5   %   $��0��6   .@�5      �� $
��Auto��6   .��5      �� -   5         ��Name$ 	TableAuto��5       �� View 5      �� 5      .P�5      ��  �� DisplayMemberBinding 6-   ��  �� Path$ CarID�.��6Y   Car ID6a   �� 5       6-   �� $ Model�.��6Y   Model6`   �� 5       6-   �� $ MaxSpeed�.��6\   
MaxSpeed6f   �� 5       6-   �� $ OwnerID�.��6[   
Owner ID6e   5      5      5      �� 5      $��Owners��6   .��5      �� -   5        $ TableOwners��5       5      �� 5      .P�5       ��  6-   �� $ OwnerID�.��6[   
Owner ID6e   �� 5!       6-   �� $ FullName�.��6\   
Owner ID6f   5"      5$      5%      5&      �� 5'   
   -   5         ��Name$ 
stackpanel��5'   <   $��1��6    �� Orientation$ Vertical=�6%   .+�5)      �� -   5        $��Add (or Update)��5)      $��3q�6@   �� 5*      -   5        $��Delete��5*      $��3q�6:   �� 5,      
���   �$��2q�6)   $��3q�6=   ._�5-      �� -   5        $��Update��5-      6*   5.      5/      50      51      