using System.Windows;
using System.Windows.Controls;

namespace WPFCore.Shared.UI.DLG
{
    public static class DialogUtility
    {
        public static Window GetDialogWindow(UserControl ctl, string title, int? width = null, int? height = null)
        {
            ChildWindow w = new ChildWindow();
            w.Title = title;
            w.Owner = Application.Current.MainWindow;
            if (width.HasValue)
                w.Width = width.Value;
            if (height.HasValue)
                w.Height = height.Value;
            w.AddControl(ctl); 
            ((DialogVM)ctl.DataContext).Init(ctl);
            w.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            w.ShowInTaskbar = false;
            w.WindowStyle = WindowStyle.ToolWindow;
            return w;
        }

    }
}
