using Bing.Maps;
using BingMapExample.Common;
using BingMapExample.DataModels;
using BingMapExample.ViewModels;
using ReactiveUI;
using System.Reactive.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Diagnostics;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Windows.Input;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Items Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234233

namespace BingMapExample.Views
{
    public sealed partial class MapPage : BasePage, IViewFor<MapVM>
    {
        public GeolocationHelper GeolocationHelper { get; private set; }
        public MapVM ViewModel { get; set; }
        object IViewFor.ViewModel { get { return ViewModel; } set { ViewModel = (MapVM)value; } }

        public MapPage()
        {
            this.InitializeComponent();

            GeolocationHelper = new GeolocationHelper();
        }

        protected override void SaveState(SaveStateEventArgs e)
        {
            e.PageState["MapVM"] = ViewModel;
        }

        protected override async void LoadState(LoadStateEventArgs e)
        {
            // Restore the viewModel state
            if (e.PageState != null && e.PageState.ContainsKey("MapVM"))
            {
                ViewModel = (MapVM)e.PageState["MapVM"];
            }
            else
            {
                ViewModel = new MapVM();
            }
            DataContext = ViewModel;

            // Any other operations
            var geoposition = await GeolocationHelper.GetCurrentGeoposition();

            ViewModel.CurrentLocation = new Location(geoposition.Latitude, geoposition.Longitude);

            BingMap.SetView(ViewModel.CurrentLocation);
        }

        private void Pushpin_Tapped(object sender, TappedRoutedEventArgs e)
        {
            var pushpin = (Pushpin)sender;
            var mapItem = (MapItem)pushpin.DataContext;

            ViewModel.CurrentLocation = mapItem.Location;

            FlyoutBase.ShowAttachedFlyout((FrameworkElement)sender);

            // stop event from bubbling up to Map.
            e.Handled = true;
        }
    }
}
