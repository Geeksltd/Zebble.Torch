namespace Zebble.Device
{
    using System;
    using System.Threading.Tasks;
    using Windows.Devices.Lights;

    public class Torch
    {
        static Lamp CurrentLamp;
        static async Task<Lamp> GetLamp()
        {
            if (CurrentLamp == null) CurrentLamp = await Lamp.GetDefaultAsync().AsTask();
            return CurrentLamp;
        }

        public static async Task<bool> IsAvailable() => await GetLamp() != null;

        public static async Task TurnOn(OnError errorAction = OnError.Toast) => (await GetLamp()).Set(l => l.IsEnabled = true);

        public static async Task TurnOff(OnError errorAction = OnError.Toast) => (await GetLamp()).Set(l => l.IsEnabled = false);

        static async Task Turn(bool on, OnError errorAction)
        {
            try
            {
                var lamp = await GetLamp();
                if (lamp == null) throw new Exception("Lamp was not found on this device.");

                lamp.IsEnabled = on;
            }
            catch (Exception ex) { await errorAction.Apply(ex); }
        }
    }
}