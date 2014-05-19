using BingMapPCL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace BingMapExample.Views
{
    public class MapItemDataTemplateSelector : DataTemplateSelector
    {
        public DataTemplate RedTemplate { get; set; }
        public DataTemplate BlueTemplate { get; set; }

        protected override DataTemplate SelectTemplateCore(object item, DependencyObject container)
        {
            var mapItem = (MapItem)item;
            DataTemplate dt = null;

            // Switch on type... dumb C#
            if (mapItem is RedItem) dt = RedTemplate;
            if (mapItem is BlueItem) dt = BlueTemplate;

            return dt;
        }
    }
}
