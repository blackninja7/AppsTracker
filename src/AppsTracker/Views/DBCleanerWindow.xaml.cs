﻿#region Licence
/*
  *  Author: Marko Devcic, madevcic@gmail.com
  *  Copyright: Marko Devcic, 2015
  *  Licence: http://creativecommons.org/licenses/by-nc-nd/4.0/
 */
#endregion

using System;
using System.ComponentModel.Composition;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Animation;
using AppsTracker.Data.Service;
using AppsTracker.Widgets;


namespace AppsTracker.Views
{
    [Export(typeof(IShell))]
    [ExportMetadata("ShellUse", "DbCleaner window")]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public partial class DBCleanerWindow : Window, IShell
    {
        private readonly IDataService dataService;
        private readonly IWindowService windowService;

        [ImportingConstructor]
        public DBCleanerWindow(IDataService dataService, IWindowService windowService)
        {
            InitializeComponent();
            this.dataService = dataService;
            this.windowService = windowService;
        }

        private void lblCancel_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Close();
        }

        private async void lblClean_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            int days;
            if (!int.TryParse(tbDays.Text, out days))
                return;

            overlayGrd.Visibility = System.Windows.Visibility.Visible;
            int count = await Task<int>.Run(() => DeleteOldScreenshots(days));
            overlayGrd.Visibility = System.Windows.Visibility.Collapsed;
            windowService.ShowMessageDialog(string.Format("Deleted {0} screenshots", count));
            Close();
        }

        private void tbDays_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private int DeleteOldScreenshots(int days)
        {
            return dataService.DeleteOldScreenshots(days);
        }


        public object ViewArgument
        {
            get;
            set;
        }
    }
}