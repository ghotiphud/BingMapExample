using Bing.Maps;
using BingMapExample.Common;
using BingMapExample.DataModels;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.Devices.Geolocation;

namespace BingMapExample.ViewModels
{
    public class MapVM : ReactiveObject
    {
        private static Location StartingLocation = new Location(27.952433, -82.462234);

        public Location _currentLocation;
        public Location CurrentLocation
        {
            get { return _currentLocation; }
            set { this.RaiseAndSetIfChanged(ref _currentLocation, value); }
        }

        public ReactiveCommand LocationChanged { get; protected set; }

        public ObservableCollection<MapItem> MapItems { get; set; }

        public ReactiveCommand AddRandomRedRelay { get; protected set; }

        public ReactiveCommand AddRandomBlueRelay { get; protected set; }

        public ReactiveCommand ClearMapItemsRelay { get; protected set; }

        public MapVM()
        {
            MapItems = new ObservableCollection<MapItem>();

            LocationChanged = new ReactiveCommand();
            this.WhenAnyValue(x => x.CurrentLocation).InvokeCommand(LocationChanged);

            AddRandomRedRelay = new ReactiveCommand();
            AddRandomRedRelay.Subscribe(x => AddRandomRed());

            AddRandomBlueRelay = new ReactiveCommand();
            AddRandomBlueRelay.Subscribe(x => AddRandomBlue());

            ClearMapItemsRelay = new ReactiveCommand();
            ClearMapItemsRelay.Subscribe(x => ClearMapItems());
        }

        private void AddRandomRed()
        {
            var random = new Random();

            for (var i = 0; i < 100; i++)
            {
                var lat = CurrentLocation.Latitude + random.NextDouble() - .5;
                var lon = CurrentLocation.Longitude + random.NextDouble() - .5;

                var location = new Location(lat, lon);

                MapItems.Add(new RedItem { Location = location, Text = "Red!" });
            }
        }

        private void AddRandomBlue()
        {
            var random = new Random();

            for (var i = 0; i < 100; i++)
            {
                var lat = CurrentLocation.Latitude + random.NextDouble() - .5;
                var lon = CurrentLocation.Longitude + random.NextDouble() - .5;

                var location = new Location(lat, lon);

                MapItems.Add(new BlueItem { Location = location, Text = "Blue!" });
            }
        }

        private void ClearMapItems()
        {
            MapItems.Clear();
        }
    }
}
