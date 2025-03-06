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

    public enum PaymentMethod
    {
        Cash = 1,
        Bank = 2,
        EWallet = 3
    }

    public enum BookingStatus
    {
        Pending = 0,
        Confirmed = 1,
        Cancelled = 2,
        Completed = 3
    }

    public enum ScreenType
    {
        // Standard Screens
        TwoD,
        ThreeD,

        // Premium & Large Format Screens
        IMAX,
        IMAX3D,
        DolbyCinema,
        RPX,
        CinemarkXD,
        ScreenX,
        BarcoEscape,
        UltraPanavision70,
        FourDX,

        // Luxury & VIP Screens
        DirectorsClub,
        GoldClass,
        PremierScreen,
        DineInTheater,

        // Special Projection Screens
        Cinerama,
        DBox,
        DriveIn,
        FloatingCinema
    }



}
