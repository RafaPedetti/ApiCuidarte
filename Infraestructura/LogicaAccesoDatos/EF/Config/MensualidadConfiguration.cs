using LogicaNegocio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
public class MensualidadConfiguration : IEntityTypeConfiguration<Mensualidad>
{
	public void Configure(EntityTypeBuilder<Mensualidad> builder)
	{
		// Nombre de la tabla
		builder.ToTable("Mensualidades");

		// Clave primaria
		builder.HasKey(m => m.Id);

		// Propiedades requeridas
		builder.Property(m => m.PeriodoDesde)
			   .IsRequired();

		builder.Property(m => m.PeriodoHasta)
			   .IsRequired();
		builder.Property(m => m.Estado)
			   .IsRequired()
			   .HasConversion<string>()
			   .HasMaxLength(20);

		builder.HasOne(m => m.Subscription)
			   .WithMany(s => s.Mensualidades)
			   .HasForeignKey(m => m.SubscriptionId)
			   .OnDelete(DeleteBehavior.Cascade);
	}
}
