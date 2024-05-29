using System.Windows.Input;

namespace WPFCore.Shared.UI.TV
{
    public static class TNCommands
    {
        public const string RefreshMsg = "TNCommands.RefreshMsg";
        public const string CollapseAllMsg = "TNCommands.CollapseAllMsg";
        public const string ExpandAllMsg = "TNCommands.ExpandAllMsg";
        public const string ExpandMsg = "TNCommands.ExpandMsg";
        public const string CollapseMsg = "TNCommands.CollapseMsg";
        public const string SelectAllMsg = "TNCommands.SelectAllMsg";
        public const string UnselectAllMsg = "TNCommands.UnselectAllMsg";
        public const string ContextMenuOpenMsg = "TNCommands.ContextMenuOpenMsg";

        static TNCommands()
        {
            InputGestureCollection inputs = new InputGestureCollection();
            inputs.Add(new KeyGesture(Key.R, ModifierKeys.Control, "Ctrl+R"));
            RefreshCommand = new RoutedUICommand("Refresh", "Refresh", typeof(TNCommands), inputs);

            inputs = new InputGestureCollection();
            inputs.Add(new KeyGesture(Key.Left, ModifierKeys.Control, "Ctrl+Left"));
            CollapseAllCommand = new RoutedUICommand("Collapse All", "CollapseAll", typeof(TNCommands), inputs);

            inputs = new InputGestureCollection();
            inputs.Add(new KeyGesture(Key.Right, ModifierKeys.Control, "Ctrl+Right"));
            ExpandAllCommand = new RoutedUICommand("Expand All", "ExpandAll", typeof(TNCommands), inputs);

            inputs = new InputGestureCollection();
            inputs.Add(new KeyGesture(Key.A, ModifierKeys.Control, "Ctrl+A"));
            SelectAllCommand = new RoutedUICommand("Select All", "SelectAll", typeof(TNCommands), inputs);

            inputs = new InputGestureCollection();
            inputs.Add(new KeyGesture(Key.U, ModifierKeys.Control, "Ctrl+U"));
            UnselectAllCommand = new RoutedUICommand("Unselect All", "UnselectAll", typeof(TNCommands), inputs);
        }

        private static RoutedUICommand RefreshCommand;
        public static RoutedUICommand Refresh
        {
            get { return RefreshCommand; }
        }

        private static RoutedUICommand CollapseAllCommand;
        public static RoutedUICommand CollapseAll
        {
            get
            {
                return CollapseAllCommand;
            }
        }

        private static RoutedUICommand ExpandAllCommand;
        public static RoutedUICommand ExpandAll
        {
            get
            {
                return ExpandAllCommand;
            }
        }

        private static RoutedUICommand SelectAllCommand;
        public static RoutedUICommand SelectAll
        {
            get
            {
                return SelectAllCommand;
            }
        }

        private static RoutedUICommand UnselectAllCommand;
        public static RoutedUICommand UnselectAll
        {
            get
            {
                return UnselectAllCommand;
            }
        }
    }
}
