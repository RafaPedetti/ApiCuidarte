using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructura.LogicaAccesoDatos.EF.Config
{
	using LogicaNegocio.Entidades;
	using Microsoft.EntityFrameworkCore;
	using Microsoft.EntityFrameworkCore.Metadata.Builders;

	public class TareaConfiguration : IEntityTypeConfiguration<Tarea>
	{
		public void Configure(EntityTypeBuilder<Tarea> builder)
		{
			builder.ToTable("Tareas");

			builder.HasKey(t => t.Id);

			builder.Property(t => t.fecha)
				.IsRequired();

			builder.Property(t => t.Estado)
				.IsRequired();

			builder.Property(t => t.Descripcion)
				.HasMaxLength(500); // opcional

			builder.HasOne(t => t.Cliente)
				.WithMany()
				.IsRequired();

			builder.HasOne(t => t.EmpleadoResponsable)
				.WithMany()
				.IsRequired();

			builder.HasMany(t => t.serviciosUsados)
				.WithOne()
				.OnDelete(DeleteBehavior.Cascade);

			builder.HasMany(t => t.ServiciosExtras)
				.WithOne()
				.OnDelete(DeleteBehavior.Cascade);

			builder.OwnsOne(t => t.Calificación, cal =>
			{
				cal.Property(c => c.Nota)
					.HasColumnName("CalificacionNota")
					.IsRequired();

				cal.Property(c => c.Comentario)
					.HasColumnName("CalificacionTexto")
					.HasMaxLength(1000);
			});

			builder.Ignore(t => t.CostoTotal);
		}
	}
}
