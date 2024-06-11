using System.ComponentModel;

namespace WPFCore.Shared.UI.LV
{
    /// <summary>
    /// Manage the ICollectionView.SortDescriptions
    /// </summary>
    public class ListViewSort
    {
        // manage column sorting for an ICollectionView
        public ListViewSort(ICollectionView view)
        {
            this.View = view;
        }

        public ICollectionView View { get; set; }

        public bool IsPropertySorted(string propertyName)
        {
            if (this.View == null)
                return false;
            return this.View.SortDescriptions.Where(s => s.PropertyName == propertyName).Count() > 0;
        }

        public SortDescription GetSort(string propertyName)
        {
            if (this.IsPropertySorted(propertyName))
                return this.View.SortDescriptions.Where(i => i.PropertyName == propertyName).First();
            else
                return new SortDescription(propertyName, ListSortDirection.Ascending);
        }

        // set the sorting for a property in the ICollectionView
        // will allow multi-properties sorting
        public void SetSort(string propertyName, bool isMultiColumn)
        {
            if (this.View == null)
                return;

            // default sorting direction
            var s = new SortDescription(propertyName, ListSortDirection.Ascending); 
            // is the property currently sorted
            var exist = this.IsPropertySorted(propertyName);
            int idx = -1;
            if (exist)
            {
                // get the current sort
                s = this.GetSort(propertyName);
                idx = this.View.SortDescriptions.IndexOf(s);
                s = new SortDescription
                {
                    PropertyName = s.PropertyName,
                    // switch the sorting direction of an existing sorted property 
                    Direction = s.Direction == ListSortDirection.Ascending ?
                        ListSortDirection.Descending : ListSortDirection.Ascending
                };
            }

            if (!isMultiColumn && this.View.SortDescriptions.Count >= 1)
            {
                // switch to a single property sorting
                this.View.SortDescriptions.Clear();
                exist = false;
            }


            if (!exist)
                this.View.SortDescriptions.Add(s); // single property sorting
            else 
                this.View.SortDescriptions[idx] = s; // multi-properties sorting
        }

        public void ResetSort()
        {
            if (this.View != null)
                this.View.SortDescriptions.Clear();
        }
    }
}
