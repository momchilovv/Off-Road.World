using System.ComponentModel.DataAnnotations;

namespace OffRoadWorld.Web.ViewModels.Marketplace
{
    public class AddFundsFormModel
    {
        [Required]
        [Range(1.0, double.MaxValue, ErrorMessage = "The minimum amount for deposit is ${1}!")]
        public decimal Amount { get; set; }
    }
}