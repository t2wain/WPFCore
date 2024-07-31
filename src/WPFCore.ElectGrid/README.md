## Display a DataView in a ListView or DataGrid control

This library display a DataView within a ListView or DataGrid based on a ReportDefinition.

- UListView (WPFCore.ElectGrid.LV) is a ListView control that can display a DataView
- UDataGridView (WPFCore.ElectGrid.DG) is a DataGrid control that can display a DataView
- UTabControl (WPFCore.ElectGrid.TC) is a tab control that can host UListView or UDataGridView control in each tab (ReportTabItem)
- UReportDef (WPFCore.ElectGrid.RPT) is a form control that displays the configuration of the ReportDefinition (WPFCore.Data.Report)
- UReportFilter (WPFCore.ElectGrid.RPT) is a form control the filter setting of each report column (WPFCore.Data.Report)

## Command Configuration

The UTabControl control listens for commands:

- TACommands.ViewDetail to display the report TabItem.
- ApplicationCommands.Close to close the report TabItem.

The ReportTabItem control listens for commands:

- TACommands.Edit to display the UReportDef control in a modal child window 
- TNCommands.Refresh to refresh the DataView
- TACommands.SetFilter to display the report UReportFilter in a modal child window.
- TACommands.ClearFilter to clear report filter.

The DataGrid and Listview listen for commands:

- TNCommands.Refresh to refresh the DataView
- Applications.Copy to clean-up data in DataGridCell befored copied into Windows clipboard
- Applications.Paste to paste data into DataGrid from Windows clipboard