﻿using Microsoft.EntityFrameworkCore;
using POS.Domain.Entities;
using POS.Infraestructure.Persistences.Contexts;
using POS.Infraestructure.Persistences.Interfaces;
using POS.Utilities.Static;

namespace POS.Infraestructure.Persistences.Repositories
{
    public class DocumentTypeRepository : IDocumentTypeRepository
    {
        private readonly POSContext _context;

        public DocumentTypeRepository(POSContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<DocumentType>> ListDocumentTypes()
        {
            var documentTypes = await _context.DocumentTypes
                .Where(x => x.State.Equals((int)StateTypes.Active))
                .AsNoTracking()
                .ToListAsync();

            return documentTypes;
        }
    }
}
