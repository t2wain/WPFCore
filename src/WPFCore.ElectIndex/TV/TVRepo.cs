using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Reflection.Metadata.Ecma335;
using WPFCore.Common.Data;
using WPFCore.Common.ElectIndex;
using WPFCore.Shared.UI.TV;
using NT = WPFCore.Common.ElectIndex.TIndexNodeEnum;

namespace WPFCore.ElectIndex.TV
{
    /// <summary>
    /// Create child view models for parent tree nodes
    /// </summary>
    public class TVRepo
    {
        private readonly IEquipRepo _repo;
        private readonly IReportDS _reportds;

        public TVRepo(IEquipRepo repo, IReportDS reportds)
        {
            this._repo = repo;
            this._reportds = reportds;
        }

        #region Nodes

        public Task<List<INotifyPropertyChanged>> GetIndexRoot(TreeVM parent) =>
            Task.FromResult(new List<INotifyPropertyChanged>
            {
                CreateNode("Electrical Equipment", NT.ElectricalEquipment, parent),
                CreateNode("Wiring Equipment", NT.WiringEquipment, parent),
                CreateNode("Process Equipment", NT.ProcessEquipment, parent),
                CreateNode("Documents", NT.Documents, parent),
                CreateNode("Reports", NT.Reports, parent),
            });

        public Task<List<INotifyPropertyChanged>> GetChildren(NT nodeType, TNodeData? data, TreeVM parent) =>
            nodeType switch
            {
                NT.ElectricalEquipment => GetElectricalEquipent(parent),
                NT.Loads => GetLoads(parent),
                NT.MiscellaneousElectricalEquipment => GetMiscellaneousElectricalEquipment(parent),
                NT.StaticElectricalEquipment => GetStaticElectricalEquipments(parent),
                NT.ConvertingEquipment => GetConvertingEquipment(parent),
                NT.PowerDistributionEquipment => GetPowerDistributionEquipment(parent),

                NT.WiringEquipment => GetWiringEquipment(parent),
                NT.Panels => GetPanels(parent),
                NT.Cables => GetCables(parent),

                NT.Documents => GetDocuments(parent),

                NT.Motors => GetMotors(parent),
                NT.OtherElectricalEquipment => GetOEE(parent),
                NT.Generators => GetGenerators(parent),
                NT.Transformers => GetTransformers(parent),
                NT.VariableFrequencyDrives => GetVFDs(parent),
                NT.PowerDistributionBoards => GetPDBs(parent),

                NT.Reports => GetReports(parent),

                _ => Task.FromResult(new List<INotifyPropertyChanged>())
            };

        #endregion

        #region Equipment

        protected Task<List<INotifyPropertyChanged>> GetElectricalEquipent(TreeVM parent) =>
            Task.FromResult(new List<INotifyPropertyChanged>
            {
                CreateNode("Loads", NT.Loads, parent),
                CreateNode("Offsite Power Supplies", NT.OffsitePowerSupplies, parent),
                CreateNode("Generators", NT.Generators, parent),
                CreateNode("BatterBanks", NT.BatterBanks, parent),
                CreateNode("Converting Equipment", NT.ConvertingEquipment, parent),
                CreateNode("PowerDistribution Equipment", NT.PowerDistributionEquipment, parent),
                CreateNode("Instruments", NT.Instruments, parent),
                CreateNode("Signals", NT.Signals, parent),
            });

        protected Task<List<INotifyPropertyChanged>> GetLoads(TreeVM parent) =>
            Task.FromResult(new List<INotifyPropertyChanged>
            {
                CreateNode("Motors", NT.Motors, parent),
                CreateNode("Static Electrical Equipment", NT.StaticElectricalEquipment, parent),
                CreateNode("Miscellaneous Electrical Equipment", NT.MiscellaneousElectricalEquipment, parent),
            });

