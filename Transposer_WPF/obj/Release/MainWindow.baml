   M S B A M L   `   `   `  ����   Transposer_WPF    Transposer_WPF.MainWindow   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml       � Title$  
Transposer��$��830.4��$
��1000�� � WindowStartupLocation$ CenterScreen=�.���  � ColumnDefinitions �� $��0* ��� .+��� -    ��Name$ TABCONTROL_MAIN��$��2��$��
0,0,0,-0.2q� J� SnapsToDevicePixels . }�. .@��� -    ��Name$ TABCONTROL_MAIN_TAB_MAIN��$
��Main��$	��100��.��� -    �Name$ GRID_TABCONTROL�����	#FFE5E5E5$��	-4,0,0,-2q�.+��� -    ��Name$ DISPLAY_MAIN��$��10,44,277,19q� �� TextWrapping$
 Wrap=�	 �� 
IsReadOnly	 . 
 �� IsReadOnlyCaretVisible
 . " �� VerticalScrollBarVisibility$
 Auto=�$��2�� J� IsManipulationEnabled . �� -   $ DISPLAY_PLAYLIST��$��0,320,16,20q�$
 Wrap=�$	��241��$��Right=�	 . $
 Auto=� J� 	AllowDrop . � -   $ MAIN_GRID_SONGDROP��$
��Left=�$	��728��$��
734,38,0,0q�$	��Top=�$	��219�����	#FF6E6E6E .  J� OpacityMask �	#FFA19999 J� Opacity$ 0Q� �� �� -    ��Name$ BUTTON_TRANSPOSE_UP��$��Transpose Up��$��Right=�$��
0,48,139,0q�$	��Top=�$	��120��$	��120�����	#FFDDDDDD�� -   $ BUTTON_TRANSPOSE_DOWN��$��Transpose Down��$��Right=�$��0,169,139,0q�$	��Top=�$	��120��$	��120���� -	   $ MAIN_BUTTON_NEXT��$
��Next��$��	0,48,17,0q�$	��Top=�$	��120��$	��120��$��Right=��� -
   $ MAIN_BUTTON_PREVIOUS��$��Previous��$��
0,169,17,0q�$	��Top=�$	��120��$	��120�����	#FFDDDDDD$��Right=��� -   $ MAIN_BUTTON_EDIT_PLAYLIST��$��Edit Playlist��$��
0,294,15,0q�$	��Top=�$	��242��$��Right=��� -   $ MAIN_BUTTON_EDIT_SONG��$��	Edit Song��$
��Left=�$��	11,13,0,0q�$	��Top=�$	��350���� -   $ MAIN_BUTTON_RELOAD��$��Reload��$��Right=�$��	0,13,20,0q�$	��Top=�$	��237���� -   $ MAIN_BUTTON_SAVE_STATE��$��
Save State��$
��Left=�$��
363,13,0,0q�$	��Top=�$	��347��� $��0,55,297,567q�$��Right=�$��90��.+��� $��Right=�$��67��$��0,76,10.067,0q�$	��Top=�$��75��$��zoom-out.png���� $��Right=�$��67��$��0,3,11.067,0q�$	��Top=�$��75��$��zoom-in.png���� -   $ BUTTON_INCREASE_FONT_SIZE��$�� ��$��Right=�$��0,4,4.067,0q�$	��Top=�$��75��$��67��$��Right=�$	 0.2Q����	#FFFFFEFE
���   ��� -   $ BUTTON_DECREASE_FONT_SIZE��$�� ��$��Right=�$��0,76,4.067,0q�$	��Top=�$��75��$��67��$��Right=�$	 0.2Q�
�������
���   ��� -   $! TABCONTROL_MAIN_TAB_OPTIONS��$��Options��$	��100��.��� ���	#FFE5E5E5$��0,1,0,11q�.+��� -   $" OPTIONS_BUTTON_LOAD_PLAYLIST��$��Load Playlist��$��	0,30,13,0q�$	��Top=�$	��240��$��30��$��Right=��� -   $ OPTIONS_BUTTON_ADD_SONG��$��Add Song��$��	0,70,13,0q�$	��Top=�$	��240��$��30��$��Right=��� -   $' !OPTIONS_BUTTON_PLAYLIST_DIRECTORY��$��Playlist Directory��$
��Left=�$��	33,29,0,0q�$	��Top=�$	��146��$	��109���� -   $# OPTIONS_BUTTON_SONG_DIRECTORY��$��Song Directory��$
��Left=�$��
195,28,0,0q�$	��Top=�$	��146��$	��109���� -   $ BUTTON_HOME��$
��Home��$��0,119,13,24q�$	��240��$��Right=� �� IsCancel . �� -   $  OPTIONS_BUTTON_CREATE_FILE��$��Create File��$��
0,30,266,0q�$	��Top=�$	��240��$��30��$��Right=�� -   $ OPTIONS_GRID_FILEDROP��$
��Left=�$	��180��$��
20,502,0,0q�$	��Top=�$	��231�����	#FFD5D5D5 . .+��� -    ��Name$ GRID_FILE_DROP_LABEL��$��Center=�$��
10,57,21,0q�$
��Wrap=�$#��Drop songs here to load them.��$	��Top=�$	��200��$��64�� �� TextAlignment$ Center=��� -   $ OPTIONS_DISPLAY_PLAYLIST��$
��Left=�$	��287��$��
21,210,0,0q�$
 Wrap=�$	��Top=�$	��231��	 . $
 Auto=��� -   $" OPTIONS_BUTTON_CLEARPLAYLIST��$��Clear Playlist��$
��Left=�$��
22,181,0,0q�$	��Top=�$	��229��� $��263,150,0,55q�$	��432��$
��Left=�.+��� -   $ OPTIONS_INPUT_SEARCH��$��22��$��	24,31,0,0q�$
 Wrap=�$	��Top=�$	��391��$
��Left=��� -   $! OPTIONS_BUTTON_ADD_SELECTED��$	��Add��$��Right=�$��	0,58,17,0q�$	��Top=�$��75���� $
��Left=�$��	24,10,0,0q�$
��Wrap=�$��Search:��$	��Top=��� -    ��Name$# OPTIONS_LISTBOX_SEARCHRESULTS��$��24,61,0,6.867q�$	��308��$
��Left=�� -   $$ OPTIONS_CREATE_FILE_MESSAGEBOX��$	��159��$��
0,76,266,0q�$	��Top=�$	��347�����	#FFD7D6D6$��Right=�$y�Hidden=� J� Effect 3  -System.Windows.Media.Effects.DropShadowEffect     ShadowDepth$	 1.5Q�.+��� -    $ OPTIONS_TEXTBOX_INPUT��$��Right=�$��23��$��0,56,6,0q�$
 Wrap=�$	��Top=�$	��338��$y�Visible=��� -!   $$ OPTIONS_BUTTON_CREATE_PLAYLIST��$��Create Playlist��$
��Left=�$��129,117,0,0q�$	��Top=�$��96��$��26���� -"   $ OPTIONS_BUTTON_CANCEL��$��Cancel��$
��Left=�$��236,117,0,0q�$	��Top=�$��96��$��26�� . �� -#   $ OPTIONS_MESSAGEBOX_HEADER��$
��Left=�$��19��$
 Wrap=�$��Create File��$	��Top=�$	��351��	 . ���	#FFAEAEAE���	#FF727272
���   ��� $
��Left=�$��	11,33,0,0q�$
��Wrap=�$9��3Enter the file name of the file you want to create:��$	��Top=�$	��290���� -$   $  OPTIONS_BUTTON_CREATE_SONG��$��Create Song��$
��Left=�$��
21,117,0,0q�$	��Top=�$��96��$��26���� $��Chord Chart��$
��Left=�$��20.2666666666667��$	��Top=�$	��100��.��� ���	#FFE5E5E5.+��� -%    ��Name$ IMAGE_CHORDCHART��$��94,37,98,57q�$��Key Chord Chart.png��$
��Fill=��� $��Export��$
��Left=�$��20��$	��Top=�$	��100��.��� ���	#FFE5E5E5.+��� -&   $" IMPORTEXPORT_DISPLAY_TEXTBOX��$
 Wrap=�$��	0,35,0,15q��� -'   $  IMPORTEXPORT_BUTTON_EXPORT��$��Export��$��Right=�$	��Top=�$	��157��$��30��$��0,2,2,0q��� -(   $$ IMPORTEXPORT_DISIPLAY_FEEDBACK��$
��Left=�$��23��$��10,7,0,0q�$
 Wrap=�$	��Top=�$	��291�����	#FFE5E5E5��Z� �� $��
Bug Report��$
��Left=�$��20��$	��Top=�$	��100��.��� ���	#FFE5E5E5.+�(  "System.Windows.Controls.WebBrowser  -)     Name$ UPDATES_WEBBROWSER��$��	15,37,5,0q�$	��966��   Source$6 0http://transposerlive.weebly.com/bug-report.html?��� $
��Left=�$��	15,10,0,0q�$
��Wrap=�$&�� http://transposerlive.weebly.com��$	��Top=�$	��197��$��22���� -*   $ BUTTON_UPDATES_REFRESH��$��Refresh��$��Right=�$��0,8,10,0q�$	��Top=�$��75��