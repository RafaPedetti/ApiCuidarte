using LogicaNegocio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

internal class SuscripcionConfiguration : IEntityTypeConfiguration<Suscripcion>
{
	public void Configure(EntityTypeBuilder<Suscripcion> builder)
	{
		builder.ToTable("Suscripciones");
		builder.HasKey(s => s.Id);
		builder.Property(s => s.FechaInicio)
			   .IsRequired();

		builder.Property(s => s.ProximoCobro)
			   .IsRequired();
		builder.Property(s => s.Estado)
			   .IsRequired()
			   .HasConversion<string>()
			   .HasMaxLength(20);

		builder.HasOne(s => s.Plan)
			   .WithMany()
			   .HasForeignKey(s => s.PlanId)
			   .OnDelete(DeleteBehavior.Restrict);

		builder.HasMany(s => s.Clientes)
		.WithOne(c => c.Suscripcion)
		.HasForeignKey(c => c.SuscripcionId)
		.OnDelete(DeleteBehavior.SetNull);
	}
}
