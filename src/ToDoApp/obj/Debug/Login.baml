   M S B A M L   `   `   `  ����  ToDoApp    ToDoApp.Login   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    6!   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     6c   @d2http://schemas.microsoft.com/expression/blend/2008     6�   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     6�     � Title$  Đăng Nhập��6/  $	��360��6A  $	��340��6N   � 
ResizeMode$ NoResize=�6Z   � WindowStartupLocation$ CenterScreen=�6p   � WindowStyle$
 None=�6�   � AllowsTransparency . 
������  � Icon$ Assets/Images/logo.ico��6�   � 	Resources 5       �� Source�� 5   
   $ 
style.xaml?�6   %    .��5      ��  �� Padding$ 24q�6   	 �� CornerRadius$	 10��6   ���#F5F5F6._�5   
   � 
 � RowDefinitions
 5      �� 5      $��1* �6    �� 5	      $��4* �6    �� 5
      $��1* �6    .+�5      � $��0��6    � ColumnDefinitions 5      �� 5      �� 5      $��50 �6'   .+�5      �� $��18(�6   $��Medium"�6*   $��./Assets/Fonts/#Asap)�6>   $��Center=�6`   Đăng Nhập6�   �� 5      -   5        $��1��5   /    � Cursor$
 Hand��6?   $��36��6M   $��36��6Y   $��Center=�6d   $��Center=�6   
������ 
������    btn#��[  6�   .��5      �� $"��Assets/Images/exitButton.png��6   5      5      � 5      $��1��6   $
��0 16q�6    
 5      �� 5      �� 5      �� 5      $	��.8* �6$   �� 5      .+�5      �� $��0��6   
�������  EntryFieldStyle#��[ 69   ._�5      � .+�5       �� $��16(�6$   $��Tài Khoản��62    �� Padding$
 8 11q�6C   ���#7F7494y�6g   ��  �� ElementName$ txtUserAccount�� �� Path$ Text.IsEmpty�   BooleanToVisibilityConverter �� 	Converter# [ �� 5!      -   5        $��txtUserAccount��5!   H   $��2q�6"   $��0��6-   $��16(�6:     TextBoxBaseStyle#��[ 6^   6�   5"      5#      �� 5$      $��1��6   
�������#��[ 69   ._�5%      � .+�5&      �� $��16(�6$   $��Mật Khẩu��62   $
 8 11q�6B   ���#7F7494y�6f   �� $ txtPassword��$ Password.IsEmpty�# [ G� 5'      -   5        $��txtPassword��5'   M   $��2q�6'   $��1��62   $��16(�6?     PasswordBoxBaseStyle#��[ 6`   6�   5(      5)      �� 5*      -   5        $��txtErrorMessage��5*   )   $��2��6     txtError#��[ 6@   �� 5+      -   5        $��btnLogIn��5+      $��2��6)     yellowButton#��[ 66   $��3��6\   .��6�   Đăng Nhập6�   5-      �� 5.      -   5        $��	btnSignUp��5.   /   $��2��6   $��3��6"   $��Center=�6@   $��Center=�6]   $	��200��6x   $
 Hand��6�   
������ 
������ ��5/      �� 50      .�51      ��  �� Property � �� �� Value 52      �� 53   "    �� 
TargetType" ��62   .R�54   &   �� $��0q�6-   
������ ._�55   *   �� $��Center=�6;   $��Center=�6X   56   '   57   #   59      5:      .��5<      �� ���#3A362A$��	Underline{�61   $��14(�6M   Đăng Kí?6e   5=      5>      5?      5@      