using System.ComponentModel;

namespace CrudAsp.Models.app.Enum
{
    public enum Role
    {
        [Description("Admin")]
        Admin = 1,
        [Description("User")]
        User = 2
    }
    public enum Gender
    {
        [Description("Male")]
        Male = 1,
        [Description("Female")]
        Female = 2,
        [Description("Others")]
        Others = 3
    }

    public enum TicketStatus
    {
        [Description("Booked")]
        Booked = 1,
        [Description("Cancelled")]
        Cancelled = 2
    }

}
