   M S B A M L   `   `   `  ����E  AChatClient, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'clr-namespace:ChatClient
ChatClient  5   	       ChatClient.MainWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   *localclr-namespace:ChatClient     5   	   -   5          �Name$  
RootWindow��5   B    � Title$ Chatting client: logout��6	   $	��450��6)   $	��800��66   .��5	      ]� ��. .+�5
   
   o� -   5         o�Name$ MenuElem��5
   $   $	��Top=�6   .@�5      m� -   5        $
��Auth��5      6%   �� 5      -   5         ��Name$ LoginTextBox��5   "   $	��100��6   6"   m� 5      -   5        $
��Exit��5      6%   5      �� 5   
   $
��Left=�6   $	��250��6+   .+�5      �� -   5        $ ConnectionTextBox��5   !   $��5q�6   6!   �� 5       �� Orientation$ 
Horizontal=�6   $��5q�62   .+�5      �� -   5        $��Open connection��5      $��0 0 5 0q�6J   �� 5      -   5        $��Close selected��5      $��0q�62   6=   5      �� 5      -	   5         ��Name$ ChatsListView��5      6.   5      � 5   
    � RowDefinitions 5      �� 5      $
��Auto �6    �� 5      �� 5      $
��Auto �6    .+�5       �� -
   5         ��Name$! CurrentInterviewerTextBlock��5    %   $��0��6   6%   �� 5"      -   5        $ ChatView��5"   $   $��1��6   6$   � 5#      $��2��6    � ColumnDefinitions 5$      �� 5%      $��* �6'   �� 5&      $
��Auto �6'   .+�5(      �� -   5        $ MessageTextBox��5(   ;   $��0��6   $��0 3 3 3q�6*   6;   �� 5)      -   5        $��1��5)      $��=>��6)   $��0 3 3 3q�66   6G   5*      5+      5,      5-      