using LogicaNegocio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructura.LogicaAccesoDatos.EF.Config
{
	public class EmpresaConfiguration : IEntityTypeConfiguration<Empresa>
	{
		public void Configure(EntityTypeBuilder<Empresa> builder)
		{
			builder.HasKey(e => e.Id);

			builder.HasOne(e => e.Plan)
				   .WithOne(p => p.Empresa)
				   .HasForeignKey<Empresa>(e => e.TipoPlanId)
				   .OnDelete(DeleteBehavior.Restrict);

			builder.HasOne(e => e.Suscripcion)
	   .WithMany()
	   .HasForeignKey(e => e.SuscripcionId)
	   .IsRequired(false)
	   .OnDelete(DeleteBehavior.SetNull);


			builder.Property(e => e.Nombre)
				   .IsRequired()
				   .HasMaxLength(100);

			builder.OwnsOne(c => c.TelefonoContacto, telefono =>
			{
				telefono.Property(e => e.Value).HasColumnName("TelefonoContacto");
			}
			   );

			builder.Property(e => e.Eliminado)
				   .HasDefaultValue(false);
		}

	}
}
