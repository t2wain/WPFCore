using System.Windows.Controls;
using System.Windows.Input;
using WPFCore.Common.Data;
using WPFCore.Common.ElectIndex;
using WPFCore.Common.UI;

namespace WPFCore.ElectGrid.TC
{
    /// <summary>
    /// Interaction logic for UTabControl.xaml
    /// </summary>
    public partial class UTabControl : UserControl
    {
        public UTabControl()
        {
            InitializeComponent();
        }

        public void Init(UTabConrolVM vm)
        {
            this.VM = vm;
            this.DataContext = vm;
            this.ConfigureCommands();
        }

        public UTabConrolVM VM { get; set; } = null!;

        #region Configure commands

        private void ConfigureCommands()
        {
            this.CommandBindings.Add(new(TACommands.ViewDetail, this.OnViewDetail, this.OnViewDetailCanExecuted));
        }

        virtual protected void OnViewDetail(object sender, ExecutedRoutedEventArgs e)
        {
            if (e.Parameter is TNodeData n
                && n.Data is EquipItem eq
                && !String.IsNullOrWhiteSpace(eq.ID))
            {
                this.VM.AddReport(eq.ID);
            }
        }

        virtual protected void OnViewDetailCanExecuted(object sender, CanExecuteRoutedEventArgs e)
        {
            if (e.Parameter is TNodeData n
                && n.Data is EquipItem eq
                && !String.IsNullOrWhiteSpace(eq.ID))
            {
                e.CanExecute = true;
            }
            else { e.CanExecute = false; }
        }

        #endregion

    }
}
