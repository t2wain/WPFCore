using System.Collections.ObjectModel;
using System.ComponentModel;
using CT = WPFCore.Menu.MenuCommandEnum;
using MT = WPFCore.Shared.UI.MNU.MenuTypeEnum;

namespace WPFCore.Menu
{
    /// <summary>
    /// A repository of menu items and parent/child relationships
    /// </summary>
    public class MNRepo
    {
        #region Main

        public List<INotifyPropertyChanged> GetMain() =>
            new List<INotifyPropertyChanged>() 
            {
                CreateSubMenu("File", CT.File),
                CreateSubMenu("Edit", CT.Edit),
                CreateSubMenu("View", CT.View),
                CreateSubMenu("Actions", CT.Actions),
                CreateSubMenu("Tools", CT.Tools),
                CreateSubMenu("SmartPlant", CT.SmartPlant),
                CreateSubMenu("Reports", CT.Reports),
                CreateSubMenu("Window", CT.Window),
                CreateSubMenu("Help", CT.Help),
            };

        public List<INotifyPropertyChanged> GetSubMenu(CT parentCmd)
        {
            return parentCmd switch
            {
                CT.File => GetFileSubMenu(),
                CT.New => GetFileNewSubMenu(),
                CT.Open => GetFileOpenSubMenu(),
                CT.Edit => GetEditSubMenu(),
                CT.View => GetViewSubMenu(),
                CT.Display => GetViewDisplaySubMenu(),
                CT.Actions => GetActionSubMenu(),
                CT.CalculateBusLoad => GetActionBusLoadSubMenu(),
                CT.SwitchPlantOperationCase => GetActionCaseSubMenu(),
                CT.Cables => GetActionCablesSubMenu(),
                CT.Tools => GetToolsSubMenu(),
                CT.ETAPInterface => GetToolsETAPSubMenu(),
                CT.SmartPlant => GetSmartPlantSubMenu(),
                CT.Reports => GetReportsSubMenu(),
                CT.Window => GetWindowSubMenu(),
                CT.Window_New => GetWindowNewSubMenu(),
                CT.Help => GetHelpSubMenu(),
                _ => []
            };
        }

        #endregion

        #region File

        protected List<INotifyPropertyChanged> GetFileSubMenu() =>
            new List<INotifyPropertyChanged>()
            {
                CreateSubMenu("New", CT.New),
                CreateSubMenu("Open", CT.Open),
                CreateMenuItem("Close", CT.Close),
                CreateSeparator(),
                CreateMenuItem("Save As...", CT.SaveAs),
                CreateSeparator(),
                CreateMenuItem("Preferences...", CT.Preferences),
                CreateSeparator(),
                CreateMenuItem("Exit", CT.Exit),
            };

        protected List<INotifyPropertyChanged> GetFileNewSubMenu() =>
            new List<INotifyPropertyChanged>()
            {
                CreateMenuItem("Item...", CT.Item),
                CreateMenuItem("Multiple Item...", CT.MultipleItem),
                CreateMenuItem("SLD...", CT.New_SLD),
                CreateMenuItem("Template", CT.New_Template),
                CreateMenuItem("Custom Folder...", CT.CustomFolder),
            };

        protected List<INotifyPropertyChanged> GetFileOpenSubMenu() =>
            new List<INotifyPropertyChanged>()
            {
                CreateMenuItem("Plant Group...", CT.PlantGroup),
                CreateMenuItem("Drawing", CT.Drawing),
                CreateMenuItem("PDB Layout", CT.PDBLayout),
                CreateMenuItem("SLD", CT.Open_SLD),
                CreateMenuItem("Last Saved Document", CT.LastSavedDocument),
                CreateMenuItem("External Drawing...", CT.ExternalDrawing),
                CreateMenuItem("Template...", CT.Open_Template),
                CreateMenuItem("Log Files...", CT.LogFiles),
            };

        #endregion

        #region Edit

