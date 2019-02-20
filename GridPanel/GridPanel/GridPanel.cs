using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;

namespace SamSoft.Windows
{
    public class GridPanel : Grid
    {
        public GridPanel() : base()
        {
        }

        /// <summary>
        /// Orientation
        /// </summary>

        public Orientation Orientation
        {
            get { return (Orientation)GetValue(OrientationProperty); }
            set { SetValue(OrientationProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Orientation.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty OrientationProperty =
            DependencyProperty.Register("Orientation", typeof(Orientation), typeof(GridPanel), new PropertyMetadata(Orientation.Horizontal, OnOrientationChange));

        private static void OnOrientationChange(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var gridPanel = d as GridPanel;
            gridPanel.ChangeOrientation((Orientation)e.NewValue);
        }

        private void ChangeOrientation(Orientation orientation)
        {
            this.ColumnDefinitions.Clear();
            this.RowDefinitions.Clear();

            for (int i = 0; i < this.InternalChildren.Count; i++)
            {
                this.AddGridDefinition(orientation);
            }

            this.ApplyGridIndex();
        }

        private void AddGridDefinition(Orientation orientation)
        {
            if (orientation == Orientation.Horizontal)
            {
                this.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });
            }
            else
            {
                this.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(1, GridUnitType.Star) });
            }
        }

        private void RemoveGridDefinition(Orientation orientation)
        {
            if (orientation == Orientation.Horizontal)
            {
                this.ColumnDefinitions.RemoveAt(0);
            }
            else
            {
                this.RowDefinitions.RemoveAt(0);
            }
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
                this.AddGridDefinition(this.Orientation);
            }

            if(visualRemoved != null)
            {
                this.RemoveGridDefinition(this.Orientation);
            }

            this.ApplyGridIndex();
        }

        private void ApplyGridIndex()
        {
            for (int i = 0; i < this.InternalChildren.Count; i++)
            {
                var child = this.InternalChildren[i];

                if (this.Orientation == Orientation.Horizontal)
                {
                    Grid.SetColumn(child, i);
                }
                else
                {
                    Grid.SetRow(child, i);
                }
            }
        }
    }
}
