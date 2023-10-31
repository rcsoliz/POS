namespace POS.Domain.Entities
{
    public partial class Category: BaseEntity
    {
        public Category()
        {
            Products = new HashSet<Product>();
        }

     //   public int CategoryId { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;


        public virtual ICollection<Product> Products { get; set; }
    }
}
