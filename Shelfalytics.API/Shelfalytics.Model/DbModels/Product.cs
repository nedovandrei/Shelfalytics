using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Shelfalytics.Model.DbModelHelpers;

namespace Shelfalytics.Model.DbModels
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string EAN { get; set; }
        [Required]
        public string SKUName { get; set; }
        [Required]
        public string ShortSKUName { get; set; }
        [Required]
        [MaxLength(100)]
        public string TradeMark { get; set; }
        [Required]        
        public decimal Price { get; set; }
        [Required]
        public double Volume { get; set; }
        [Required]
        [MaxLength(20)]
        public string UnitOfMeasurement { get; set; }
        public int PackagingTypeId { get; set; }
        [ForeignKey("PackagingTypeId")]
        public PackagingType PackagingType { get; set; }
    }
}