        protected List<INotifyPropertyChanged> GetEditSubMenu() =>
            new List<INotifyPropertyChanged>()
            {
                CreateMenuItem("Cut", CT.Cut),
                CreateMenuItem("Copy", CT.Copy),
                CreateMenuItem("Paste", CT.Paste),
                CreateSeparator(),
                CreateMenuItem("Delete", CT.Delete),
                CreateMenuItem("Select All", CT.SelectAll),
                CreateSeparator(),
                CreateMenuItem("Duplicate", CT.Duplicate),
                CreateMenuItem("Rename...", CT.Rename),
                CreateSeparator(),
                CreateMenuItem("Document Properties...", CT.DocumentProperties),
                CreateMenuItem("Common Properties...", CT.CommonProperties),
                CreateMenuItem("Transformer Connection And Tapping...", CT.TransformerConnectionAndTapping),
            };

        #endregion

        #region View

        protected List<INotifyPropertyChanged> GetViewSubMenu() =>
            new List<INotifyPropertyChanged>()
            {
                CreateMenuItem("Refresh", CT.Refresh),
                CreateSeparator(),
                CreateMenuItem("Show Selected Electrical Branch Only", CT.ShowSelectedElectricalBranchOnly),
                CreateMenuItem("Show In New Window", CT.ShowInNewWindow),
                CreateMenuItem("Show Items of All Plant Groups", CT.ShowItemsOfAllPlantGroups),
                CreateMenuItem("Show Related Items", CT.ShowRelatedItems),
                CreateSeparator(),
                CreateSubMenu("Display", CT.Display),
                CreateMenuItem("Toolbars...", CT.Toolbars),
            };

        protected List<INotifyPropertyChanged> GetViewDisplaySubMenu() =>
            new List<INotifyPropertyChanged>()
            {
                CreateMenuItem("Properties Window", CT.PropertiesWindow),
                CreateMenuItem("Working Area", CT.WorkingArea),
                CreateMenuItem("Item Status In Project", CT.ItemStatusInProject),
                CreateMenuItem("Catalog Explorer Window", CT.CatalogExplorerWindow),
            };

        #endregion

        #region Actions

        protected List<INotifyPropertyChanged> GetActionSubMenu() =>
            new List<INotifyPropertyChanged>()
            {
                CreateMenuItem("Design PDB Structure...", CT.DesignPDBStructure),
                CreateSeparator(),
                CreateMenuItem("Batch Load Association...", CT.BatchLoadAssociation),
                CreateMenuItem("Total Bus Load Validation", CT.TotalBusLoadValidation),
                CreateSubMenu("Calculate Bus Load", CT.CalculateBusLoad),
                CreateMenuItem("Dissociate Bus From PDB", CT.DissociateBusFromPDB),
                CreateSeparator(),
                CreateMenuItem("Set Circuit Sequence...", CT.SetCircuitSequence),
                CreateSeparator(),
                CreateMenuItem("Generate SLD For PDB...", CT.GenerateSLDforPDB),
                CreateMenuItem("Generate Schematic...", CT.GenerateSchematic),
                CreateSeparator(),
                CreateMenuItem("Define Document Reference...", CT.DefineDocumentReference),
                CreateMenuItem("Associate Document...", CT.AssociateDocument),
                CreateMenuItem("Global Revision...", CT.GlobalRevision),
                CreateMenuItem("Associate/Dissociate Custom Symbols...", CT.AssociateDissociateCustomSymbols),
                CreateSeparator(),
                CreateMenuItem("Dissociate", CT.Dissociate),
                CreateMenuItem("Move Items...", CT.MoveItems),
                CreateSubMenu("Switch Plant Operation Case", CT.SwitchPlantOperationCase),
                CreateSeparator(),
                CreateSubMenu("Cables", CT.Cables),
                CreateMenuItem("Parallel Equipment Assistant...", CT.ParallelEquipmentAssistant),
                CreateSeparator(),
                CreateMenuItem("Out-of-Date Composite Drawings Summary Report...", CT.OutOfDateCompositeDrawingsSummaryReport),
                CreateMenuItem("Fix Inconsistencies...", CT.FixInconsistencies),
                CreateSeparator(),
                CreateMenuItem("Register Report...", CT.RegisterReport),
                CreateMenuItem("Manage Operating Cases...", CT.ManageOperatingCases),
            };

