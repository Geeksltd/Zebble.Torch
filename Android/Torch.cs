namespace Zebble.Device
{
    using Android.Graphics;
    using Android.Hardware;
    using System;
    using System.Threading.Tasks;
    using AndroidCamera = Android.Hardware.Camera;
    using Olive;

    public static class Torch
    {
        static AndroidCamera camera;

        static AndroidCamera Camera
        {
            get
            {
                if (camera != null) return camera;
                camera = AndroidCamera.Open();

                if (camera == null) throw new Exception("Camera failed to initialize");

                return camera;
            }
        }

        public static Task<bool> IsAvailable()
        {
            try
            {
                var cameraParams = Camera?.GetParameters().SupportedFlashModes;
                var result = cameraParams.OrEmpty().Contains(AndroidCamera.Parameters.FlashModeTorch, caseSensitive: false);
                return Task.FromResult(result);
            }
            catch
            {
                return Task.FromResult(result: false);
            }
        }

        public static async Task TurnOn(OnError errorAction = OnError.Toast)
        {
            try
            {
                await SetMode(AndroidCamera.Parameters.FlashModeTorch);
                Camera.StartPreview();
            }
            catch (Exception ex) { await errorAction.Apply(ex); return; }

            // Fix for Nexus 5
            try { Camera.SetPreviewTexture(new SurfaceTexture(0)); }
            catch { /* Ignore. No logging needed. */ }
        }

        public static Task TurnOff() => SetMode(AndroidCamera.Parameters.FlashModeOff);

        async static Task SetMode(string mode)
        {
            if (!(await IsAvailable()))
                throw new Exception("Torch is not available on this device.");

            var param = Camera.GetParameters();
            param.FlashMode = mode;
            Camera.SetParameters(param);
        }
    }
}