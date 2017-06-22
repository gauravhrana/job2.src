using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace PrismSample.Infrastructure.Extensions
{

    public class DataGridColumnsBehavior
    {
        private static List<DataGridColumn> StaticColumns { get; set; }

        public static readonly DependencyProperty BindableColumnsProperty =
            DependencyProperty.RegisterAttached("BindableColumns",
                                                typeof(ObservableCollection<DataGridColumn>),
                                                typeof(DataGridColumnsBehavior),
                                                new UIPropertyMetadata(null, BindableColumnsPropertyChanged));

        private static void BindableColumnsPropertyChanged(DependencyObject source, DependencyPropertyChangedEventArgs e)
        {
            DataGrid dataGrid = source as DataGrid;
            ObservableCollection<DataGridColumn> columns = e.NewValue as ObservableCollection<DataGridColumn>;

            if (StaticColumns == null)
            {
                StaticColumns = dataGrid.Columns.ToList();
            }

            dataGrid.Columns.Clear();

            if (columns == null)
            {
                return;
            }

            foreach (DataGridColumn column in StaticColumns)
            {
                dataGrid.Columns.Add(column);
            }

            foreach (DataGridColumn column in columns)
            {
                dataGrid.Columns.Add(column);
            }

            //var newColumns = new ObservableCollection<DataGridColumn>();

            //foreach (DataGridColumn column in StaticColumns)
            //{
            //    newColumns.Add(column);
            //}

            //foreach (DataGridColumn column in columns)
            //{
            //    newColumns.Add(column);
            //}

            //if(newColumns != dataGrid.Columns)
            //{
            //    dataGrid.Columns.Clear();
            //    dataGrid.Columns.AddRange(newColumns);
            //}

            columns.CollectionChanged += (sender, e2) =>
            {
                NotifyCollectionChangedEventArgs ne = e2 as NotifyCollectionChangedEventArgs;
                if (ne.Action == NotifyCollectionChangedAction.Reset)
                {
                    dataGrid.Columns.Clear();
                    if (ne.NewItems != null)
                    {
                        foreach (DataGridColumn column in ne.NewItems)
                        {
                            dataGrid.Columns.Add(column);
                        }
                    }
                }
                else if (ne.Action == NotifyCollectionChangedAction.Add)
                {
                    if (ne.NewItems != null)
                    {
                        foreach (DataGridColumn column in ne.NewItems)
                        {
                            dataGrid.Columns.Add(column);
                        }
                    }
                }
                else if (ne.Action == NotifyCollectionChangedAction.Move)
                {
                    dataGrid.Columns.Move(ne.OldStartingIndex, ne.NewStartingIndex);
                }
                else if (ne.Action == NotifyCollectionChangedAction.Remove)
                {
                    if (ne.OldItems != null)
                    {
                        foreach (DataGridColumn column in ne.OldItems)
                        {
                            dataGrid.Columns.Remove(column);
                        }
                    }
                }
                else if (ne.Action == NotifyCollectionChangedAction.Replace)
                {
                    dataGrid.Columns[ne.NewStartingIndex] = ne.NewItems[0] as DataGridColumn;
                }
            };
        }

        public static void SetBindableColumns(DependencyObject element, ObservableCollection<DataGridColumn> value)
        {
            element.SetValue(BindableColumnsProperty, value);
        }

        public static ObservableCollection<DataGridColumn> GetBindableColumns(DependencyObject element)
        {
            return (ObservableCollection<DataGridColumn>)element.GetValue(BindableColumnsProperty);
        }

    }

}
