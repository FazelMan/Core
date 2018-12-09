using System.ComponentModel.DataAnnotations;

namespace FazelMan.Core.Common.Enums
{
    public enum ClientTypeEnum
    {
        [Display(Name = "اندروید")]
        Android = 1,

        [Display(Name = "iOS")]
        iOS = 2,

        [Display(Name = "وب")]
        Web = 3,
    }
}