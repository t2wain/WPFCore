namespace WPFCore.ElectIndex.TV
{
    // Identify electrical equipment category
    public enum TIndexNodeEnum
    {
        Dummy,
        NoResult,
        Loading,

        #region Loads

        ElectricalEquipment,
        Loads,
        Motors,
        Motor,
        StaticElectricalEquipment,
        Heaters,
        Heater,
        HeatTraces,
        HeatTrace,
        Capacitors,
        Capacitor,
        HarmonicFilters,
        HarmonicFilter,
        Resistors,
        Resistor,
        MiscellaneousElectricalEquipment,
        LightingFixtures,
        LightingFixture,
        SocketOutlets,
        SocketOutlet,
        WeldingOutlets,
        WeldingOutlet,
        OtherElectricalEquipment,
        OEE,

        #endregion

        #region Equipment

        OffsitePowerSupplies,
        Generators,
        Generator,
        BatterBanks,
        ConvertingEquipment,
        BatterChargers,

        OtherConvertingEquipment,
        Transformers,
        Transformer,
        UniterruptiblePowerSupplies,
        UPS,
        VariableFrequencyDrives,
        VFD,
        CurrentLimitingReactors,

        PowerDistributionEquipment,
        PowerDistributionBoards,
        PDB,
        Bus,
        Circuit,
        DisconnectElectricalEquipment,
        FreeStandingBuses,

        Instruments,
        Signals,

        #endregion

        #region Wiring

        WiringEquipment,

        Panels,
        Cabinets,
        ControlStations,
        LocalPanels,
        JunctionBoxes,

        Cables,
        PowerCables,
        ControlCables,
        GroundingCables,
        InstrumentCables,
        SingleCoreCableAssemblies,

        Busways,
        Drums,
        CableWays,

        #endregion

        ProcessEquipment,

        #region Document

        Documents,
        SingleLineDiagrams,
        SchematicDrawings,
        RegisteredReports,
        ElectricalAnalysisSLDs,
        MiscellaneousDrawings,
        PDBLayoutDrawings,
        WiringDiagrams,
        CableBlockDiagrams

        #endregion
    }
}
