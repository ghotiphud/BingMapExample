using AutoMapper;
using Bing.Maps;
using Cirrious.CrossCore.Platform;
using Cirrious.MvvmCross.Plugins.Location;
using Cirrious.MvvmCross.ViewModels;
using Cirrious.MvvmCross.WindowsStore.Platform;
using Windows.UI.Xaml.Controls;

namespace BingMapExample
{
    public class Setup : MvxStoreSetup
    {
        public Setup(Frame rootFrame) : base(rootFrame)
        {
        }

        protected override IMvxApplication CreateApp()
        {
            Mapper.CreateMap<MvxCoordinates, Location>();
            Mapper.CreateMap<Location, MvxCoordinates>();

            return new BingMapPCL.App();
        }
		
        protected override IMvxTrace CreateDebugTrace()
        {
            return new DebugTrace();
        }
    }
}