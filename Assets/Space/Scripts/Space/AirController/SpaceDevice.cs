using SwordGC.AirController;

namespace Space.AirController
{
    public class SpaceDevice : Device
    {
        public SpaceDevice(int deviceId) : base(deviceId)
        {
        }

        public override string Classes
        {
            get
            {
                if (HasPlayer)
                {
                    return "player" + PlayerId;
                }
                else
                {
                    return "";
                }
            }
        }

        public override string View
        {
            get
            {
                if (HasPlayer)
                {
                    return "Gameplay";
                }
                else
                {
                    return "Full";
                }
            }
        }
    }
}