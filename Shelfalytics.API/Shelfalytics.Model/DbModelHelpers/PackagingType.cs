using System.ComponentModel.DataAnnotations;

namespace Shelfalytics.Model.DbModelHelpers
{
    public class PackagingType
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(200)]
        public string Name { get; set; }
    }
}
