using System.Runtime.Serialization;

namespace book_rating_api.Models
{
    public enum Role
    {
        [EnumMember(Value = "Admin")]
        Admin,
        [EnumMember(Value = "Member")]
        Member
    }
}