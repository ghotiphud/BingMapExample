using Bing.Maps;
using BingMapExample.Common;
using BingMapExample.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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

namespace BingMapExample
{
    /// <summary>
    /// A page that displays a collection of item previews.  In the Split App this page
    /// is used to display and select one of the available groups.
    /// </summary>
    public sealed partial class MapPage : Page
    {
        public NavigationHelper NavigationHelper { get; private set; }
        public GeolocationHelper GeolocationHelper { get; private set; }
        public MapVM MapVM { get; set; }

        public MapPage()
        {
            this.InitializeComponent();
            NavigationHelper = new NavigationHelper(this);
            NavigationHelper.LoadState += navigationHelper_LoadState;
            NavigationHelper.SaveState += navigationHelper_SaveState;

            GeolocationHelper = new GeolocationHelper();
        }

        private void navigationHelper_SaveState(object sender, SaveStateEventArgs e)
        {
            e.PageState["MapVM"] = MapVM;
        }

        private async void navigationHelper_LoadState(object sender, LoadStateEventArgs e)
        {
            // Restore the viewModel state
            if (e.PageState != null && e.PageState.ContainsKey("MapVM"))
            {
                MapVM = (MapVM)e.PageState["MapVM"];
            }
            else
            {
                MapVM = new MapVM();
            }
            DataContext = MapVM;

            // Any other operations
            var geoposition = await GeolocationHelper.GetCurrentGeoposition();

            MapVM.CurrentLocation = new Location(geoposition.Latitude, geoposition.Longitude);

            BingMap.SetView(MapVM.CurrentLocation);
        }

        // Wire up NavigationHelper
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            NavigationHelper.OnNavigatedTo(e);
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            NavigationHelper.OnNavigatedFrom(e);
        }

        private void Pushpin_Tapped(object sender, TappedRoutedEventArgs e)
        {
            FlyoutBase.ShowAttachedFlyout((FrameworkElement)sender);
        }
    }
}
