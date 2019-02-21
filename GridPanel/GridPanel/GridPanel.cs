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

        public void SynchroGridDefinition(Orientation orientation)
        {
            if (this.InternalChildren.Count == 0)
            {
                this.ColumnDefinitions.Clear();
                this.RowDefinitions.Clear();
                return;
            }

            if (orientation == Orientation.Horizontal)
            {
                if (this.ColumnDefinitions.Count > this.InternalChildren.Count)
                {
                    for (int x = 0; x < this.ColumnDefinitions.Count - this.InternalChildren.Count; x++)
                    {
                        this.RemoveGridDefinition(orientation);
                    }
                }
                else if (this.ColumnDefinitions.Count < this.InternalChildren.Count)
                {
                    for (int x = 0; x < this.InternalChildren.Count - this.ColumnDefinitions.Count; x++)
                    {
                        this.AddGridDefinition(orientation);
                    }
                }
            }
            else
            {
                if (this.RowDefinitions.Count > this.InternalChildren.Count)
                {
                    for (int x = 0; x < this.RowDefinitions.Count - this.InternalChildren.Count; x++)
                    {
                        this.RemoveGridDefinition(orientation);
                    }
                }
                else if (this.RowDefinitions.Count < this.InternalChildren.Count)
                {
                    for (int x = 0; x < this.InternalChildren.Count - this.RowDefinitions.Count; x++)
                    {
                        this.AddGridDefinition(orientation);
                    }
                }
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

        protected override void OnVisualChildrenChanged(DependencyObject visualAdded, DependencyObject visualRemoved)
        {
            base.OnVisualChildrenChanged(visualAdded, visualRemoved);

            this.SynchroGridDefinition(this.Orientation);

            this.ApplyGridIndex();
        }

        private void ApplyGridIndex()
        {
            for (int i = 0; i < this.InternalChildren.Count; i++)
            {
                var child = this.InternalChildren[i];

                if (child != null)
                {
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
}

