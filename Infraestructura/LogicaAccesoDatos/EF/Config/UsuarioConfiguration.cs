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
	public class UsuarioConfiguration : IEntityTypeConfiguration<Usuario>
	{
		public void Configure(EntityTypeBuilder<Usuario> builder)
		{

			builder.OwnsOne(u => u.NombreCompleto, nomC =>
			{
				nomC.Property(n => n.Nombre).HasColumnName("Nombre");
				nomC.Property(n => n.Apellido).HasColumnName("Apellido");
			});

			builder.OwnsOne(u => u.Email, email =>
			{
				email.Property(e => e.Value).HasColumnName("Email");
				email.HasIndex(e => e.Value).IsUnique();
			}
		   );
			

			builder.OwnsOne(u => u.Password, pass =>
			{
				pass.Property(e => e.Value).HasColumnName("Password");
			}
			   );
		}
	}
}
