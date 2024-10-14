## WPF Application

This is a reference implementation of a data-centric WPF application. Some class libraries are intended for use in a new project. While others are for reference only since they display specific set of sample data. The sample features and data are based on the Hexagon Smart Electrical application. However, there is no business logic implemented in the application.

This demo application only uses out-of-the-box Microsoft dotNet libraries. For MVVM data binding support, this application uses the CommunityToolkit.MVVM library.

*1. Tree view hierarchical navigation*

![Load List Worksheet](./Hierarchial%20Navigation.png)

*2. Databound menu*

![Load List Worksheet](./Databound%20Menu.png)

*3. Datagrid report*

![Load List Worksheet](./Muliple%20DataGrid%20displayed%20in%20Tab%20View.png)

*4. Set report filter*

![Load List Worksheet](./Set%20Report%20Filter.png)

*5. Report configuration*

![Load List Worksheet](./Configuring%20Report.png)

## Custom Controls

Currently, the application is composed of 4 custom user controls:

- WPFCore.Menu.UMenu
- WPFCore.Shared.UI.SB.UStatusBar
- WPFCore.ElectIndex
- WPFCore.ElectGrid

Please see the README document of each perspective project.

## Shared UI Library

- WPFCore.Shared.UI

## Data Access Libraries

- WPFCore.Data
- WPFCore.Data.OleDb
- WPFCore.Data.Report
- ADOLib.dll

## Other Common Library for the App

- WPFCore.Common
- WPFCore.Common.UI

## WPFCore.App

This application display data from an MS Access database. Database access is based on my ADOLib library.

## WPFCore.App2

This application display mocked data.

## Exploring WPF Features

- MVVM Data Bindings
- Custom RoutedUICommand
- Custom RoutedEvent
- Custom User Control