        protected Task<List<INotifyPropertyChanged>> GetMiscellaneousElectricalEquipment(TreeVM parent) =>
            Task.FromResult(new List<INotifyPropertyChanged>
            {
                CreateNode("Lighting Fixtures", NT.LightingFixtures, parent),
                CreateNode("Socket Outlets", NT.SocketOutlets, parent),
                CreateNode("Welding Outlets", NT.WeldingOutlets, parent),
                CreateNode("Other Electrical Equipment", NT.OtherElectricalEquipment, parent),
            });

        protected Task<List<INotifyPropertyChanged>> GetStaticElectricalEquipments(TreeVM parent) =>
            Task.FromResult(new List<INotifyPropertyChanged>
            {
                CreateNode("Heaters", NT.Heaters, parent),
                CreateNode("Heat Traces", NT.HeatTraces, parent),
                CreateNode("Capacitors", NT.Capacitors, parent),
                CreateNode("Harmonic Filters", NT.HarmonicFilters, parent),
                CreateNode("Resistors", NT.Resistors, parent),
            });

        protected Task<List<INotifyPropertyChanged>> GetConvertingEquipment(TreeVM parent) =>
            Task.FromResult(new List<INotifyPropertyChanged>
            {
                CreateNode("Batter Chargers", NT.BatterChargers, parent),
                CreateNode("Other Converting Equipment", NT.OtherConvertingEquipment, parent),
                CreateNode("Transformers", NT.Transformers, parent),
                CreateNode("Uniterruptible Power Supplies", NT.UniterruptiblePowerSupplies, parent),
                CreateNode("Variable Frequency Drives", NT.VariableFrequencyDrives, parent),
                CreateNode("Current Limiting Reactors", NT.CurrentLimitingReactors, parent),
            });

        protected Task<List<INotifyPropertyChanged>> GetPowerDistributionEquipment(TreeVM parent) =>
            Task.FromResult(new List<INotifyPropertyChanged>
            {
                CreateNode("Power Distribution Boards", NT.PowerDistributionBoards, parent),
                CreateNode("Disconnect Electrical Equipment", NT.DisconnectElectricalEquipment, parent),
                CreateNode("Free-Standing Buses", NT.FreeStandingBuses, parent),
            });

        #endregion

        #region Wiring

        protected Task<List<INotifyPropertyChanged>> GetWiringEquipment(TreeVM parent) =>
            Task.FromResult(new List<INotifyPropertyChanged>
            {
                CreateNode("Panels", NT.Panels, parent),
                CreateNode("Cables", NT.Cables, parent),
                CreateNode("Busways", NT.Busways, parent),
                CreateNode("Drums", NT.Drums, parent),
                CreateNode("CableWays", NT.CableWays, parent),
            });

        protected Task<List<INotifyPropertyChanged>> GetPanels(TreeVM parent) =>
            Task.FromResult(new List<INotifyPropertyChanged>
            {
                CreateNode("Cabinets", NT.Cabinets, parent),
                CreateNode("Control Stations", NT.ControlStations, parent),
                CreateNode("Local Panels", NT.LocalPanels, parent),
                CreateNode("Junction Boxes", NT.JunctionBoxes, parent),
            });

        protected Task<List<INotifyPropertyChanged>> GetCables(TreeVM parent) =>
            Task.FromResult(new List<INotifyPropertyChanged>
            {
                CreateNode("Power Cables", NT.PowerCables, parent),
                CreateNode("Control Cables", NT.ControlCables, parent),
                CreateNode("Grounding Cables", NT.GroundingCables, parent),
                CreateNode("Instrument Cables", NT.InstrumentCables, parent),
                CreateNode("Single-Core Cable Assemblies", NT.SingleCoreCableAssemblies, parent),
            });


        #endregion

        #region Document

        protected Task<List<INotifyPropertyChanged>> GetDocuments(TreeVM parent) =>
            Task.FromResult(new List<INotifyPropertyChanged>
            {
                CreateNode("Single Line Diagrams", NT.SingleLineDiagrams, parent),
                CreateNode("Schematic Drawings", NT.SchematicDrawings, parent),
                CreateNode("Registered Reports", NT.RegisteredReports, parent),
                CreateNode("Electrical Analysis SLDs", NT.ElectricalAnalysisSLDs, parent),
                CreateNode("Miscellaneous Drawings", NT.MiscellaneousDrawings, parent),
                CreateNode("PDB Layout Drawings", NT.PDBLayoutDrawings, parent),
                CreateNode("Wiring Diagrams", NT.WiringDiagrams, parent),
                CreateNode("Cable Block Diagrams", NT.CableBlockDiagrams, parent),
            });

