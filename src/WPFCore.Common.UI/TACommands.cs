using System.Windows.Input;

namespace WPFCore.Common.UI
{
    public static class TACommands
    {
        public const string SetFilterMsg = "TACommands.SetFilter";
        public const string ClearFilterMsg = "TACommands.ClearFilter";
        public const string ViewDetailMsg = "TACommands.ViewDetail";

        static TACommands()
        {
            var inputs = new InputGestureCollection();
            inputs.Add(new KeyGesture(Key.Space, ModifierKeys.None, "<Spacebar>"));
            ViewDetailCommand = new RoutedUICommand("View Detail", ViewDetailMsg, typeof(TACommands), inputs);

            inputs = new InputGestureCollection();
            inputs.Add(new KeyGesture(Key.F, ModifierKeys.Control, "Ctrl+F"));
            SetFilterCommand = new RoutedUICommand("Set Filter...", SetFilterMsg, typeof(TACommands), inputs);

            ClearFilterCommand = new RoutedUICommand("Clear Filter", ClearFilterMsg, typeof(TACommands));
        }

        private static RoutedUICommand SetFilterCommand;
        public static RoutedUICommand SetFilter
        {
            get { return SetFilterCommand; }
        }

        private static RoutedUICommand ClearFilterCommand;
        public static RoutedUICommand ClearFilter
        {
            get
            {
                return ClearFilterCommand;
            }
        }

        private static RoutedUICommand ViewDetailCommand;
        public static RoutedUICommand ViewDetail
        {
            get
            {
                return ViewDetailCommand;
            }
        }

    }
}
