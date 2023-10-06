﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using POS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Infraestructure.Persistences.Contexts.Configuration
{
    public class ProviderConfiguration : IEntityTypeConfiguration<Provider>
    {
        public void Configure(EntityTypeBuilder<Provider> builder)
        {

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id)
                .HasColumnName("ProviderId");

            builder.Property(e => e.DocumentNumber)
            .HasMaxLength(20)
            .IsUnicode(false);

            builder.Property(e => e.Email).HasMaxLength(255);

            builder.Property(e => e.Phone).HasMaxLength(15);

            builder.HasOne(d => d.DocumentType)
                .WithMany(p => p.Providers)
                .HasForeignKey(d => d.DocumentTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Providers__Docum__5165187F");
        }
    }
}
