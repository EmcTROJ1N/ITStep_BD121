   M S B A M L   `   `   `  ����@  <Chess, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:ChessChess  5   	       Chess.ChessBoardForm   5   	   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   %localclr-namespace:Chess     5   	   -   5          �Name$  MyWindow��5   	    � Title$ Chess��5	   	   $	��640��6   $
��1000��6$   
�������$��Book Antiqua)�5
       � Icon$ 	/icon.png��66   .��5      (�  (� StretchDirection$
 Both=�6   .��5   
   ]� $
��True��6   $	��680��6)   $
��1050��66   .+�5      r� -   5         r�Name$ 
TickPlayer��5   \    r� Source$ TimerTickSound.mp3?�6    r� Volume$ .1Q�6p   o� 5      $	��Top=�6   $
��Auto��6(   $��0 0 10 0q�66   .@�5      m� -   5        $��Новая игра��5      $��Book Antiqua)�6?   $��15(�6Y   m� 5      -   5        $!��Сохранить Игру��5      $��Book Antiqua)�6D   $��15(�6^   m� 5      -   5        $!��Сохранить ходы��5      $��Book Antiqua)�6E   $��15(�6_   m� 5      -   5        $'��!Перевернуть доску��5      $��Book Antiqua)�6J   $��15(�6d   m� 5      -   5        $'��!Удалить подсказки��5      $��Book Antiqua)�6I   $��15(�6c   m� 5      -   5        $��Выйти из игры��5      $��Book Antiqua)�6?   $��15(�6Y   5      �� 5      -	   5         ��Name$ EatenFirstTeam��5   0   $��Right=�6   $��0 0 5 0q�6H   $��50��6Y   �� 5      -
   5        $ EatemSecondTeam��5   0   $��Right=�6   $��0 0 5 0q�6I   $��50��6Z   �� 5      $��Right=�6   $
��Auto��62   $��	10 0 10 0q�6?   .��5      �� -   5        $
 Info��5   .   $
��Auto��6!   .+�5      �� -   5         ��Name$ TimeLeftLabelTeamFirst��5   B   $��First team time left: ��6$   $
��Wrap=�6b   $��Book Antiqua)�6v   $��20(�6�   �� 5      -   5        $ TimeLeftLabelTeamSecond��5   C   $��Second team time left: ��6$   $
��Wrap=�6d   $��Book Antiqua)�6x   $��20(�6�   �� 5       -   5        	 ��Name$	 NotationView��5    #   
�������$��20(�5!   3   5"      5#      �� 5$      -   5        
 ��Name$
 borderField��5$      ���	#FF0596C7$��5q�6B   $��Bottom=�6V   $
��Auto��5%      ��5$   n   ��  �� ElementName$ borderField�� �� Path$ ActualWidth�._�5&      � -   5         �Name$ 
ChessBoard��5&       � ColumnDefinitions 5'      �� 5(      $
��Auto �6+   �� 5)      �� 5*      �� 5+      �� 5,      �� 5-      �� 5.      �� 5/      �� 50       � RowDefinitions 52      �� 53      $
��Auto �6(   �� 54      �� 55      �� 56      �� 57      �� 58      �� 59      �� 5:      �� 5;      .+�5=      � -   5        $ Field��5=      $��1��6*   $��1��67   $��8��6G   $��8��6X    5>      �� 5?      $��1* �6,    5A      �� 5B      $��1* �6/   5D      5E      5F      5G      5H      5I      