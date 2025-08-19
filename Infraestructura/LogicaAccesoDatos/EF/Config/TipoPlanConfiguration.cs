using LogicaNegocio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructura.LogicaAccesoDatos.EF.Config
{
	internal class TipoPlanConfiguration : IEntityTypeConfiguration<TipoPlan>
	{
		public void Configure(EntityTypeBuilder<TipoPlan> builder)
		{
			builder.ToTable("Planes");
			builder.HasKey(p => p.Id);

			builder.Property(p => p.Nombre).IsRequired().HasMaxLength(100);
			builder.Property(p => p.Precio).IsRequired().HasColumnType("decimal(18,2)");

			// Relación Plan → Empresa
			builder.HasOne(p => p.Empresa)
				   .WithOne(e => e.Plan)
				   .OnDelete(DeleteBehavior.Restrict);

			// Relación Plan → Suscripciones
			builder.HasMany(p => p.Suscripciones)
				   .WithOne(s => s.Plan)
				   .HasForeignKey(s => s.PlanId);
		}
	}


}
