using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using TesteTecnico.Domain.Entities;

namespace TesteTecnico.Infra.Data.Mapping
{
    public class AirplaneMap : IEntityTypeConfiguration<Airplane>
    {
        public void Configure(EntityTypeBuilder<Airplane> builder)
        {
            builder.ToTable("Airplane");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Model)
                   .IsRequired()
                   .HasColumnName("Model");

            builder.Property(x => x.NumberOfPassengers)
                   .IsRequired()
                   .HasColumnName("NumberOfPassengers");

            builder.Property(x => x.CreatedDate)
                   .IsRequired()
                   .HasColumnName("CreatedDate");
        }
    }
}