        #endregion

        #region Report

        protected Task<List<INotifyPropertyChanged>> GetReports(TreeVM parent) 
        {
            var t = _reportds.GetReportDefinitions();
            if (t != null) {
                return t
                    .ContinueWith(t => t.Result.Select(rdef => new EquipItem() { ID = rdef.FileName, Name = rdef.Name }),
                        TaskContinuationOptions.OnlyOnRanToCompletion | TaskContinuationOptions.RunContinuationsAsynchronously)
                    .ContinueWith(t => CreateEquipNode(t.Result.OrderBy(r => r.Name), NT.Report, parent).ToList(),
                        TaskContinuationOptions.OnlyOnRanToCompletion | TaskContinuationOptions.RunContinuationsAsynchronously);
            }
            else return Task.FromResult(new List<INotifyPropertyChanged>());
        }

        #endregion

        #region Data access

        protected Task<List<INotifyPropertyChanged>> GetMotors(TreeVM parent) =>
            _repo.GetMotor()
                .ContinueWith(t => CreateEquipNode(t.Result, NT.Motor, parent));

        protected Task<List<INotifyPropertyChanged>> GetOEE(TreeVM parent) =>
            _repo.GetOEE()
                .ContinueWith(t => CreateEquipNode(t.Result, NT.OEE, parent));

        protected Task<List<INotifyPropertyChanged>> GetGenerators(TreeVM parent) =>
            _repo.GetGenerators()
                .ContinueWith(t => CreateEquipNode(t.Result, NT.Generator, parent));

        protected Task<List<INotifyPropertyChanged>> GetTransformers(TreeVM parent) =>
            _repo.GetTransformers()
                .ContinueWith(t => CreateEquipNode(t.Result, NT.Transformer, parent));

        protected Task<List<INotifyPropertyChanged>> GetVFDs(TreeVM parent) =>
            _repo.GetVFDs()
                .ContinueWith(t => CreateEquipNode(t.Result, NT.VFD, parent));

        protected Task<List<INotifyPropertyChanged>> GetPDBs(TreeVM parent) =>
            _repo.GetPDBs()
                .ContinueWith(t => CreateEquipNode(t.Result, NT.PDB, parent));

        #endregion

        #region Utility

        static List<INotifyPropertyChanged> CreateEquipNode(IEnumerable<EquipItem> items, NT nodeType, TreeVM parent) =>
            items.Select(eq =>
                new NodeVM()
                {
                    Name = eq.Name!,
                    NodeType = nodeType,
                    Parent = parent,
                    DataItem = new() { NodeType = nodeType, Data = eq },
                    IsLeafNode = true,
                })
                .Cast<INotifyPropertyChanged>()
                .ToList();

        static INotifyPropertyChanged CreateNode(string name, NT nodeType, TreeVM parent, bool isLeaf = false) =>
            new NodeVM() { 
                Name = name, 
                NodeType = nodeType, 
                Parent = parent, 
                DataItem = new() { NodeType = nodeType },
                Children = isLeaf ? [] : new ObservableCollection<INotifyPropertyChanged> { CreateDummyNode() } 
            };

        internal INotifyPropertyChanged CreateLoadingNode()
        {
            return new NodeVM
            {
                Name = "Loading....",
                NodeType = NT.NoResult
            };
        }

        static INotifyPropertyChanged CreateDummyNode() =>
            new DummyNodeVM
            {
                Name = "Loading...."
            };

        internal INotifyPropertyChanged CreateNoResultNode()
        {
            return new NodeVM
            {
                Name = "<Not Found>",
                NodeType = NT.NoResult
            };
        }

        #endregion
    }
}
