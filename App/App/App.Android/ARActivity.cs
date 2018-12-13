using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Com.Wikitude.Architect;
using Com.Wikitude.Common.Camera;

namespace App.Droid
{
    [Activity(Label = "ARActivty", ConfigurationChanges = Android.Content.PM.ConfigChanges.Orientation | Android.Content.PM.ConfigChanges.KeyboardHidden | Android.Content.PM.ConfigChanges.ScreenSize)]
    public class ARActivity : Activity
    {
        public readonly static string IntentExtrasKeyExperienceData = "ExperienceData";
        protected ArchitectView architectView;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);


            // Create your application here
            var config = new ArchitectStartupConfiguration
            {
                LicenseKey = "A2ZnCv82eu1ze2cZNMort/BqAhByBxx8DYLFFxui4JGOjVNGKDRigSECd6AmwNU+/pYXTDBpXHgOvAeCX2ocW66syFbWekO0FoAeDntOy5KKuDb+dyaQFzXiOxoDc6ldI7PuYN4I9QUhf5eVVn3isVW70M/6XJo0dJJ5JCr4GahTYWx0ZWRfX8lZDLlCDRflGsptEHC7g7TyP7iG3rtcxePrrNk/pN+hgvLmVYyWTMXhDFwblbP+E7VLLQ8Ddsi9RdVjhvT/FLS7jVhOlKuJq8UH49YC13n6tLAU1WP2J+LIrrNjBbwrma7xCCmPNn/i/U3RAJrd2PmGOUfZxzjk8FpKZOS4797HpTMTI/HXJhBPDsGR9OlERF3WpcXRo47RDIWwT4cJ26fBAdHT1OxCvnu0DG55Lq/cirkmuFE7gpEUYvnXGZRSIAt/OUO//XNle1KSQM7QHHlg33zuDjjSzwWo1/3kApDmjSpjfZktjon/F2jcpyewDFubdrTPScWYSVUvx9ccpSn0kONvWwSM0pCcS9IsMdPthdx9hpSESy+3zLxt4+OH2VrWHls2mI6EuLWMBkZ753SKHqpNiyNf2H6ZRBtMzfezpnvY8W7gL+bpOqXDzDR7G3azmyZE7sgprGoYWMTq54dJMO06/w83QBcipmfwuQ4p4EuGriSfga6wxwS2buHGCuTwTf5hC/kfU6cfXAjNBr9nRZ+SoTBJ6A==",
                CameraPosition = CameraSettings.CameraPosition.Back ,
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
            architectView.Load("test.html");
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

    }
    
}