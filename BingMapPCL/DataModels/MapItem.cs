using Cirrious.MvvmCross.Plugins.Location;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BingMapPCL.DataModels
{
    public abstract class MapItem
    {
        public MvxCoordinates Location { get; set; }
        public string Text { get; set; }
    }

    public class RedItem : MapItem
    {
    }

    public class BlueItem : MapItem
    {
    }
}
