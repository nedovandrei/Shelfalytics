using System;

namespace Shelfalytics.RepositoryInterface.DTO
{
    public class PointOfSaleDTO
    {
        public int Id { get; set; }
        public uint TaxCode { get; set; }
        public string PointOfSaleName { get; set; }
        public string ChainName { get; set; }
        public string Address { get; set; }
        public string ContactPersonName { get; set; }
        public string Telephone { get; set; }
        public string Email { get; set; }
        public DateTime OpeningHour { get; set; }
        public DateTime ClosingHour { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string City { get; set; }
        public string TradeChannel { get; set; }
    }
}
