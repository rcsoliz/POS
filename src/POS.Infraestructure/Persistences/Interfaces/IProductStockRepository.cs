﻿using POS.Domain.Entities;

namespace POS.Infraestructure.Persistences.Interfaces
{
    public interface IProductStockRepository
    {
        Task<bool> RegisterProductStock(ProductStock productStock);
    }
}
