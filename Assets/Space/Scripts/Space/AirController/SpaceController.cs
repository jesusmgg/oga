namespace Space.AirController
{
    public class SpaceController : SwordGC.AirController.AirController
    {
        protected override SwordGC.AirController.Device GetNewDevice(int deviceId)
        {
            return new SpaceDevice(deviceId);
        }
    }
}