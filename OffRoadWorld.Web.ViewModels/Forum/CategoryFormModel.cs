#nullable disable

using System.ComponentModel.DataAnnotations;
using static OffRoadWorld.Common.DataValidations.Forum;

namespace OffRoadWorld.Web.ViewModels.Forum
{
    public class CategoryFormModel
    {
        [Required]
        [StringLength(TitleMaxLenght, MinimumLength = TitleMinLength)]
        public string Title { get; set; }

        [Required]
        [StringLength(DescriptionMaxLength, MinimumLength = DescriptionMinLength)]
        public string Description { get; set; }
    }
}