#nullable disable

using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using static OffRoadWorld.Common.DataValidations.Category;

namespace OffRoadWorld.Data.Models
{
    [Comment("Category of off-road event.")]
    public class Category
    {
        [Key]
        [Comment("Id of category.")]
        public int Id { get; set; }

        [Required]
        [MaxLength(NameMaxLength)]
        [Comment("Name of category.")]
        public string Name { get; set; }

        public ICollection<Event> Events { get; set; } = new HashSet<Event>();
    }
}