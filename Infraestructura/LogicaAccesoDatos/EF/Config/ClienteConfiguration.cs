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
	internal class ClienteConfiguration : IEntityTypeConfiguration<Cliente>
	{
		public void Configure(EntityTypeBuilder<Cliente> builder)
		{
			builder.OwnsOne(c => c.NombreCompleto, nomC =>
			{
				nomC.Property(n => n.Nombre).HasColumnName("Nombre");
				nomC.Property(n => n.Apellido).HasColumnName("Apellido");
			});


			builder.OwnsOne(c => c.Telefono, telefono =>
			{
				telefono.Property(e => e.Value).HasColumnName("Telefono");
			}
			   );

		}
	}
}
