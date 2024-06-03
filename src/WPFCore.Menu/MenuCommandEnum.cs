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
        Open,
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

        DesignPDBStructre,
        BatchLoadAssociation,
        TotalBusLoadValidation,
        CalculateBusLoad,
            SelectedPDBorBuses,
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
            SwitchActiveOperatinCase,
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
            InserPowerCable,
        ParallelEquipmentAssistant,
        OutOfDateCompositeDrawingsSummaryReport,
        FixInconsistencies,
        RegisterReport,
        ManageOperatinCases,

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

        PUblishPlantGroups,
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
