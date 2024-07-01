﻿using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

using Xlfdll.Windows.Presentation;

namespace BatchRenamer
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            MainViewModel mainViewModel = new MainViewModel();

            mainViewModel.Files.CollectionChanged += BatchFiles_CollectionChanged;

            this.DataContext = mainViewModel;
        }

        private void MainWindow_Closing(object sender, CancelEventArgs e)
        {
            MainViewModel mainViewModel = this.DataContext as MainViewModel;

            if (mainViewModel?.Files.Count > 0)
            {
                MessageBoxResult result = MessageBox.Show(App.Current.MainWindow,
                               "There are still files in the list that might be waiting for processing."
                               + Environment.NewLine
                               + Environment.NewLine
                               + "Do you want to exit?",
                               App.Current.MainWindow.Title,
                               MessageBoxButton.YesNo,
                               MessageBoxImage.Question,
                               MessageBoxResult.No);

                e.Cancel = (result == MessageBoxResult.No);
            }
        }

        private void MainWindow_PreviewDragOver(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effects = DragDropEffects.Copy;
                e.Handled = true; // Only stop handling after a file drop
            }
        }

        private void MainWindow_Drop(object sender, DragEventArgs e)
        {
            String[] files = e.Data.GetData(DataFormats.FileDrop) as String[];

            if (files != null && files.Length > 0)
            {
                MainViewModel mainViewModel = this.DataContext as MainViewModel;

                mainViewModel?.AddFiles(files);
            }
        }

        private void ApplyPatternButton_Click(object sender, RoutedEventArgs e)
        {
            (sender as Button)?.ShowDropMenu();
        }

        private void BatchFileListView_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (!BatchFileListView.HasItems)
            {
                LayoutHelper.AdjustListViewColumns(BatchFileListView);
            }
        }

        private void BatchFileListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            MainViewModel mainViewModel = this.DataContext as MainViewModel;

            mainViewModel?.UpdateSelection();
        }

        private void BatchFileListView_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Delete)
            {
                if (BatchFileListView.SelectedItem != null)
                {
                    MainViewModel mainViewModel = this.DataContext as MainViewModel;

                    mainViewModel?.Files.Remove(BatchFileListView.SelectedItem as BatchFileInfo);
                }

                e.Handled = true;
            }
        }

        private void BatchFileListViewColumnHeader_Click(object sender, RoutedEventArgs e)
        {
            GridViewColumnHeader headerClicked = e.OriginalSource as GridViewColumnHeader;

            if (headerClicked != null)
            {
                if (headerClicked.Role != GridViewColumnHeaderRole.Padding)
                {
                    if (lastDirection == ListSortDirection.Ascending)
                    {
                        lastDirection = ListSortDirection.Descending;
                    }
                    else
                    {
                        lastDirection = ListSortDirection.Ascending;
                    }

                    String header = headerClicked.Column.Header.ToString();
                    ICollectionView defaultView = CollectionViewSource.GetDefaultView(BatchFileListView.ItemsSource);

                    defaultView.SortDescriptions.Clear();
                    defaultView.SortDescriptions.Add(new SortDescription(header.Replace(" ", String.Empty), lastDirection));
                    defaultView.Refresh();
                }
            }
        }

        private void BatchFiles_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            LayoutHelper.AdjustListViewColumns(BatchFileListView);
        }

        private void NewFileNameTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            ListViewItem item = (sender as TextBox).GetParent<ListViewItem>();

            if (item != null)
            {
                BatchFileListView.SelectedItem = item.Content;
            }
        }

        private void NewFileNameTextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Up || e.Key == Key.Down)
            {
                MainViewModel mainViewModel = this.DataContext as MainViewModel;

                if (mainViewModel != null)
                {
                    Int32 currentIndex = BatchFileListView.SelectedIndex;

                    if (e.Key == Key.Up)
                    {
                        currentIndex--;
                    }
                    else if (e.Key == Key.Down)
                    {
                        currentIndex++;
                    }
                    else
                    {
                        currentIndex = -1;
                    }

                    if (currentIndex >= 0 && currentIndex < mainViewModel.Files.Count)
                    {
                        Int32 textIndex = 0;

                        if (BatchFileListView.SelectedIndex >= 0)
                        {
                            ListViewItem currentItem = BatchFileListView.ItemContainerGenerator.ContainerFromItem(BatchFileListView.SelectedItem) as ListViewItem;
                            TextBox currentNewFileNameTextBox = currentItem.GetDescendant<TextBox>("NewFileNameTextBox");

                            textIndex = currentNewFileNameTextBox.SelectionStart;
                        }

                        BatchFileListView.SelectedIndex = currentIndex;

                        ListViewItem nextItem = BatchFileListView.ItemContainerGenerator.ContainerFromItem(BatchFileListView.SelectedItem) as ListViewItem;
                        TextBox nextNewFileNameTextBox = nextItem.GetDescendant<TextBox>("NewFileNameTextBox");

                        nextNewFileNameTextBox.Select(textIndex, 0);
                        nextNewFileNameTextBox.Focus();

                        e.Handled = true;
                    }
                }
            }
        }

        private ListSortDirection lastDirection = ListSortDirection.Ascending;
    }
}