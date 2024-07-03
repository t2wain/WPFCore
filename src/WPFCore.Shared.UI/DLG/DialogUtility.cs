using System.Windows;
using System.Windows.Controls;

namespace WPFCore.Shared.UI.DLG
{
    public static class DialogUtility
    {
        public static Window GetDialogWindow(UserControl ctl, string title)
        {
            ChildWindow w = new ChildWindow();
            w.Title = title;
            w.Owner = Application.Current.MainWindow;
            w.SizeToContent = SizeToContent.WidthAndHeight;
            w.Width = ctl.Width;
            w.Height = ctl.Height;
            w.AddControl(ctl); 
            ((DialogVM)ctl.DataContext).Init(ctl);
            w.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            w.ShowInTaskbar = false;
            w.WindowStyle = WindowStyle.ToolWindow;
            return w;
        }

    }
}
