using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace WPFCore.Shared.UI.DLG
{
    public class DialogUtility
    {
        virtual protected Window GetDialogWindow(UserControl ctl, string title)
        {
            ChildWindow w = new ChildWindow();
            w.Title = title;
            w.Owner = Application.Current.MainWindow;
            w.AddControl(ctl);
            ((DialogVM)ctl.DataContext).Init(ctl);
            w.SizeToContent = SizeToContent.WidthAndHeight;
            w.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            w.ShowInTaskbar = false;
            w.WindowStyle = WindowStyle.ToolWindow;
            return w;
        }

    }
}
