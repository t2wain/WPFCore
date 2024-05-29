## WPF Application

This is a reference implementation of a data-centric WPF application. Some class libraries are intended for use in a new project. While others are for reference only since they display specific set of sample data. The sample features and data are based on the Hexagon Smart Electrical application. However, there is no business logic implemented in the application.

## UTreeView WPF Custom Control (WPFCore.ElectIndex)

This control consist of a TreeView and a ListBox WPF controls. The TreeView display a deeply nested hierarchy set of data and the ListBox display the child set of data of the selected tree node.

The TreeView has a Context menu with several RoutedUICommand commands:

- Refresh (Ctrl+R)
- Collapse All (Ctrl+Left)
- Expand All (Ctrl+Right)

The ListBox has a Context menu with the following RoutedUICommand commands:

- Select All (Ctrl+U)
- Unselect All (Ctrl+A)

All data binding are based on the MVVM pattern. The ViewModel class is based on the CommunityToolkit.Mvvm library.

## WPFCore.App

This application display data from an MS Access database. Database access is based on my ADOLib library.

## WPFCore.App2

This application display dynamically generated data.