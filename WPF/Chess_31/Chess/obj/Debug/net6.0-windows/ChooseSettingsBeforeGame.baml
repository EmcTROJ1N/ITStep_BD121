   M S B A M L   `   `   `  ����@  <Chess, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:ChessChess  5   	   $    Chess.ChooseSettingsBeforeGame   5   	   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   %localclr-namespace:Chess     5   	     � Title$/  )Предыгровые настройки��5   	   $	��326��6'   $	��413��64   ���#ADD8E6 � 
ResizeMode$ 	CanResize=�6U    � Icon$ 	/icon.png��6l   .��5	      (� .��5
   
   �  � RowDefinitions 5      �� 5      $
��auto �6    �� 5      $
��auto �6    �� 5      $
��auto �6    �� 5      $
��auto �6    �� 5      $
��auto �6    �� 5      $
��auto �6    �� 5      $
��auto �6    �� 5       � ColumnDefinitions 5      �� 5      $
��auto �6#   �� 5      $
��auto �6#   .+�5      �� $.��(Установите сложность:��6   $��0��64   $��0��6A   $��	10,10,0,0q�6Q   $��Book Antiqua)�6d   $��15(�6~   $
��Bold"�6�   
���   �6�   �� 5      -   5         ��Name$ difficultyComboBox��5   /   $��0��6K   $��1��6X   $��10,10,10,10q�6h   $��15(�6}   ���#FFFFFF
���   ���6   �� ��5      �� 5      .R�5      ��  �� Orientation$ 
Horizontal=�6%   .+�5      �� $��15��6D   $��15��6O   $��5,0,5,0q�6[   ��6(   �� *	RectColor+6[   �� 5      ��6(   �� *Title+5       5!      5#      �� 5$      $H��BУкажите время для команд (в минутах):��6   $��1��6D   $��0��6Q   $��	10,10,0,0q�6a   $��15(�6t   $
��Bold"�6�   $��Book Antiqua)�6�   
���   �6�   �� 5%      -   5         ��Name$ timeTextBox��5%      $��1��6+   $��1��68   $��10,10,10,10q�6H   $��Book Antiqua)�6]   $��15(�6w    �� TextWrapping$
 Wrap=�6�   $	��120��6�   ���#FFFFFF
���   �.��6�   306�   �� 5&      $��Ваш оппонент:��6   $��2��6,   $��0��69   $��	10,10,0,0q�6I   $
��Bold"�6]   $��15(�6o   $��Book Antiqua)�6}   
���   �6�   �� 5'      -   5        $ opponentComboBox��5'      $��2��61   $��1��6>   $��10,10,10,10q�6N   $��15(�6c   ���#FFFFFF
���   �$��Book Antiqua)�6�   .@�5(      �� $��PC��6   �� 5)      $��Friend��6   5*      �� 5+      $��Шахматы 960��6   $��3��6*   $��0��67   $��10q�6G   $
��Bold"�6T   $��15(�6f   $��Book Antiqua)�6t   
���   �6�   �� 5,      -   5        	 ��Name$	 CheckBoxPlay960��5,      $��3��60   $��1��6=   $��
10 10 0 10q�6M   6a   � 5-      $��4��6   $��2��6     5.      �� 5/      �� 50      .+�52      �� -   5        
 ��Name$
 WhiteBorder��52      
������� �� Padding$ 10q�6D   $��0��6Q   ._�53      �� -   5         ��Name$ 	WhiteTeam��53      $��/wk.png��6/   54      �� 55      -   5        $
 BlackBorder��55      $��2��6.   
���   �$ 10q�6V   $��1��6c   $��5q�6s   ._�56      �� -   5        $ 	BlackTeam��56      $��/bk.png��6/   57      58      �� 59      -	   5        $��5��59      $��2��6"      shadowButtonTemplate#��[  67   $��15(�6h   $��30��6v   $��Book Antiqua)�6�   $��Начать игру��6�   $��0 0 15 0q�6�   5:      5;      5<      