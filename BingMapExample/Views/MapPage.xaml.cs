using AutoMapper;
using Bing.Maps;
using BingMapExample.Common;
using BingMapPCL.ViewModels;
using Cirrious.MvvmCross.Plugins.Location;
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

namespace BingMapExample.Views
{
    public sealed partial class MapPage : BasePage
    {
        public GeolocationHelper GeolocationHelper { get; private set; }

        public new MapVM ViewModel
        {
            get { return (MapVM)base.ViewModel; }
            set { base.ViewModel = value; }
        }

        public MapPage()
        {
            this.InitializeComponent();

            GeolocationHelper = new GeolocationHelper();
        }

        protected override void SaveState(SaveStateEventArgs e)
        {
            e.PageState["MapVM"] = ViewModel;

            base.SaveState(e);
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

            ViewModel.CurrentLocation = Mapper.Map<MvxCoordinates>(new Location(geoposition.Latitude, geoposition.Longitude));

            BingMap.SetView(Mapper.Map<Location>(ViewModel.CurrentLocation));

            base.LoadState(e);
        }

        private void Pushpin_Tapped(object sender, TappedRoutedEventArgs e)
        {
            FlyoutBase.ShowAttachedFlyout((FrameworkElement)sender);
        }
    }
}
