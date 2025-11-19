using LogicaNegocio.Entidades;
using Microsoft.EntityFrameworkCore;


namespace Infraestructura.LogicaAccesoDatos.EF.Config
{
	public class FondoPortadaConfiguration : IEntityTypeConfiguration<FondoPortada>
	{
		public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<LogicaNegocio.Entidades.FondoPortada> builder)
		{
			builder.HasKey(fp => fp.Id);
			builder.Property(fp => fp.Url)
				   .IsRequired();
		}
	}
}
