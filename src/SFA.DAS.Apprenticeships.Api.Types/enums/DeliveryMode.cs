using System.ComponentModel;

namespace SFA.DAS.Apprenticeships.Api.Types.enums
{
    public enum DeliveryMode
    {
        [Description("day release")]
        DayRelease,
        [Description("block release")]
        BlockRelease, 
        [Description("at your location")]
        HundredPercentEmployer   // MFCMFC 100PercentEmployer
    }
}
