#nullable disable

using OffRoadWorld.Web.ViewModels.Category;
using System.ComponentModel.DataAnnotations;
using static OffRoadWorld.Common.DataValidations.Event;

namespace OffRoadWorld.Web.ViewModels.Event
{
    public class EventFormModel
    {
        [Required]
        [StringLength(TitleMaxLength, MinimumLength =TitleMinLength)]
        public string Title { get; set; }
       
        [Required]
        [StringLength(DescriptionMaxLength, MinimumLength = DescriptionMinLength)]
        public string Description { get; set; }
        
        [Required]
        public DateTime Start { get; set; }

        [Required]
        public DateTime End { get; set; }

        [Required]
        public int CategoryId { get; set; }

        public string OwnerId { get; set; }

        public ICollection<CategoryModel> Categories { get; set;} = new List<CategoryModel>();
    }
}