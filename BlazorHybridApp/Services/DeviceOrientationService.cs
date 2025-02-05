using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#if ANDROID
using Android.Content;
using Android.Views;
using Android.Runtime;
#elif IOS
using UIKit;
#endif

namespace BlazorHybridApp.Services
{
    public class DeviceOrientationService
    {
        public DeviceOrientation GetOrientation()
        {
#if ANDROID
            // Get the Window Manager for Android
            IWindowManager windowManager = Android.App.Application.Context
                .GetSystemService(Context.WindowService).JavaCast<IWindowManager>();

            // Read the device's current rotation
            SurfaceOrientation orientation = windowManager.DefaultDisplay.Rotation;

            bool isLandscape = orientation == SurfaceOrientation.Rotation90
                || orientation == SurfaceOrientation.Rotation270;

            return isLandscape ? DeviceOrientation.Landscape : DeviceOrientation.Portrait;

#elif IOS
            // For iOS, read the status bar orientation
            UIInterfaceOrientation orientation = UIApplication.SharedApplication.StatusBarOrientation;
            bool isPortrait = orientation == UIInterfaceOrientation.Portrait
                || orientation == UIInterfaceOrientation.PortraitUpsideDown;

            return isPortrait ? DeviceOrientation.Portrait : DeviceOrientation.Landscape;
#else
            // If it's neither ANDROID nor IOS, we return Undefined
            return DeviceOrientation.Undefined;
#endif
        }
    }
}
