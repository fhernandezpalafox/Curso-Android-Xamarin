using Android.App;
using Android.OS;
using Xamarin.Essentials;
using System;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using AlertDialog = Android.App.AlertDialog;
using static Android.Support.V4.App.ActivityCompat;

using Android.Gms.Maps.Model;
using Android.Locations;
using Android.Gms.Maps;
using Android.Runtime;
using Android.Content;
using Android.Support.V7.App;
using static Android.Gms.Maps.GoogleMap;
using Android.Widget;
using Location = Android.Locations.Location;

namespace App10
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity, IOnMapReadyCallback, 
                                                    ILocationListener, 
                                                    IOnInfoWindowClickListener

    {
        private MapFragment mapFragment;
        private GoogleMap map;
        private MarkerOptions markerLocation;

        private static readonly LatLng deLaSalle = new LatLng(21.1484813, -101.7074495);
        private static readonly LatLng Casa = new LatLng(21.1269137, -101.6523417);

        private LocationManager locationManager;
        private String provider;

        //Interface : IOnMarkerClickListener
        public bool OnMarkerClick(Marker marker)
        {
            Toast.MakeText(this, marker.Title, ToastLength.Long).Show();
            return false;
        }

        //Interface :  IOnMapReadyCallback
        public void OnMapReady(GoogleMap googleMap)
        {

            map = googleMap;
            map.UiSettings.MyLocationButtonEnabled = true;
 
            map.UiSettings.ZoomControlsEnabled = true;

            map.SetOnInfoWindowClickListener(this);

            Anotaciones();
        }

        public void crearMarker(GoogleMap map) {

            MarkerOptions markerOpt1 = new MarkerOptions();
            markerOpt1.SetPosition(new LatLng(21.126840, -101.652270));
            markerOpt1.SetTitle("Mi ubicacion");
            map.AddMarker(markerOpt1);

        }

        public async System.Threading.Tasks.Task GestionPermisos(GoogleMap map) {

            try
            {
                var status = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Location);
                if (status != PermissionStatus.Granted)
                {
                    if (await CrossPermissions.Current.ShouldShowRequestPermissionRationaleAsync(Permission.Location))
                    {
                        AlertDialog.Builder builder = new AlertDialog.Builder(this);
                        builder.SetMessage("Necesita acceder la aplicacion a tu geolocalizacion")
                               .SetTitle("Permisos")
                               .SetPositiveButton("Aceptar", (senderAlert, args) =>
                               {
                                   Console.WriteLine("Dijo que si");
                               });
                        builder.Create().Show();
                    }

                    var results = await CrossPermissions.Current.RequestPermissionsAsync(Permission.Location);
                    //Best practice to always check that the key exists
                    if (results.ContainsKey(Permission.Location)){

                        status = results[Permission.Location];

                        if (status == PermissionStatus.Granted)
                        {
                            Localizacion();
                        }
                    }
                        
                }

                if (status == PermissionStatus.Granted)
                {
                    Localizacion();
                   
                }
                else if (status != PermissionStatus.Unknown)
                {
                    Console.WriteLine("Error de permisos");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error "+ex);
                
            }

        }

        public async System.Threading.Tasks.Task  geoLocalizacion(GoogleMap map) {

            map.MyLocationEnabled = true;
            map.UiSettings.MyLocationButtonEnabled = true;

            try
            {
                var request = new GeolocationRequest(GeolocationAccuracy.Medium);
                var location = await Geolocation.GetLocationAsync(request);

                if (location != null)
                {
                    Console.WriteLine($"Latitude: {location.Latitude}, Longitude: {location.Longitude}, Altitude: {location.Altitude}");

                    LatLng latLng = new LatLng(location.Latitude, location.Longitude);
                    Marker perth = map.AddMarker(new MarkerOptions()
                                              .SetPosition(latLng)
                                              .SetTitle("Mi ubicacion")
                                              .SetSnippet("Mi hogar"));
                }
            }
            catch (FeatureNotSupportedException fnsEx)
            {
                // Handle not supported on device exception
            }
            catch (FeatureNotEnabledException fneEx)
            {
                // Handle not enabled on device exception
            }
            catch (PermissionException pEx)
            {
                // Handle permission exception
            }
            catch (Exception ex)
            {
                // Unable to get location
            }
        }

        public void crearPoligono(GoogleMap map) {

            PolygonOptions rectOptions = new PolygonOptions();

            rectOptions.Add(new LatLng(37.35, -122.0));
            rectOptions.Add(new LatLng(37.45, -122.0));
            rectOptions.Add(new LatLng(37.45, -122.2));
            rectOptions.Add(new LatLng(37.35, -122.2));
             
            // notice we don't need to close off the polygon

            map.AddPolygon(rectOptions);

        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);

            var mapFragment = (MapFragment)FragmentManager.FindFragmentById(Resource.Id.map);
            mapFragment.GetMapAsync(this);

            Plugin.CurrentActivity.CrossCurrentActivity.Current.Init(this, savedInstanceState);

            InicializarMapa();

            Localizacion();

            

        }

        public void Localizacion()
        {
            GestionPermisos(map);

        }

        protected override void OnResume()
        {
            base.OnResume();
            //locationManager.RequestLocationUpdates(provider, 400, 1, this);
        }

        protected override void OnPause()
        {
            base.OnPause();
            //locationManager.RemoveUpdates(this);
        }

        public void Anotaciones()
        {
            MarkerOptions markerOpt1 = new MarkerOptions();
            markerOpt1.SetPosition(Casa);
            markerOpt1.SetTitle("Casa");
            markerOpt1.SetIcon(BitmapDescriptorFactory.DefaultMarker(BitmapDescriptorFactory.HueBlue));
            map.AddMarker(markerOpt1);

            MarkerOptions markerOpt2 = new MarkerOptions();
            markerOpt2.SetPosition(deLaSalle);
            markerOpt2.SetTitle("De la Salle");
            markerOpt2.SetSnippet("Universidad");
            BitmapDescriptor icon = BitmapDescriptorFactory.FromResource(Resource.Drawable.abc_ab_share_pack_mtrl_alpha);
            markerOpt2.SetIcon(icon);
            map.AddMarker(markerOpt2);

            // We create an instance of CameraUpdate, and move the map to it.
            CameraUpdate cameraUpdate = CameraUpdateFactory.NewLatLngZoom(Casa, 15);
            map.MoveCamera(cameraUpdate);
        }

        public void InicializarMapa()
        {
            mapFragment = FragmentManager.FindFragmentByTag("map") as MapFragment;

            if (mapFragment == null)
            {
                GoogleMapOptions mapOptions = new GoogleMapOptions()
                    .InvokeMapType(GoogleMap.MapTypeNormal)
                    .InvokeZoomControlsEnabled(true)
                    .InvokeCompassEnabled(true);

                FragmentTransaction fragTx = FragmentManager.BeginTransaction();
                mapFragment = MapFragment.NewInstance(mapOptions);
                fragTx.Add(Resource.Id.map, mapFragment, "map");
                fragTx.Commit();
            }

            mapFragment.GetMapAsync(this);
        }


        //Interface :  ILocationListener
        public void OnLocationChanged(Location location)
        {
            Double lat, lng;

            lat = location.Latitude;
            lng = location.Longitude;

            if (markerLocation == null)
            {
                markerLocation = new MarkerOptions();
                markerLocation.SetPosition(new LatLng(lat, lng));
                markerLocation.SetTitle("Mi Posicion");
                map.AddMarker(markerLocation);
            }
            else
            {
                markerLocation.SetPosition(new LatLng(lat, lng));
            }



            CameraPosition.Builder builder = CameraPosition.InvokeBuilder();
            builder.Target(new LatLng(lat, lng));


            CameraUpdate cameraUpdate = CameraUpdateFactory.NewLatLngZoom(new LatLng(lat, lng), 15);
            map.MoveCamera(cameraUpdate);

        }

        public void OnProviderDisabled(string provider)
        {
            // throw new NotImplementedException();
        }

        public void OnProviderEnabled(string provider)
        {
            //throw new NotImplementedException();
        }

        public void OnStatusChanged(string provider, [GeneratedEnum] Availability status, Bundle extras)
        {
            //throw new NotImplementedException();
        }


        //interface: IOnInfoWindowClickListener
        public void OnInfoWindowClick(Marker marker)
        {
            Toast.MakeText(this, marker.Title, ToastLength.Long).Show();
        }
    }
}