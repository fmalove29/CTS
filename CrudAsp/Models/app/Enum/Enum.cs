using System.ComponentModel;

namespace CrudAsp.Models.app.Enum
{
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
        [Description("2D")]
        C2D = 0,
        [Description("3D")]
        C3D = 1,

        // Premium & Large Format Screens
        IMAX = 2,
        IMAX3D = 3,
        DolbyCinema = 4,
        RPX = 5,
        CinemarkXD = 6,
        ScreenX = 7,
        BarcoEscape = 8,
        UltraPanavision70 = 9,
        FourDX = 10,

        // Luxury & VIP Screens
        DirectorsClub = 11,
        GoldClass = 12,
        PremierScreen = 13,
        DineInTheater = 14,

        // Special Projection Screens
        Cinerama = 15,
        DBox = 16,
        DriveIn = 17, 
        FloatingCinema = 18
    }
    public enum ManagementLevel
    {
        [Description("Executive")]
        Executive = 1,
        [Description("Manager")]
        Manager = 2,
        [Description("Supervisor")]
        Supervisor = 3
    }

}
