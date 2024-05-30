using System.Windows;
using System.Windows.Controls;

namespace WPFCore.Shared.UI
{
    /// <summary>
    /// Interaction logic for ChildWindow.xaml
    /// </summary>
    public partial class ChildWindow : Window
    {
        public ChildWindow()
        {
            InitializeComponent();
        }
        public void AddControl(UserControl ctl)
        {
            this.mygrid.Children.Add(ctl);
        }

    }
}
