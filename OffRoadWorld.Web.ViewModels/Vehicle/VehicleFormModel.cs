#nullable disable

using OffRoadWorld.Web.ViewModels.Category;
using System.ComponentModel.DataAnnotations;
using static OffRoadWorld.Common.DataValidations.Vehicle;

namespace OffRoadWorld.Web.ViewModels.Vehicle
{
    public class VehicleFormModel
    {
        [Required]
        [StringLength(MakeMaxLength, MinimumLength = MakeMinLength)]
        public string Make { get; set; }

        [Required]
        [StringLength(ModelMaxLength, MinimumLength = ModelMinLength)]
        public string Model { get; set; }

        [Required]
        [MaxLength(UrlMaxLength)]
        public string ImageUrl { get; set; }

        [Required]
        [Range(MinProductionYear, MaxProductionYear)]
        public int ProductionYear { get; set; }

        [Required]
        [Range(MinHorsePower, MaxHorsePower)]
        public int HorsePower { get; set; }

        [Range(MinEngineCapacity, MaxEngineCapacity)]
        public int? EngineCapacity { get; set; }

        [Required]
        [Range(1.00, double.MaxValue)]
        public decimal Price { get; set; }

        [Required]
        public int CategoryId { get; set; }

        public ICollection<CategoryModel> Categories { get; set; } = new List<CategoryModel>();
    }
}