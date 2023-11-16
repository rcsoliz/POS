﻿using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Configuration;
using POS.Domain.Entities;
using POS.Infraestructure.Persistences.Contexts;
using POS.Infraestructure.Persistences.Interfaces;
using System.Data;

namespace POS.Infraestructure.Persistences.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly POSContext _context;

        public IUserRepository _user = null!;
        public IGenericRepository<Category> _category = null!;
        public IGenericRepository<Provider> _provider = null!;
        public IGenericRepository<DocumentType> _documentType = null!;
        public IGenericRepository<Client> _client = null!;
        public IGenericRepository<Product> _product= null!;
        public IWareHouseRepository _warehouse = null!;
        public IProductStockRepository _productStock = null!;
        public UnitOfWork(POSContext context, IConfiguration configuration)
        {
            _context = context;
        }

        public IGenericRepository<Category> Category => _category?? new GenericRepository<Category>(_context);
        public IGenericRepository<Provider> Provider => _provider ?? new GenericRepository<Provider>(_context);
        public IGenericRepository<DocumentType> DocumentType => _documentType ?? new GenericRepository<DocumentType>(_context);
        public IGenericRepository<Client> Client => _client ?? new GenericRepository<Client>(_context);
        public IGenericRepository<Product> Product => _product ?? new GenericRepository<Product>(_context);

        public IUserRepository User => _user ?? new UserRepository(_context);

        public IWareHouseRepository WareHouse => _warehouse??  new WareHouseRepository(_context);

        public IProductStockRepository ProductStock => _productStock ?? new ProductStockRepository(_context);

        public IDbTransaction BeginTransaction()
        {
            var transaction = _context.Database.BeginTransaction();
            return transaction.GetDbTransaction();
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public void SaveChages()
        {
            _context.SaveChanges();
        }

        public async Task SaveChangeAsync()
        {
            await _context.SaveChangesAsync();  
        }
    }
}
