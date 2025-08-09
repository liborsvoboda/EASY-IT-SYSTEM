//using Microsoft.Xaml.Behaviors;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interactivity;

namespace EasyITSystemCenter.SystemStructure {

    public class SystemObjectsBehaviors : Behavior<DataGrid> {
        private bool _monitorForTab;

        protected override void OnAttached() {
            base.OnAttached();
            AssociatedObject.BeginningEdit += EditStarting;
            AssociatedObject.CellEditEnding += CellEditEnding;
            AssociatedObject.PreviewKeyDown += KeyDown;
        }

        private void EditStarting(object sender, DataGridBeginningEditEventArgs e) {
            if (e.Column.DisplayIndex == AssociatedObject.Columns.Count - 1)
                _monitorForTab = true;
        }

        private void CellEditEnding(object sender, DataGridCellEditEndingEventArgs e) {
            _monitorForTab = false;
        }

        private void KeyDown(object sender, KeyEventArgs e) {
            if (_monitorForTab && e.Key == Key.Tab) {
                AssociatedObject.CommitEdit(DataGridEditingUnit.Row, false);
            }
        }

        protected override void OnDetaching() {
            base.OnDetaching();
            AssociatedObject.BeginningEdit -= EditStarting;
            AssociatedObject.CellEditEnding -= CellEditEnding;
            AssociatedObject.PreviewKeyDown -= KeyDown;
            _monitorForTab = false;
        }
    }

    public class SetMinWidthToAutoAttachedBehaviour {

        public static bool GetSetMinWidthToAuto(DependencyObject obj) {
            return (bool)obj.GetValue(SetMinWidthToAutoProperty);
        }

        public static void SetSetMinWidthToAuto(DependencyObject obj, bool value) {
            obj.SetValue(SetMinWidthToAutoProperty, value);
        }

        public static readonly DependencyProperty SetMinWidthToAutoProperty =
            DependencyProperty.RegisterAttached("SetMinWidthToAuto", typeof(bool), typeof(SetMinWidthToAutoAttachedBehaviour), new UIPropertyMetadata(false, WireUpLoadedEvent));

        public static void WireUpLoadedEvent(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            var grid = (DataGrid)d;

            var doIt = (bool)e.NewValue;

            if (doIt) {
                grid.Loaded += SetMinWidths;
            }
        }

        public static void SetMinWidths(object source, EventArgs e) {
            var grid = (DataGrid)source;

            foreach (var column in grid.Columns) {
                column.MinWidth = column.ActualWidth;
                column.Width = new DataGridLength(1, DataGridLengthUnitType.Star);
            }
        }
    }
}