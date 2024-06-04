namespace WPFCore.Menu
{
    public enum MenuCommandEnum
    {
        #region Menu

        File,
        Edit,
        View,
        Actions,
        Tools,
        SmartPlant,
        Reports,
        Window,
        Help,

        #endregion

        #region File

        New,
            Item,
            MultipleItem,
            New_SLD,
            New_Template,
            CustomFolder,
        Open,
            PlantGroup,
            Drawing,
            PDBLayout,
            Open_SLD,
            LastSavedDocument,
            ExternalDrawing,
            Open_Template,
            LogFiles,
        Close,
        SaveAs,
        Preferences,
        Exit,

        #endregion

        #region Edit

        Cut,
        Copy,
        Paste,
        Delete,
        SelectAll,
        Duplicate,
        Rename,
        DocumentProperties,
        CommonProperties,
        TransformerConnectionAndTapping,

        #endregion

        #region View

        Refresh,
        ShowSelectedElectricalBranchOnly,
        ShowInNewWindow,
        ShowItemsOfAllPlantGroups,
        ShowRelatedItems,
        Display,
            PropertiesWindow,
            WorkingArea,
            ItemStatusInProject,
            CatalogExplorerWindow,
        Toolbars,

        #endregion

        #region Actions

        DesignPDBStructure,
        BatchLoadAssociation,
        TotalBusLoadValidation,
        CalculateBusLoad,
            SelectedPDBforBuses,
            AllPlantBuses,
        AssociateBusWithPDB,
        DissociateBusFromPDB,
        SetCircuitSequence,
        GenerateSLDforPDB,
        GenerateSchematic,
        DefineDocumentReference,
        AssociateDocument,
        GlobalRevision,
        AssociateDissociateCustomSymbols,
        Dissociate,
        MoveItems,
        SwitchPlantOperationCase,
            SwitchActiveOperatingCase,
            SwitchModeRuleDriven,
        Cables,
            BatchSizeCables,
            ReplaceCableStructure,
            ApplyReferenceDataToCables,
            AssignDrumsToCables,
            PredefinedRoutes,
            BatchCableRouting,
            DefineColorPattern,
            RefreshLoadDataforPowerCables,
            AssociateCablesWithEquipmentCircuits,
            BatchCableSideAndGlandAssociations,
            SynchronizeGlandAssociations,
            BatchCableConnection,
            BatchCableDissociationFromDrums,
            InsertPowerCable,
        ParallelEquipmentAssistant,
        OutOfDateCompositeDrawingsSummaryReport,
        FixInconsistencies,
        RegisterReport,
        ManageOperatingCases,

        #endregion

        #region Tools

        ApplyOptions,
        UpdateSelectLists,
        Customize,
        Options,
        ApplyNamingConvention,
        ETAPInterface,
            PublishAll,
            ETAP_Retrieve,
        SmartCompletionLookup,

        #endregion

        #region SmartPlant

        PublishPlantGroups,
        Publish,
        FindDocumentToPublish,
        SmartPlant_Retrieve,
        CompareWithPublishedVersion,
        Uncorrelate,
        PublishToExternalAnalyzingTool,
        ToDoList,
        Browser,
        UpgradeSchema,

        #endregion

        #region Reports

        Report_New,
        Report_Edit,
        Report_Delete,
        PlantReports,
        MyReports,

        #endregion

        #region Window

        Window_New,
            EngineeringDataEditor,
            ElectricalIndex,
            ElectricalEngineer,
            ReferenceDataExplorer,
            ReferenceElectricalEngineer,
            ProjectManagement,
        Cascade,
        TileHorizontally,
        TileVertically,

        #endregion

        #region Help

        IntegraphSmartElectricalHelp,
        TechnicalUserForum,
        AboutIntegraphSmartElectrical,

        #endregion
    }
}
