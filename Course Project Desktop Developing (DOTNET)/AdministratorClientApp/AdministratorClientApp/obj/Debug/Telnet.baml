   M S B A M L   `   `   `  ����Q  MAdministratorClientApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null?$clr-namespace:AdministratorClientAppAdministratorClientApp  5   	   #    AdministratorClientApp.Telnet   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   6local$clr-namespace:AdministratorClientApp     5   	   _ [MaterialDesignThemes.Wpf, Version=4.8.0.0, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5   	     � Title$  Telnet��5	   	   $	��450��6   $	��800��6%   
������� � Icon$ /AdminIcon.ico��6G   .��5
      ]� ��. .+�5   
   o� $	��Top=�6   .@�5      m� -   5        $��New session��5      $��25��6D   $��13(�6P   5      �� 5   
   $��Bottom=�6   $��3q�6)   
�������._�5      �� -   5         ��Name$ CommandTextBox��5      
��� d �
���   �$��Terminal)�5   %   $��15(�6;   $
��Left=�5      $��Center=�6-   $��30��5      ��6   ��  �� ElementName$ TelnetRichTextBox�� �� Path$ ActualWidth�5      �� 5   
   -   5         ��Name$ TelnetRichTextBox��5      
���   �
��� d � �� 
IsReadOnly . $
��Left=�5      $	��Top=�61   $��15(�5      $��Terminal)�6$   5      5      5       