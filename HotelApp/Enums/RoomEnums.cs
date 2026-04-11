using System.ComponentModel;
using System.Reflection;

namespace HotelApp.Enums
{
    public enum RoomType
    {
        [Description("Enkelrum")]
        OneBed,
        [Description("Tvåbäddsrum")]
        TwoBed
    }
    public static class RoomEnums
    {        
        public static string GetDescription(this Enum value)
        {
            var field = value.GetType().GetField(value.ToString());
            if (field == null)
            {
                return value.ToString();
            }

            var attribute = field.GetCustomAttribute<DescriptionAttribute>();
            return attribute != null ? attribute.Description : value.ToString();
        }
    }
}
