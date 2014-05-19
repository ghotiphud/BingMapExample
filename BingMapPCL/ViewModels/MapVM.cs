using BingMapPCL.DataModels;
using Cirrious.MvvmCross.Plugins.Location;
using Cirrious.MvvmCross.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BingMapPCL.ViewModels
{
    public class MapVM : MvxViewModel
    {
        private static MvxCoordinates StartingLocation = new MvxCoordinates{ Latitude = 27.952433, Longitude = -82.462234};

        public MvxCoordinates CurrentLocation { get; set; }
        public ObservableCollection<MapItem> MapItems { get; set; }

        public ICommand AddRandomRedRelay { get; set; }
        public ICommand AddRandomBlueRelay { get; set; }
        public ICommand ClearMapItemsRelay { get; set; }

        public MapVM()
        {
            CurrentLocation = StartingLocation;
            MapItems = new ObservableCollection<MapItem>();

            AddRandomRedRelay = new MvxCommand(() => AddRandomMapItems(() => new RedItem()));
            AddRandomBlueRelay = new MvxCommand(() => AddRandomMapItems(() => new BlueItem()));
            ClearMapItemsRelay = new MvxCommand(() => ClearMapItems());
        }

        private void AddRandomMapItems(Func<MapItem> MapItemFactory)
        {
            var random = new Random();

            for (var i = 0; i < 100; i++)
            {
                var lat = CurrentLocation.Latitude + random.NextDouble() - .5;
                var lon = CurrentLocation.Longitude + random.NextDouble() - .5;

                var item = MapItemFactory();
                item.Location = new MvxCoordinates { Latitude = lat, Longitude = lon };;

                MapItems.Add(item);
            }
        }

        private void ClearMapItems()
        {
            MapItems.Clear();
        }
    }
}
