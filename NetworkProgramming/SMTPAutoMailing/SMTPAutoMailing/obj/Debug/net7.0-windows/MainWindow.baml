   M S B A M L   `   `   `  ����J  FSMTPAutoMailing, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:SMTPAutoMailingSMTPAutoMailing  5   	        SMTPAutoMailing.MainWindow   5   	   W SPresentationCore, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   /localclr-namespace:SMTPAutoMailing     5   	     � Title$  
MainWindow��5   	   $	��500��6   $	��800��6)   .��5	   
   �  � RowDefinitions 5
      �� 5      $
��Auto �6   �� 5      �� 5      �� 5      $
��Auto �6   �� 5      $
��Auto �6    � ColumnDefinitions 5      �� 5      $
��Auto �6'   �� 5      .+�5   
   �� $��0��6   $��0��6"   $��10q�62   .+�5      �� -   5         ��Name$ LoginTextBox��5      $��Login��6,   $��0 0 0 10q�69   G� 5      -   5         G�Name$ 	PasswdBox��5      $��0 0 0 5q�6-   5      �� 5   
   -   5         ��Name$ EmailListView��5   0   $��1��6   $��0��6    $��10q�6G   �� 5    
   $��2��6   $��0��6"    �� Orientation$ Vertical=�62   $��10q�6I   .+�5!      �� -   5        $ EmailTextBox��5!      $	��200��6,   $��0,0,0,10q�68   �� 5"      -   5         ��Name$ 	AddButton��5"      $	��Add��6(   66   �� 5#      -   5        $ DeleteButton��5#   &   $��0 5 0 0q�6   $��Delete selected mail��6<   6[   �� 5$      -   5        $ DeleteAllButton��5$   &   $��0 5 0 0q�6   $��Delete all mail��6?   6Y   �� 5%      -   5        $ StartButton��5%      $��Start��6*   $��0 10 0 0q�6T   5&      �� 5+   
   $��1��6   $��0��6%   $��4��62   $��10q�6C   .+�5,      �� -	   5        $ SubjectTextBox��5,       �� AcceptsReturn$
 True��6.   $��20��6C   $��0 0 0 10q�6O   $!��Тема сообщения��6a   �� 5-      -
   5        $ MessageTextBox��5-      	 �� TextWrapping$
	 Wrap=�6.   $
 True��6B   $	��150��6W   �� 5.      -   5        $ AttachmentsListView��5.   4   $��0��6   $��0��6$   $��	0 10 0 10q�6Q   $	��100��6d   �� 5/      -   5        $ AttachmentButton��5/      $��	0 10 0 10q�6/   $��Attach File��6B   6X   50      � 53   
   -   5        
 �Name$
 MailingProgress��53   S   $��0��6   $��2��6&   $��3��6:   $��30��6G   $��10q�6l   54      55      