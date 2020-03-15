using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace XaBikeStand.ViewModels
{
    class MapViewModel : BaseViewModel
    {

        public MapViewModel()
        {
            //multiple types of "Map" 
            Map = new Xamarin.Forms.Maps.Map();
        }

        //Same ambigous types of "Map"
        public Xamarin.Forms.Maps.Map Map { get; set; }


        #region --Command implementations--

        public ICommand UnlockCMD => new Command<Button>(async (Button UnlockBike) =>
        {
            

            try
            {
                var location = await Geolocation.GetLastKnownLocationAsync();

                if (location != null)
                {
                    Console.WriteLine("Unlock CMD works!");
                    Console.WriteLine($"Latitude: {location.Latitude}, Longitude: {location.Longitude}, Altitude: {location.Altitude}");
                }
            }
            catch (FeatureNotSupportedException fnsEx)
            {
                Console.WriteLine("Handle not supported on device exception");
            }
            catch (FeatureNotEnabledException fneEx)
            {
                Console.WriteLine("Handle not enabled on device exception");
            }
            catch (PermissionException pEx)
            {
                Console.WriteLine("Handle permission exception");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Unable to get location");
            }


        });

        public ICommand LockCMD => new Command<Button>(async (Button LockBike) =>
        {
            

            try
            {
                var location = await Geolocation.GetLastKnownLocationAsync();

                if (location != null)
                {
                    Console.WriteLine("Lock CMD works!");
                    Console.WriteLine($"Latitude: {location.Latitude}, Longitude: {location.Longitude}, Altitude: {location.Altitude}");
                }
            }
            catch (FeatureNotSupportedException fnsEx)
            {
                Console.WriteLine("Handle not supported on device exception");
            }
            catch (FeatureNotEnabledException fneEx)
            {
                Console.WriteLine("Handle not enabled on device exception");
            }
            catch (PermissionException pEx)
            {
                Console.WriteLine("Handle permission exception");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Unable to get location");
            }

        });

        #endregion
    }
}
