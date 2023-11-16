namespace POS.Domain.Entities
{
    public partial class Product: BaseEntity
    {
        //public Product()
        //{
        //    PurcharseDetails = new HashSet<PurcharseDetail>();
        //    SaleDetails = new HashSet<SaleDetail>();
        //}

        //public int ProductId { get; set; }
        public string Code { get; set; } = null!;
        public string Name { get; set; } = null!;
        public int StockMin { get; set; }
        public int StockMax { get; set; }
        public string? Image { get; set; } = null!;
        public int CategoryId { get; set; } 
 
        public virtual Category Category { get; set; } = null!;

        public virtual ICollection<ProductStock> ProductStocks { get; set; } = null!;

       // public virtual ICollection<PurcharseDetail> PurcharseDetails { get; set; }
       // public virtual ICollection<SaleDetail> SaleDetails { get; set; }
    }
}
