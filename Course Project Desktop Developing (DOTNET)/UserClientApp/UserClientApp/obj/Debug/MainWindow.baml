   M S B A M L   `   `   `  ����H  DUserClientApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null-clr-namespace:UserClientAppUserClientApp  5   	       UserClientApp.MainWindow   5   	   _ [MaterialDesignThemes.Wpf, Version=4.8.0.0, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5   	   _ [Hardcodet.NotifyIcon.Wpf, Version=1.1.0.0, Culture=neutral, PublicKeyToken=682384a853a08aad)tb http://www.hardcodet.net/taskbar  5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   -localclr-namespace:UserClientApp     5   	   -   5          � Title$  User client��5
   	   $	��450��6   $	��800��6*    � Icon$ /UserIcon.ico��5   	   .��5      �  � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $��* �6   �� 5      $
��Auto �6   .+�5   
   3  -Hardcodet.Wpf.TaskbarNotification.TaskbarIcon  -   5          Name$
 Icon��5      $y�Hidden=�6'      ToolTipText$ 	I see you��6;      
IconSource$ /UserIcon.ico��6S    � ContextMenu 5      �� 5      .@�5      m� -   5        $��Log out��5      60   m� 5      -   5        $
��Quit��5      6-   5      5      o� 5   
   $��0��6   .@�5      m� -   5         m�Name$ ConnectionStatusItem��5   @   $%��Connection status: Disconnected��6   $��16(�6^   $��30��6l   5      �� 5!   
   -   5         ��Name$
 test��5!      #  MaterialDesignThemes.Wpf.Card  5#   
   $��1��6   $��6q�6+   $��16q�67   $��35��6C   .��5$      �� $��Remote control��6   $��15(�6.   5%        5'   
   $��2��6   $��6q�6+   $��16q�67   $
��Auto��6C   .��5(      �� $��Center=�6   $��Center=�66   .+�5)      �� -   5        	 ��Name$	 LoginTextBox��5)   0   $��Your login: ��6   $��25(�6F   $��5q�6T   �� 5*      -   5        $	 ConnectorIDTextBox��5*   2   $��Connector ID: ��6   $��25(�6N   $��5q�6\   �� 5+      -	   5        $��Log In��5+      $��5q�68   $��15(�6C   �� 5,      -
   5        $��Log Out��5,      $��5q�6?   $��15(�6J   �� 5-      -   5        $��Register��5-      $��5q�6=   $��15(�6H   5.      5/      o� 51   
   $��3��6   .@�52      m� -   5        $ ConnectionWithItem��52   2   $��Connection with: ��6   $��16(�6N   $��30��6\   m� 53      -   5        $��Remove from startup��53      $��16(�6I   $��30��6W   54      55      56      