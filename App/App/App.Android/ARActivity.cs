using System;
using System.Collections.Generic;
using System.Json;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Locations;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Com.Wikitude.Architect;
using Com.Wikitude.Common.Camera;

namespace App.Droid
{
    [Activity(Label = "ARActivty", ConfigurationChanges = Android.Content.PM.ConfigChanges.Orientation | Android.Content.PM.ConfigChanges.KeyboardHidden | Android.Content.PM.ConfigChanges.ScreenSize)]
    public class ARActivity : Activity, ILocationListener, ArchitectView.ISensorAccuracyChangeListener
    {
        private bool injectedPoi = false;


        public readonly static string IntentExtrasKeyExperienceData = "ExperienceData";
        protected ArchitectView architectView;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);


            // Create your application here
            var config = new ArchitectStartupConfiguration
            {
                LicenseKey = "A2ZnCv82eu1ze2cZNMort/BqAhByBxx8DYLFFxui4JGOjVNGKDRigSECd6AmwNU+/pYXTDBpXHgOvAeCX2ocW66syFbWekO0FoAeDntOy5KKuDb+dyaQFzXiOxoDc6ldI7PuYN4I9QUhf5eVVn3isVW70M/6XJo0dJJ5JCr4GahTYWx0ZWRfX8lZDLlCDRflGsptEHC7g7TyP7iG3rtcxePrrNk/pN+hgvLmVYyWTMXhDFwblbP+E7VLLQ8Ddsi9RdVjhvT/FLS7jVhOlKuJq8UH49YC13n6tLAU1WP2J+LIrrNjBbwrma7xCCmPNn/i/U3RAJrd2PmGOUfZxzjk8FpKZOS4797HpTMTI/HXJhBPDsGR9OlERF3WpcXRo47RDIWwT4cJ26fBAdHT1OxCvnu0DG55Lq/cirkmuFE7gpEUYvnXGZRSIAt/OUO//XNle1KSQM7QHHlg33zuDjjSzwWo1/3kApDmjSpjfZktjon/F2jcpyewDFubdrTPScWYSVUvx9ccpSn0kONvWwSM0pCcS9IsMdPthdx9hpSESy+3zLxt4+OH2VrWHls2mI6EuLWMBkZ753SKHqpNiyNf2H6ZRBtMzfezpnvY8W7gL+bpOqXDzDR7G3azmyZE7sgprGoYWMTq54dJMO06/w83QBcipmfwuQ4p4EuGriSfga6wxwS2buHGCuTwTf5hC/kfU6cfXAjNBr9nRZ+SoTBJ6A==",
                CameraPosition = CameraSettings.CameraPosition.Back,
                CameraResolution = CameraSettings.CameraResolution.FULLHD1920x1080,
                CameraFocusMode = CameraSettings.CameraFocusMode.Continuous,
                ArFeatures = ArchitectStartupConfiguration.Features.ImageTracking
            };

            architectView = new ArchitectView(this);
            architectView.OnCreate(config);

            SetContentView(architectView);

        }

        protected override void OnPostCreate(Bundle savedInstanceState)
        {
            base.OnPostCreate(savedInstanceState);
            architectView.OnPostCreate();
            architectView.Load("index.html");

            if (!injectedPoi)
            {
                Random random = new Random();

                JsonValue poi;
                var poiInfo = new Dictionary<string, JsonValue>
                {
                    {"id", 0},
                    {"name", "Boulder"},
                    {"description", "Description here"},
                    {"latitude", 40.0076f },
                    {"longitude", 105.2659f },
                    {"altitude",  5000f}

                };


                poi = new JsonObject(poiInfo);


                architectView.CallJavascript("World.loadPoisFromJsonData(" + poi.ToString() + ")");
                injectedPoi = true;
            }
        }
        protected override void OnResume()
        {
            base.OnResume();
            architectView.OnResume();
        }

        protected override void OnPause()
        {
            base.OnPause();
            architectView.OnPause();
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            architectView.OnDestroy();
        }

        public virtual void OnLocationChanged(Location location)
        {
            float accuracy = location.HasAccuracy ? location.Accuracy : 1000;
            if (location.HasAltitude)
            {
                architectView.SetLocation(location.Latitude, location.Longitude, location.Altitude, accuracy);
            }
            else
            {
                architectView.SetLocation(location.Latitude, location.Longitude, accuracy);
            }
            
            

                /*for (int i = 0; i < 20; i++)
                {
                    double[] poiLocationLatLon = GetRandomLatLonNearby(random, location.Latitude, location.Longitude);
                    float unknownAltitude = -32768f;  // equals "AR.CONST.UNKNOWN_ALTITUDE" in JavaScript (compare AR.GeoLocation specification)
                                                      // Use "AR.CONST.UNKNOWN_ALTITUDE" to tell ARchitect that altitude of places should be on user level. Be aware to handle altitude properly in locationManager in case you use valid POI altitude value (e.g. pass altitude only if GPS accuracy is <7m).

                    var id = i + 1;
                    var poiInformation = new Dictionary<string, JsonValue>
                {
                    {"id", 0},
                    {"name", "place"},
                    {"description", "Description here"},
                    {"latitude", poiLocationLatLon[0] },
                    {"longitude", poiLocationLatLon[1] },
                    {"altitude", unknownAltitude }
                };
                    pois[i] = new JsonObject(poiInformation);
                }
                */

            
        }

        /*
         * The very basic Activity setup of this sample app does not handle the following callbacks
         * to keep the sample app as small as possible. They should be used to handle changes in a production app.
         */
        public void OnProviderDisabled(string provider) { }

        public void OnProviderEnabled(string provider) { }

        public void OnStatusChanged(string provider, [GeneratedEnum] Availability status, Bundle extras) { }


        /*
         * The ArchitectView.ISensorAccuracyChangeListener notifies of changes in the accuracy of the compass.
         * This can be used to notify the user that the sensors need to be recalibrated.
         *
         * This listener has to be registered after OnCreate and unregistered before OnDestroy in the ArchitectView.
         */
        public void OnCompassAccuracyChanged(int accuracy)
        {
            if (accuracy < 2)
            { // UNRELIABLE = 0, LOW = 1, MEDIUM = 2, HIGH = 3
                
            }
        }

        private static double[] GetRandomLatLonNearby(Random random, double latitude, double longitude)
        {
            double minimumOffset = -0.3;
            double maximumOffset = 0.3;

            double latitudeOffset = random.NextDouble() * (maximumOffset - minimumOffset) + minimumOffset;
            double longitudeOffset = random.NextDouble() * (maximumOffset - minimumOffset) + minimumOffset;
            return new double[] { latitude + latitudeOffset, longitude + longitudeOffset };
        }

    }
    
}