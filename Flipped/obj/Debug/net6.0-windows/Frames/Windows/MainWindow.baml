   M S B A M L   `   `   `  ����B  >Flipped, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null?$clr-namespace:Flipped.Frames.WindowsFlipped.Frames.Windows  5   	   e aMicrosoft.Web.WebView2.Wpf, Version=1.0.2365.46, Culture=neutral, PublicKeyToken=2a8ab48044d2601ekLclr-namespace:Microsoft.Web.WebView2.Wpf;assembly=Microsoft.Web.WebView2.WpfMicrosoft.Web.WebView2.Wpf 5   	   B >MicaWPF, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullD/clr-namespace:MicaWPF.Controls;assembly=MicaWPFMicaWPF.Controls 5	   	   '    !Flipped.Frames.Windows.MainWindow   5   	   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      5   
   R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   
   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   A	 =Wpf.Ui, Version=2.1.0.0, Culture=neutral, PublicKeyToken=null/ui&http://schemas.lepo.co/wpfui/2022/xaml 	 5   	   0local$clr-namespace:Flipped.Frames.Windows 	 5   	   ZWebViewLclr-namespace:Microsoft.Web.WebView2.Wpf;assembly=Microsoft.Web.WebView2.Wpf 	 5   	   :mica/clr-namespace:MicaWPF.Controls;assembly=MicaWPF 	 5	   	   ���#242424  � WindowStartupLocation$  CenterScreen=�5   	    � WindowStyle$
 None=�5   	    � AllowsTransparency$
 True��5   	    � 
ResizeMode$ CanMinimize=�5   	    � Title$ Flipped��5   	   $	��620��6   $
��1020��6&   .��5      � .+�5   
   �� $��-26,-30,0,0q�6   ��5      �� 5       �� ImageSource$! /Assets/LoginBackground.png��6   6I   5      �� 5   
   -   5         ��Name$ TopBar��5      $��	0,0,0,586q�6!   $��2��5   &   ��5      �� 5      $! /Assets/LoginBackground.png��6    j� Stretch$ UniformToFill=�6G   6a   5        MicaWPF.Controls.Button  5    
   -   5        $��	975,0,0,0q�5       
�������
������ $��Segoe)�6c   $��20(�6v   $��Center=�6�   $��0q�6�   $	��Top=�6�   $��37��6�   $��1��6�   .��6�   x6�     5!   
   -   5        $��
934,0,41,0q�5!      
�������
������ $��Segoe)�6g   $��20(�6z   $��Center=�6�   $��0q�6�   $	��Top=�6�   $��37��6�   $��1��6�   .��6�   -6�   �� 5"   
   $��/Assets/AppLogo.png��6   $��6,6,984,588q�6-   $��1��6B   6T   �� 5#   
   $��
44,9,691,0q�6   $	��Top=�6(   
�������$��15(�6S   $��1��6a   	Flipped6z   �� 5%   
   -   5         ��Name$
 Home��5%   *   
������ $��0q�6   $��74��6�   $��71��6�   $	��Top=�6�   $��1,39,0,0q�6�   $��1��6�   .��5&      � .+�5'        	 Wpf.Ui.Controls.SymbolIcon  -   5        	  Name$	 
HomeSymbol��5'   =   $��Center=�6    $��	0,-20,0,0q�6Q   $��30(�6d   
   Symbol$
 Home24=�6r   �� 5(      -   5         ��Name$ HomeText��5(   0   $��
0,21,0,-20q�6   Home6H   5)      5*      �� 5+   
   -   5        $ Library��5+   -   
������ $��0q�6�   $��74��6�   $��71��6�   $	��Top=�6�   $��	1,118,0,0q�6�   $��1��6�   .��5,      � .+�5-        -   5        $	 LibrarySymbol��5-   >   $��Center=�6    $��	0,-20,0,0q�6U   $��30(�6h   $
 	Library24=�6v   �� 5.      -	   5        $ LibraryText��5.   /   $��0,39,0,0q�6   $	��Top=�6D   $��Center=�6\   	Library6�   5/      50      �� 51   
   -
   5        $ Settings��51   .   
������ $��0q�6�   $��75��6�   $��71��6�   $	��Top=�6�   $��	1,535,0,0q�6�   $��1��6�   .��52      � .+�53        -   5        $	 SettingsSymbol��53   =   $��Center=�6    $��	0,-20,0,0q�6V   $��30(�6i   $
 
Settings24=�6w   �� 54      -   5        $ SettingsText��54   U   $��0,39,0,0q�6   $	��Top=�6.   $��12(�6F   $��Center=�6k   
Settings6�   55      56      �� 57   
   -   5        $
 Shop��57   *   
������ $��0q�6   $��75��6�   $��71��6�   $	��Top=�6�   $��-102,184,0,0q�6�   .��58      � .+�59        -   5        $	 
ShopSymbol��59   =   $��Center=�6    $��	0,-20,0,0q�6R   $��30(�6e   $
 ShoppingBag16=�6s   �� 5:      -   5        $ ShopText��5:   U   $��0,39,0,0q�6   $	��Top=�6.   $��12(�6F   $��Center=�6g   Shop6�   5;      5<      �� 5>   
    �� CornerRadius$ 10��6   $��77,37,-16,-18q�6#   ��5?      �� 5@       �� 
StartPoint$	 0,0&�6&    �� EndPoint$	 0,1&�67   .��5A      �  � Color$ #101c30��6#    � Offset$	 0.0Q�63   � 5B      $ #0e1e38��6#   $	 1.0Q�63   5C      ._�5E       � -   5          �Name$ ContentFrame��5E      $��	0,0,17,19q�6*   5G      5H       � 5I   
   -   5        $ SettingsContentFrame��5I      $y�	Collapsed=�6.   $��0,37,0,0q�6E   5K      5L      5M      