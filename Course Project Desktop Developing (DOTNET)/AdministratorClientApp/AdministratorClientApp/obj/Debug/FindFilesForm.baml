   M S B A M L   `   `   `  ����Q  MAdministratorClientApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null?$clr-namespace:AdministratorClientAppAdministratorClientApp  5   	   *    $AdministratorClientApp.FindFilesForm   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   6local$clr-namespace:AdministratorClientApp     5   	   -   5          � Title$  FindFilesForm��5   	   $	��550��6   $	��900��6,    � Icon$ /AdminIcon.ico��6O    � 	Resources 5
         GroupHeaderStyle �� 
TargetType �� Property �� Value �� 
TargetType 9�Name 9� 
IsExpanded��%  &	        �� 5   
   " ��6)   .�5      ��  � �� 5      �� 5      " ��6&   .R�5      9� $	 exp��6#    . 
�������
���   ���5      �� 5   "   ��6,   �� *Name+.��5      �� 5      5      5      5      5      .��5      � 	 � RowDefinitions	 5   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      �� 5       $
��Auto �6   �� 5!      $
��Auto �6   .+�5$   
   �� 
 �� Orientation$
 
Horizontal=�6   .+�5%      �� $��Searching mask: ��6   $��15(�60   $��Center=�6>   $��Center=�6[   $��5q�6v   �� 5&      -   5         ��Name$ MaskTextBox��5&      $��5q�6+   $	��100��66   �� 5'      -   5        $��Start search��5'      $��5q�6,   67   �� 5(      -   5        $��Pause search��5(      $��5q�6,   67   �� 5)      -   5        $��Resume search��5)      $��5q�6-   68   �� 5*      -   5        $��Stop search��5*      $��5q�6+   66   5+      �� 5-   
   $��1��6   $
 
Horizontal=�6"   .+�5.      �� $��Searching path: ��6   $��15(�60   $��Center=�6>   $��Center=�6[   $��5q�6v   �� 5/      -   5        $ 
FolderPath��5/      $	��C:\��6*   $��5q�65   $	��300��6@   �� 50      -   5        $��Browse��50      6&   51      &   System.Windows.Controls.DataGrid  53   
   -	   5          Name$ FilesDataGrid��53   2   $��2��6      
IsReadOnly .    AutoGenerateColumns .   �� 
GroupStyle 54      (  "System.Windows.Controls.GroupStyle  55         ContainerStyle# [  6    � ContextMenu 57      �� 58      .@�59      m� -
   5        $��Open on remote PC��59      6:   5:         Columns 5<      0  *System.Windows.Controls.DataGridTextColumn  5=      ,  &System.Windows.Controls.DataGridColumn   Header$ FileName��6%   1  +System.Windows.Controls.DataGridBoundColumn   Binding 67   �� *Name+4  .System.Windows.Controls.DataGridCheckBoxColumn  5>      $ 
IsReadOnly��6)    6>   �� *
IsReadOnly+  5?      $ LastAccessTime��6%    6=   �� *LastAccessTime+  5@      $ LastWriteTime��6%    6<   �� *LastWriteTime+  5A      $ Length��6%    65   �� *Length+   RowDetailsTemplate 5C      5  -System.Windows.Controls.ItemContainerTemplate�  5D      .R�5E      �� ��6    �� *
FullName+5F      5H      �� 5J   
   -   5        $��3��5J      $��Check searching status��6   $��15q�6T   5K      5L      