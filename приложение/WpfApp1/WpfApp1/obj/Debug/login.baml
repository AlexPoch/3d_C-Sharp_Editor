   M S B A M L   `   `   `  ����B  >WpfApp1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null!clr-namespace:WpfApp1WpfApp1  5   	       WpfApp1.login   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   'localclr-namespace:WpfApp1     5   	     � Title$  Авторизация��5   	   $	��360��6   $	��300��6*   $��14(�66   ���#c2f6f8 � 
ResizeMode$ NoResize=�6Y    � WindowStartupLocation$ CenterScreen=�6o   .��5
      �� 
������� �� CornerRadius$ 20��6    $��20,20q�62   ._�5   
   �� $��20,20,19.6,37q�6   .+�5      �� $ ��Вход в систему��6   
���   �$��25(�6@   $��Center=�6N   �� 5      �� 5      $%��Имя пользователя��6   
���   �6/   �� 5      -   5         ��Name$ txtUsername��5      
����������#545d6a$��18(�6S   �� 5      $��Пароль��6   
���   �6%   G� 5      -   5         G�Name$ txtPassword��5      
����������#545d6a$��18(�6W   �� 5      -   5         ��Name$ 	btnSubmit��5      
���   �   WpfApp1.login   SignIn#��Z 5      ���#c2f6f8$��18(�5      $��Вход��5      $��60,10q�5       � Cursor$
 Hand��5      $��29��5      �� 5      
���   �$��Bottom=�6,   $��Center=�6G   $
 Hand��6d   $��	Underline{�6r   $��Регистрация��6�   	 J� InputBindings	 5      d� 5      
 d� MouseAction$
 	LeftClicke�6#      SignUp �� Command# Z 6;   6e   5       5"      5#      5$      