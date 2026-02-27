using LogicaAplicacion.Dtos.Clientes;
using LogicaNegocio.Entidades;
using LogicaNegocio.InterfacesServicios;
using LogicaNegocio.ValueObject;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;


namespace LogicaAplicacion.Clientes
{
	public class FormularioNuevoCliente : IFormularioNuevoCliente<ClienteFormularioDto>
	{
		private readonly EmailOptions _email;

		public FormularioNuevoCliente(IOptions<EmailOptions> emailOptions)
		{
			_email = emailOptions.Value;
		}
		public async Task Ejecutar(ClienteFormularioDto obj)
		{
			Console.Write("Email: " +_email.FromAddress,_email.Password);
			var message = new MimeMessage();
			message.From.Add(new MailboxAddress(
				_email.FromName,
				_email.FromAddress
			));

			message.To.Add(MailboxAddress.Parse(_email.FromAddress));
			message.Subject = $"Nuevo mensaje de {obj.nombre} {obj.apellido}";

			var builder = new BodyBuilder
			{
				TextBody = $@"
				Nombre: {obj.nombre} {obj.apellido}
				Email: {obj.email}
				Teléfono: {obj.telefono}
				Fecha de Nacimiento: {obj.fechaNacimiento.ToShortDateString()}
				Dirección: {obj.direccion}
				CI: {obj.ci}
				Responsable de Pago: {obj.responsablePago}
				Forma de Pago: {obj.formaPago}
				Observaciones: {obj.observaciones}
				Servicios Solicitados:
				{string.Join(Environment.NewLine, obj.servicios.Select(s => $"- {s.servicio}: {s.horas} horas"))}
"
			};

			message.Body = builder.ToMessageBody();

			using var client = new SmtpClient();
			await client.ConnectAsync(
				_email.SmtpHost,
				_email.SmtpPort,
				SecureSocketOptions.StartTls
			);

			await client.AuthenticateAsync(
				_email.FromAddress,
				_email.Password
			);

			await client.SendAsync(message);
			await client.DisconnectAsync(true);
		}
	}
}