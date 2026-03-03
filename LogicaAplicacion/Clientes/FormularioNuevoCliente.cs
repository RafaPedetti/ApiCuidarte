using LogicaAplicacion.Dtos.Clientes;
using LogicaNegocio.InterfacesServicios;
using LogicaNegocio.ValueObject;

using Microsoft.Extensions.Options;

using Resend;


namespace LogicaAplicacion.Clientes
{
	public class FormularioNuevoCliente : IFormularioNuevoCliente<ClienteFormularioDto>
	{
		private readonly EmailOptions _email;
		private IResend _client;


		public FormularioNuevoCliente(IOptions<EmailOptions> emailOptions)
		{
			_email = emailOptions.Value;
			_client = ResendClient.Create(_email.Key);

		}
		public async Task Ejecutar(ClienteFormularioDto obj)
		{
			var serviciosHtml = string.Join("",
				obj.servicios.Select(s => $"<li>{s.servicio}: {s.horas} horas</li>")
			);

			var htmlBody = $@"
        <html>
          <body style='font-family: Arial, sans-serif; color: #333;'>
            <h2 style='color:#2c3e50;'>Nuevo Cliente</h2>
            <p><strong>Nombre:</strong> {obj.nombre} {obj.apellido}</p>
            <p><strong>Email:</strong> {obj.email}</p>
            <p><strong>Teléfono:</strong> {obj.telefono}</p>
            <p><strong>Fecha de Nacimiento:</strong> {obj.fechaNacimiento.ToShortDateString()}</p>
            <p><strong>Dirección:</strong> {obj.direccion}</p>
            <p><strong>CI:</strong> {obj.ci}</p>
            <p><strong>Responsable de Pago:</strong> {obj.responsablePago}</p>
            <p><strong>Forma de Pago:</strong> {obj.formaPago}</p>
            <p><strong>Observaciones:</strong> {obj.observaciones}</p>
            <h3 style='color:#2980b9;'>Servicios Solicitados</h3>
            <ul>
              {serviciosHtml}
            </ul>
          </body>
        </html>";

			var email = new EmailMessage
			{
				From = _email.FromAddress,
				To = new[] { _email.ToAddress },
				Subject = $"{obj.nombre} {obj.apellido} completo el formulario.",
				HtmlBody = htmlBody,
				TextBody = $"Información cliente: {obj.nombre} {obj.apellido} - {obj.email}"
			};

			var response = await _client.EmailSendAsync(email);
		}
	}

}