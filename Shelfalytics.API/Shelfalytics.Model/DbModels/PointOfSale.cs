using System;
using System.ComponentModel.DataAnnotations;

namespace Shelfalytics.Model.DbModels
{
    public class PointOfSale
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public uint TaxCode { get; set; }
        [Required]
        [MaxLength(200)]
        public string PointOfSaleName { get; set; }
        [Required]
        [MaxLength(300)]
        public string Address { get; set; }
        [Required]
        [MaxLength(100)]
        public string ContactPersonName { get; set; }
        [Required]
        [Phone]
        public string Telephone { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public DateTime OpeningHour { get; set; }
        [Required]
        public DateTime ClosingHour { get; set; }
    }
}
