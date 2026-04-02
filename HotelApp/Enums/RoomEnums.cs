using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

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
