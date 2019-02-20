using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;

namespace GridPanels
{
    public class GridPanel : Grid
    {
        public GridPanel() : base()
        {
        }

        protected override void OnIsItemsHostChanged(bool oldIsItemsHost, bool newIsItemsHost)
        {
            base.OnIsItemsHostChanged(oldIsItemsHost, newIsItemsHost);

            if(newIsItemsHost == true)
            {
                this.ColumnDefinitions.Clear();
                this.RowDefinitions.Clear();
            }
        }

        protected override void OnVisualChildrenChanged(DependencyObject visualAdded, DependencyObject visualRemoved)
        {
            base.OnVisualChildrenChanged(visualAdded, visualRemoved);

            if(visualAdded != null)
            {
                this.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });
            }

            if(visualRemoved != null)
            {
                this.ColumnDefinitions.RemoveAt(0);
            }

            for (int i = 0; i < this.InternalChildren.Count; i++)
            {
                var child = this.InternalChildren[i];
                Grid.SetColumn(child, i);
            }
        }
    }
}
