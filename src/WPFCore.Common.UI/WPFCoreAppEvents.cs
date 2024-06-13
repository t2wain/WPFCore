using System.Windows;
using WPFCore.Common.ElectIndex;

namespace WPFCore.Common.UI
{
    public delegate void ViewItemDetailEventHandler(object sender, ViewItemDetailEventArgs args);

    public class ViewItemDetailEventArgs : RoutedEventArgs
    {
        public ViewItemDetailEventArgs(RoutedEvent routedEvent,
            object source, TNodeData data) : base(routedEvent, source)
        {
            Data = data;
        }
        public TNodeData Data { get; init; }
    }

    public static class WPFCoreApp
    {
        public static readonly RoutedEvent ViewItemDetailEvent = null!;
        public static readonly RoutedEvent CustomControlFocusEvent = null!;
        public static readonly RoutedEvent ItemCountChangedEvent = null!;

        static WPFCoreApp()
        {
            WPFCoreApp.ViewItemDetailEvent = EventManager.RegisterRoutedEvent(
                "ViewItemDetail",
                RoutingStrategy.Bubble,
                typeof(ViewItemDetailEventHandler),
                typeof(WPFCoreApp));

            WPFCoreApp.CustomControlFocusEvent = EventManager.RegisterRoutedEvent(
                "CustomControlFocus",
                RoutingStrategy.Bubble,
                typeof(RoutedEventHandler),
                typeof(WPFCoreApp));

            WPFCoreApp.ItemCountChangedEvent = EventManager.RegisterRoutedEvent(
                "ItemCountChanged",
                RoutingStrategy.Bubble,
                typeof(RoutedEventHandler),
                typeof(WPFCoreApp));
        }

    }
}
