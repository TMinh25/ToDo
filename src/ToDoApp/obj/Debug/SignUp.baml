   M S B A M L   `   `   `  ����B  >ToDoApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null!clr-namespace:ToDoAppToDoApp  5         ToDoApp.SignUp   6	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    6"   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     6d   @d2http://schemas.microsoft.com/expression/blend/2008     6�   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     6�   'localclr-namespace:ToDoApp     6    � Title$  
Đăng Kí��6T  $��592.5��6d  $	��500��6s   � 
ResizeMode$ NoResize=�6   � WindowStartupLocation$ CenterScreen=�6�   � WindowStyle$
 None=�6�   � AllowsTransparency . 
������  � Icon$ Assets/Images/logo.ico��6    � 	Resources 5       �� Source�� 5   
   $ 
style.xaml?�6   %    .��5      ��  �� Padding$ 24q�6   	 �� CornerRadius$	 10��6   ���#F5F5F6._�5   
   � 
 � ColumnDefinitions
 5      �� 5	      �� 5
      $��5* �6#   �� 5       � RowDefinitions 5      �� 5      $��1* �6    �� 5      .+�5      �� -   5        $��2��5   ,    � Cursor$
 Hand��6<   $��36��6J   $��36��6V   $	��Top=�6a   $��Right=�6y   
������ 
������    btn#��[  6�   .��5      �� $"��Assets/Images/exitButton.png��6   5      �� 5      $��1��6   ._�5      �� -   5          ImageButtonStyle#��[ 5      $��0��6C   .��5      A� $	��250��6   $	��250��6*   $��Center=�67   ��5      �� 5       j� Stretch$ UniformToFill=�6)   -   5        imgAvatarSelection�� �� ImageSource 5   ]   �� *AvatarImageSource+5      5      5      �� 5      $��1��6   $��1��6&   .+�5      �� $��1��6   
�������  EntryFieldStyle#��[ 69   ._�5       � .+�5!      �� $��14(�6$   $��Tài Khoản��62    �� Padding$
 8 10q�6C   ���#3A362Ay�6g   ��  �� ElementName$ txtUserAccount�� �� Path$ Text.IsEmpty�   BooleanToVisibilityConverter �� 	Converter# [ �� 5"      -   5        $��txtUserAccount��5"   H   $��2q�6"   $��1��6-   $��14(�6:     SmallTextBoxBaseStyle#��[ 6w   5#      5$      �� 5%      $��1��6   
�������#��[ 69   ._�5&      � .+�5'      �� $��14(�6$   $��Mật Khẩu��62   $
 8 10q�6B   ���#3A362Ay�6f   �� $ txtPassword��$ Text.IsEmpty�# [ �� 5(      -   5        $��txtPassword��5(   H   $��2q�6"   $��2��6-   $��14(�6:   #��[ 6t   5)      5*      �� 5+      $��1��6   
�������#��[ 69   ._�5,      � .+�5-      �� $��14(�6$   $��Nhập Lại Mật Khẩu��62   $
 8 10q�6K   ���#3A362Ay�6o   �� $ txtRePassword��$ Text.IsEmpty�# [ �� 5.      -   5        $��txtRePassword��5.   H   $��2q�6"   $��3��6-   $��14(�6:   #��[ 6v   5/      50      �� 51      $��1��6   
�������#��[ 69   ._�52      � .+�53      �� $��14(�6$   $��	Họ Tên��62   $
 8 10q�6@   ���#3A362Ay�6d   �� $ txtDisplayName��$ Text.IsEmpty�# [ �� 54      -   5        $��txtDisplayName��54   H   $��2q�6"   $��4��6-   $��14(�6:   #��[ 6w   55      56      �� 57      -   5         ��Name$ txtErrorMessage��57        txtError#��[ 65   $��0 10 0 8q�6W   �� 58      -	   5        $��0��58      $��14(�6)   $��5��67     yellowButton#��[ 6D   $��40��6j   .��6�   
Đăng Kí6�   59      5:      5;      5<      