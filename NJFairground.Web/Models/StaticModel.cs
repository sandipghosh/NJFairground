
namespace NJFairground.Web.Models
{
    public enum Page
    {
        Main = 1,
        Home = 2,
        Fair = 3,
        Event = 4,
        HorseShow = 5,
        AGLearningCenter = 6,
        ConservatoryAndCourtyard = 7,
        EventRental = 8,
        Promote = 9,
        Directions = 10,
        WhatsNew = 11,
        Info = 12,
        Map = 13,
        DailyHighlights = 14,
        Fun = 15,
        Food = 16,
        Shopping = 17,
        Social = 18,
        Complex = 19,
        DirectionToFairground = 20,
        Carnival = 21,
        CircusHollywood = 22,
        CraftTent = 23,
        CommercialInfo = 24,
        CommercialCraftTent = 25,
        FoodAndBeverage = 27,
        NonProfit = 28
    }

    public enum FeedFor
    {
        Facebook = 1,
        Twitter = 2,
        Pinterest = 3,
        Instagram = 4
    }

    public enum StatusEnum
    {
        Active = 1,
        Inactive = 2
    }

    public enum DeliveryScheduleEnum
    {
        StandardTurnaround = 1,
        ExpeditedTurnaround = 2
    }

    public enum AddressTypeEnum
    {
        Shipping = 1,
        Billing = 2
    }

    public enum PaymentTypeEnum
    {
        CreditCard = 1,
        Paypal = 2
    }

    public enum AttachmentTypeEnum
    {
        PDF,
        Archive
    }

    public enum DisplayProperty
    {
        Name,
        Description
    }

    public enum ResponseStatus
    {
        success,
        failure
    }

    public enum SponsorType
    {
        Splash = 1,
        Banner = 2
    }

    public enum MobileDeviceType
    {
        Android = 1,
        iOS = 2
    }

    public enum NotificationType
    {
        info = 1,
        warning = 2,
        danger = 3,
        success = 4
    }
}