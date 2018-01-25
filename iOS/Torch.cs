namespace Zebble.Device
{
    using System;
    using System.Threading.Tasks;
    using AVFoundation;

    public static class Torch
    {
        public static Task<bool> IsAvailable()
        {
            using (var device = GetCamera())
                return Task.FromResult(device?.HasTorch == true || device?.HasFlash == true);
        }

        static AVCaptureDevice GetCamera() => AVCaptureDevice.DefaultDeviceWithMediaType(AVMediaType.Video);

        public static Task TurnOn(OnError errorAction = OnError.Toast) => Turn(AVCaptureTorchMode.On, errorAction);

        public static Task TurnOff(OnError errorAction = OnError.Toast) => Turn(AVCaptureTorchMode.Off, errorAction);

        static async Task Turn(AVCaptureTorchMode mode, OnError errorAction)
        {
            using (var captureDevice = GetCamera())
            {
                if (captureDevice == null) throw new Exception("Camera device was not found.");

                try
                {
                    captureDevice.LockForConfiguration(out var error);

                    if (error != null) throw new Exception(error.Description);

                    if (captureDevice.TorchMode != mode)
                        captureDevice.TorchMode = mode;

                    captureDevice.UnlockForConfiguration();
                }
                catch (Exception ex) { await errorAction.Apply(ex); }
            }
        }
    }
}