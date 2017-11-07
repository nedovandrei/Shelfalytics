namespace Shelfalytics.RepositoryInterface.DTO
{
    public class EquipmentPlanogramDTO
    {
        public int ProductId { get; set; }
        public int EquipmentId { get; set; }
        public int Row { get; set; }
        public string ProductName { get; set; }
        public string SKUName { get; set; }
        public string ShortSKUName { get; set; }
        public double BottleDiameter { get; set; }
        public decimal Price { get; set; }
        public string PhotoPath { get; set; }
    }
}
