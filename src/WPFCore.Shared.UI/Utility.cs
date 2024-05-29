using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;

namespace WPFCore.Shared.UI
{
    public class Utility
    {
        public static T? FindParent<T>(DependencyObject child) where T : DependencyObject
        {
            //get parent item
            DependencyObject parentObject = VisualTreeHelper.GetParent(child);

            //we've reached the end of the tree
            if (parentObject == null) return null;

            //check if the parent matches the type we're looking for
            T? parent = parentObject as T;
            if (parent != null)
                return parent;
            else
                return FindParent<T>(parentObject);
        }

        #region Cursor

        public static void SetWaitCursor()
        {

            SetCursor(Cursors.Wait);
        }

        public static void SetNormalCursor()
        {
            // wait until rendering is completed
            Dispatcher.CurrentDispatcher.BeginInvoke(new Action(() => SetCursor(Cursors.Arrow)), 
                DispatcherPriority.ContextIdle, null);
        }

        public static void SetCursor(Cursor c)
        {
            Application.Current.MainWindow.Cursor = c;
        }

        #endregion
    }
}
