using System.ComponentModel.DataAnnotations;

namespace Shelfalytics.Model.DbModels
{
    public class Client
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string ClientName { get; set; }
        [Required]
        [MaxLength(200)]
        public string Address { get; set; }
        [Required]
        [MaxLength(100)]
        public string ContactPersonName { get; set; }
        [Required]
        [MaxLength(30)]
        [Phone]
        public string Telephone { get; set; }
        [Required]
        [MaxLength(100)]
        [EmailAddress]
        public string Email { get; set; }
    }
}
