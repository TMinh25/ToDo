   M S B A M L   `   `   `  ����B  >ToDoApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null!clr-namespace:ToDoAppToDoApp  5   	       ToDoApp.PromptDialog   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   'localclr-namespace:ToDoApp     5   	   -   5          � Title$   ��5   	   $	��175��6   $	��252��6    � WindowStartupLocation$ CenterScreen=�6+    � 
ResizeMode$ NoResize=�6y    � WindowStyle$
 None=�6�    � AllowsTransparency . 
������  � ShowInTaskbar .   � SizeToContent$ Height=�6�    � 	Resources 5	       �� Source�� 5
   
   $ ./style.xaml?�6   %    .��5      �� $
��Auto��6   	 �� Padding$	 12q�6   
 �� CornerRadius$
 10��6(   ���#F5F5F6._�5   
   �  � ColumnDefinitions 5      �� 5      $��5* �6#   �� 5       � RowDefinitions 5      �� 5      $��1* �6    �� 5      $��4* �6    .+�5      �� -   5        $��1��5   ,    � Cursor$
 Hand��6<   $��28��6J   $��28��6V   $	��Top=�6a   $��Right=�6y   
������ 
������    btn#��[  6�   .��5      �� $"��Assets/Images/exitButton.png��6   5      � 5      -   5        $
��grid��5   4   $��1��6   $��2��6     5      �� 5      $
��Auto �6$   �� 5      $
��Auto �6$   �� 5      $
��Auto �6$   .+�5      �� -   5        $��lblQuestion��5      .��6+   
Tác Vụ:64   �� 5       -   5        $��answerInput��5       $��1��6,   
�������  EntryFieldStyle#��[ 6L   ._�5!      � .+�5"      �� $��14(�6$   $�� ��62    �� Padding$
 8 10q�6:   ���#3A362Ay�6^   ��  �� ElementName$ 	txtAnswer�� �� Path$ Text.IsEmpty�   BooleanToVisibilityConverter �� 	Converter# [ �� 5#      -   5        $��	txtAnswer��5#   H   $��2q�6"   $��1��6-   $��14(�6:     SmallTextBoxBaseStyle#��[ 6r   5$      5%      � 5&      $��2��6   $��2��6)   $��Right=�6=   $��0,15,0,0q�6Y   .+�5'      �� -   5        $��btnDialogCancel��5'      $��1��64    �� IsCancel . $
 Hand��6T   $��34��6b     RoundedCancel#��[ 6n   .��6�   Hủy5)      �� 5*      -   5        $��btnDialogOk��5*       �� 	IsDefault . $��1��6[   $
 Hand��6k   $��34��6y     	RoundedOk#��[ 6�   .��6�   Ok5+      5,      5-      5.      5/      50      