using Bing.Maps;
using BingMapExample.Common;
using BingMapExample.DataModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;

namespace BingMapExample.ViewModels
{
    public class MapVM : ObservableObject
    {
        private static Location StartingLocation = new Location(27.952433, -82.462234);

        public Location CurrentLocation { get; set; }
        public ObservableCollection<MapItem> MapItems { get; set; }

        public RelayCommand AddRandomRedRelay { get; set; }
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

        public RelayCommand AddRandomBlueRelay { get; set; }
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

        public RelayCommand ClearMapItemsRelay { get; set; }
        private void ClearMapItems()
        {
            MapItems.Clear();
        }

        public MapVM()
        {
            CurrentLocation = StartingLocation;
            MapItems = new ObservableCollection<MapItem>();

            AddRandomRedRelay = new RelayCommand(() => AddRandomRed());
            AddRandomBlueRelay = new RelayCommand(() => AddRandomBlue());
            ClearMapItemsRelay = new RelayCommand(() => ClearMapItems());
        }
    }
}
