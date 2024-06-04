## UTreeView Custom Control

This control consist of a TreeView and a ListBox WPF controls. The TreeView display a deeply nested hierarchy set of data and the ListBox display the child set of data of the selected tree node.

The TreeView has a Context menu with several RoutedUICommand commands:

- Refresh (Ctrl+R)
- Collapse All (Ctrl+Left)
- Expand All (Ctrl+Right)
- 
The ListBox has a Context menu with the following RoutedUICommand commands:

- Select All (Ctrl+U)
- Unselect All (Ctrl+A)

All data binding are based on the MVVM pattern. The ViewModel class is based on the CommunityToolkit.Mvvm library.