        protected List<INotifyPropertyChanged> GetActionBusLoadSubMenu() =>
            new List<INotifyPropertyChanged>()
            {
                CreateMenuItem("Selected PDB For Buses", CT.SelectedPDBforBuses),
                CreateMenuItem("All Plant Buses", CT.AllPlantBuses),
            };

        protected List<INotifyPropertyChanged> GetActionCaseSubMenu() =>
            new List<INotifyPropertyChanged>()
            {
                CreateMenuItem("Switch Active Operating Case...", CT.SwitchActiveOperatingCase),
                CreateMenuItem("Switch Mode (Rule Driven)...", CT.SwitchModeRuleDriven),
            };

        protected List<INotifyPropertyChanged> GetActionCablesSubMenu() =>
            new List<INotifyPropertyChanged>()
            {
                CreateMenuItem("Batch Size Cables...", CT.BatchSizeCables),
                CreateMenuItem("Replace Cable Structure...", CT.ReplaceCableStructure),
                CreateMenuItem("Apply Reference Data To Cables...", CT.ApplyReferenceDataToCables),
                CreateMenuItem("Assign Drums To Cables...", CT.AssignDrumsToCables),
                CreateMenuItem("Predefined Routes...", CT.PredefinedRoutes),
                CreateMenuItem("Batch Cable Routing...", CT.BatchCableRouting),
                CreateMenuItem("Define Color Pattern...", CT.DefineColorPattern),
                CreateMenuItem("Refresh Load Data For Power Cables", CT.RefreshLoadDataforPowerCables),
                CreateMenuItem("Associate Cables With Equipment Circuits", CT.AssociateCablesWithEquipmentCircuits),
                CreateMenuItem("Batch Cable Side And Gland Associations...", CT.BatchCableSideAndGlandAssociations),
                CreateMenuItem("Synchronize Gland Associations...", CT.SynchronizeGlandAssociations),
                CreateMenuItem("Batch Cable Connection...", CT.BatchCableConnection),
                CreateMenuItem("Batch Cable Dissociation From Drums", CT.BatchCableDissociationFromDrums),
                CreateMenuItem("Insert Power Cable...", CT.InsertPowerCable),
            };

        #endregion

        #region Tools

        protected List<INotifyPropertyChanged> GetToolsSubMenu() =>
            new List<INotifyPropertyChanged>()
            {
                CreateMenuItem("Apply Options...", CT.ApplyOptions),
                CreateSeparator(),
                CreateMenuItem("Update Select Lists...", CT.UpdateSelectLists),
                CreateSeparator(),
                CreateMenuItem("Customize...", CT.Customize),
                CreateMenuItem("Options...", CT.Options),
                CreateMenuItem("Apply Naming Convention...", CT.ApplyNamingConvention),
                CreateSeparator(),
                CreateSubMenu("ETAP Interface", CT.ETAPInterface),
                CreateSeparator(),
                CreateMenuItem("Smart Completion Lookup...", CT.SmartCompletionLookup),
            };

        protected List<INotifyPropertyChanged> GetToolsETAPSubMenu() =>
            new List<INotifyPropertyChanged>()
            {
                CreateMenuItem("Publish All", CT.PublishAll),
                CreateMenuItem("Retrieve", CT.ETAP_Retrieve),
            };


        #endregion

        #region SmartPlant

