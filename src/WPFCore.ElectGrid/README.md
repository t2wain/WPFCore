## Display a DataView in a ListView or DataGrid control

This library display a DataView within a ListView or DataGrid based on a ReportDefinition.

- UListView (WPFCore.ElectGrid.LV) is a ListView control that can display a DataView
- UDataGridView (WPFCore.ElectGrid.DG) is a DataGrid control that can display a DataView
- UTabControl (WPFCore.ElectGrid.TC) is a tab control that can host UListView or UDataGridView control in each tab (ReportTabItem)
- UReportDef (WPFCore.ElectGrid.RPT) is a form control that displays the configuration of the ReportDefinition (WPFCore.Data.Report)

## Command Configuration

The UTabControl control listens for commands:

- TACommands.ViewDetail to display the report.
- ApplicationCommands.Close to close the report tab.

The ReportTabItem control listens for commands:

- TACommands.Edit to display the UReportDef control in a modal child window 
- TNCommands.Refresh to refresh the DataView

The DataGrid and Listview listen for commands:

- TNCommands.Refresh to refresh the DataView