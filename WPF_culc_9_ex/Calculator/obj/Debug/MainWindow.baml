   M S B A M L   `   `   `  ����B  >WpfApp3, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null!clr-namespace:WpfApp3WpfApp3  5   	       WpfApp3.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   'localclr-namespace:WpfApp3     5   	     � Title$  
MainWindow��5   	   $	��550��6   $	��400��6)      beautifulStyleText#���   5	   	    � 	Resources 5
      ��%    5      .��5      �   beautifulStyleGrid#���  6    � RowDefinitions 5   
   �� 5      $	��75* �6   �� 5      �� 5      $	��48* �6   �� 5      $	��49* �6   �� 5      $	��49* �6   �� 5      $	��50* �6   �� 5      $	��49* �6   �� 5      $	��50* �6    � ColumnDefinitions 5   
   �� 5      $	��47* �6   �� 5      $
��151* �6   �� 5      $	��25* �6   �� 5      $
��173* �6   �� 5      $
��111* �6   �� 5      $	��87* �6   �� 5      $
��198* �6   .+�5"   
   �� $��2,2,2,3q�6   $��7��6"     beautifulStyleRect#���  66    �� Padding$ 0,0,5,0q�6c   ._�5#      �� -   5         ��Name$ output��5#      $
��Wrap=�6(    �� TextAlignment$ Right=�6<    K� ZIndex$ 2��6R   $��36��6c   $��Center=�6o   06�   5$      �� 5&   
   $��Button��6   $��2,0,2,48q�6"     beautifulStyleControl#���  64   $��2��6d   $��2��6q   �� 5'   
   -   5        $��%��5'      $��2q�6   $��2��6(   #���  65   $��2��6z   �� 5(   
   -   5        $	��1/x��5(      $��2q�6   $��3��6*   #���  67   $��2��6}   �� 5)   
   -   5        $��7��5)      $��2q�6   $��4��6(   #���  65   $��2��6z   �� 5*   
   -   5        $��4��5*      $��2q�6   $��5��6(   #���  65   $��2��6z   �� 5+   
   -   5        $��1��5+      $��2q�6   $��6��6(   #���  65   $��2��6z   �� 5,   
   -   5        $	��+/-��5,      $��2q�6   $��7��6*   #���  67   $��2��6|   �� 5-   
   -   5        $��Button��5-      $��2,0,2,48q�6"   $��2��64   #���  6D   $��2��6�   $��2��6�   �� 5.   
   -	   5        $��CE��5.      $��2q�6   $��2��6)   $��2��66   #���  6F   $��2��6�   �� 5/   
   -
   5        $	��sqr��5/      $��2q�6   $��3��6*   $��2��67   #���  6G   $��2��6�   �� 50   
   -   5        $��8��50      $��2q�6   $��4��6(   $��2��65   #���  6E   $��2��6�   �� 51   
   -   5        $��5��51      $��2q�6   $��5��6(   $��2��65   #���  6E   $��2��6�   �� 52   
   -   5        $��2��52      $��2q�6   $��6��6(   $��2��65   #���  6E   $��2��6�   �� 53   
   -   5        $��0��53      $��2q�6   $��7��6(   $��2��65   #���  6E   $��2��6�   �� 54   
   -   5        $��Button��54      $��2,0,2,48q�6"   $��4��64   #���  6D   $��2��6�   $��2��6�   �� 55   
   -   5        $��C��55      $��2q�6   $��2��6(   $��4��65   #���  6E   $��2��6�   �� 56   
   -   5        $
��sqrt��56      $��2q�6    $��3��6+   $��4��68   #���  6H   $��2��6�   �� 57   
   -   5        $��9��57      $��2q�6   $��4��6(   $��4��65   #���  6E   $��2��6�   �� 58   
   -   5        $��6��58      $��2q�6   $��5��6(   $��4��65   #���  6E   $��2��6�   �� 59   
   -   5        $��3��59      $��2q�6   $��6��6(   $��4��65   #���  6E   $��2��6�   �� 5:   
   -   5        $��.��5:      $��2q�6   $��7��6(   $��4��65   #���  6E   $��2��6�   �� 5;   
   -   5        $��Button��5;      $��2,0,2,48q�6"   $��6��64   #���  6D   $��2��6�   �� 5<   
   -   5        $	��del��5<      $��2q�6   $��2��6*   $��6��67   #���  6G   6w   �� 5=   
   -   5        $��/��5=      $��2q�6   $��3��6(   $��6��65   #���  6E   6u   �� 5>   
   -   5        $��*��5>      $��2q�6   $��4��6(   $��6��65   #���  6E   6u   �� 5?   
   -   5        $��-��5?      $��2q�6   $��5��6(   $��6��65   #���  6E   6u   �� 5@   
   -   5        $��+��5@      $��2q�6   $��6��6(   $��6��65   #���  6E   6u   �� 5A   
   -   5        $��=��5A      $��2q�6   $��7��6(   $��6��65   #���  6E   6u   5D      5E      