   M S B A M L   `   `   `  ����K  GAbschlussprojekt, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullA%clr-namespace:Abschlussprojekt.SeitenAbschlussprojekt.Seiten  5      -    'Abschlussprojekt.Seiten.Spiel_erstellen   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      7local%clr-namespace:Abschlussprojekt.Seiten     5      
�������.��5	      � .+�5
   
   �� $��Spieler rot: ��6   $
��Left=�6(   $��	20,50,0,0q�6C   $	��Top=�6V   $��85��6n   
���  ��
���   �$
��Bold"�6�   �� 5   
   $��Spieler gelb: ��6   $
��Left=�6)   $��	20,77,0,0q�6D   $	��Top=�6W   $��85��6o   
���   ����	#FFFFF700$
��Bold"�6�   �� 5   
   $��Spieler grün: ��6   $
��Left=�6)   $��
20,104,0,0q�6D   $	��Top=�6X   ���	#FF2BFF00$
��Bold"�6�   �� 5   
   $��Spieler blau: ��6   $
��Left=�6)   $��
20,131,0,0q�6D   $	��Top=�6X   $��85��6p   ���	#FF2543C2$
��Bold"�6�   �� 5   
   -   5          ��Name$  L_Name_Spieler_rot��5      $
��Left=�6{   $��
105,50,0,0q�6�   $	��Top=�6�   
���   �|�.   �� 
IsReadOnly . ��6.   �� *SelectedItem.Content+ �� ElementName$ comboBox_rot�� �� Mode$ OneWay=�6  �� 5   
   -   5        $  L_Name_Spieler_gelb��5      $
��Left=�6}   $��
105,77,0,0q�6�   $	��Top=�6�   |�.   . ��6/   �� *SelectedItem.Content+$ comboBox_gelb��$ OneWay=�6  �� 5   
   -   5        $  L_Name_Spieler_gruen��5      $
��Left=�6   $��105,104,0,0q�6�   $	��Top=�6�   |�.   . ��60   �� *SelectedItem.Content+$ comboBox_gruen��$ OneWay=�6  �� 5   
   -   5        $  L_Name_Spieler_blau��5      $
��Left=�6}   $��105,131,0,0q�6�   $	��Top=�6�   |�.   . ��6/   �� *SelectedItem.Content+$ comboBox_blau��$ OneWay=�6  �� 5   
   -   5         ��Name$ comboBox_rot��5      $
��Left=�6)   $��225,53,-45,0q�6D   $	��Top=�6Z   $	��120��6r    �� SelectedIndex$ 0��6~   .@�5      �� $��Geschlossen��6   �� 5      $	��Ich��6   �� 5      $��Computergegner��6   �� 5      $��Offen��6   5      �� 5   
   -   5        $ comboBox_gelb��5      $
��Left=�6*   $��225,80,-45,0q�6E   $	��Top=�6[   $	��120��6s   $ 0��6   .@�5      �� $��Geschlossen��6   �� 5      $	��Ich��6   �� 5      $��Computergegner��6   �� 5      $��Offen��6   5      �� 5   
   -   5        $ comboBox_gruen��5      $
��Left=�6+   $��225,107,-45,0q�6F   $	��Top=�6]   $	��120��6u   $ 0��6�   .@�5      �� $��Geschlossen��6   �� 5       $	��Ich��6   �� 5!      $��Computergegner��6   �� 5"      $��Offen��6   5#      �� 5$   
   -   5        $ comboBox_blau��5$      $
��Left=�6*   $��225,134,-45,0q�6E   $	��Top=�6\   $	��120��6t   $ 0��6�   .@�5%      �� $��Geschlossen��6   �� 5&      $	��Ich��6   �� 5'      $��Computergegner��6   �� 5(      $��Offen��6   5)      �� 5*   
   $��Spiel erstellen��6   $
��Left=�6*   $��	20,10,0,0q�6E   $	��Top=�6X   $��20(�6p   �� 5+   
   -	   5         ��Name$ btn_spiel_starten��5+      $��Los geht's amk��6,   $
��Left=�6E   $��225,205,0,0q�6`   $	��Top=�6u   $	��114��6�   $��37��6�   6�   �� 5,   
   -
   5        $  Spielername_eingabe��5,      $
��Left=�6/   $��23��6J   $��
20,177,0,0q�6V    �� TextWrapping$
 Wrap=�6j   $��Hier Namen eingeben��6~   $	��Top=�6�   $	��200��6�   6�   �� 5-   
   $��max 20 Zeichen��6   $
��Left=�6)   $��225,173,0,0q�6D   $	��Top=�6Y   $	��102��6q   �� 5.   
   -   5        $ btn_abbrechen��5.      $��	Abbrechen��6(   $
��Left=�6<   $��346,205,0,0q�6W   $	��Top=�6l   $	��117��6�   $��37��6�   6�   �� 5/   
   -   5        $ 
btn_Hosten��5/      $��Hosten��6%   $
��Left=�66   $��
20,205,0,0q�6Q   $	��Top=�6e   $	��114��6}   $��37��6�   6�   51      52      