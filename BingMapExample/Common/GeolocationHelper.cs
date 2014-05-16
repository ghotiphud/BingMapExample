using Bing.Maps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;

namespace BingMapExample.Common
{
    public class GeolocationHelper
    {
        private Geolocator _geoLocator = new Geolocator();

        public GeolocationHelper()
        {
            //To help conserve power, set the DesiredAccuracy property to indicate to the location platform whether your app needs 
            //high-accuracy data. If no apps require high-accuracy data, the system can save power by not turning on GPS providers.
            //Set DesiredAccuracy to HIGH to enable the GPS to acquire data.
            //Apps that use location information solely for ad targeting should set DesiredAccuracy to Default and use only a 
            //single-shot call pattern to minimize power consumption.
            _geoLocator.DesiredAccuracy = PositionAccuracy.High;
            _geoLocator.MovementThreshold = 25;

            //Most apps, other than real-time navigation apps, don't require a highly accurate, constant stream of location updates. 
            //If your app doesn't require the most accurate stream of data possible, or requires updates infrequently, 
            //set the ReportInterval property to indicate the minimum frequency of location updates that your app needs. 
            //The location source can then conserve power by calculating location only when needed.
            //Apps that do require real-time data should set ReportInterval to 0, to indicate that no minimum interv
            _geoLocator.ReportInterval = 0;
        }

        public async Task<Location> GetCurrentGeoposition()
        {
            var geoposition = await _geoLocator.GetGeopositionAsync();

            var position = geoposition.Coordinate.Point.Position;

            return geoposition != null
                ? new Location(position.Latitude, position.Longitude) 
                : null;
        }
    }
}
