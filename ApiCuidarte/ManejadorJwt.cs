using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using LogicaAplicacion.Dtos.Usuarios;

namespace WebApi
{
    public class ManejadorJwt
    {
		private readonly string _clave;

		public ManejadorJwt(IConfiguration configuration)
		{
			_clave = configuration["Jwt:SecretKey"]
					 ?? throw new Exception("JWT SecretKey no configurada");
		}
		public string GenerarToken(UsuarioDto user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
			var claveBytes = Encoding.ASCII.GetBytes(_clave);
			//clave secreta, generalmente se incluye en el archivo de configuración
			//Debe ser un vector de bytes 


			//Se incluye un claim (privelegios) para el rol

			var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.Role, user.Discriminador)
                }),
                Expires = DateTime.UtcNow.AddMonths(1),

				SigningCredentials = new SigningCredentials(
					new SymmetricSecurityKey(claveBytes),
					SecurityAlgorithms.HmacSha256Signature
				)
			};

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

    }
}
