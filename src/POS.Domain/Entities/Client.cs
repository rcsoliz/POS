using System;
using System.Collections.Generic;

namespace POS.Domain.Entities
{
    public partial class Client: BaseEntity
    {
        public Client()
        {
            Sales = new HashSet<Sale>();
        }

      //  public int ClientId { get; set; }
        public string? Name { get; set; } = null!;
        public int DocumentTypeId { get; set; } 
        public string? DocumentNumber { get; set; } = null!;
        public string? Address { get; set; } = null!;
        public string? Phone { get; set; } = null!;
        public string? Email { get; set; } = null!;

        public virtual DocumentType DocumentType { get; set; } = null!;
        public virtual ICollection<Sale> Sales { get; set; }
    }
}