        protected List<INotifyPropertyChanged> GetSmartPlantSubMenu() =>
            new List<INotifyPropertyChanged>()
            {
                CreateMenuItem("Publish Plant Groups", CT.PublishPlantGroups),
                CreateSeparator(),
                CreateMenuItem("Publish...", CT.Publish),
                CreateMenuItem("Find Document To Publish...", CT.FindDocumentToPublish),
                CreateMenuItem("Retrieve...", CT.SmartPlant_Retrieve),
                CreateMenuItem("Compare With Published Version...", CT.CompareWithPublishedVersion),
                CreateSeparator(),
                CreateMenuItem("Uncorrelate...", CT.Uncorrelate),
                CreateSeparator(),
                CreateMenuItem("Publish To External Analyzing Tool...", CT.PublishToExternalAnalyzingTool),
                CreateSeparator(),
                CreateMenuItem("To Do List...", CT.ToDoList),
                CreateSeparator(),
                CreateMenuItem("Browser...", CT.Browser),
                CreateSeparator(),
                CreateMenuItem("UpgradeSchema", CT.UpgradeSchema),
            };

        #endregion

        #region Reports

        protected List<INotifyPropertyChanged> GetReportsSubMenu() =>
            new List<INotifyPropertyChanged>()
            {
                CreateMenuItem("New...", CT.Report_New),
                CreateMenuItem("Edit...", CT.Report_Edit),
                CreateMenuItem("Delete...", CT.Report_Delete),
                CreateSeparator(),
                CreateMenuItem("Plant Reports...", CT.PlantReports),
                CreateMenuItem("My Reports...", CT.MyReports),
            };

        #endregion

        #region Window

        protected List<INotifyPropertyChanged> GetWindowSubMenu() =>
            new List<INotifyPropertyChanged>()
            {
                CreateSubMenu("New", CT.Window_New),
                CreateMenuItem("Cascade", CT.Cascade),
                CreateMenuItem("Tile Horizontally", CT.TileHorizontally),
                CreateMenuItem("Tile Vertically", CT.TileVertically),
            };

        protected List<INotifyPropertyChanged> GetWindowNewSubMenu() =>
            new List<INotifyPropertyChanged>()
            {
                CreateMenuItem("Engineering Data Editor...", CT.EngineeringDataEditor),
                CreateSeparator(),
                CreateMenuItem("Electrical Index", CT.ElectricalIndex),
                CreateMenuItem("Electrical Engineer", CT.ElectricalEngineer),
                CreateMenuItem("Reference Data Explorer", CT.ReferenceDataExplorer),
                CreateMenuItem("Reference Electrical Engineer...", CT.ReferenceElectricalEngineer),
                CreateSeparator(),
                CreateMenuItem("Project Management", CT.ProjectManagement),
            };

        #endregion

        #region Help

        protected List<INotifyPropertyChanged> GetHelpSubMenu() =>
            new List<INotifyPropertyChanged>()
            {
                CreateMenuItem("Integraph Smart Electrical Help", CT.IntegraphSmartElectricalHelp),
                CreateSeparator(),
                CreateMenuItem("Technical User Forum", CT.TechnicalUserForum),
                CreateSeparator(),
                CreateMenuItem("About Integraph Smart Electrical", CT.AboutIntegraphSmartElectrical),
            };

        #endregion

        #region Utility

        static INotifyPropertyChanged CreateSubMenu(string name, CT cmdType) =>
            new SELMenuItemVM()
            {
                Name = name,
                MenuCommandType = cmdType,
                MenuType = MT.SubMenu,
                Children = new ObservableCollection<INotifyPropertyChanged> { CreateDummyMenu() }
            };


        static INotifyPropertyChanged CreateMenuItem(string name, CT cmdType) =>
            new SELMenuItemVM()
            {
                Name = name,
                MenuCommandType = cmdType,
                MenuType = MT.Menu,
            };

        static INotifyPropertyChanged CreateDummyMenu() =>
            new SELMenuItemVM
            {
                Name = "Loading....",
                MenuType = MT.Dummy,
            };

        static INotifyPropertyChanged CreateSeparator() =>
            new SELMenuItemVM
            {
                MenuType = MT.Separator,
            };

        #endregion

    }
}
