using System.ComponentModel;

namespace HotelApp.Enums
{
    public class RoomEnums
    {
        public enum RoomTypes
        {
            [Description("Luxury OneBed")]
            LuxuryOneBed,
            [Description("Luxury TwoBed")]
            LuxuryTwoBed,
            [Description("Regular OneBed")]
            RegularOneBed,
            [Description("Regular TwoBed")]
            RegularTwoBed
        }
    }
}
