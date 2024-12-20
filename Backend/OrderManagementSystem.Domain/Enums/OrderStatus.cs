using System.Runtime.Serialization;

namespace OrderManagementSystem.Domain.Enums
{
    public enum OrderStatus
    {

        [EnumMember(Value = "Pending")]
        Pending = 0,

        [EnumMember(Value = "Completed")]
        Completed = 1,

        [EnumMember(Value = "Canceled")]
        Canceled = 2
    }
}
